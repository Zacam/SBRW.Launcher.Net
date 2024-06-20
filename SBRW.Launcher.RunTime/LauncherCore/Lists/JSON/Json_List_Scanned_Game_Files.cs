using System.IO;

namespace SBRW.Launcher.Core.Reference.Json_.Newtonsoft_
{
    /// <summary>
    /// JSON Format for Game Files List
    /// </summary>
    /// <remarks><i>Requires <b>Newtonsoft.Json</b> Library</i></remarks>
    public class Json_List_Scanned_Game_Files
    {
        /// <summary>
        /// 
        /// </summary>
        public FileInfo? File_Info { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public FileSystemInfo? File_System_Info { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DirectoryInfo? Directory_Info { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Skip { get; set; }
    }
}