Imports System.Data

''' <summary>
''' Provides a framework for the implenmentation of the IDataNameClauseLookup interface.
''' </summary>
''' <remarks>
''' Provides a framework for the implenmentation of the IDataNameClauseLookup interface.
''' 
''' Subclasses must call initialiseAllTables() from within their constructor.
''' To implement subclasses two methods, openTable() and getDetails() must be implenmented.
''' </remarks>
Public MustInherit Class AbstractDataNameClauseLookup
    Implements IDataNameClauseLookup

    Private m_dtbGeoExtentClauses As DataTable
    Private m_dtbDataCategoriesClauses As DataTable
    Private m_dtbDataThemeClauses As DataTable
    Private m_dtbDataTypeClauses As DataTable
    Private m_dtbScaleClauses As DataTable
    Private m_dtbSourceClauses As DataTable
    Private m_dtbPermissionClauses As DataTable
    'Private doRecusion As Boolean

    Protected Sub New()
        'initialiseAllTables()
    End Sub

    ''' <summary>
    ''' Opens each of the dataname clause lookup tables. Must be called from the constructor
    ''' </summary>
    ''' <remarks>
    ''' Opens each of the dataname clause lookup tables. Must be called from the constructor
    ''' 
    ''' Uses the subclasses implenmentation of the openTable() method.
    ''' </remarks>
    Protected Sub initialiseAllTables()
        Dim dtbCurrent As DataTable
        Dim arylColDetails As ArrayList

        For Each tableName In g_strAryClauseTableNames
            Try
                dtbCurrent = openTable(tableName)
                arylColDetails = New ArrayList

                For Each col In dtbCurrent.Columns
                    arylColDetails.Add(col)
                Next

                If doDataColumnsMatch(CType(g_htbAllDataNameColumns.Item(tableName), ArrayList), arylColDetails) Then
                    Select Case tableName
                        Case TABLENAME_GEOEXTENT
                            m_dtbGeoExtentClauses = dtbCurrent

                        Case TABLENAME_DATA_CAT
                            m_dtbDataCategoriesClauses = dtbCurrent

                        Case TABLENAME_DATA_THEME
                            m_dtbDataThemeClauses = dtbCurrent

                        Case TABLENAME_DATA_TYPE
                            m_dtbDataTypeClauses = dtbCurrent

                        Case TABLENAME_SCALE
                            m_dtbScaleClauses = dtbCurrent

                        Case TABLENAME_SOURCE
                            m_dtbSourceClauses = dtbCurrent

                        Case TABLENAME_PERMISSION
                            m_dtbPermissionClauses = dtbCurrent

                    End Select
                End If
            Catch ex As Exception
                Throw New LookupTableException(dnLookupTableError.general, tableName, ex)
            End Try

        Next
    End Sub

    ''' <summary>
    ''' This method provides an implenmention specific means to open an individual flat table
    ''' from a data source (eg, Access DB, ESRI GDB, XML files etc).
    ''' </summary>
    ''' <param name="strTableName">The name of the table to open. This should normally be passed
    ''' using one of the API constants with the prefix "TABLENAME_"</param>
    ''' <returns>A DataTable object representing the </returns>
    ''' <remarks>
    ''' This method provides an implenmention specific means to open an individual flat table
    ''' from a data source (eg, Access DB, ESRI GDB, XML files etc).
    ''' </remarks>
    Protected MustOverride Function openTable(ByVal strTableName As String) As DataTable

    'todo should this be deleted?
    Public MustOverride Function isWriteable() As Boolean Implements IDataNameClauseLookup.isWriteable

    'todo add summary
    Public MustOverride Function getDetails() As String Implements IDataNameClauseLookup.getDetails

    ''' <summary>
    ''' Compared two ArrayLists of DataColumn objects, to confirm whether or not the specfication
    ''' of the columns (ie the schema) is identical for both.
    ''' </summary>
    ''' <param name="dcal1">An ArrayList of DataColumn objects</param>
    ''' <param name="dcal2">An ArrayList of DataColumn objects</param>
    ''' <returns>TRUE = the DataColumn specfications match, FALSE otherwise.</returns>
    ''' <remarks>
    ''' Compared two ArrayLists of DataColumn objects, to confirm whether or not the specfication
    ''' of the columns (ie the schema) is identical for both. This is used to verify the schema of
    ''' any Data Name Clause Lookup Tables that have been opened.
    ''' 
    ''' Is this really the best way to check the schema of the DB? probably could be done better using the XML schema definition stuff in ADO.NET
    ''' Columns *must* be in the same order.
    ''' </remarks>
    Private Function doDataColumnsMatch(ByRef dcal1 As ArrayList, ByRef dcal2 As ArrayList) As Boolean
        Dim blnResult As Boolean
        Dim strExcepReason As String
        Dim dc1 As DataColumn, dc2 As DataColumn

        'todo MEDIUM: Adjust doDataColumnsMatch to allow for optional ObjectID column

        'assume true until proved otherwise
        blnResult = True
        strExcepReason = Nothing

        If dcal1.Count <> dcal2.Count Then
            'returnRes = False
            Throw New LookupTableException(dnLookupTableError.wrong_no_of_cols, _
                                           CStr(dcal1.Count & " vs " & dcal2.Count))
        Else
            For intCurIndx = 0 To (dcal1.Count - 1)
                dc1 = CType(dcal1.Item(intCurIndx), DataColumn)
                dc2 = CType(dcal2.Item(intCurIndx), DataColumn)

                'todo MOVE THESE STRING TO CONSTATS AND ENUMERATIONS
                Select Case True
                    Case dc1.ColumnName <> dc2.ColumnName
                        strExcepReason = "column names doesn't match"
                    Case dc1.DataType.FullName <> dc2.DataType.FullName
                        strExcepReason = "column data type doesn't match"
                    Case dc1.MaxLength <> dc2.MaxLength
                        strExcepReason = "column length doesn't match"
                    Case dc1.Unique <> dc2.Unique
                        strExcepReason = "column unique requirement doesn't match"
                    Case dc1.AutoIncrement <> dc2.AutoIncrement
                        strExcepReason = "column AutoIncrement requirement doesn't match"
                    Case dc1.Caption <> dc2.Caption
                        strExcepReason = "column Caption requirement doesn't match"
                    Case dc1.ReadOnly <> dc2.ReadOnly
                        strExcepReason = "column ReadOnly requirement doesn't match"
                    Case Else
                        strExcepReason = Nothing
                End Select

                If strExcepReason IsNot Nothing Then
                    Throw New LookupTableException(dnLookupTableError.wrong_col_spec, strExcepReason)
                End If
            Next
        End If

        Return blnResult
    End Function

  
    ''' <summary>
    ''' A convenience function to extact all of the clauses (ie the Primary Keys) from a
    ''' Data Name Clause Lookup Table.
    ''' </summary>
    ''' <param name="dt">A DataTable representing the a Data Name Clause Lookup
    ''' Table. An Exception will occur if the table does not have a primary column with a
    ''' name which matches the PRI_KEY_COL_NAME constant.</param>
    ''' <returns>A List(Of String) of each of the clauses in the DataTable</returns>
    ''' <remarks>
    ''' A convenience function to extact all of the clauses (ie the Primary Keys) from a
    ''' Data Name Clause Lookup Table.
    ''' 
    ''' An Exception will occur if the table does not have a primary column with a
    ''' name which matches the PRI_KEY_COL_NAME constant.
    ''' </remarks>
    Private Function getListFromTable(ByRef dt As DataTable) As List(Of String)
        Dim dtr As DataTableReader
        Dim intPkIdx As Integer 'Column number of Primary Key
        Dim lstReturnStrs As New List(Of String)

        dtr = CType(dt.CreateDataReader(PRI_KEY_COL_NAME), DataTableReader)
        intPkIdx = dt.Columns.IndexOf(PRI_KEY_COL_NAME)

        While dtr.HasRows
            lstReturnStrs.Add(dtr.GetString(intPkIdx))
            dtr.NextResult()
        End While

        getListFromTable = lstReturnStrs
    End Function


    ''' <summary>
    ''' Checks whether a particular test String is listed in the primary key column of the 
    ''' DataTable. All strings are converted to lower case before comparision.
    ''' </summary>
    ''' <param name="strTestparam">The test String</param>
    ''' <param name="dt">A DataTable representing a Data Name Lookup Table</param>
    ''' <returns>TRUE = The test string is present in the primary key column of the 
    ''' DataTable, FALSE otherwise.</returns>
    ''' <remarks>
    ''' Checks whether a particular test String is listed in the primary key column of the 
    ''' DataTable. All strings are converted to lower case before comparision.
    ''' 
    ''' This method is at the core of checking that the Data Name is valid.
    ''' </remarks>
    Private Function isStrInPriKeyCol(ByRef strTestparam As String, ByRef dt As DataTable) As Boolean
        Dim intPkIdx As Integer 'Column number of Primary Key
        Dim blnPresent As Boolean
        Dim strTestVal As String 'A lower case version of the strTestparam.

        blnPresent = False
        strTestVal = Strings.LCase(strTestparam)

        intPkIdx = dt.Columns.IndexOf(PRI_KEY_COL_NAME)

        For i = 0 To (dt.Rows.Count - 1)
            If (Strings.LCase(CStr(dt.Rows.Item(i).Item(intPkIdx))) = strTestVal) _
              And (Not blnPresent) Then
                blnPresent = True
            End If
        Next

        Return blnPresent
    End Function


    ''' <summary>
    ''' A convenience function to extact all of the Data Category clauses as a List(Of Strings).
    ''' </summary>
    ''' <returns>A List(Of Strings) of all the valid Data Category clauses.
    ''' </returns>
    ''' <remarks>
    ''' A convenience function to extact all of the Data Category clauses as a List(Of Strings).
    ''' </remarks>
    Public Function getDataCategoryList() As List(Of String) Implements IDataNameClauseLookup.getDataCategoryList
        getDataCategoryList = getListFromTable(m_dtbDataCategoriesClauses)
    End Function


    ''' <summary>
    ''' A convenience function to return the Data Category clauses lookup table, including all columns
    ''' </summary>
    ''' <returns>A DataTable representing the Data Category clauses lookup table.
    ''' </returns>
    ''' <remarks>
    ''' A convenience function to return the Data Category clauses lookup table, including all columns
    ''' </remarks>
    Public Function getDataCategoryTable() As DataTable Implements IDataNameClauseLookup.getDataCategoryTable
        getDataCategoryTable = m_dtbDataCategoriesClauses
    End Function


    ''' <summary>
    ''' A convenience function to extact all of the Data Theme clauses as a List(Of Strings).
    ''' </summary>
    ''' <returns>A List(Of Strings) of all the valid Data Theme clauses.
    ''' </returns>
    ''' <remarks>
    ''' A convenience function to extact all of the Data Theme clauses as a List(Of Strings).
    ''' </remarks>
    Public Function getDataThemeList() As List(Of String) Implements IDataNameClauseLookup.getDataThemeList
        getDataThemeList = getListFromTable(m_dtbDataThemeClauses)
    End Function


    ''' <summary>
    ''' A convenience function to return the Data Theme clauses lookup table, including all columns
    ''' </summary>
    ''' <returns>A DataTable representing the Data Theme clauses lookup table.
    ''' </returns>
    ''' <remarks>
    ''' A convenience function to return the Data Theme clauses lookup table, including all columns
    ''' </remarks>
    Public Function getDataThemeTable() As DataTable Implements IDataNameClauseLookup.getDataThemeTable
        getDataThemeTable = m_dtbDataThemeClauses
    End Function


    ''' <summary>
    ''' A convenience function to extact all of the Data Type clauses as a List(Of Strings).
    ''' </summary>
    ''' <returns>A List(Of Strings) of all the valid Data Type clauses.
    ''' </returns>
    ''' <remarks>
    ''' A convenience function to extact all of the Data Type clauses as a List(Of Strings).
    ''' </remarks>
    Public Function getDataTypeList() As List(Of String) Implements IDataNameClauseLookup.getDataTypeList
        getDataTypeList = getListFromTable(m_dtbDataTypeClauses)
    End Function


    ''' <summary>
    ''' A convenience function to return the Data Type clauses lookup table, including all columns
    ''' </summary>
    ''' <returns>A DataTable representing the Data Type clauses lookup table.
    ''' </returns>
    ''' <remarks>
    ''' A convenience function to return the Data Type clauses lookup table, including all columns
    ''' </remarks>
    Public Function getDataTypeTable() As DataTable Implements IDataNameClauseLookup.getDataTypeTable
        getDataTypeTable = m_dtbDataTypeClauses
    End Function

    ''' <summary>
    ''' A convenience function to extact all of the GeoExtent clauses as a List(Of Strings).
    ''' </summary>
    ''' <returns>A List(Of Strings) of all the valid GeoExtent clauses.
    ''' </returns>
    ''' <remarks>
    ''' A convenience function to extact all of the GeoExtent clauses as a List(Of Strings).
    ''' </remarks>
    Public Function getGeoExtentList() As List(Of String) Implements IDataNameClauseLookup.getGeoExtentList
        getGeoExtentList = getListFromTable(m_dtbGeoExtentClauses)
    End Function


    ''' <summary>
    ''' A convenience function to return the GeoExtent clauses lookup table, including all columns
    ''' </summary>
    ''' <returns>A DataTable representing the GeoExtent clauses lookup table.
    ''' </returns>
    ''' <remarks>
    ''' A convenience function to return the GeoExtent clauses lookup table, including all columns
    ''' </remarks>
    Public Function getGeoExtentTable() As DataTable Implements IDataNameClauseLookup.getGeoExtentTable
        getGeoExtentTable = m_dtbGeoExtentClauses
    End Function


    ''' <summary>
    ''' A convenience function to extact all of the Permissions clauses as a List(Of Strings).
    ''' </summary>
    ''' <returns>A List(Of Strings) of all the valid Permissions clauses.
    ''' </returns>
    ''' <remarks>
    ''' A convenience function to extact all of the Permissions clauses as a List(Of Strings).
    ''' </remarks>
    Public Function getPermissionsList() As List(Of String) Implements IDataNameClauseLookup.getPermissionsList
        getPermissionsList = getListFromTable(m_dtbPermissionClauses)
    End Function


    ''' <summary>
    ''' A convenience function to return the Permissions clauses lookup table, including all columns
    ''' </summary>
    ''' <returns>A DataTable representing the Permissions clauses lookup table.
    ''' </returns>
    ''' <remarks>
    ''' A convenience function to return the Permissions clauses lookup table, including all columns
    ''' </remarks>
    Public Function getPermissionsTable() As DataTable Implements IDataNameClauseLookup.getPermissionsTable
        getPermissionsTable = m_dtbPermissionClauses
    End Function


    ''' <summary>
    ''' A convenience function to extact all of the Scale clauses as a List(Of Strings).
    ''' </summary>
    ''' <returns>A List(Of Strings) of all the valid Scale clauses.
    ''' </returns>
    ''' <remarks>
    ''' A convenience function to extact all of the Scale clauses as a List(Of Strings).
    ''' </remarks>
    Public Function getScaleCodesList() As List(Of String) Implements IDataNameClauseLookup.getScaleCodesList
        getScaleCodesList = getListFromTable(m_dtbScaleClauses)
    End Function


    ''' <summary>
    ''' A convenience function to return the Scale clauses lookup table, including all columns
    ''' </summary>
    ''' <returns>A DataTable representing the Scale clauses lookup table.
    ''' </returns>
    ''' <remarks>
    ''' A convenience function to return the Scale clauses lookup table, including all columns
    ''' </remarks>
    Public Function getScaleCodesTable() As DataTable Implements IDataNameClauseLookup.getScaleCodesTable
        getScaleCodesTable = m_dtbScaleClauses
    End Function


    ''' <summary>
    ''' A convenience function to extact all of the Source clauses as a List(Of Strings).
    ''' </summary>
    ''' <returns>A List(Of Strings) of all the valid Source clauses.
    ''' </returns>
    ''' <remarks>
    ''' A convenience function to extact all of the Source clauses as a List(Of Strings).
    ''' </remarks>
    Public Function getSourceCodesList() As List(Of String) Implements IDataNameClauseLookup.getSourceCodesList
        getSourceCodesList = getListFromTable(m_dtbSourceClauses)
    End Function


    ''' <summary>
    ''' A convenience function to return the Source clauses lookup table, including all columns
    ''' </summary>
    ''' <returns>A DataTable representing the Source clauses lookup table.
    ''' </returns>
    ''' <remarks>
    ''' A convenience function to return the Source clauses lookup table, including all columns
    ''' </remarks>
    Public Function getSourceCodesTable() As DataTable Implements IDataNameClauseLookup.getSourceCodesTable
        getSourceCodesTable = m_dtbSourceClauses
    End Function


    ''' <summary>
    ''' Checks whether a particular test String is a valid Data Category Clause.
    ''' The strings are converted to lower case before comparision.
    ''' </summary>
    ''' <param name="strTestDataCatClause">The test String</param>
    ''' <returns>TRUE = The test string if the test string is a valid Data Category Clause,
    ''' FALSE otherwise.</returns>
    ''' <remarks>
    ''' Checks whether a particular test String is a valid Data Category Clause.
    ''' The strings are converted to lower case before comparision.
    ''' </remarks>
    Public Function isValidDataCategoryClause(ByVal strTestDataCatClause As String) As Boolean Implements IDataNameClauseLookup.isvalidDataCategoryClause
        Return isStrInPriKeyCol(strTestDataCatClause, m_dtbDataCategoriesClauses)
    End Function


    ''' <summary>
    ''' Checks whether a particular test String is a valid Data Theme Clause. Because
    ''' Data Themes are nested within Data Categories, it is also necessary to pass the 
    ''' Data Category to this method. The strings are converted to lower case before
    ''' comparision.
    ''' </summary>
    ''' <param name="strTestDataThemeClause">The Data Theme Clause test String</param>
    ''' <param name="strTestDataCatClause">The Data Category Clause test String</param>
    ''' <returns>TRUE = The test string if the test string is a valid Data Theme Clause,
    ''' FALSE otherwise.</returns>
    ''' <remarks>
    ''' Checks whether a particular test String is a valid Data Theme Clause. Because
    ''' Data Themes are nested within Data Categories, it is also necessary to pass the 
    ''' Data Category to this method. The strings are converted to lower case before
    ''' comparision.
    ''' </remarks>
    Public Function isValidDataThemeClause(ByVal strTestDataThemeClause As String, _
                                           ByVal strTestDataCatClause As String) _
                          As Boolean Implements IDataNameClauseLookup.isvalidDataThemeClause

        Dim intThemeCol As Integer, intCatCol As Integer
        Dim blnIsValid As Boolean
        Dim strCatVal As String     'lower case version of strTestDataCatClause
        Dim strThemeVal As String   'lower case version of strTestDataThemeClause

        blnIsValid = False

        strCatVal = Strings.LCase(strTestDataCatClause)
        strThemeVal = Strings.LCase(strTestDataThemeClause)

        intThemeCol = m_dtbDataThemeClauses.Columns.IndexOf(PRI_KEY_COL_NAME)
        intCatCol = m_dtbDataThemeClauses.Columns.IndexOf("Data_Category")

        For i = 0 To (m_dtbDataThemeClauses.Rows.Count - 1)
            'For Each row In myDataThemeTable.Rows
            If Strings.LCase(CStr(m_dtbDataThemeClauses.Rows.Item(i).Item(intCatCol))) = Strings.LCase(strCatVal) _
            AndAlso Strings.LCase(CStr(m_dtbDataThemeClauses.Rows.Item(i).Item(intThemeCol))) = Strings.LCase(strThemeVal) _
            AndAlso (Not blnIsValid) Then
                blnIsValid = True
            End If
        Next

        Return blnIsValid

    End Function


    ''' <summary>
    ''' Checks whether a particular test String is a valid Data Type Clause.
    ''' The strings are converted to lower case before comparision.
    ''' </summary>
    ''' <param name="strTestDataTypeClause">The test String</param>
    ''' <returns>TRUE = The test string if the test string is a valid Data Type Clause,
    ''' FALSE otherwise.</returns>
    ''' <remarks>
    ''' Checks whether a particular test String is a valid Data Type Clause.
    ''' The strings are converted to lower case before comparision.
    ''' </remarks>
    Public Function isValidDataTypeClause(ByVal strTestDataTypeClause As String) As Boolean _
                                            Implements IDataNameClauseLookup.isvalidDataTypeClause
        Return isStrInPriKeyCol(strTestDataTypeClause, m_dtbDataTypeClauses)
    End Function


    ''' <summary>
    ''' Checks whether a particular test String is a valid Geoextent Clause.
    ''' The strings are converted to lower case before comparision.
    ''' </summary>
    ''' <param name="strTestGeoExtentClause">The test String</param>
    ''' <returns>TRUE = The test string if the test string is a valid Geoextent Clause,
    ''' FALSE otherwise.</returns>
    ''' <remarks>
    ''' Checks whether a particular test String is a valid Geoextent Clause.
    ''' The strings are converted to lower case before comparision.
    ''' </remarks>
    Public Function isValidGeoextentClause(ByVal strTestGeoExtentClause As String) As Boolean Implements IDataNameClauseLookup.isvalidGeoextentClause
        isValidGeoextentClause = isStrInPriKeyCol(strTestGeoExtentClause, m_dtbGeoExtentClauses)
    End Function


    ''' <summary>
    ''' Checks whether a particular test String is a valid Permissions Clause.
    ''' The strings are converted to lower case before comparision.
    ''' </summary>
    ''' <param name="strTestPermissionsClause">The test String</param>
    ''' <returns>TRUE = The test string if the test string is a valid Permissions Clause,
    ''' FALSE otherwise.</returns>
    ''' <remarks>
    ''' Checks whether a particular test String is a valid Permissions Clause.
    ''' The strings are converted to lower case before comparision.
    ''' </remarks>
    Public Function isValidPermissionsClause(ByVal strTestPermissionsClause As String) As Boolean Implements IDataNameClauseLookup.isvalidPermissionsClause
        isValidPermissionsClause = isStrInPriKeyCol(strTestPermissionsClause, m_dtbPermissionClauses)
    End Function


    ''' <summary>
    ''' Checks whether a particular test String is a valid Scale Clause.
    ''' The strings are converted to lower case before comparision.
    ''' </summary>
    ''' <param name="strTestScaleClause">The test String</param>
    ''' <returns>TRUE = The test string if the test string is a valid Scale Clause,
    ''' FALSE otherwise.</returns>
    ''' <remarks>
    ''' Checks whether a particular test String is a valid Scale Clause.
    ''' The strings are converted to lower case before comparision.
    ''' </remarks>
    Public Function isValidScaleClause(ByVal strTestScaleClause As String) As Boolean Implements IDataNameClauseLookup.isvalidScaleClause
        isValidScaleClause = isStrInPriKeyCol(strTestScaleClause, m_dtbScaleClauses)
    End Function


    ''' <summary>
    ''' Checks whether a particular test String is a valid Source Clause.
    ''' The strings are converted to lower case before comparision.
    ''' </summary>
    ''' <param name="strTestSourceClause">The test String</param>
    ''' <returns>TRUE = The test string if the test string is a valid Source Clause,
    ''' FALSE otherwise.</returns>
    ''' <remarks>
    ''' Checks whether a particular test String is a valid Source Clause.
    ''' The strings are converted to lower case before comparision.
    ''' </remarks>
    Public Function isValidSourceClause(ByVal strTestSourceClause As String) As Boolean Implements IDataNameClauseLookup.isvalidSourceClause
        isValidSourceClause = isStrInPriKeyCol(strTestSourceClause, m_dtbSourceClauses)
    End Function

    ''''''''''''''''''''''''''''''
    ' Checked to here
    ''''''''''''''''''''''''''''''
    'todo SORT THIS OUT NAD MOVE PART OF IT CONSTANTS FILES
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
