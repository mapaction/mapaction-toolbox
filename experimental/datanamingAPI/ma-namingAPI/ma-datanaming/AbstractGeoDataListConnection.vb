Imports System.IO

Public MustInherit Class AbstractGeoDataListConnection
    Implements IGeoDataListConnection

    Private doRecusion As Boolean = True

    Protected Sub New()

    End Sub

    Public MustOverride Function doesLayerExist(ByVal layerName As String) As Boolean Implements IGeoDataListConnection.doesLayerExist

    Public MustOverride Function getDefaultDataNameClauseLookup() As IDataNameClauseLookup Implements IGeoDataListConnection.getDefaultDataNameClauseLookup

    Public MustOverride Function getDetails() As String Implements IGeoDataListConnection.getDetails

    Public MustOverride Function getGeoDataListConnectionType() As Integer Implements IGeoDataListConnection.getGeoDataListConnectionType

    Public MustOverride Function getGeoDataListConnectionTypeDesc() As String Implements IGeoDataListConnection.getGeoDataListConnectionTypeDesc

    Public MustOverride Function getLayerNamesStrings() As List(Of String) Implements IGeoDataListConnection.getLayerNamesStrings

    Public Function getLayerDataNamesList() As List(Of IDataName) Implements IGeoDataListConnection.getLayerDataNamesList
        Return getLayerDataNamesList(getDefaultDataNameClauseLookup())
    End Function

    Public Sub setRecuse(ByVal recuse As Boolean) Implements IGeoDataListConnection.setRecuse
        doRecusion = recuse
    End Sub

    Public Function getRecuse() As Boolean Implements IGeoDataListConnection.getRecuse
        Return doRecusion
    End Function

    Public MustOverride Function getLayerDataNamesList(ByRef myDNCL As IDataNameClauseLookup) As List(Of IDataName) Implements IGeoDataListConnection.getLayerDataNamesList

End Class
