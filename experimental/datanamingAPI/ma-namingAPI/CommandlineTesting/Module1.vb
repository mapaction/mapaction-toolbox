
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.Common
Imports System.IO
Imports System.Xml



Module Module1


    'Constants relating to the names of table which store all of the data name clauses
    Public Const TABLENAME_GEOEXTENT As String = "datanaming_clause_geoextent"
    Public Const TABLENAME_DATA_CAT As String = "datanaming_clause_data_categories"
    Public Const TABLENAME_DATA_THEME As String = "datanaming_clause_data_theme"
    Public Const TABLENAME_DATA_TYPE As String = "datanaming_clause_data_type"
    Public Const TABLENAME_SCALE As String = "datanaming_clause_scale"
    Public Const TABLENAME_SOURCE As String = "datanaming_clause_source"
    Public Const TABLENAME_PERMISSION As String = "datanaming_clause_permission"

    'Common name for Primary Key for all data name clause tables
    Public Const PRI_KEY_COL_NAME As String = "clause"

    'An array of all of the data name clause tables names
    Private m_strAryTableNames() As String = _
                New String() {CStr(TABLENAME_GEOEXTENT), _
                              CStr(TABLENAME_DATA_CAT), _
                              CStr(TABLENAME_DATA_THEME), _
                              CStr(TABLENAME_DATA_TYPE), _
                              CStr(TABLENAME_SCALE), _
                              CStr(TABLENAME_SOURCE), _
                              CStr(TABLENAME_PERMISSION)}

    Sub Main()
        ' Dim gbdcon As geodatasourceconnection
        Dim oleDBConnection As OleDb.OleDbConnection = Nothing
        Dim strPrefixCon As String = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source="
        Dim strConnect As String
        Dim ds As DataSet
        Dim da As DbDataAdapter
        Dim strQuery As String
        Dim dbCommand As IDbCommand = New OleDbCommand
        Dim dtm As DataTableMapping
        'Dim da As IDbDataAdapter = New OleDbDataAdapter
        Dim dt As DataTable

        Try
            strConnect = strPrefixCon & "D:\MapAction\bronze\custom_tools\managedcode\testing_datanamingAPI\" & _
                                        "2010-05-05-testing-v02\GIS\2_Active_Data\data_naming_conventions_v0.9.mdb"

            oleDBConnection = New OleDbConnection(strConnect)
            oleDBConnection.Open()
            'System.Console.WriteLine(strConnect)

            ds = New DataSet("dncl")

            Dim strXMLSchema As String
            Dim xmlReader As System.Xml.XmlReader
            Dim srt As Stream
            Dim txtReader As System.IO.TextReader

            'strXMLSchema = My.Resources.datanameclauselookup_schema_v1_0
            strXMLSchema = My.Resources.schema

            txtReader = New StringReader(strXMLSchema)

            ds.ReadXmlSchema(txtReader)

            ds.WriteXmlSchema(System.Console.OpenStandardOutput())
            System.Console.WriteLine()
            System.Console.WriteLine()


            strQuery = "SELECT * FROM " & m_strAryTableNames(0)
            'System.Console.WriteLine(strQuery)

            'dbCommand.CommandText = strQuery
            dbCommand.Connection = oleDBConnection

            da = New OleDb.OleDbDataAdapter(dbCommand)
            'da = New OleDb.OleDbDataAdapter(strQuery, oleDBConnection)

            'todo HIGH resovle DataAdapter.MissingMappingAction problem
            ' Since the schema has already been read above, then I would
            ' expect this to work with the setting:
            '   da.MissingMappingAction = MissingMappingAction.Error
            ' but it doesn't for some reason. No idea why...!
            da.MissingMappingAction = MissingMappingAction.Passthrough
            da.MissingSchemaAction = MissingSchemaAction.Error
            da.FillLoadOption = LoadOption.OverwriteChanges

            For Each strTableName In m_strAryTableNames
                'dtm = New DataTableMapping
                'dtm.

                strQuery = "SELECT * FROM " & strTableName
                da.SelectCommand.CommandText = strQuery
                'ds.Tables.Add(strTableName)

                System.Console.WriteLine("strTableName : " & strTableName)
                'da.TableMappings.Add(strTableName, strTableName)
                'da.TableMappings.Add(DbDataAdapter.DefaultSourceTableName, strTableName)
                'For i As Short = 0 To da.TableMappings.Count - 1
                '    System.Console.WriteLine("srcTblmap : " & da.TableMappings.Item(i).SourceTable & _
                '                             "    dstTblmap : " & da.TableMappings.Item(i).DataSetTable)
                'Next
				
				                'Set up the mappings
                dtm = da.TableMappings.Add(strTableName, strTableName)

                For i As Integer = 0 To ds.Tables.Item(strTableName).Columns.Count - 1
                    dcl = ds.Tables.Item(strTableName).Columns.Item(i)
                    dtm.ColumnMappings.Add(dcl.ColumnName, dcl.ColumnName)
                Next
				
                da.Fill(ds, strTableName)

            Next


            ds.WriteXmlSchema("D:\MapAction\bronze\custom_tools\managedcode\mapaction-toolbox\" & _
                              "experimental\datanamingAPI\ma-namingAPI\CommandlineTesting\schema.xml")

            ds.WriteXml("D:\MapAction\bronze\custom_tools\managedcode\mapaction-toolbox\" & _
                        "experimental\datanamingAPI\ma-namingAPI\CommandlineTesting\output.xml")

        Catch ex As Exception
            System.Console.WriteLine("error occured")
            System.Console.WriteLine(ex.ToString())

        End Try

        'For Each strTableName In m_strAryTableNames

        '    strQuery = "SELECT * FROM " & strTableName

        '    dbCommand.CommandText = strQuery
        '    dbCommand.Connection = m_DBConnection

        '    dbCommand.

        '    dt = getTableFromReader(dbCommand.ExecuteReader())

        'Next

    End Sub

  
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


End Module
