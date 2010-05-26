Imports System.IO
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.DataSourcesGDB
Imports ESRI.ArcGIS.DataSourcesFile

'todo HIGH check the required access (public, friend, private) requirements for each method in this module.
''' <summary>
''' A private convenience module to help search directories for the DataName
''' Clause Lookup table in whatever form they may take.
''' </summary>
''' <remarks>
''' A private convenience module to help search directories for the DataName
''' Clause Lookup table in whatever form they may take.
''' </remarks>
Friend Module MADirectorySearcher


    ''' <summary>
    ''' Attempts to find and return the root of the MapAction directory structure.
    ''' Typically this is in the form "yyyy-mm-dd-destination". However the search
    ''' is conducted by resurcavely looking and the contents of the parent directory
    ''' and looking for the MA_DIR_STRUCT_DATA_DIR (normally "2_Active_Data").
    ''' </summary>
    ''' <param name="strPathName">A string representing the either a directory or a
    ''' file as the starting location for the search.</param>
    ''' <returns>A DirectoryInfo object pointing to the root of the MapAction
    ''' directory structure.</returns>
    ''' <remarks>
    ''' Attempts to find and return the root of the MapAction directory structure.
    ''' Typically this is in the form "yyyy-mm-dd-destination". However the search
    ''' is conducted by resurcavely looking and the contents of the parent directory
    ''' and looking for the MA_DIR_STRUCT_DATA_DIR (normally "2_Active_Data").
    ''' 
    ''' If the root of the MapAction directory structure cannot be found then an
    ''' ArgumentException is thrown.
    ''' </remarks>
    Private Function getMARootDir(ByVal strPathName As String) As DirectoryInfo
        Dim fInfo As FileInfo

        fInfo = New FileInfo(strPathName)

        Return getMARootDir(fInfo)
    End Function

    ''' <summary>
    ''' Attempts to find and return the root of the MapAction directory structure.
    ''' Typically this is in the form "yyyy-mm-dd-destination". However the search
    ''' is conducted by resurcavely looking and the contents of the parent directory
    ''' and looking for the MA_DIR_STRUCT_DATA_DIR (normally "2_Active_Data").
    ''' </summary>
    ''' <param name="dInfoArg">A DirectoryInfo object representing the starting 
    ''' directory for the search.</param>
    ''' <returns>A DirectoryInfo object pointing to the root of the MapAction
    ''' directory structure.</returns>
    ''' <remarks>
    ''' Attempts to find and return the root of the MapAction directory structure.
    ''' Typically this is in the form "yyyy-mm-dd-destination". However the search
    ''' is conducted by resurcavely looking and the contents of the parent directory
    ''' and looking for the MA_DIR_STRUCT_DATA_DIR (normally "2_Active_Data").
    ''' 
    ''' If the root of the MapAction directory structure cannot be found then an
    ''' ArgumentException is thrown.
    ''' </remarks>
    Private Function getMARootDir(ByRef dInfoArg As DirectoryInfo) As DirectoryInfo
        Dim dInfoRoot As DirectoryInfo

        If dInfoArg.Exists() AndAlso (dInfoArg.GetDirectories(MA_DIR_STRUCT_DATA_DIR).Length > 0) Then
            'We've found the root of the MA directory structure return this
            dInfoRoot = dInfoArg
        ElseIf IsDBNull(dInfoArg.Parent) Then
            'we've got to the system root directory without finding the top of the MA directory structure
            Throw New ArgumentException("Unable to find directory " & MA_DIR_STRUCT_DATA_DIR)
        Else
            dInfoRoot = getMARootDir(dInfoArg.Parent)
        End If

        Return dInfoRoot
    End Function

    ''' <summary>
    ''' Attempts to find and return the root of the MapAction directory structure.
    ''' Typically this is in the form "yyyy-mm-dd-destination". However the search
    ''' is conducted by resurcavely looking and the contents of the parent directory
    ''' and looking for the MA_DIR_STRUCT_DATA_DIR (normally "2_Active_Data").
    ''' </summary>
    ''' <param name="fInfoArg">A FileInfo object representing the starting 
    ''' location for the search.</param>
    ''' <returns>A DirectoryInfo object pointing to the MapAction Active Data 
    ''' directory.</returns>
    ''' <remarks>
    ''' Attempts to find and return the root of the MapAction directory structure.
    ''' Typically this is in the form "yyyy-mm-dd-destination". However the search
    ''' is conducted by resurcavely looking and the contents of the parent directory
    ''' and looking for the MA_DIR_STRUCT_DATA_DIR (normally "2_Active_Data").
    ''' 
    ''' If the root of the MapAction directory structure cannot be found then an
    ''' ArgumentException is thrown.
    ''' </remarks>
    Private Function getMARootDir(ByRef fInfoArg As FileInfo) As DirectoryInfo
        Dim dInfoRoot As DirectoryInfo

        If fInfoArg.Exists() And (Not (fInfoArg.Attributes And FileAttributes.Directory) = FileAttributes.Directory) Then
            dInfoRoot = getMARootDir(fInfoArg.Directory)
        ElseIf (fInfoArg.Attributes And FileAttributes.Directory) = FileAttributes.Directory Then
            Dim dInfo As DirectoryInfo
            dInfo = New DirectoryInfo(fInfoArg.FullName)
            dInfoRoot = getMARootDir(dInfo)
        Else
            Throw New ArgumentException("File not found " & fInfoArg.FullName)
        End If

        Return dInfoRoot
    End Function

    ''' <summary>
    ''' Attempts to find and return the MapAction Active Data directory within the 
    ''' MapAction standard data structure. The name of this directory is given by the
    ''' const MA_DIR_STRUCT_DATA_DIR (normally "2_Active_Data"). The search is
    ''' conducted by resurcavely search the parent directories.
    ''' </summary>
    ''' <param name="strPathName">A string representing the either a directory or a
    ''' file as the starting location for the search.</param>
    ''' <returns>A DirectoryInfo object pointing to the MapAction Active Data 
    ''' directory.</returns>
    ''' <remarks>
    ''' Attempts to find and return the MapAction Active Data directory within the 
    ''' MapAction standard data structure. The name of this directory is given by the
    ''' const MA_DIR_STRUCT_DATA_DIR (normally "2_Active_Data"). The search is
    ''' conducted by resurcavely search the parent directories.
    ''' 
    ''' If the root of the MapAction Active Data directory structure cannot be 
    ''' found then an ArgumentException is thrown.
    ''' </remarks>
    Friend Function getMAActiveDataDir(ByVal strPathName As String) As DirectoryInfo
        Return appendMAActiveDataDir(getMARootDir(strPathName))
    End Function

    ''' <summary>
    ''' Attempts to find and return the MapAction Active Data directory within the 
    ''' MapAction standard data structure. The name of this directory is given by the
    ''' const MA_DIR_STRUCT_DATA_DIR (normally "2_Active_Data"). The search is
    ''' conducted by resurcavely search the parent directories.
    ''' </summary>
    ''' <param name="dInfoArg">A DirectoryInfo object representing the starting 
    ''' directory for the search.</param>
    ''' <returns>A DirectoryInfo object pointing to the MapAction Active Data 
    ''' directory.</returns>
    ''' <remarks>
    ''' Attempts to find and return the MapAction Active Data directory within the 
    ''' MapAction standard data structure. The name of this directory is given by the
    ''' const MA_DIR_STRUCT_DATA_DIR (normally "2_Active_Data"). The search is
    ''' conducted by resurcavely search the parent directories.
    ''' 
    ''' If the root of the MapAction Active Data directory structure cannot be 
    ''' found then an ArgumentException is thrown.
    ''' </remarks>
    Friend Function getMAActiveDataDir(ByRef dInfoArg As DirectoryInfo) As DirectoryInfo
        Return appendMAActiveDataDir(getMARootDir(dInfoArg))
    End Function

    ''' <summary>
    ''' Attempts to find and return the MapAction Active Data directory within the 
    ''' MapAction standard data structure. The name of this directory is given by the
    ''' const MA_DIR_STRUCT_DATA_DIR (normally "2_Active_Data"). The search is
    ''' conducted by resurcavely search the parent directories.
    ''' </summary>
    ''' <param name="fInfoArg">A FileInfo object representing the starting 
    ''' location for the search.</param>
    ''' <returns>A DirectoryInfo object pointing to the MapAction Active Data 
    ''' directory.</returns>
    ''' <remarks>
    ''' Attempts to find and return the MapAction Active Data directory within the 
    ''' MapAction standard data structure. The name of this directory is given by the
    ''' const MA_DIR_STRUCT_DATA_DIR (normally "2_Active_Data"). The search is
    ''' conducted by resurcavely search the parent directories.
    ''' 
    ''' If the root of the MapAction Active Data directory structure cannot be 
    ''' found then an ArgumentException is thrown.
    ''' </remarks>
    Friend Function getMAActiveDataDir(ByRef fInfoArg As FileInfo) As DirectoryInfo
        Return appendMAActiveDataDir(getMARootDir(fInfoArg))
    End Function

    ''' <summary>
    ''' A private convinence function that attempts to locate and return a reference 
    ''' to the MapAction Active Data directory within the subdirectory of the argument.
    ''' The name of this directory is given by the
    ''' const MA_DIR_STRUCT_DATA_DIR (normally "2_Active_Data").
    ''' </summary>
    ''' <param name="dInfoRoot">A DirectoryInfo object pointing to the root of the MapAction
    ''' directory structure.</param>
    ''' <returns>A DirectoryInfo object that refers to a directory named 
    ''' MA_DIR_STRUCT_DATA_DIR and is an imediate subdirectory of the dInfoRoot argument.</returns>
    ''' <remarks></remarks>
    Private Function appendMAActiveDataDir(ByRef dInfoRoot As DirectoryInfo) As DirectoryInfo
        Dim returnDir As DirectoryInfo

        If dInfoRoot.GetDirectories(MA_DIR_STRUCT_DATA_DIR).Length = 1 Then
            returnDir = CType(dInfoRoot.GetDirectories(MA_DIR_STRUCT_DATA_DIR).GetValue(0), DirectoryInfo)
        Else
            Throw New ArgumentException("Unable to find ActiveData directory " & _
                                        MA_DIR_STRUCT_DATA_DIR & " in path " & dInfoRoot.FullName)
        End If

        Return returnDir
    End Function

    ''' <summary>
    ''' Searchs a directory and returns are list for all possible GDBs or GDB connection files with the directory
    ''' </summary>
    ''' <param name="dirArg"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getGDBsInDir(ByRef dirArg As DirectoryInfo) As List(Of FileInfo)
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

    ''' <summary>
    ''' Searches for an appropriate type of ESRI IWorkspace based on the nature of the fInfo
    ''' passed.
    ''' </summary>
    ''' <param name="strFullFilePath">A string pointing to an ESRI Data source.</param>
    ''' <returns>An ESRI.ArcGIS.Geodatabase.IWorkspace object refering to the same
    ''' location as the fInfo argument.</returns>
    ''' <remarks>
    ''' Searches for an appropriate type of ESRI IWorkspace based on the nature of the fInfo
    ''' passed.
    ''' 
    ''' If strFullFilePath is a file with extention = ".mdb", returns a AccessWorkspaceFactoryClass
    ''' If strFullFilePath is a file with extention = ".sde", ".ags" or ".gds", returns a SdeWorkspaceFactory
    ''' If strFullFilePath is a directory with extention = ".gdb", returns a FileGDBWorkspaceFactoryClass
    ''' If strFullFilePath is a directory with no extention, returns a ShapefileWorkspaceFactory
    ''' </remarks>
    Friend Function getESRIWorkspaceFromFile(ByVal strFullFilePath As String) As IWorkspace
        Dim fInfo As FileInfo

        fInfo = New FileInfo(strFullFilePath)

        Return getESRIWorkspaceFromFile(fInfo)
    End Function

    ''' <summary>
    ''' Searches for an appropriate type of ESRI IWorkspace based on the nature of the fInfo
    ''' passed.
    ''' </summary>
    ''' <param name="fInfo">A FileInfo object pointing to an ESRI Data source.</param>
    ''' <returns>An ESRI.ArcGIS.Geodatabase.IWorkspace object refering to the same
    ''' location as the fInfo argument.</returns>
    ''' <remarks>
    ''' Searches for an appropriate type of ESRI IWorkspace based on the nature of the fInfo
    ''' passed.
    ''' 
    ''' If fInfo is a file with extention = ".mdb", returns a AccessWorkspaceFactoryClass
    ''' If fInfo is a file with extention = ".sde", ".ags" or ".gds", returns a SdeWorkspaceFactory
    ''' If fInfo is a directory with extention = ".gdb", returns a FileGDBWorkspaceFactoryClass
    ''' If fInfo is a directory with no extention, returns a ShapefileWorkspaceFactory
    ''' </remarks>
    Friend Function getESRIWorkspaceFromFile(ByVal fInfo As FileInfo) As IWorkspace
        Dim wkspReturnRef As ESRI.ArcGIS.Geodatabase.IWorkspace = Nothing

        If Not fInfo.Exists() Then
            Throw New ArgumentException("GeoDatabase type not recgonised. The file " & _
                                        fInfo.FullName & " does not exist")
        Else
            If (Not (fInfo.Attributes And FileAttributes.Directory) = FileAttributes.Directory) Then
                Dim wkspFactory As IWorkspaceFactory

                Select Case fInfo.Extension
                    Case ".mdb"
                        wkspFactory = New AccessWorkspaceFactoryClass
                        'myGDBtype = GDB_TYPE_MDB
                    Case ".sde", ".ags", ".gds"
                        wkspFactory = New SdeWorkspaceFactory
                        'myGDBtype = GDB_TYPE_SDE_BY_CONFILE
                    Case Else
                        Throw New ArgumentException("GeoDatabase type not recgonised")
                End Select

                wkspReturnRef = wkspFactory.OpenFromFile(fInfo.FullName, 0)

            ElseIf (fInfo.Attributes And FileAttributes.Directory) = FileAttributes.Directory Then
                Dim dInfo As DirectoryInfo
                dInfo = New DirectoryInfo(fInfo.FullName)

                Dim wkspFactory2 As IWorkspaceFactory2

                If dInfo.FullName.EndsWith(".gdb") Then
                    wkspFactory2 = New FileGDBWorkspaceFactoryClass
                    'myGDBtype = GDB_TYPE_FILEGDB
                Else
                    wkspFactory2 = New ShapefileWorkspaceFactory
                End If

                wkspReturnRef = wkspFactory2.OpenFromFile(dInfo.FullName, 0)

            End If
        End If

        Return wkspReturnRef
    End Function

    ''' <summary>
    ''' Returns a list of ESRI.ArcGIS.Geodatabase.IDataset objects present within
    ''' an IWorkspace or IEnumDatasetName object.
    ''' </summary>
    ''' <param name="wrksp">A IWorkspace object</param>
    ''' <param name="blnRecuse">TRUE = The workspace should be recused if relevant,
    ''' FALSE = the workspace should not be recursed</param>
    ''' <returns>a list of ESRI.ArcGIS.Geodatabase.IDataset objects present within
    ''' an IWorkspace or IEnumDatasetName object.</returns>
    ''' <remarks>
    ''' Returns a list of ESRI.ArcGIS.Geodatabase.IDataset objects present within
    ''' an IWorkspace or IEnumDatasetName object.
    ''' 
    ''' To access the IDataset themselves use the method getESRIDataSetsFromWorkspace()
    ''' </remarks>
    Friend Function getESRIDataSetNamesFromWorkspace(ByRef wrksp As IWorkspace, ByVal blnRecuse As Boolean) As List(Of IDatasetName)
        Return getESRIDataSetNamesFromWorkspace(wrksp.DatasetNames(esriDatasetType.esriDTAny), blnRecuse)
    End Function

    ''' <summary>
    ''' Returns a list of ESRI.ArcGIS.Geodatabase.IDatasetName objects present within
    ''' an IWorkspace or IEnumDatasetName object.
    ''' </summary>
    ''' <param name="edn">A DatasetName emnumerator IEnumDatasetName</param>
    ''' <param name="blnRecuse">TRUE = The workspace should be recused if relevant,
    ''' FALSE = the workspace should not be recursed</param>
    ''' <returns>a list of ESRI.ArcGIS.Geodatabase.IDatasetName objects present within
    ''' an IWorkspace or IEnumDatasetName object.</returns>
    ''' <remarks>
    ''' Returns a list of ESRI.ArcGIS.Geodatabase.IDatasetName objects present within
    ''' an IWorkspace or IEnumDatasetName object.
    ''' 
    ''' To access the IDataset themselves use the method getESRIDataSetsFromWorkspace()
    ''' </remarks>
    Friend Function getESRIDataSetNamesFromWorkspace(ByRef edn As IEnumDatasetName, _
                                                      ByVal blnRecuse As Boolean) As List(Of IDatasetName)
        Dim lstDSNames As New List(Of IDatasetName)
        Dim dsName As IDatasetName

        dsName = edn.Next()

        While Not dsName Is Nothing
            If blnRecuse And (dsName.Type = esriDatasetType.esriDTContainer) Then
                lstDSNames.AddRange(getESRIDataSetNamesFromWorkspace(dsName.SubsetNames, blnRecuse))
            Else
                lstDSNames.Add(dsName)
            End If
            dsName = edn.Next()
        End While

        Return lstDSNames
    End Function


    ''' <summary>
    ''' A convinence function for exacting a list of names as strings froma list of IDatasetName objects.
    ''' </summary>
    ''' <param name="dsList">A list of IDatasetName objects</param>
    ''' <returns>A List(Of Strings), with the names of each of the dsList objects.</returns>
    ''' <remarks>
    ''' A convinence function for exacting a list of names as strings froma list of IDatasetName objects.
    ''' </remarks>
    Friend Function getNamesStrFromESRIDataSetName(ByRef dsList As List(Of IDatasetName)) As List(Of String)
        Dim strList As New List(Of String)

        For Each ds In dsList
            strList.Add(ds.Name)
        Next

        Return strList
    End Function


    ''' <summary>
    ''' Returns a list of ESRI.ArcGIS.Geodatabase.IDataset objects present within
    ''' an IWorkspace or IEnumDataset object.
    ''' </summary>
    ''' <param name="wrksp">An IWorkspace</param>
    ''' <param name="blnRecuse">TRUE = The workspace should be recused if relevant,
    ''' FALSE = the workspace should not be recursed</param>
    ''' <returns>a list of ESRI.ArcGIS.Geodatabase.IDataset objects present within
    ''' an IWorkspace or IEnumDatasetName object.</returns>
    ''' <remarks>
    ''' Returns a list of ESRI.ArcGIS.Geodatabase.IDataset objects present within
    ''' an IWorkspace or IEnumDataset object.
    ''' 
    ''' To access the just the IDatasetNames use the method 
    ''' getESRIDataSetNamesFromWorkspace()
    ''' </remarks>
    Friend Function getESRIDataSetsFromWorkspace(ByRef wrksp As IWorkspace, _
                                                  ByVal blnRecuse As Boolean) As List(Of IDataset)
        Return getESRIDataSetsFromWorkspace(wrksp.Datasets(esriDatasetType.esriDTAny), blnRecuse)
    End Function


    ''' <summary>
    ''' Returns a list of ESRI.ArcGIS.Geodatabase.IDataset objects present within
    ''' an IWorkspace or IEnumDataset object.
    ''' </summary>
    ''' <param name="eds">A Dataset emnumerator IEnumDataset</param>
    ''' <param name="blnRecuse">TRUE = The workspace should be recused if relevant,
    ''' FALSE = the workspace should not be recursed</param>
    ''' <returns>a list of ESRI.ArcGIS.Geodatabase.IDataset objects present within
    ''' an IWorkspace or IEnumDatasetName object.</returns>
    ''' <remarks>
    ''' Returns a list of ESRI.ArcGIS.Geodatabase.IDataset objects present within
    ''' an IWorkspace or IEnumDataset object.
    ''' 
    ''' To access the just the IDatasetNames use the method 
    ''' getESRIDataSetNamesFromWorkspace()
    ''' </remarks>
    Friend Function getESRIDataSetsFromWorkspace(ByRef eds As IEnumDataset, _
                                                  ByVal blnRecuse As Boolean) As List(Of IDataset)
        Dim lstDSet As New List(Of IDataset)
        Dim ds As IDataset

        ds = eds.Next()

        While Not ds Is Nothing
            If blnRecuse And (ds.Type = esriDatasetType.esriDTContainer) Then
                lstDSet.AddRange(getESRIDataSetsFromWorkspace(ds.Subsets, blnRecuse))
            Else
                lstDSet.Add(ds)
            End If
            ds = eds.Next()
        End While

        Return lstDSet
    End Function

End Module
