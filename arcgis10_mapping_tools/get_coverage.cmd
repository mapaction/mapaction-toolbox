C:\nuget\nuget.exe install "%~dp0arcgis10_mapping_tools\.nuget\packages.config"

%~dp0packages\OpenCover.4.7.922\tools\OpenCover.Console.exe -register:user -returntargetcode "-filter:+[MapAction]* +[MapActionToolbars]* -[*Test]*" "-target:%~dp0run-unittests.cmd" "-output:%~dp0opencover-results.xml"

%~dp0packages\ReportGenerator.2.4.4.0\tools\ReportGenerator.exe "-reports:%~dp0opencover-results.xml" "-targetdir:%~dp0coverage"