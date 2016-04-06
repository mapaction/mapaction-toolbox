
%~dp0packages\OpenCover.4.6.519\tools\OpenCover.Console.exe -register:user -returntargetcode "-filter:+[MapAction]* -[*Test]*" "-target:run-unittests.cmd" "-output:opencover-results.xml"

%~dp0packages\ReportGenerator.2.4.4.0\tools\ReportGenerator.exe "-reports:opencover-results.xml" "-targetdir:.\coverage"