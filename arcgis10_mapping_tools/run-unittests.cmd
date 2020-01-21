echo off
echo %date% - %time%
mkdir "%~dp0unittestresults"
echo on
"%~dp0packages\NUnit.ConsoleRunner.3.10.0\tools\nunit3-console.exe" "%~dp0CommonTests\ExportIntergrationTests.nunit" --result "%~dp0unittestresults\TestResult.xml"