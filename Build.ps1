param(
    [Parameter(Mandatory=$true)]
    [ValidateSet("0", "1", "2", "3", "4", "5")]
    [string]$CompileMode
)

try
{
    # Map numeric values to corresponding environments
    $CompileModeMapping = @{
        "0" = "Release"
        "1" = "Beta"
        "2" = "Development"
        "3" = "Information (Release)"
        "4" = "Information (Beta)"
        "5" = "Information (Development)"
    }

    # Get the corresponding environment based on the provided code
    $Environment = $CompileModeMapping[$CompileMode]
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
    elseif ($Environment -eq "Information (Release)") 
    {
        echo "========== Information (Release) Build Mode =========="
        $EnvironmentInt = 3
    }
    elseif ($Environment -eq "Information (Beta)") 
    {
        echo "========== Information (Beta) Build Mode =========="
        $EnvironmentInt = 4
    }
    elseif ($Environment -eq "Information (Development)") 
    {
        echo "========== Information (Development) Build Mode =========="
        $EnvironmentInt = 5
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

	# Create a Short Version of Current Date
	$currentDateShort = Get-Date -Format "MM-dd-yy"
    $newBuildInfoContent = $newBuildInfoContent -replace 'const\s+string\s+DATE_SHORT\s*=\s*".*";', "const string DATE_SHORT = `"$currentDateShort`";"

    # Update the constant variable BUILD_TIME with the current UTC time
    $currentTime = Get-Date -Format "HHmm"
    $newBuildInfoContent = $newBuildInfoContent -replace 'const\s+string\s+TIME\s*=\s*".*";', "const string TIME = `"$currentTime`";"

	$currentTimeSeconds = Get-Date -Format "ss"
	$newBuildInfoContent = $newBuildInfoContent -replace 'const\s+string\s+TIME_SECONDS\s*=\s*".*";', "const string TIME_SECONDS = `"$currentTimeSeconds`";"

    $currentTimeZone = Get-Date -Format "zzz"
    $newBuildInfoContent = $newBuildInfoContent -replace 'const\s+string\s+TIME_ZONE\s*=\s*".*";', "const string TIME_ZONE = `"$currentTimeZone`";"

    # Save the updated content back to the C# class file
    $newBuildInfoContent | Set-Content $buildInformationClassFilePath -NoNewline

    echo ([string]::Format("========== DATE updated to: {0}  ==========", $currentDate))
	echo ([string]::Format("========== DATE_SHORT updated to: {0}  ==========", $currentDate))
	echo ([string]::Format("========== TIME updated to: {0}  ==========", $currentTime))
	echo ([string]::Format("========== TIME_SECONDS updated to: {0}  ==========", $currentTimeSeconds))
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

	# Beta Builds
	if (($EnvironmentInt -eq 1) -or ($EnvironmentInt -eq 4))
	{
		$BuildDevBoolean = "false"
		$BuildBetaBoolean = "true"
	}
	# Development Builds
	elseif (($EnvironmentInt -eq 2) -or ($EnvironmentInt -eq 5))
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
	if (!(($EnvironmentInt -eq 2) -or ($EnvironmentInt -eq 3) -or ($EnvironmentInt -eq 4) -or ($EnvironmentInt -eq 5)))
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
	# Development Builds
	if (($EnvironmentInt -eq 2) -or ($EnvironmentInt -eq 5))
	{
		# We need Debug Symbols so Lets Build in Debug Mode
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
# SIG # Begin signature block
# MIIlkwYJKoZIhvcNAQcCoIIlhDCCJYACAQExCzAJBgUrDgMCGgUAMGkGCisGAQQB
# gjcCAQSgWzBZMDQGCisGAQQBgjcCAR4wJgIDAQAABBAfzDtgWUsITrck0sYpfvNR
# AgEAAgEAAgEAAgEAAgEAMCEwCQYFKw4DAhoFAAQUTbDuijioVlu0iLU4t11ltJ3s
# AlOggh/lMIIEfjCCA2agAwIBAgIIdRbquLTQwT0wDQYJKoZIhvcNAQELBQAwRzEL
# MAkGA1UEBhMCVVMxGzAZBgNVBAoTElNvYXBib3ggUmFjZSBXb3JsZDEbMBkGA1UE
# AxMSU29hcGJveCBSYWNlIFdvcmxkMB4XDTI0MDMyMDA3MTMwMFoXDTI1MDMyMDA3
# MTMwMFowajEbMBkGA1UEChMSU29hcGJveCBSYWNlIFdvcmxkMRowGAYDVQQLDBFH
# YW1lTGF1bmNoZXJfTkZTVzEvMC0GA1UEAxMmU29hcGJveCBSYWNlIFdvcmxkIC0g
# TGF1bmNoZXIgRGl2aXNpb24wggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIB
# AQDKKHfSgvl+ngzlqGCuxhlTR/FfEAB8UD+fPWqwZiVCaqKL5JWH1rBH1vA91Dqw
# i+kxUcAn1g9zBDeSJCmVeHnwbKYTBBzZxtyeVePc5LnxuWMRxru+8iTfp6qzX+/y
# J/YUZSNq8ReYc+kYtsJYSb9qOf24lyZePJR9X4S0/1czRPSlkPh+UkBjPJsf7iE8
# G6TNrhu0qIctE4B1zXZP4M4lSLVO0eTb8z/8dqHESPV1duhwJEKyR16SNcHEmg3T
# crKTc8Pplq6r+JZxo3xz5TA3xnxZqWGWG+kc6hWOUVVOPUKKJnAooD+LHHvRMrWN
# cCNW/wwmMGqVcWUbHcDy4873AgMBAAGjggFJMIIBRTAJBgNVHRMEAjAAMB0GA1Ud
# DgQWBBRb+8d23RiYXXSAZjPTkNb3nVedWDALBgNVHQ8EBAMCAoQwQQYDVR0lBDow
# OAYIKwYBBQUHAwMGCCsGAQUFBwMIBgorBgEEAYI3AgEVBgorBgEEAYI3AgEWBgor
# BgEEAYI3CgMBMC8GA1UdEQQoMCaCDmNhcmJvbmNyZXcub3JnghRkYXZpZGNhcmJv
# bi5kb3dubG9hZDAvBgNVHRIEKDAmgg5jYXJib25jcmV3Lm9yZ4IUZGF2aWRjYXJi
# b24uZG93bmxvYWQwZwYDVR0fBGAwXjAqoCigJoYkaHR0cDovL2NybC5jYXJib25j
# cmV3Lm9yZy9DQ1AtQ0EuY3JsMDCgLqAshipodHRwOi8vY3JsLmRhdmlkY2FyYm9u
# LmRvd25sb2FkL0NDUC1DQS5jcmwwDQYJKoZIhvcNAQELBQADggEBAG7SPOvheGfl
# 5bV7vqYuB/yrFLd0FQiZcHnnMxLcW91W12KI7NNgpovsEIR1Vc32VRH0tcIADoNY
# s/+Eirjgiurz2Mtqz3yioTPuZmVVESWRxip9CdVea9xh2woDi85VH/EyPRHdAIZY
# WFxuByi+UymBTUKE83gbojIofYqwCxRsdZ26wKtkLnrMsHI0ia7uU74oogBsG41y
# /m+eaCTo5vqCGAXG07g5DqfZd5AW5Wq5zrmWeYJABgMIXsBfS/XQmVxmPpPHHNVj
# fqDhLDfIJUuEaTqq0aHtHJ5qVm7d/NmX4oo6yHFsXaPec7NClBNA1y+WMNP3ZOXp
# EFU7jfnwqWYwggWNMIIEdaADAgECAhAOmxiO+dAt5+/bUOIIQBhaMA0GCSqGSIb3
# DQEBDAUAMGUxCzAJBgNVBAYTAlVTMRUwEwYDVQQKEwxEaWdpQ2VydCBJbmMxGTAX
# BgNVBAsTEHd3dy5kaWdpY2VydC5jb20xJDAiBgNVBAMTG0RpZ2lDZXJ0IEFzc3Vy
# ZWQgSUQgUm9vdCBDQTAeFw0yMjA4MDEwMDAwMDBaFw0zMTExMDkyMzU5NTlaMGIx
# CzAJBgNVBAYTAlVTMRUwEwYDVQQKEwxEaWdpQ2VydCBJbmMxGTAXBgNVBAsTEHd3
# dy5kaWdpY2VydC5jb20xITAfBgNVBAMTGERpZ2lDZXJ0IFRydXN0ZWQgUm9vdCBH
# NDCCAiIwDQYJKoZIhvcNAQEBBQADggIPADCCAgoCggIBAL/mkHNo3rvkXUo8MCIw
# aTPswqclLskhPfKK2FnC4SmnPVirdprNrnsbhA3EMB/zG6Q4FutWxpdtHauyefLK
# EdLkX9YFPFIPUh/GnhWlfr6fqVcWWVVyr2iTcMKyunWZanMylNEQRBAu34LzB4Tm
# dDttceItDBvuINXJIB1jKS3O7F5OyJP4IWGbNOsFxl7sWxq868nPzaw0QF+xembu
# d8hIqGZXV59UWI4MK7dPpzDZVu7Ke13jrclPXuU15zHL2pNe3I6PgNq2kZhAkHnD
# eMe2scS1ahg4AxCN2NQ3pC4FfYj1gj4QkXCrVYJBMtfbBHMqbpEBfCFM1LyuGwN1
# XXhm2ToxRJozQL8I11pJpMLmqaBn3aQnvKFPObURWBf3JFxGj2T3wWmIdph2PVld
# QnaHiZdpekjw4KISG2aadMreSx7nDmOu5tTvkpI6nj3cAORFJYm2mkQZK37AlLTS
# YW3rM9nF30sEAMx9HJXDj/chsrIRt7t/8tWMcCxBYKqxYxhElRp2Yn72gLD76GSm
# M9GJB+G9t+ZDpBi4pncB4Q+UDCEdslQpJYls5Q5SUUd0viastkF13nqsX40/ybzT
# QRESW+UQUOsxxcpyFiIJ33xMdT9j7CFfxCBRa2+xq4aLT8LWRV+dIPyhHsXAj6Kx
# fgommfXkaS+YHS312amyHeUbAgMBAAGjggE6MIIBNjAPBgNVHRMBAf8EBTADAQH/
# MB0GA1UdDgQWBBTs1+OC0nFdZEzfLmc/57qYrhwPTzAfBgNVHSMEGDAWgBRF66Kv
# 9JLLgjEtUYunpyGd823IDzAOBgNVHQ8BAf8EBAMCAYYweQYIKwYBBQUHAQEEbTBr
# MCQGCCsGAQUFBzABhhhodHRwOi8vb2NzcC5kaWdpY2VydC5jb20wQwYIKwYBBQUH
# MAKGN2h0dHA6Ly9jYWNlcnRzLmRpZ2ljZXJ0LmNvbS9EaWdpQ2VydEFzc3VyZWRJ
# RFJvb3RDQS5jcnQwRQYDVR0fBD4wPDA6oDigNoY0aHR0cDovL2NybDMuZGlnaWNl
# cnQuY29tL0RpZ2lDZXJ0QXNzdXJlZElEUm9vdENBLmNybDARBgNVHSAECjAIMAYG
# BFUdIAAwDQYJKoZIhvcNAQEMBQADggEBAHCgv0NcVec4X6CjdBs9thbX979XB72a
# rKGHLOyFXqkauyL4hxppVCLtpIh3bb0aFPQTSnovLbc47/T/gLn4offyct4kvFID
# yE7QKt76LVbP+fT3rDB6mouyXtTP0UNEm0Mh65ZyoUi0mcudT6cGAxN3J0TU53/o
# Wajwvy8LpunyNDzs9wPHh6jSTEAZNUZqaVSwuKFWjuyk1T3osdz9HNj0d1pcVIxv
# 76FQPfx2CWiEn2/K2yCNNWAcAgPLILCsWKAOQGPFmCLBsln1VWvPJ6tsds5vIy30
# fnFqI2si/xK4VC0nftg62fC2h5b9W9FcrBjDTZ9ztwGpn1eqXijiuZQwggauMIIE
# lqADAgECAhAHNje3JFR82Ees/ShmKl5bMA0GCSqGSIb3DQEBCwUAMGIxCzAJBgNV
# BAYTAlVTMRUwEwYDVQQKEwxEaWdpQ2VydCBJbmMxGTAXBgNVBAsTEHd3dy5kaWdp
# Y2VydC5jb20xITAfBgNVBAMTGERpZ2lDZXJ0IFRydXN0ZWQgUm9vdCBHNDAeFw0y
# MjAzMjMwMDAwMDBaFw0zNzAzMjIyMzU5NTlaMGMxCzAJBgNVBAYTAlVTMRcwFQYD
# VQQKEw5EaWdpQ2VydCwgSW5jLjE7MDkGA1UEAxMyRGlnaUNlcnQgVHJ1c3RlZCBH
# NCBSU0E0MDk2IFNIQTI1NiBUaW1lU3RhbXBpbmcgQ0EwggIiMA0GCSqGSIb3DQEB
# AQUAA4ICDwAwggIKAoICAQDGhjUGSbPBPXJJUVXHJQPE8pE3qZdRodbSg9GeTKJt
# oLDMg/la9hGhRBVCX6SI82j6ffOciQt/nR+eDzMfUBMLJnOWbfhXqAJ9/UO0hNoR
# 8XOxs+4rgISKIhjf69o9xBd/qxkrPkLcZ47qUT3w1lbU5ygt69OxtXXnHwZljZQp
# 09nsad/ZkIdGAHvbREGJ3HxqV3rwN3mfXazL6IRktFLydkf3YYMZ3V+0VAshaG43
# IbtArF+y3kp9zvU5EmfvDqVjbOSmxR3NNg1c1eYbqMFkdECnwHLFuk4fsbVYTXn+
# 149zk6wsOeKlSNbwsDETqVcplicu9Yemj052FVUmcJgmf6AaRyBD40NjgHt1bicl
# kJg6OBGz9vae5jtb7IHeIhTZgirHkr+g3uM+onP65x9abJTyUpURK1h0QCirc0PO
# 30qhHGs4xSnzyqqWc0Jon7ZGs506o9UD4L/wojzKQtwYSH8UNM/STKvvmz3+Drhk
# Kvp1KCRB7UK/BZxmSVJQ9FHzNklNiyDSLFc1eSuo80VgvCONWPfcYd6T/jnA+bIw
# pUzX6ZhKWD7TA4j+s4/TXkt2ElGTyYwMO1uKIqjBJgj5FBASA31fI7tk42PgpuE+
# 9sJ0sj8eCXbsq11GdeJgo1gJASgADoRU7s7pXcheMBK9Rp6103a50g5rmQzSM7TN
# sQIDAQABo4IBXTCCAVkwEgYDVR0TAQH/BAgwBgEB/wIBADAdBgNVHQ4EFgQUuhbZ
# bU2FL3MpdpovdYxqII+eyG8wHwYDVR0jBBgwFoAU7NfjgtJxXWRM3y5nP+e6mK4c
# D08wDgYDVR0PAQH/BAQDAgGGMBMGA1UdJQQMMAoGCCsGAQUFBwMIMHcGCCsGAQUF
# BwEBBGswaTAkBggrBgEFBQcwAYYYaHR0cDovL29jc3AuZGlnaWNlcnQuY29tMEEG
# CCsGAQUFBzAChjVodHRwOi8vY2FjZXJ0cy5kaWdpY2VydC5jb20vRGlnaUNlcnRU
# cnVzdGVkUm9vdEc0LmNydDBDBgNVHR8EPDA6MDigNqA0hjJodHRwOi8vY3JsMy5k
# aWdpY2VydC5jb20vRGlnaUNlcnRUcnVzdGVkUm9vdEc0LmNybDAgBgNVHSAEGTAX
# MAgGBmeBDAEEAjALBglghkgBhv1sBwEwDQYJKoZIhvcNAQELBQADggIBAH1ZjsCT
# tm+YqUQiAX5m1tghQuGwGC4QTRPPMFPOvxj7x1Bd4ksp+3CKDaopafxpwc8dB+k+
# YMjYC+VcW9dth/qEICU0MWfNthKWb8RQTGIdDAiCqBa9qVbPFXONASIlzpVpP0d3
# +3J0FNf/q0+KLHqrhc1DX+1gtqpPkWaeLJ7giqzl/Yy8ZCaHbJK9nXzQcAp876i8
# dU+6WvepELJd6f8oVInw1YpxdmXazPByoyP6wCeCRK6ZJxurJB4mwbfeKuv2nrF5
# mYGjVoarCkXJ38SNoOeY+/umnXKvxMfBwWpx2cYTgAnEtp/Nh4cku0+jSbl3ZpHx
# cpzpSwJSpzd+k1OsOx0ISQ+UzTl63f8lY5knLD0/a6fxZsNBzU+2QJshIUDQtxMk
# zdwdeDrknq3lNHGS1yZr5Dhzq6YBT70/O3itTK37xJV77QpfMzmHQXh6OOmc4d0j
# /R0o08f56PGYX/sr2H7yRp11LB4nLCbbbxV7HhmLNriT1ObyF5lZynDwN7+YAN8g
# Fk8n+2BnFqFmut1VwDophrCYoCvtlUG3OtUVmDG0YgkPCr2B2RP+v6TR81fZvAT6
# gt4y3wSJ8ADNXcL50CN/AAvkdgIm2fBldkKmKYcJRyvmfxqkhQ/8mJb2VVQrH4D6
# wPIOK+XW+6kvRBVK5xMOHds3OBqhK/bt1nz8MIIGwjCCBKqgAwIBAgIQBUSv85Sd
# CDmmv9s/X+VhFjANBgkqhkiG9w0BAQsFADBjMQswCQYDVQQGEwJVUzEXMBUGA1UE
# ChMORGlnaUNlcnQsIEluYy4xOzA5BgNVBAMTMkRpZ2lDZXJ0IFRydXN0ZWQgRzQg
# UlNBNDA5NiBTSEEyNTYgVGltZVN0YW1waW5nIENBMB4XDTIzMDcxNDAwMDAwMFoX
# DTM0MTAxMzIzNTk1OVowSDELMAkGA1UEBhMCVVMxFzAVBgNVBAoTDkRpZ2lDZXJ0
# LCBJbmMuMSAwHgYDVQQDExdEaWdpQ2VydCBUaW1lc3RhbXAgMjAyMzCCAiIwDQYJ
# KoZIhvcNAQEBBQADggIPADCCAgoCggIBAKNTRYcdg45brD5UsyPgz5/X5dLnXaEO
# CdwvSKOXejsqnGfcYhVYwamTEafNqrJq3RApih5iY2nTWJw1cb86l+uUUI8cIOrH
# mjsvlmbjaedp/lvD1isgHMGXlLSlUIHyz8sHpjBoyoNC2vx/CSSUpIIa2mq62DvK
# Xd4ZGIX7ReoNYWyd/nFexAaaPPDFLnkPG2ZS48jWPl/aQ9OE9dDH9kgtXkV1lnX+
# 3RChG4PBuOZSlbVH13gpOWvgeFmX40QrStWVzu8IF+qCZE3/I+PKhu60pCFkcOvV
# 5aDaY7Mu6QXuqvYk9R28mxyyt1/f8O52fTGZZUdVnUokL6wrl76f5P17cz4y7lI0
# +9S769SgLDSb495uZBkHNwGRDxy1Uc2qTGaDiGhiu7xBG3gZbeTZD+BYQfvYsSzh
# Ua+0rRUGFOpiCBPTaR58ZE2dD9/O0V6MqqtQFcmzyrzXxDtoRKOlO0L9c33u3Qr/
# eTQQfqZcClhMAD6FaXXHg2TWdc2PEnZWpST618RrIbroHzSYLzrqawGw9/sqhux7
# UjipmAmhcbJsca8+uG+W1eEQE/5hRwqM/vC2x9XH3mwk8L9CgsqgcT2ckpMEtGlw
# Jw1Pt7U20clfCKRwo+wK8REuZODLIivK8SgTIUlRfgZm0zu++uuRONhRB8qUt+JQ
# ofM604qDy0B7AgMBAAGjggGLMIIBhzAOBgNVHQ8BAf8EBAMCB4AwDAYDVR0TAQH/
# BAIwADAWBgNVHSUBAf8EDDAKBggrBgEFBQcDCDAgBgNVHSAEGTAXMAgGBmeBDAEE
# AjALBglghkgBhv1sBwEwHwYDVR0jBBgwFoAUuhbZbU2FL3MpdpovdYxqII+eyG8w
# HQYDVR0OBBYEFKW27xPn783QZKHVVqllMaPe1eNJMFoGA1UdHwRTMFEwT6BNoEuG
# SWh0dHA6Ly9jcmwzLmRpZ2ljZXJ0LmNvbS9EaWdpQ2VydFRydXN0ZWRHNFJTQTQw
# OTZTSEEyNTZUaW1lU3RhbXBpbmdDQS5jcmwwgZAGCCsGAQUFBwEBBIGDMIGAMCQG
# CCsGAQUFBzABhhhodHRwOi8vb2NzcC5kaWdpY2VydC5jb20wWAYIKwYBBQUHMAKG
# TGh0dHA6Ly9jYWNlcnRzLmRpZ2ljZXJ0LmNvbS9EaWdpQ2VydFRydXN0ZWRHNFJT
# QTQwOTZTSEEyNTZUaW1lU3RhbXBpbmdDQS5jcnQwDQYJKoZIhvcNAQELBQADggIB
# AIEa1t6gqbWYF7xwjU+KPGic2CX/yyzkzepdIpLsjCICqbjPgKjZ5+PF7SaCinEv
# GN1Ott5s1+FgnCvt7T1IjrhrunxdvcJhN2hJd6PrkKoS1yeF844ektrCQDifXcig
# LiV4JZ0qBXqEKZi2V3mP2yZWK7Dzp703DNiYdk9WuVLCtp04qYHnbUFcjGnRuSvE
# xnvPnPp44pMadqJpddNQ5EQSviANnqlE0PjlSXcIWiHFtM+YlRpUurm8wWkZus8W
# 8oM3NG6wQSbd3lqXTzON1I13fXVFoaVYJmoDRd7ZULVQjK9WvUzF4UbFKNOt50MA
# cN7MmJ4ZiQPq1JE3701S88lgIcRWR+3aEUuMMsOI5ljitts++V+wQtaP4xeR0arA
# VeOGv6wnLEHQmjNKqDbUuXKWfpd5OEhfysLcPTLfddY2Z1qJ+Panx+VPNTwAvb6c
# Kmx5AdzaROY63jg7B145WPR8czFVoIARyxQMfq68/qTreWWqaNYiyjvrmoI1VygW
# y2nyMpqy0tg6uLFGhmu6F/3Ed2wVbK6rr3M66ElGt9V/zLY4wNjsHPW2obhDLN9O
# TH0eaHDAdwrUAuBcYLso/zjlUlrWrBciI0707NMX+1Br/wd3H3GXREHJuEbTbDJ8
# WC9nR2XlG3O2mflrLAZG70Ee8PBf4NvZrZCARK+AEEGKMIIIVjCCBD6gAwIBAgII
# WXxE9K4oPYowDQYJKoZIhvcNAQELBQAwaDELMAkGA1UEBhMCVVMxIDAeBgNVBAoT
# F0NhcmJvbiBDcmV3IFByb2R1Y3Rpb25zMR4wHAYDVQQLExVDZXJ0aWZpY2F0ZSBB
# dXRob3JpdHkxFzAVBgNVBAMTDkNhcmJvbiBDcmV3IENBMB4XDTIxMDIwNTA4NDcw
# MFoXDTMxMDIwNTA4NDcwMFowRzELMAkGA1UEBhMCVVMxGzAZBgNVBAoTElNvYXBi
# b3ggUmFjZSBXb3JsZDEbMBkGA1UEAxMSU29hcGJveCBSYWNlIFdvcmxkMIIBIjAN
# BgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAu8rjPMIDUj2x5tf3t0DjtW3rqGcK
# KlaQ44ff+D3SEEP9zIJbtrV+l4VsnjxmTKGpy2QNCU7a8Q0PdOUUOO2nWJBqjfta
# MualJw7Ow1A7di3PFizGKzEdG+3f/wCdnqxdMhvXLiNXaldjwwnthWnEQpzjOuBJ
# EKCIdi2FyQTe8Nz/Nm4qLz4efAuioPfnQ2zWbC4Hq8rA05H9rDLsSH0g+X6r6kte
# kWbz8lbOb8NwRGuu9f3bsORDdvzNvVfJFsHEbeobIpvZZqzBnrIIv+vr5isDsuds
# TqXgSqMfccgnFYzJTM66IKT17ZT4i4xhlygpYmfHc9PD74zX2fGB/yGZZwIDAQAB
# o4ICIzCCAh8wDwYDVR0TAQH/BAUwAwEB/zAdBgNVHQ4EFgQU05BlSGhhqmuvuwDo
# rjL6zvbGZ64wHwYDVR0jBBgwFoAUtNL2YDIsEp6xX6/InuLAlKn22qAwDwYDVR0P
# AQH/BAUDAwf/gDCB7wYDVR0lAQH/BIHkMIHhBggrBgEFBQcDAQYIKwYBBQUHAwIG
# CCsGAQUFBwMDBggrBgEFBQcDBAYIKwYBBQUHAwgGCisGAQQBgjcCARUGCisGAQQB
# gjcCARYGCisGAQQBgjcKAwEGCisGAQQBgjcKAwMGCisGAQQBgjcKAwQGCWCGSAGG
# +EIEAQYLKwYBBAGCNwoDBAEGCCsGAQUFBwMFBggrBgEFBQcDBgYIKwYBBQUHAwcG
# CCsGAQUFCAICBgorBgEEAYI3FAICBggrBgEFBQcDCQYIKwYBBQUHAw0GCCsGAQUF
# BwMOBgcrBgEFAgMFMC8GA1UdEQQoMCaCDmNhcmJvbmNyZXcub3JnghRkYXZpZGNh
# cmJvbi5kb3dubG9hZDAvBgNVHRIEKDAmgg5jYXJib25jcmV3Lm9yZ4IUZGF2aWRj
# YXJib24uZG93bmxvYWQwZwYDVR0fBGAwXjAqoCigJoYkaHR0cDovL2NybC5jYXJi
# b25jcmV3Lm9yZy9DQ1AtQ0EuY3JsMDCgLqAshipodHRwOi8vY3JsLmRhdmlkY2Fy
# Ym9uLmRvd25sb2FkL0NDUC1DQS5jcmwwDQYJKoZIhvcNAQELBQADggQBAFMYGxMa
# s7Ipg8Yin/XicMq4rOFi4l9jGUuA+U2EGrG72LynJ8qLT1AQYb03I3uaqzpV7iQ+
# LOtyV34Z4lXY4wUmyNilzhJkqigKjZnFGpp4T+a0CvZfiClc9axUnDC0EN1Mzuy+
# 7R0E35Lrky3LLUNHcDhV7cOi15IfATy7Pgcghdycs/EPx1e63uMj3XJqbPwn88u6
# s1vyHf83R2OvNRGiUKECR4i3qxPH5QI1vIenehO/c58y+0IVgIhvSxMCX/63xu+A
# ruf9fd/DAMf/nV0jDfRJvBoHssG3kg7ZumDNZWqNrZ0BGlaYD5N2b0z+28TulqWE
# QVxynmHsBh1DjDjHzNdL7FnjFWQEXR1Ybox118wXaFijgaJJN71E3irpHd+FcIPr
# uXofMWQfSFBmDhe0ib/T/5AvGL8DXV+O/8EaQfbDkUp9FRpUQ2AAlsKHeGz0jCGE
# 4laB5DJzFGiXrLncxRwaCRXQUygEiCV2RBgNocbStnqIpbtcv2qJSHJUoIHNKXPr
# n8Dw7C0FBrngPwlRm/aOd/1NOJnUZhaQ1qrpyla1Pimn+tk0qbivJRlkGa+nDB6a
# DEo3QNv438LZZhZEPPoTT8QCy+SFxFa0SLS6loRrfxY7KJUvc3XvHiJB37W7aAGv
# rNFuCeH0cL25wnT0DWFVn91ir1BY6AKy6W7Kxbhj6Fff67iWk7taQ1MxIwStCuHw
# ncZY1oHZN1VVHZZAfK1Bzy3ISvIgsC+pyFYcahAXpaWG8ojEglXMjof8d+0TBpur
# Ig9cPlEsntJMfV4W3y+C8iW6K+wEwFTUdEJsGp+Aj38QsHCBW3i5XtQG39AxIrKs
# M3UP2m34zATmaNF/eHLm+BIqgUiC2REje36rXVOnn6zI2C83XNOQ9Kgt9VhIv6RQ
# aUfWW4mmYLaMVKPinoklQK5YabDKXIvrbfqhnSQGfWqu6n7vGIO1u0mqIWDBN7Sc
# m7ol8ChKWerOUkG6KmTMsvfPzj9swt8ljzqnN7GxaYESCYkD74kqP6ZXvS9ga7K/
# dbq9BTDKPn8d5V6BKKjvFMkmAKasdWTGIVNYVTQYlu0UA9SjXHACVS12kWFhf973
# JUrLzqboOIsg8Blm9+B93Gzh6SH4Dn7qofKidoaYez9L1idz2+y0wf+jd0gtMp/B
# BKeMYAjt7PoWxuaFbttMLATYTgactB3Vj/n1eqmSZArlATSIC2Tfvtd07gmhU73V
# ImLiWF9K8NcAiyekpW12jpGM6ODhlCJo2hNGyD09FGinjTZxtbfmt4itqet88pBP
# oDUigvdrCVAAMZwKX5hCTtiNPcYOq1wyI2ugl7hwI5iNqwypDh7HAhPyQCwOxd1V
# qyxXkz33tGv9zXcxggUYMIIFFAIBATBTMEcxCzAJBgNVBAYTAlVTMRswGQYDVQQK
# ExJTb2FwYm94IFJhY2UgV29ybGQxGzAZBgNVBAMTElNvYXBib3ggUmFjZSBXb3Js
# ZAIIdRbquLTQwT0wCQYFKw4DAhoFAKB4MBgGCisGAQQBgjcCAQwxCjAIoAKAAKEC
# gAAwGQYJKoZIhvcNAQkDMQwGCisGAQQBgjcCAQQwHAYKKwYBBAGCNwIBCzEOMAwG
# CisGAQQBgjcCARYwIwYJKoZIhvcNAQkEMRYEFNfp8fLF5qmdymXuCrYL3cp4CPg6
# MA0GCSqGSIb3DQEBAQUABIIBAIrYqV46QFXwAb1h9Q+j2GSJp0OLuXTTU9UkBLuH
# bIbsqBiL9zWsYgCQafNTGKewI6nsS95MF2JTo56RMfRD7Vc1e32skb+7N1zOEuqF
# 5Z+hEhd7HeQKn7SrNN/wlzZrjJs4TtMoOpJeqADYKAdUDpg2s31D5w2PhdhIK0x2
# YPGFWbxuTgePo5H3CHHZ5IuRzZb9CZQ+3HZVcRUAUPJjCxh0puP3XpoTqAMcJPYd
# Pb8kshvo9TaIqpkHxyrqWovE9lEFPEbg9t4L6+yLbuFqWMqgLp9pidygmTPTNBIV
# 3WG7ddhXyByX3Au2qUYKzCVQB1AKNwSWiI01TZ8iEDse7+GhggMgMIIDHAYJKoZI
# hvcNAQkGMYIDDTCCAwkCAQEwdzBjMQswCQYDVQQGEwJVUzEXMBUGA1UEChMORGln
# aUNlcnQsIEluYy4xOzA5BgNVBAMTMkRpZ2lDZXJ0IFRydXN0ZWQgRzQgUlNBNDA5
# NiBTSEEyNTYgVGltZVN0YW1waW5nIENBAhAFRK/zlJ0IOaa/2z9f5WEWMA0GCWCG
# SAFlAwQCAQUAoGkwGAYJKoZIhvcNAQkDMQsGCSqGSIb3DQEHATAcBgkqhkiG9w0B
# CQUxDxcNMjQwNDI2MDAxMTMzWjAvBgkqhkiG9w0BCQQxIgQgVNiNbRorrs0WrWiN
# eayCoXOLo41g3BcNIbb4wXPzNSswDQYJKoZIhvcNAQEBBQAEggIAaQIATqfZhPr+
# jzdnlps6mlWekSHzlkKKEpmfANO/bfRowHb22IeWj3/l6/I3X529OcZwaZvR9ISH
# DLCat98sTomtQ93kRo3Z1IHdfkHLwwYlKZL+68EZS59bbkgHxotLpCOOs+j27vPZ
# TSE9ZcgzEoiK12qcIA9PtYVYpNecuRL3P3m7vsDg6gnVmHVCURbDZWYM2JEGjHrF
# n9sTa3yjbFcqGM1+MvWKzZXi8zvo2LRwnDkF8+WTTV+e7icjt4OgHV4I/f8RTsKd
# w5c5JvqylPja/UjomfDfmbD8XvkWBrKSZ+DKGLErK+iMz3Y1LwVWFXhIvdrOQEo9
# J/ZXg9So6+D6fQ9kYvlcN49RwDgiphzbDvJkAnvPe49B82uYeyhxqpTCeuUcUpAv
# PETb/uBl7lNb6NJabf4J8CcM1UhRG66VDwqXBq4HAeShCOjTH2mod3odw26dyJHz
# VbTZDBx3QnQldZviajezyZ4TGfkmZ6rj/qHn8G1UlQhpKlWxIMX80hFGCnoFQUaL
# nk3syjgZYlz4lo+Eay0OGDVGMPO5ORePBJCM8afXGQYBvDGlclMgmutaewuV85HK
# he2MCyN4Eek2A2FvGBf2SFsjNVnhepwogzh4uvmZgDWfkDm0g8KtpbfcpuTl43zG
# 0jAQVoalwtjEwXJzb5sxLBJuKR01ELU=
# SIG # End signature block
