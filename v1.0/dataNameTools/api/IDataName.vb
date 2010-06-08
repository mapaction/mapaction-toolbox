''' <summary>
''' Classes implementing this interface represent the status of the name of an individual layer, table
''' dataset or other file which is a part of a MapAction GIS data directory.
''' </summary>
''' <remarks>
''' Classes implementing this interface represent the status of the name of an individual layer, table
''' dataset or other file which is a part of a MapAction GIS data directory.
''' 
''' Instances of these classes may or may not represent a name which is valid, or even syntatically
''' correct. The intention is that these classes will assist the user in determining whether or not
''' individual names are valid or not.
''' 
''' It is not expected that that will be any public constructors for classes implenementing IDataName.
''' In general, instances of IDataName would be created within the implenmentation of the
''' IDataListConnection.getLayerDataNamesList() method.
''' </remarks>
Public Interface IDataName

    ''' <summary>
    ''' Returns the current IDataName as a String.
    ''' </summary>
    ''' <returns>a string of the current Data Name</returns>
    ''' <remarks>
    ''' Returns the current IDataName as a String. The returned name should not include
    ''' (i) the path (ii) the filename extension (eg ".shp") (iii) the RDBMS database or
    ''' RDBMS user name prefixes (eg "mapaction.sde.")
    ''' </remarks>
    Function getNameStr() As String

    ''' <summary>
    ''' Returns the path of the current IDataName as a String if possible. Should not
	''' include trailing slash or backslash.
    ''' </summary>
    ''' <returns>
    ''' a string of the current Data Name's path. Or is a path is not available a null 
    ''' or zero length string is returned.
    ''' </returns>
    ''' <remarks>
    ''' Returns the path of the current Data Name as a String, if a suitable meaning of
    ''' path is applicable. If there is no easy or meaningful sense of a path (eg for a 
    ''' RDBMS) then a null or zero length string is returned.
	'''
    ''' Should not include trailing slash or backslash.
    '''</remarks>
    Function getPathStr() As String


    ''' <summary>
    ''' Allows the IDataName to return an object representing the dataset named 
    ''' in this interface.
    ''' </summary>
    ''' <returns>A object appropriate for the particular implenmentation</returns>
    ''' <remarks>
    ''' Allows the IDataName to return an object representing the dataset named 
    ''' in this interface. For DataNameESRIFeatureClass this is an IDataSet and for
    ''' DataNameNormalFile this is a FileInfo object
    ''' </remarks>
    Function getObject() As Object


    ''' <summary>
    ''' Returns the fully qualified IDataName as a String if possible.
    ''' </summary>
    ''' <returns>
    ''' a string of the current IDataName's the fully qualified name. Or is a path is not
    ''' available then just the fully qualified name is returned (ie including the filename
    ''' extension [eg ".shp"] the RDBMS database or RDBMS user name prefixes
    ''' [eg "mapaction.sde."]
    ''' </returns>
    ''' <remarks>
    ''' Returns getPathStr() + "\" + getNameStr()
    ''' Returns the fully qualified IDataName as a String, if a suitable meaning of
    ''' path is applicable. If there is no easy or meaningful sense of a path (eg for a 
    ''' RDBMS) then the fully qualified name is returned (ie including the filename
    ''' extension [eg ".shp"] the RDBMS database or RDBMS user name prefixes
    ''' [eg "mapaction.sde."].
    ''' </remarks>
    Function getNameAndFullPathStr() As String

    ''' <summary>
    ''' This method does the core processing to determine whether or not the particular name represented by 
    ''' this object is syntatically correct and valid or not.
    ''' </summary>
    ''' <returns>A Long which is the sum of the dnNameStatus emnumeration members</returns>
    ''' <remarks>
    ''' This method does the core processing to determine whether or not the particular name represented by 
    ''' this object is syntatically correct and valid or not. 
    ''' 
	''' The status flags are a sum of the dnNameStatus emnumeration members and are arranged in four categories:
	'''     INVALID = "One or more of the clauses (excluding Free Text) cannot be found in the Data Name Clause Lookup Tables
	'''     SYNTAX_ERROR = "The format of the name cannot be understood. Individual clauses cannot be identified."
	'''     WARN = "The name can be understood and the clauses are valid, but for some reason there is a risk that it will be misinterprited"
	'''     INFO = "Other information about the name"
	''' 
	''' All of the flags are prefixed with one of these four names. It is possible to test
	''' for all flags within a particular category by just testing agains the root. eg:
	''' 
	''' ((myNameStatus And dnNameStatus.SYNTAX_ERROR) = dnNameStatus.SYNTAX_ERROR)
	''' 
	''' will return true for SYNTAX_ERROR_CONTAINS_HYPHENS, SYNTAX_ERROR_TOO_FEW_CLAUSES,
	''' SYNTAX_ERROR_DOUBLE_UNDERSCORE and SYNTAX_ERROR_OTHER
	''' 
	''' There is no "is valid" flag since depending on context this is any 
	''' combination of "not DATANAME_INVALID", "not DATANAME_SYNTAX_ERROR" 
	''' and maybe "not DATANAME_WARN"
    ''' </remarks>
    Function checkNameStatus() As Long

    ''' <summary>
    ''' Tests whether or not the IDataName is syntatically correct.
    ''' </summary>
    ''' <returns>Boolean. TRUE is the name is syntatically correct, FALSE otherwise</returns>
    ''' <remarks>
    ''' Tests whether or not the IDataName is syntatically correct.
    ''' 
    ''' This method is shorthand for:
    ''' (Not (checkNameStatus() And dnNameStatus.SYNTAX_ERROR) = dnNameStatus.SYNTAX_ERROR))
    ''' </remarks>
    Function isNameParseable() As Boolean

    ''' <summary>
    ''' Tests whether or not the Data Name is both syntatically correct and all of the clauses are found in the
    ''' relevant IDataNameClauseLookupTables.
    ''' </summary>
    ''' <returns>
    ''' Boolean. TRUE is the name is syntatically correct AND all of the clauses are found in the
    ''' relevant IDataNameClauseLookupTables, FALSE otherwise
    ''' </returns>
    ''' <remarks>
    ''' Tests whether or not the Data Name is both syntatically correct and all of the clauses are found in the
    ''' relevant IDataNameClauseLookupTables. If optional clauses are present then their values must be found in
    ''' the relevant  IDataNameClauseLookupTables.
    ''' 
    ''' This method is shorthand for:
    ''' (Not (checkNameStatus() And dnNameStatus.SYNTAX_ERROR) = dnNameStatus.SYNTAX_ERROR)) _
    ''' and (Not (checkNameStatus() And dnNameStatus.INVALID) = dnNameStatus.INVALID))
    ''' </remarks>
    Function isNameValid() As Boolean

    ''' <summary>
    ''' Does this name include the optional "Scale" clause in it?
    ''' </summary>
    ''' <returns>
    ''' TRUE = The optional scale clause is present AND the name is syntatically correct.
    ''' FALSE = The optional scale clause is not present OR the name is not syntatically correct.
    ''' </returns>
    ''' <remarks>Does this name include the optional "Scale" clause in it?</remarks>
    Function hasOptionalScaleClause() As Boolean

    ''' <summary>
    ''' Does this name include the optional "Data Permissions" clause in it?
    ''' </summary>
    ''' <returns>
    ''' TRUE = The optional Data Permissions clause is present AND the name is syntatically correct.
    ''' FALSE = The optional Data Permissions clause is not present OR the name is not syntatically correct.
    ''' </returns>
    ''' <remarks>Does this name include the optional "Data Permissions" clause in it?</remarks>
    Function hasOptionalPermissionClause() As Boolean

    ''' <summary>
    ''' Does this IDataName include the optional "Free Text" clause?
    ''' </summary>
    ''' <returns>
    ''' TRUE = The optional Free Text clause is present AND the name is syntatically correct.
    ''' FALSE = The optional Free Text clause is not present OR the name is not syntatically correct.
    ''' </returns>
    ''' <remarks>Does this IDataName include the optional "Data Permissions" clause in it?</remarks>
    Function hasOptionalFreeText() As Boolean

    ''' <summary>
    ''' Returns a runtime value for whether or not it is possible for the IDataName of this data layer 
    ''' to be changed? For example this made be no if the underlying file system is readony, or 
    ''' the data resides on a webservice such as WMS/WFS.
    ''' </summary>
    ''' <returns>
    ''' TRUE = the name of the data layer can be changed.
    ''' FALSE = the name of the data layer cannot be changed.
    ''' </returns>
    ''' <remarks>
    ''' Returns a runtime value for whether or not it is possible for the IDataName of this data layer 
    ''' to be changed? For example this may be FALSE if the underlying file system is readony, or 
    ''' the data resides on a webservice such as WMS/WFS.
    ''' </remarks>
    Function isRenameable() As Boolean

    ''' <summary>
    ''' Attempts to rename the IDataName, to the newNameStr. The newNameStr does not need to be valid
    ''' in the sense of the data naming convention, but must be valid in terms of the underlying storage.
    ''' </summary>
    ''' <param name="newNameStr">The new Name for the IDataName.</param>
    ''' <remarks>
    ''' Attempts to rename the DataName, to the newNameStr. The newNameStr does not need to be valid
    ''' in the sense of the data naming convention, but must be valid in terms of the underlying storage.
    ''' 
    ''' If the renaming fails for any reason a RenamingDataException is thrown.
    ''' </remarks>
    Sub rename(ByVal newNameStr As String)

#Region "change methods"
    ''' <summary>
    ''' A convenance function, to subsutute the GeoExtent clause of the current name with a new value.
    ''' </summary>
    ''' <param name="newGeoExtent">The new, subsutute GeoExtent clause</param>
    ''' <remarks>
    ''' A convenance function, to subsutute the GeoExtent clause of the current name with a new value.
    ''' The new value does not need to be valid in the sense of the data naming convention, but must
    ''' be valid in terms of the underlying storage.
    ''' 
    ''' Throws an RenamingDataException if the IDataName is either un-renamable [test with 
    ''' .isRenameable()] or is not syntaticatally correct [test with .isNameParseable()]
    ''' </remarks>
    Sub changeGeoExtentClause(ByVal newGeoExtent As String)


    ''' <summary>
    ''' A convenance function, to subsutute the DataCategory clause of the current name with a new value.
    ''' </summary>
    ''' <param name="newDataCategory">The new, subsutute DataCategory clause</param>
    ''' <remarks>
    ''' A convenance function, to subsutute the DataCategory clause of the current name with a new value.
    ''' The new value does not need to be valid in the sense of the data naming convention, but must
    ''' be valid in terms of the underlying storage.
    ''' 
    ''' Throws an RenamingDataException if the IDataName is either un-renamable [test with 
    ''' .isRenameable()] or is not syntaticatally correct [test with .isNameParseable()]
    ''' </remarks>
    Sub changeDataCategoryClause(ByVal newDataCategory As String)


    ''' <summary>
    ''' A convenance function, to subsutute the DataTheme clause of the current name with a new value.
    ''' </summary>
    ''' <param name="newDataTheme">The new, subsutute DataTheme clause</param>
    ''' <remarks>
    ''' A convenance function, to subsutute the DataTheme clause of the current name with a new value.
    ''' The new value does not need to be valid in the sense of the data naming convention, but must
    ''' be valid in terms of the underlying storage.
    ''' 
    ''' Throws an RenamingDataException if the IDataName is either un-renamable [test with 
    ''' .isRenameable()] or is not syntaticatally correct [test with .isNameParseable()]
    ''' </remarks>
    Sub changeDataThemeClause(ByVal newDataTheme As String)


    ''' <summary>
    ''' A convenance function, to subsutute the DataType clause of the current name with a new value.
    ''' </summary>
    ''' <param name="newDataType">The new, subsutute DataType clause</param>
    ''' <remarks>
    ''' A convenance function, to subsutute the DataType clause of the current name with a new value.
    ''' The new value does not need to be valid in the sense of the data naming convention, but must
    ''' be valid in terms of the underlying storage.
    ''' 
    ''' Throws an RenamingDataException if the IDataName is either un-renamable [test with 
    ''' .isRenameable()] or is not syntaticatally correct [test with .isNameParseable()]
    ''' </remarks>
    Sub changeDataTypeClause(ByVal newDataType As String)


    ''' <summary>
    ''' A convenance function, to subsutute the Permissions clause of the current name with a new value.
    ''' </summary>
    ''' <param name="newPermissionsClause">The new, subsutute Permissions clause</param>
    ''' <remarks>
    ''' A convenance function, to subsutute the Permissions clause of the current name with a new value.
    ''' The new value does not need to be valid in the sense of the data naming convention, but must
    ''' be valid in terms of the underlying storage.
    ''' 
    ''' If the Permissions clause is not already present it will be inserted and the relevant undersources
    ''' will be inserted. To completely remove an existing Permissions clause set newPermissionsClause=""
    ''' and the relevant undersorces will be removed.
    ''' 
    ''' Throws an RenamingDataException if the IDataName is either un-renamable [test with 
    ''' .isRenameable()] or is not syntaticatally correct [test with .isNameParseable()]
    ''' </remarks>
    Sub changePermissionsClause(ByVal newPermissionsClause As String)


    ''' <summary>
    ''' A convenance function, to subsutute the Scale clause of the current name with a new value.
    ''' </summary>
    ''' <param name="newScaleClause">The new, subsutute Scale clause</param>
    ''' <remarks>
    ''' A convenance function, to subsutute the Scale clause of the current name with a new value.
    ''' The new value does not need to be valid in the sense of the data naming convention, but must
    ''' be valid in terms of the underlying storage.
    ''' 
    ''' If the Scale clause is not already present it will be inserted and the relevant undersources
    ''' will be inserted. To completely remove an existing Scale clause set newScaleClause="" and
    ''' the relevant undersorces will be removed.
    ''' 
    ''' Throws an RenamingDataException if the IDataName is either un-renamable [test with 
    ''' .isRenameable()] or is not syntaticatally correct [test with .isNameParseable()]
    ''' </remarks>
    Sub changeScaleClause(ByVal newScaleClause As String)


    ''' <summary>
    ''' A convenance function, to subsutute the Source clause of the current name with a new value.
    ''' </summary>
    ''' <param name="newSourceClause">The new, subsutute Source clause</param>
    ''' <remarks>
    ''' A convenance function, to subsutute the Source clause of the current name with a new value.
    ''' The new value does not need to be valid in the sense of the data naming convention, but must
    ''' be valid in terms of the underlying storage.
    ''' 
    ''' Throws an RenamingDataException if the IDataName is either un-renamable [test with 
    ''' .isRenameable()] or is not syntaticatally correct [test with .isNameParseable()]
    ''' </remarks>
    Sub changeSourceClause(ByVal newSourceClause As String)

    ''' <summary>
    ''' A convenance function, to subsutute the FreeText clause of the current name with a new value.
    ''' </summary>
    ''' <param name="newFreeTextClause">The new, subsutute FreeText clause</param>
    ''' <remarks>
    ''' A convenance function, to subsutute the FreeText clause of the current name with a new value.
    ''' The new value does not need to be valid in the sense of the data naming convention, but must
    ''' be valid in terms of the underlying storage.
    ''' 
    ''' If the FreeText clause is not already present it will be inserted and the relevant undersources
    ''' will be inserted. To completely remove an existing FreeText clause set newFreeTextClause="" and
    ''' the relevant undersorces will be removed.
    ''' 
    ''' Throws an RenamingDataException if the IDataName is either un-renamable [test with 
    ''' .isRenameable()] or is not syntaticatally correct [test with .isNameParseable()]
    ''' </remarks>
    Sub changeFreeTextClause(ByVal newFreeTextClause As String)

#End Region

#Region "get name part methods"
    Function getGeoExtentClause() As String
    Function getDataCategoryClause() As String
    Function getDataThemeClause() As String
    Function getDataTypeClause() As String
    Function getPermissionsClause() As String
    Function getScaleClause() As String
    Function getSourceClause() As String
    Function getFreeTextClause() As String
#End Region


End Interface
