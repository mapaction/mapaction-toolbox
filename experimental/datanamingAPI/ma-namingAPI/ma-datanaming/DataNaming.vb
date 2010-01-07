Public Class DataNaming

    ' The general pattern of the naming convention is:
    '
    '  geoextent_datacategory_theme_datatype[_scale]_source[_permission][_FreeText]
    '

    Public Function IsValid(ByVal nameStr As String, ByVal myCon As GeoDataSourceConnection) As Boolean
    End Function

    Private Function AsArray(ByVal nameStr As String) As ArrayList
    End Function

    Private Function hasScaleClause(ByVal nameStr As String) As Boolean
    End Function

    Private Function hasPermissionClause(ByVal nameStr As String) As Boolean
    End Function

    Private Function hasFreeText(ByVal nameStr As String) As Boolean
    End Function


    '
    '
    '
    Public Function ChangeGeoExtent(ByVal dataname As String, ByVal newGeoExtent as String) as String
    End Function

    Public Function ChangeDataCategory(ByVal dataname As String, ByVal newDataCategory As String) As String
    End Function

    Public Function ChangeDataTheme(ByVal dataname As String, ByVal newDataTheme As String) As String
    End Function


End Class
