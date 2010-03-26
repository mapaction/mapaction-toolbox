Imports System.Data
Imports System.Data.OleDb

Public Class MDBDataNameClauseLookup
    Inherits AbstractDataNameClauseLookup

    Private myDBConnection As IDbConnection = Nothing

    ''' <summary>
    ''' Expects the first argument to be the full path to the Access MDB file. Ignores other argument.
    ''' </summary>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Protected Friend Sub New(ByVal args As String())
        If args Is Nothing OrElse args.Length < 1 Then
            Throw New ArgumentException("Invalid path passed to New MDBDataNameClauseLookup(args)")
        Else
            'System.Console.WriteLine("args(0)= " & args(0))
            initialiseConnectionObject(args(0))
        End If

        initialiseAllTables()
    End Sub

    Private Sub initialiseConnectionObject(ByRef pathToMDB As String)

        Dim prefixConStr As String = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source="
        Dim constr As String

        constr = prefixConStr & pathToMDB

        'System.Console.WriteLine("constr= " & constr)

        myDBConnection = New System.Data.OleDb.OleDbConnection(constr)
        myDBConnection.Open()

    End Sub


    Public Overrides Function isWriteable() As Boolean
        isWriteable = False
    End Function

    Protected Overrides Function openTable(ByVal tableName As String) As System.Data.DataTable
        Dim queryString As String
        Dim dbCommand As IDbCommand = New OleDbCommand
        Dim thedataAdapter As IDbDataAdapter = New OleDbDataAdapter
        Dim theDataTable As DataTable

        queryString = "SELECT * FROM " & tableName

        'System.Console.WriteLine("openTable() tableName = " & tableName)
        'System.Console.WriteLine("myDBConnection.Database = " & myDBConnection.Database)

        dbCommand.CommandText = queryString
        dbCommand.Connection = myDBConnection

        theDataTable = getTableFromReader(dbCommand.ExecuteReader())

        openTable = theDataTable

    End Function

    Private Function getTableFromReader(ByVal sqldr As IDataReader) As DataTable

        Dim i As Int32
        Dim _table As DataTable = sqldr.GetSchemaTable()
        Dim _dt As DataTable = New System.Data.DataTable
        Dim _dc As DataColumn
        'Dim _row As DataRow

        For i = 0 To _table.Rows.Count - 1
            _dc = New DataColumn
            _dc.ColumnName = _table.Rows(i)("ColumnName").ToString()
            _dc.DataType = _table.Rows(i)("DataType")
            _dc.Unique = Convert.ToBoolean(_table.Rows(i)("IsUnique"))
            _dc.AllowDBNull = Convert.ToBoolean(_table.Rows(i)("AllowDBNull"))
            _dc.ReadOnly = Convert.ToBoolean(_table.Rows(i)("IsReadOnly"))
            _dt.Columns.Add(_dc)
        Next

        While sqldr.Read()
            Dim dr As DataRow = _dt.NewRow()
            For i = 0 To sqldr.FieldCount - 1
                dr(i) = sqldr.GetValue(i)
            Next
            _dt.Rows.Add(dr)
        End While

        Return _dt

    End Function


End Class
