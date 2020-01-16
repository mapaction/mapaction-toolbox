echo Getting dependancies
dotnet add package Newtonsoft.Json --version 12.0.3

echo Building...
"C:\Program Files (x86)\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\MSBuild.exe" %~dp0arcgis10_mapping_tools/MapAction-toolbox.sln /t:build /p:PlatformTarget=x86 /p:Configuration=Release /maxcpucount

:: echo Copying...
:: call %~dp0arcgis10_mapping_tools\arcaddins_for_testing\post_build_copy_addins.cmd

:: echo Running Tests...
:: call %~dp0arcgis10_mapping_tools\get_coverage.cmd

