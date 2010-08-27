Imports System.IO
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Framework

''' <summary>
''' A factory class for generating IDataListConnection objects.
''' </summary>
''' <remarks>
''' A factory class for generating IDataListConnection objects.
''' 
''' The class is a singleton object. There is no public constructor.
''' References to this object should be obtained using the 
''' getFactory() method.
''' </remarks>
Public Class DataListConnectionFactory

    Private Shared m_Factory As DataListConnectionFactory = Nothing

    ''' <summary>
    ''' A private constructor to force the user to obtain a copy using
    ''' the getFactory method.
    ''' </summary>
    ''' <remarks>
    ''' A private constructor to force the user to obtain a copy using
    ''' the getFactory method.
    ''' </remarks>
    Private Sub New()
    End Sub

    ''' <summary>
    ''' A method for obtaining a reference to this object.
    ''' </summary>
    ''' <returns>A reference to the singleton 
    ''' DataListConnectionFactory object.</returns>
    ''' <remarks>
    ''' A method for obtaining a reference to this object.
    ''' </remarks>
    Public Shared Function getFactory() As DataListConnectionFactory
        If m_Factory Is Nothing Then
            m_Factory = New DataListConnectionFactory()
        End If

        Return m_Factory
    End Function

    ''' <summary>
    ''' Creates a new IDataListConnection of the type specified by enuListType 
    ''' using the values in the string array parameter.
    ''' </summary>
    ''' <param name="enuListType">The physical type of DataListConnection 
    ''' list to dnListType</param>
    ''' <param name="strAryArgs">A String containing the path to the file or directory 
    ''' for the list. See remarks for more details.</param>
    ''' <returns>An IDataListConnection based on the location specificed in 
    ''' strAryArgs of the type specified. See remarks for more details.</returns>
    ''' <remarks>
    ''' Creates a new IDataListConnection of the type specified by enuListType 
    ''' using the values in the string array parameter.
    ''' 
    ''' If dnListType.MXD then strAryArgs(0) should be a file with extention = ".mxd".
    ''' If dnListType.GDB then strAryArgs(0) should be a file with extention = ".mdb", ".sde",
    '''  ".ags" or ".gds", OR strAryArgs(0) is a directory with extention = ".gdb", OR
    '''  strAryArgs is a list of parameters for connection to a SDE GDB.
    ''' If dnListType.DIR then strAryArgs(0) should be a directory with no extention,
    ''' </remarks>
    Public Function createDataListConnection(ByVal enuListType As dnListType, _
                                             ByRef strAryArgs() As String) As IDataListConnection
        Dim dlcResult As IDataListConnection

        Select Case enuListType
            Case dnListType.GDB
                dlcResult = New DataListGeoDBConnection(strAryArgs(0))
            Case dnListType.DIR
                dlcResult = New DataListFileSystemDirectory(New DirectoryInfo(strAryArgs(0)))
                'Case DATALIST_TYPE_MIXED_FILES
                'todo LOW: Add constructor call here, once it is writen
            Case dnListType.MXD
                dlcResult = New DataListMXDConnection(strAryArgs(0))
            Case Else
                Throw New ArgumentException("Unregconised GeoDataListConnection Type")
        End Select

        Return dlcResult
    End Function


    ''' <summary>
    ''' Creates a new IDataListConnection based on the string array parameter.
    ''' </summary>
    ''' <param name="strAryArgs">A String containing the path to the file or directory 
    ''' for the list. See remarks for more details.</param>
    ''' <returns>An IDataListConnection based on the location specificed in 
    ''' strPath and of an automatically determined type. See remarks for more
    ''' details.</returns>
    ''' <remarks>
    ''' Creates a new IDataListConnection based on the string array parameter.
    ''' 
    ''' The method attempts to determine the type of the IDataListConnection, initially by 
    ''' checking the contents of the first member of the string array, which is assumed 
    ''' to be a full path name:
    ''' 
    ''' If strAryArgs(0) is a file with extention = ".mxd", a DataListMXDConnection is returned.
    ''' If strAryArgs(0) is a file with extention = ".mdb", ".sde", ".ags" or ".gds", OR 
    '''  strAryArgs(0) is a directory with extention = ".gdb", a DataListDBConnection is returned.
    ''' If strAryArgs(0) is a directory with no extention, a DataListFileSystemDirectory is returned.
    ''' 
    ''' If none of the conditions above are meet, then the method assumes that the strAryArgs
    ''' is a list of parameters for connection to a SDE GDB.
    ''' </remarks>
    Public Function createDataListConnection(ByRef strAryArgs() As String) As IDataListConnection

        Dim dlcResult As IDataListConnection
        Dim fInfoArgs As FileInfo

        If (strAryArgs Is Nothing) OrElse strAryArgs.Length < 1 Then
            Throw New ArgumentNullException()
        Else
            'See if the first argument is a valid file or directory
            fInfoArgs = New FileInfo(strAryArgs(0))


            If fInfoArgs.Exists() Then
                'args(0) is a file
                'therefore it is either a personal GDB, a GDB connection file or an MXD
                If fInfoArgs.Extension = ".mxd" Then
                    dlcResult = createDataListConnection(dnListType.MXD, strAryArgs)
                Else
                    dlcResult = createDataListConnection(dnListType.GDB, strAryArgs)
                End If
            ElseIf (New DirectoryInfo(fInfoArgs.FullName)).Exists() Then
                'args(0) is a directory
                'therefore it is either a filebasedGDB or a normal directory
                If fInfoArgs.FullName.EndsWith(".gdb") Then
                    dlcResult = createDataListConnection(dnListType.GDB, strAryArgs)
                Else
                    dlcResult = createDataListConnection(dnListType.DIR, strAryArgs)
                End If

            Else
                'args(0) is neither a file nor a directory, hence we will try it as a list of
                'connection proporties
                dlcResult = createDataListConnection(dnListType.GDB, strAryArgs)
            End If

        End If

        Return dlcResult
    End Function


    ''' <summary>
    ''' Creates a new IDataListConnection based on the string array parameter.
    ''' </summary>
    ''' <param name="strPath">A String containing the path to the file or directory 
    ''' for the list. See remarks for more details.</param>
    ''' <returns>An IDataListConnection based on the location specificed in 
    ''' strPath and of an automatically determined type. See remarks for more
    ''' details.</returns>
    ''' <remarks>
    ''' Creates a new IDataListConnection based on the string array parameter.
    ''' 
    ''' The method attempts to determine the type of the IDataListConnection:
    ''' 
    ''' If strPath is a file with extention = ".mxd", a DataListMXDConnection is returned.
    ''' If strPath is a file with extention = ".mdb", ".sde", ".ags" or ".gds", OR 
    '''  strPath is a directory with extention = ".gdb", a DataListDBConnection is returned.
    ''' If strPath is a directory with no extention, a DataListFileSystemDirectory is returned.
    ''' </remarks>
    Public Function createDataListConnection(ByRef strPath As String) As IDataListConnection
        Dim srtAryArgs() As String = {strPath}
        Return createDataListConnection(srtAryArgs)
    End Function


    '''' <summary>
    '''' Creates a new IDataListConnection (specifically a DataListMXDConnection) 
    '''' based on the reference to the IMxDocument object from within ArcMap.
    '''' </summary>
    '''' <param name="mxMap">A reference to the IMxDocument object from within 
    '''' ArcMap.</param>
    '''' <returns>An IDataListConnection based on the MXD currently loaded
    '''' into ArcMap.</returns>
    '''' <remarks>
    '''' Creates a new IDataListConnection (specifically a DataListMXDConnection) 
    '''' based on the reference to the IMxDocument object from within ArcMap.
    '''' </remarks>
    'Public Function createDataListConnection(ByRef mxMap As IMxDocument) As IDataListConnection
    '    Return New DataListMXDConnection(mxMap)
    'End Function

    'todo HIGH: test this
    Public Function createDataListConnection(ByRef app As IApplication) As IDataListConnection
        Return New DataListMXDConnection(app)
    End Function

End Class
