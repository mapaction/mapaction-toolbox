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

    ''' <summary>
    ''' The GeoDataListConnection should attempt to locate the Data Names Clause Tables physically either
    ''' on the filesystem or in the GDB as apropriate. It should then return an IDataNameClauseLooup object
    ''' as appropriate.
    ''' 
    ''' If the Data Names Clause Tables cannot be physically located becuase either they do not exist or there
    ''' is not a uquine location (eg in the case of a MXD), then a exception is raised.
    ''' </summary>
    ''' <returns>IDataNameClauseLooup</returns>
    ''' <remarks></remarks>
    Function getDefaultDataNameClauseLookup() As IDataNameClauseLookup

End Interface
