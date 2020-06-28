[CmdletBinding()]
param(
	[validateset("Debug", "Release")]
	[string] $BuildConfig="Release",
	[validateset("Any CPU")]
    [string] $BuildPlatform="Any CPU"
)
$NUnitConsole=".\packages\NUnit.ConsoleRunner.3.11.1\tools\nunit3-console.exe"

If (Test-Path $NUnitConsole)
{
	& $NUnitConsole DemoLibrary.Tests\bin\$BuildConfig\DemoLibrary.Tests.dll  /result=.\DemoLibrary.Tests\nunit-result.xml
}
else 
{
	throw "NUnit not found, skipping" 
}
