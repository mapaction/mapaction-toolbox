Imports System.IO
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.DataSourcesGDB
Imports ESRI.ArcGIS.DataSourcesFile

''' <summary>
''' A private convenience class to help search directories for the DataName Clause Lookup table 
''' in whatever form they may take.
''' </summary>
''' <remarks></remarks>
Friend Module MADirectorySearcher

    Public Function getMARootDir(ByVal pathName As String) As DirectoryInfo
        Dim fInfo As FileInfo

        fInfo = New FileInfo(pathName)

        Return getMARootDir(fInfo)
    End Function

    Public Function getMARootDir(ByRef dirInfoArg As DirectoryInfo) As DirectoryInfo
        Dim returnDir As DirectoryInfo

        If dirInfoArg.Exists() AndAlso (dirInfoArg.GetDirectories(MA_DIR_STRUCT_DATA_DIR).Length > 0) Then
            'We've found the root of the MA directory structure return this
            returnDir = dirInfoArg
        ElseIf IsDBNull(dirInfoArg.Parent) Then
            'we've got to the system root directory without finding the top of the MA directory structure
            Throw New ArgumentException("Unable to find directory " & MA_DIR_STRUCT_DATA_DIR)
        Else
            returnDir = getMARootDir(dirInfoArg.Parent)
        End If

        Return returnDir
    End Function

    Public Function getMARootDir(ByRef fileInfoArg As FileInfo) As DirectoryInfo
        Dim returnDir As DirectoryInfo

        If fileInfoArg.Exists() And (Not (fileInfoArg.Attributes And FileAttributes.Directory) = FileAttributes.Directory) Then
            returnDir = getMARootDir(fileInfoArg.Directory)
        ElseIf (fileInfoArg.Attributes And FileAttributes.Directory) = FileAttributes.Directory Then
            Dim dInfo As DirectoryInfo
            dInfo = New DirectoryInfo(fileInfoArg.FullName)
            returnDir = getMARootDir(dInfo)
        Else
            Throw New ArgumentException("File not found " & fileInfoArg.FullName)
        End If

        Return returnDir
    End Function

    Public Function getMAActiveDataDir(ByVal pathName As String) As DirectoryInfo
        Return appendMAActiveDataDir(getMARootDir(pathName))
    End Function
    Public Function getMAActiveDataDir(ByRef dirInfoArg As DirectoryInfo) As DirectoryInfo
        Return appendMAActiveDataDir(getMARootDir(dirInfoArg))
    End Function
    Public Function getMAActiveDataDir(ByRef fileInfoArg As FileInfo) As DirectoryInfo
        Return appendMAActiveDataDir(getMARootDir(fileInfoArg))
    End Function

    Private Function appendMAActiveDataDir(ByRef rootDirInfoArg As DirectoryInfo) As DirectoryInfo
        Dim returnDir As DirectoryInfo

        If rootDirInfoArg.GetDirectories(MA_DIR_STRUCT_DATA_DIR).length = 1 Then
            returnDir = CType(rootDirInfoArg.GetDirectories(MA_DIR_STRUCT_DATA_DIR).GetValue(0), DirectoryInfo)
        Else
            Throw New ArgumentException("Unable to find ActiveData directory " & _
                                        MA_DIR_STRUCT_DATA_DIR & " in path " & rootDirInfoArg.FullName)
        End If

        Return returnDir
    End Function


    ''' <summary>
    ''' Searchs a directory and returns are list for all possible GDBs or GDB connection files with the directory
    ''' </summary>
    ''' <param name="dirArg"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function getGDBsInDir(ByRef dirArg As DirectoryInfo) As List(Of FileInfo)
        Dim containedFiles() As FileInfo
        Dim containedDirs() As DirectoryInfo
        Dim returnList As New List(Of FileInfo)

        If Not dirArg.Exists() Then
            Throw New ArgumentException("DataNameClauseLookup tables could not be found in not existant directory " & dirArg.FullName)
        Else
            containedDirs = dirArg.GetDirectories("*.gdb")

            For Each dirInLoop In containedDirs
                'System.Console.WriteLine("getGDBsInDir, found GDB: " & dirInLoop.Extension & "  " & dirInLoop.Name)
                returnList.Add(New FileInfo(dirInLoop.FullName))
            Next


            containedFiles = dirArg.GetFiles()

            'Search for File GDBs amogst imediate subdirectories
            'Seach for other GDBs or connections files based on known file extension types

            'todo Investigate soft coding the list of possible GDB file extensions
            For Each curFile In containedFiles
                'System.Console.WriteLine("getGDBsInDir, found GDB: " & curFile.Extension & "  " & curFile.Name)
                Select Case curFile.Extension
                    Case ".mdb", ".sde", ".ags"
                        'System.Console.WriteLine("getGDBsInDir, found GDB: " & fileOrDir.FullName)
                        returnList.Add(curFile)
                End Select

            Next

        End If

        Return returnList
    End Function

    Friend Function getESRIWorkspaceFromFile(ByVal fullFilePath As String) As ESRI.ArcGIS.Geodatabase.IWorkspace
        Dim fInfo As FileInfo

        fInfo = New FileInfo(fullFilePath)

        Return getESRIWorkspaceFromFile(fInfo)
    End Function

    Friend Function getESRIWorkspaceFromFile(ByVal fInfo As FileInfo) As ESRI.ArcGIS.Geodatabase.IWorkspace
        Dim returnRef As ESRI.ArcGIS.Geodatabase.IWorkspace = Nothing

        If fInfo.Exists() And (Not (fInfo.Attributes And FileAttributes.Directory) = FileAttributes.Directory) Then
            Dim workspaceFactory As IWorkspaceFactory

            Select Case fInfo.Extension
                Case ".mdb"
                    workspaceFactory = New AccessWorkspaceFactoryClass
                    'myGDBtype = GDB_TYPE_MDB
                Case ".sde", ".ags", ".gds"
                    workspaceFactory = New SdeWorkspaceFactory
                    'myGDBtype = GDB_TYPE_SDE_BY_CONFILE
                Case Else
                    Throw New ArgumentException("GeoDatabase type not recgonised")
            End Select

            returnRef = workspaceFactory.OpenFromFile(fInfo.FullName, 0)

        ElseIf (fInfo.Attributes And FileAttributes.Directory) = FileAttributes.Directory Then
            Dim dInfo As DirectoryInfo
            dInfo = New DirectoryInfo(fInfo.FullName)

            Dim workspaceFactory As IWorkspaceFactory2

            If dInfo.FullName.EndsWith(".gdb") Then
                workspaceFactory = New FileGDBWorkspaceFactoryClass
                'myGDBtype = GDB_TYPE_FILEGDB
            Else
                workspaceFactory = New ShapefileWorkspaceFactory
            End If

            returnRef = workspaceFactory.OpenFromFile(dInfo.FullName, 0)

        End If

        Return returnRef
    End Function

    Friend Function getESRIDataSetNamesFromWorkspace(ByRef wrksp As IWorkspace, ByVal recuse As Boolean) As List(Of IDatasetName)
        Return getESRIDataSetNamesFromWorkspace(wrksp.DatasetNames(esriDatasetType.esriDTAny), recuse)
    End Function

    Friend Function getESRIDataSetNamesFromWorkspace(ByRef edn As IEnumDatasetName, ByVal recuse As Boolean) As List(Of IDatasetName)
        Dim dNamesList As New List(Of IDatasetName)
        Dim dsn As IDatasetName

        dsn = edn.Next()

        While Not dsn Is Nothing
            If recuse And (dsn.Type = esriDatasetType.esriDTContainer) Then
                dNamesList.AddRange(getESRIDataSetNamesFromWorkspace(dsn.SubsetNames, recuse))
            Else
                dNamesList.Add(dsn)
            End If
            dsn = edn.Next()
        End While

        Return dNamesList
    End Function

    Friend Function getNamesStrFromESRIDataSetName(ByRef dsList As List(Of IDatasetName)) As List(Of String)
        Dim strList As New List(Of String)

        For Each ds In dsList
            strList.Add(ds.Name)
        Next

        Return strList
    End Function


    Friend Function getESRIDataSetsFromWorkspace(ByRef wrksp As IWorkspace, ByVal recuse As Boolean) As List(Of IDataset)
        Return getESRIDataSetsFromWorkspace(wrksp.Datasets(esriDatasetType.esriDTAny), recuse)
    End Function

    Friend Function getESRIDataSetsFromWorkspace(ByRef eds As IEnumDataset, ByVal recuse As Boolean) As List(Of IDataset)
        Dim dSetList As New List(Of IDataset)
        Dim ds As IDataset

        ds = eds.Next()

        While Not ds Is Nothing
            If recuse And (ds.Type = esriDatasetType.esriDTContainer) Then
                dSetList.AddRange(getESRIDataSetsFromWorkspace(ds.Subsets, recuse))
            Else
                dSetList.Add(ds)
            End If
            ds = eds.Next()
        End While

        Return dSetList
    End Function

End Module
