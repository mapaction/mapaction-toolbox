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

Imports System.Data
Imports System.Data.Common
Imports System.IO
Imports System.Configuration
Imports System.Xml


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

    Private m_DataSet As DataSet

    Private m_dtbGeoExtentClauses As DataTable
    Private m_dtbDataCategoriesClauses As DataTable
    Private m_dtbDataThemeClauses As DataTable
    Private m_dtbDataTypeClauses As DataTable
    Private m_dtbScaleClauses As DataTable
    Private m_dtbSourceClauses As DataTable
    Private m_dtbPermissionClauses As DataTable
    'Private doRecusion As Boolean

    Protected m_lngReadWriteMode As Long

    Protected Sub New()
        'initialiseAllTables()
    End Sub

    Friend ReadOnly Property dataSet() As DataSet
        Get
            Return m_DataSet
        End Get
    End Property

    '''' <summary>
    '''' Opens each of the dataname clause lookup tables. Must be called from the constructor
    '''' </summary>
    '''' <remarks>
    '''' Opens each of the dataname clause lookup tables. Must be called from the constructor
    '''' 
    '''' Uses the subclasses implenmentation of the openTable() method.
    '''' </remarks>
    'Protected Sub initialiseAllTables()
    '    Dim dtbCurrent As DataTable
    '    Dim arylColDetails As ArrayList

    '    For Each strTableName In g_strAryClauseTableNames
    '        Try
    '            dtbCurrent = openTable(strTableName)
    '            arylColDetails = New ArrayList

    '            For Each col In dtbCurrent.Columns
    '                arylColDetails.Add(col)
    '            Next

    '            If doDataColumnsMatch(CType(g_htbAllDataNameColumns.Item(strTableName), ArrayList), arylColDetails) Then
    '                Select Case strTableName
    '                    Case TABLENAME_GEOEXTENT
    '                        m_dtbGeoExtentClauses = dtbCurrent

    '                    Case TABLENAME_DATA_CAT
    '                        m_dtbDataCategoriesClauses = dtbCurrent

    '                    Case TABLENAME_DATA_THEME
    '                        m_dtbDataThemeClauses = dtbCurrent

    '                    Case TABLENAME_DATA_TYPE
    '                        m_dtbDataTypeClauses = dtbCurrent

    '                    Case TABLENAME_SCALE
    '                        m_dtbScaleClauses = dtbCurrent

    '                    Case TABLENAME_SOURCE
    '                        m_dtbSourceClauses = dtbCurrent

    '                    Case TABLENAME_PERMISSION
    '                        m_dtbPermissionClauses = dtbCurrent

    '                End Select
    '            End If
    '        Catch ex As Exception
    '            Throw New LookupTableException(dnLookupTableError.general, strTableName, ex)
    '        End Try

    '    Next
    'End Sub

    Protected Friend MustOverride Function getDBDataAdapter() As DbDataAdapter


    Protected Sub initialiseAllTables()
        Dim ds As DataSet
        Dim da As DbDataAdapter
        Dim strQuery As String
        Dim strDSName As String
        Dim strSchemaFileName As String
        Dim strXMLSchema As String
        Dim txtRdrXMLSchema As TextReader
        Dim dtm As DataTableMapping
        Dim dcl As DataColumn

        ''
        'Get various information about the Dataset name and schema
        'strDSName = System.Configuration.ConfigurationManager.AppSettings.Item(APP_CONF_DNCL_DATASET_NAME)
        'strSchemaFileName = System.Configuration.ConfigurationManager.AppSettings.Item(APP_CONF_SCHEMA_FILENAME)
        strDSName = DNCL_DATASET_NAME
        strSchemaFileName = SCHEMA_FILENAME

        'System.Console.WriteLine("strDSName : " & strDSName)
        'System.Console.WriteLine("strSchemaFileName : " & strSchemaFileName)

        My.Resources.ResourceManager.IgnoreCase = True
        'strXMLSchema = My.Resources.ResourceManager.GetString(strSchemaFileName)
        strXMLSchema = My.Resources.datanameclauselookup_schema_v1_0

        txtRdrXMLSchema = New StringReader(strXMLSchema)

        ''
        'Create the new Dataset object
        ds = New DataSet(strDSName)
        ds.ReadXmlSchema(txtRdrXMLSchema)

        'ds.WriteXmlSchema(System.Console.OpenStandardOutput())
        'System.Console.WriteLine()
        'System.Console.WriteLine()

        ''
        'Now get the DataAdapter from the implementing subclass
        da = getDBDataAdapter()

        'todo HIGH: resovle DataAdapter.MissingMappingAction problem
        ' Since the schema has already been read above, then I would
        ' expect this to work with the setting:
        '   da.MissingMappingAction = MissingMappingAction.Error
        ' but it doesn't for some reason. No idea why...!
        da.MissingMappingAction = MissingMappingAction.Passthrough
        da.MissingSchemaAction = MissingSchemaAction.Add
        da.FillLoadOption = LoadOption.OverwriteChanges

        Try
            'Now load each of the data name clause lookup tables
            For Each strTableName In g_strAryClauseTableNames
                'Nice simple select querry
                strQuery = "SELECT * FROM " & strTableName
                da.SelectCommand.CommandText = strQuery

                'Set up the mappings
                dtm = da.TableMappings.Add(strTableName, strTableName)

                For i As Integer = 0 To ds.Tables.Item(strTableName).Columns.Count - 1
                    dcl = ds.Tables.Item(strTableName).Columns.Item(i)
                    dtm.ColumnMappings.Add(dcl.ColumnName, dcl.ColumnName)
                Next

                'And fill using the dataadapter
                da.Fill(ds, strTableName)
            Next

            'set the global m_dataSet paramter
            m_DataSet = ds

            'Now setup refs to the individual tables. This step is could probably be
            'removed and just directly ref ds.Tables.Item through the class.
            m_dtbGeoExtentClauses = ds.Tables.Item(TABLENAME_GEOEXTENT)
            m_dtbDataCategoriesClauses = ds.Tables.Item(TABLENAME_DATA_CAT)
            m_dtbDataThemeClauses = ds.Tables.Item(TABLENAME_DATA_THEME)
            m_dtbDataTypeClauses = ds.Tables.Item(TABLENAME_DATA_TYPE)
            m_dtbScaleClauses = ds.Tables.Item(TABLENAME_SCALE)
            m_dtbSourceClauses = ds.Tables.Item(TABLENAME_SOURCE)
            m_dtbPermissionClauses = ds.Tables.Item(TABLENAME_PERMISSION)


            'ds.WriteXmlSchema("D:\MapAction\bronze\custom_tools\managedcode\mapaction-toolbox\" & _
            '                  "experimental\datanamingAPI\ma-namingAPI\CommandlineTesting\schema.xml")

            'ds.WriteXml("D:\MapAction\bronze\custom_tools\managedcode\mapaction-toolbox\" & _
            '            "experimental\datanamingAPI\ma-namingAPI\CommandlineTesting\output.xml")

        Catch sysEx As SystemException
            Throw New LookupTableException(dnLookupTableError.general, strDSName, sysEx)
        End Try

    End Sub

    '''' <summary>
    '''' This method provides an implenmention specific means to open an individual flat table
    '''' from a data source (eg, Access DB, ESRI GDB, XML files etc).
    '''' </summary>
    '''' <param name="strTableName">The name of the table to open. This should normally be passed
    '''' using one of the API constants with the prefix "TABLENAME_"</param>
    '''' <returns>A DataTable object representing the named table</returns>
    '''' <remarks>
    '''' This method provides an implenmention specific means to open an individual flat table
    '''' from a data source (eg, Access DB, ESRI GDB, XML files etc).
    '''' </remarks>
    'Protected MustOverride Function openTable(ByVal strTableName As String) As DataTable

    ''' <summary>
    ''' Returns a string describing the storage location of the dataname clause tables.
    ''' </summary>
    ''' <returns>A string describing the storage location of the dataname clause tables.</returns>
    ''' <remarks>
    ''' Returns a string describing the storage location of the dataname clause tables.
    ''' 
    ''' This may be the operating system file path if appropriate or a RDMS connection
    ''' string, or a URL etc.
    ''' </remarks>
    Public MustOverride Function getDetails() As String Implements IDataNameClauseLookup.getDetails

    ''' <summary>
    ''' Returns the operating system file path to the container of the dataname clause tables.
    ''' </summary>
    ''' <returns>A FileInfo object representing the operating system file path to the container 
    ''' of the dataname clause tables.</returns>
    ''' <remarks>
    ''' Returns the operating system file path to the container of the dataname clause tables.
    ''' 
    ''' If the location of these tables cannot represented as an operating system file (eg if they 
    ''' are located in a RDBMS) then the Nothing object is returned.
    ''' </remarks>
    Public MustOverride Function getPath() As FileInfo Implements IDataNameClauseLookup.getPath

    '''' <summary>
    '''' Compared two ArrayLists of DataColumn objects, to confirm whether or not the specfication
    '''' of the columns (ie the schema) is identical for both.
    '''' </summary>
    '''' <param name="aryl1">An ArrayList of DataColumn objects</param>
    '''' <param name="aryl2">An ArrayList of DataColumn objects</param>
    '''' <returns>TRUE = the DataColumn specfications match. If the DataColumn specfications
    '''' do not match then a LookupTableException is thrown.</returns>
    '''' <remarks>
    '''' Compared two ArrayLists of DataColumn objects, to confirm whether or not the specfication
    '''' of the columns (ie the schema) is identical for both. This is used to verify the schema of
    '''' any Data Name Clause Lookup Tables that have been opened.
    '''' 
    '''' Is this really the best way to check the schema of the DB? probably could be done better using the XML schema definition stuff in ADO.NET
    '''' Columns *must* be in the same order.
    '''' </remarks>
    'Private Function doDataColumnsMatch(ByRef aryl1 As ArrayList, ByRef aryl2 As ArrayList) As Boolean
    '    Dim blnOverallResult As Boolean
    '    Dim blnCurColMatch As Boolean 'The result of the current column comparision
    '    Dim enuExcepReason As dnLookupTableError
    '    Dim strMismatchColDesc As String 'description of the name and table of a failed column
    '    Dim dc1 As DataColumn, dc2 As DataColumn

    '    'todo MEDIUM: Adjust doDataColumnsMatch to allow for optional ObjectID column

    '    'assume true until proved otherwise
    '    blnOverallResult = True

    '    If aryl1.Count <> aryl2.Count Then
    '        'returnRes = False
    '        Throw New LookupTableException(dnLookupTableError.wrongNoOfCols, _
    '                                       CStr(aryl1.Count & " vs " & aryl2.Count))
    '    Else
    '        For intCurIndx = 0 To (aryl1.Count - 1)
    '            dc1 = CType(aryl1.Item(intCurIndx), DataColumn)
    '            dc2 = CType(aryl2.Item(intCurIndx), DataColumn)
    '            blnCurColMatch = False

    '            Select Case True
    '                Case dc1.ColumnName <> dc2.ColumnName
    '                    enuExcepReason = dnLookupTableError.colNamesMismatch
    '                Case dc1.DataType.FullName <> dc2.DataType.FullName
    '                    enuExcepReason = dnLookupTableError.colTypeMismatch
    '                Case dc1.MaxLength <> dc2.MaxLength
    '                    enuExcepReason = dnLookupTableError.colLenthMismatch
    '                Case dc1.Unique <> dc2.Unique
    '                    enuExcepReason = dnLookupTableError.colUniqueReqMismatch
    '                Case dc1.AutoIncrement <> dc2.AutoIncrement
    '                    enuExcepReason = dnLookupTableError.colAutoIncrementMismatch
    '                Case dc1.Caption <> dc2.Caption
    '                    enuExcepReason = dnLookupTableError.colCaptionMismatch
    '                Case dc1.ReadOnly <> dc2.ReadOnly
    '                    enuExcepReason = dnLookupTableError.colReadOnlyMismatch
    '                Case Else
    '                    blnCurColMatch = True
    '            End Select


    '            If Not blnCurColMatch Then
    '                strMismatchColDesc = "Table1: " & dc1.Table.TableName & _
    '                                     "Column1: " & dc1.ColumnName & _
    '                                     "Table2: " & dc2.Table.TableName & _
    '                                     "Column2: " & dc2.ColumnName

    '                Throw New LookupTableException(enuExcepReason, strMismatchColDesc)
    '            End If
    '        Next
    '    End If

    '    Return blnOverallResult
    'End Function


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

    'todo: HIGH Rewrite this using dt.select() method!
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
    Public Function isValidDataCategoryClause(ByVal strTestDataCatClause As String) As Boolean Implements IDataNameClauseLookup.isValidDataCategoryClause
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
                          As Boolean Implements IDataNameClauseLookup.isValidDataThemeClause

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
                                            Implements IDataNameClauseLookup.isValidDataTypeClause
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
    Public Function isValidGeoextentClause(ByVal strTestGeoExtentClause As String) As Boolean Implements IDataNameClauseLookup.isValidGeoextentClause
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
    Public Function isValidPermissionsClause(ByVal strTestPermissionsClause As String) As Boolean Implements IDataNameClauseLookup.isValidPermissionsClause
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
    Public Function isValidScaleClause(ByVal strTestScaleClause As String) As Boolean Implements IDataNameClauseLookup.isValidScaleClause
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
    Public Function isValidSourceClause(ByVal strTestSourceClause As String) As Boolean Implements IDataNameClauseLookup.isValidSourceClause
        isValidSourceClause = isStrInPriKeyCol(strTestSourceClause, m_dtbSourceClauses)
    End Function

End Class
