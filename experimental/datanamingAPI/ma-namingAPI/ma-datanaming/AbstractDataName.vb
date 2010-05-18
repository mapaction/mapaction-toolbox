Public MustInherit Class AbstractDataName
    Implements IDataName

    Protected Friend myNameStr As String
    Protected Friend myDNCL As IDataNameClauseLookup
    Private cacheIsUptoDate As Boolean = False
    Private cachedBitSum As Long
    Protected Friend allowReNaming As Boolean

    Protected Friend Sub New(ByVal theName As String, ByRef theDNCL As IDataNameClauseLookup, ByVal allowReNames As Boolean)
        myNameStr = theName
        myDNCL = theDNCL
        allowReNaming = allowReNames
    End Sub

    Public Function getNameStr() As String Implements IDataName.getNameStr
        getNameStr = myNameStr
    End Function

    ''' <summary>
    ''' Does not include trailing slash or backslash
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public MustOverride Function getPathStr() As String Implements IDataName.getPathStr

    ''' <summary>
    ''' This is used to check that the data type clause matches actually physical data type.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Friend MustOverride Function getUnderlyingDataType() As String

    Public Function getNameAndFullPathStr() As String Implements IDataName.getNameAndFullPathStr
        getNameAndFullPathStr = getPathStr() & "\" & getNameStr()
    End Function

    'todo LOW: rewrite this summary!
    ''' <summary>
    ''' Rewrite this!!!!
    '''  
    ''' If this is true then any methods which requires a valid name should succed. If this
    ''' returns false then any methods which requires a valid name will throw an InvalidDataNameException.
    ''' </summary>
    ''' <returns>Returns a runtime value that that the current DataName is or isn't valid according to both syntax 
    ''' and the current values in the associated DataClauseLookupTable.
    ''' current values</returns>
    ''' <remarks></remarks>
    Public Function checkNameStatus() As Long Implements IDataName.checkNameStatus
        Dim returnVal As Long
        If cacheIsUptoDate Then
            returnVal = cachedBitSum
        Else
            'todo LOW: A DataName should really listen for changes in the DataNameLookupTables
            returnVal = nameSyntaxStatus(myNameStr)
            cachedBitSum = returnVal
            cacheIsUptoDate = True
        End If

        Return returnVal
    End Function

    Public Function isNameParseable() As Boolean Implements IDataName.isNameParseable
        Dim bitSum As Long
        Dim myResult As Boolean

        bitSum = checkNameStatus()
        myResult = (Not (bitSum And dnNameStatus.SYNTAX_ERROR) = dnNameStatus.SYNTAX_ERROR)

        Return myResult
    End Function

    Public Function isNameValid() As Boolean Implements IDataName.isNameValid
        Dim bitSum As Long
        Dim myResult As Boolean

        bitSum = checkNameStatus()
        myResult = (Not (bitSum And dnNameStatus.SYNTAX_ERROR) = dnNameStatus.SYNTAX_ERROR) And _
                   (Not (bitSum And dnNameStatus.INVALID) = dnNameStatus.INVALID)

        Return myResult
    End Function


    ''' <summary>
    ''' 
    ''' If true then attempting to calling one of the methods to rename the
    ''' DataName should not throw an expeption. If false then attempting to call one of the method
    ''' will raise a RenamingDataException.
    ''' </summary>
    ''' <returns>Boolean. Returns a runtime value signifying whether or not the underlying dataname
    ''' is both renaming (physically) and that the relevant permissions are held.
    ''' </returns>
    ''' <remarks></remarks>
    Public Function isRenameable() As Boolean Implements IDataName.isRenameable
        Return (allowReNaming And renamePossible())
    End Function

    Protected Friend MustOverride Function renamePossible() As Boolean

    Protected Friend Sub resetCacheFlags()
        cacheIsUptoDate = False
        cachedBitSum = dnNameStatus.UNKNOWN_STATUS
    End Sub

    Public Function hasOptionalScaleClause() As Boolean Implements IDataName.hasOptionalScaleClause
        Dim myResult As Boolean
        Dim bitsum As Long

        bitsum = checkNameStatus()

        myResult = isNameParseable() And _
           (Not (bitsum And dnNameStatus.WARN_MISSING_PERMISSIONS_CLAUSE) = dnNameStatus.WARN_MISSING_PERMISSIONS_CLAUSE)

        hasOptionalScaleClause = myResult
    End Function

    Public Function hasOptionalPermissionClause() As Boolean Implements IDataName.hasOptionalPermissionClause
        Dim blnResult As Boolean
        Dim bitsum As Long

        bitsum = checkNameStatus()

        blnResult = isNameParseable() And _
           (Not (bitsum And dnNameStatus.WARN_MISSING_PERMISSIONS_CLAUSE) = dnNameStatus.WARN_MISSING_PERMISSIONS_CLAUSE)

        hasOptionalPermissionClause = blnResult
    End Function

    Public Function hasOptionalFreeText() As Boolean Implements IDataName.hasOptionalFreeText
        Dim myResult As Boolean
        Dim bitsum As Long

        bitsum = checkNameStatus()

        myResult = isNameParseable() And _
           ((bitsum And dnNameStatus.INFO_FREE_TEXT_PRESENT) = dnNameStatus.INFO_FREE_TEXT_PRESENT)

        hasOptionalFreeText = myResult
    End Function

    Public Sub rename(ByVal newNameStr As String)
        resetCacheFlags()
        performRename(newNameStr)
    End Sub

    Public MustOverride Sub performRename(ByVal newNameStr As String)

    Public Function changeGeoExtentClause(ByVal newGeoExtent As String) As Integer Implements IDataName.changeGeoExtentClause
        'changeGeoExtentClause = Nothing
        rename(generateNameWithReplacedClause(newGeoExtent, CLAUSE_GEOEXTENT))
    End Function

    Public Function changeDataCategoryClause(ByVal newDataCategory As String) As Integer Implements IDataName.changeDataCategoryClause
        'changeDataCategoryClause = Nothing
        rename(generateNameWithReplacedClause(newDataCategory, CLAUSE_DATACATEGORY))
    End Function

    Public Function changeDataThemeClause(ByVal newDataTheme As String) As Integer Implements IDataName.changeDataThemeClause
        'changeDataThemeClause = Nothing
        rename(generateNameWithReplacedClause(newDataTheme, CLAUSE_DATATHEME))
    End Function

    Public Function changeDataTypeClause(ByVal newDataType As String) As Integer Implements IDataName.changeDataTypeClause
        'changeDataTypeClause = Nothing
        rename(generateNameWithReplacedClause(newDataType, CLAUSE_DATATYPE))
    End Function

    Function changeScaleCodesClause(ByVal newScale As String) As Integer Implements IDataName.changeScaleCodesClause
        'changeScaleCodesClause = Nothing
        rename(generateNameWithReplacedClause(newScale, CLAUSE_SCALE))
    End Function

    Function changeSourceCodesClause(ByVal newSource As String) As Integer Implements IDataName.changeSourceCodesClause
        'changeSourceCodesClause = Nothing
        rename(generateNameWithReplacedClause(newSource, CLAUSE_SOURCE))

    End Function

    Function changePermissionsClause(ByVal newPermissions As String) As Integer Implements IDataName.changePermissionsClause
        'changePermissionsClause = Nothing
        rename(generateNameWithReplacedClause(newPermissions, CLAUSE_PERMISSIONS))
    End Function

    Function changeFreeTextClause(ByVal newFreeText As String) As Integer Implements IDataName.changeFreeTextClause
        'changeFreeTextClause = Nothing
        rename(generateNameWithReplacedClause(newFreeText, CLAUSE_FREETEXT))
    End Function

    ''' <summary>
    ''' Does NOT alter the underlying DataName - for that use the rename function. This function is a helper function to help prepare
    ''' the arguments for the rename function.
    ''' If the clauseName is for a optional clause that is currently not included it will be inserted into the string.
    ''' If the clauseName is for a clause that is already present then the old clause will be replaced.
    ''' </summary>
    ''' <param name="newClauseValue"></param>
    ''' <param name="clauseName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function generateNameWithReplacedClause(ByVal newClauseValue As String, ByVal clauseName As String) As String
        Dim returnVal As String = Nothing
        Dim nameParts() As String
        Dim clauseIdx As Short

        If Not isNameParseable() Then
            Throw New ErroreousDataNameException(checkNameStatus())
        Else
            nameParts = Strings.Split(myNameStr, "_")

            clauseIdx = getClauseIndex(clauseName)
            If clauseIdx <> -1 Then
                'Name clause exists, replace it
                nameParts(clauseIdx) = newClauseValue
            Else
                'Optional Name clause does not exists, insert it
                Dim tempNameParts(nameParts.Length + 1) As String
                Select Case clauseName
                    '  geoextent_datacategory_theme_datatype[_scale]_source[_permission][_FreeText]
                    '  #0        #1           #2    #3       #4     #5      #6           #7
                    Case CLAUSE_SCALE
                        clauseIdx = 4
                        
                    Case CLAUSE_PERMISSIONS
                        If hasOptionalScaleClause() Then
                            clauseIdx = 6
                        Else
                            clauseIdx = 5
                        End If

                    Case CLAUSE_FREETEXT
                        clauseIdx = CShort(nameParts.Length)
                        
                End Select

                Array.Copy(nameParts, 0, tempNameParts, 0, clauseIdx)
                tempNameParts(clauseIdx) = newClauseValue
                Array.Copy(nameParts, clauseIdx, tempNameParts, (clauseIdx + 1), nameParts.Length - clauseIdx)

                nameParts = tempNameParts

            End If

            'now concatenate it all together
            For Each newNamePart In nameParts
                If returnVal Is Nothing Then
                    returnVal = newNamePart
                Else
                    returnVal = returnVal & "_" & newNamePart
                End If
            Next

        End If

        Return returnVal
    End Function

    ''' <summary>
    ''' Reutrn as ZERO based index of the named clause. Will throw an InvalidDataNameException if 
    ''' the bitSum refers to an invalid name.
    ''' </summary>
    ''' <param name="clauseName"></param>
    ''' <returns>If optional cluase is no present, the value "-1" will be returned.</returns>
    ''' <remarks></remarks>
    Private Function getClauseIndex(ByVal clauseName As String) As Short
        Dim returnIdx As Short

        If Not isNameParseable() Then
            Throw New ErroreousDataNameException(checkNameStatus())
        Else

            Select Case clauseName
                Case CLAUSE_GEOEXTENT
                    returnIdx = 0
                Case CLAUSE_DATACATEGORY
                    returnIdx = 1
                Case CLAUSE_DATATHEME
                    returnIdx = 2
                Case CLAUSE_DATATYPE
                    returnIdx = 3
                Case CLAUSE_SCALE
                    If hasOptionalScaleClause() Then
                        returnIdx = 4
                    Else
                        returnIdx = -1
                    End If
                Case CLAUSE_SOURCE
                    If hasOptionalScaleClause() Then
                        returnIdx = 5
                    Else
                        returnIdx = 4
                    End If
                Case CLAUSE_PERMISSIONS
                    If hasOptionalPermissionClause() Then
                        If hasOptionalScaleClause() Then
                            returnIdx = 6
                        Else
                            returnIdx = 5
                        End If
                    Else
                        returnIdx = -1
                    End If
                Case CLAUSE_FREETEXT
                    If hasOptionalFreeText() Then
                        returnIdx = 5
                        If hasOptionalPermissionClause() Then
                            returnIdx = CShort(returnIdx + 1)
                        End If
                        If hasOptionalScaleClause() Then
                            returnIdx = CShort(returnIdx + 1)
                        End If
                    Else
                        returnIdx = -1
                    End If

            End Select
        End If
        Return returnIdx
    End Function


    Private Function nameSyntaxStatus(ByVal myStr As String) As Long
        Dim returnResult As Long
        Dim nameParts As String()

        returnResult = dnNameStatus.UNKNOWN_STATUS

        'Check Zero
        'does it contain hyphens "-" which probably should be underscorces "_"
        If InStr(myStr, "-") <> 0 Then
            returnResult = returnResult Or dnNameStatus.SYNTAX_ERROR_CONTAINS_HYPHENS Or nameSyntaxStatus(myStr.Replace("-", "_"))
        ElseIf InStr(myStr, "__") <> 0 Then
            returnResult = returnResult Or dnNameStatus.SYNTAX_ERROR_CONTAINS_HYPHENS Or nameSyntaxStatus(myStr.Replace("__", "_"))
        Else
            'Check one
            'does at least five components?  5 <= nameParts
            nameParts = Strings.Split(myStr, "_")

            If nameParts.Length < 5 Then
                returnResult = returnResult Or dnNameStatus.SYNTAX_ERROR_TOO_FEW_CLAUSES
            Else
                'Now check the actual validity of the individual clauses
                returnResult = returnResult Or nameValidityStatus(nameParts)
            End If
        End If

        Return returnResult

    End Function


    Private Function nameValidityStatus(ByVal curNameParts() As String, Optional ByVal clauseName As String = CLAUSE_GEOEXTENT) As Long
        Dim returnResult As Long, tempResult As Long
        Dim nextNameParts(curNameParts.Length - 1) As String

        '  geoextent_datacategory_theme_datatype[_scale]_source[_permission][_FreeText]
        '  #1        #2           #3    #4        #5     #6      #7           #8
        'system.console.WriteLine("nameValidityStatus: ")

        Select Case clauseName
            Case CLAUSE_GEOEXTENT
                If curNameParts(0) Is Nothing OrElse curNameParts.Length < 1 Then
                    returnResult = returnResult Or dnNameStatus.SYNTAX_ERROR_TOO_FEW_CLAUSES Or dnNameStatus.INVALID_GEOEXTENT
                Else
                    'system.console.WriteLine("CLAUSE_GEOEXTENT: " & curNameParts(0))

                    If Not myDNCL.isvalidGeoextentClause(curNameParts(0)) Then
                        returnResult = returnResult Or dnNameStatus.INVALID_GEOEXTENT
                    End If
                    Array.Copy(curNameParts, 1, nextNameParts, 0, (curNameParts.Length - 1))
                    returnResult = returnResult Or nameValidityStatus(nextNameParts, CLAUSE_DATACATEGORY)
                End If

            Case CLAUSE_DATACATEGORY
                'Since the DataCategory and DataTheme are nested it makes sence to test the two clauses together.
                'This Select Case is present for consistancy, so that this function proceedes parsing the DataName
                'in the order from left to right.
                If curNameParts(0) Is Nothing OrElse curNameParts.Length < 1 Then
                    returnResult = returnResult Or dnNameStatus.SYNTAX_ERROR_TOO_FEW_CLAUSES Or dnNameStatus.INVALID_DATACATEGORY
                Else
                    'system.console.WriteLine("CLAUSE_DATACATEGORY: " & curNameParts(0))

                    'Note that we deliberately pass all of the currentNameParts without removing the left end clause.
                    returnResult = returnResult Or nameValidityStatus(curNameParts, CLAUSE_DATATHEME)
                End If

            Case CLAUSE_DATATHEME
                'We need a minimum of two clauses here and then must truncate by two clauses before moving on to the
                'next clause
                If curNameParts(0) Is Nothing OrElse curNameParts.Length < 2 OrElse curNameParts(1) Is Nothing Then
                    returnResult = returnResult Or dnNameStatus.SYNTAX_ERROR_TOO_FEW_CLAUSES Or dnNameStatus.INVALID_DATATHEME
                Else
                    'system.console.WriteLine("CLAUSE_DATATHEME: " & curNameParts(0))

                    'Are the next two clauses valid DataCategory and DataTheme respectively?
                    If Not myDNCL.isvalidDataThemeClause(curNameParts(1), curNameParts(0)) Then
                        returnResult = returnResult Or dnNameStatus.INVALID_DATATHEME
                        'Check Three.one
                        If Not myDNCL.isvalidDataCategoryClause(curNameParts(1)) Then
                            returnResult = returnResult Or dnNameStatus.INVALID_DATACATEGORY
                        End If
                    End If
                    'go ahead a check out results from the rest of the string array
                    'Note that we truncate the first TWO left end clauses in this case
                    Array.Copy(curNameParts, 2, nextNameParts, 0, (curNameParts.Length - 2))
                    returnResult = returnResult Or nameValidityStatus(nextNameParts, CLAUSE_DATATYPE)
                End If

            Case CLAUSE_DATATYPE
                'NOTE THAT THIS DOES NOT TEST WHETHER THE DATA TYPE ACURATELY REFLECTS THE UNDERLYING DATA!
                If curNameParts(0) Is Nothing OrElse curNameParts.Length < 1 Then
                    returnResult = returnResult Or dnNameStatus.SYNTAX_ERROR_TOO_FEW_CLAUSES Or dnNameStatus.INVALID_DATATYPE
                Else
                    'system.console.WriteLine("CLAUSE_DATATYPE: " & curNameParts(0))

                    If Not myDNCL.isvalidDataTypeClause(curNameParts(0)) Then
                        'system.console.WriteLine("Not myDNCL.isvalidDataTypeClause(curNameParts(0))")
                        returnResult = returnResult Or dnNameStatus.INVALID_DATATYPE
                    End If

                    'system.console.WriteLine(" getUnderlyingDataType() " & getUnderlyingDataType() & "  curNameParts(0)  " & curNameParts(0))
                    If Not curNameParts(0).Equals(getUnderlyingDataType()) Then
                        'system.console.WriteLine("Not curNameParts(0).Equals(getUnderlyingDataType()")
                        returnResult = returnResult Or dnNameStatus.INCORRECT_DATATYPE
                    End If

                    Array.Copy(curNameParts, 1, nextNameParts, 0, (curNameParts.Length - 1))
                    returnResult = returnResult Or nameValidityStatus(nextNameParts, CLAUSE_SCALE)
                End If

            Case CLAUSE_SCALE
                If curNameParts(0) Is Nothing OrElse curNameParts.Length < 1 Then
                    'we've got to the end without finding a scale clause which means we have problems!
                    returnResult = returnResult Or dnNameStatus.INVALID_SOURCE Or dnNameStatus.WARN_MISSING_SCALE_CLAUSE
                Else
                    'system.console.WriteLine("CLAUSE_SCALE: " & curNameParts(0))

                    'go ahead a check out results from the rest of the string array
                    Array.Copy(curNameParts, 1, nextNameParts, 0, (curNameParts.Length - 1))
                    tempResult = nameValidityStatus(nextNameParts, CLAUSE_SOURCE)

                    If myDNCL.isvalidScaleClause(curNameParts(0)) Then
                        'Scale is present which is good, move on to the next thing - source
                        returnResult = returnResult Or tempResult
                    Else
                        If (tempResult And dnNameStatus.INVALID_SOURCE) = dnNameStatus.INVALID_SOURCE Then
                            'the next clause isn't a source clause so assume that this one is the
                            'source clase and that there is no scale clause
                            returnResult = returnResult Or dnNameStatus.WARN_MISSING_SCALE_CLAUSE
                            returnResult = returnResult Or nameValidityStatus(curNameParts, CLAUSE_SOURCE)
                        Else
                            'assume that the next clause is the source clause so join that into the result
                            returnResult = returnResult Or tempResult
                        End If
                    End If
                End If

            Case CLAUSE_SOURCE
                If curNameParts(0) Is Nothing OrElse curNameParts.Length < 1 Then
                    'we've got to the end without finding a source clause which means we have problems!
                    returnResult = returnResult Or dnNameStatus.INVALID_SOURCE
                Else
                    'system.console.WriteLine("CLAUSE_SOURCE: " & curNameParts(0))

                    If Not myDNCL.isvalidSourceClause(curNameParts(0)) Then
                        'Either the source clause is incorrect OR the scale clause has been
                        'incorrectly assigned as missing/present. Either way return an incorrect source error.
                        returnResult = returnResult Or dnNameStatus.INVALID_SOURCE
                    End If
                    'Whether or not the source clause is correct, since it is not optional, at this point
                    'assume that the next cluase will the the permissions.
                    Array.Copy(curNameParts, 1, nextNameParts, 0, (curNameParts.Length - 1))
                    returnResult = returnResult Or nameValidityStatus(nextNameParts, CLAUSE_PERMISSIONS)
                End If

            Case CLAUSE_PERMISSIONS
                If curNameParts(0) Is Nothing OrElse curNameParts.Length < 1 Then
                    'we've got to the end without finding an optional permissions clause.
                    returnResult = returnResult Or dnNameStatus.WARN_MISSING_PERMISSIONS_CLAUSE
                Else
                    'system.console.WriteLine("CLAUSE_PERMISSIONS: " & curNameParts(0))

                    If myDNCL.isvalidPermissionsClause(curNameParts(0)) Then
                        'Permissions is present which is good, move on to the next thing but we don't care about
                        'short free text now, so we only need to know if there is more text
                        If Not curNameParts(1) Is Nothing Then
                            returnResult = returnResult Or dnNameStatus.INFO_FREE_TEXT_PRESENT
                            ''system.console.WriteLine("free-text A ")
                        End If
                    Else
                        'go ahead a check out results from the rest of the string array
                        returnResult = returnResult Or dnNameStatus.WARN_MISSING_PERMISSIONS_CLAUSE
                        Array.Copy(curNameParts, 1, nextNameParts, 0, (curNameParts.Length - 1))
                        returnResult = returnResult Or nameValidityStatus(nextNameParts, CLAUSE_FREETEXT)
                    End If
                End If

            Case CLAUSE_FREETEXT
                ''system.console.WriteLine("free-text  " & curNameParts(0))

                If curNameParts(0) Is Nothing OrElse curNameParts.Length < 1 Then
                    'we've got to the end without finding any free text
                Else
                    'system.console.WriteLine("CLAUSE_FREETEXT: " & curNameParts(0))

                    If curNameParts(0).Length = 2 Then
                        'two character long free text is present
                        returnResult = returnResult Or dnNameStatus.WARN_TWO_CHAR_FREE_TEXT Or dnNameStatus.INFO_FREE_TEXT_PRESENT
                        ''system.console.WriteLine("free-text B ")
                    Else
                        returnResult = returnResult Or dnNameStatus.INFO_FREE_TEXT_PRESENT
                        ''system.console.WriteLine("free-text C " & curNameParts(0) & "  " & curNameParts(0).Length)
                    End If
                End If

        End Select

        Return returnResult
    End Function

End Class
