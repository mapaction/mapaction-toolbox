Module ModStandardFunctions
    Public bolCurrentDataUsed As Boolean        'PUBLIC VARIABLE TO CHECK THE STATUS OF THE DATA TABLE USED FOR THE NAMES

    Public Function ReturnFilePath(ByVal strDirectoryAndFileName As String) As String
        'Splitting the filepath out of the file path and name
        Dim iPos As Integer 'the position of the '/' between the file path and the file name
        iPos = strDirectoryAndFileName.LastIndexOf("\")
        ReturnFilePath = strDirectoryAndFileName.Substring(0, iPos)
    End Function

    Public Function ReturnFileName(ByVal strDirectoryAndFileName As String) As String
        'Splitting the filename out of the file path and name
        Dim iPos As Integer 'the position of the '/' between the file path and the file name
        iPos = strDirectoryAndFileName.LastIndexOf("\")
        ReturnFileName = strDirectoryAndFileName.Substring(iPos + 1, strDirectoryAndFileName.Length - iPos - 1)
    End Function

    'Public Sub CheckForMXD()
    '    'Lookup the running applications and see if a map window (MXD) is running
    '    'This isnt the best method - it would be better to get the application name and not the title but this is quite simple
    '    Dim bolMXD As Boolean   'use a flag for whether a map is running- prevents flickering radio button 
    '    bolMXD = True
    '    Dim procs As Process() = Process.GetProcesses
    '    For Each proc As Process In procs
    '        If proc.MainWindowHandle <> IntPtr.Zero Then 'Means it has a user interface open
    '            'Debug.WriteLine(proc.MainWindowTitle)
    '            If proc.MainWindowTitle Like ("*mxd*") Then 'theres a window open with mxd in the title bar. NB: What if there's two mxd's open?
    '                bolMXD = True
    '                Exit For
    '            Else
    '                bolMXD = False
    '            End If
    '        End If
    '    Next
    '    If bolMXD = True Then
    '        Main.rdbSelectCurMap.Enabled = True
    '    Else
    '        Main.rdbSelectCurMap.Enabled = False
    '    End If
    'End Sub

    Public Sub RunProgressBar()
        'set the progress bar values
        frmMain.pgbProgressBar.Minimum = 0
        frmMain.pgbProgressBar.Maximum = 100
        frmMain.pgbProgressBar.Value = 0
        'Make the progress bar run according to system events
        Dim iProgress As Integer
        For iProgress = 0 To 100
            frmMain.pgbProgressBar.Value = iProgress
            Application.DoEvents()  'make the progress bar run according to processor events
            'System.Threading.Thread.Sleep(1000) '### test purposes to slow things down a little
        Next
        If frmMain.pgbProgressBar.Value = 100 Then
            frmMain.pgbProgressBar.Value = 0   'Rest the progress bar
        End If

    End Sub

    Public Sub TestForUpdate()
        'This function checks the latest data is stored and writes it locally incase the laptop is being deployed

        bolCurrentDataUsed = False  'Global variable stored in ModStandardFunctions

        bolCurrentDataUsed = TestForLatestData(bolCurrentDataUsed)

        If bolCurrentDataUsed = True Then  'we have the correct database for the file names - Access in this case
            frmMain.lblDataStatus.Text = "Current data fine"
            frmMain.imgLatestDataLoaded.Image = My.Resources.TrafficLightGreen2 'show the green traffic light on the Main form

        Else    'Update Failed - so use the backup data resource
            frmMain.lblDataStatus.Text = "Update required"
            frmMain.imgLatestDataLoaded.Image = My.Resources.TrafficLightRed2   'show the red traffic light on the Main form
            frmMain.lblFilePath.Text = "Please update the file path to the correct directory"
            frmMain.lblFilePath.ForeColor = Color.DarkRed 'slightly classy
            frmMain.txtWorkingDirectory.ReadOnly = False
            'MsgBox("Warning: This machine does not have the latest" & vbNewLine & "naming files. It will use the stored version", MsgBoxStyle.Critical, "MapAction")
        End If
    End Sub

    Private Function TestForLatestData(ByVal bolCurrentDataUsed As Boolean) As Boolean
        Dim Con As New OleDb.OleDbConnection

        On Error GoTo NoConnection
        Con.ConnectionString = "PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source = C:\data-naming-conventions-beta_v0.8.mdb"
        TestForLatestData = False

        Con.Open()  'A Connection to the Database is now open
        '######### STORE DATASET INFORMATION HERE
        TestForLatestData = True
        Con.Close()   'The Connection to the Database is now Closed
        Exit Function
NoConnection:
        'The program cannot connect to the database
        MsgBox("Please place the file naming data in the location: " & vbNewLine & "C:\data-naming-conventions-beta_v0.8.mdb")
    End Function
End Module
