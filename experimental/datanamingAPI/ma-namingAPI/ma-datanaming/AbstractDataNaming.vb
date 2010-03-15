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
    '  #1        #2           #3    #4        #5     #6      #7           #8
    '
    '  no Scale clause
    '  geoextent_datacategory_theme_datatype_source[_permission][_FreeText]
    '  #1        #2           #3    #4       #5      #6           #7
    '
    '  no Permission clause
    '  geoextent_datacategory_theme_datatype[_scale]_source[_FreeText]
    '  #1        #2           #3    #4        #5     #6      #7
    '
    '  no Scale or permissions clause
    '  geoextent_datacategory_theme_datatype_source[_FreeText]
    '  #1        #2           #3    #4       #5      #6
    '
    '
    '   geoextent       ==> clause #1
    '   datacategory    ==> clause #2
    '   theme           ==> clause #3
    '   datatype        ==> clause #4
    '   [scale]         ==> clause #5             
    '       scale can be
    '           'correct'       = set scale_status_known = TRUE, therefore source is clause #6
    '           'missing'       = set scale_status_known = FALSE, only assume this if clause #7 is valid scale clause
    '           'erroroneous'   = set scale_status_known = FALSE
    '
    '   source          ==> #5 or #6
    '       if scale_status_known=TRUE then
    '           source clause should be #6
    '               'correct'       = ok
    '               'erroroneous'   = SCALE_CLAUSE_ERROR
    '       elseif scale_status_known=FALSE then
    '           check if clause #5 is a correct source clause
    '               'correct'       = ok, and therefore SCALE_CLAUSE_MISSING and  set scale_status_known = TRUE,
    '           check if clause #6 is a correct source clause
    '               'correct'       = ok, and therefore SCALE_CLAUSE_ERROR and  set scale_status_known = TRUE,
    '           if neither #5 nor #6 are valid sources and scale_status_known=FALSE then ??????
    '                SOURCE_CLAUSE_ERROR (still don't know whether scale is missing or erroroneous)
    '       
    '   [permission]    ==> #6 or #7
    '           Either there is a valid Source clause, or there is a valid explict Scale clause with or without a valid source clause
    '               Therefore we can determine what possition to expect the permission clause (either #6 or #7)
    '                   permission clause does not exist = MISSING_PERMISSION_CLAUSE_WARNING and permission_status_known = TRUE
    '                   permission clause exists and is valid and permission_status_known = TRUE
    '                       if #8 onwards exists then INFO_FREE_TEXT_INCLUDED
    '                   permission clause exists and is invalid - is it really a permission clause or is it the begining of free text?
    '                       if #6 or #7 (as appropriate) is two characters long then WARNING_FREE_TEXT_COULD_BE_MISFORMED_PERMISSIONS_CLAUSE + MISSING_PERMISSIONS_WARNING + INFO_FREE_TEXT_INCLUDED
    '                       if #6 or #7 (as appropriate) is longer than two characters then MISSING_PERMISSIONS_WARNING + INFO_FREE_TEXT_INCLUDED
    '
    '           if #5 is nieher a valid scale or source clause AND #6 is not a valid source clause, then we don't know whether to expect 
    '           the permission clause to be #6 or #7
    '               if #6 is valid permissions clause then SCALE_CLAUSE_MISSING (assume that #7 onwards are free text if they exist)
    '                   if #7 onwards exists then INFO_FREE_TEXT_INCLUDED
    '               if #6 is invalid and #7 is valid permissions clause then SCALE_CLAUSE_ERROR
    '                   if #8 onwards exists then INFO_FREE_TEXT_INCLUDED
    '               if #6 is invalid and #7 does not exist or is invalid then is it really a permission clause or is it the begining of free text?
    '                   if #6 is two characters long then WARNING_FREE_TEXT_COULD_BE_MISFORMED_PERMISSIONS_CLAUSE + MISSING_PERMISSIONS_WARNING + INFO_FREE_TEXT_INCLUDED
    '                   if #6 is longer than two characters then MISSING_PERMISSIONS_WARNING + INFO_FREE_TEXT_INCLUDED
    
    '
    '   [FreeText]      ==> starting from #6, #7 or #8 onwards
    '       Already determined previous check
    '
    '
    '
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

    Protected Function IsNameValid(ByVal nameParts As String(), ByVal dnLookup As IDataNameClauseLookup) As Integer Implements IDataName.IsNameValid

        Dim returnResult As Integer
        Dim partsCnt As Integer

        returnResult = DATANAME_UNKNOWN_STATUS
        partsCnt = nameParts.Length

        'Check Zero
        'does it contain hyphens "-" which probably should be underscorces "_"
        For Each namePart In nameParts
            If InStr(namePart, "-") <> 0 Then
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

    Public MustOverride Function isEditable() As Boolean Implements IDataName.isEditable

    Public Function hasOptionalScaleClause() As Boolean Implements IDataName.hasOptionalScaleClause
    End Function

    Public Function hasOptionalPermissionClause() As Boolean Implements IDataName.hasOptionalPermissionClause
    End Function

    Public Function hasOptionalFreeText() As Boolean Implements IDataName.hasOptionalFreeText
    End Function


    '
    '
    '
    Public Function changeGeoExtentClause(ByVal newGeoExtent As String) As Integer Implements IDataName.changeGeoExtentClause
        changeGeoExtentClause = Nothing
    End Function

    Public Function changeDataCategoryClause(ByVal newDataCategory As String) As Integer Implements IDataName.changeDataCategoryClause
        changeDataCategoryClause = Nothing
    End Function

    Public Function changeDataThemeClause(ByVal newDataTheme As String) As Integer Implements IDataName.changeDataThemeClause
        changeDataThemeClause = Nothing
    End Function

    Public Function changeDataTypeClause(ByVal newDataType As String) As Integer Implements IDataName.changeDataTypeClause
        changeDataTypeClause = Nothing
    End Function


    Function changeScaleCodesClause(ByVal newDataTheme As String) As Integer Implements IDataName.changeScaleCodesClause
        changeScaleCodesClause = Nothing
    End Function

    Function changeSourceCodesClause(ByVal newDataTheme As String) As Integer Implements IDataName.changeSourceCodesClause
        changeSourceCodesClause = Nothing
    End Function

    Function changePermissionsClause(ByVal newDataTheme As String) As Integer Implements IDataName.changePermissionsClause
        changePermissionsClause = Nothing
    End Function

    Function changeFreeTextClause(ByVal newDataTheme As String) As Integer Implements IDataName.changeFreeTextClause
        changeFreeTextClause = Nothing
    End Function


    Public Sub New()
        'dnLookup = New DataNameCodeLookup
    End Sub
End Class
