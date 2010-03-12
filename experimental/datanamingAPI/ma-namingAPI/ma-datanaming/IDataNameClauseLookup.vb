
Public Interface IDataNameClauseLookup

    'Public const geoExtentTableName = "datanaming_geoextent"
    'Public const dataCategoryTableName = "datanaming_data_categories"
    'Public const dataThemeTableName = "datanaming_data_theme"
    'Public const dataTypeTableName = "datanaming_data_type_codes"
    'Public const scaleTableName = "datanaming_scale_codes"
    'Public const sourceTableName = "datanaming_scale_codes"
    'Public const permissionsTableName = "datanaming_permission"

    'Test whether the Data Name Clause tables can be written to or not
    Function isWriteable() As Boolean

    Function isvalidGeoextentClause(ByVal testGeoExtentClause As String) As Boolean

    Function isvalidDataCategoryClause(ByVal testDataCatClause As String) As Boolean

    Function isvalidDataThemeClause(ByVal testDataThemeClause As String, ByVal testDataCatClause As String) As Boolean

    Function isvalidDataTypeClause(ByVal testDataTypeClause As String) As Boolean

    Function isvalidScaleClause(ByVal testScaleClause As String) As Boolean

    Function isvalidSourceClause(ByVal testSourceClause As String) As Boolean

    Function isvalidPermissionsClause(ByVal testPermissionsClause As String) As Boolean

    'Get just the list of clauses, without any extra information
    Sub GetGeoExtentList()

    Sub GetDataCategoryList()

    Sub GetDataThemeList()

    Sub GetDataTypeList()

    Sub GetScaleCodesList()

    Sub GetSourceCodesList()

    Sub GetPermissionsList()

    'Get the full tables including dscriptions etc
    Sub GetGeoExtentTable()

    Sub GetDataCategoryTable()

    Sub GetDataThemeTable()

    Sub GetDataTypeTable()

    Sub GetScaleCodesTable()

    Sub GetSourceCodesTable()

    Sub GetPermissionsTable()


End Interface
