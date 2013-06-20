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


    ''' <summary>
    ''' The constuctor. This should only be call from within the relevant 
    ''' factory class.
    ''' </summary>
    ''' <param name="strArgs">A string of the full path to the Access MDB file</param>
    ''' <remarks>
    ''' The constuctor. This should only be call from within the relevant 
    ''' factory class.
    ''' </remarks>
    Protected Friend Sub New(ByVal strArgs As String)
        If strArgs Is Nothing Then
            Throw New ArgumentException("Invalid path passed to New MDBDataNameClauseLookup(args)")
        Else
            'System.Console.WriteLine("args(0)= " & args(0))
            initialiseConnectionObject(strArgs)
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
        If Not fileInfoArg.Exists Then
            Throw New ArgumentException("Invalid path passed to New MDBDataNameClauseLookup(fileInfoArg)")
        Else
            initialiseConnectionObject(fileInfoArg.FullName)
            initialiseAllTables()
        End If
    End Sub


    ''' <summary>
    ''' Sets up the m_DBConnection object. Only called from within the constructor
    ''' </summary>
    ''' <param name="strPathToMDB">A string of the full path to the Access MDB file</param>
    ''' <remarks>
    ''' Sets up the m_DBConnection object. Only called from within the constructor
    ''' </remarks>
    Private Sub initialiseConnectionObject(ByRef strPathToMDB As String)

        Dim strPrefixCon As String = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source="
        Dim strConnect As String

        strConnect = strPrefixCon & strPathToMDB

        m_DBConnection = New OleDbConnection(strConnect)
        m_DBConnection.Open()

    End Sub


    'todo is this needed?
    Public Overrides Function isWriteable() As Boolean
        isWriteable = False
    End Function


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
        Dim dt As DataTable

        strQuery = "SELECT * FROM " & strTableName

        dbCommand.CommandText = strQuery
        dbCommand.Connection = m_DBConnection

        dt = getTableFromReader(dbCommand.ExecuteReader())

        Return dt

    End Function


    'todo is this required?
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function getDetails() As String
        Return m_DBConnection.ConnectionString
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
