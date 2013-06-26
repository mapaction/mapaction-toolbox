Option Strict Off
Option Explicit On
Imports System.Runtime.InteropServices

Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs

<ComClass(maApplyDefaultSymbols.ClassId, maApplyDefaultSymbols.InterfaceId, maApplyDefaultSymbols.EventsId)> _
Public Class maApplyDefaultSymbols
    Inherits BaseCommand

    Private m_HookHelper As IHookHelper

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "9bac5701-d65d-4ce1-80de-a46d6b9c1226"
    Public Const InterfaceId As String = "62a2a150-fb77-44e8-a464-0782498de6ff"
    Public Const EventsId As String = "b5eb9ce7-ad34-4192-b0c3-573e538514c9"
#End Region

#Region "Component Category Registration"
    <ComRegisterFunction()> _
    Public Shared Sub Reg(ByVal regKey As String)
        MxCommands.Register(regKey)
    End Sub

    <ComUnregisterFunction()> _
    Public Shared Sub Unreg(ByVal regKey As String)
        MxCommands.Unregister(regKey)
    End Sub
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()

        MyBase.m_caption = "Apply Default Symbology"
        MyBase.m_category = "MapAction"
        MyBase.m_message = "Applies the default symbology to selected layers"
        MyBase.m_name = "MapAction_SymbolApply"
        MyBase.m_toolTip = "Applies the default symbology to selected layers"

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        m_HookHelper = Nothing
    End Sub

    Public Overrides Sub OnClick()
        'Dim pGxFile As IGxFile
        'Dim pGFLayer As IGeoFeatureLayer
        'Dim pGxLayer As IGxLayer
        'Dim pGxDialog As IGxDialog
        'Dim pGxObjFilter As IGxObjectFilter
        'Dim pEnumGxObj As IEnumGxObject
        'Dim pAnnoLayerPropsColl As IAnnotateLayerPropertiesCollection
        'Dim pGxObj As IGxObject
        'Dim pMxDoc As IMxDocument

        'Set pMxDoc = ThisDocument
        'If pMxDoc.SelectedLayer Is Nothing Then
        '    MsgBox "Please select feature class to label with .lyr file label classes"
        '    Exit Sub
        'End If
        'Set pGxDialog = New GxDialog
        'Set pGxObjFilter = New GxFilterLayers
        'Set pGxDialog.ObjectFilter = pGxObjFilter
        'pGxDialog.Title = "Select Layer(.lyr) file"
        'pGxDialog.ButtonCaption = "Apply Labels"

        'If pGxDialog.DoModalOpen(0, pEnumGxObj) Then
        '    Set pGxObj = pEnumGxObj.Next
        '    Set pGxLayer = pGxObj
        'Else
        '    Exit Sub
        'End If
        'Set pGFLayer = pGxLayer.Layer
        'Set pAnnoLayerPropsColl = pGFLayer.AnnotationProperties

        ''Apply label classes  to selected layer in arcmap
        'Set pGFLayer = pMxDoc.SelectedLayer
        'pGFLayer.AnnotationProperties = pAnnoLayerPropsColl
        'pGFLayer.DisplayAnnotation = True
        'pMxDoc.ActiveView.Refresh
        'pMxDoc.CurrentContentsView.Refresh pGFLayer

    End Sub



    Public Overrides Sub OnCreate(ByVal hook As Object)
        m_HookHelper = New HookHelperClass
        m_HookHelper.Hook = hook
    End Sub
End Class


