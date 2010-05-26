Imports System.Data
Imports System.Data.OleDb
Imports System.IO

'todo check the name of the XXXX factory class.
''' <summary>
''' Provides a specfic implenmentation of the IDataNameClauseLookup, based on storing the
''' Data Name Clause Lookup Tables in an Access Database file.
''' </summary>
''' <remarks>
''' Provides a specfic implenmentation of the IDataNameClauseLookup, based on storing the
''' Data Name Clause Lookup Tables in an Access Database file.
'''
''' There is no public constructor for this class. New instances should be generated using 
''' the XXXX factory class.
''' </remarks>
Public Class MDBDataNameClauseLookup
    Inherits AbstractDataNameClauseLookup

    Private m_DBConnection As IDbConnection = Nothing
    Private m_fInfoPath As FileInfo

    ''' <summary>
    ''' The constuctor. This should only be call from within the relevant 
    ''' factory class.
    ''' </summary>
    ''' <param name="strFullName">A string of the full path to the Access MDB file</param>
    ''' <remarks>
    ''' The constuctor. This should only be call from within the relevant 
    ''' factory class.
    ''' </remarks>
    Protected Friend Sub New(ByVal strFullName As String)
        Dim fInfoArg As FileInfo

        If strFullName Is Nothing Then
            Throw New ArgumentException("Invalid path passed to New MDBDataNameClauseLookup(args)")
        Else
            fInfoArg = New FileInfo(strFullName)
            'System.Console.WriteLine("args(0)= " & args(0))
            initialiseConnectionObject(fInfoArg)
            initialiseAllTables()
        End If
    End Sub


    ''' <summary>
    ''' The constuctor. This should only be call from within the relevant 
    ''' factory class.
    ''' </summary>
    ''' <param name="fileInfoArg">A FileInfo object pointing to the full 
    ''' path to the Access MDB file</param>
    ''' <remarks>
    ''' The constuctor. This should only be call from within the relevant 
    ''' factory class.
    ''' </remarks>
    Protected Friend Sub New(ByRef fileInfoArg As FileInfo)
        initialiseConnectionObject(fileInfoArg)
        initialiseAllTables()
    End Sub


    ''' <summary>
    ''' Sets up the m_DBConnection object. Only called from within the constructor
    ''' </summary>
    ''' <param name="fInfoPathToMDB">A string of the full path to the Access MDB file</param>
    ''' <remarks>
    ''' Sets up the m_DBConnection object. Only called from within the constructor
    ''' </remarks>
    Private Sub initialiseConnectionObject(ByRef fInfoPathToMDB As FileInfo)

        Dim strPrefixCon As String = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source="

        If Not fInfoPathToMDB.Exists Then
            Throw New ArgumentException("Invalid path passed to New MDBDataNameClauseLookup(fileInfoArg)")

            m_DBConnection = New OleDbConnection(strPrefixCon & fInfoPathToMDB.FullName)
            m_DBConnection.Open()

            m_fInfoPath = fInfoPathToMDB
        End If
    End Sub


    ''' <summary>
    ''' This method opens the individual named flat table from as a DataTable object from the 
    ''' Access DB.
    ''' </summary>
    ''' <param name="strTableName">The name of the table to open. This should normally be passed
    ''' using one of the API constants with the prefix "TABLENAME_"</param>
    ''' <returns>A DataTable object representing the named table</returns>
    ''' <remarks>
    ''' This method opens the individual named flat table from as a DataTable object from the 
    ''' Access DB.
    ''' </remarks>
    Protected Overrides Function openTable(ByVal strTableName As String) As System.Data.DataTable
        Dim strQuery As String
        Dim dbCommand As IDbCommand = New OleDbCommand
        'Dim da As IDbDataAdapter = New OleDbDataAdapter
        Dim dtb As DataTable

        strQuery = "SELECT * FROM " & strTableName

        dbCommand.CommandText = strQuery
        dbCommand.Connection = m_DBConnection

        dtb = getTableFromReader(dbCommand.ExecuteReader())

        Return dtb

    End Function


    ''' <summary>
    ''' Returns the ConnectionString used to connect to the Acess DB.
    ''' </summary>
    ''' <returns>the ConnectionString used to connect to the Acess DB.</returns>
    ''' <remarks>
    ''' Returns the ConnectionString used to connect to the Acess DB.
    ''' </remarks>
    Public Overrides Function getDetails() As String
        Return m_DBConnection.ConnectionString
    End Function


    ''' <summary>
    ''' Returns the operating system file path to the Access DataBase.
    ''' </summary>
    ''' <returns>A FileInfo object representing the operating system file path to the  
    ''' Access DataBase.</returns>
    ''' <remarks>
    ''' Returns the operating system file path to the Access DataBase.
    ''' </remarks>
    Public Overrides Function getPath() As System.IO.FileInfo
        Return m_fInfoPath
    End Function


    ''' <summary>
    ''' A convenence function to produce a new disconnected rewindable datatable
    ''' object based data read from the dataReader object.
    ''' </summary>
    ''' <param name="sqldr">A readonly, read-foward DataReader object</param>
    ''' <returns>A new DataTable object</returns>
    ''' <remarks>
    ''' A convenence function to produce a new disconnected rewindable datatable
    ''' object based data read from the dataReader object.
    ''' </remarks>
    Private Function getTableFromReader(ByVal sqldr As IDataReader) As DataTable

        Dim i As Int32
        Dim dtSchema As DataTable = sqldr.GetSchemaTable()
        Dim dtNew As DataTable = New System.Data.DataTable
        Dim dc As DataColumn
        'Dim _row As DataRow

        'set the schema of the new datatable to match the Data reader
        For i = 0 To dtNew.Rows.Count - 1
            dc = New DataColumn
            dc.ColumnName = dtSchema.Rows(i)("ColumnName").ToString()
            dc.DataType = CType(dtSchema.Rows(i)("DataType"), System.Type)
            dc.Unique = Convert.ToBoolean(dtSchema.Rows(i)("IsUnique"))
            dc.AllowDBNull = Convert.ToBoolean(dtSchema.Rows(i)("AllowDBNull"))
            dc.ReadOnly = Convert.ToBoolean(dtSchema.Rows(i)("IsReadOnly"))
            dtNew.Columns.Add(dc)
        Next

        'now copy across the contents
        While sqldr.Read()
            Dim dr As DataRow = dtNew.NewRow()
            For i = 0 To sqldr.FieldCount - 1
                dr(i) = sqldr.GetValue(i)
            Next
            dtNew.Rows.Add(dr)
        End While

        Return dtNew

    End Function

End Class
