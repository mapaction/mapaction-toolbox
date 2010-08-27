Imports mapaction.datanames.api
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.CatalogUI
Imports ESRI.ArcGIS.Catalog
Imports System.IO
Imports System.Windows.Forms

Public Class DataListSelectorPanel_orgional

    Private m_DataList As IDataListConnection = Nothing
    Private m_dncl As IDataNameClauseLookup = Nothing
    Private m_blnIsParentArcMap As Boolean
    Private m_App As IApplication = Nothing
    Private m_opnFDialog As OpenFileDialog
    Private m_opnDDialog As New FolderBrowserDialog
    Private m_gxDialog As GxDialog
    Private m_strLastDataListPath As String = Nothing
    Private m_strLastDNCLPath As String
    Private m_blnReady As Boolean = False

    Public Event ReadinessChanged(ByVal blnNewReadiness As Boolean)

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        initialise()
        setNonArcMapDefaults()
    End Sub

    Public Sub setArcGISRef(ByRef app As IApplication)
        Dim strMXDPath As String

        If app IsNot Nothing Then
            m_blnIsParentArcMap = True

            Dim mxApp As IMxApplication
            Dim gxApp As IGxApplication

            mxApp = TryCast(app, IMxApplication)

            'Is the application ArcCatalog or ArcMap?
            'If mxApp IsNot Nothing Then
            If TypeOf app Is IMxApplication Then
                'ArcMap
                m_App = app
                radBtnCurMxDoc.Enabled = True
                radBtnCurMxDoc.Checked = True
                btnBrowseDataList.Enabled = False
                'Filter out the nonsense tempfile that is present 
                'before an mxd is saved
                strMXDpath = m_App.Templates.Item(m_App.Templates.Count - 1)
                If strMXDpath.EndsWith(".mxd") Then
                    txtBoxDataList.Text = strMXDpath
                    createNewDataList()
                End If

            ElseIf TypeOf app Is IGxApplication Then
                setNonArcMapDefaults()

                gxApp = TryCast(app, IGxApplication)
                If gxApp IsNot Nothing Then
                    txtBoxDataList.Text = gxApp.SelectedObject.FullName
                    radBtnGDB.Checked = True
                    createNewDataList()
                End If
            Else
                setNonArcMapDefaults()

            End If
        End If

    End Sub

    Private Sub setNonArcMapDefaults()
        m_blnIsParentArcMap = False
        radBtnCurMxDoc.Enabled = False
        radBtnDirectory.Checked = True
        btnBrowseDataList.Enabled = True
    End Sub

    Private Sub initialise()
        'Seup Open File Dialog
        m_opnFDialog = New OpenFileDialog
        m_opnFDialog.Multiselect = False
        m_opnFDialog.Title = "Select Data List"

        'Seup Open GDB Dialog
        m_gxDialog = New GxDialog
        m_gxDialog.AllowMultiSelect = False
        m_gxDialog.Title = "Select Data List"

        Dim gxFltrContain As IGxObjectFilter
        'Dim gxFltrFileGDB As IGxObjectFilter
        Dim gxFltrPerGDB As IGxObjectFilter

        Dim fltrCol As IGxObjectFilterCollection

        gxFltrContain = New GxFilterContainers
        'gxFltrFileGDB = New GxFilterFileGeodatabases
        gxFltrPerGDB = New GxFilterPersonalGeodatabases

        fltrCol = CType(m_gxDialog, IGxObjectFilterCollection)
        fltrCol.AddFilter(gxFltrContain, True)
        'fltrCol.AddFilter(gxFltrFileGDB, False)
        fltrCol.AddFilter(gxFltrPerGDB, False)

        'Seup Open Directory Dialog
        m_opnDDialog.Description = "Select Data List"
        m_opnDDialog.ShowNewFolderButton = False

        'Other stuff
        radBtnDCNL_MDB.Checked = True
    End Sub

    Public ReadOnly Property isReady() As Boolean
        Get
            Return m_blnReady
        End Get
    End Property

    Public ReadOnly Property DataListConnection() As IDataListConnection
        Get
            Return m_DataList
        End Get
    End Property

    Public ReadOnly Property DataNameClauseLookup() As IDataNameClauseLookup
        Get
            Return m_dncl
        End Get
    End Property

    Private Sub chkBxOverrideDNCLT_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBxOverrideDNCLT.CheckedChanged
        txtBoxDNCL.ReadOnly = Not chkBxOverrideDNCLT.Checked
        flwPlnDCNLtype.Enabled = chkBxOverrideDNCLT.Checked
        btnBrowseDNCLT.Enabled = chkBxOverrideDNCLT.Checked
    End Sub

    Private Sub doRadBtnChange()
        Dim strMXDpath As String

        Select Case True
            Case radBtnCurMxDoc.Checked
                btnBrowseDataList.Enabled = False
                chkBxRecurse.Enabled = False
                txtBoxDataList.ReadOnly = True

                'Filter out the nonsense tempfile that is present 
                'before an mxd is saved
                strMXDpath = m_App.Templates.Item(m_App.Templates.Count - 1)
                If strMXDpath.EndsWith(".mxd") Then
                    txtBoxDataList.Text = strMXDpath
                End If
                'txtBoxDataList.Text = m_App.Templates.

            Case radBtnDirectory.Checked
                btnBrowseDataList.Enabled = True
                chkBxRecurse.Enabled = True
                txtBoxDataList.ReadOnly = False
                createNewDataList()

            Case radBtnGDB.Checked
                btnBrowseDataList.Enabled = True
                chkBxRecurse.Enabled = True
                txtBoxDataList.ReadOnly = False
                createNewDataList()

            Case radBtnMXD.Checked
                btnBrowseDataList.Enabled = True
                chkBxRecurse.Enabled = False
                txtBoxDataList.ReadOnly = False
                createNewDataList()

        End Select


    End Sub

    Private Sub doBrowseDataList()
        'Dim strFilterText As String
        Dim blnNewPath As Boolean = False
        Dim strNewPath As String = Nothing

        If m_strLastDataListPath Is Nothing And txtBoxDataList.Text <> String.Empty Then
            m_strLastDataListPath = validatePath(txtBoxDataList.Text)
        End If

        Select Case True
            'The browse button should be disabled if radBtnCurMxDoc.Checked = True
            'so we'll ignore this possiblity.
            Case radBtnCurMxDoc.Checked
                strNewPath = Nothing

            Case radBtnDirectory.Checked
                If m_strLastDataListPath IsNot Nothing OrElse m_strLastDataListPath <> String.Empty Then
                    m_opnDDialog.SelectedPath = m_strLastDataListPath
                End If

                If m_opnDDialog.ShowDialog() = DialogResult.OK Then
                    strNewPath = m_opnDDialog.SelectedPath
                End If

            Case radBtnGDB.Checked
                Dim pEnumGx As IEnumGxObject

                If m_strLastDataListPath IsNot Nothing OrElse m_strLastDataListPath <> String.Empty Then
                    m_gxDialog.StartingLocation = m_strLastDataListPath
                End If

                If m_gxDialog.DoModalOpen(0, pEnumGx) Then
                    strNewPath = pEnumGx.Next.FullName
                End If

            Case radBtnMXD.Checked
                m_opnFDialog.Filter = "Mxd files (*.mxd)|*.mxd"
                If m_strLastDataListPath IsNot Nothing OrElse m_strLastDataListPath <> String.Empty Then
                    m_opnFDialog.InitialDirectory = m_strLastDataListPath
                End If

                m_opnFDialog.FileName = String.Empty

                If m_opnFDialog.ShowDialog = DialogResult.OK Then
                    strNewPath = m_opnFDialog.FileName
                End If

        End Select

        If strNewPath IsNot Nothing Then
            txtBoxDataList.Text = strNewPath
            m_strLastDataListPath = validatePath(strNewPath)
            createNewDataList()
        End If

    End Sub


    Private Sub createNewDataList()
        Dim dListFactory As DataListConnectionFactory
        Dim strAryArgs(1) As String

        System.Console.WriteLine(txtBoxDataList.Text)
        If isValidFileOrDirectory(txtBoxDataList.Text) Then

            dListFactory = DataListConnectionFactory.getFactory()
            strAryArgs(0) = txtBoxDataList.Text

            Try
                Select Case True
                    Case radBtnCurMxDoc.Checked
                        m_DataList = dListFactory.createDataListConnection(TryCast(m_App, IApplication))
                    Case radBtnDirectory.Checked
                        m_DataList = dListFactory.createDataListConnection(dnListType.DIR, strAryArgs)
                    Case radBtnGDB.Checked
                        m_DataList = dListFactory.createDataListConnection(dnListType.GDB, strAryArgs)
                    Case radBtnMXD.Checked
                        m_DataList = dListFactory.createDataListConnection(dnListType.MXD, strAryArgs)
                End Select

                m_DataList.setRecuse(chkBxRecurse.Checked)

                createDefaultDNCL()
                grpBoxDNCLT.Enabled = True
            Catch ex1 As Exception
                Dim strMsg As String = String.Format("Unable to open a valid Data List at :{0}{1}{2}Please check and try again.", Chr(13), txtBoxDataList.Text, Chr(13))

                MsgBox(strMsg, CType(MsgBoxStyle.ApplicationModal + MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, MsgBoxStyle), "Data List Selection")

                m_DataList = Nothing
                grpBoxDNCLT.Enabled = False
                setReadinessState(False)
            End Try
        Else
            m_DataList = Nothing
            grpBoxDNCLT.Enabled = False
            setReadinessState(False)
        End If

        m_strLastDataListPath = validatePath(txtBoxDataList.Text)
    End Sub

    ''' <summary>
    ''' Sets the value of m_strLastDataListPath
    ''' </summary>
    ''' <param name="strUnvalidiatedPath"></param>
    ''' <remarks>
    ''' Sets the value of m_strLastDataListPath
    ''' 
    ''' If strUnvalidiatedPath points to a valid directory then m_strLastDataListPath the directory
    ''' If strUnvalidiatedPath points to a valid file then m_strLastDataListPath the file's directory
    ''' If strUnvalidiatedPath does not point to a valid file or directory then m_strLastDataListPath 
    ''' is left unchanged.
    ''' </remarks>
    Private Function validatePath(ByVal strUnvalidiatedPath As String) As String
        Dim fInfo As FileInfo
        Dim strResult As String

        If strUnvalidiatedPath Is Nothing OrElse strUnvalidiatedPath = String.Empty Then
            strResult = Nothing
        Else
            'm_strLastDataList
            fInfo = New FileInfo(strUnvalidiatedPath)

            If fInfo.Exists() Then
                strResult = fInfo.Directory.FullName
            ElseIf (New DirectoryInfo(fInfo.FullName)).Exists() Then
                strResult = fInfo.FullName
            Else
                strResult = Nothing
            End If
        End If

        Return strResult
    End Function

    Private Function isValidFileOrDirectory(ByVal strPath As String) As Boolean
        If strPath Is Nothing OrElse strPath = String.Empty Then
            Return False
        Else
            Return isValidFileOrDirectory(New FileInfo(strPath))
        End If
    End Function

    Private Function isValidFileOrDirectory(ByVal fInfo As FileInfo) As Boolean
        If fInfo Is Nothing Then
            Return False
        Else
            'System.Console.WriteLine("fInfo.Exists() : " & fInfo.Exists())
            'System.Console.WriteLine("(New DirectoryInfo(fInfo.FullName)).Exists() : " & (New DirectoryInfo(fInfo.FullName)).Exists())
            Return fInfo.Exists() OrElse _
                        (New DirectoryInfo(fInfo.FullName)).Exists()
            '((fInfo.Attributes And FileAttributes.Directory) = FileAttributes.Directory)
        End If
    End Function

    Private Sub createDefaultDNCL()
        Try
            m_dncl = m_DataList.getDefaultDataNameClauseLookup()
            txtBoxDNCL.Text = m_dncl.getPath.FullName
            txtBoxDNCL.ReadOnly = True

            setReadinessState(True)
        Catch ex2 As Exception
            Dim strMsg As String = String.Format("Unable to automatically locate valid Data Name Clause Lookup Tables in the Current Data List at :{0}{1}{2}Please manually locate them below.", Chr(13), m_DataList.getPath(), Chr(13))

            MsgBox(strMsg, CType(MsgBoxStyle.ApplicationModal + MsgBoxStyle.OkOnly + MsgBoxStyle.Information, MsgBoxStyle), "Data List Selection")

            m_dncl = Nothing
            setReadinessState(False)
            chkBxOverrideDNCLT.Checked = True
        End Try
    End Sub

    Private Sub setReadinessState(ByVal blnNewValue As Boolean)
        If blnNewValue <> m_blnReady Then
            If blnNewValue And m_DataList IsNot Nothing And m_dncl IsNot Nothing Then
                m_blnReady = True
            Else
                m_blnReady = False
            End If

            RaiseEvent ReadinessChanged(blnNewValue)
        End If
    End Sub




    Private Sub createOverridedDNCL()
        Dim dnclFactory As DataNameClauseLookupFactory
        Dim strAryArgs(1) As String

        If isValidFileOrDirectory(txtBoxDNCL.Text) Then

            dnclFactory = DataNameClauseLookupFactory.getFactory()
            strAryArgs(0) = txtBoxDNCL.Text

            Try
                If radBtnDNCL_GDB.Checked = True Then
                    m_dncl = DataNameClauseLookupFactory.createDataNameClauseLookup(dnClauseLookupType.ESRI_GDB, strAryArgs)
                Else
                    m_dncl = DataNameClauseLookupFactory.createDataNameClauseLookup(dnClauseLookupType.MDB, strAryArgs)
                End If

                setReadinessState(True)
            Catch ex As Exception
                Dim strMsg As String = String.Format("Unable to locate valid Data Name Clause Lookup Tables at the specificed location :{0}{1}{2}Please try again.", Chr(13), txtBoxDNCL.Text, Chr(13))

                MsgBox(strMsg, CType(MsgBoxStyle.ApplicationModal + MsgBoxStyle.OkOnly + MsgBoxStyle.Information, MsgBoxStyle), "Data List Selection")

                m_dncl = Nothing
                setReadinessState(False)
            End Try
        Else
            setReadinessState(False)
        End If

    End Sub

    Private Sub doBrowseDNCL()
        Dim strNewDNCLPath As String = Nothing

        If m_strLastDNCLPath Is Nothing And txtBoxDNCL.Text <> String.Empty Then
            m_strLastDNCLPath = validatePath(txtBoxDNCL.Text)
        End If


        If radBtnDNCL_GDB.Checked Then

            Dim gxFltrContain As IGxObjectFilter
            Dim gxFltrFileGDB As IGxObjectFilter
            Dim gxFltrPerGDB As IGxObjectFilter

            Dim fltrCol As IGxObjectFilterCollection

            Dim pEnumGx As IEnumGxObject
            gxFltrContain = New GxFilterContainers
            gxFltrFileGDB = New GxFilterFileGeodatabases
            gxFltrPerGDB = New GxFilterPersonalGeodatabases

            fltrCol = CType(m_gxDialog, IGxObjectFilterCollection)
            fltrCol.AddFilter(gxFltrContain, True)
            fltrCol.AddFilter(gxFltrFileGDB, False)
            fltrCol.AddFilter(gxFltrPerGDB, False)
            If m_strLastDNCLPath IsNot Nothing OrElse m_strLastDNCLPath <> String.Empty Then
                m_gxDialog.StartingLocation = m_strLastDNCLPath
            End If

            If m_gxDialog.DoModalOpen(0, pEnumGx) Then
                strNewDNCLPath = pEnumGx.Next.FullName
            End If
        Else
            m_opnFDialog.Filter = "Access Database files (*.mdb)|*.mdb"
            If m_strLastDNCLPath IsNot Nothing OrElse m_strLastDNCLPath <> String.Empty Then
                m_opnFDialog.InitialDirectory = m_strLastDNCLPath
            End If

            m_opnFDialog.FileName = String.Empty

            If m_opnFDialog.ShowDialog = DialogResult.OK Then
                strNewDNCLPath = m_opnFDialog.FileName
            End If
        End If

        If strNewDNCLPath IsNot Nothing Then
            txtBoxDNCL.Text = strNewDNCLPath
            m_strLastDataListPath = validatePath(strNewDNCLPath)
        End If

        createOverridedDNCL()
    End Sub

    Private Sub chkBxRecurse_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBxRecurse.CheckedChanged
        If m_DataList IsNot Nothing Then
            m_DataList.setRecuse(chkBxRecurse.Checked)
        End If
    End Sub

    Private Sub txtBoxDNCL_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBoxDNCL.TextChanged
        If Me.ParentForm.Visible Then
            If chkBxOverrideDNCLT.Checked Then
                createOverridedDNCL()
            Else
                'createDefaultDNCL()
            End If

        End If
    End Sub

    Private Sub txtBoxDataList_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBoxDataList.TextChanged
        If Me.ParentForm.Visible Then
            createNewDataList()
        End If

    End Sub

    Private Sub btnBrowseDataList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseDataList.Click
        doBrowseDataList()
    End Sub

    Private Sub radBtnCurMxDoc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radBtnCurMxDoc.CheckedChanged
        doRadBtnChange()
    End Sub

    Private Sub radBtnMXD_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radBtnMXD.CheckedChanged
        doRadBtnChange()
    End Sub

    Private Sub radBtnGDB_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radBtnGDB.CheckedChanged
        doRadBtnChange()
    End Sub

    Private Sub radBtnDirectory_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radBtnDirectory.CheckedChanged
        doRadBtnChange()
    End Sub

    Private Sub btnBrowseDNCLT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseDNCLT.Click
        doBrowseDNCL()
    End Sub

End Class
