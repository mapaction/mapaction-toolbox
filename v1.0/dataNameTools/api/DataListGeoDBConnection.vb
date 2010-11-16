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
Imports ADODB

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
    Inherits AbstractDataListConnection

    Private m_wkspDNLT As IWorkspace = Nothing
    Private m_fInfoPath As FileInfo

    'todo LOW: implenment constructor for DataListGeoDBConnection based on connection parameters for SDE.
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
        Dim lstWrkSp As List(Of IWorkspace) = Nothing

        Try
            'Use a try block to catch null, nothing or invalid strFullFilePath
            lstWrkSp = getESRIWorkspacesFromFile(strFullFilePath)
        Finally
            If lstWrkSp IsNot Nothing AndAlso lstWrkSp.Count = 1 Then
                m_wkspDNLT = lstWrkSp.Item(0)
            Else
                Throw New ArgumentException("Unable to open DataListGeoDBConnection")
            End If

            m_fInfoPath = New FileInfo(strFullFilePath)
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
        Dim strFullFilePath As String = Nothing
        Dim lstWrkSp As List(Of IWorkspace) = Nothing

        Try
            'Use a try block to catch null, nothing or invalid strFullFilePath
            If strDirectory.EndsWith(Path.DirectorySeparatorChar) Then
                strFullFilePath = strDirectory & strFileName
            Else
                strFullFilePath = strDirectory & Path.DirectorySeparatorChar & strFileName
            End If

            lstWrkSp = getESRIWorkspacesFromFile(strFullFilePath)
        Finally
            If lstWrkSp IsNot Nothing AndAlso lstWrkSp.Count = 1 Then
                m_wkspDNLT = lstWrkSp.Item(0)
            Else
                Throw New ArgumentException("Unable to open DataListGeoDBConnection")
            End If

            m_fInfoPath = New FileInfo(strFullFilePath)
        End Try

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
        Dim lstWrkSp As List(Of IWorkspace) = Nothing

        Try
            lstWrkSp = getESRIWorkspacesFromFile(fInfo)
        Finally
            If lstWrkSp IsNot Nothing AndAlso lstWrkSp.Count = 1 Then
                m_wkspDNLT = lstWrkSp.Item(0)
            Else
                Throw New ArgumentException("Unable to open DataListGeoDBConnection")
            End If

            m_fInfoPath = fInfo
        End Try
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
    Public Overrides Function getDataListConnectionType() As dnListType
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
    Public Overrides Function getDataListConnectionTypeDesc() As String
        Return DATALIST_TYPE_GDB
    End Function


    'todo MEDIUM: check if this method need to explictly take recursion into account
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


    ''' <summary>
    ''' Returns the operating system file path to the GDB or the relevant 
    ''' connection file.
    ''' </summary>
    ''' <returns>A FileInfo object representing the operating system file path
    ''' to the GDB or the relevant connection file..</returns>
    ''' <remarks>
    ''' Returns the operating system file path to the GDB or the relevant 
    ''' connection file.
    ''' </remarks>
    Public Overrides Function getpath() As System.IO.FileInfo
        Return m_fInfoPath
    End Function

    Public Overrides Function isheirachical() As Boolean
        Return True
    End Function

    'todo MEDIUM: check the use of connection file paths in this method.
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
            dnclDefault = New GDBDataNameClauseLookup(m_wkspDNLT, ConnectModeEnum.adModeShareDenyWrite)
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
                Throw New LookupTableException(dnLookupTableError.DefaultTablesNotFound, m_wkspDNLT.PathName)
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
    Public Overrides Function getLayerDataNamesList(ByRef dnclUserSelected As IDataNameClauseLookup) As List(Of IDataName)
        Dim lstDSet As List(Of IDataset)
        Dim lstDName As New List(Of IDataName)

        lstDSet = getESRIDataSetsFromWorkspace(m_wkspDNLT, getRecuse())

        For Each ds In lstDSet
            lstDName.Add(New DataNameESRIFeatureClass(ds, dnclUserSelected, True))
        Next

        Return lstDName
    End Function

    '''' <summary>
    '''' Returns a list of ESRI.ArcGIS.Geodatabase.IDataset objects present within
    '''' an IWorkspace or IEnumDatasetName object.
    '''' </summary>
    '''' <param name="wrksp">A IWorkspace object</param>
    '''' <param name="blnRecuse">TRUE = The workspace should be recused if relevant,
    '''' FALSE = the workspace should not be recursed</param>
    '''' <returns>a list of ESRI.ArcGIS.Geodatabase.IDataset objects present within
    '''' an IWorkspace or IEnumDatasetName object.</returns>
    '''' <remarks>
    '''' Returns a list of ESRI.ArcGIS.Geodatabase.IDataset objects present within
    '''' an IWorkspace or IEnumDatasetName object.
    '''' 
    '''' To access the IDataset themselves use the method getESRIDataSetsFromWorkspace()
    '''' </remarks>
    'Private Function getESRIDataSetNamesFromWorkspace(ByRef wrksp As IWorkspace, ByVal blnRecuse As Boolean) As List(Of IDatasetName)
    '    Return getESRIDataSetNamesFromWorkspace(wrksp.DatasetNames(esriDatasetType.esriDTAny), blnRecuse)
    'End Function

    '''' <summary>
    '''' Returns a list of ESRI.ArcGIS.Geodatabase.IDatasetName objects present within
    '''' an IWorkspace or IEnumDatasetName object.
    '''' </summary>
    '''' <param name="edn">A DatasetName emnumerator IEnumDatasetName</param>
    '''' <param name="blnRecuse">TRUE = The workspace should be recused if relevant,
    '''' FALSE = the workspace should not be recursed</param>
    '''' <returns>a list of ESRI.ArcGIS.Geodatabase.IDatasetName objects present within
    '''' an IWorkspace or IEnumDatasetName object.</returns>
    '''' <remarks>
    '''' Returns a list of ESRI.ArcGIS.Geodatabase.IDatasetName objects present within
    '''' an IWorkspace or IEnumDatasetName object.
    '''' 
    '''' To access the IDataset themselves use the method getESRIDataSetsFromWorkspace()
    '''' </remarks>
    'Private Function getESRIDataSetNamesFromWorkspace(ByRef edn As IEnumDatasetName, _
    '                                                  ByVal blnRecuse As Boolean) As List(Of IDatasetName)
    '    Dim lstDSNames As New List(Of IDatasetName)
    '    Dim dsName As IDatasetName

    '    dsName = edn.Next()

    '    While Not dsName Is Nothing
    '        If blnRecuse And (dsName.Type = esriDatasetType.esriDTContainer) Then
    '            lstDSNames.AddRange(getESRIDataSetNamesFromWorkspace(dsName.SubsetNames, blnRecuse))
    '        Else
    '            lstDSNames.Add(dsName)
    '        End If
    '        dsName = edn.Next()
    '    End While

    '    Return lstDSNames
    'End Function

    ''' <summary>
    ''' A convinence function for exacting a list of names as strings froma list of IDatasetName objects.
    ''' </summary>
    ''' <param name="dsList">A list of IDatasetName objects</param>
    ''' <returns>A List(Of Strings), with the names of each of the dsList objects.</returns>
    ''' <remarks>
    ''' A convinence function for exacting a list of names as strings froma list of IDatasetName objects.
    ''' </remarks>
    Private Function getNamesStrFromESRIDataSetName(ByRef dsList As List(Of IDatasetName)) As List(Of String)
        Dim lstDSNames As New List(Of String)

        For Each ds In dsList
            lstDSNames.Add(ds.Name)
        Next

        Return lstDSNames
    End Function


End Class
