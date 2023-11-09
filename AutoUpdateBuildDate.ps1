try
{
    # Get Path to C# class file
    $classFilePath = "$PSScriptRoot\SBRW.Launcher.RunTime\InsiderKit\KitEnabler.cs"

    # Read the C# class file
    $classFileContent = Get-Content $classFilePath -Raw

    # Update the constant variable BUILD_DATE with the current date
    $currentDate = Get-Date -Format "MM-dd-yyyy"
    $newContent = $classFileContent -replace 'const\s+string\s+BUILD_DATE\s*=\s*".*"', "const string BUILD_DATE = `"$currentDate`";"

    # Update the constant variable BUILD_TIME with the current UTC time
    $currentTime = Get-Date -Format "HHmmss"
    $newContent = $newContent -replace 'const\s+string\s+BUILD_TIME\s*=\s*".*"', "const string BUILD_TIME = `"$currentTime`";"

    $currentTimeZone = Get-Date -Format "zzz"
    $newContent = $newContent -replace 'const\s+string\s+BUILD_TIME_ZONE\s*=\s*".*"', "const string BUILD_TIME_ZONE = `"$currentTimeZone`";"

    # Save the updated content back to the C# class file
    $newContent | Set-Content $classFilePath -Force

    echo "========== BUILD_DATE updated to: $currentDate =========="
    echo "========== BUILD_TIME updated to: $currentTime =========="
    echo "========== BUILD_TIME_ZONE updated to: $currentTimeZone =========="
}
catch
{
    throw $_
    return
}