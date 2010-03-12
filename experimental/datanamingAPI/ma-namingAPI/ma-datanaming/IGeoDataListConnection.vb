
Public Interface IGeoDataListConnection

    'TYPE_MXD (what about multiple data frames)?
    'TYPE_GEODATABASE
    'TYPE_DIR_SHP_FILES
    'TYPE_UNKNOWN

    Sub Connect()

    Sub Disconnect()

    Function getGeoDataListConnectionType() As Integer

    Function getGeoDataListConnectionTypeDesc() As String

    Function getDetails() As String

    Function getLayerNamesList() As List(Of String)

    Function doesLayerExist(ByVal layerName As String) As Boolean

    Function getDefaultDataNameClauseLookup() As IDataNameClauseLookup

End Interface
