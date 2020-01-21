echo Getting dependancies
C:\nuget\nuget.exe install "%~dp0arcgis10_mapping_tools\.nuget\packages.config" -NoCache
C:\nuget\nuget.exe install "%~dp0arcgis10_mapping_tools\MapAction\MapAction\packages.config" -NoCache
C:\nuget\nuget.exe install nunit

echo Building...
"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\MSBuild.exe" %~dp0arcgis10_mapping_tools/MapAction-toolbox.sln /t:build /p:PlatformTarget=x86 /p:Configuration=Release /maxcpucount

:: echo Copying...
:: call %~dp0arcgis10_mapping_tools\arcaddins_for_testing\post_build_copy_addins.cmd

:: echo Running Tests...
:: call %~dp0arcgis10_mapping_tools\get_coverage.cmd
