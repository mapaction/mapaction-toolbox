﻿Imports ESRI.ArcGIS.Geodatabase
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
    Private m_wkspDataNameLookup As ESRI.ArcGIS.Geodatabase.IWorkspace = Nothing
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
    Protected Friend Sub New(ByRef dnlw As ESRI.ArcGIS.Geodatabase.IWorkspace)
        m_wkspDataNameLookup = dnlw
        m_fInfoPath = New FileInfo(dnlw.PathName)
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
    Protected Friend Sub New(ByVal strPathName As String)
        'ESRI.ArcGIS.Geodatabase.IWorkspace()
        m_wkspDataNameLookup = getESRIWorkspaceFromFile(strPathName)
        m_fInfoPath = New FileInfo(strPathName)
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

    Protected Friend Overrides Function getDBDataAdapter() As System.Data.Common.DbDataAdapter
        Dim daResult As DbDataAdapter
        Dim strConnectPattern As String
        Dim strConnection As String
        Dim oleDBConnection As OleDb.OleDbConnection = Nothing
        Dim dbCommand As OleDb.OleDbCommand = New OleDb.OleDbCommand
        Dim strGeoDBType As String

        strGeoDBType = "WKB"
        'strGeoDBType = "OBJECT"

        If ((m_fInfoPath.Attributes And FileAttributes.Directory) = FileAttributes.Directory) Then
            If m_fInfoPath.FullName.EndsWith(".gdb") Then
                '"Provider=ESRI.GeoDB.OLEDB.1;{0};Extended Properties=WorkspaceType= esriDataSourcesGDB.FileGDBWorkspaceFactory.1;Geometry={1}"
                'strConnectPattern = System.Configuration.ConfigurationManager.AppSettings.Item(APP_CONF_GDB_FILE_OLE_CONNECT_STRING)
                strConnectPattern = GDB_FILE_OLE_CONNECT_STRING
                strConnection = String.Format(strConnectPattern, strConnectPattern, strGeoDBType)

            Else
                Throw New ArgumentException("GeoDatabase type not recgonised")
            End If

        ElseIf Not m_fInfoPath.Exists() Then
            Select Case m_fInfoPath.Extension
                Case ".mdb"
                    '"Provider=ESRI.GeoDB.OLEDB.1;{0};Extended Properties=WorkspaceType= esriCore.AccessWorkspaceFactory.1;Geometry={1}"
                    'strConnectPattern = System.Configuration.ConfigurationManager.AppSettings.Item(APP_CONF_GDB_PERSONAL_OLE_CONNECT_STRING)
                    strConnectPattern = GDB_PERSONAL_OLE_CONNECT_STRING
                    strConnection = String.Format(strConnectPattern, strConnectPattern, strGeoDBType)

                Case ".sde", ".ags", ".gds"
                    '"Provider=ESRI.GeoDB.OLEDB.1;Extended Properties=WorkspaceType= esriDataSourcesGDB.SDEWorkspaceFactory.1;ConnectionFile={0}"
                    'strConnectPattern = System.Configuration.ConfigurationManager.AppSettings.Item(APP_CONF_GDB_SDE_OLE_CONNECT_STRING)
                    strConnectPattern = GDB_SDE_OLE_CONNECT_STRING
                    strConnection = String.Format(strConnectPattern, strConnectPattern)

                Case Else
                    Throw New ArgumentException("GeoDatabase type not recgonised")
            End Select

        Else
            Throw New ArgumentException("Invalid path passed to New GDBDataNameClauseLookup(fileInfoArg)")

        End If

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