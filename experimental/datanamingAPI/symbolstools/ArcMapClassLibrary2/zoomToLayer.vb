Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Carto
Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI

<ComClass(zoomToLayer.ClassId, zoomToLayer.InterfaceId, zoomToLayer.EventsId), _
 ProgId("ArcMapClassLibrary2.zoomToLayer")> _
Public NotInheritable Class zoomToLayer
    Inherits BaseCommand

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "bb9210bb-f6eb-4da1-92dd-a1b63b8e49b6"
    Public Const InterfaceId As String = "66c39bf6-9210-4aef-9d04-f99ef254d044"
    Public Const EventsId As String = "3b99e83a-1334-4e32-a8a9-5f8f4ecc1af2"
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

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()

        ' TODO: Define values for the public properties
        MyBase.m_category = "Dev Samples"  'localizable text 
        MyBase.m_caption = "Zoom to layer in VB.net"   'localizable text 
        MyBase.m_message = "Zoom to layer in VB.net"   'localizable text 
        MyBase.m_toolTip = "sghsdfvhs" 'localizable text 
        MyBase.m_name = "dev_sample_ZoomToLayerVBNET"  'unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")

        Try
            'TODO: change bitmap name if necessary
            Dim bitmapResourceName As String = Me.GetType().Name + ".bmp"
            MyBase.m_bitmap = New Bitmap(Me.GetType(), bitmapResourceName)
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap")
        End Try


    End Sub


    Public Overrides Sub OnCreate(ByVal hook As Object)
        If Not hook Is Nothing Then
            m_application = CType(hook, IApplication)

            'Disable if it is not ArcMap
            If TypeOf hook Is IMxApplication Then
                MyBase.m_enabled = True
            Else
                MyBase.m_enabled = False
            End If
        End If

        ' TODO:  Add other initialization code
    End Sub

    

#Region "Get MxDocument from ArcMap"


    '''<summary>Get MxDocument from ArcMap.</summary>
    '''<param name="application">An IApplication interface that is the ArcMap application.</param>
    '''<returns>An IMxDocument interface.</returns>
    '''<remarks></remarks>
    Public Function GetMxDocumentFromArcMap(ByVal application As IApplication) As IMxDocument

        If application Is Nothing Then
            Return Nothing
        End If

        Dim document As IDocument = application.Document
        Dim mxDocument As IMxDocument = CType(document, IMxDocument) ' Explicit Cast

        Return mxDocument

    End Function
#End Region

#Region "Zoom to Active Layer in TOC"

    '''<summary>Zooms to the selected layer in the TOC associated with the active view.</summary>
    ''' 
    '''<param name="mxDocument">An IMxDocument interface</param>
    ''' 
    '''  
    '''
    '''<remarks></remarks>
    ''' 
    Public Sub ZoomToActiveLayerInTOC(ByVal mxDocument As IMxDocument)


        If mxDocument Is Nothing Then
            Return
        End If

        ' Get the map
        Dim activeView As IActiveView = mxDocument.ActiveView

        ' Get the TOC
        Dim contentsView As IContentsView = mxDocument.CurrentContentsView

        ' Get the selected layer
        Dim selectedItem As System.Object = contentsView.SelectedItem
        If Not (TypeOf selectedItem Is ILayer) Then
            Return
        End If

        Dim layer As ILayer = TryCast(selectedItem, ILayer) ' Dynamic Cast

        ' Zoom to the extent of the layer and refresh the map
        activeView.Extent = layer.AreaOfInterest
        activeView.Refresh()

    End Sub

#End Region

    Public Overrides Sub OnClick()
        'TODO: Add zoomToLayer.OnClick implementation
        Dim mxDocument As IMxDocument = GetMxDocumentFromArcMap(m_application)
        ZoomToActiveLayerInTOC(mxDocument)

    End Sub

End Class



