echo off
echo %date% - %time%
echo on
"C:\Program Files (x86)\NUnit 2.6.4\bin\nunit-console-x86.exe" "%~dp0CommonTests\ExportIntergrationTests.nunit"
