Imports System.IO

''' <summary>
''' Classes implementing this interface represent a logical group of layers. They may be stored
''' in one location (eg. GDB or directory of shapefiles) or they may be used together in some
''' other logical manner (eg. in a map MXD).
''' </summary>
''' <remarks>
''' Classes implementing this interface represent a logical group of layers. They may be stored
''' in one location (eg. GDB or directory of shapefiles) or they may be used together in some
''' other logical manner (eg. in a map MXD).
''' 
''' This interface is generally used along with two others, IDataNameClauseLookup and IDataName
''' </remarks>
Public Interface IGeoDataListConnection

    'todo convert to an enumeration and re-write summary
    ''' <summary>
    ''' Returns an Integer which represents the type of connection
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function getGeoDataListConnectionType() As Integer

    'todo possible convert to an enumeration and re-write summary
    ''' <summary>
    ''' Returns a String which represents the type of connection
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function getGeoDataListConnectionTypeDesc() As String

    ''' <summary>
    ''' Returns a string describing the type of DataListConnection.
    ''' </summary>
    ''' <returns>A string describing the type of DataListConnection.</returns>
    ''' <remarks>
    ''' Returns a string describing the type of DataListConnection.
    ''' 
    ''' This may take on of a number of forms:
    ''' * An operating system directory path, for a directory containing shapefiles
    ''' * An operating system file path for a Personal GDB, MXD or connection file.
    ''' * A RDMS connection string, 
    ''' * A URL
    ''' * etc.
    ''' </remarks>
    Function getDetails() As String

    ''' <summary>
    ''' Returns the operating system file path to the DataListConnection.
    ''' </summary>
    ''' <returns>A FileInfo object representing the operating system file path
    ''' to the DataListConnection.</returns>
    ''' <remarks>
    ''' Returns the operating system file path to the DataListConnection.
    ''' 
    ''' If the location of the DataListConnection cannot represented as an 
    ''' operating system file (eg if they are located in a RDBMS) then the 
    ''' Nothing object is returned.
    ''' </remarks>
    'Function getPath() As fileinfo

    ''' <summary>
    ''' Returns a List of strings representing the names of all of the layers defined
    ''' by this DataListConnection. 
    ''' </summary>
    ''' <returns>
    ''' List of strings representing the names of all of the layers defined
    ''' by this DataListConnection.
    ''' </returns>
    ''' <remarks>
    ''' Returns a List of strings representing the names of all of the layers defined
    ''' by this DataListConnection. 
    ''' </remarks>
    Function getLayerNamesStrings() As List(Of String)

    ''' <summary>
    ''' Returns a List of IDataName objects representing the names of all of the layers defined
    ''' by this DataListConnection. 
    ''' </summary>
    ''' <returns>Returns a List of IDataName objects representing the names of all of the layers defined
    ''' by this GeoDataListConnection.</returns>
    ''' <remarks>
    ''' Returns a List of IDataName objects representing the names of all of the layers defined
    ''' by this GeoDataListConnection.
    ''' 
    ''' This function is a shorthand for getLayerDataNamesList(getDefaultDataNameClauseLookup()) 
    ''' The result of the getDefaultDataNameClauseLookup() function is passed to the constructor of
    ''' the IDataName objects. If a different DataNameClauseLookup object should be used then use the
    ''' overloaded alternative form of this function.
    ''' </remarks>
    Function getLayerDataNamesList() As List(Of IDataName)

    ''' <summary>
    ''' Returns a List of IDataName objects representing the names of all of the layers defined
    ''' by this DataListConnection. 
    ''' </summary>
    ''' <param name="dnclUserSelected">
    ''' IDataNameClauseLookup object. This is passed to the constructor of
    ''' the IDataName objects
    ''' </param>
    ''' <returns>A List of IDataName objects representing the names of all of the layers defined
    ''' by this DataListConnection.</returns>
    ''' <remarks>
    ''' Returns a List of IDataName objects representing the names of all of the layers defined
    ''' by this DataListConnection.
    ''' 
    ''' The IDataNameClauseLookup object is explictly assigned and this is passed to the constructor of
    ''' the IDataName objects. If the default DataNameClauseLookup object is sufficent then the alternative
    ''' shorthand function can be used.
    ''' </remarks>
    Function getLayerDataNamesList(ByRef dnclUserSelected As IDataNameClauseLookup) As List(Of IDataName)

    ''' <summary>
    ''' A convenience function to test whether or not the named layer is present in this DataListConnection.
    ''' </summary>
    ''' <param name="strLayerName">The name of the layer, whose presense is being tested for.</param>
    ''' <returns>TRUE if strLayerName is present in this DataListConnection, FALSE otherwise</returns>
    ''' <remarks>
    ''' A convenience function to test whether or not the named layer is present in this DataListConnection.
    ''' </remarks>
    Function doesLayerExist(ByVal strLayerName As String) As Boolean

    ''' <summary>
    ''' The IDataListConnection should attempt to physically locate the DataNames Clause Tables either
    ''' on the filesystem or in the GDB as apropriate. It should then return an IDataNameClauseLookup object
    ''' as appropriate. The search method to find the DataNames Clause Tables is relevative to the location of the 
    ''' IDataListConnection
    ''' 
    ''' If the Data Names Clause Tables cannot be physically located becuase either they do not exist or there
    ''' is not a uquine location (eg in the case of a MXD), then a exception is raised.
    ''' </summary>
    ''' <returns>An IDataNameClauseLooup object representing automatically located DataNames Clause Tables</returns>
    ''' <remarks>
    ''' The IDataListConnection should attempt to physically locate the DataNames Clause Tables either
    ''' on the filesystem or in the GDB as apropriate. It should then return an IDataNameClauseLookup object
    ''' as appropriate. The search method to find the DataNames Clause Tables is relevative to the location of the 
    ''' IDataListConnection.
    ''' 
    ''' If the Data Names Clause Tables cannot be physically located becuase either they do not exist or there
    ''' is not a uquine location (eg in the case of a MXD), then a exception is raised.
    ''' 
    ''' For more details on how this should be implenmented please see:
    ''' http://code.google.com/p/mapaction-toolbox/wiki/SearchForDefaultDataNameClauseLookupTables
    ''' </remarks>
    Function getDefaultDataNameClauseLookup() As IDataNameClauseLookup


    ''' <summary>
    ''' Does the IDataListConnection represents some from of heirachical list of data?
    ''' </summary>
    ''' <returns>
    ''' TRUE = the IDataListConnection has a heirachical structure.
    ''' FALSE = the IDataListConnection does not have a heirachical structure.
    ''' </returns>
    ''' <remarks>
    ''' Does the IDataListConnection represents some from of heirachical list of data?
    ''' 
    ''' Some implenmentation of the IDataListConnection, will represents some from of heirachical list of data
    ''' (eg a directory full of subdirectories and shapefiles). The value returned by this method represents
    ''' whether or not the underlying list of data is heirachical.
    ''' 
    ''' The implenmentor is free to interperate the meaning of heirachicalin the manner deemed most appropriate 
    ''' for the particular type of IDataListConnection.
    ''' 
    ''' For example. Most end users will have a clear expectation that this would be TRUE for a 
    ''' filesystem directory. 
    ''' 
    ''' Strictly an MXD is a heirachical list of layer, with the top level of the heirachy declared as DataFrames 
    ''' and lower levels declared as potenially nested Group Layers. However MXDs are typically such shallow heirachies
    ''' that enforcing a heirachical search of an MXD may confuse more users than it helps. In such curcumstances 
    ''' the implenmentor may choose to return FALSE here and flatten the MXD automatically.
    ''' </remarks>
    'Function isHeirachical() As Boolean

    ''' <summary>
    ''' If the IDataListConnection, represents some from of heirachical list of data (eg a directory
    ''' full of subdirectories and shapefiles), this method sets whether or not the IDataListConnection should recuse
    ''' the subdirectories.
    ''' </summary>
    ''' <param name="blnRecuse">TRUE = the heirachy should be recused. FALSE = the heirachy should not be recused.</param>
    ''' <remarks>
    ''' If the IDataListConnection, represents some from of heirachical list of data (eg a directory
    ''' full of subdirectories and shapefiles), this method sets whether or not the IDataListConnection should recuse
    ''' the subdirectories when either of the methods getLayerNamesStrings() or getLayerDataNamesList() are called.
    ''' </remarks>
    Sub setRecuse(ByVal blnRecuse As Boolean)

    ''' <summary>
    ''' Gets the current setting of whether or not heirachical lists should be recused.
    ''' </summary>
    ''' <returns>TRUE = the heirachy should be recused. FALSE = the heirachy should not be recused.</returns>
    ''' <remarks>Gets the current setting of whether or not heirachical lists should be recused.</remarks>
    Function getRecuse() As Boolean

End Interface

