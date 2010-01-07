
Public Class DataNameCodeLookup

    Public geoExtentTableName = "datanaming_geoextent"
    Public dataCategoryTableName = "datanaming_data_categories"
    Public dataThemeTableName = "datanaming_data_theme"
    Public dataTypeTableName = "datanaming_data_type_codes"
    Public scaleTableName = "datanaming_scale_codes"
    Public sourceTableName = "datanaming_scale_codes"
    Public permissionsTableName = "datanaming_permission"

    Private gdbcon As GeoDataSourceConnection


    Sub GetGeoExtentList()

    End Sub

    Sub GetDataCategoryList()

    End Sub

    Sub GetDataThemeList()

    End Sub

    Sub GetDataTypeList()

    End Sub

    Sub GetScaleCodesList()

    End Sub

    Sub GetSourceCodesList()

    End Sub

    Sub GetPermissionsList()

    End Sub


    Public Sub New()
        gdbcon = New GeoDBConnection

    End Sub
End Class
