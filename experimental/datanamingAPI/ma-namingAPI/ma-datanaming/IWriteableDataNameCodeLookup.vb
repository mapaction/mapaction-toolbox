
Public Interface IWriteableDataNamingCodeLookup
    Inherits IDataNameClauseLookup

    Sub DeleteDataCategory()

    Sub AddDataCategoryClause()

    Sub DeleteDataThemeClause()

    Sub AddDataThemeClause()

    Sub AddDataTypeClause()

    Sub DeleteDataTypeClause()

    Sub DeleteGeoExtentClause()

    Sub AddGeoExtentClause()

    Sub AddPermissionClause()

    Sub DeletePermissionClause()

    Sub AddScaleClause()

    Sub DeleteScaleClause()

    Sub AddSourceClause()

    Sub DeleteSourceClause()

End Interface

