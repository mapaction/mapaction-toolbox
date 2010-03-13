
Public MustInherit Class AbstractDataNameClauseLookup
    Implements IDataNameClauseLookup


    Public Sub GetDataCategoryList() Implements IDataNameClauseLookup.GetDataCategoryList

    End Sub

    Public Sub GetDataCategoryTable() Implements IDataNameClauseLookup.GetDataCategoryTable

    End Sub

    Public Sub GetDataThemeList() Implements IDataNameClauseLookup.GetDataThemeList

    End Sub

    Public Sub GetDataThemeTable() Implements IDataNameClauseLookup.GetDataThemeTable

    End Sub

    Public Sub GetDataTypeList() Implements IDataNameClauseLookup.GetDataTypeList

    End Sub

    Public Sub GetDataTypeTable() Implements IDataNameClauseLookup.GetDataTypeTable

    End Sub

    Public Sub GetGeoExtentList() Implements IDataNameClauseLookup.GetGeoExtentList

    End Sub

    Public Sub GetGeoExtentTable() Implements IDataNameClauseLookup.GetGeoExtentTable

    End Sub

    Public Sub GetPermissionsList() Implements IDataNameClauseLookup.GetPermissionsList

    End Sub

    Public Sub GetPermissionsTable() Implements IDataNameClauseLookup.GetPermissionsTable

    End Sub

    Public Sub GetScaleCodesList() Implements IDataNameClauseLookup.GetScaleCodesList

    End Sub

    Public Sub GetScaleCodesTable() Implements IDataNameClauseLookup.GetScaleCodesTable

    End Sub

    Public Sub GetSourceCodesList() Implements IDataNameClauseLookup.GetSourceCodesList

    End Sub

    Public Sub GetSourceCodesTable() Implements IDataNameClauseLookup.GetSourceCodesTable

    End Sub

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
