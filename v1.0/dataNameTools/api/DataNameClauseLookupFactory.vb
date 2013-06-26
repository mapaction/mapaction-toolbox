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
Imports ADODB

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

    'There seems to be able to a problem with this
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
    Public Shared Function createDataNameClauseLookup(ByVal enuDNCLType As dnClauseLookupType, _
                                               ByRef strAryArgs() As String, _
                                               ByVal blnReadWrite As Boolean) As IDataNameClauseLookup
        Dim dnclResult As IDataNameClauseLookup

        Select Case enuDNCLType
            Case dnClauseLookupType.MDB
                dnclResult = New MDBDataNameClauseLookup(strAryArgs(0), getRWmodeAsLong(blnReadWrite))
            Case dnClauseLookupType.ESRI_GDB
                dnclResult = New GDBDataNameClauseLookup(strAryArgs(0), getRWmodeAsLong(blnReadWrite))
            Case Else
                Throw New ArgumentException("DataNameClauseLookup of type " & enuDNCLType & " not recgonised.")
        End Select

        Return dnclResult
    End Function


    ''' <summary>
    ''' Create a new IDataNameClauseLookup, based on the location specified.
    ''' </summary>
    ''' <param name="strPath">A string of the path to the specific physical location 
    ''' of the Data Name Clause Lookup Tables</param>
    ''' <returns>A new IDataNameClauseLookup, based on the location specified.</returns>
    ''' <remarks>
    ''' Create a new IDataNameClauseLookup, based on the type and location specified.
    ''' 
    ''' If the string is to an Access DB, then the method will first attempt to open
    ''' it as a GDB and if that fails it will attempt to open it as a regular Access DB.
    ''' All other paths are interperated as pointing to a GDB (either filebased or a 
    ''' connection file).
    ''' </remarks>
    Public Shared Function createDataNameClauseLookup(ByRef strPath As String, ByVal blnReadWriteMode As Boolean) As IDataNameClauseLookup
        Dim dnclResult As IDataNameClauseLookup
        Dim fInfo As FileInfo
        Dim dInfo As DirectoryInfo
        Dim strAryArgs(1) As String

        fInfo = New FileInfo(strPath)
        strAryArgs(0) = strPath


        If fInfo.Exists() Then
            Select Case fInfo.Extension
                Case ".sde", ".ags", ".gds"
                    dnclResult = createDataNameClauseLookup(dnClauseLookupType.ESRI_GDB, strAryArgs, blnReadWriteMode)
                Case ".mdb"
                    Try
                        dnclResult = createDataNameClauseLookup(dnClauseLookupType.ESRI_GDB, strAryArgs, blnReadWriteMode)
                    Catch ex As Exception
                        dnclResult = createDataNameClauseLookup(dnClauseLookupType.MDB, strAryArgs, blnReadWriteMode)
                    End Try
                Case Else
                    Throw New ArgumentException(String.Format("Data Name Clause Lookup Tables could not be found at:", strPath))
            End Select
        ElseIf (New DirectoryInfo(fInfo.FullName)).Exists() Then
            'args(0) is a directory
            'therefore it is either a filebasedGDB or a normal directory
            If fInfo.FullName.EndsWith(".gdb") Then
                dnclResult = createDataNameClauseLookup(dnClauseLookupType.ESRI_GDB, strAryArgs, blnReadWriteMode)
            Else
                dInfo = New DirectoryInfo(fInfo.FullName)
                dnclResult = createDataNameClauseLookup(dInfo, blnReadWriteMode)
            End If
        Else
            Throw New ArgumentException(String.Format("Data Name Clause Lookup Tables could not be found at:", strPath))
        End If

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
    Public Shared Function createDataNameClauseLookup(ByRef dInfo As DirectoryInfo, ByVal blnReadWriteMode As Boolean) As IDataNameClauseLookup
        Dim dnclResult As IDataNameClauseLookup
        Dim lstfInfo As List(Of FileInfo)

        dnclResult = Nothing
        'System.Console.WriteLine("Starting createDataNameClauseLookup(ByRef dirInfo As DirectoryInfo)")

        If Not dInfo.Exists() Then
            Throw New ArgumentException("DataNameClauseLookup tables could not be found in not existent directory " & dInfo.FullName)
        Else
            'todo LOW: For now we temporarily call look for a standalone MDB before a GDB. In due course the ESRI route should be prefered

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
                        dnclResult = New MDBDataNameClauseLookup(curFileInfo, getRWmodeAsLong(blnReadWriteMode))
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
                            dnclResult = New GDBDataNameClauseLookup(curFileInfo.FullName, getRWmodeAsLong(blnReadWriteMode))
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

    Public Shared Function getFallBackDataNameClauseLookup() As IDataNameClauseLookup
        Dim dnclResult As IDataNameClauseLookup
        Dim strDSNpath As String

        'strMDBpath = My.Application.Info.DirectoryPath & My.Resources.fallbackMDBfilename

        'todo HIGH: Undo this ugly hack!!!!
        strDSNpath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles) & _
                        "\MapAction\dataNameTools\fall_back_data_naming_conventions_v1.0.dsn"
        'dnclResult = New MDBDataNameClauseLookup(strMDBpath, getRWmodeAsLong(False))
        dnclResult = New MDBDataNameClauseLookup(strDSNpath, getRWmodeAsLong(False), True)
        Return dnclResult
    End Function

    Private Shared Function getRWmodeAsLong(ByVal blnReadWrite As Boolean) As Long
        Dim lngResult As Long

        If blnReadWrite Then
            lngResult = ConnectModeEnum.adModeShareDenyNone
        Else
            lngResult = ConnectModeEnum.adModeRead
        End If

        Return lngResult
    End Function
End Class
