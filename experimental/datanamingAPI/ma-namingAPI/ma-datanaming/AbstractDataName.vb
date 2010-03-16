Public MustInherit Class AbstractDataName
    Implements IDataName

    Protected Friend myNameStr As String
    Protected Friend myDNCL As IDataNameClauseLookup

    

    Public Function getNameStr() As String Implements IDataName.getNameStr
        getNameStr = myNameStr
    End Function

    Public MustOverride Function getPathStr() As String Implements IDataName.getPathStr

    Public Function getNameAndFullPathStr() As String Implements IDataName.getNameAndFullPathStr
        getNameAndFullPathStr = getPathStr() & "\" & getNameStr()
    End Function

    
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsNameValid() As Integer Implements IDataName.IsNameValid
        IsNameValid = IsNameValid(myNameStr)
    End Function

    Private Function IsNameValid(ByVal myStr As String) As Integer
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
        '
        '   [FreeText]      ==> starting from #6, #7 or #8 onwards
        '       Already determined previous checks
        '
        Dim returnResult As Integer
        Dim partsCnt As Integer
        Dim nameParts As String()

        Dim geoextentIdx As Integer
        Dim dataCategoryIdx As Integer
        Dim dataThemeIdx As Integer
        Dim dataTypeIdx As Integer
        Dim scaleIdx As Integer
        Dim sourceIdx As Integer
        Dim permissionIdx As Integer
        Dim freeTextIdx As Integer

        '  +ve number is the index in the array
        '  Zero mean the position is unknown
        '  -ve number means that the option clause is missing
        '  Those that are not fixed index (due to the persence or absence of optional clauses are not 
        '  initialised as late as possible in the code. This is so that the compiler will catch any
        '  routes by which they can't initialised.
        geoextentIdx = 1
        dataCategoryIdx = 2
        dataThemeIdx = 3
        dataTypeIdx = 4
        scaleIdx = 5
        'sourceIdx
        'permissionIdx
        'freeTextIdx

        Dim scale_status_known = False
        Dim permissions_status_known = False

        returnResult = DATANAME_UNKNOWN_STATUS

        'Check Zero
        'does it contain hyphens "-" which probably should be underscorces "_"
        If InStr(myStr, "-") <> 0 Then
            returnResult = returnResult Or DATANAME_ERROR_CONTAINS_HYPHENS
            returnResult = returnResult Or IsNameValid(myStr.Replace("-", "_"))
        End If

        nameParts = Strings.Split(myStr, "_")
        partsCnt = nameParts.Length

        'Check one
        'does at least five components?  5 <= nameParts
        If partsCnt < 5 Then
            returnResult = returnResult Or DATANAME_ERROR_TOO_FEW_CLAUSES
        Else
            'Check Two
            'Is the first clause a valid GeoExtent?
            If Not myDNCL.isvalidGeoextentClause(nameParts(geoextentIdx)) Then
                returnResult = returnResult Or DATANAME_ERROR_INVALID_GEOEXTENT
            End If

            'Check Three
            'Are the secound and thrid clauses valid DataCategory and DataTheme?
            If Not myDNCL.isvalidDataThemeClause(nameParts(dataThemeIdx), nameParts(dataCategoryIdx)) Then
                returnResult = returnResult Or DATANAME_ERROR_INVALID_DATATHEME
                'Check Three.one
                If Not myDNCL.isvalidDataCategoryClause(nameParts(dataCategoryIdx)) Then
                    returnResult = returnResult Or DATANAME_ERROR_INVALID_DATACATEGORY
                End If
            End If

            'Check Four
            'is the four clause a valid Data Type Clause?
            'NOTE THAT THIS DOES NOT TEST WHETHER THE DATA TYPE ACURATELY REFLECTS THE UNDERLYING DATA!
            If Not myDNCL.isvalidDataTypeClause(nameParts(dataTypeIdx)) Then
                returnResult = returnResult Or DATANAME_ERROR_INVALID_DATATYPE
            End If

            'Check Five
            'Is the fifth clause a scale clause or a source clause?
            If myDNCL.isvalidScaleClause(nameParts(scaleIdx)) Then
                'fifth clause is scale clause, now test sixth clause for source
                sourceIdx = 6
                permissionIdx = 7
                scale_status_known = True

                If Not myDNCL.isvalidSourceClause(nameParts(sourceIdx)) Then
                    returnResult = returnResult Or DATANAME_ERROR_INCORRECT_SOURCE
                End If
            Else
                scale_status_known = False
                'Else
                'fifth clause isn't valid scale clause. Is this becuase the fifth clause is actually a source clause 
                'or because it is an invalid scale clause?
                'delibartilay passing the scaleInx to isValidSourceClause
                If myDNCL.isvalidSourceClause(nameParts(scaleIdx)) Then
                    'fifth clause is a valid source clause therefore there is no scale clause
                    returnResult = returnResult Or DATANAME_WARN_MISSING_SCALE_CLAUSE
                    scaleIdx = -1
                    sourceIdx = 5
                    permissionIdx = 6
                Else
                    'Could be that the fifth clause is supposed to be a Scale Clause but is erroronious
                    'OR
                    'There isn't a Scale Clause and that this clause is supposed to be a Source Clause and is
                    'errorenious.

                    'returnResult = returnResult and dataname_error_
                    If nameParts.Length > 5 Then
                        If myDNCL.isvalidSourceClause(nameParts(6)) Then
                            scale_status_known = True
                            scaleIdx = 5
                            sourceIdx = 6
                            returnResult = returnResult Or DATANAME_ERROR_INCORRECT_SCALE
                        Else
                            scale_status_known = False
                            returnResult = returnResult Or DATANAME_ERROR_INCORRECT_SOURCE
                        End If
                    Else
                        returnResult = returnResult Or DATANAME_WARN_MISSING_SCALE_CLAUSE
                        returnResult = returnResult Or DATANAME_WARN_MISSING_PERMISSIONS_CLAUSE
                    End If
                End If
            End If

            'check combination of Permission clause and Free Text
            If (Not (returnResult And DATANAME_ERROR_INCORRECT_SOURCE) = DATANAME_ERROR_INCORRECT_SOURCE) Or _
                (scale_status_known And Not (returnResult And DATANAME_WARN_MISSING_SCALE_CLAUSE) = DATANAME_WARN_MISSING_SCALE_CLAUSE) Then
                If Not nameParts.Length > sourceIdx Then
                    permissions_status_known = True
                    returnResult = returnResult Or DATANAME_WARN_MISSING_PERMISSIONS_CLAUSE
                Else
                    permissionIdx = sourceIdx + 1
                    If myDNCL.isvalidPermissionsClause(nameParts(permissionIdx)) Then
                        permissions_status_known = True
                        If nameParts.Length > permissionIdx Then
                            returnResult = returnResult Or DATANAME_INFO_FREE_TEXT_PRESENT
                        End If
                    Else
                        returnResult = returnResult Or DATANAME_INFO_FREE_TEXT_PRESENT Or DATANAME_WARN_MISSING_PERMISSIONS_CLAUSE
                        If nameParts(permissionIdx).Length = 2 Then
                            returnResult = returnResult Or DATANAME_WARN_TWO_CHAR_FREE_TEXT
                        End If

                    End If
                End If
            Else
                ''final details to go in here
                If nameParts.Length > 5 AndAlso myDNCL.isvalidPermissionsClause(nameParts(6)) Then
                    scale_status_known = True
                    returnResult = returnResult Or DATANAME_WARN_MISSING_SCALE_CLAUSE
                    If nameParts.Length > 6 Then
                        returnResult = returnResult Or DATANAME_INFO_FREE_TEXT_PRESENT
                    End If
                ElseIf nameParts.Length > 6 AndAlso myDNCL.isvalidPermissionsClause(nameParts(7)) Then
                    scale_status_known = True
                    returnResult = returnResult Or DATANAME_ERROR_INCORRECT_SCALE
                    If nameParts.Length > 7 Then
                        returnResult = returnResult Or DATANAME_INFO_FREE_TEXT_PRESENT
                    End If
                Else
                    returnResult = returnResult Or DATANAME_INFO_FREE_TEXT_PRESENT Or DATANAME_WARN_MISSING_PERMISSIONS_CLAUSE
                    If nameParts(permissionIdx).Length = 2 Then
                        returnResult = returnResult Or DATANAME_WARN_TWO_CHAR_FREE_TEXT
                    End If

                End If
            End If

        End If

        IsNameValid = returnResult
    End Function

    Public MustOverride Function isEditable() As Boolean Implements IDataName.isEditable

    Public Function hasOptionalScaleClause() As Boolean Implements IDataName.hasOptionalScaleClause
        Dim bitSum As Integer
        Dim myResult As Boolean

        bitSum = IsNameValid()

        myResult = (Not (bitSum And DATANAME_ERROR) = DATANAME_ERROR) And _
           (Not (bitSum And DATANAME_WARN_MISSING_SCALE_CLAUSE) = DATANAME_WARN_MISSING_SCALE_CLAUSE)

        hasOptionalScaleClause = myResult
    End Function

    Public Function hasOptionalPermissionClause() As Boolean Implements IDataName.hasOptionalPermissionClause
        Dim bitSum As Integer
        Dim myResult As Boolean

        bitSum = IsNameValid()

        myResult = (Not (bitSum And DATANAME_ERROR) = DATANAME_ERROR) And _
           (Not (bitSum And DATANAME_WARN_MISSING_PERMISSIONS_CLAUSE) = DATANAME_WARN_MISSING_PERMISSIONS_CLAUSE)

        hasOptionalPermissionClause = myResult
    End Function

    Public Function hasOptionalFreeText() As Boolean Implements IDataName.hasOptionalFreeText
        Dim bitSum As Integer
        Dim myResult As Boolean

        bitSum = IsNameValid()

        myResult = (Not (bitSum And DATANAME_ERROR) = DATANAME_ERROR) And _
           ((bitSum And DATANAME_INFO_FREE_TEXT_PRESENT) = DATANAME_INFO_FREE_TEXT_PRESENT)

        hasOptionalFreeText = myResult
    End Function


    '
    '
    '
    Public Function changeGeoExtentClause(ByVal newGeoExtent As String) As Integer Implements IDataName.changeGeoExtentClause
        'changeGeoExtentClause = Nothing
    End Function

    Public Function changeDataCategoryClause(ByVal newDataCategory As String) As Integer Implements IDataName.changeDataCategoryClause
        'changeDataCategoryClause = Nothing
    End Function

    Public Function changeDataThemeClause(ByVal newDataTheme As String) As Integer Implements IDataName.changeDataThemeClause
        'changeDataThemeClause = Nothing
    End Function

    Public Function changeDataTypeClause(ByVal newDataType As String) As Integer Implements IDataName.changeDataTypeClause
        'changeDataTypeClause = Nothing
    End Function

    Function changeScaleCodesClause(ByVal newDataTheme As String) As Integer Implements IDataName.changeScaleCodesClause
        'changeScaleCodesClause = Nothing
    End Function

    Function changeSourceCodesClause(ByVal newDataTheme As String) As Integer Implements IDataName.changeSourceCodesClause
        'changeSourceCodesClause = Nothing
    End Function

    Function changePermissionsClause(ByVal newDataTheme As String) As Integer Implements IDataName.changePermissionsClause
        'changePermissionsClause = Nothing
    End Function

    Function changeFreeTextClause(ByVal newDataTheme As String) As Integer Implements IDataName.changeFreeTextClause
        'changeFreeTextClause = Nothing
    End Function


    Public Sub New(ByVal theName As String, ByRef theDNCL As IDataNameClauseLookup)
        myNameStr = theName
        myDNCL = theDNCL
    End Sub
End Class
