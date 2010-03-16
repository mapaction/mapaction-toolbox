
Public MustInherit Class AbstractGeoDataListConnection
    Implements IGeoDataListConnection


    Public Sub Connect() Implements IGeoDataListConnection.Connect

    End Sub

    Public Sub Disconnect() Implements IGeoDataListConnection.Disconnect

    End Sub

    Public Function doesLayerExist(ByVal layerName As String) As Boolean Implements IGeoDataListConnection.doesLayerExist

    End Function

    Public Function getDefaultDataNameClauseLookup() As IDataNameClauseLookup Implements IGeoDataListConnection.getDefaultDataNameClauseLookup

    End Function

    Public Function getDetails() As String Implements IGeoDataListConnection.getDetails

    End Function

    Public Function getGeoDataListConnectionType() As Integer Implements IGeoDataListConnection.getGeoDataListConnectionType

    End Function

    Public Function getGeoDataListConnectionTypeDesc() As String Implements IGeoDataListConnection.getGeoDataListConnectionTypeDesc

    End Function

    Public Function getLayerNamesStrings() As List(Of String) Implements IGeoDataListConnection.getLayerNamesStrings

    End Function

    Public Function getLayerDataNamesList() As List(Of IDataName) Implements IGeoDataListConnection.getLayerDataNamesList

    End Function

    Public Function getLayerDataNamesList(ByRef myDNCL As IDataNameClauseLookup) As List(Of IDataName) Implements IGeoDataListConnection.getLayerDataNamesList

    End Function

End Class
