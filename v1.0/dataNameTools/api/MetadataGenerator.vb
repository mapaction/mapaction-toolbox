Public Class MetadataGenerator

    Private m_dncl As IDataNameClauseLookup

    'tablename 'columnname


    Sub New()

    End Sub

    Private Function getStrValueFromDNCLtable(ByRef clauseTable As DataTable, ByVal strClause As String, ByVal strColumnName As String) As String
        'Eric()
        'LaCrefcenta()

        Dim row As DataRow
        Dim strResult As String

        row = getRowFromDNCLtable(clauseTable, strClause)
        strResult = row.Item(strColumnName).ToString

        Return strResult
    End Function

    Private Function getRowFromDNCLtable(ByRef clauseTable As DataTable, ByVal strClause As String) As DataRow
        Dim drAry As DataRow()
        Dim drReturn As DataRow

        drAry = clauseTable.Select("clause = " & strClause)

        If drAry.GetLength(0) <> 1 Then
            Throw New ArgumentException()
        Else
            drReturn = drAry(0)
        End If

        Return drReturn
    End Function


    Private Function generateValue() As String
        Dim strResult As String

        '{data_theme.description} & " - " & substitute (free_text, "_", " ").





        Return strResult

    End Function

End Class
