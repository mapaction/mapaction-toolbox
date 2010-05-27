Imports System.IO

''' <summary>
''' A factory class for generating IDataNameClauseLookup objects.
''' </summary>
''' <remarks>
''' A factory class for generating IDataNameClauseLookup objects.
''' 
''' The class is a singleton object. There is no public constructor.
''' References to this object should be obtained using the 
''' getFactory() method.
''' </remarks>
Public Class DataNameClauseLookupFactory
    Private Shared m_Factory As DataNameClauseLookupFactory

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
    Public Shared Function getFactory() As DataNameClauseLookupFactory
        If m_Factory Is Nothing Then
            m_Factory = New DataNameClauseLookupFactory
        End If

        getFactory = m_Factory
    End Function

    
    ''' <summary>
    ''' Create a new IDataNameClauseLookup, based on the type and location specified.
    ''' </summary>
    ''' <param name="enuDNCLType">The type of IDataNameClauseLookup to be created</param>
    ''' <param name="strAryArgs">An array of strings which specific the details of the
    ''' physical location of the Data Name Clause Lookup Tables</param>
    ''' <returns>A new IDataNameClauseLookup, based on the type and location specified.</returns>
    ''' <remarks>
    ''' Create a new IDataNameClauseLookup, based on the type and location specified.
    ''' 
    ''' If dnClauseLookupType.MDB then strAryArgs(0) should be the full path of the Access DB.
    ''' If dnClauseLookupType.ESRI_GDB then either strAryArgs(0) should be the full path of 
    ''' the GDB or strAryArgs should be a collection of connection parameters for an SDE GDB.
    ''' </remarks>
    Public Function createDataNameClauseLookup(ByVal enuDNCLType As dnClauseLookupType, _
                                               ByRef strAryArgs() As String) As IDataNameClauseLookup
        Dim dnclResult As IDataNameClauseLookup

        Select Case enuDNCLType
            Case dnClauseLookupType.MDB
                dnclResult = New MDBDataNameClauseLookup(strAryArgs(0))
            Case dnClauseLookupType.ESRI_GDB
                dnclResult = New GDBDataNameClauseLookup(strAryArgs)
            Case Else
                Throw New ArgumentException("DataNameClauseLookup of type " & enuDNCLType & " not recgonised.")
        End Select

        Return dnclResult
    End Function

    ''' <summary>
    ''' Searches for the physical location of Data Name Clause Lookup Tables within the 
    ''' Directory specified.
    ''' </summary>
    ''' <param name="dInfo">The directory to search. If the directory does not exist then
    ''' an ArgumentException is raised.</param>
    ''' <returns>A IDataNameClauseLookup object based on a set of Data Name Clause Lookup
    ''' Tables which where found in the directory specificed. If no Data Name Clause Lookup
    ''' Tables are found then a LookupTableException is raised.</returns>
    ''' <remarks>
    ''' Searches for the physical location of Data Name Clause Lookup Tables within the 
    ''' Directory specified.
    ''' 
    ''' First searches for Access DBs, which matchs the filename pattern given by
    ''' MDB_DATACLAUSE_FILE_PREFIX. 
    ''' 
    ''' If an Access DB is not found then the method searchs for any GDB with teh relevant
    ''' tables present.
    ''' </remarks>
    Public Function createDataNameClauseLookup(ByRef dInfo As DirectoryInfo) As IDataNameClauseLookup
        Dim dnclResult As IDataNameClauseLookup
        Dim lstfInfo As List(Of FileInfo)

        dnclResult = Nothing
        'System.Console.WriteLine("Starting createDataNameClauseLookup(ByRef dirInfo As DirectoryInfo)")

        If Not dInfo.Exists() Then
            Throw New ArgumentException("DataNameClauseLookup tables could not be found in not existent directory " & dInfo.FullName)
        Else
            'todo LOW For now we temporarily call look for a standalone MDB before a GDB. In due course the ESRI route should be prefered

            'Search for MDBs and open as an MDBDataNameClauseLookup
            Dim aryFInfoAccessDBs() As FileInfo
            'Dim fInfoCurrent As FileInfo
            'Dim curDirIdx As Integer

            aryFInfoAccessDBs = dInfo.GetFiles(MDB_DATACLAUSE_FILE_PREFIX & "*.mdb")
            Array.Sort(aryFInfoAccessDBs, New FileInfoComparer())
            Array.Reverse(aryFInfoAccessDBs)

            'curDirIdx = aryFInfoAccessDBs.Length - 1

            'first try MDB files
            For Each curFileInfo In aryFInfoAccessDBs
                'System.Console.WriteLine("found file: " & curFileInfo.FullName)
                If dnclResult Is Nothing Then
                    Try
                        dnclResult = New MDBDataNameClauseLookup(curFileInfo)
                    Catch ex As Exception
                        'System.Console.WriteLine("MDBDataNameClauseLookup threw an exception")
                        'System.Console.WriteLine(ex.ToString())
                    End Try
                End If
            Next

            'if still no joy try searching in ESRI GDBs 
            If dnclResult Is Nothing Then
                lstfInfo = getGDBsInDir(dInfo)

                For Each curFileInfo In lstfInfo
                    'System.Console.WriteLine("found file: " & curFileInfo.FullName)
                    If dnclResult Is Nothing Then
                        Try
                            dnclResult = New GDBDataNameClauseLookup(curFileInfo.FullName)
                        Catch ex As Exception
                            'System.Console.WriteLine("GeoDBDataNameClauseLookup threw an exception")
                            'System.Console.WriteLine(ex.ToString())
                        End Try
                    End If
                Next
            End If

            'finally if no joy then throw exception
            If dnclResult Is Nothing Then
                'Throw New LookupTableException("Unable to find valid DataName Clause Lookup Tables in directory: " & dirInfo.FullName)
                Throw New LookupTableException(dnLookupTableError.DefaultTablesNotFound, dInfo.FullName)
            End If

        End If

        Return dnclResult
    End Function

End Class
