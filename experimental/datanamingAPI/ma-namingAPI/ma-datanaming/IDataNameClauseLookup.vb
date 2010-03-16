
Public Interface IDataNameClauseLookup

    'Public const geoExtentTableName = "datanaming_clause_geoextent"
    'Public const dataCategoryTableName = "datanaming_clause_data_categories"
    'Public const dataThemeTableName = "datanaming_clause_data_theme"
    'Public const dataTypeTableName = "datanaming_clause_data_type"
    'Public const scaleTableName = "datanaming_clause_scale"
    'Public const sourceTableName = "datanaming_clause_source"
    'Public const permissionsTableName = "datanaming_clause_permission"

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
    Function getGeoExtentList() As List(Of String)

    Function getDataCategoryList() As List(Of String)

    Function getDataThemeList() As List(Of String)

    Function getDataTypeList() As List(Of String)

    Function getScaleCodesList() As List(Of String)

    Function getSourceCodesList() As List(Of String)

    Function getPermissionsList() As List(Of String)

    'Get the full tables including dscriptions etc
    Sub getGeoExtentTable()

    Sub getDataCategoryTable()

    Sub getDataThemeTable()

    Sub getDataTypeTable()

    Sub getScaleCodesTable()

    Sub getSourceCodesTable()

    Sub getPermissionsTable()


End Interface
