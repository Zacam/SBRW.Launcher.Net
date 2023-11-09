try
{
	#Get Path to csproj
	$path = "$PSScriptRoot\SBRW.Launcher.Net\SBRW.Launcher.csproj"

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

			if ($versionBuildInt % 2 -eq 0) 
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
}
catch
{
	throw $_
	return
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
	$stringWriter.ToString() | Out-File $path -Force

	echo ([string]::Format("========== New Version Number: {0}.{1}.{2} ==========",$xml.Project.PropertyGroup.VersionMajor,$xml.Project.PropertyGroup.VersionMinor,$xml.Project.PropertyGroup.VersionBuild))
}
catch
{
	throw $_
	return
}