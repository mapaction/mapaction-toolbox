
Public MustInherit Class AbstractDataNameClauseLookup
    Implements IDataNameClauseLookup

    Private myGeoExtentTable As DataTable
    Private myDataCategoriesTable As DataTable
    Private myDataThemeTable As DataTable
    Private myDataTypeTable As DataTable
    Private myScaleTable As DataTable
    Private mySourceTable As DataTable
    Private myPermissionTable As DataTable

    Protected Sub New()
        'initialiseAllTables()
    End Sub

    Protected Sub initialiseAllTables()
        Dim myTable As DataTable
        Dim myColArrayList As ArrayList

        For Each tableName In allDataNameTables
            myTable = openTable(tableName)
            myColArrayList = New ArrayList

            For Each col In myTable.Columns
                myColArrayList.Add(col)
            Next

            Try
                If doDataColumnsMatch(CType(allDataNameColumns.Item(tableName), ArrayList), myColArrayList) Then
                    Select Case tableName
                        Case TABLENAME_GEOEXTENT
                            myGeoExtentTable = myTable

                        Case TABLENAME_DATA_CAT
                            myDataCategoriesTable = myTable

                        Case TABLENAME_DATA_THEME
                            myDataThemeTable = myTable

                        Case TABLENAME_DATA_TYPE
                            myDataTypeTable = myTable

                        Case TABLENAME_SCALE
                            myScaleTable = myTable

                        Case TABLENAME_SOURCE
                            mySourceTable = myTable

                        Case TABLENAME_PERMISSION
                            myPermissionTable = myTable

                    End Select
                End If
            Catch ex As Exception
                Throw New LookupTableException("Error whist reading data clause lookup table " & tableName, ex)
            End Try

        Next
    End Sub

    Protected MustOverride Function openTable(ByVal tableName As String) As DataTable

    Public MustOverride Function isWriteable() As Boolean Implements IDataNameClauseLookup.isWriteable

    ''' <summary>
    ''' Is this really the best way to check the schema of the DB? probably could be done better using the XML schema definition stuff in ADO.NET
    ''' Columns *must* be in the same order.
    ''' </summary>
    ''' <param name="dcal1"></param>
    ''' <param name="dcal2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function doDataColumnsMatch(ByRef dcal1 As ArrayList, ByRef dcal2 As ArrayList) As Boolean
        Dim returnRes As Boolean
        Dim excepReason As String
        Dim curIndx As Integer
        Dim dc1 As DataColumn, dc2 As DataColumn

        'assume true until proved otherwise
        returnRes = True
        excepReason = Nothing

        If dcal1.Count <> dcal2.Count Then
            'returnRes = False
            Throw New LookupTableException("Incorrect number of columns in table. " & dcal1.Count & " vs " & dcal2.Count)
        Else
            For curIndx = 0 To (dcal1.Count - 1)
                dc1 = CType(dcal1.Item(curIndx), DataColumn)
                dc2 = CType(dcal2.Item(curIndx), DataColumn)

                'If dc1.ColumnName <> dc2.ColumnName _
                'Or dc1.DataType.FullName <> dc2.DataType.FullName _
                'Or dc1.MaxLength <> dc2.MaxLength _
                'Or dc1.Unique <> dc2.Unique _
                'Or dc1.AutoIncrement <> dc2.AutoIncrement _
                'Or dc1.Caption <> dc2.Caption _
                'Or dc1.ReadOnly <> dc2.ReadOnly Then
                '    'returnRes = False

                Select Case True
                    Case dc1.ColumnName <> dc2.ColumnName
                        excepReason = "column names doesn't match"
                    Case dc1.DataType.FullName <> dc2.DataType.FullName
                        excepReason = "column data type doesn't match"
                    Case dc1.MaxLength <> dc2.MaxLength
                        excepReason = "column length doesn't match"
                    Case dc1.Unique <> dc2.Unique
                        excepReason = "column unique requirement doesn't match"
                    Case dc1.AutoIncrement <> dc2.AutoIncrement
                        excepReason = "column AutoIncrement requirement doesn't match"
                    Case dc1.Caption <> dc2.Caption
                        excepReason = "column Caption requirement doesn't match"
                    Case dc1.ReadOnly <> dc2.ReadOnly
                        excepReason = "column CaReadOnlyption requirement doesn't match"
                    Case Else
                        excepReason = Nothing
                End Select

                If excepReason Is Nothing Then
                    Throw New LookupTableException("Incorrect column specification for column: " & excepReason)
                End If
            Next
        End If

        doDataColumnsMatch = returnRes
    End Function


    Private Function getListFromTable(ByRef theDataTable As DataTable) As List(Of String)
        Dim dtr As DataTableReader
        Dim pkIdx As Integer
        Dim myList As New List(Of String)

        dtr = theDataTable.CreateDataReader(PRI_KEY_COL_NAME)
        pkIdx = theDataTable.Columns.IndexOf(PRI_KEY_COL_NAME)

        While dtr.HasRows
            myList.Add(dtr.GetString(pkIdx))
            dtr.NextResult()
        End While

        getListFromTable = myList
    End Function

    Private Function isStrInPriKeyCol(ByRef theStr As String, ByRef theDataTable As DataTable) As Boolean
        'Dim dtr As DataTableReader
        Dim pkIdx As Integer
        Dim strPresent As Boolean

        strPresent = False

        'dtr = theDataTable.CreateDataReader()
        'dtr = theDataTable.CreateDataReader(PRI_KEY_COL_NAME)
        pkIdx = theDataTable.Columns.IndexOf(PRI_KEY_COL_NAME)

        'dtr.NextResult()
        'dtr.GetValue()

        For Each row In theDataTable.Rows
            If Strings.LCase(row.item(pkIdx)) = Strings.LCase(theStr) Then
                'System.Console.WriteLine(theStr & "    " & PRI_KEY_COL_NAME & "    " & row.item(pkIdx))
                strPresent = True
            End If
        Next

        'While dtr.HasRows And Not strPresent
        '    If Strings.LCase(dtr.GetValue(pkIdx)) = Strings.LCase(theStr) Then
        '        strPresent = True
        '    Else
        '        dtr.NextResult()
        '    End If
        'End While

        isStrInPriKeyCol = strPresent
    End Function

    Public Function getDataCategoryList() As List(Of String) Implements IDataNameClauseLookup.getDataCategoryList
        getDataCategoryList = getListFromTable(myDataCategoriesTable)
    End Function

    Public Function getDataCategoryTable() As DataTable Implements IDataNameClauseLookup.getDataCategoryTable
        getDataCategoryTable = myDataCategoriesTable
    End Function

    Public Function getDataThemeList() As List(Of String) Implements IDataNameClauseLookup.getDataThemeList
        getDataThemeList = getListFromTable(myDataThemeTable)
    End Function

    Public Function getDataThemeTable() As DataTable Implements IDataNameClauseLookup.getDataThemeTable
        getDataThemeTable = myDataThemeTable
    End Function

    Public Function getDataTypeList() As List(Of String) Implements IDataNameClauseLookup.getDataTypeList
        getDataTypeList = getListFromTable(myDataTypeTable)
    End Function

    Public Function getDataTypeTable() As DataTable Implements IDataNameClauseLookup.getDataTypeTable
        getDataTypeTable = myDataTypeTable
    End Function

    Public Function getGeoExtentList() As List(Of String) Implements IDataNameClauseLookup.getGeoExtentList
        getGeoExtentList = getListFromTable(myGeoExtentTable)
    End Function

    Public Function getGeoExtentTable() As DataTable Implements IDataNameClauseLookup.getGeoExtentTable
        getGeoExtentTable = myGeoExtentTable
    End Function

    Public Function getPermissionsList() As List(Of String) Implements IDataNameClauseLookup.getPermissionsList
        getPermissionsList = getListFromTable(myPermissionTable)
    End Function

    Public Function getPermissionsTable() As DataTable Implements IDataNameClauseLookup.getPermissionsTable
        getPermissionsTable = myPermissionTable
    End Function

    Public Function getScaleCodesList() As List(Of String) Implements IDataNameClauseLookup.getScaleCodesList
        getScaleCodesList = getListFromTable(myScaleTable)
    End Function

    Public Function getScaleCodesTable() As DataTable Implements IDataNameClauseLookup.getScaleCodesTable
        getScaleCodesTable = myScaleTable
    End Function

    Public Function getSourceCodesList() As List(Of String) Implements IDataNameClauseLookup.getSourceCodesList
        getSourceCodesList = getListFromTable(mySourceTable)
    End Function

    Public Function getSourceCodesTable() As DataTable Implements IDataNameClauseLookup.getSourceCodesTable
        getSourceCodesTable = mySourceTable
    End Function

    Public Function isvalidDataCategoryClause(ByVal testDataCatClause As String) As Boolean Implements IDataNameClauseLookup.isvalidDataCategoryClause
        isvalidDataCategoryClause = isStrInPriKeyCol(testDataCatClause, myDataCategoriesTable)
    End Function

    Public Function isvalidDataThemeClause(ByVal testDataThemeClause As String, ByVal testDataCatClause As String) As Boolean Implements IDataNameClauseLookup.isvalidDataThemeClause

        'Dim dtr As DataTableReader
        Dim clauseColIdx As Integer, dataCatColIdx As Integer
        Dim strPresent As Boolean
        
        strPresent = False

        'dtr = myDataThemeTable.CreateDataReader()
        clauseColIdx = myDataThemeTable.Columns.IndexOf(PRI_KEY_COL_NAME)
        dataCatColIdx = myDataThemeTable.Columns.IndexOf("Data_Category")

        For Each row In myDataThemeTable.Rows
            If Strings.LCase(row.item(dataCatColIdx)) = Strings.LCase(testDataCatClause) _
            AndAlso Strings.LCase(row.item(clauseColIdx)) = Strings.LCase(testDataThemeClause) Then
                strPresent = True
            End If
        Next

        'While dtr.HasRows And Not strPresent
        '    If Strings.LCase(dtr.GetString(dataCatColIdx)) = Strings.LCase(testDataCatClause) _
        '    AndAlso Strings.LCase(dtr.GetString(clauseColIdx)) = Strings.LCase(testDataThemeClause) Then
        '        strPresent = True
        '    Else
        '        dtr.NextResult()
        '    End If
        'End While

        isvalidDataThemeClause = strPresent

    End Function


    Public Function isvalidDataTypeClause(ByVal testDataTypeClause As String) As Boolean Implements IDataNameClauseLookup.isvalidDataTypeClause
        isvalidDataTypeClause = isStrInPriKeyCol(testDataTypeClause, myDataTypeTable)
    End Function

    Public Function isvalidGeoextentClause(ByVal testGeoExtentClause As String) As Boolean Implements IDataNameClauseLookup.isvalidGeoextentClause
        isvalidGeoextentClause = isStrInPriKeyCol(testGeoExtentClause, myGeoExtentTable)
    End Function

    Public Function isvalidPermissionsClause(ByVal testPermissionsClause As String) As Boolean Implements IDataNameClauseLookup.isvalidPermissionsClause
        isvalidPermissionsClause = isStrInPriKeyCol(testPermissionsClause, myPermissionTable)
    End Function

    Public Function isvalidScaleClause(ByVal testScaleClause As String) As Boolean Implements IDataNameClauseLookup.isvalidScaleClause
        isvalidScaleClause = isStrInPriKeyCol(testScaleClause, myScaleTable)
    End Function

    Public Function isvalidSourceClause(ByVal testSourceClause As String) As Boolean Implements IDataNameClauseLookup.isvalidSourceClause
        isvalidSourceClause = isStrInPriKeyCol(testSourceClause, mySourceTable)
    End Function


    Private Function getNameStatus(ByVal myStr As String) As Long Implements IDataNameClauseLookup.getNameStatus
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
        Dim returnResult As Long
        Dim partsCnt As Integer
        Dim nameParts As String()
        Dim innerNameParts As String()

        Dim geoextentIdx As Integer
        Dim dataCategoryIdx As Integer
        Dim dataThemeIdx As Integer
        Dim dataTypeIdx As Integer
        Dim scaleIdx As Integer
        Dim sourceIdx As Integer
        Dim permissionIdx As Integer
        'Dim freeTextIdx As Integer

        '  +ve number is the index in the array
        '  Zero mean the position is unknown
        '  -ve number means that the option clause is missing
        '  Those that are not fixed index (due to the persence or absence of optional clauses are not 
        '  initialised as late as possible in the code. This is so that the compiler will catch any
        '  routes by which they can't initialised.
        geoextentIdx = 0
        dataCategoryIdx = 1
        dataThemeIdx = 2
        dataTypeIdx = 3
        scaleIdx = 4
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
            returnResult = returnResult Or getNameStatus(myStr.Replace("-", "_"))
        Else

            nameParts = Strings.Split(myStr, "_")
            'partsCnt = nameParts.Length
            
            'System.Console.WriteLine("partCnt = " & partsCnt)

            'Check one
            'does at least five components?  5 <= nameParts
            If nameParts.Length < 5 Then
                returnResult = returnResult Or DATANAME_ERROR_TOO_FEW_CLAUSES
            Else
                'Check Two
                'Is the first clause a valid GeoExtent?
                If Not isvalidGeoextentClause(nameParts(geoextentIdx)) Then
                    returnResult = returnResult Or DATANAME_ERROR_INVALID_GEOEXTENT
                End If

                'Check Three
                'Are the secound and thrid clauses valid DataCategory and DataTheme?
                If Not isvalidDataThemeClause(nameParts(dataThemeIdx), nameParts(dataCategoryIdx)) Then
                    returnResult = returnResult Or DATANAME_ERROR_INVALID_DATATHEME
                    'Check Three.one
                    If Not isvalidDataCategoryClause(nameParts(dataCategoryIdx)) Then
                        returnResult = returnResult Or DATANAME_ERROR_INVALID_DATACATEGORY
                    End If
                End If

                'Check Four
                'is the four clause a valid Data Type Clause?
                'NOTE THAT THIS DOES NOT TEST WHETHER THE DATA TYPE ACURATELY REFLECTS THE UNDERLYING DATA!
                If Not isvalidDataTypeClause(nameParts(dataTypeIdx)) Then
                    returnResult = returnResult Or DATANAME_ERROR_INVALID_DATATYPE
                End If

                'geoextent_datacategory_theme_datatype[_scale]_source[_permission][_FreeText]

                'do the remainer of the checks in a seperate recursive function
                ReDim innerNameParts(nameParts.Length - 4)
                Array.Copy(nameParts, 4, innerNameParts, 0, (nameParts.Length - 4))
                returnResult = returnResult Or innerNameStatus(innerNameParts, "scale")

            End If
        End If

        'if we've got this far without an error or a warning then we assume that we are OK
        If ((returnResult And DATANAME_ERROR) <> DATANAME_ERROR) And _
            ((returnResult And DATANAME_INFO) <> DATANAME_INFO) Then
            returnResult = returnResult Or DATANAME_VALID
        End If

        getNameStatus = returnResult
    End Function

    Private Function innerNameStatus(ByVal curNameParts() As String, ByRef clauseName As String) As Long
        Dim returnResult As Long, tempResult As Long
        Dim nextNameParts(curNameParts.Length - 1) As String

        'System.Console.WriteLine("clauseName.ToString " & clauseName)
        'For Each myStr In curNameParts
        '    System.Console.Write(" -  " & myStr)
        'Next


        Select Case clauseName
            Case "scale"
                If curNameParts(0) Is Nothing OrElse curNameParts.Length < 1 Then
                    'we've got to the end without finding a scale clause which means we have problems!
                    returnResult = returnResult Or DATANAME_ERROR_INCORRECT_SOURCE Or DATANAME_WARN_MISSING_SCALE_CLAUSE
                Else
                    'go ahead a check out results from the rest of the string array
                    Array.Copy(curNameParts, 1, nextNameParts, 0, (curNameParts.Length - 1))
                    tempResult = innerNameStatus(nextNameParts, "source")

                    If isvalidScaleClause(curNameParts(0)) Then
                        'Scale is present which is good, move on to the next thing - source
                        returnResult = returnResult Or tempResult
                    Else
                        If (tempResult And DATANAME_ERROR_INCORRECT_SOURCE) = DATANAME_ERROR_INCORRECT_SOURCE Then
                            'the next clause isn't a source clause so assume that this one is the
                            'source clase and that there is no scale clause
                            returnResult = returnResult Or DATANAME_WARN_MISSING_SCALE_CLAUSE
                            returnResult = returnResult Or innerNameStatus(curNameParts, "source")
                        Else
                            'assume that the next clause is the source clause so join that into the result
                            returnResult = returnResult Or tempResult
                        End If
                    End If
                End If


            Case "source"
                If curNameParts(0) Is Nothing OrElse curNameParts.Length < 1 Then
                    'we've got to the end without finding a source clause which means we have problems!
                    returnResult = returnResult Or DATANAME_ERROR_INCORRECT_SOURCE
                Else
                    If Not isvalidSourceClause(curNameParts(0)) Then
                        'Either the source clause is incorrect OR the scale clause has been
                        'incorrectly assigned as missing/present. Either way return an incorrect source error.
                        returnResult = returnResult Or DATANAME_ERROR_INCORRECT_SOURCE
                    End If
                    'Whether or not the source clause is correct, since it is not optional, at this point
                    'assume that the next cluase will the the permissions.
                    Array.Copy(curNameParts, 1, nextNameParts, 0, (curNameParts.Length - 1))
                    returnResult = returnResult Or innerNameStatus(nextNameParts, "permissions")
                End If

            Case "permissions"
                If curNameParts(0) Is Nothing OrElse curNameParts.Length < 1 Then
                    'we've got to the end without finding an optional permissions clause.
                    returnResult = returnResult Or DATANAME_WARN_MISSING_PERMISSIONS_CLAUSE
                Else

                    If isvalidPermissionsClause(curNameParts(0)) Then
                        'Permissions is present which is good, move on to the next thing but we don't care about
                        'short free text now, so we only need to know if there is more text
                        If Not curNameParts(1) Is Nothing Then
                            returnResult = returnResult Or DATANAME_INFO_FREE_TEXT_PRESENT
                            'System.Console.WriteLine("free-text A ")
                        End If
                    Else
                        'go ahead a check out results from the rest of the string array
                        returnResult = returnResult Or DATANAME_WARN_MISSING_PERMISSIONS_CLAUSE
                        Array.Copy(curNameParts, 1, nextNameParts, 0, (curNameParts.Length - 1))
                        returnResult = returnResult Or innerNameStatus(nextNameParts, "free-text")
                    End If
                End If

            Case "free-text"
                'System.Console.WriteLine("free-text  " & curNameParts(0))

                If curNameParts(0) Is Nothing OrElse curNameParts.Length < 1 Then
                    'we've got to the end without finding any free text
                Else
                    If curNameParts(0).Length = 2 Then
                        'two character long free text is present
                        returnResult = returnResult Or DATANAME_WARN_TWO_CHAR_FREE_TEXT Or DATANAME_INFO_FREE_TEXT_PRESENT
                        'System.Console.WriteLine("free-text B ")
                    Else
                        returnResult = returnResult Or DATANAME_INFO_FREE_TEXT_PRESENT
                        'System.Console.WriteLine("free-text C " & curNameParts(0) & "  " & curNameParts(0).Length)
                    End If
                End If

        End Select

        innerNameStatus = returnResult
    End Function

    '    'Check Five
    '    'Is the fifth clause a scale clause or a source clause?
    '    If isvalidScaleClause(nameparts(scaleIdx)) Then




    '        'fifth clause is scale clause, now test sixth clause for source
    '        sourceIdx = 5
    '        permissionIdx = 6
    '        scale_status_known = True

    '        If Not isvalidSourceClause(nameparts(sourceIdx)) Then
    '            returnResult = returnResult Or DATANAME_ERROR_INCORRECT_SOURCE
    '        End If
    '    Else
    '        scale_status_known = False
    '        'Else
    '        'fifth clause isn't valid scale clause. Is this becuase the fifth clause is actually a source clause 
    '        'or because it is an invalid scale clause?
    '        'delibartilay passing the scaleInx to isValidSourceClause
    '        If isvalidSourceClause(nameparts(scaleIdx)) Then
    '            'fifth clause is a valid source clause therefore there is no scale clause
    '            returnResult = returnResult Or DATANAME_WARN_MISSING_SCALE_CLAUSE
    '            scaleIdx = -1
    '            sourceIdx = 4
    '            permissionIdx = 5
    '        Else
    '            'Could be that the fifth clause is supposed to be a Scale Clause but is erroronious
    '            'OR
    '            'There isn't a Scale Clause and that this clause is supposed to be a Source Clause and is
    '            'errorenious.

    '            'returnResult = returnResult and dataname_error_
    '            If nameparts.Length > 5 Then
    '                If isvalidSourceClause(nameparts(5)) Then
    '                    scale_status_known = True
    '                    scaleIdx = 4
    '                    sourceIdx = 5
    '                    returnResult = returnResult Or DATANAME_ERROR_INCORRECT_SCALE
    '                Else
    '                    scale_status_known = False
    '                    returnResult = returnResult Or DATANAME_ERROR_INCORRECT_SOURCE
    '                End If
    '            Else
    '                returnResult = returnResult Or DATANAME_WARN_MISSING_SCALE_CLAUSE
    '                returnResult = returnResult Or DATANAME_WARN_MISSING_PERMISSIONS_CLAUSE
    '            End If
    '        End If
    '    End If

    '    'check combination of Permission clause and Free Text
    '    If (Not (returnResult And DATANAME_ERROR_INCORRECT_SOURCE) = DATANAME_ERROR_INCORRECT_SOURCE) Or _
    '        (scale_status_known And Not (returnResult And DATANAME_WARN_MISSING_SCALE_CLAUSE) = DATANAME_WARN_MISSING_SCALE_CLAUSE) Then
    '        If Not nameparts.Length > sourceIdx Then
    '            permissions_status_known = True
    '            returnResult = returnResult Or DATANAME_WARN_MISSING_PERMISSIONS_CLAUSE
    '        Else
    '            permissionIdx = sourceIdx + 1
    '            If isvalidPermissionsClause(nameparts(permissionIdx - 1)) Then
    '                permissions_status_known = True
    '                If nameparts.Length > permissionIdx Then
    '                    returnResult = returnResult Or DATANAME_INFO_FREE_TEXT_PRESENT
    '                End If
    '            Else
    '                returnResult = returnResult Or DATANAME_INFO_FREE_TEXT_PRESENT Or DATANAME_WARN_MISSING_PERMISSIONS_CLAUSE
    '                If nameparts(permissionIdx - 1).Length = 2 Then
    '                    returnResult = returnResult Or DATANAME_WARN_TWO_CHAR_FREE_TEXT
    '                End If

    '            End If
    '        End If
    '    Else
    '        ''final details to go in here
    '        If nameparts.Length > 5 AndAlso isvalidPermissionsClause(nameparts(5)) Then
    '            scale_status_known = True
    '            returnResult = returnResult Or DATANAME_WARN_MISSING_SCALE_CLAUSE
    '            If nameparts.Length > 6 Then
    '                returnResult = returnResult Or DATANAME_INFO_FREE_TEXT_PRESENT
    '            End If
    '        ElseIf nameparts.Length > 6 AndAlso isvalidPermissionsClause(nameparts(6)) Then
    '            scale_status_known = True
    '            returnResult = returnResult Or DATANAME_ERROR_INCORRECT_SCALE
    '            If nameparts.Length > 7 Then
    '                returnResult = returnResult Or DATANAME_INFO_FREE_TEXT_PRESENT
    '            End If
    '        Else
    '            returnResult = returnResult Or DATANAME_INFO_FREE_TEXT_PRESENT Or DATANAME_WARN_MISSING_PERMISSIONS_CLAUSE
    '            If nameparts(permissionIdx).Length = 2 Then
    '                returnResult = returnResult Or DATANAME_WARN_TWO_CHAR_FREE_TEXT
    '            End If

    '        End If
    '    End If

    '    End If

    '    'if we've got this far without an error or a warning then we assume that we are OK
    '    If ((returnResult And DATANAME_ERROR) <> DATANAME_ERROR) And _
    '        ((returnResult And DATANAME_INFO) <> DATANAME_INFO) Then
    '        returnResult = returnResult Or DATANAME_VALID
    '    End If

    '    getNameStatus = returnResult
    'End Function

    Public Shared Function getDataNamingStatusStrings(ByVal status As Long) As List(Of String)
        Dim outputList As New List(Of String)

        Dim statusCodes() As Long = {DATANAME_ERROR_INVALID_GEOEXTENT, _
                                    DATANAME_ERROR_INVALID_DATACATEGORY, _
                                    DATANAME_ERROR_INVALID_DATATHEME, _
                                    DATANAME_ERROR_INVALID_DATATYPE, _
                                    DATANAME_ERROR_INCORRECT_DATATYPE, _
                                    DATANAME_ERROR_INCORRECT_SCALE, _
                                    DATANAME_ERROR_INCORRECT_SOURCE, _
                                    DATANAME_ERROR_INCORRECT_PERMISSIONS, _
                                    DATANAME_ERROR_OTHER_ERROR, _
                                    DATANAME_ERROR_CONTAINS_HYPHENS, _
                                    DATANAME_ERROR_TOO_FEW_CLAUSES, _
                                    DATANAME_WARN_MISSING_SCALE_CLAUSE, _
                                    DATANAME_WARN_MISSING_PERMISSIONS_CLAUSE, _
                                    DATANAME_WARN_TWO_CHAR_FREE_TEXT, _
                                    DATANAME_INFO_FREE_TEXT_PRESENT}

        For Each staCode In statusCodes
            If ((status And staCode) = staCode) Then
                outputList.Add(CType(allDataNameStrMessages.Item(staCode), String))
                'outputList.Add(errCode)
            End If
        Next

        getDataNamingStatusStrings = outputList
    End Function

End Class
