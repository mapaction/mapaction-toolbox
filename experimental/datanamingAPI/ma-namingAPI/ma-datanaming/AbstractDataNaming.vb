Public MustInherit Class AbstractDataName

    Public Const DATANAME_UNKNOWN_STATUS = 0
    Public Const DATANAME_VALID = -1

    Public Const DATANAME_ERROR = 2
    Public Const DATANAME_WARN = 4

    Public Const DATANAME_ERROR_TOO_FEW_CLAUSES = DATANAME_ERROR + (2 ^ 3)
    Public Const DATANAME_ERROR_INVALID_GEOEXTENT = DATANAME_ERROR + (2 ^ 4)
    Public Const DATANAME_ERROR_INVALID_DATACATEGORY = DATANAME_ERROR + (2 ^ 5)
    Public Const DATANAME_ERROR_INVALID_DATATHEME = DATANAME_ERROR + (2 ^ 6)
    Public Const DATANAME_ERROR_INVALID_DATATYPE = DATANAME_ERROR + (2 ^ 7)
    'Public Const DATANAME_ERROR_INCORRECT_DATATYPE = DATANAME_ERROR + (2^8)
    'Public Const DATANAME_ERROR_OTHER_ERROR = DATANAME_ERROR + (2^9)

    Public Const DATANAME_WARN_MISSING_SCALE_CLAUSE = DATANAME_WARN + (2 ^ 10)
    Public Const DATANAME_WARN_MISSING_PERMISSIONS_CLAUSE = DATANAME_WARN + (2 ^ 11)
    Public Const DATANAME_WARN_CONTAINS_HYPHENS = DATANAME_WARN + (2 ^ 12)
    'Public Const DATANAME_WARN

    Private dnLookup As IDataNameClauseLookup


    ' The general pattern of the naming convention is:
    '
    '  geoextent_datacategory_theme_datatype[_scale]_source[_permission][_FreeText]
    '
    Public Function IsValid(ByVal nameStr As String, ByVal myCon As IGeoDataListConnection) As Boolean
        IsValid = IsValid(AsArray(nameStr), myCon)
    End Function

    Public Function IsValid(ByVal nameParts As String(), ByVal myCon As IGeoDataListConnection) As Integer

        Dim returnResult As Integer
        Dim partsCnt As Integer

        returnResult = DATANAME_UNKNOWN_STATUS
        partsCnt = nameParts.Length


        'Check one
        'does at least five components?  5 <= nameParts
        '
        If partsCnt < 5 Then
            returnResult = returnResult + DATANAME_ERROR_TOO_FEW_CLAUSES
        Else
            'Check Two
            'Is the first clause a valid GeoExtent?
            If Not dnLookup.isvalidGeoextentClause(nameParts(1)) Then
                returnResult = returnResult + DATANAME_ERROR_INVALID_GEOEXTENT
            End If

            'Check Three
            'Are the secound and thrid clauses valid DataCategory and DataTheme?
            If Not dnLookup.isvalidDataThemeClause(nameParts(3), nameParts(2)) Then
                returnResult = returnResult + DATANAME_ERROR_INVALID_DATATHEME
                'Check Three.one
                If Not dnLookup.isvalidDataCategoryClause(nameParts(2)) Then
                    returnResult = returnResult + DATANAME_ERROR_INVALID_DATACATEGORY
                End If
            End If

            'Check Four
            'is the four clause a valid Data Type Clause?
            'NOTE THAT THIS DOES NOT TEST WHETHER THE DATA TYPE ACURATELY REFLECTS THE UNDERLYING DATA!
            If Not dnLookup.isvalidDataTypeClause(nameParts(3)) Then
                returnResult = returnResult + DATANAME_ERROR_INVALID_DATATYPE
            End If

            'Check Five
            'Is the fifth clause a scale clause or a source clause?
            If dnLookup.isvalidScaleClause(nameParts(4)) Then
                'fifth clause is scale clause, now test sixth clause for source
            Else
                'fifth clause isn't valid scale clause. Is this becuase the fifth clause is actually a source clause 
                'or because it is an invalid scale clause?
                If dnLookup.isvalidSourceClause(nameParts(4)) Then
                    returnResult = returnResult + DATANAME_WARN_MISSING_SCALE_CLAUSE
                Else
                    'returnResult = returnResult + dataname_error_
                End If
            End If


        End If


        IsValid = returnResult
    End Function

    Private Function AsArray(ByVal nameStr As String) As Array
        AsArray = Strings.Split(nameStr, "_")
    End Function

    Private Function hasScaleClause(ByVal nameStr As String) As Boolean
    End Function

    Private Function hasPermissionClause(ByVal nameStr As String) As Boolean
    End Function

    Private Function hasFreeText(ByVal nameStr As String) As Boolean
    End Function


    '
    '
    '
    Public Function changeGeoExtent(ByVal dataname As String, ByVal newGeoExtent As String) As String
        changeGeoExtent = Nothing
    End Function

    Public Function changeDataCategory(ByVal dataname As String, ByVal newDataCategory As String) As String
        changeDataCategory = Nothing
    End Function

    Public Function changeDataTheme(ByVal dataname As String, ByVal newDataTheme As String) As String
        changeDataTheme = Nothing
    End Function


    Public Sub New()
        'dnLookup = New DataNameCodeLookup
    End Sub
End Class
