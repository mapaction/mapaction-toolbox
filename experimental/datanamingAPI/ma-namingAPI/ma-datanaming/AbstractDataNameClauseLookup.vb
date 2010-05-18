Imports System.Data

Public MustInherit Class AbstractDataNameClauseLookup
    Implements IDataNameClauseLookup

    Private myGeoExtentTable As DataTable
    Private myDataCategoriesTable As DataTable
    Private myDataThemeTable As DataTable
    Private myDataTypeTable As DataTable
    Private myScaleTable As DataTable
    Private mySourceTable As DataTable
    Private myPermissionTable As DataTable
    Private doRecusion As Boolean

    Protected Sub New()
        'initialiseAllTables()
    End Sub

    Protected Sub initialiseAllTables()
        Dim myTable As DataTable
        Dim myColArrayList As ArrayList

        For Each tableName In g_strAryClauseTableNames
            Try
                myTable = openTable(tableName)
                myColArrayList = New ArrayList

                For Each col In myTable.Columns
                    myColArrayList.Add(col)
                Next

                If doDataColumnsMatch(CType(g_htbAllDataNameColumns.Item(tableName), ArrayList), myColArrayList) Then
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
                Throw New LookupTableException(dnLookupTableError.general, tableName, ex)
            End Try

        Next
    End Sub

    Protected MustOverride Function openTable(ByVal tableName As String) As DataTable

    Public MustOverride Function isWriteable() As Boolean Implements IDataNameClauseLookup.isWriteable

    Public MustOverride Function getDetails() As String Implements IDataNameClauseLookup.getDetails

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

        'todo MEDIUM: Adjust doDataColumnsMatch to allow for optional ObjectID column

        'assume true until proved otherwise
        returnRes = True
        excepReason = Nothing

        If dcal1.Count <> dcal2.Count Then
            'returnRes = False
            Throw New LookupTableException(dnLookupTableError.wrong_no_of_cols, CStr(dcal1.Count & " vs " & dcal2.Count))
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
                        excepReason = "column ReadOnly requirement doesn't match"
                    Case Else
                        excepReason = Nothing
                End Select

                If excepReason Is Nothing Then
                    Throw New LookupTableException(dnLookupTableError.wrong_col_spec, excepReason)
                End If
            Next
        End If

        doDataColumnsMatch = returnRes
    End Function


    Private Function getListFromTable(ByRef theDataTable As DataTable) As List(Of String)
        Dim dtr As DataTableReader
        Dim pkIdx As Integer
        Dim myList As New List(Of String)

        dtr = CType(theDataTable.CreateDataReader(PRI_KEY_COL_NAME), DataTableReader)
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

        'For Each row In theDataTable.Rows.
        For i = 0 To (theDataTable.Rows.Count - 1)
            'If Strings.LCase(row.item(pkIdx)) = Strings.LCase(theStr) Then
            If Strings.LCase(CStr(theDataTable.Rows.Item(i).Item(pkIdx))) = Strings.LCase(theStr) Then
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


        'For i = 0 To (theDataTable.Rows.Count - 1)
        '    'If Strings.LCase(row.item(pkIdx)) = Strings.LCase(theStr) Then
        '    If Strings.LCase(CStr(theDataTable.Rows.Item(i).Item(pkIdx))) = Strings.LCase(theStr) Then

        For i = 0 To (myDataThemeTable.Rows.Count - 1)
            'For Each row In myDataThemeTable.Rows
            If Strings.LCase(CStr(myDataThemeTable.Rows.Item(i).Item(dataCatColIdx))) = Strings.LCase(testDataCatClause) _
            AndAlso Strings.LCase(CStr(myDataThemeTable.Rows.Item(i).Item(clauseColIdx))) = Strings.LCase(testDataThemeClause) Then
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

        Dim statusCodes As List(Of Long) = New List(Of Long)

        statusCodes.Add(dnNameStatus.SYNTAX_ERROR_CONTAINS_HYPHENS)
        statusCodes.Add(dnNameStatus.SYNTAX_ERROR_DOUBLE_UNDERSCORE)
        statusCodes.Add(dnNameStatus.SYNTAX_ERROR_OTHER)
        statusCodes.Add(dnNameStatus.SYNTAX_ERROR_TOO_FEW_CLAUSES)
        statusCodes.Add(dnNameStatus.INVALID_DATACATEGORY)
        statusCodes.Add(dnNameStatus.INVALID_DATATHEME)
        statusCodes.Add(dnNameStatus.INVALID_DATATYPE)
        statusCodes.Add(dnNameStatus.INCORRECT_DATATYPE)
        statusCodes.Add(dnNameStatus.INVALID_GEOEXTENT)
        statusCodes.Add(dnNameStatus.INVALID_PERMISSIONS)
        statusCodes.Add(dnNameStatus.INVALID_SCALE)
        statusCodes.Add(dnNameStatus.INVALID_SOURCE)
        statusCodes.Add(dnNameStatus.INCORRECT_DATATYPE)
        statusCodes.Add(dnNameStatus.WARN_MISSING_SCALE_CLAUSE)
        statusCodes.Add(dnNameStatus.WARN_MISSING_PERMISSIONS_CLAUSE)
        statusCodes.Add(dnNameStatus.WARN_TWO_CHAR_FREE_TEXT)
        statusCodes.Add(dnNameStatus.INFO_FREE_TEXT_PRESENT)

        For Each staCode In statusCodes
            If ((status And staCode) = staCode) Then
                outputList.Add(CStr(g_htbDNStatusStrMessages.Item(staCode)))
                'outputList.Add(errCode)
            End If
        Next

        getDataNamingStatusStrings = outputList
    End Function

End Class
