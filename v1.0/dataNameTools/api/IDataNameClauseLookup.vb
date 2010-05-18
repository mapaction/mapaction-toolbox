Imports System.IO

' todo double check the name of the XXXX factory class
' todo double check the name of the YYYY enumeration
''' <summary>
''' Classes implementing this interface provide a means to interact with a collection of flat database
''' tables, which collectively, list valid values for each individual clause in a data name.
''' </summary>
''' <remarks>
''' Classes implementing this interface provide a means to interact with a collection of flat database
''' tables, which collectively, list valid values for each individual clause in a data name.
''' 
''' The names of the relevant tables are:
'''     "datanaming_clause_geoextent"
'''     "datanaming_clause_data_categories"
'''     "datanaming_clause_data_theme"
'''     "datanaming_clause_data_type"
'''     "datanaming_clause_scale"
'''     "datanaming_clause_source"
'''     "datanaming_clause_permission"
''' (these are defined in the YYYY enumeration)
''' 
''' It is not expected that that will be any public constructors for classes implenementing IDataNameClauseLookup.
''' In general, instances of IDataNameClauseLookup should be obtained either by use of the 
''' IDataList.getDefaultDataNameClauseLookup() method or by using a correponding XXXX Factory class.
''' </remarks>
Public Interface IDataNameClauseLookup

    ''' <summary>
    ''' Tests whether the testGeoExtentClause is listed in the datanaming_clause_geoextent table.
    ''' </summary>
    ''' <param name="testGeoExtentClause">The test clause</param>
    ''' <returns>TRUE if the testGeoExtentClause is found in the relevant table, FALSE otherwise.</returns>
    ''' <remarks>
    ''' Tests whether the testGeoExtentClause is listed in the datanaming_clause_geoextent table.
    ''' </remarks>
    Function isvalidGeoextentClause(ByVal testGeoExtentClause As String) As Boolean

    ''' <summary>
    ''' Tests whether the testDataCatClause is listed in the datanaming_clause_data_categories table.
    ''' </summary>
    ''' <param name="testDataCatClause">The test clause</param>
    ''' <returns>TRUE if the testDataCatClause is found in the relevant table, FALSE otherwise.</returns>
    ''' <remarks>
    ''' Tests whether the testDataCatClause is listed in the datanaming_clause_data_categories table.
    ''' </remarks>
    Function isvalidDataCategoryClause(ByVal testDataCatClause As String) As Boolean

    ''' <summary>
    ''' Tests whether the testDataThemeClause is listed in the datanaming_clause_data_theme table.
    ''' </summary>
    ''' <param name="testDataThemeClause">The test clause</param>
    ''' <returns>TRUE if the testDataThemeClause is found in the relevant table, FALSE otherwise.</returns>
    ''' <remarks>
    ''' Tests whether the testDataThemeClause is listed in the datanaming_clause_data_theme table.
    ''' </remarks>
    Function isvalidDataThemeClause(ByVal testDataThemeClause As String, ByVal testDataCatClause As String) As Boolean

    'todo. Add ref to the For that see .....
    ''' <summary>
    ''' Tests whether the testDataTypeClause is listed in the datanaming_clause_data_type table.
    ''' </summary>
    ''' <param name="testDataTypeClause">The test clause</param>
    ''' <returns>TRUE if the testDataTypeClause is found in the relevant table, FALSE otherwise.</returns>
    ''' <remarks>
    ''' Tests whether the testDataTypeClause is listed in the datanaming_clause_data_type table.
    ''' 
    ''' Note that this does NOT test whether or not the testDataTypeClause matches the data type 
    ''' of a particular layer. For that see .....
    ''' </remarks>
    Function isvalidDataTypeClause(ByVal testDataTypeClause As String) As Boolean

    ''' <summary>
    ''' Tests whether the testScaleClause is listed in the datanaming_clause_scale table.
    ''' </summary>
    ''' <param name="testScaleClause">The test clause</param>
    ''' <returns>TRUE if the testScaleClause is found in the relevant table, FALSE otherwise.</returns>
    ''' <remarks>
    ''' Tests whether the testScaleClause is listed in the datanaming_clause_scale table.
    ''' </remarks>
    Function isvalidScaleClause(ByVal testScaleClause As String) As Boolean

    ''' <summary>
    ''' Tests whether the testSourceClause is listed in the datanaming_clause_source table.
    ''' </summary>
    ''' <param name="testSourceClause">The test clause</param>
    ''' <returns>TRUE if the testSourceClause is found in the relevant table, FALSE otherwise.</returns>
    ''' <remarks>
    ''' Tests whether the testSourceClause is listed in the datanaming_clause_source table.
    ''' </remarks>
    Function isvalidSourceClause(ByVal testSourceClause As String) As Boolean

    ''' <summary>
    ''' Tests whether the testPermissionsClause is listed in the datanaming_clause_permission table.
    ''' </summary>
    ''' <param name="testPermissionsClause">The test clause</param>
    ''' <returns>TRUE if the testPermissionsClause is found in the relevant table, FALSE otherwise.</returns>
    ''' <remarks>
    ''' Tests whether the testPermissionsClause is listed in the datanaming_clause_permission table.
    ''' </remarks>
    Function isvalidPermissionsClause(ByVal testPermissionsClause As String) As Boolean

    ''' <summary>
    ''' This method returns a simple list of all of the valid GeoExtent clauses, as Strings, from the
    ''' datanaming_clause_geoextent table.
    ''' </summary>
    ''' <returns>A list of clauses as Strings, without any extra information.</returns>
    ''' <remarks>
    ''' This method returns a simple list of all of the valid GeoExtent clauses, as Strings, from the
    ''' datanaming_clause_geoextent table.
    ''' </remarks>
    Function getGeoExtentList() As List(Of String)

    ''' <summary>
    ''' This method returns a simple list of all of the valid GeoExtent clauses, as Strings, from the
    ''' datanaming_clause_data_categories table.
    ''' </summary>
    ''' <returns>A list of clauses as Strings, without any extra information.</returns>
    ''' <remarks>
    ''' This method returns a simple list of all of the valid GeoExtent clauses, as Strings, from the
    ''' datanaming_clause_data_categories table.
    ''' </remarks>
    Function getDataCategoryList() As List(Of String)

    ''' <summary>
    ''' This method returns a simple list of all of the valid GeoExtent clauses, as Strings, from the
    ''' datanaming_clause_data_theme table.
    ''' </summary>
    ''' <returns>A list of clauses as Strings, without any extra information.</returns>
    ''' <remarks>
    ''' This method returns a simple list of all of the valid GeoExtent clauses, as Strings, from the
    ''' datanaming_clause_data_theme table.
    ''' </remarks>
    Function getDataThemeList() As List(Of String)

    ''' <summary>
    ''' This method returns a simple list of all of the valid GeoExtent clauses, as Strings, from the
    ''' datanaming_clause_data_type table.
    ''' </summary>
    ''' <returns>A list of clauses as Strings, without any extra information.</returns>
    ''' <remarks>
    ''' This method returns a simple list of all of the valid GeoExtent clauses, as Strings, from the
    ''' datanaming_clause_data_type table.
    ''' </remarks>
    Function getDataTypeList() As List(Of String)

    ''' <summary>
    ''' This method returns a simple list of all of the valid GeoExtent clauses, as Strings, from the
    ''' datanaming_clause_scale table.
    ''' </summary>
    ''' <returns>A list of clauses as Strings, without any extra information.</returns>
    ''' <remarks>
    ''' This method returns a simple list of all of the valid GeoExtent clauses, as Strings, from the
    ''' datanaming_clause_scale table.
    ''' </remarks>
    Function getScaleCodesList() As List(Of String)

    ''' <summary>
    ''' This method returns a simple list of all of the valid GeoExtent clauses, as Strings, from the
    ''' datanaming_clause_source table.
    ''' </summary>
    ''' <returns>A list of clauses as Strings, without any extra information.</returns>
    ''' <remarks>
    ''' This method returns a simple list of all of the valid GeoExtent clauses, as Strings, from the
    ''' datanaming_clause_source table.
    ''' </remarks>
    Function getSourceCodesList() As List(Of String)


    ''' <summary>
    ''' This method returns a simple list of all of the valid GeoExtent clauses, as Strings, from the
    ''' datanaming_clause_permission table.
    ''' </summary>
    ''' <returns>A list of clauses as Strings, without any extra information.</returns>
    ''' <remarks>
    ''' This method returns a simple list of all of the valid GeoExtent clauses, as Strings, from the
    ''' datanaming_clause_permission table.
    ''' </remarks>
    Function getPermissionsList() As List(Of String)


    ''' <summary>
    ''' This method returns a full bindable version of the datanaming_clause_geoextent table.
    ''' </summary>
    ''' <returns>A full bindable version of the datanaming_clause_geoextent table.</returns>
    ''' <remarks>
    ''' This method returns a full bindable version of the datanaming_clause_geoextent table.
    ''' 
    ''' This includes the clause and description columns plus any other columns included in the 
    ''' datanaming_clause_geoextent table.
    ''' </remarks>
    Function getGeoExtentTable() As DataTable

    ''' <summary>
    ''' This method returns a full bindable version of the datanaming_clause_data_categories table.
    ''' </summary>
    ''' <returns>A full bindable version of the datanaming_clause_data_categories table.</returns>
    ''' <remarks>
    ''' This method returns a full bindable version of the datanaming_clause_data_categories table.
    ''' 
    ''' This includes the clause and description columns plus any other columns included in the 
    ''' datanaming_clause_data_categories table.
    ''' </remarks>
    Function getDataCategoryTable() As DataTable

    ''' <summary>
    ''' This method returns a full bindable version of the datanaming_clause_data_theme table.
    ''' </summary>
    ''' <returns>A full bindable version of the datanaming_clause_data_theme table.</returns>
    ''' <remarks>
    ''' This method returns a full bindable version of the datanaming_clause_data_theme table.
    ''' 
    ''' This includes the clause and description columns plus any other columns included in the 
    ''' datanaming_clause_data_theme table.
    ''' </remarks>
    Function getDataThemeTable() As DataTable

    ''' <summary>
    ''' This method returns a full bindable version of the datanaming_clause_data_type table.
    ''' </summary>
    ''' <returns>A full bindable version of the datanaming_clause_data_type table.</returns>
    ''' <remarks>
    ''' This method returns a full bindable version of the datanaming_clause_data_type table.
    ''' 
    ''' This includes the clause and description columns plus any other columns included in the 
    ''' datanaming_clause_data_type table.
    ''' </remarks>
    Function getDataTypeTable() As DataTable

    ''' <summary>
    ''' This method returns a full bindable version of the datanaming_clause_scale table.
    ''' </summary>
    ''' <returns>A full bindable version of the datanaming_clause_scale table.</returns>
    ''' <remarks>
    ''' This method returns a full bindable version of the datanaming_clause_scale table.
    ''' 
    ''' This includes the clause and description columns plus any other columns included in the 
    ''' datanaming_clause_scale table.
    ''' </remarks>
    Function getScaleCodesTable() As DataTable

    ''' <summary>
    ''' This method returns a full bindable version of the datanaming_clause_source table.
    ''' </summary>
    ''' <returns>A full bindable version of the datanaming_clause_source table.</returns>
    ''' <remarks>
    ''' This method returns a full bindable version of the datanaming_clause_source table.
    ''' 
    ''' This includes the clause and description columns plus any other columns included in the 
    ''' datanaming_clause_source table.
    ''' </remarks>
    Function getSourceCodesTable() As DataTable

    ''' <summary>
    ''' This method returns a full bindable version of the datanaming_clause_permission table.
    ''' </summary>
    ''' <returns>A full bindable version of the datanaming_clause_permission table.</returns>
    ''' <remarks>
    ''' This method returns a full bindable version of the datanaming_clause_permission table.
    ''' 
    ''' This includes the clause and description columns plus any other columns included in the 
    ''' datanaming_clause_permission table.
    ''' </remarks>
    Function getPermissionsTable() As DataTable

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
    Function getDetails() As String

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
    Function getPath() As FileInfo

End Interface
