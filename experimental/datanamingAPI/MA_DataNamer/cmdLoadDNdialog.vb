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


<ComClass(cmdLoadDNdialog.ClassId, cmdLoadDNdialog.InterfaceId, cmdLoadDNdialog.EventsId)> _
Public Class cmdLoadDNdialog
    Inherits BaseCommand

    Private m_HookHelper As IHookHelper     'The hook that is passed to the OnCreate event
    Private frm As frmMain

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "1b0f5496-13c8-4628-9753-573f2a399c23"
    Public Const InterfaceId As String = "8d93a834-3d12-4db5-b5f5-4883f13f6dfb"
    Public Const EventsId As String = "a6ab3ab9-d753-44db-9a59-bdcea27f080d"
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

        'Create an IHookHelper object
        m_HookHelper = New HookHelperClass

        'Set the tool properties
        MyBase.m_caption = "Check Data Names"
        MyBase.m_category = "MapAction"
        MyBase.m_message = "Check Data Names"
        MyBase.m_name = "MapAction_NameCheck"
        MyBase.m_toolTip = "Checks the validity of the names of dataset"
    End Sub

    Public Overrides Sub OnCreate(ByVal hook As Object)
        m_HookHelper.Hook = hook
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        m_HookHelper = Nothing
    End Sub

    Public Overrides Sub OnClick()
        frm = New frmMain(m_HookHelper.Hook)
        frm = Nothing

    End Sub
End Class


