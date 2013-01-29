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

Public Class LookupTableException
    Inherits Exception

    Private m_enmErrorType As dnLookupTableError
    Private m_strSearchParam As String

    Protected Friend Sub New(ByVal enmErrorType As dnLookupTableError, ByVal strSearchParam As String)
        MyBase.New(createMessage(enmErrorType, strSearchParam))
        m_enmErrorType = enmErrorType
        m_strSearchParam = strSearchParam
    End Sub

    Protected Friend Sub New(ByVal enmErrorType As dnLookupTableError, ByVal strSearchParam As String, ByVal innerEx As Exception)
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
