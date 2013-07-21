
Public Interface IDataNameClauseLookup

    ''' <summary>
    ''' Test whether the Data Name Clause tables can be written to or not
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
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
    Function getGeoExtentTable() As DataTable

    Function getDataCategoryTable() As DataTable

    Function getDataThemeTable() As DataTable

    Function getDataTypeTable() As DataTable

    Function getScaleCodesTable() As DataTable

    Function getSourceCodesTable() As DataTable

    Function getPermissionsTable() As DataTable

    Function getDetails() As String

End Interface
