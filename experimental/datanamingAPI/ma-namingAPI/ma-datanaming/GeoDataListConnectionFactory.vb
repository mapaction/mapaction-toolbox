
Public Class GeoDataListConnectionFactory
    Shared myFactory As GeoDataListConnectionFactory

    Private Sub New()

    End Sub

    Public Shared Function getFactory() As GeoDataListConnectionFactory
        If myFactory Is Nothing Then
            myFactory = New GeoDataListConnectionFactory
        End If

        getFactory = myFactory
    End Function

    Public Function createGeoDataListConnection(ByVal listType As Integer, Optional ByRef args As String() = Nothing) As IGeoDataListConnection

    End Function



End Class
