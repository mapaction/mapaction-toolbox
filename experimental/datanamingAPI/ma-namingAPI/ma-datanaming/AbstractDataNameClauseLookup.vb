
Public MustInherit Class AbstractDataNameClauseLookup
    Implements IDataNameClauseLookup


    Public MustOverride Function getDataCategoryList() As List(Of String) Implements IDataNameClauseLookup.getDataCategoryList

    Public MustOverride Sub getDataCategoryTable() Implements IDataNameClauseLookup.getDataCategoryTable

    Public MustOverride Function getDataThemeList() As List(Of String) Implements IDataNameClauseLookup.getDataThemeList

    Public MustOverride Sub getDataThemeTable() Implements IDataNameClauseLookup.getDataThemeTable

    Public MustOverride Function getDataTypeList() As List(Of String) Implements IDataNameClauseLookup.getDataTypeList

    Public MustOverride Sub getDataTypeTable() Implements IDataNameClauseLookup.getDataTypeTable

    Public MustOverride Function getGeoExtentList() As List(Of String) Implements IDataNameClauseLookup.getGeoExtentList

    Public MustOverride Sub getGeoExtentTable() Implements IDataNameClauseLookup.getGeoExtentTable

    Public MustOverride Function getPermissionsList() As List(Of String) Implements IDataNameClauseLookup.getPermissionsList

    Public MustOverride Sub getPermissionsTable() Implements IDataNameClauseLookup.getPermissionsTable

    Public MustOverride Function getScaleCodesList() As List(Of String) Implements IDataNameClauseLookup.getScaleCodesList

    Public MustOverride Sub getScaleCodesTable() Implements IDataNameClauseLookup.getScaleCodesTable

    Public MustOverride Function getSourceCodesList() As List(Of String) Implements IDataNameClauseLookup.getSourceCodesList

    Public MustOverride Sub getSourceCodesTable() Implements IDataNameClauseLookup.getSourceCodesTable

    Public Function isvalidDataCategoryClause(ByVal testDataCatClause As String) As Boolean Implements IDataNameClauseLookup.isvalidDataCategoryClause

    End Function

    Public Function isvalidDataThemeClause(ByVal testDataThemeClause As String, ByVal testDataCatClause As String) As Boolean Implements IDataNameClauseLookup.isvalidDataThemeClause

    End Function

    Public Function isvalidDataTypeClause(ByVal testDataTypeClause As String) As Boolean Implements IDataNameClauseLookup.isvalidDataTypeClause

    End Function

    Public Function isvalidGeoextentClause(ByVal testGeoExtentClause As String) As Boolean Implements IDataNameClauseLookup.isvalidGeoextentClause

    End Function

    Public Function isvalidPermissionsClause(ByVal testPermissionsClause As String) As Boolean Implements IDataNameClauseLookup.isvalidPermissionsClause

    End Function

    Public Function isvalidScaleClause(ByVal testScaleClause As String) As Boolean Implements IDataNameClauseLookup.isvalidScaleClause

    End Function

    Public Function isvalidSourceClause(ByVal testSourceClause As String) As Boolean Implements IDataNameClauseLookup.isvalidSourceClause

    End Function

    Public Function isWriteable() As Boolean Implements IDataNameClauseLookup.isWriteable

    End Function
End Class
