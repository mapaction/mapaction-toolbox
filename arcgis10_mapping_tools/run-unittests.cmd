echo off
echo %date% - %time%
echo on
"%~dp0packages\NUnit.ConsoleRunner.3.10.0\tools\nunit3-console.exe" "%~dp0CommonTests\ExportIntergrationTests.nunit"
