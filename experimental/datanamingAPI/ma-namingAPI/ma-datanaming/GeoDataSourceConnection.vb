
Public MustInherit Class GeoDataSourceConnection





    Public MustOverride Sub Connect()

    Public MustOverride Sub Disconnect()

    Public MustOverride Function GetDetails() As String

    Public MustOverride Function GetDatasetList() As List(Of String)


    Public MustOverride Function GetTable(ByVal tableName As String) As Object

End Class
