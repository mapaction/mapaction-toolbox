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

Imports System.IO
Imports System.Collections.Generic
Imports ESRI.ArcGIS.Geodatabase


''' <summary>
''' Provides a specfic implenmentation of the IDataListConnection, based on reading the list
''' from ordinary operating system files contained in a directory.
''' </summary>
''' <remarks>
''' Provides a specfic implenmentation of the IDataListConnection, based on reading the list
''' from ordinary operating system files contained in a directory. Specific logical GIS files
''' (eg shapefiles) can be filtered out to just leave the files which ArcGIS isn't interested
''' in.
'''
''' There is no public constructor for this class. New instances should be generated using 
''' the XXXX factory class.
'''
''' Implenmentation Note: Ideally this class would not have any dependencies on ESRI 
''' ArcEngine. However in lue of being able to programmatically detect depenancy on either
''' ArcEngine or MapWindow, the handling of specific logical GIS files is passed handled
''' using ESRI.ArcGIS.Geodatabase.IWorkspace.
'''  </remarks>
Public Class DataListFileSystemDirectory
    Inherits AbstractDataListConnection

    Private m_DirInfo As DirectoryInfo
    Private m_ESRIWorkspace As IWorkspace


    ''' <summary>
    ''' Constructs a new list based on the directory identified by dInfo.
    ''' </summary>
    ''' <param name="dInfoArg">A DirectoryInfo object representing the 
    ''' directory to be listed.
    ''' </param>
    ''' <remarks>
    ''' Constructs a new list based on the directory identified by dInfo.
    ''' 
    ''' This constuctor should only be call from within the relevant factory class.
    ''' </remarks>
    Protected Friend Sub New(ByRef dInfoArg As DirectoryInfo)
        If Not dInfoArg.Exists() Then
            Throw New ArgumentException("Non-existent directory: " & dInfoArg.FullName)
        Else
            m_DirInfo = dInfoArg
            m_ESRIWorkspace = getESRIWorkspaceFromFile(New FileInfo(dInfoArg.FullName))
        End If
    End Sub


    ''' <summary>
    ''' Constructs a new list based on the directory identified by strPathName.
    ''' </summary>
    ''' <param name="strPathName">A string representing full path of the 
    ''' directory to be listed.
    ''' </param>
    ''' <remarks>
    ''' Constructs a new list based on the directory identified by strPathName.
    ''' 
    ''' This constuctor should only be call from within the relevant factory class.
    ''' </remarks>
    Protected Friend Sub New(ByVal strPathName As String)
        MyClass.new(New DirectoryInfo(strPathName))
    End Sub


    ''' <summary>
    ''' A convenience function to test whether or not the named layer is present in this directory.
    ''' </summary>
    ''' <param name="strLayerName">The name of the layer, whose presence is being tested for.</param>
    ''' <returns>TRUE if strLayerName is present in this directory, FALSE otherwise</returns>
    ''' <remarks>
    ''' A convenience function to test whether or not the named layer is present in this directory.
    ''' </remarks>
    Public Overrides Function doesLayerExist(ByVal strLayerName As String) As Boolean
        'if there is exactly one file which matches strLayerName, then the layer exists.
        Return (m_DirInfo.GetFiles(strLayerName).Length = 1)
    End Function


    ''' <summary>
    ''' This method will attempt to physically locate a suitable set of DataNames Clause Lookup Tables, by
    ''' searching this directory and it's parent directories.
    ''' </summary>
    ''' <returns>An IDataNameClauseLooup object representing automatically located DataNames Clause Tables</returns>
    ''' <remarks>
    ''' This method will attempt to physically locate a suitable set of DataNames Clause Lookup Tables, by
    ''' searching this directory and it's parent directories.
    ''' 
    ''' If either this directory or one of it's parent directories contains a subdirectory "2_Active_Data",
    ''' then that Active Data Directory is searched for GDB or Access DBs containing DataNames Clause Lookup Tables.
    ''' 
    ''' If searching for Access DBs then the file which matches the pattern "data_naming_conventions_v*", which
    ''' has the highest version number will be used.
    ''' 
    ''' For more details on how this should be implenmented please see:
    ''' http://code.google.com/p/mapaction-toolbox/wiki/SearchForDefaultDataNameClauseLookupTables
    ''' </remarks>
    Public Overrides Function getDefaultDataNameClauseLookup() As IDataNameClauseLookup
        Dim dncFact As DataNameClauseLookupFactory
        Dim dncl As IDataNameClauseLookup

        dncFact = DataNameClauseLookupFactory.getFactory()

        Try
            dncl = DataNameClauseLookupFactory.createDataNameClauseLookup(getMAActiveDataDir(m_DirInfo.FullName), True)
        Catch ex As Exception
            dncl = DataNameClauseLookupFactory.createDataNameClauseLookup(m_DirInfo, True)
        End Try

        Return dncl
    End Function


    ''' <summary>
    ''' Returns a string describing the connection to the directory. This is 
    ''' gernerally just the file path.
    ''' </summary>
    ''' <returns>A string describing the connection to the directory.</returns>
    ''' <remarks>
    ''' Returns a string describing the connection to the directory. This is 
    ''' gernerally just the file path.
    ''' </remarks>
    Public Overrides Function getDetails() As String
        Return m_DirInfo.FullName
    End Function

    ''' <summary>
    ''' Returns the operating system file path to the directory.
    ''' </summary>
    ''' <returns>A FileInfo object representing the operating system file path
    ''' to the directory.</returns>
    ''' <remarks>
    ''' Returns the operating system file path to the directory.
    ''' </remarks>
    Public Overrides Function getpath() As System.IO.FileInfo
        Return New FileInfo(m_DirInfo.FullName)
    End Function


    ''' <summary>
    ''' Returns TRUE to indicate that an directory does have a heirachical structure.
    ''' </summary>
    ''' <returns>
    ''' TRUE to indicate that an directory does have a heirachical structure.
    ''' </returns>
    ''' <remarks>
    ''' Returns TRUE to indicate that an directory does have a heirachical structure.
    ''' </remarks>
    Public Overrides Function isheirachical() As Boolean
        Return True
    End Function

    ''' <summary>
    ''' Returns an dnListType enumeration which represents the underlying
    ''' physical type of the connection (dnListType.DIR).
    ''' </summary>
    ''' <returns>dnListType.DIR</returns>
    ''' <remarks>
    ''' Returns an dnListType enumeration which represents the underlying
    ''' physical type of the connection (dnListType.DIR).
    ''' </remarks>
    Public Overrides Function getDataListConnectionType() As dnListType
        Return dnListType.DIR
    End Function


    ''' <summary>
    ''' Returns a String which describes the type of connection.
    ''' </summary>
    ''' <returns>A String which describes the type of connection, in this case
    ''' equal to DATALIST_TYPE_DIR.
    ''' </returns>
    ''' <remarks>
    ''' Returns a String which describes the type of connection.
    ''' </remarks>
    Public Overrides Function getDataListConnectionTypeDesc() As String
        Return DATALIST_TYPE_DIR
    End Function


    ''' <summary>
    ''' Returns a List of IDataName objects representing the names of all of the layers
    ''' in this directory. If Recuse is set to TRUE, then layer from all of the subdirectories
    ''' are included.
    ''' </summary>
    ''' <param name="dnclUserSelected">
    ''' IDataNameClauseLookup object. This is passed to the constructor of
    ''' the IDataName objects.
    ''' </param>
    ''' <returns>A List of IDataName objects representing the names of all of the layers defined
    ''' by this DataListConnection.</returns>
    ''' <remarks>
    ''' Returns a List of IDataName objects representing the names of all of the layers
    ''' in this directory. If Recuse is set to TRUE, then layer from all of the subdirectories
    ''' are included.
    ''' 
    ''' The IDataNameClauseLookup object is explictly assigned and this is passed to the 
    ''' constructor of the IDataName objects. If the default DataNameClauseLookup object 
    ''' is sufficent then the alternative shorthand function can be used.
    ''' </remarks>
    Public Overrides Function getLayerDataNamesList(ByRef dnclUserSelected As IDataNameClauseLookup) As List(Of IDataName)
        Dim lstDN As New List(Of IDataName)
        Dim blnAllowRenames As Boolean

        blnAllowRenames = Not ((m_DirInfo.Attributes And FileAttributes.ReadOnly) = FileAttributes.ReadOnly)

        'open the GIS related files using an Arc Workspace
        'Don't recuse here becuase this is taken care of by the directory recursing below
        For Each ds In getESRIDataSetsFromWorkspace(m_ESRIWorkspace, False)
            lstDN.Add(New DataNameESRIFeatureClass(ds, dnclUserSelected, blnAllowRenames))
        Next

        'open the non-GIS related files using regular OS FileInfo Obj
        For Each fInfo In filterFilesForSpecialGISData(False)
            lstDN.Add(New DataNameNormalFile(fInfo, dnclUserSelected, blnAllowRenames))
        Next

        'Now recurse if necessary
        If getRecuse() Then
            Dim dataList As IDataListConnection

            For Each dInfo In filterDirsForSpecialGISData()
                dataList = New DataListFileSystemDirectory(dInfo)
                dataList.setRecuse(getRecuse())
                lstDN.AddRange(dataList.getLayerDataNamesList(dnclUserSelected))
            Next
        End If

        Return lstDN
    End Function


    ''' <summary>
    ''' Returns a List of strings representing the names of all of the layers defined
    ''' by this Directory. If Recuse is set to TRUE, then layer from all of the 
    ''' subdirectories are included.
    ''' </summary>
    ''' <returns>
    ''' A List of strings representing the names of all of the layers defined
    ''' by this Directory.
    ''' </returns>
    ''' <remarks>
    ''' Returns a List of strings representing the names of all of the layers defined
    ''' by this Directory. If Recuse is set to TRUE, then layer from all of the 
    ''' subdirectories are included.
    ''' </remarks>
    Public Overrides Function getLayerNamesStrings() As List(Of String)
        Dim lstStrNames As New List(Of String)

        For Each curFileInfo In filterFilesForSpecialGISData()
            lstStrNames.Add(curFileInfo.Name)
        Next

        'Now recurse if necessary
        If getRecuse() Then
            Dim dataList As IDataListConnection

            For Each dInfo In filterDirsForSpecialGISData()
                dataList = New DataListFileSystemDirectory(dInfo)
                dataList.setRecuse(getRecuse())
                lstStrNames.AddRange(dataList.getLayerNamesStrings())
            Next
        End If

        Return lstStrNames
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
    ''' <param name="blnIncludeSpecialBaseFiles">If this is true then the basefiles of special GIS file eg. "example.shp" are 
    ''' included in the returned list. If False, all special GIS files including the basefiles are excluded</param>
    ''' <returns>A list of FileInfo objects representing those files which are not part of a logical GIS file.</returns>
    ''' <remarks>
    ''' Returns a list of FileInfo Objects, representing the files (not subdirectories) in the Directory, filtering out
    ''' all of the OS files which are part of logical GIS files. 
    ''' 
    ''' If blnIncludeSpecialBaseFiles is true then the basefiles of special GIS file eg. "example.shp" are 
    ''' included in the returned list. If False, all special GIS files including the basefiles are excluded
    ''' 
    ''' If an DBF or XML file is included which is not part of a logical GIS file, then these are included in the 
    ''' returned List.
    ''' </remarks>
    Private Function filterFilesForSpecialGISData(ByVal blnIncludeSpecialBaseFiles As Boolean) As List(Of FileInfo)
        Dim aryfInfoAll() As FileInfo
        Dim lstSpecialFileNames As New List(Of String)
        Dim lstfInfoFiltered As New List(Of FileInfo)

        aryfInfoAll = m_DirInfo.GetFiles()

        'First build a list of the "front" files for multiple OS-File storage
        For Each curFileInfo In aryfInfoAll
            'System.Console.WriteLine("curFileInfo.Name " & curFileInfo.Name & "   curFileInfo.FullName " & curFileInfo.FullName)
            'System.Console.WriteLine("curFileInfo.Name " & curFileInfo.Name.Remove(curFileInfo.Name.LastIndexOf(curFileInfo.Extension)) & "   curFileInfo.FullName " & curFileInfo.Extension)

            Select Case curFileInfo.Extension
                Case ".shp", ".bmp", ".gif", ".img", ".jpg", "jp2", ".png", ".tif", ".asc"
                    'Only add if not part of a shapefile
                    lstSpecialFileNames.Add(curFileInfo.Name.Remove(curFileInfo.Name.LastIndexOf(curFileInfo.Extension)))
                    If blnIncludeSpecialBaseFiles Then
                        lstfInfoFiltered.Add(curFileInfo)
                    End If
            End Select
        Next

        'Now build a list of all of the files excluding those where the main part of the there name (e.g. before the 
        'extension) matches one which is in the specialFileList
        For Each curFileInfo In aryfInfoAll
            Dim found As Boolean = False

            For Each fileName In allBaseFileNameOptions(curFileInfo.Name)
                If lstSpecialFileNames.Contains(fileName) Then
                    found = True
                End If
            Next

            If Not found Then
                lstfInfoFiltered.Add(curFileInfo)
            End If

        Next

        Return lstfInfoFiltered
    End Function

    ''' <summary>
    ''' Helper function for filterFilesForSpecialGISData(). Returns all of the possible base 
    ''' filename without the extension, in order to compare member files of a logical GIS 
    ''' file.
    ''' </summary>
    ''' <param name="strFileName">The filename as a string</param>
    ''' <returns>A list of possible base filename without extensions</returns>
    ''' <remarks>
    ''' Helper function for filterFilesForSpecialGISData(). Returns all of the possible base 
    ''' filename without the extension, in order to compare member files of a logical GIS 
    ''' file. Takes account of the multiple extensions that shapefile member files sometimes
    ''' have. eg "example.one.shp.xml" will return:
    ''' 
    ''' "example.one.shp" 
    ''' "example.one" 
    ''' "example" 
    ''' </remarks>
    Private Function allBaseFileNameOptions(ByVal strFileName As String) As List(Of String)
        Dim lstStrAllOptions As New List(Of String)

        Do While strFileName <> String.Empty
            If strFileName.LastIndexOf(".") >= 0 Then
                strFileName = Left(strFileName, strFileName.LastIndexOf("."))
                lstStrAllOptions.Add(strFileName)
            Else
                strFileName = String.Empty
            End If
        Loop

        Return lstStrAllOptions
    End Function


    ''' <summary>
    ''' A private helper function. Returns a list of DirectoryInfo representing 
    ''' subdirectories excluding those which are actually filebased GDBs.
    ''' </summary>
    ''' <returns>A list of DirectoryInfo representing subdirectories excluding
    ''' those which are actually filebased GDBs.</returns>
    ''' <remarks>
    ''' A private helper function. Returns a list of DirectoryInfo representing 
    ''' subdirectories excluding those which are actually filebased GDBs.
    ''' </remarks>
    Private Function filterDirsForSpecialGISData() As List(Of DirectoryInfo)
        Dim allDirInfos() As DirectoryInfo
        Dim filteredDirInfos As New List(Of DirectoryInfo)

        allDirInfos = m_DirInfo.GetDirectories()

        For Each subDir In allDirInfos
            If Not subDir.FullName.EndsWith(".gdb") Then
                filteredDirInfos.Add(subDir)
            End If
        Next

        Return filteredDirInfos
    End Function

End Class
