Public Enum dnLookupTableError As Short
    general
    wrong_no_of_cols
    wrong_col_spec
    default_tbls_not_found
End Enum


Public Class LookupTableException
    Inherits Exception

    Private Const STR_LOOKUP_TABLE_ERROR_GENERAL = "Error whist reading data clause lookup table"
    Private Const STR_LOOKUP_TABLE_ERROR_WRONG_NO_OF_COLS = "Incorrect number of columns in table"
    Private Const STR_LOOKUP_TABLE_ERROR_WRONG_COL_SPEC = "Incorrect specification for column"
    Private Const STR_LOOKUP_TABLE_ERROR_DEFAULT_TBLS_NOT_FOUND = "Cannot find a valid default Data Name Clause Lookup Table"
    '"Unable to find valid DataName Clause Lookup Tables in directory: "

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

    Private Shared Function createMessage(ByVal dnLTE As dnLookupTableError, ByVal strSearchParam As String)
        Dim strReturnVal As String = ""

        Select Case dnLTE
            Case dnLookupTableError.general
                strReturnVal = STR_LOOKUP_TABLE_ERROR_GENERAL
            Case dnLookupTableError.wrong_no_of_cols
                strReturnVal = STR_LOOKUP_TABLE_ERROR_WRONG_NO_OF_COLS
            Case dnLookupTableError.wrong_col_spec
                strReturnVal = STR_LOOKUP_TABLE_ERROR_WRONG_COL_SPEC
            Case dnLookupTableError.default_tbls_not_found
                strReturnVal = STR_LOOKUP_TABLE_ERROR_DEFAULT_TBLS_NOT_FOUND
        End Select

        If strSearchParam IsNot Nothing AndAlso strSearchParam <> "" Then
            strReturnVal = strReturnVal & " : " & strSearchParam
        End If

        Return strReturnVal
    End Function

    Private Function getSearchParam()
        Return m_strSearchParam
    End Function

    Private Function getEnmErrorType()
        Return m_enmErrorType
    End Function
End Class
