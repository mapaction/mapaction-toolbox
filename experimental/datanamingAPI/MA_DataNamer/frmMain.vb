Option Strict On

Imports System.IO       'You need this import to rename system files and folders
Imports mapaction.datanaming
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Controls

Public Class frmMain
    Private targetFileNames() As String

    Private licHandler As New ESRIlicenceHandler
    Private myDataList As IGeoDataListConnection = Nothing
    Private myDNCL As IDataNameClauseLookup = Nothing
    Private myGDLConFactory As GeoDataListConnectionFactory
    Private m_HookHelper As IHookHelper = Nothing

    Private Sub Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        licHandler.getESRIlicence()

        Call TestForUpdate()    'Test to see if there's a new data naming convention on the PC/laptop
        'Call CheckForMXD()  'See if a map is running on the PC/tablet

        'Set the icon in the DataGridView so the 'Status' column is a grey line
        Me.datGV_NameList.Columns("clmStatusIcon").DefaultCellStyle.NullValue = "Nothing.bmp"

        If isParentArcMap() Then
            rdbSelectCurMap.Enabled = True
            rdbSelectCurMap.Checked = True
        End If

        RadioButtons()
    End Sub

    Private Function isParentArcMap() As Boolean
        Return ((Not m_HookHelper Is Nothing) AndAlso (TypeOf m_HookHelper.Hook Is IMxApplication))

        'Dim myApp As IApplication
        'Dim returnVal As Boolean

        'Try
        '    Dim t As Type = Type.GetTypeFromProgID("esriFramework.AppRef")
        '    Dim obj As System.Object = Activator.CreateInstance(t)
        '    Dim app As IApplication = CType(obj, IApplication)
        '    myApp = app
        '    returnVal = True
        'Catch ex As Exception
        '    returnVal = False
        'End Try

        'Return returnVal
    End Function

    Private Function getParentArcMap() As IApplication
        Dim app As IApplication

        If ((Not m_HookHelper Is Nothing) AndAlso (TypeOf m_HookHelper.Hook Is IMxApplication)) Then
            'Get the application template collection
            app = CType(m_HookHelper.Hook, IApplication)
        Else
            app = Nothing
        End If

        Return app
    End Function

    Private Function getMxDocFromApp(ByRef myApp As IApplication) As IMxDocument
        Return TryCast(myApp.Document, IMxDocument)
    End Function

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Dim strDirectoryAndFileName As String
        Dim strFilePath As String
        Dim strFileName As String
        Dim strInitialDirectory As String
        Dim bolBrowseForFile As Boolean 'there's a need to see if the user has browsed for a folder (i.e. a GeoDatabase), or a file (i.e. layer, mxd, raster of shape file)
        Dim strFileNameStatus As String

        Dim args(1) As String


        'initisalising variables
        bolBrowseForFile = False

        'reset the directory text box
        'txtWorkingDirectory.Text = ""

        'Make sure the screen is reset
        Call RadioButtons()

        OpenFileDialog.Multiselect = False  'Check that multiselect isnt true, unless it's for the miscellaneous option

        If txtWorkingDirectory.Text = "" Then
            'todo HIGH PUt this right before the training session
            strInitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.MyComputer)
            'strInitialDirectory = "D:\MapAction\bronze\custom_tools\managedcode\testing_datanamingAPI\2010-05-05-testing-v02\GIS\2_Active_Data\21_Vector"
        Else
            strInitialDirectory = ReturnFilePath(txtWorkingDirectory.Text)
        End If

        'Set the appropriate file type filters in the open dialog
        If rdbSelectMXD.Checked = True Then
            OpenFileDialog.Filter = "Mxd files (*.mxd)|*.mxd"
            OpenFileDialog.FileName = ""    'keeps the file name empty
            bolBrowseForFile = True
        ElseIf rdbSelectGDB.Checked = True Then
            'OpenFileDialog.Filter = "ERSI Geodatabase and connection files|*.mdb,*.sde,*.ags|All Files|*.*"
            OpenFileDialog.Filter = "All files (*.*)|*.*"
            OpenFileDialog.FileName = ""    'keeps the file name empty
            bolBrowseForFile = True
        ElseIf rdbMiscFiles.Checked Then
            bolBrowseForFile = False
        Else 'no radio button is selected but the user has pressed browse
            MsgBox("Please selct the type of source data you wish to rename")
            Exit Sub
        End If

        'Having found which radio button was chosen, the dialog needs to browse for a file or folder depending on which radio button is selected
        'Folders are needed for a GeoDatabase, whilst files are needed for an mxd or shape, raster or layer files
        If bolBrowseForFile = True Then 'probaby better to put this in a seperate sub
            OpenFileDialog.InitialDirectory = strInitialDirectory
            OpenFileDialog.Title = "Open Data List"
            If OpenFileDialog.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            Else
                strDirectoryAndFileName = OpenFileDialog.FileName  'Returns the entire file path and name(s)
            End If 'need to spend time making sure if's are logical

            txtWorkingDirectory.Text = strDirectoryAndFileName

        Else
            'so we're browsing for a folder (i.e. a GeoDatabase)
            ' First create a FolderBrowserDialog object
            Dim FolderBrowserDialog As New FolderBrowserDialog

            ' Then use the following code to create the Dialog window
            ' Change the .SelectedPath property to the default location
            With FolderBrowserDialog
                ' Desktop is the root folder in the dialog.
                .RootFolder = Environment.SpecialFolder.Desktop
                ' Select the C:\Windows directory on entry.
                .SelectedPath = strInitialDirectory
                ' Prompt the user with a custom message.
                .Description = "Select Folder"
                If .ShowDialog = DialogResult.OK Then
                    ' Display the selected folder if the user clicked on the OK button.
                    txtWorkingDirectory.Text = .SelectedPath
                Else
                    Exit Sub
                End If
            End With
            'OpenFileDialog.Filter = "gdb|*.gdb"
        End If

        'todo
        myGDLConFactory = GeoDataListConnectionFactory.getFactory()
        args(0) = txtWorkingDirectory.Text

        If rdbSelectMXD.Checked = True Then
            myDataList = myGDLConFactory.createGeoDataListConnection(DATALIST_TYPE_MXD, args)
        ElseIf rdbSelectGDB.Checked = True Then
            myDataList = myGDLConFactory.createGeoDataListConnection(DATALIST_TYPE_GDB, args)
        ElseIf rdbMiscFiles.Checked Then
            myDataList = myGDLConFactory.createGeoDataListConnection(DATALIST_TYPE_DIR, args)
        Else 'no radio button is selected but the user has pressed browse
        End If

        gotNewDataList()

        'allow the files to be processed
        btnProcess.Enabled = True
    End Sub

    Private Sub gotNewDataList()
        txtWorkingDirectory.Text = myDataList.getDetails()
        myDataList.setRecuse(ckBxRecurse.Checked)

        Try
            myDNCL = myDataList.getDefaultDataNameClauseLookup()
            txtBxDNLookupPath.Text = myDNCL.getDetails()
            ckBxOverrideLookupDB.Checked = False

            btnProcess.Enabled = True
            'Blank the DataGridView
            datGV_NameList.Rows.Clear()

        Catch ex As Exception
            showSelectDNCLTdialog()
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        'Close the form
        Me.Dispose()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerCheckForMXD.Tick
        'Timer to check for open MXDs - this is a really simple (bodgy) way to do it. Much better to add an event lister for background apps
        'or change of focus to/from the form incase te user opens ArcMap before or after this program
        'Call CheckForMXD()
    End Sub

    Private Sub rdbSelectCurMap_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbSelectCurMap.CheckedChanged
        Call RadioButtons()
    End Sub

    Private Sub rdbSelectMXD_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbSelectMXD.CheckedChanged
        Call RadioButtons()
    End Sub

    Private Sub rdbSelectGDB_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbSelectGDB.CheckedChanged
        Call RadioButtons()
    End Sub

    Private Sub RadioButtons()
        'There's no need to browse for a map if ArcMap is currently running
        btnProcess.Enabled = False
        If rdbSelectCurMap.Checked Then
            btnBrowse.Enabled = False
            myDataList = myGDLConFactory.createGeoDataListConnection(getMxDocFromApp(getParentArcMap()))
            myDataList.setRecuse(True)
            ckBxRecurse.Enabled = False

            gotNewDataList()

        ElseIf rdbSelectMXD.Checked Then
            ckBxRecurse.Enabled = False
        Else
            ckBxRecurse.Enabled = True
            btnBrowse.Enabled = True
            txtWorkingDirectory.Text = ""
            Me.datGV_NameList.Rows.Clear()
        End If
        'The user has chosen a directory so allow the process button to be enabled
        If txtWorkingDirectory.Text <> "" Then
            btnProcess.Enabled = True
        End If


    End Sub

    Private Sub dataNameClauseTablesNotFound(ByVal pathName As String)
        Dim mesStr As String
        If pathName Is Nothing Then
            mesStr = "Unable to automatically locate the Data Naming Convention Database" & Chr(13) & _
                     "Please select alternative"
        Else
            mesStr = "Unable to locate the Data Naming Convention Database at:" & Chr(13) & _
                     pathName & _
                     "Please select alternative"
        End If

        MsgBox(mesStr, MsgBoxStyle.Exclamation, "MA Data Naming")

        showSelectDNCLTdialog()
    End Sub

    Private Sub btnProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        Dim nameList As List(Of IDataName)
        Dim fileNameStatus As Long
        Dim tempCommentsStr As String

        'Dim colIdxFileName As String = "clmFileName"
        'Dim colIdxIcon As String = "clmStatus"
        'Dim colIdxComments As String = "clmComments"
        'Dim colIdxRenameBtn As String = "clmRename"

        Dim colIdxFileName As Integer = 0
        Dim colIdxIcon As Integer = 4
        Dim colIdxComments As Integer = 5
        Dim colIdxRenameBtn As Integer = 6

        datGV_NameList.Rows.Clear()
        'Call CheckForMXD()  'check to see if a valid MXD window is open
        'Call RunProgressBar()   'Process may take a while so run the process bar

        If myDataList Is Nothing Then
            btnProcess.Enabled = False
        ElseIf myDNCL Is Nothing Then
            btnProcess.Enabled = False
        Else
            nameList = myDataList.getLayerDataNamesList(myDNCL)

            For Each dName In nameList
                tempCommentsStr = ""
                'System.Console.WriteLine("populating grid with: " & dName.getNameAndFullPathStr())

                Dim rDGVRow As New DataGridViewRow  'Rows to be added to the datagrid view. I dont like Dim here - but it's the correct way
                rDGVRow.CreateCells(datGV_NameList) 'correct
                With rDGVRow
                    'System.Console.WriteLine("populating grid : got Row Obj")
                    '.CreateCells(datGV_NameList)
                    'strFileName = ReturnFileName(File)
                    'System.Console.WriteLine("populating grid : dName.getNameStr(): " & dName.getNameStr())

                    '.Cells.Item(colIdxFileName).Value = "anything"
                    .Cells.Item(colIdxFileName).Value = dName.getNameStr()

                    '#### Calling the Function CheckForValidFileName to see if it's Valid, Warning or Error ######

                    Dim icoValid As Icon = Drawing.Icon.FromHandle(My.Resources.Yes.GetHicon)
                    Dim icoWarning As Icon = Drawing.Icon.FromHandle(My.Resources.Warning.GetHicon)
                    Dim icoError As Icon = Drawing.Icon.FromHandle(My.Resources._Error.GetHicon)

                    fileNameStatus = dName.checkNameStatus()

                    Dim statusList As List(Of String)
                    Dim lineCnt As Integer = 0
                    statusList = AbstractDataNameClauseLookup.getDataNamingStatusStrings(fileNameStatus)

                    For Each statusStr In statusList
                        If (Not statusStr Is Nothing) Or (Not statusStr = "") Then
                            If tempCommentsStr = "" Then
                                tempCommentsStr = statusStr
                                lineCnt = 1
                            Else
                                tempCommentsStr = tempCommentsStr & vbNewLine & statusStr
                                lineCnt = lineCnt + 1
                            End If
                        End If
                    Next

                    'System.Console.WriteLine("tempCommentsStr : " & tempCommentsStr)

                    .Cells.Item(colIdxComments).Value = tempCommentsStr
                    .Cells.Item(colIdxComments).Style.WrapMode = DataGridViewTriState.True
                    If statusList.Count > 1 Then
                        .Height = CInt(datGV_NameList.RowTemplate.Height * statusList.Count * 0.9)
                    End If

                    If ((fileNameStatus And DATANAME_INVALID) = DATANAME_INVALID) Or _
                     ((fileNameStatus And DATANAME_SYNTAX_ERROR) = DATANAME_SYNTAX_ERROR) Then
                        '.Cells(3).Value = True  'Tick the error box
                        .Cells.Item(colIdxIcon).Value = icoError
                    ElseIf (fileNameStatus And DATANAME_WARN) = DATANAME_WARN Then
                        '.Cells(2).Value = True  'tick the Warning box
                        .Cells.Item(colIdxIcon).Value = icoWarning
                    Else
                        '.Cells(1).Value = True  'tick the Valid box
                        .Cells.Item(colIdxIcon).Value = icoValid
                    End If

                End With
                datGV_NameList.Rows.Add(rDGVRow)
                'datGV_NameList.Rows.GetNextRow()
            Next
        End If

    End Sub



    Private Sub datGV_NameList_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles datGV_NameList.CellContentClick
        Dim intCursorPosition As Integer
        Dim strOriginalFileName, strCorrectFileName As String

        ' Ignore clicks that are not on the 'Rename..' button. Without this any click triggers an event
        If e.RowIndex < 0 OrElse Not e.ColumnIndex = _
            datGV_NameList.Columns("clmRename").Index Then Return

        'The best way to retrieve data is using the cursor position 
        'since the button would effect the selected row
        intCursorPosition = datGV_NameList.CurrentRow.Index   'datGV_NameList.Rows.IndexOf()
        strOriginalFileName = CStr(datGV_NameList.Rows(intCursorPosition).Cells("clmFileName").Value)
        '##### Key line of code - having chosen to rename the file use tghe renamer function stored in the module ModRenamer

        strCorrectFileName = Renamer(strOriginalFileName)
        'Check to see the status of the naming table
        If bolCurrentDataUsed = True Then
            MsgBox(strCorrectFileName)
        Else
            'Give the user a chance to rename the file correctly.  Need to check in case user cancels.
            strCorrectFileName = InputBox("The data used for renaming is not the most recent." & vbNewLine & _
                                          "Please rename the file if appropriate", "Rename", strCorrectFileName)
        End If
    End Sub


    Private Sub ckBxOverrideLookupDB_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckBxOverrideLookupDB.CheckedChanged
        If myDataList Is Nothing Then
            ckBxOverrideLookupDB.Checked = False
        End If

        txtBxDNLookupPath.Enabled = ckBxOverrideLookupDB.Checked
        btnBrowseLookup.Enabled = ckBxOverrideLookupDB.Checked
    End Sub


    Private Sub btnBrowseLookup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseLookup.Click
        showSelectDNCLTdialog()
    End Sub

    Private Sub showSelectDNCLTdialog()
        Dim myDNCLfact As DataNameClauseLookupFactory
        Dim args(1) As String

        With OpenFileDialog
            .Multiselect = False
            .InitialDirectory = myDataList.getDetails()
            .Title = "Please select the Data Naming Convention tables"
            .Filter = "Access Database|*.mdb"
        End With

        If OpenFileDialog.ShowDialog = Windows.Forms.DialogResult.Cancel Then
            ckBxOverrideLookupDB.Checked = False
            txtBxDNLookupPath.Text = ""
            btnProcess.Enabled = False
        Else
            args(0) = OpenFileDialog.FileName
            myDNCLfact = DataNameClauseLookupFactory.getFactory()
            Try
                myDNCL = myDNCLfact.createDataNameClauseLookup(DATACLAUSE_LOOKUP_MDB, args)
                txtBxDNLookupPath.Text = myDNCL.getDetails()
                btnProcess.Enabled = True
            Catch ex As Exception
                showSelectDNCLTdialog()
            End Try

        End If
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        licHandler.dropESRILicence()
    End Sub

    Private Sub ckBxRecurse_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckBxRecurse.CheckedChanged
        If Not myDataList Is Nothing Then
            myDataList.setRecuse(ckBxRecurse.Checked)
        End If
    End Sub

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_HookHelper = Nothing
    End Sub

    Public Sub New(ByRef hook As Object)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_HookHelper = New HookHelper
        m_HookHelper.Hook = hook
    End Sub

End Class

