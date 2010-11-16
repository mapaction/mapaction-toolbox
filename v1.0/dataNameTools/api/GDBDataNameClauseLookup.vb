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

Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.DataSourcesGDB
Imports System.IO
Imports System.Data
Imports System.Data.Common
Imports System.Data.OleDb

''' <summary>
''' Provides a specfic implenmentation of the IDataNameClauseLookup, based on storing the
''' Data Name Clause Lookup Tables in any form of ESRI GDB (file, personal, SDE etc)
''' </summary>
''' <remarks>
''' Provides a specfic implenmentation of the IDataNameClauseLookup, based on storing the
''' Data Name Clause Lookup Tables in any form of ESRI GDB (file, personal, SDE etc)
'''
''' There is no public constructor for this class. New instances should be generated using 
''' the XXXX factory class.
''' </remarks>
Public Class GDBDataNameClauseLookup
    Inherits AbstractDataNameClauseLookup

    'ESRI.ArcGIS.Utility.Converter.ToDataSet
    Private m_wkspDataNameLookup As IWorkspace = Nothing
    Private m_fInfoPath As FileInfo

    ''' <summary>
    ''' The constuctor. This should only be call from within the relevant 
    ''' factory class.
    ''' </summary>
    ''' <param name="dnlw"></param>
    ''' <remarks>
    ''' The constuctor. This should only be call from within the relevant 
    ''' factory class.
    ''' </remarks>
    Protected Friend Sub New(ByRef dnlw As ESRI.ArcGIS.Geodatabase.IWorkspace, ByVal lngReadWriteMode As Long)
        m_wkspDataNameLookup = dnlw
        m_fInfoPath = New FileInfo(dnlw.PathName)
        m_lngReadWriteMode = lngReadWriteMode
        initialiseAllTables()
    End Sub


    ''' <summary>
    ''' The constuctor. This should only be call from within the relevant 
    ''' factory class.
    ''' </summary>
    ''' <param name="strPathName">A string presenting the path </param>
    ''' <remarks>
    ''' The constuctor. This should only be call from within the relevant 
    ''' factory class.
    ''' </remarks>
    Protected Friend Sub New(ByVal strPathName As String, ByVal lngReadWriteMode As Long)
        'ESRI.ArcGIS.Geodatabase.IWorkspace()
        Dim lstWrkSp As List(Of IWorkspace)

        lstWrkSp = getESRIWorkspacesFromFile(strPathName)
        If lstWrkSp.Count = 1 Then
            m_wkspDataNameLookup = lstWrkSp.Item(0)
        Else
            Throw New ArgumentException("Unable to open GDBDataNameClauseLookup")
        End If

        m_fInfoPath = New FileInfo(strPathName)
        m_lngReadWriteMode = lngReadWriteMode
        initialiseAllTables()
    End Sub


    ''todo HIGH: rewrite this using the ESRI OLEDB Datasource driver.
    'Protected Overrides Function openTable(ByVal strTableName As String) As DataTable
    '    Dim enumDS As IEnumDataset
    '    Dim ds As IDataset
    '    Dim obj As Object
    '    Dim esriTbl As ITable
    '    Dim qryFtr As IQueryFilter2
    '    Dim rs As IRecordSet
    '    Dim dtb As DataTable

    '    enumDS = m_wkspDataNameLookup.Datasets(esriDatasetType.esriDTTable)

    '    obj = Nothing
    '    esriTbl = Nothing

    '    ds = enumDS.Next

    '    While Not ds Is Nothing
    '        'System.Console.WriteLine("Name:" & myIDataSet.Name & "    searchName: " & tableName)
    '        If ds.Name = strTableName Then
    '            'System.Console.WriteLine("Found searchName: " & tableName)
    '            obj = ds
    '        End If
    '        ds = enumDS.Next()
    '    End While

    '    If obj Is Nothing Then
    '        'The table was not found
    '        Throw New ArgumentException("tablename: " & strTableName & " not found in DB: " & getDetails())
    '    Else
    '        'System.Console.WriteLine("Found myObject: ")
    '        If TypeOf obj Is ITable Then
    '            esriTbl = DirectCast(obj, ITable)
    '            'System.Console.WriteLine("Found myESRITable: ")
    '        End If

    '        rs = New RecordSet
    '        Dim rsi As IRecordSetInit
    '        rsi = New RecordSet

    '        qryFtr = New QueryFilter()
    '        qryFtr.WhereClause = String.Empty

    '        'todo LOW: check whether or not passing a Null to IRecordSetInit.SetSourceTable
    '        'rsi.SetSourceTable(myESRITable, DBNull.Value)
    '        rsi.SetSourceTable(esriTbl, qryFtr)

    '        rs = DirectCast(rsi, IRecordSet)
    '        Dim myDTC As DataTableCollection
    '        myDTC = ESRI.ArcGIS.Utility.Converter.ToDataSet(rs).Tables()
    '        'For i As Short = 0 To (myDotNETDataTableCol.Count - 1) Step 1
    '        '    System.Console.WriteLine("table Name " & myDotNETDataTableCol.Item(i).ToString())

    '        '    myDotNETDataTableCol.Item(i).WriteXml(System.Console.OpenStandardOutput)

    '        'Next
    '        If myDTC.Count > 0 Then
    '            dtb = myDTC.Item(0)
    '        Else
    '            'The table was not found
    '            Throw New ArgumentException("tablename: " & strTableName & " not found in DB: " & getDetails())
    '        End If
    '    End If

    '    Return dtb
    'End Function

    'todo: HIGH rewirte this using esri IESRIDataSourceCreate.CreateDataSource method
    Protected Friend Overrides Function getDBDataAdapter() As System.Data.Common.DbDataAdapter
        Dim daResult As DbDataAdapter
        Dim strConnectPattern As String
        Dim strConnection As String
        Dim oleDBConnection As OleDb.OleDbConnection = Nothing
        Dim dbCommand As OleDb.OleDbCommand = New OleDb.OleDbCommand
        Dim strGeoDBType As String

        strGeoDBType = "WKB"
        'strGeoDBType = "OBJECT"

        If m_fInfoPath.Exists() Then
            Select Case m_fInfoPath.Extension
                Case ".mdb"
                    '"Provider=ESRI.GeoDB.OLEDB.1;{0};Extended Properties=WorkspaceType= esriCore.AccessWorkspaceFactory.1;Geometry={1}"
                    'strConnectPattern = System.Configuration.ConfigurationManager.AppSettings.Item(APP_CONF_GDB_PERSONAL_OLE_CONNECT_STRING)
                    strConnectPattern = GDB_PERSONAL_OLE_CONNECT_STRING
                    strConnection = String.Format(strConnectPattern, m_fInfoPath.FullName, strGeoDBType, m_lngReadWriteMode)

                Case ".sde", ".ags", ".gds"
                    '"Provider=ESRI.GeoDB.OLEDB.1;Extended Properties=WorkspaceType= esriDataSourcesGDB.SDEWorkspaceFactory.1;ConnectionFile={0}"
                    'strConnectPattern = System.Configuration.ConfigurationManager.AppSettings.Item(APP_CONF_GDB_SDE_OLE_CONNECT_STRING)
                    strConnectPattern = GDB_SDE_OLE_CONNECT_STRING
                    strConnection = String.Format(strConnectPattern, m_fInfoPath.FullName, m_lngReadWriteMode)

                Case Else
                    Throw New ArgumentException("GeoDatabase type not recgonised")
            End Select

        ElseIf (New DirectoryInfo(m_fInfoPath.FullName)).Exists() Then
            If m_fInfoPath.FullName.EndsWith(".gdb") Then
                '"Provider=ESRI.GeoDB.OLEDB.1;{0};Extended Properties=WorkspaceType= esriDataSourcesGDB.FileGDBWorkspaceFactory.1;Geometry={1}"
                'strConnectPattern = System.Configuration.ConfigurationManager.AppSettings.Item(APP_CONF_GDB_FILE_OLE_CONNECT_STRING)
                strConnectPattern = GDB_FILE_OLE_CONNECT_STRING
                strConnection = String.Format(strConnectPattern, strConnectPattern, strGeoDBType, m_lngReadWriteMode)
            Else
                Throw New ArgumentException("GeoDatabase type not recgonised")
            End If

        Else
            Throw New ArgumentException("Invalid path passed to New GDBDataNameClauseLookup(fileInfoArg)")

        End If

        'System.Console.WriteLine("getDBDataAdapter() strConnection: " & strConnection)

        oleDBConnection = New OleDbConnection(strConnection)
        oleDBConnection.Open()
        dbCommand.Connection = oleDBConnection

        daResult = New OleDb.OleDbDataAdapter(dbCommand)

        Return daResult
    End Function


    ''' <summary>
    ''' Returns the Path name used to connect to the GDB.
    ''' </summary>
    ''' <returns>The Path name used to connect to the GDB.</returns>
    ''' <remarks>
    ''' Returns the Path name used to connect to the GDB.
    ''' </remarks>
    Public Overrides Function getDetails() As String
        Return m_wkspDataNameLookup.PathName
    End Function

    ''' <summary>
    ''' Returns the Path name used to connect to the GDB.
    ''' </summary>
    ''' <returns>The Path name used to connect to the GDB.</returns>
    ''' <remarks>
    ''' Returns the Path name used to connect to the GDB.
    ''' </remarks>
    Public Overrides Function getPath() As fileinfo
        Return New FileInfo(m_wkspDataNameLookup.PathName)
    End Function

End Class
