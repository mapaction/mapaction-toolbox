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

Imports System.Runtime.InteropServices
Imports System.Drawing
Imports System.Windows.Forms
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.CatalogUI
Imports mapaction.datanames.gui

<ComClass(cmdDataNameChecker.ClassId, cmdDataNameChecker.InterfaceId, cmdDataNameChecker.EventsId), _
 ProgId("mapaction.datanames.ArcToolBars.cmdDataNameChecker")> _
Public NotInheritable Class cmdDataNameChecker
    Inherits BaseCommand

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "107c9234-9c69-4737-b25d-a6271767d094"
    Public Const InterfaceId As String = "d3d7601f-a6fd-4766-86fd-a0c5b2e3255d"
    Public Const EventsId As String = "6d09694c-c57a-468d-8c51-41269579e6d3"
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
    Private m_frmMain As frmMain

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()

        ' TODO: Define values for the public properties
        MyBase.m_category = "MA Data Names"  'localizable text 
        MyBase.m_caption = "Data Name Checker"   'localizable text 
        MyBase.m_message = "Checks that the names of dataset match the naming convention"   'localizable text 
        MyBase.m_toolTip = "Checks that the names of dataset match the naming convention" 'localizable text 
        MyBase.m_name = "ma_Data_Names_Launch_Checker_Dialog"  'unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")

    End Sub


    Public Overrides Sub OnCreate(ByVal hook As Object)
        If Not hook Is Nothing Then
            m_application = CType(hook, IApplication)

            'Disable if it is not ArcMap
            If TypeOf hook Is IMxApplication Then
                MyBase.m_enabled = True
                m_frmMain = New frmMain(m_application)

            ElseIf TypeOf hook Is IGxApplication Then
                MyBase.m_enabled = True
                m_frmMain = New frmMain(m_application)
            End If
        End If

        'Add other initialization code here
    End Sub

    Public Overrides Sub OnClick()
        Dim nWnd As NativeWindow

        nWnd = New NativeWindow()
        nWnd.AssignHandle(New IntPtr(Me.m_application.hWnd))

        m_frmMain.updateArcGISRef(Me.m_application)
        m_frmMain.Show(nWnd)
    End Sub
End Class



