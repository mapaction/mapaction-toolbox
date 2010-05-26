Option Strict On

Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.DataSourcesGDB
Imports System.IO

''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
Public Class DataListGeoDBConnection
    Inherits AbstractGeoDataListConnection
    ' IGeoDataListConnection

    Private Const GDB_TYPE_MDB As Integer = CInt(2 ^ 1)
    Private Const GDB_TYPE_FILEGDB As Integer = CInt(2 ^ 2)
    Private Const GDB_TYPE_SDE_BY_PROPS As Integer = CInt(2 ^ 3)
    Private Const GDB_TYPE_SDE_BY_CONFILE As Integer = CInt(2 ^ 4)
    Private Const GDB_TYPE_SQL_EXPRESS As Integer = CInt(2 ^ 5)

    Private dataNameLookupWorkspace As ESRI.ArcGIS.Geodatabase.IWorkspace = Nothing
    Private myGDBtype As Integer

    Sub New(ByVal args() As String)
        dataNameLookupWorkspace = openList(args)

        If dataNameLookupWorkspace Is Nothing Then
            Throw New ArgumentException("Unable to open DataListGeoDBConnection")
        End If
    End Sub

    Sub New(ByRef fInfo As FileInfo)
        dataNameLookupWorkspace = openList(fInfo)

        If dataNameLookupWorkspace Is Nothing Then
            Throw New ArgumentException("Unable to open DataListGeoDBConnection")
        End If
    End Sub

    Private Function openList(ByVal myProps() As String) As ESRI.ArcGIS.Geodatabase.IWorkspace
        Dim returnRef As ESRI.ArcGIS.Geodatabase.IWorkspace = Nothing

        If myProps Is Nothing OrElse myProps.Length < 1 Then
            Throw New ArgumentNullException()
        ElseIf myProps.Length = 1 Or myProps(1) Is Nothing Then
            returnRef = getESRIWorkspaceFromFile(myProps(0))
        ElseIf myProps.Length = 2 Then
            'In this case we assume that myProps refer to a filesystem base GDB (Personal, File or SDE Connectionfile)
            If myProps(0).EndsWith(Path.DirectorySeparatorChar) Then
                returnRef = getESRIWorkspaceFromFile(myProps(0) & myProps(1))
            Else
                returnRef = getESRIWorkspaceFromFile(myProps(0) & Path.DirectorySeparatorChar & myProps(1))
            End If
        Else
            'todo: In this case we assume that myProps refer to connection parameters for opening a SDE or SDE Personal connection
            returnRef = Nothing
        End If

        Return returnRef
    End Function

    Private Function openList(ByRef fInfo As FileInfo) As ESRI.ArcGIS.Geodatabase.IWorkspace
        openList = getESRIWorkspaceFromFile(fInfo)
    End Function

    Public Overrides Function getGeoDataListConnectionType() As Integer
        getGeoDataListConnectionType = dnListType.GDB
    End Function

    Public Overrides Function getGeoDataListConnectionTypeDesc() As String
        'todo LOW: move this to DataNamingConstants
        getGeoDataListConnectionTypeDesc = "DATALIST TYPE is an ESRI GDB"
    End Function

    Public Overrides Function doesLayerExist(ByVal layerName As String) As Boolean
        Dim dSets As IEnumDatasetName
        Dim curDatasetName As IDatasetName
        Dim returnVal As Boolean = False

        dSets = dataNameLookupWorkspace.DatasetNames(esriDatasetType.esriDTAny)

        curDatasetName = dSets.Next()

        Do While (Not curDatasetName Is Nothing) And (Not returnVal)
            If curDatasetName.ToString() = layerName Then
                returnVal = True
            End If
        Loop

        doesLayerExist = returnVal
    End Function

    Public Overrides Function getLayerNamesStrings() As List(Of String)
        Return getNamesStrFromESRIDataSetName(getESRIDataSetNamesFromWorkspace(dataNameLookupWorkspace, getRecuse()))
    End Function

    Public Overrides Function getDetails() As String
        getDetails = dataNameLookupWorkspace.ConnectionProperties.ToString()
    End Function

    Public Overrides Function getDefaultDataNameClauseLookup() As IDataNameClauseLookup
        Dim defaultDNCL As IDataNameClauseLookup

        'System.Console.WriteLine("starting DataListGeoDBConnection.getDefaultDataNameClauseLookup()")
        Try
            'System.Console.WriteLine("Try 1")
            defaultDNCL = New GDBDataNameClauseLookup(dataNameLookupWorkspace)
        Catch ex1 As Exception
            'System.Console.WriteLine(ex1.ToString)
            Try
                'System.Console.WriteLine("Try 2")

                If dataNameLookupWorkspace.IsDirectory Then
                    defaultDNCL = New DataListFileSystemDirectory(dataNameLookupWorkspace.PathName).getDefaultDataNameClauseLookup()
                Else
                    defaultDNCL = New DataListFileSystemDirectory(New FileInfo(dataNameLookupWorkspace.PathName).DirectoryName) _
                                            .getDefaultDataNameClauseLookup()
                End If

            Catch ex2 As Exception
                System.Console.WriteLine(ex2.ToString)
                Throw New LookupTableException(dnLookupTableError.default_tbls_not_found, dataNameLookupWorkspace.PathName)
            End Try
        End Try

        Return defaultDNCL
    End Function

    Public Overrides Function getLayerDataNamesList(ByRef myDNCL As IDataNameClauseLookup) As List(Of IDataName)
        Dim dSetList As List(Of IDataset)
        Dim dNamesList As New List(Of IDataName)

        dSetList = getESRIDataSetsFromWorkspace(dataNameLookupWorkspace, getRecuse())

        For Each ds In dSetList
            dNamesList.Add(New DataNameESRIFeatureClass(ds, myDNCL, True))
        Next

        Return dNamesList
    End Function

End Class
