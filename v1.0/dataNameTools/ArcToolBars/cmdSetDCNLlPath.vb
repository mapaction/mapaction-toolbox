Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI
Imports System.IO
Imports System.Windows.Forms
Imports mapaction.cartotools.api


<ComClass(cmdSetDCNLlPath.ClassId, cmdSetDCNLlPath.InterfaceId, cmdSetDCNLlPath.EventsId), _
 ProgId("mapaction.datanames.ArcToolBars.cmdSetDCNLlPath")> _
Public NotInheritable Class cmdSetDCNLlPath
    Inherits BaseCommand

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "c5b53818-955d-47b6-8e31-f3df753d48b3"
    Public Const InterfaceId As String = "8a0a0551-2562-4426-96d3-1975b86246c6"
    Public Const EventsId As String = "35d05578-dacd-42e6-80e9-e172ba55d103"
#End Region

#Region "COM Registration Function(s)"
    <ComRegisterFunction(), ComVisibleAttribute(False)> _
    Public Shared Sub RegisterFunction(ByVal registerType As Type)
        ' Required for ArcGIS Component Category Registrar support
        ArcGISCategoryRegistration(registerType)

        'Add any COM registration code after the ArcGISCategoryRegistration() call

    End Sub

    <ComUnregisterFunction(), ComVisibleAttribute(False)> _
    Public Shared Sub UnregisterFunction(ByVal registerType As Type)
        ' Required for ArcGIS Component Category Registrar support
        ArcGISCategoryUnregistration(registerType)

        'Add any COM unregistration code after the ArcGISCategoryUnregistration() call

    End Sub

#Region "ArcGIS Component Category Registrar generated code"
    Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        MxCommands.Register(regKey)

    End Sub
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        MxCommands.Unregister(regKey)

    End Sub

#End Region
#End Region


    Private m_application As IApplication
    Private m_opnfDialog As OpenFileDialog

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()

        ' TODO: Define values for the public properties
        MyBase.m_category = "MA Data Names"  'localizable text 
        MyBase.m_caption = "Set Symbols DCNL Path"   'localizable text 
        MyBase.m_message = "Set the path to the Data Name Lookup Tables for default symbology"   'localizable text 
        MyBase.m_toolTip = "Set the path to the Data Name Lookup Tables for default symbology" 'localizable text 
        MyBase.m_name = "ma_Data_Names_Set_Symbols_DNCL_Path"  'unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")



    End Sub


    Public Overrides Sub OnCreate(ByVal hook As Object)
        If Not hook Is Nothing Then
            m_application = CType(hook, IApplication)

            'Disable if it is not ArcMap
            If TypeOf hook Is IMxApplication Then
                MyBase.m_enabled = True


                'Seup Open Directory Dialog
                m_opnfDialog = New OpenFileDialog
                m_opnfDialog.Title = "Select Path to Data Name Lookup Symbols MDB"
                m_opnfDialog.Filter = "Access MDB files (*.mdb)|*.mdb"


            Else
                MyBase.m_enabled = False
            End If
        End If

        ' TODO:  Add other initialization code
    End Sub

    Public Overrides Sub OnClick()
        'TODO: Add cmdSetDCNLlPath.OnClick implementation
        Dim nWnd As NativeWindow

        nWnd = New NativeWindow()
        nWnd.AssignHandle(New IntPtr(Me.m_application.hWnd))

        If m_opnfDialog.ShowDialog(nWnd) = DialogResult.OK Then
            rootSymbDNCLpath = New FileInfo(m_opnfDialog.FileName)
        End If

    End Sub
End Class



