del /q "%~dp0Alpha_ConfigTool.esriAddIn"
del /q "%~dp0Alpha_ExportTool.esriAddIn"
del /q "%~dp0Alpha_LayoutTool.esriAddIn"
del /q "%~dp0MapActionToolbars.esriAddIn"
del /q "%~dp0RenameLayer.esriAddIn"

REM 3/3/20 HSG: not aware of a separate RenameLayer esriAddin, this is causing GOCD pipeline to fail
REM xcopy /y "%~dp0..\RenameLayer\RenameLayer\bin\Release\RenameLayer.esriAddIn" "%~dp0"
REM working name for the addin version of the tools, renames to distinguish from COM/installed version
xcopy /y "%~dp0..\MapActionToolbar_Addin\bin\Release\MapActionToolbar_Addin.esriAddIn" "%~dp0"
REM also copy the installer version to the same output folder (despite its inappropriate name)
REM this is expected to fail as I believe GOCD cannot build installer (.vdproj) projects
xcopy /y "%~dp0..\MapActionToolbarInstaller\Release\MapActionToolbarInstaller.msi" "%~dp0"