
Public Class LookupTableException
    Inherits Exception

    Private m_enmErrorType As dnLookupTableError
    Private m_strSearchParam As String

    Protected Friend Sub New(ByVal enmErrorType As dnLookupTableError, ByVal strSearchParam As String)
        MyBase.New(createMessage(enmErrorType, strSearchParam))
        m_enmErrorType = enmErrorType
        m_strSearchParam = strSearchParam
    End Sub

    Protected Friend Sub New(ByVal enmErrorType As dnLookupTableError, ByVal strSearchParam As String, ByRef innerEx As Exception)
        MyBase.New(createMessage(enmErrorType, strSearchParam), innerEx)
        m_enmErrorType = enmErrorType
        m_strSearchParam = strSearchParam
    End Sub

    Private Shared Function createMessage(ByVal dnLTE As dnLookupTableError, ByVal strSearchParam As String) As String
        Dim strReturnVal As String = ""

        Select Case dnLTE
            Case dnLookupTableError.general
                strReturnVal = LOOKUP_TABLE_ERROR_GENERAL
            Case dnLookupTableError.wrongNoOfCols
                strReturnVal = LOOKUP_TABLE_ERROR_WRONG_NO_OF_COLS
            Case dnLookupTableError.DefaultTablesNotFound
                strReturnVal = LOOKUP_TABLE_ERROR_DEFAULT_TBLS_NOT_FOUND
            Case dnLookupTableError.colNamesMismatch
                strReturnVal = LOOKUP_TABLE_ERROR_COL_NAMES_MISMATCH
            Case dnLookupTableError.colTypeMismatch
                strReturnVal = LOOKUP_TABLE_ERROR_COL_TYPE_MISMATCH
            Case dnLookupTableError.colLenthMismatch
                strReturnVal = LOOKUP_TABLE_ERROR_COL_LENTH_MISMATCH
            Case dnLookupTableError.colUniqueReqMismatch
                strReturnVal = LOOKUP_TABLE_ERROR_COL_UNIQUEREQ_MISMATCH
            Case dnLookupTableError.colAutoIncrementMismatch
                strReturnVal = LOOKUP_TABLE_ERROR_COL_AUTOINCREMENT_MISMATCH
            Case dnLookupTableError.colCaptionMismatch
                strReturnVal = LOOKUP_TABLE_ERROR_COL_CAPTION_MISMATCH
            Case dnLookupTableError.colReadOnlyMismatch
                strReturnVal = LOOKUP_TABLE_ERROR_COL_READONLY_MISMATCH

        End Select

        If strSearchParam IsNot Nothing AndAlso strSearchParam <> "" Then
            strReturnVal = strReturnVal & " : " & strSearchParam
        End If

        Return strReturnVal
    End Function

    Private Function getSearchParam() As String
        Return m_strSearchParam
    End Function

    Private Function getEnmErrorType() As dnLookupTableError
        Return m_enmErrorType
    End Function
End Class
