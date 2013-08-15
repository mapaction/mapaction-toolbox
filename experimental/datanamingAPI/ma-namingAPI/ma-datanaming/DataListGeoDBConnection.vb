Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.DataSourcesGDB
Imports System.IO

'todo should getESRIWorkspaceFromFile be moved to this class?
'todo should getNamesStrFromESRIDataSetName be moved to this class?
'todo should getESRIDataSetNamesFromWorkspace be moved to this class?
''' <summary>
''' Provides a specfic implenmentation of the IDataListConnection, based on reading 
''' the list from an ESRI Geodatabase.
''' </summary>
''' <remarks>
''' Provides a specfic implenmentation of the IDataListConnection, based on reading 
''' the list from an ESRI Geodatabase.
'''
''' There is no public constructor for this class. New instances should be generated using 
''' the XXXX factory class.
'''  </remarks>
Public Class DataListGeoDBConnection
    Inherits AbstractGeoDataListConnection

    'Private Const m_GDB_TYPE_MDB As Integer = CInt(2 ^ 1)
    'Private Const m_GDB_TYPE_FILEGDB As Integer = CInt(2 ^ 2)
    'Private Const m_GDB_TYPE_SDE_BY_PROPS As Integer = CInt(2 ^ 3)
    'Private Const m_GDB_TYPE_SDE_BY_CONFILE As Integer = CInt(2 ^ 4)
    'Private Const m_GDB_TYPE_SQL_EXPRESS As Integer = CInt(2 ^ 5)

    Private m_wkspDNLT As ESRI.ArcGIS.Geodatabase.IWorkspace = Nothing

    'Private m_intGDBtype As Integer


    'todo LOW: implenment this.
    '''' <summary>
    '''' Constructs a new list based on a GDB described in the argument.
    '''' </summary>
    '''' <param name="strAryArgs">A string array assumed to describes 
    '''' connection parameters for an SDE GDB.
    '''' </param>
    '''' <remarks>
    '''' Constructs a new list based on a GDB described in the argument.
    '''' 
    '''' This constuctor should only be call from within the relevant factory class.
    '''' </remarks>
    'Sub New(ByVal strAryArgs() As String)
    '    'todo LOW: implenment this.
    '    m_wkspDNLT = nothing
    '
    '    If m_wkspDNLT Is Nothing Then
    '        Throw New ArgumentException("Unable to open DataListGeoDBConnection")
    '    End If
    'End Sub


    ''' <summary>
    ''' Constructs a new list based on a GDB identified by strFullFilePath.
    ''' </summary>
    ''' <param name="strFullFilePath">A String representing the full path name of 
    ''' a geodatabase. If strFullFilePath:
    ''' - points to a ".mdb" file, opens the file as a Personal GDB
    ''' - points to a ".gdb" directory, opens the directory as a filebased GDB
    ''' - points to a ".sde", ".ags" or ".gds" file, opens the SDE GDB described
    ''' </param>
    ''' <remarks>
    ''' Constructs a new list based on a GDB identified by strFullFilePath.
    ''' 
    ''' This constuctor should only be call from within the relevant factory class.
    ''' </remarks>
    Sub New(ByVal strFullFilePath As String)
        Try
            'Use a try block to catch null, nothing or invalid strFullFilePath
            m_wkspDNLT = getESRIWorkspaceFromFile(strFullFilePath)
        Finally
            If m_wkspDNLT Is Nothing Then
                Throw New ArgumentException("Unable to open DataListGeoDBConnection")
            End If
        End Try

    End Sub

    ''' <summary>
    ''' Constructs a new list based on a GDB identified by the arguments.
    ''' </summary>
    ''' <param name="strFileName">A String representing the file name of 
    ''' a geodatabase. If strFullFilePath:
    ''' - points to a ".mdb" file, opens the file as a Personal GDB
    ''' - points to a ".gdb" directory, opens the directory as a filebased GDB
    ''' - points to a ".sde", ".ags" or ".gds" file, opens the SDE GDB described
    ''' </param>
    '''  <param name="strDirectory">A String representing the directory
    ''' containing strFileName.</param>
    ''' <remarks>
    ''' Constructs a new list based on a GDB identified by the arguments.
    ''' 
    ''' This constuctor should only be call from within the relevant factory class.
    ''' </remarks>
    Sub New(ByVal strFileName As String, ByVal strDirectory As String)
        
        If strDirectory.EndsWith(Path.DirectorySeparatorChar) Then
            m_wkspDNLT = getESRIWorkspaceFromFile(strDirectory & strFileName)
        Else
            m_wkspDNLT = getESRIWorkspaceFromFile(strDirectory & Path.DirectorySeparatorChar & strFileName)
        End If

        If m_wkspDNLT Is Nothing Then
            Throw New ArgumentException("Unable to open DataListGeoDBConnection")
        End If
    End Sub

    ''' <summary>
    ''' Constructs a new list based on a GDB identified by fInfo.
    ''' </summary>
    ''' <param name="fInfo">A FileInfo object pointing a geodatabase. If fInfo:
    ''' - points to a ".mdb" file, opens the file as a Personal GDB
    ''' - points to a ".gdb" directory, opens the directory as a filebased GDB
    ''' - points to a ".sde", ".ags" or ".gds" file, opens the SDE GDB described
    ''' </param>
    ''' <remarks>
    ''' Constructs a new list based on a GDB identified by fInfo.
    ''' 
    ''' This constuctor should only be call from within the relevant factory class.
    ''' </remarks>
    Sub New(ByRef fInfo As FileInfo)
        m_wkspDNLT = getESRIWorkspaceFromFile(fInfo)

        If m_wkspDNLT Is Nothing Then
            Throw New ArgumentException("Unable to open DataListGeoDBConnection")
        End If
    End Sub

  
    ''' <summary>
    ''' Returns an dnListType enumeration which represents the underlying
    ''' physical type of the connection (dnListType.GDB).
    ''' </summary>
    ''' <returns>dnListType.GDB</returns>
    ''' <remarks>
    ''' Returns an dnListType enumeration which represents the underlying
    ''' physical type of the connection.
    ''' </remarks>
    Public Overrides Function getGeoDataListConnectionType() As Integer
        Return dnListType.GDB
    End Function


    ''' <summary>
    ''' Returns a String which describes the type of connection.
    ''' </summary>
    ''' <returns>A String which describes the type of connection, in this case
    ''' equal to DATALIST_TYPE_GDB.
    ''' </returns>
    ''' <remarks>
    ''' Returns a String which describes the type of connection.
    ''' </remarks>
    Public Overrides Function getGeoDataListConnectionTypeDesc() As String
        'todo LOW: move this to DataNamingConstants
        Return DATALIST_TYPE_GDB
    End Function

    ''''''''''''''''''''
    ' CHECKED TO HERE
    ''''''''''''''''''''

    'todo MEDIUM check if this method need to explictly take recursion into account
    ''' <summary>
    ''' A convenience function to test whether or not the named layer is present in this GDB.
    ''' </summary>
    ''' <param name="strLayerName">The name of the layer, whose presense is being tested for.</param>
    ''' <returns>TRUE if strLayerName is present in this GDB, FALSE otherwise</returns>
    ''' <remarks>
    ''' A convenience function to test whether or not the named layer is present in this GDB.
    ''' </remarks>
    Public Overrides Function doesLayerExist(ByVal strLayerName As String) As Boolean
        Dim edn As IEnumDatasetName
        Dim dsnCurrent As IDatasetName
        Dim blnResult As Boolean = False

        edn = m_wkspDNLT.DatasetNames(esriDatasetType.esriDTAny)

        dsnCurrent = edn.Next()

        Do While (Not dsnCurrent Is Nothing) And (Not blnResult)
            If dsnCurrent.ToString() = strLayerName Then
                blnResult = True
            End If
        Loop

        Return blnResult
    End Function


    ''' <summary>
    ''' Returns a List of strings representing the names of all of the layers defined
    ''' by this GDB.
    ''' </summary>
    ''' <returns>A list of strings representing the names of all of the layers 
    ''' within the GDB.
    ''' </returns>
    ''' <remarks>
    ''' Returns a List of strings representing the names of all of the layers defined
    ''' by this MXD.
    ''' </remarks>
    Public Overrides Function getLayerNamesStrings() As List(Of String)
        Return getNamesStrFromESRIDataSetName(getESRIDataSetNamesFromWorkspace(m_wkspDNLT, getRecuse()))
    End Function


    ''' <summary>
    ''' Returns a string describing the connection to the Geodatabase. This is not
    ''' necessarily just the file path.
    ''' </summary>
    ''' <returns>A string describing the connection to the Geodatabase.</returns>
    ''' <remarks>
    ''' Returns a string describing the connection to the Geodatabase. This is not
    ''' necessarily just the file path.
    ''' </remarks>
    Public Overrides Function getDetails() As String
        Return m_wkspDNLT.ConnectionProperties.ToString()
    End Function


    'todo MEDIUM check teh use fo connection file paths in this method.
    ''' <summary>
    ''' This method will attempt to physically locate a suitable set of DataNames Clause Lookup Tables, initially
    ''' by checking the contents of this GDB, and if that fails by searching the directory containing the GDB or
    ''' connection file.
    ''' </summary>
    ''' <returns>An IDataNameClauseLooup object representing automatically located DataNames Clause Tables</returns>
    ''' <remarks>
    ''' This method will attempt to physically locate a suitable set of DataNames Clause Lookup Tables, initially
    ''' by checking the contents of this GDB, and if that fails by searching the directory containing the GDB or
    ''' connection file.
    ''' 
    ''' Initially the GDB referenced in the DataListGeoDBConnection object is check to see if it contains the
    ''' relevant tables.
    ''' 
    ''' When searching by directory, the method attempts to location a parent directories containing 
    ''' a subdirectory "2_Active_Data", if that is found then thatActive Data Directory is searched for GDB or
    ''' Access DBs containing DataNames Clause Lookup Tables.
    ''' 
    ''' If the Data Names Clause Tables cannot be physically located becuase either they do not exist or there
    ''' is not a unquine location, then a exception is raised.
    ''' 
    ''' For more details on how this is implenmented please see:
    ''' http://code.google.com/p/mapaction-toolbox/wiki/SearchForDefaultDataNameClauseLookupTables
    ''' </remarks>
    Public Overrides Function getDefaultDataNameClauseLookup() As IDataNameClauseLookup
        Dim dnclDefault As IDataNameClauseLookup

        'System.Console.WriteLine("starting DataListGeoDBConnection.getDefaultDataNameClauseLookup()")
        Try
            'System.Console.WriteLine("Try 1")
            dnclDefault = New GDBDataNameClauseLookup(m_wkspDNLT)
        Catch ex1 As Exception
            'System.Console.WriteLine(ex1.ToString)
            Try
                'System.Console.WriteLine("Try 2")

                If m_wkspDNLT.IsDirectory Then
                    dnclDefault = New DataListFileSystemDirectory(m_wkspDNLT.PathName).getDefaultDataNameClauseLookup()
                Else
                    dnclDefault = New DataListFileSystemDirectory(New FileInfo(m_wkspDNLT.PathName).DirectoryName) _
                                            .getDefaultDataNameClauseLookup()
                End If

            Catch ex2 As Exception
                System.Console.WriteLine(ex2.ToString)
                Throw New LookupTableException(dnLookupTableError.default_tbls_not_found, m_wkspDNLT.PathName)
            End Try
        End Try

        Return dnclDefault
    End Function


    ''' <summary>
    ''' Returns a List of IDataName objects representing the Data names of all of the unique layers 
    ''' defined by this MXD. Layers which occur multiple time in the MXD (either within the 
    ''' same map/data frame or within different maps/data frames) are filtered out and only 
    ''' occur once in the returned list.
    ''' </summary>
    ''' <returns>
    ''' List of IDataName objects representing the names of all of the layers defined
    ''' by this MXD.
    ''' </returns>
    ''' <remarks>
    ''' Returns a List of IDataName objects representing the Data names of all of the unique layers 
    ''' defined by this MXD. Layers which occur multiple time in the MXD (either within the 
    ''' same map/data frame or within different maps/data frames) are filtered out and only 
    ''' occur once in the returned list.
    ''' </remarks>
    Public Overrides Function getLayerDataNamesList(ByRef dncl As IDataNameClauseLookup) As List(Of IDataName)
        Dim lstDSet As List(Of IDataset)
        Dim lstDName As New List(Of IDataName)

        lstDSet = getESRIDataSetsFromWorkspace(m_wkspDNLT, getRecuse())

        For Each ds In lstDSet
            lstDName.Add(New DataNameESRIFeatureClass(ds, dncl, True))
        Next

        Return lstDName
    End Function

End Class
