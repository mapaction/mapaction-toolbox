''' <summary>
''' This object represents a logical group of layers. They may be stored in one location (eg. GDB or
''' directory of shapefiles) or they may be used together (eg. in a map MXD)
''' </summary>
''' <remarks>
''' This object represents a logical group of layers. They may be stored in one location (eg. GDB or
''' directory of shapefiles) or they may be used together (eg. in a map MXD)
''' 
''' This interface is generally used along with two others, IDataNameClauseLookup and IDataName
''' </remarks>
Public Interface IGeoDataListConnection

    'TYPE_MXD (what about multiple data frames)?
    'TYPE_GEODATABASE
    'TYPE_DIR_SHP_FILES
    'TYPE_UNKNOWN

    Sub Connect()

    Sub Disconnect()

    ''' <summary>
    ''' Returns an Integer which represents the type of connection
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function getGeoDataListConnectionType() As Integer

    ''' <summary>
    ''' Returns a String which represents the type of connection
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function getGeoDataListConnectionTypeDesc() As String

    ''' <summary>
    ''' Returns a String which represents the type of connection. Typically this would be the path/URI to the data and or MXD etc.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function getDetails() As String

    ''' <summary>
    ''' Returns a List of strings representing the names of all of the layers defined
    ''' by this GeoDataListConnection. 
    ''' </summary>
    ''' <returns>
    ''' List of strings representing the names of all of the layers defined
    ''' by this GeoDataListConnection.
    ''' </returns>
    ''' <remarks>
    ''' Returns a List of strings representing the names of all of the layers defined
    ''' by this GeoDataListConnection. 
    ''' </remarks>
    Function getLayerNamesStrings() As List(Of String)

    ''' <summary>
    ''' Returns a List of IDataName objects representing the names of all of the layers defined
    ''' by this GeoDataListConnection. 
    ''' </summary>
    ''' <returns>Returns a List of IDataName objects representing the names of all of the layers defined
    ''' by this GeoDataListConnection.</returns>
    ''' <remarks>
    ''' Returns a List of IDataName objects representing the names of all of the layers defined
    ''' by this GeoDataListConnection.
    ''' 
    ''' The result of the getDefaultDataNameClauseLookup() function is passed to the constructor of
    ''' the IDataName objects. If a different DataNameClauseLookup object should be used then use the
    ''' alternative version of this function.
    ''' 
    ''' This function is a shorthand for getLayerDataNamesList(getDefaultDataNameClauseLookup())
    ''' </remarks>
    Function getLayerDataNamesList() As List(Of IDataName)

    ''' <summary>
    ''' Returns a List of IDataName objects representing the names of all of the layers defined
    ''' by this GeoDataListConnection. 
    ''' </summary>
    ''' <param name="myDNCL">
    ''' IDataNameClauseLookup object. This is passed to the constructor of
    ''' the IDataName objects
    ''' </param>
    ''' <returns>Returns a List of IDataName objects representing the names of all of the layers defined
    ''' by this GeoDataListConnection.</returns>
    ''' <remarks>
    ''' Returns a List of IDataName objects representing the names of all of the layers defined
    ''' by this GeoDataListConnection.
    ''' 
    ''' The IDataNameClauseLookup object is explictly assigned and this is passed to the constructor of
    ''' the IDataName objects. If the default DataNameClauseLookup object is sufficent then the alternative
    ''' shorthand function can be used.
    ''' </remarks>
    Function getLayerDataNamesList(ByRef myDNCL As IDataNameClauseLookup) As List(Of IDataName)

    ''' <summary>
    ''' A convenience function to test whether or not the named layer is present in this DataList.
    ''' </summary>
    ''' <param name="layerName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function doesLayerExist(ByVal layerName As String) As Boolean

    ''' <summary>
    ''' The GeoDataListConnection should attempt to locate the Data Names Clause Tables physically either
    ''' on the filesystem or in the GDB as apropriate. It should then return an IDataNameClauseLookup object
    ''' as appropriate.
    ''' 
    ''' If the Data Names Clause Tables cannot be physically located becuase either they do not exist or there
    ''' is not a uquine location (eg in the case of a MXD), then a exception is raised.
    ''' </summary>
    ''' <returns>IDataNameClauseLooup</returns>
    ''' <remarks></remarks>
    Function getDefaultDataNameClauseLookup() As IDataNameClauseLookup

End Interface

