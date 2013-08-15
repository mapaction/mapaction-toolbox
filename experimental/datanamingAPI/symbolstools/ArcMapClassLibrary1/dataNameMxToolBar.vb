Option Strict Off
Option Explicit On
Imports System.Runtime.InteropServices

Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.ADF.CATIDs

<ComClass(dataNameMxToolBar.ClassId, dataNameMxToolBar.InterfaceId, dataNameMxToolBar.EventsId)> _
Public Class dataNameMxToolBar
    Implements IToolBarDef


#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "43a19e16-d91b-4d16-a231-12b5f821df01"
    Public Const InterfaceId As String = "df4992a0-a31e-4a0f-b641-6a484dfb5f44"
    Public Const EventsId As String = "867a9021-c3ce-4ffd-8c05-590d4dfa45b8"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
    End Sub


#Region "Component Category Registration"
    <ComRegisterFunction()> _
    Public Shared Sub Reg(ByVal regKey As String)
        MxCommandBars.Register(regKey)
    End Sub

    <ComUnregisterFunction()> _
    Public Shared Sub Unreg(ByVal regKey As String)
        MxCommandBars.Unregister(regKey)
    End Sub
#End Region

    Public ReadOnly Property Caption() As String Implements ESRI.ArcGIS.SystemUI.IToolBarDef.Caption
        Get
            Return "MapAction DataNaming"
        End Get
    End Property

    Public Sub GetItemInfo(ByVal pos As Integer, ByVal itemDef As ESRI.ArcGIS.SystemUI.IItemDef) Implements ESRI.ArcGIS.SystemUI.IToolBarDef.GetItemInfo
        Select Case pos
            Case 0
                itemDef.ID = "MA_DataNamer.cmdLoadDNdialog"
            Case 1
                itemDef.ID = "MA_DataNamer.maApplyDefaultSymbols"
        End Select
    End Sub

    Public ReadOnly Property ItemCount() As Integer Implements ESRI.ArcGIS.SystemUI.IToolBarDef.ItemCount
        Get
            Return 2
        End Get
    End Property

    Public ReadOnly Property Name() As String Implements ESRI.ArcGIS.SystemUI.IToolBarDef.Name
        Get
            Return "MapAction DataNaming Toolbar"
        End Get
    End Property
End Class


