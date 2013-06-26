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
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI
Imports mapaction.cartotools.api

<ComClass(cmdApplyDefaultSymbology.ClassId, cmdApplyDefaultSymbology.InterfaceId, cmdApplyDefaultSymbology.EventsId), _
 ProgId("mapaction.datanames.ArcToolBars.cmdApplyDefaultSymbology")> _
Public NotInheritable Class cmdApplyDefaultSymbology
    Inherits BaseCommand

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "1ed95d67-15a5-4acd-8637-3217192c935f"
    Public Const InterfaceId As String = "124434e8-1f71-4fc7-a544-b064f66d43c8"
    Public Const EventsId As String = "53c45982-b75f-42fd-9668-998185f11454"
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
    'Private m_defSymb As DefaultSymbololgy

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()

        ' TODO: Define values for the public properties
        MyBase.m_category = "MA Data Names"  'localizable text 
        MyBase.m_caption = "Apply Default Symbology"   'localizable text 
        MyBase.m_message = "Apply Default Symbology to the currently selected layers"   'localizable text 
        MyBase.m_toolTip = "Apply Default Symbology to the currently selected layers" 'localizable text 
        MyBase.m_name = "ma_Data_Names_Apply_Default_Symbology"  'unique id, non-localizable (e.g. "MyCategory_MyCommand")


    End Sub


    Public Overrides Sub OnCreate(ByVal hook As Object)
        If Not hook Is Nothing Then
            m_application = CType(hook, IApplication)

            'Disable if it is not ArcMap
            If TypeOf hook Is IMxApplication Then
                MyBase.m_enabled = True
                'm_defSymb = DefaultSymbololgy.getDefaultSymbololgy()

            Else
                MyBase.m_enabled = False
            End If
        End If

        'Add other initialization code here
    End Sub

    Public Overrides Sub OnClick()
        'todo MEDIUM: Add Map/PageLayout related logic
        Dim strResult As String
        Dim mxDoc As IMxDocument

        Try

            mxDoc = CType(m_application.Document, IMxDocument)
            'MsgBox("did cast")
            strResult = applyDefaultSymbols(mxDoc, m_application)
            'MsgBox("did applyDefaultSymbols")
        Catch ex As Exception
            strResult = "An Error occured:" & vbNewLine & ex.Message
            strResult = strResult & vbNewLine & ex.InnerException.Message
            strResult = strResult & vbNewLine & "..."
        Finally
            MsgBox(strResult, CType(MsgBoxStyle.Information + MsgBoxStyle.OkOnly, MsgBoxStyle), _
                   "Default Symbology Tool")
            'MsgBox("bye!")
        End Try

    End Sub
End Class



