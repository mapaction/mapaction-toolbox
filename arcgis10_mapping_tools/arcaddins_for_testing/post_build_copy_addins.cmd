del /q "%~dp0Alpha_ConfigTool.esriAddIn"
del /q "%~dp0Alpha_ExportTool.esriAddIn"
del /q "%~dp0Alpha_LayoutTool.esriAddIn"
del /q "%~dp0MapActionToolbars.esriAddIn"
del /q "%~dp0RenameLayer.esriAddIn"

xcopy /y "%~dp0..\RenameLayer\RenameLayer\bin\Release\RenameLayer.esriAddIn" "%~dp0"
xcopy /y "%~dp0..\MapActionToolbars\bin\Release\MapActionToolbars.esriAddIn" "%~dp0"