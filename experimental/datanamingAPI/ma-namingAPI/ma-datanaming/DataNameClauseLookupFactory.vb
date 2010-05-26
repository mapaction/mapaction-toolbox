Imports System.IO

Public Class DataNameClauseLookupFactory
    Shared myFactory As DataNameClauseLookupFactory

    Private Sub New()
    End Sub

    Public Shared Function getFactory() As DataNameClauseLookupFactory
        If myFactory Is Nothing Then
            myFactory = New DataNameClauseLookupFactory
        End If

        getFactory = myFactory
    End Function

    Public Function createDataNameClauseLookup(ByVal dnclType As dnClauseLookupType, Optional ByRef args As String() = Nothing) As IDataNameClauseLookup
        Dim theDataNameClauseLookup As IDataNameClauseLookup

        Select Case dnclType
            Case dnClauseLookupType.MDB
                theDataNameClauseLookup = New MDBDataNameClauseLookup(args(0))
            Case dnClauseLookupType.ESRI_GDB
                theDataNameClauseLookup = New GDBDataNameClauseLookup(args(0))
            Case Else
                Throw New ArgumentException("DataNameClauseLookup of type " & dnclType & " not recgonised.")
        End Select

        Return theDataNameClauseLookup
    End Function

    Public Function createDataNameClauseLookup(ByRef dirInfo As DirectoryInfo) As IDataNameClauseLookup
        Dim returnNDCL As IDataNameClauseLookup
        Dim fileRefs As List(Of FileInfo)

        returnNDCL = Nothing
        'System.Console.WriteLine("Starting createDataNameClauseLookup(ByRef dirInfo As DirectoryInfo)")

        If Not dirInfo.Exists() Then
            Throw New ArgumentException("DataNameClauseLookup tables could not be found in not existent directory " & dirInfo.FullName)
        Else
            'todo LOW For now we temporarily call look for a standalone MDB before a GDB. In due course the ESRI route should be prefered

            'Search for MDBs and open as an MDBDataNameClauseLookup
            Dim dirContents() As FileInfo
            Dim curFileInfo As FileInfo
            Dim curDirIdx As Integer

            dirContents = dirInfo.GetFiles(MDB_DATACLAUSE_FILE_PREFIX & "*.mdb")
            Array.Sort(dirContents, New FileInfoComparer())
            Array.Reverse(dirContents)

            curDirIdx = dirContents.Length - 1

            'first try MDB files
            For Each curFileInfo In dirContents
                'System.Console.WriteLine("found file: " & curFileInfo.FullName)
                If returnNDCL Is Nothing Then
                    Try
                        returnNDCL = New MDBDataNameClauseLookup(curFileInfo)
                    Catch ex As Exception
                        'System.Console.WriteLine("MDBDataNameClauseLookup threw an exception")
                        'System.Console.WriteLine(ex.ToString())
                    End Try
                End If
            Next

            'if still no joy try searching in ESRI GDBs 
            If returnNDCL Is Nothing Then
                fileRefs = getGDBsInDir(dirInfo)
                
                For Each curFileInfo In fileRefs
                    'System.Console.WriteLine("found file: " & curFileInfo.FullName)
                    If returnNDCL Is Nothing Then
                        Try
                            returnNDCL = New GDBDataNameClauseLookup(curFileInfo.FullName)
                        Catch ex As Exception
                            'System.Console.WriteLine("GeoDBDataNameClauseLookup threw an exception")
                            'System.Console.WriteLine(ex.ToString())
                        End Try
                    End If
                Next
            End If

            'finally if no joy then throw exception
            If returnNDCL Is Nothing Then
                'Throw New LookupTableException("Unable to find valid DataName Clause Lookup Tables in directory: " & dirInfo.FullName)
                Throw New LookupTableException(dnLookupTableError.default_tbls_not_found, dirInfo.FullName)
            End If

        End If

        Return returnNDCL
    End Function

End Class
