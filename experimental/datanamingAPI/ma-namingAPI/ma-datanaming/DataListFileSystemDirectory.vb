Imports System.IO
Imports System.Collections.Generic
Imports ESRI.ArcGIS.Geodatabase

Public Class DataListFileSystemDirectory
    Inherits AbstractGeoDataListConnection
    Private myDirInfo As DirectoryInfo
    Private myESRIWorkspace As IWorkspace

    Protected Friend Sub New(ByRef dirInfoArg As DirectoryInfo)
        setConstructorArgs(dirInfoArg)
    End Sub

    Protected Friend Sub New(ByVal pathNameArg As String)
        Dim dirInfoArg As DirectoryInfo

        dirInfoArg = New DirectoryInfo(pathNameArg)

        setConstructorArgs(dirInfoArg)
    End Sub

    Private Sub setConstructorArgs(ByRef dirInfo As DirectoryInfo)
        If Not dirInfo.Exists() Then
            Throw New ArgumentException("Non-existent directory: " & dirInfo.FullName)
        Else
            myDirInfo = dirInfo
            myESRIWorkspace = getESRIWorkspaceFromFile(New FileInfo(dirInfo.FullName))
        End If
    End Sub

    Public Overrides Function doesLayerExist(ByVal layerName As String) As Boolean
        Dim returnVal As Boolean

        If myDirInfo.GetFiles(layerName).Length = 1 Then
            returnVal = True
        Else
            returnVal = False
        End If

        Return returnVal
    End Function

    Public Overrides Function getDefaultDataNameClauseLookup() As IDataNameClauseLookup
        Dim theFactory As DataNameClauseLookupFactory
        Dim returnRef As IDataNameClauseLookup

        theFactory = DataNameClauseLookupFactory.getFactory()

        Try
            returnRef = theFactory.createDataNameClauseLookup(getMAActiveDataDir(myDirInfo.FullName))
        Catch ex As Exception
            returnRef = theFactory.createDataNameClauseLookup(myDirInfo)
        End Try

        Return returnRef
    End Function

    Public Overrides Function getDetails() As String
        Return myDirInfo.FullName
    End Function

    Public Overrides Function getGeoDataListConnectionType() As Integer
        Return dnListType.DIR
    End Function

    Public Overrides Function getGeoDataListConnectionTypeDesc() As String
        'todo LOW: move this to DataNameConstants module
        Return "DATALIST_TYPE_DIR"
    End Function

    Public Overrides Function getLayerDataNamesList(ByRef myDNCL As IDataNameClauseLookup) As List(Of IDataName)
        Dim returnList As New List(Of IDataName)
        Dim allowReNames As Boolean

        allowReNames = ((myDirInfo.Attributes And FileAttributes.ReadOnly) = FileAttributes.ReadOnly)

        'open the GIS related files using an Arc Workspace
        'Don't recuse here becuase this is taken care of by the directory recursing below
        For Each ds In getESRIDataSetsFromWorkspace(myESRIWorkspace, False)
            returnList.Add(New DataNameESRIFeatureClass(ds, myDNCL, allowReNames))
        Next

        'open the non-GIS related files using regular OS FileInfo Obj
        For Each fInfo In filterFilesForSpecialGISData(False)
            returnList.Add(New DataNameNormalFile(fInfo, myDNCL, allowReNames))
        Next

        'Now recurse if necessary
        If getRecuse() Then
            Dim dataList As IGeoDataListConnection

            For Each dInfo In filterDirsForSpecialGISData()
                dataList = New DataListFileSystemDirectory(dInfo)
                dataList.setRecuse(getRecuse())
                returnList.AddRange(dataList.getLayerDataNamesList(myDNCL))
            Next
        End If

        Return returnList
    End Function

    Public Overrides Function getLayerNamesStrings() As List(Of String)
        'Dim pDSName As IDatasetName
        'Dim pEnumDSName As IEnumDatasetName
        Dim strList As New List(Of String)

        'pEnumDSName = myESRIWorkspace.DatasetNames(esriDatasetType.esriDTAny)
        'pDSName = pEnumDSName.Next()

        'While Not pDSName Is Nothing
        '    'format names to ensure that any filename extensions are removed
        '    'If Not pDSName.Type = esriDatasetType.esriDTContainer Then
        '    System.Console.WriteLine("Name: " & pDSName.Name & "   type: " & pDSName.Type)
        '    namesList.Add(pDSName.Name.Substring(1 + pDSName.Name.LastIndexOf(".")))
        '    pDSName = pEnumDSName.Next()
        '    'End If
        'End While

        For Each curFileInfo In filterFilesForSpecialGISData()
            strList.Add(curFileInfo.Name)
        Next

        'Now recurse if necessary
        If getRecuse() Then
            Dim dataList As IGeoDataListConnection

            For Each dInfo In filterDirsForSpecialGISData()
                dataList = New DataListFileSystemDirectory(dInfo)
                dataList.setRecuse(getRecuse())
                strList.AddRange(dataList.getLayerNamesStrings())
            Next
        End If

        Return strList
    End Function

    ''' <summary>
    ''' Equivilant to filterFilesForSpecialGISData(true)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function filterFilesForSpecialGISData() As List(Of FileInfo)
        Return filterFilesForSpecialGISData(True)
    End Function

    ''' <summary>
    ''' Returns a list of FileInfo Objects, representing the files (not subdirectories) in the Directory, filtering out
    ''' all of the OS files which are part of logical GIS files. 
    ''' 
    ''' If an DBF or XML file is included which is not part of a logical GIS file, then these are included in the 
    ''' returned List.
    ''' </summary>
    ''' <param name="includeSpecialBaseFiles">If this is true then the basefiles of special GIS file eg. "example.shp" are 
    ''' included in the returned list. If False, all special GIS files including the basefiles are excluded</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function filterFilesForSpecialGISData(ByVal includeSpecialBaseFiles As Boolean) As List(Of FileInfo)
        Dim allFileInfos() As FileInfo
        Dim specialFileList As New List(Of String)
        Dim filteredFileInfos As New List(Of FileInfo)

        allFileInfos = myDirInfo.GetFiles()

        'First build a list of the "front" files for multiple OS-File storage
        For Each curFileInfo In allFileInfos
            'System.Console.WriteLine("curFileInfo.Name " & curFileInfo.Name & "   curFileInfo.FullName " & curFileInfo.FullName)
            'System.Console.WriteLine("curFileInfo.Name " & curFileInfo.Name.Remove(curFileInfo.Name.LastIndexOf(curFileInfo.Extension)) & "   curFileInfo.FullName " & curFileInfo.Extension)

            Select Case curFileInfo.Extension
                Case ".shp", ".bmp", ".gif", ".img", ".jpg", "jp2", ".png", ".tif", ".asc"
                    'Only add if not part of a shapefile
                    specialFileList.Add(curFileInfo.Name.Remove(curFileInfo.Name.LastIndexOf(curFileInfo.Extension)))
                    If includeSpecialBaseFiles Then
                        filteredFileInfos.Add(curFileInfo)
                    End If
            End Select
        Next

        'Now build a list of all of the files excluding those where the main part of the there name (e.g. before the 
        'extension) matches one which is in the specialFileList
        For Each curFileInfo In allFileInfos
            Dim found As Boolean = False

            For Each fileName In allBaseFileNameOptions(curFileInfo.Name)
                If specialFileList.Contains(fileName) Then
                    found = True
                End If
            Next

            If Not found Then
                filteredFileInfos.Add(curFileInfo)
            End If

        Next

        Return filteredFileInfos
    End Function

    ''' <summary>
    ''' Helper function for filterFilesForSpecialGISData()
    ''' </summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function allBaseFileNameOptions(ByVal fileName As String) As List(Of String)
        Dim allNameOptions As New List(Of String)
        Dim i As Integer = 1

        Do While fileName <> ""
            If fileName.LastIndexOf(".") >= 0 Then
                fileName = Left(fileName, fileName.LastIndexOf("."))
                allNameOptions.Add(fileName)
            Else
                fileName = ""
            End If
        Loop

        Return allNameOptions
    End Function

    Private Function filterDirsForSpecialGISData() As List(Of DirectoryInfo)
        Dim allDirInfos() As DirectoryInfo
        Dim filteredDirInfos As New List(Of DirectoryInfo)

        allDirInfos = myDirInfo.GetDirectories()

        For Each subDir In allDirInfos
            If Not subDir.Extension = ".gdb" Then
                filteredDirInfos.Add(subDir)
            End If
        Next

        Return filteredDirInfos
    End Function

End Class
