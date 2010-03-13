Public MustInherit Class AbstractDataName
    Implements IDataName

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

    'Private dnLookup As IDataNameClauseLookup


    ' The general pattern of the naming convention is:
    '
    '  geoextent_datacategory_theme_datatype[_scale]_source[_permission][_FreeText]
    '
    '  geoextent

    '   datacategory    = clause #1
    '   theme           = clause #2
    '   datatype        = clause #3
    '   [scale]         = clause #4             
    '       scale can be
    '           'correct'       = therefore source is clause #5
    '           'missing'       = only assume this if clause #4 is valid scale clause
    '           'erroroneous'   = ???
    '
    '   source          = 4 or 5
    '       If scale is correct then search clause #5
    '           'correct'       = ok
    '           'erroroneous'   = 
    '       if scale is not correct then search clause #4
    '           'correct'       = warn that scale clause is missing
    '           'erroroneous'   = ???
    '       
    '       
    '       
    '   [permission]    = 5 or 6
    '   [FreeText]      = starting from 5 or 6 or 7 onwards

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="nameStr"></param>
    ''' <param name="myCon"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Function IsNameValid(ByVal nameStr As String, ByVal myCon As IGeoDataListConnection) As Boolean Implements IDataName.IsNameValid
        IsNameValid = IsNameValid(AsArray(nameStr), myCon)
    End Function

    Public Function IsNameValid() As Integer Implements IDataName.IsNameValid

    End Function

    Protected Function IsNameValid(ByVal nameParts As String(), ByVal dnLookup As IDataNameClauseLookup) As Integer

        Dim returnResult As Integer
        Dim partsCnt As Integer

        returnResult = DATANAME_UNKNOWN_STATUS
        partsCnt = nameParts.Length

        'Check Zero
        'does it contain hyphens "-" which probably should be underscorces "_"
        For Each namePart In nameParts
            If InStr(namePart, "-") =  Then
                returnResult = returnResult + DATANAME_WARN_CONTAINS_HYPHENS
            End If
        Next

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
            If Not dnLookup.isvalidDataTypeClause(nameParts(4)) Then
                returnResult = returnResult + DATANAME_ERROR_INVALID_DATATYPE
            End If

            'Check Five
            'Is the fifth clause a scale clause or a source clause?
            If dnLookup.isvalidScaleClause(nameParts(5)) Then
                'fifth clause is scale clause, now test sixth clause for source
            Else
                'fifth clause isn't valid scale clause. Is this becuase the fifth clause is actually a source clause 
                'or because it is an invalid scale clause?
                If dnLookup.isvalidSourceClause(nameParts(4)) Then
                    returnResult = returnResult + DATANAME_WARN_MISSING_SCALE_CLAUSE

                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    'HAVE CODED UP TO HERE 
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                Else
                    'Could be that the fifth clause is supposed to be a Scale Clause but is erroronious
                    'OR
                    'There isn't a Scale Clause and that this clause is supposed to be a Source Clause and is
                    'errorenious.

                    'returnResult = returnResult + dataname_error_

                End If
            End If
            '  geoextent_datacategory_theme_datatype[_scale]_source[_permission][_FreeText]

            
        End If


        IsNameValid = returnResult
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
