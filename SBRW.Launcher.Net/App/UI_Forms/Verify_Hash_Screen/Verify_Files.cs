using SBRW.Launcher.Core.Extension.Hash_;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace SBRW.Launcher.App.UI_Forms.Verify_Hash_Screen
{
    public class Verify_Files
    {
        private readonly Verify_Config _config;
        private readonly Verify_ILogger _logger;
        private readonly HttpClient _httpClient;

        // Internal helper struct for checksums
        private struct FileChecksum
        {
            public string Hash;
            public string RelativePath;
        }

        public Verify_Files(Verify_Config config, Verify_ILogger logger, HttpClient httpClient)
        {
            _config = config;
            _logger = logger;
            _httpClient = httpClient;
        }

        /// <summary>
        /// Runs the entire verification, cleanup, and download process.
        /// </summary>
        public async Task<Verify_Model_Verification_Result> RunAsync(IProgress<Verify_Model_Verification_Progress> progress, CancellationToken token)
        {
            var result = new Verify_Model_Verification_Result();
            try
            {
                await CleanupDirectoryAsync(progress, token);

                token.ThrowIfCancellationRequested();

                var checksums = await DownloadAndParseChecksumsAsync(progress, token);
                if (checksums == null)
                {
                    progress.Report(new Verify_Model_Verification_Progress { Message = "Failed to download checksums. Stopping." });
                    return result; // Early exit
                }

                token.ThrowIfCancellationRequested();

                var invalidFiles = await ScanFilesAsync(checksums, result, progress, token);
                result.TotalFilesMissingOrInvalid = invalidFiles.Count;

                token.ThrowIfCancellationRequested();

                if (invalidFiles.Any())
                {
                    progress.Report(new Verify_Model_Verification_Progress { Message = "Downloading invalid or missing files..." });
                    await DownloadInvalidFilesAsync(invalidFiles, result, progress, token);
                }

                progress.Report(new Verify_Model_Verification_Progress { Message = "Verification complete.", Percentage = 100 });
            }
            catch (OperationCanceledException)
            {
                result.WasCancelled = true;
                progress.Report(new Verify_Model_Verification_Progress { Message = "Operation cancelled by user." });
                _logger.Info("Verification process was cancelled by the user.");
            }
            catch (Exception ex)
            {
                progress.Report(new Verify_Model_Verification_Progress { Message = $"An error occurred: {ex.Message}" });
                _logger.Error("An unhandled exception occurred during verification.", ex);
            }
            return result;
        }

        private async Task CleanupDirectoryAsync(IProgress<Verify_Model_Verification_Progress> progress, CancellationToken token)
        {
            progress.Report(new Verify_Model_Verification_Progress { Message = "Cleaning game directory..." });
            _logger.Info("Starting game directory cleanup.");

            var gameDir = new DirectoryInfo(_config.GamePath);
            if (!gameDir.Exists) return;

            // 1. Delete .orig files
            var origFiles = gameDir.EnumerateFiles("*.orig", SearchOption.AllDirectories);
            foreach (var file in origFiles)
            {
                token.ThrowIfCancellationRequested();
                try
                {
                    file.Delete();
                    _logger.Info($"Deleted '.orig' file: {file.FullName}");
                }
                catch (Exception ex)
                {
                    _logger.Error($"Failed to delete file: {file.FullName}", ex);
                }
            }

            // 2. Delete symbolic links (More robust check)
            var allEntries = gameDir.EnumerateFileSystemInfos("*", SearchOption.AllDirectories);
            foreach (var entry in allEntries)
            {
                token.ThrowIfCancellationRequested();
                if (entry.Attributes.HasFlag(FileAttributes.ReparsePoint))
                {
                    try
                    {
                        if (Directory.Exists(entry.FullName))
                        {
                            Directory.Delete(entry.FullName, true);
                        }
                        else
                        {
                            File.Delete(entry.FullName);
                        }
                        _logger.Info($"Deleted symbolic link: {entry.FullName}");
                    }
                    catch (Exception ex)
                    {
                        _logger.Error($"Failed to delete symbolic link: {entry.FullName}", ex);
                    }
                }
            }

            // 3. Clean 'scripts' folder if configured
            if (_config.CleanScriptsFolder)
            {
                var scriptsDir = new DirectoryInfo(Path.Combine(_config.GamePath, "scripts"));
                if (scriptsDir.Exists)
                {
                    foreach (var file in scriptsDir.EnumerateFiles())
                    {
                        if (file.Name.Equals("LangPicker.ini", StringComparison.OrdinalIgnoreCase))
                            continue;

                        try
                        {
                            file.Delete();
                            _logger.Info($"Deleted script file: {file.Name}");
                        }
                        catch (Exception ex)
                        {
                            _logger.Error($"Failed to delete script file: {file.Name}", ex);
                        }
                    }
                }
            }

            _logger.Info("Cleanup complete.");
        }

        private async Task<List<FileChecksum>?> DownloadAndParseChecksumsAsync(IProgress<Verify_Model_Verification_Progress> progress, CancellationToken token)
        {
            progress.Report(new Verify_Model_Verification_Progress { Message = "Downloading checksum manifest..." });

            string checksumUrl = $"{_config.CdnBaseUrl.TrimEnd('/')}/{_config.CdnUnpackedPath.Trim('/')}/{_config.ChecksumFileName}";
            string[] lines;

            try
            {
                using (var response = await _httpClient.GetAsync(checksumUrl, token))
                {
                    response.EnsureSuccessStatusCode();
                    string content = await response.Content.ReadAsStringAsync();
                    lines = content.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Failed to download checksum file from {checksumUrl}", ex);
                return null;
            }

            var checksums = new List<FileChecksum>();
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(new[] { ' ' }, 2);
                if (parts.Length == 2)
                {
                    checksums.Add(new FileChecksum
                    {
                        Hash = parts[0].Trim(),
                        RelativePath = parts[1].Trim()
                    });
                }
            }

            _logger.Info($"Parsed {checksums.Count} file checksums.");
            return checksums;
        }

        private async Task<List<string>> ScanFilesAsync(List<FileChecksum> checksums, Verify_Model_Verification_Result result, IProgress<Verify_Model_Verification_Progress> progress, CancellationToken token)
        {
            _logger.Info("Starting file scan...");
            var invalidFiles = new List<string>();
            int filesScanned = 0;

            foreach (var item in checksums)
            {
                token.ThrowIfCancellationRequested();
                filesScanned++;
                string localPath = Path.Combine(_config.GamePath, item.RelativePath.TrimStart('\\', '/'));

                if (!File.Exists(localPath))
                {
                    _logger.Warn($"Missing file: {item.RelativePath}");
                    invalidFiles.Add(item.RelativePath);
                }
                else
                {
                    string localHash = await CalculateFileHashAsync(localPath);
                    if (!localHash.Equals(item.Hash, StringComparison.OrdinalIgnoreCase))
                    {
                        _logger.Warn($"Invalid hash for file: {item.RelativePath} (Expected: {item.Hash}, Got: {localHash})");
                        invalidFiles.Add(item.RelativePath);
                    }
                }

                int percent = filesScanned * 100 / checksums.Count;
                progress.Report(new Verify_Model_Verification_Progress
                {
                    Message = $"Scanning files: {percent}%",
                    CurrentFile = item.RelativePath,
                    Percentage = percent
                });
            }

            result.TotalFilesScanned = filesScanned;
            _logger.Info($"Scan complete. Found {invalidFiles.Count} invalid or missing files.");
            return invalidFiles;
        }

        private async Task DownloadInvalidFilesAsync(List<string> invalidFiles, Verify_Model_Verification_Result result, IProgress<Verify_Model_Verification_Progress> progress, CancellationToken token)
        {
            int filesDownloaded = 0;
            int totalToDownload = invalidFiles.Count;

            foreach (var relativePath in invalidFiles)
            {
                token.ThrowIfCancellationRequested();

                string fileUrl = $"{_config.CdnBaseUrl.TrimEnd('/')}/{_config.CdnUnpackedPath.Trim('/')}{relativePath.Replace("\\", "/")}";
                string localPath = Path.Combine(_config.GamePath, relativePath.TrimStart('\\', '/'));

                try
                {
                    // Ensure directory exists
                    Directory.CreateDirectory(Path.GetDirectoryName(localPath)!);

                    // Delete old file if it exists
                    if (File.Exists(localPath))
                    {
                        File.Delete(localPath);
                    }

                    _logger.Info($"Downloading: {relativePath}");

                    // Download file with progress
                    await DownloadFileWithProgressAsync(fileUrl, localPath, relativePath, (filesDownloaded + 1), totalToDownload, progress, token);

                    filesDownloaded++;
                    result.FilesSuccessfullyDownloaded = filesDownloaded;
                }
                catch (Exception ex)
                {
                    if (ex is OperationCanceledException) throw; // Re-throw cancellation

                    _logger.Error($"Failed to download file: {relativePath} from {fileUrl}", ex);
                    result.FilesFailedToDownload++;
                    result.FailedDownloadFiles.Add(relativePath);
                }
            }
        }

        private async Task DownloadFileWithProgressAsync(string url, string destinationPath, string relativePath, int currentFileNum, int totalFiles, IProgress<Verify_Model_Verification_Progress> progress, CancellationToken token)
        {
            using (var response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, token))
            {
                response.EnsureSuccessStatusCode();

                long? totalBytes = response.Content.Headers.ContentLength;
                long totalBytesRead = 0;

                using (var contentStream = await response.Content.ReadAsStreamAsync())
                using (var fileStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true))
                {
                    var buffer = new byte[8192];
                    int bytesRead;
                    while ((bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length, token)) > 0)
                    {
                        await fileStream.WriteAsync(buffer, 0, bytesRead, token);
                        totalBytesRead += bytesRead;

                        if (totalBytes.HasValue)
                        {
                            progress.Report(new Verify_Model_Verification_Progress
                            {
                                Message = $"Downloading [{currentFileNum}/{totalFiles}]:",
                                CurrentFile = relativePath,
                                Percentage = (int)(totalBytesRead * 100 / totalBytes.Value),
                                BytesDownloaded = totalBytesRead,
                                TotalBytesToDownload = totalBytes.Value
                            });
                        }
                    }
                }
            }
        }

        private async Task<string> CalculateFileHashAsync(string filePath)
        {
            string Converted_Hash = string.Empty;

            await Task.Run(() =>
            {
                Converted_Hash = Hashes.Hash_SHA(filePath).Trim();
            });

            return Converted_Hash;
        }
    }
}