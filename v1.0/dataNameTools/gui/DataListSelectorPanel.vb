'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''Copyright (C) 2010 MapAction UK Charity No. 1075977
''
''www.mapaction.org
''
''This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 3 of the License, or (at your option) any later version.
''
''This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
''
''You should have received a copy of the GNU General Public License along with this program; if not, see <http://www.gnu.org/licenses>.
''
''Additional permission under GNU GPL version 3 section 7
''
''If you modify this Program, or any covered work, by linking or combining it with 
''ESRI ArcGIS Desktop Products (ArcView, ArcEditor, ArcInfo, ArcEngine Runtime and ArcEngine Developer Kit) (or a modified version of that library), containing parts covered by the terms of ESRI's single user or concurrent use license, the licensors of this Program grant you additional permission to convey the resulting work.
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports mapaction.datanames.api
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.CatalogUI
Imports ESRI.ArcGIS.Catalog
Imports System.IO
Imports System.Windows.Forms

Public Enum DataListSelectorPanelStatus As Short
    SELECT_DATALIST = 1
    UNREADABLE_DATALIST = 2
    SELECT_DNCL = 3
    NO_AUTO_DNCL = 4
    UNREADABLE_DNCL = 5
    READY = 6
End Enum


Public Class DataListSelectorPanel

    Private m_DataList As IDataListConnection = Nothing
    Private m_dncl As IDataNameClauseLookup = Nothing
    Private m_blnIsParentArcMap As Boolean
    Private m_App As IApplication = Nothing
    Private m_opnFDialog As OpenFileDialog
    Private m_opnDDialog As New FolderBrowserDialog
    Private m_gxDialog As GxDialog


    'Private m_strLastDataListPath As String
    Private m_strLastDataListDirPath As String = Nothing
    Private m_strLastDataListMXDPath As String = Nothing
    Private m_strLastDataListGDBPath As String = Nothing
    Private m_blnLastDataListDirRecuse As Boolean = True
    Private m_blnLastDataListGDBRecuse As Boolean = True

    Private m_strLastDNCLPath As String
    Private m_blnDataListReady As Boolean = False
    Private m_blnDNCLReady As Boolean = False

    Private m_dlspStatus As DataListSelectorPanelStatus

    Public Event statusChanged(ByVal srtNewStatus As DataListSelectorPanelStatus)
    Public Event inputError(ByVal strErrorMsg As String)

    Public m_htbStatusStrings As Dictionary(Of DataListSelectorPanelStatus, String)


    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        initialise()
        setNonArcMapDefaults()
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

        'Setup Status Strings
        m_dlspStatus = DataListSelectorPanelStatus.SELECT_DATALIST
        m_htbStatusStrings = New Dictionary(Of DataListSelectorPanelStatus, String)

        m_htbStatusStrings.Add(DataListSelectorPanelStatus.SELECT_DATALIST, "Please select a list of data layers")
        m_htbStatusStrings.Add(DataListSelectorPanelStatus.UNREADABLE_DATALIST, "The selected list of data layers could not be read")
        m_htbStatusStrings.Add(DataListSelectorPanelStatus.SELECT_DNCL, "Please select Data Name Clause Lookup Tables")
        m_htbStatusStrings.Add(DataListSelectorPanelStatus.NO_AUTO_DNCL, "Data Name Clause Lookup Tables could not be located automatically")
        m_htbStatusStrings.Add(DataListSelectorPanelStatus.UNREADABLE_DNCL, "The selected Data Name Clause Lookup Tables could not be read")
        m_htbStatusStrings.Add(DataListSelectorPanelStatus.READY, "Ready")

        'Other stuff
        m_radBtnDCNL_Manual_MDB.Checked = True
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
                m_radBtnCurMxDoc.Enabled = True
                m_radBtnCurMxDoc.Checked = True
                m_btnBrowseDataList.Enabled = False
                'Filter out the nonsense tempfile that is present 
                'before an mxd is saved
                strMXDpath = m_App.Templates.Item(m_App.Templates.Count - 1)
                If strMXDpath.EndsWith(".mxd") Then
                    m_txtBoxDataList.Text = strMXDpath
                    'createNewDataList()
                End If

            ElseIf TypeOf app Is IGxApplication Then
                setNonArcMapDefaults()

                gxApp = TryCast(app, IGxApplication)
                If gxApp IsNot Nothing Then
                    m_txtBoxDataList.Text = gxApp.SelectedObject.FullName
                    m_radBtnGDB.Checked = True
                    'createNewDataList()
                End If
            Else
                setNonArcMapDefaults()

            End If
        End If

    End Sub

    Private Sub setNonArcMapDefaults()
        m_blnIsParentArcMap = False
        m_radBtnCurMxDoc.Enabled = False
        m_radBtnDirectory.Checked = True
        m_btnBrowseDataList.Enabled = True
    End Sub


    Public ReadOnly Property getStatus() As DataListSelectorPanelStatus
        Get
            Return m_dlspStatus
        End Get
    End Property

    Public Function getStatusString(ByVal status As DataListSelectorPanelStatus) As String
        Return m_htbStatusStrings.Item(status)
    End Function

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

    Private Sub handleDataListChange(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles m_txtBoxDataList.TextChanged, _
                    m_radBtnCurMxDoc.CheckedChanged, _
                    m_radBtnMXD.CheckedChanged, _
                    m_radBtnGDB.CheckedChanged, _
                    m_radBtnDirectory.CheckedChanged

        Dim strMXDpath As String
        Dim dListFactory As DataListConnectionFactory
        Dim strAryArgs(1) As String

        System.Console.WriteLine(sender)
        System.Console.WriteLine(e)

        If TypeOf sender Is System.Windows.Forms.RadioButton Then

            Select Case True
                Case m_radBtnCurMxDoc.Checked
                    m_btnBrowseDataList.Enabled = False
                    m_chkBxRecurse.Enabled = False
                    m_txtBoxDataList.ReadOnly = True

                    'Filter out the nonsense tempfile that is present 
                    'before an mxd is saved
                    strMXDpath = m_App.Templates.Item(m_App.Templates.Count - 1)
                    If strMXDpath.EndsWith(".mxd") Then
                        m_txtBoxDataList.Text = strMXDpath
                    End If
                    'txtBoxDataList.Text = m_App.Templates.

                Case m_radBtnDirectory.Checked
                    m_btnBrowseDataList.Enabled = True
                    m_chkBxRecurse.Enabled = True
                    m_txtBoxDataList.ReadOnly = False
                    'createNewDataList()

                    If m_strLastDataListDirPath IsNot Nothing Then
                        m_txtBoxDataList.Text = m_strLastDataListDirPath
                        m_chkBxRecurse.Checked = m_blnLastDataListDirRecuse
                    Else
                        m_txtBoxDataList.Text = validatePath(m_txtBoxDataList.Text, True)
                    End If

                Case m_radBtnGDB.Checked
                    m_btnBrowseDataList.Enabled = True
                    m_chkBxRecurse.Enabled = True
                    m_txtBoxDataList.ReadOnly = False
                    'createNewDataList()

                    If m_strLastDataListGDBPath IsNot Nothing Then
                        m_txtBoxDataList.Text = m_strLastDataListGDBPath
                        m_chkBxRecurse.Checked = m_blnLastDataListGDBRecuse
                    Else
                        m_txtBoxDataList.Text = validatePath(m_txtBoxDataList.Text, False)
                    End If


                Case m_radBtnMXD.Checked
                    m_btnBrowseDataList.Enabled = True
                    m_chkBxRecurse.Enabled = False
                    m_txtBoxDataList.ReadOnly = False
                    'createNewDataList()

                    If m_strLastDataListMXDPath IsNot Nothing Then
                        m_txtBoxDataList.Text = m_strLastDataListMXDPath
                    Else
                        If isValidFileOrDirectory(m_txtBoxDataList.Text) Then
                            'do this anyway to trigger the textchange event
                            m_txtBoxDataList.Text = m_txtBoxDataList.Text
                        Else
                            m_txtBoxDataList.Text = validatePath(m_txtBoxDataList.Text, True)
                        End If

                    End If

            End Select
        End If
        'ElseIf TypeOf sender Is System.Windows.Forms.TextBox Then


        System.Console.WriteLine(m_txtBoxDataList.Text)
        If isValidFileOrDirectory(m_txtBoxDataList.Text) Then

            dListFactory = DataListConnectionFactory.getFactory()
            strAryArgs(0) = m_txtBoxDataList.Text

            Try
                Select Case True
                    Case m_radBtnCurMxDoc.Checked
                        m_DataList = dListFactory.createDataListConnection(TryCast(m_App, IApplication))
                    Case m_radBtnDirectory.Checked
                        m_DataList = dListFactory.createDataListConnection(dnListType.DIR, strAryArgs)
                        m_strLastDataListDirPath = strAryArgs(0)
                        m_blnLastDataListDirRecuse = m_chkBxRecurse.Checked
                    Case m_radBtnGDB.Checked
                        m_DataList = dListFactory.createDataListConnection(dnListType.GDB, strAryArgs)
                        m_strLastDataListGDBPath = strAryArgs(0)
                        m_blnLastDataListGDBRecuse = m_chkBxRecurse.Checked
                    Case m_radBtnMXD.Checked
                        m_DataList = dListFactory.createDataListConnection(dnListType.MXD, strAryArgs)
                        m_strLastDataListMXDPath = strAryArgs(0)
                End Select

                m_DataList.setRecuse(m_chkBxRecurse.Checked)

                'setting the DNCL will be triggered from within the setReadinessState() method
                setReadinessState()
            Catch ex1 As Exception
                Dim strMsg As String = String.Format("Unable to open a valid Data List at :{0}{1}{2}Please check and try again.", Chr(13), m_txtBoxDataList.Text, Chr(13))

                'MsgBox(ex1.ToString)

                m_DataList = Nothing
                setReadinessState()
                RaiseEvent inputError(strMsg)
            End Try
        Else
            m_DataList = Nothing
            setReadinessState()

            If m_txtBoxDataList.Text <> String.Empty Then
                Dim strMsg As String = String.Format("Unable to open a valid Data List at :{0}{1}{2}Please check and try again.", Chr(13), m_txtBoxDataList.Text, Chr(13))
                RaiseEvent inputError(strMsg)
            End If

            End If

    End Sub

    Private Sub handleDNCLChange(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles _
                    m_txtBoxDNCL.TextChanged, _
                    m_grpBoxDNCLT.EnabledChanged, _
                    m_radBtn_AutoDNCLT.CheckedChanged, _
                    m_radBtn_ManualDNCLT.CheckedChanged, _
                    m_radBtn_FallbackDNCLT.CheckedChanged, _
                    m_radBtnDCNL_Manual_MDB.CheckedChanged, _
                    m_radBtnDNCL_Manual_GDB.CheckedChanged, _
                    m_chkBxReadOnly.CheckedChanged

        If m_grpBoxDNCLT.Enabled Then
            'Set various GUI elements
            m_txtBoxDNCL.ReadOnly = Not m_radBtn_ManualDNCLT.Checked
            m_flwPlnDCNLmanualType.Enabled = m_radBtn_ManualDNCLT.Checked
            m_btnBrowseDNCLT.Enabled = m_radBtn_ManualDNCLT.Checked
            m_chkBxReadOnly.Enabled = m_radBtn_ManualDNCLT.Checked

            Select Case True
                Case m_radBtn_AutoDNCLT.Checked
                    'create automatic/default DNCL
                    Try
                        m_dncl = m_DataList.getDefaultDataNameClauseLookup()
                        m_txtBoxDNCL.Text = m_dncl.getPath.FullName
                        m_txtBoxDNCL.ReadOnly = True
                        m_chkBxReadOnly.Checked = False

                        setReadinessState()
                    Catch ex2 As Exception
                        Dim strMsg As String = String.Format("Unable to automatically locate valid Data Name Clause Lookup Tables in the Current Data List at :{0}{1}{2}Please manually locate them below.", Chr(13), m_DataList.getPath(), Chr(13))

                        RaiseEvent inputError(strMsg)
                        'MsgBox(strMsg, CType(MsgBoxStyle.ApplicationModal + MsgBoxStyle.OkOnly + MsgBoxStyle.Information, MsgBoxStyle), "Data List Selection")

                        m_dncl = Nothing
                        setReadinessState()
                        m_radBtn_ManualDNCLT.Checked = True
                    End Try

                Case m_radBtn_ManualDNCLT.Checked
                    'Manually specify DNCL
                    Dim dnclFactory As DataNameClauseLookupFactory
                    Dim strAryArgs(1) As String

                    If isValidFileOrDirectory(m_txtBoxDNCL.Text) Then

                        dnclFactory = DataNameClauseLookupFactory.getFactory()
                        strAryArgs(0) = m_txtBoxDNCL.Text

                        Try
                            If m_radBtnDNCL_Manual_GDB.Checked = True Then
                                m_dncl = DataNameClauseLookupFactory.createDataNameClauseLookup(dnClauseLookupType.ESRI_GDB, strAryArgs, (Not m_chkBxReadOnly.Checked))
                            Else
                                m_dncl = DataNameClauseLookupFactory.createDataNameClauseLookup(dnClauseLookupType.MDB, strAryArgs, (Not m_chkBxReadOnly.Checked))
                            End If

                            setReadinessState()
                        Catch ex As Exception
                            Dim strMsg As String = String.Format("Unable to locate valid Data Name Clause Lookup Tables at the specificed location :{0}{1}{2}Please try again.", Chr(13), m_txtBoxDNCL.Text, Chr(13))

                            m_dncl = Nothing
                            setReadinessState()

                            RaiseEvent inputError(strMsg)
                            'MsgBox(strMsg, CType(MsgBoxStyle.ApplicationModal + MsgBoxStyle.OkOnly + MsgBoxStyle.Information, MsgBoxStyle), "Data List Selection")
                        End Try
                    Else
                        setReadinessState()
                    End If

                Case m_radBtn_FallbackDNCLT.Checked
                    Try
                        m_dncl = DataNameClauseLookupFactory.getFallBackDataNameClauseLookup()
                        m_txtBoxDNCL.Text = m_dncl.getPath.FullName
                        m_txtBoxDNCL.ReadOnly = True
                        m_chkBxReadOnly.Checked = True

                        setReadinessState()
                    Catch ex As Exception
                        Dim strMsg As String = String.Format("Unable to locate built-in Data Name Clause Lookup Tables. Please check your installation. Please locate them manually.")
                        MsgBox(ex.ToString)

                        RaiseEvent inputError(strMsg)
                        'MsgBox(strMsg, CType(MsgBoxStyle.ApplicationModal + MsgBoxStyle.OkOnly + MsgBoxStyle.Information, MsgBoxStyle), "Data List Selection")

                        m_dncl = Nothing
                        setReadinessState()
                        m_radBtn_ManualDNCLT.Checked = True
                    End Try
            End Select

        Else
            'if m_grpBoxDNCLT is not enabled then set m_dncl to Nothing. Should be already but just in case.
            m_dncl = Nothing
        End If

        'Private Sub txtBoxDNCL_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBoxDNCL.TextChanged
        '    If Me.ParentForm.Visible Then
        '        If m_chkBxOverrideDNCLT.Checked Then
        '            createOverridedDNCL()
        '        Else
        '            'createDefaultDNCL()
        '        End If

        '    End If
        'End Sub

        'Private Sub txtBoxDataList_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBoxDataList.TextChanged
        '    If Me.ParentForm.Visible Then
        '        createNewDataList()
        '    End If

        'End Sub

    End Sub

    ''' <summary>
    ''' Attempts to reutrn a valid path based on the string provided.
    ''' </summary>
    ''' <param name="strUnvalidiatedPath"></param>
    ''' <remarks>
    ''' Attempts to reutrn a valid path based on the string provided.
    ''' 
    ''' If strUnvalidiatedPath points to a valid directory then m_strLastDataListPath the directory
    ''' If strUnvalidiatedPath points to a valid file then m_strLastDataListPath the file's directory
    ''' If strUnvalidiatedPath does not point to a valid file or directory then the value 'Nothing' is returned
    ''' </remarks>
    Private Function validatePath(ByVal strUnvalidiatedPath As String, ByVal blnExcludeFileGDB As Boolean) As String
        Dim strResult As String

        If strUnvalidiatedPath Is Nothing OrElse strUnvalidiatedPath = String.Empty Then
            strResult = Nothing
        Else
            'm_strLastDataList
            strResult = validatePath(New FileInfo(strUnvalidiatedPath), blnExcludeFileGDB)
        End If

        Return strResult
    End Function

    Private Function validatePath(ByRef fInfoUnvalidiatedPath As FileInfo, ByVal blnExcludeFileGDB As Boolean) As String
        Dim strResult As String

        If fInfoUnvalidiatedPath.Exists() Then
            strResult = validatePath(fInfoUnvalidiatedPath.Directory, blnExcludeFileGDB)
        ElseIf (fInfoUnvalidiatedPath.Attributes And FileAttributes.Directory) = FileAttributes.Directory Then
            strResult = validatePath(New DirectoryInfo(fInfoUnvalidiatedPath.FullName), blnExcludeFileGDB)
        Else
            strResult = Nothing
        End If

        Return strResult
    End Function

    Private Function validatePath(ByRef dInfoUnvalidiatedPath As DirectoryInfo, ByVal blnExcludeFileGDB As Boolean) As String
        Dim strResult As String

        If dInfoUnvalidiatedPath.Exists() Then
            If blnExcludeFileGDB AndAlso dInfoUnvalidiatedPath.FullName.EndsWith(".gdb") Then
                strResult = validatePath(dInfoUnvalidiatedPath.Parent, blnExcludeFileGDB)
            Else
                strResult = dInfoUnvalidiatedPath.FullName
            End If
        Else
            strResult = Nothing
        End If

        Return strResult
    End Function

    Private Function isValidFileOrDirectory(ByVal strPath As String) As Boolean
        Dim blnResult As Boolean

        If strPath Is Nothing OrElse strPath = String.Empty Then
            blnResult = False
        Else
            Try
                blnResult = isValidFileOrDirectory(New FileInfo(strPath))
            Catch ex As Exception
                blnResult = False
            End Try
        End If

        Return blnResult
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

    Private Sub setReadinessState()
        Dim dlspNewStatus As DataListSelectorPanelStatus

        If m_DataList Is Nothing Then
            m_picBxDataListReadiness.Image = My.Resources.icoTrafficLightRed
            m_picBxDNCLReadiness.Image = My.Resources.icoTrafficLightRed

            m_grpBoxDNCLT.Enabled = False

            dlspNewStatus = DataListSelectorPanelStatus.SELECT_DATALIST
        Else
            m_picBxDataListReadiness.Image = My.Resources.icoTrafficLightGreen
            m_grpBoxDNCLT.Enabled = True

            If m_dncl Is Nothing Then
                m_picBxDNCLReadiness.Image = My.Resources.icoTrafficLightRed

                dlspNewStatus = DataListSelectorPanelStatus.SELECT_DNCL
            Else
                m_picBxDNCLReadiness.Image = My.Resources.icoTrafficLightGreen

                dlspNewStatus = DataListSelectorPanelStatus.READY
            End If
        End If

        If dlspNewStatus <> m_dlspStatus Then
            m_dlspStatus = dlspNewStatus
            RaiseEvent statusChanged(dlspNewStatus)
        End If


        'If blnNewValue <> m_blnReady Then
        '    If blnNewValue And m_DataList IsNot Nothing And m_dncl IsNot Nothing Then
        '        m_blnReady = True
        '    Else
        '        m_blnReady = False
        '    End If

        '    'RaiseEvent statusChanged(  ReadinessChanged(blnNewValue)
        'End If
    End Sub

    Private Sub chkBxRecurse_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles m_chkBxRecurse.CheckedChanged
        If m_radBtnDirectory.Checked Then
            m_blnLastDataListDirRecuse = m_chkBxRecurse.Checked
        ElseIf m_radBtnGDB.Checked Then
            m_blnLastDataListGDBRecuse = m_chkBxRecurse.Checked
        End If

        If m_DataList IsNot Nothing Then
            m_DataList.setRecuse(m_chkBxRecurse.Checked)
        End If
    End Sub

    Private Sub btnBrowseDataList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles m_btnBrowseDataList.Click
        Dim strNewPath As String = Nothing
        Dim strStartingPath As String

        Select Case True
            'The browse button should be disabled if radBtnCurMxDoc.Checked = True
            'so we'll ignore this possiblity.
            Case m_radBtnCurMxDoc.Checked
                strNewPath = Nothing

            Case m_radBtnDirectory.Checked
                strStartingPath = validatePath(m_txtBoxDataList.Text, True)
                If strStartingPath IsNot Nothing Then
                    m_opnDDialog.SelectedPath = strStartingPath
                Else
                    m_opnDDialog.SelectedPath = m_strLastDataListDirPath
                End If

                If m_opnDDialog.ShowDialog() = DialogResult.OK Then
                    strNewPath = m_opnDDialog.SelectedPath
                End If

            Case m_radBtnGDB.Checked
                Dim pEnumGx As IEnumGxObject

                strStartingPath = validatePath(m_txtBoxDataList.Text, False)
                If strStartingPath IsNot Nothing Then
                    m_gxDialog.StartingLocation = strStartingPath
                ElseIf m_strLastDataListDirPath IsNot Nothing Then
                    m_gxDialog.StartingLocation = m_strLastDataListGDBPath
                End If

                If m_gxDialog.DoModalOpen(0, pEnumGx) Then
                    strNewPath = pEnumGx.Next.FullName
                End If

            Case m_radBtnMXD.Checked
                m_opnFDialog.Filter = "Mxd files (*.mxd)|*.mxd"

                strStartingPath = validatePath(m_txtBoxDataList.Text, True)
                If strStartingPath IsNot Nothing Then
                    m_opnFDialog.InitialDirectory = strStartingPath
                Else
                    m_opnFDialog.InitialDirectory = validatePath(m_strLastDataListMXDPath, True)
                End If

                m_opnFDialog.FileName = String.Empty

                If m_opnFDialog.ShowDialog = DialogResult.OK Then
                    strNewPath = m_opnFDialog.FileName
                End If

        End Select

        If strNewPath IsNot Nothing Then
            m_txtBoxDataList.Text = strNewPath
        End If

    End Sub

    Private Sub btnBrowseDNCLT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles m_btnBrowseDNCLT.Click
        Dim strNewDNCLPath As String = Nothing

        If m_strLastDNCLPath Is Nothing And m_txtBoxDNCL.Text <> String.Empty Then
            m_strLastDNCLPath = validatePath(m_txtBoxDNCL.Text, False)
        End If

        If m_radBtnDNCL_Manual_GDB.Checked Then

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
            m_txtBoxDNCL.Text = strNewDNCLPath
            m_strLastDNCLPath = validatePath(strNewDNCLPath, m_radBtnDCNL_Manual_MDB.Checked)
        End If

    End Sub


End Class
