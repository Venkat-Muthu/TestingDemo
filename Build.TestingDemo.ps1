[CmdletBinding()]
param(
	[validateset("Debug", "Release")]
	[string] $BuildConfig="Release",
	[validateset("Any CPU")]
    [string] $BuildPlatform="Any CPU",
	[validateset("", "Fast")]
    [string] $Build=""
)

Write-Verbose "Hello $BuildConfig"

$nugetPath = ".\.nuget\"
$nugetName = "nuget.exe"
$nugetWithRelativePath=Join-Path -Path $nugetpath -ChildPath $nugetName
$nugeturl="https://dist.nuget.org/win-x86-commandline/v5.6.0/nuget.exe"

If(!(test-path $nugetPath))
{
	New-Item -ItemType Directory -Force -Path $nugetPath
}

if (!(Test-Path $nugetWithRelativePath -pathtype leaf))
{
	invoke-webrequest -uri $nugeturl -outfile $nugetWithRelativePath
}

$packagePath = ".\packages\"
If(!(test-path $packagePath))
{
	New-Item -ItemType Directory -Force -Path $packagePath
}

& "$($nugetWithRelativePath)" config -ConfigFile .\NuGet.Config 
& "$($nugetWithRelativePath)" restore .\TestingDemo.sln | Out-Null

$msbuild = '"C:\Program Files (x86)\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\MSBuild.exe"'
$solution= ".\TestingDemo.sln"
$buildparams="/p:TargetFrameworkVersion=v4.7.2 /t:Clean,Build /p:Configuration=$BuildConfig /p:Platform='$BuildPlatform'"
Write-Host "$buildparams"

iex ("& {0} {1} {2}" -f "$msbuild", "$solution", "$buildparams")

$retcode = $LastExitCode
Write-Host $retcode
if ($retcode -ne 0)
{ 
	throw "msbuild failed" 
}


$unitTestScript = ".\UnitTests.TestingDemo.ps1"
$args = @()
$args += ("-BuildConfig", "$BuildConfig")
$args += ("-BuildPlatform", "'$BuildPlatform'")

Invoke-Expression "$unitTestScript $args"
  