Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports System.Runtime.InteropServices

<ComClass(maBronzeArcMapToolBar.ClassId, maBronzeArcMapToolBar.InterfaceId, maBronzeArcMapToolBar.EventsId), _
 ProgId("mapaction.datanames.ArcToolBars.maBronzeArcMapToolBar")> _
Public NotInheritable Class maBronzeArcMapToolBar
    Inherits BaseToolbar

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
    ''' <summary>
    ''' Required method for ArcGIS Component Category registration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        MxCommandBars.Register(regKey)

    End Sub
    ''' <summary>
    ''' Required method for ArcGIS Component Category unregistration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        MxCommandBars.Unregister(regKey)

    End Sub

#End Region
#End Region

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "57b45a4e-6724-4673-8994-a9fcb4594805"
    Public Const InterfaceId As String = "f8fd36b0-321a-4757-a78f-340a16918db6"
    Public Const EventsId As String = "5c60fafb-c78f-4eba-99d5-30f4fc7e5bed"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()

        '
        'TODO: Define your toolbar here by adding items
        '
        'AddItem("esriArcMapUI.ZoomOutTool")
        'BeginGroup() 'Separator
        'AddItem("{FBF8C3FB-0480-11D2-8D21-080009EE4E51}", 1) 'undo command
        'AddItem(New Guid("FBF8C3FB-0480-11D2-8D21-080009EE4E51"), 2) 'redo command
        AddItem("mapaction.datanames.ArcToolBars.cmdDataNameChecker")
        BeginGroup()
        AddItem("mapaction.datanames.ArcToolBars.cmdSetSymbolRoot")
        'AddItem("mapaction.datanames.ArcToolBars.cmdSetDCNLlPath")
        AddItem("mapaction.datanames.ArcToolBars.cmdApplyDefaultSymbology")
    End Sub

    Public Overrides ReadOnly Property Caption() As String
        Get
            'TODO: Replace bar caption
            Return "MA Data Name Tools"
        End Get
    End Property

    Public Overrides ReadOnly Property Name() As String
        Get
            'TODO: Replace bar ID
            Return "maBronzeArcMapToolBar"
        End Get
    End Property
End Class
