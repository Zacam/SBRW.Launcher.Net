param(
    [Parameter(Mandatory=$false)]
    [ValidateSet("0", "1", "2")]
    [string]$EnvironmentCode
)

try
{
    # Map numeric values to corresponding environments
    $EnvironmentMapping = @{
        "0" = "Release"
        "1" = "Beta"
        "2" = "Development"
    }

    # Get the corresponding environment based on the provided code
    $Environment = $EnvironmentMapping[$EnvironmentCode]
    $EnvironmentInt = 0

    #if(!(-not $Environment))
    #{
        #Write-Host "========== Selected environment: $Environment =========="
    #}

    if ($Environment -eq "Beta") 
    {
        echo "========== Beta Build Mode =========="
        $EnvironmentInt = 1
    }
    elseif ($Environment -eq "Development") 
    {
        echo "========== Development Build Mode =========="
        $EnvironmentInt = 2
    }
    else 
    {
        echo "========== Release Build Mode =========="
        $EnvironmentInt = 0
    }
}
catch
{
    throw $_
    exit 1
}

#Set for Development Console Release
$currentDate = $null
$currentTime = $null

try
{
    # Get Path to C# class file
    $buildInformationClassFilePath = "$PSScriptRoot\SBRW.Launcher.RunTime\InsiderKit\BuildInformation.cs"

    # Read the C# class file
    $buildInformationClassFileContent = Get-Content $buildInformationClassFilePath -Raw

    # Update the constant variable BUILD_DATE with the current date
    $currentDate = Get-Date -Format "MM-dd-yyyy"
    $newBuildInfoContent = $buildInformationClassFileContent -replace 'const\s+string\s+DATE\s*=\s*".*";', "const string DATE = `"$currentDate`";"

    # Update the constant variable BUILD_TIME with the current UTC time
    $currentTime = Get-Date -Format "HHmmss"
    $newBuildInfoContent = $newBuildInfoContent -replace 'const\s+string\s+TIME\s*=\s*".*";', "const string TIME = `"$currentTime`";"

    $currentTimeZone = Get-Date -Format "zzz"
    $newBuildInfoContent = $newBuildInfoContent -replace 'const\s+string\s+TIME_ZONE\s*=\s*".*";', "const string TIME_ZONE = `"$currentTimeZone`";"

    # Save the updated content back to the C# class file
    $newBuildInfoContent | Set-Content $buildInformationClassFilePath -NoNewline

    echo ([string]::Format("========== DATE updated to: {0}  ==========", $currentDate))
	echo ([string]::Format("========== TIME updated to: {0}  ==========", $currentTime))
	echo ([string]::Format("========== TIME_ZONE updated to: {0}  ==========", $currentTimeZone))
}
catch
{
    throw $_
    exit 1
}

try
{
    # Get Path to C# class files
    $buildDevelopmentClassFilePath = "$PSScriptRoot\SBRW.Launcher.RunTime\InsiderKit\BuildDevelopment.cs"
	$buildBetaClassFilePath = "$PSScriptRoot\SBRW.Launcher.RunTime\InsiderKit\BuildBeta.cs"
    
	# Read the C# class file
    $buildDevelopmentFileContent = Get-Content $buildDevelopmentClassFilePath -Raw
	$buildBetaFileContent = Get-Content $buildBetaClassFilePath -Raw

	# Set Conditionals
	$BuildDevBoolean = "false"
	$BuildBetaBoolean = "false"

	if ($EnvironmentInt -eq 1)
	{
		$BuildDevBoolean = "false"
		$BuildBetaBoolean = "true"
	}
	elseif ($EnvironmentInt -eq 2)
	{
		$BuildDevBoolean = "true"
		$BuildBetaBoolean = "false"
	}

    # Update the value
    $newBuildDevContent = $buildDevelopmentFileContent -replace 'private static bool Enabled = .*;', ([string]::Format('private static bool Enabled = {0};', $BuildDevBoolean))
	$newBuildBetaContent = $buildBetaFileContent -replace 'private static bool Enabled = .*;', ([string]::Format('private static bool Enabled = {0};', $BuildBetaBoolean))

    # Save the updated content back to the C# class file
    $newBuildDevContent | Set-Content $buildDevelopmentClassFilePath -NoNewline
	$newBuildBetaContent | Set-Content $buildBetaClassFilePath -NoNewline

    echo ([string]::Format("========== Development Enabled: {0}  ==========", $BuildDevBoolean))
	echo ([string]::Format("========== Beta Enabled: {0}  ==========", $BuildBetaBoolean))
}
catch
{
    throw $_
    exit 1
}

function Update-Version 
{
    param (
        [int]$LiveEnvironmentStatus,
        [string]$LiveFilePath
    )

	#Get Path to csproj
	$path = $LiveFilePath

	#Read csproj (XML)
	$xml = [xml](Get-Content $path)

	#Retrieve Version Nodes

	#Version Major
	$versionMajor = $xml.Project.PropertyGroup.VersionMajor
	$versionMajorUpgrade = $xml.Project.PropertyGroup.VersionMajorUpgrade

	#Version Minor
	$versionMinor = $xml.Project.PropertyGroup.VersionMinor
	$versionMinorUpgrade = $xml.Project.PropertyGroup.VersionMinorUpgrade

	#Version Build
	$versionBuild = $xml.Project.PropertyGroup.VersionBuild

	#Increment Build Version - Will reset all sub nodes

	#Create Temporary Boolean for String Null Checks
	$tempBool = $null

	#Convert String to Booleans
	if ([bool]::TryParse($versionMajorUpgrade, [ref]$tempBool)) 
	{
		# parsed to a boolean
		$versionMajorUpgrade = $tempBool
	}
	else
	{
		$versionMajorUpgrade = $false
	}

	$tempBool = $null

	if ([bool]::TryParse($versionMinorUpgrade, [ref]$tempBool)) 
	{
		# parsed to a boolean
		$versionMinorUpgrade = $tempBool
	}
	else
	{
		$versionMinorUpgrade = $false
	}

	$tempBool = $null

	#Update Version Numbers & Put new version back into csproj (XML)
	if ($versionMajorUpgrade)
	{
		$xml.Project.PropertyGroup.VersionMajor = ([Convert]::ToInt32($versionMajor,10)+1).ToString()
		$xml.Project.PropertyGroup.VersionMajorUpgrade = "false"
		$xml.Project.PropertyGroup.VersionMinor = "0"
		$xml.Project.PropertyGroup.VersionMinorUpgrade = "false"
		$xml.Project.PropertyGroup.VersionBuild = "0"
	}
	else
	{
		if ($versionMinorUpgrade)
		{
			$xml.Project.PropertyGroup.VersionMinor = ([Convert]::ToInt32($versionMinor,10)+1).ToString()
			$xml.Project.PropertyGroup.VersionMinorUpgrade = "false"
			$xml.Project.PropertyGroup.VersionBuild = "0"
		}
		else
		{
			# Check if VersionBuild is even or odd
			$versionBuildInt = [Convert]::ToInt32($versionBuild, 10)

			if ($LiveEnvironmentStatus -eq 0 -and $versionBuildInt % 2 -eq 0) 
			{
				# Even number, increment to the next even number (Should be on RELEASE MODE)
				$versionBuildInt += 2
			}
			else
			{
				# Odd number, increment to the next odd number (Should be on DEBUG MODE)
				$versionBuildInt++
			}

			$xml.Project.PropertyGroup.VersionBuild = $versionBuildInt.ToString()
		}
	}

	# File Save with Formatting Settings
	try
	{
		# Save csproj (XML) with preserving formatting
		$settings = New-Object System.Xml.XmlWriterSettings
		$settings.Indent = $true
		$settings.IndentChars = "    "  # You can adjust the number of spaces for indentation

		# Create a StringWriter to preserve formatting
		$stringWriter = New-Object System.IO.StringWriter
		$xmlWriter = [System.Xml.XmlWriter]::Create($stringWriter, $settings)

		# Save XML (Cache) and Cleanup
		$xml.Save($xmlWriter)
		$xmlWriter.Close()

		#Save csproj (Formatted XML)
		$stringWriter.ToString() | Out-File $path -NoNewline

		echo ([string]::Format("========== New Version Number: {0}.{1}.{2} ==========",$xml.Project.PropertyGroup.VersionMajor,$xml.Project.PropertyGroup.VersionMinor,$xml.Project.PropertyGroup.VersionBuild))
	}
	catch
	{
		throw $_
		exit 1
	}
}

try
{
	if (!($EnvironmentInt -eq 2))
	{
		Update-Version -LiveEnvironmentStatus $EnvironmentInt -LiveFilePath "$PSScriptRoot\SBRW.Launcher.Net\SBRW.Launcher.csproj"
		Update-Version -LiveEnvironmentStatus $EnvironmentInt -LiveFilePath "$PSScriptRoot\GameLauncher\GameLauncher.csproj"
	}
	else
	{
		echo ([string]::Format("========== New Version Number: {0}-{1} ==========",$currentDate, $currentTime))
	}
}
catch
{
	throw $_
	exit 1
}

try
{
	if ($EnvironmentInt -eq 2)
	{
		echo "========== Building DEBUG Windows Build =========="
		Invoke-Expression "dotnet build --configuration Debug"
		echo "========== Building DEBUG Unix Build =========="
		Invoke-Expression "dotnet build --configuration Debug.Unix"
	}
	else
	{
		echo "========== Building RELEASE Windows Build =========="
		Invoke-Expression "dotnet build --configuration Release"
		echo "========== Building RELEASE Unix Build =========="
		Invoke-Expression "dotnet build --configuration Release.Unix"
	}
}
catch
{
	throw $_
	exit 1
}