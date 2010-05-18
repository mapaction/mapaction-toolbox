Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Geodatabase
Imports System.Collections
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.IO

Public Class DataListMXDConnection
    Inherits AbstractGeoDataListConnection

    Private isMxDocument As Boolean
    Private myMapDocument As IMapDocument = Nothing
    Private myMxDocument As IMxDocument = Nothing

    Protected Friend Sub New(ByRef mxdFileInfo As FileInfo)
        MyClass.New(mxdFileInfo.FullName)
    End Sub

    Protected Friend Sub New(ByRef mxdFilePath As String)
        Dim mapDocArg As IMapDocument

        mapDocArg = New MapDocument

        If Not mapDocArg.IsPresent(mxdFilePath) Or Not mapDocArg.IsMapDocument(mxdFilePath) Or mapDocArg.IsPasswordProtected(mxdFilePath) Then
            'Cannot open MXD, throw Exception
            Throw New ArgumentException("Cannot open MXD file: " & mxdFilePath)
        Else
            mapDocArg.Open(mxdFilePath)
            'and mapDocArg.IsMap Then
            myMapDocument = mapDocArg
            isMxDocument = False
        End If
    End Sub

    Protected Friend Sub New(ByRef mxDoc As IMxDocument)
        myMxDocument = mxDoc
        isMxDocument = True
    End Sub

    Protected Friend Sub New(ByVal args() As String)
        MyClass.New(args(0))
    End Sub

    Private Function getMaps() As IMap()
        Dim mapArray() As IMap
        Dim mapGroup As IMaps

        If isMxDocument Then
            mapGroup = myMxDocument.Maps
            ReDim mapArray(mapGroup.Count - 1)
            For i As Integer = 0 To (mapGroup.Count - 1)
                mapArray.SetValue(mapGroup.Item(i), i)
            Next
        Else
            ReDim mapArray(myMapDocument.MapCount - 1)
            For i As Integer = 0 To (myMapDocument.MapCount - 1)
                mapArray.SetValue(myMapDocument.Map(i), i)
            Next
        End If

        Return mapArray
    End Function

    Private Function getMapCount() As Integer
        If isMxDocument Then
            Return myMxDocument.Maps.Count
        Else
            Return myMapDocument.MapCount
        End If
    End Function

    Public Overrides Function doesLayerExist(ByVal layerName As String) As Boolean
        'todo HIGH
    End Function

    Public Overrides Function getDefaultDataNameClauseLookup() As IDataNameClauseLookup
        Dim returnRef As IDataNameClauseLookup
        Dim mxdPath As String

        ' get current MXD name
        mxdPath = getFullPath()

        Try
            returnRef = DataNameClauseLookupFactory.getFactory().createDataNameClauseLookup(getMAActiveDataDir(mxdPath))
        Catch ex As Exception
            'todo LOW: put in some fancy foot work to loop through the layers and find the most common root for
            'the data and try that path(s)
            'For now we just throw an exception
            Throw ex
        End Try

        Return returnRef
    End Function

    Public Function getFullPath() As String
        Dim myApp As IApplication
        Dim detailsStr As String

        If isMxDocument Then
            'myApp = New AppRef()
            Dim t As Type = Type.GetTypeFromProgID("esriFramework.AppRef")
            Dim obj As System.Object = Activator.CreateInstance(t)
            Dim app As IApplication = CType(obj, IApplication)
            myApp = app


            ' get current MXD name
            detailsStr = myApp.Templates.Item(myApp.Templates.Count - 1)
        Else
            detailsStr = myMapDocument.DocumentFilename()
        End If

        Return detailsStr
    End Function

    Public Overrides Function getDetails() As String
        Dim myApp As IApplication
        Dim detailsStr As String

        If isMxDocument Then
            Dim t As Type = Type.GetTypeFromProgID("esriFramework.AppRef")
            Dim obj As System.Object = Activator.CreateInstance(t)
            Dim app As IApplication = CType(obj, IApplication)
            myApp = app

            ' get current MXD name
            detailsStr = myApp.Templates.Item(myApp.Templates.Count - 1)
        Else
            detailsStr = myMapDocument.DocumentFilename()
        End If

        Return detailsStr
    End Function

    Public Overrides Function getGeoDataListConnectionType() As Integer
        Return dnListType.MXD
    End Function

    Public Overrides Function getGeoDataListConnectionTypeDesc() As String
        'todo LOW: move this to DataNamingConstants
        Return "DATALIST_TYPE_MXD"
    End Function

    Public Overloads Overrides Function getLayerDataNamesList(ByRef myDNCL As IDataNameClauseLookup) As List(Of IDataName)
        Dim returnList As New List(Of IDataName)

        For Each ds In getUniqueESRIDataSets()
            returnList.Add(New DataNameESRIFeatureClass(ds, myDNCL, False))
        Next

        Return returnList
    End Function


    Private Function getUniqueESRIDataSets() As List(Of IDataset)
        Dim uniqueNames As New Hashtable()
        Dim curLayer As ILayer
        Dim curLyrName As String
        Dim dsList As New List(Of IDataset)

        For Each map In getMaps()
            For i As Integer = 0 To (map.LayerCount - 1)
                curLayer = map.Layer(i)

                If TypeOf curLayer Is IDataset Then
                    Dim ds As IDataset
                    ds = DirectCast(curLayer, IDataset)
                    If ds.Workspace.PathName.EndsWith(Path.DirectorySeparatorChar) Then
                        curLyrName = ds.Workspace.PathName & ds.Name
                    Else
                        curLyrName = ds.Workspace.PathName & Path.DirectorySeparatorChar & ds.Name
                    End If

                    'System.Console.WriteLine(ds.Name)
                    If IsDBNull(uniqueNames.Item(curLyrName)) Then
                        uniqueNames.Add(curLyrName, 1)
                        dsList.Add(ds)
                    End If
                End If
            Next
        Next

        Return dsList
    End Function

    Public Overrides Function getLayerNamesStrings() As List(Of String)
        Dim returnList As New List(Of String)

        For Each ds In getUniqueESRIDataSets()
            returnList.Add(ds.BrowseName)
        Next

        Return returnList
    End Function
End Class
