'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''Copyright (C) 2010 MapAction UK Charity No. 1075977
''
''www.mapaction.org
''
''This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 3 of the License, or (at your option) any later version.
''
''This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
''
''You should have received a copy of the GNU General Public License along with this program; if not, see <http://www.gnu.org/licenses>.
''
''Additional permission under GNU GPL version 3 section 7
''
''If you modify this Program, or any covered work, by linking or combining it with 
''ESRI ArcGIS Desktop Products (ArcView, ArcEditor, ArcInfo, ArcEngine Runtime and ArcEngine Developer Kit) (or a modified version of that library), containing parts covered by the terms of ESRI's single user or concurrent use license, the licensors of this Program grant you additional permission to convey the resulting work.
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Text

''' <summary>
''' Provides a framework for the implenmentation of the IDataName interface.
''' </summary>
''' <remarks>
''' Provides a framework for the implenmentation of the IDataName interface.
''' </remarks>
Public MustInherit Class AbstractDataName
    Implements IDataName

    Protected Friend m_strName As String
    Protected Friend m_DNCL As IDataNameClauseLookup
    Private m_blnCacheIsUptoDate As Boolean = False
    Private m_lngCachedBitSum As Long
    Protected Friend m_blnAllowReNaming As Boolean

    Public Event NameChanged(ByVal strOldName As String, ByRef dnRenamed As IDataName) Implements IDataName.NameChanged

    ''' <summary>
    ''' Creates a new IDataName.
    ''' </summary>
    ''' <param name="strNewName">The Name. The caller is reasonible for ensuring that
    ''' any prefixes or suffixes specific to a particular storage type are removed before
    ''' being passed to this constructor.
    ''' </param>
    ''' <param name="dncl">The IDataNameClauseLookup object against which the validity of 
    ''' this IDataName will be checked</param>
    ''' <param name="blnAllowRenames">Allows the caller to create the object in readonly 
    ''' mode. Useful if the IDataListConnection would need to be modified to accomidate
    ''' renamed IDataName objects</param>
    ''' <remarks>
    ''' Creates a new IDataName.
    ''' 
    ''' The caller is reasonible for ensuring that any prefixes or suffixes specific 
    ''' to a particular storage type are removed before being passed to this constructor.
    ''' The object can be created in readonly mode if required. This is useful if the 
    ''' IDataListConnection would need to be modified to accomidate renamed IDataName 
    ''' objects (eg an MXD).
    ''' </remarks>
    Protected Friend Sub New(ByVal strNewName As String, ByRef dncl As IDataNameClauseLookup, _
                             ByVal blnAllowRenames As Boolean)
        m_strName = strNewName
        m_DNCL = dncl
        m_blnAllowReNaming = blnAllowRenames
    End Sub


    Public MustOverride Function getNameStr() As String Implements IDataName.getNameStr
    '    Return m_strName
    'End Function

    ''' <summary>
    ''' Returns the path of the current DataName as a String if possible. Should not 
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
    Public MustOverride Function getPathStr() As String Implements IDataName.getPathStr

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
    Public MustOverride Function getObject() As Object Implements IDataName.getObject


    ''' <summary>
    ''' This method is used to check that the data type clause matches actually physical data type.
    ''' </summary>
    ''' <returns>One of the constants with the prefix "DATATYPE_CLAUSE_"</returns>
    ''' <remarks>
    ''' This method is used to check that the data type clause matches actually physical data type.
    ''' 
    ''' The implenmentor should check the underlying geographical data type and return an 
    ''' appropriate string. This method is called within the checkNameStatus() method to ensure
    ''' that the type specified in the data name matches the underlying geographical type.
    ''' </remarks>
    Protected Friend MustOverride Function getUnderlyingDataType() As String

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
    Public MustOverride Function getNameAndFullPathStr() As String Implements IDataName.getNameAndFullPathStr


    ''' <summary>
    ''' This method does the core processing to determine whether or not the particular name represented by 
    ''' this object is syntatically correct and valid or not.
    ''' </summary>
    ''' <returns>A Long which is the sum of the dnNameStatus emnumeration members.</returns>
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
    Public Function checkNameStatus() As Long Implements IDataName.checkNameStatus
        Dim lngReturnVal As Long

        If m_blnCacheIsUptoDate Then
            lngReturnVal = m_lngCachedBitSum
        Else
            'todo LOW: It would be very nice if a DataName could listen for changes 
            'in the DataNameLookupTables
            lngReturnVal = getNameSyntaxStatus(m_strName)
            m_lngCachedBitSum = lngReturnVal
            m_blnCacheIsUptoDate = True
        End If

        Return lngReturnVal
    End Function


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
    Public Function isNameParseable() As Boolean Implements IDataName.isNameParseable
        Dim lngBitSum As Long

        lngBitSum = checkNameStatus()

        Return (Not (lngBitSum And dnNameStatus.SYNTAX_ERROR) = dnNameStatus.SYNTAX_ERROR)
    End Function


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
    Public Function isNameValid() As Boolean Implements IDataName.isNameValid
        Dim lngBitSum As Long
        Dim blnResult As Boolean

        lngBitSum = checkNameStatus()
        blnResult = (Not (lngBitSum And dnNameStatus.SYNTAX_ERROR) = dnNameStatus.SYNTAX_ERROR) And _
                   (Not (lngBitSum And dnNameStatus.INVALID) = dnNameStatus.INVALID)

        Return blnResult
    End Function

    ''' <summary>
    ''' Test whether the IDataName can be renamed without an exception being thrown
    ''' </summary>
    ''' <returns>Boolean. Returns a runtime value signifying whether or not the underlying dataname
    ''' is both renaming (physically) and that the relevant permissions are held.
    ''' </returns>
    ''' <remarks>
    ''' Test whether the IDataName can be renamed without an exception being thrown
    ''' 
    ''' If true then attempting to calling one of the methods to rename the
    ''' DataName should not throw an expeption. If false then attempting to call one of the method
    ''' will raise a RenamingDataException.
    ''' </remarks>
    Public Function isRenameable() As Boolean Implements IDataName.isRenameable
        Return (m_blnAllowReNaming And isRenamePossible())
    End Function


    ''' <summary>
    ''' Subclasses should overwrite this. It should return a runtime value indicating
    ''' whether or not the data *could* be renamed. (It is distict from whether or not 
    ''' renaming is *allowed* with the context of the api)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Subclasses should overwrite this. It should return a runtime value indicating
    ''' whether or not the data *could* be renamed. (It is distict from whether or not 
    ''' renaming is *allowed* with the context of the api)
    ''' 
    ''' The runtime conbination of being both *allowed* and *possible* are combined by 
    ''' the isRenameable() method. End users should call this method.
    ''' </remarks>
    Protected Friend MustOverride Function isRenamePossible() As Boolean


    ''' <summary>
    ''' Used by this class and subclasses to indicate that the internally cached NameStatus
    ''' value is not longer valid. The next call to .checkNameStatus() will result in a full
    ''' check against all of the underlying Data Name Tables.
    ''' </summary>
    ''' <remarks>
    ''' Used by this class and subclasses to indicate that the internally cached NameStatus
    ''' value is not longer valid. The next call to .checkNameStatus() will result in a full
    ''' check against all of the underlying Data Name Tables.
    ''' </remarks>
    Protected Friend Sub resetCacheFlags()
        m_blnCacheIsUptoDate = False
        m_lngCachedBitSum = dnNameStatus.UNKNOWN_STATUS
    End Sub


    ''' <summary>
    ''' Indicates whether or not the DataName includes an optional scale clause.
    ''' </summary>
    ''' <returns>
    ''' TRUE if the name is parseable and the optional scale clause is present. False otherwise.
    ''' </returns>
    ''' <remarks>
    ''' Indicates whether or not the DataName includes an optional scale clause.
    ''' </remarks>
    Public Function hasOptionalScaleClause() As Boolean Implements IDataName.hasOptionalScaleClause
        Dim blnResult As Boolean
        Dim lngBitsum As Long

        lngBitsum = checkNameStatus()

        blnResult = isNameParseable() And _
           (Not (lngBitsum And dnNameStatus.WARN_MISSING_PERMISSIONS_CLAUSE) = _
            dnNameStatus.WARN_MISSING_PERMISSIONS_CLAUSE)

        Return blnResult
    End Function


    ''' <summary>
    ''' Indicates whether or not the DataName includes an optional permissions clause.
    ''' </summary>
    ''' <returns>
    ''' TRUE if the name is parseable and the optional permissions clause is present. False otherwise.
    ''' </returns>
    ''' <remarks>
    ''' Indicates whether or not the DataName includes an permissions scale clause.
    ''' </remarks>
    Public Function hasOptionalPermissionClause() As Boolean Implements IDataName.hasOptionalPermissionClause
        Dim blnResult As Boolean
        Dim lngBitsum As Long

        lngBitsum = checkNameStatus()

        blnResult = isNameParseable() And _
           (Not (lngBitsum And dnNameStatus.WARN_MISSING_PERMISSIONS_CLAUSE) = _
            dnNameStatus.WARN_MISSING_PERMISSIONS_CLAUSE)

        Return blnResult
    End Function

    ''' <summary>
    ''' Indicates whether or not the DataName includes an optional free text clause.
    ''' </summary>
    ''' <returns>
    ''' TRUE if the name is parseable and the optional free text clause is present. False otherwise.
    ''' </returns>
    ''' <remarks>
    ''' Indicates whether or not the DataName includes an free text scale clause.
    ''' </remarks>
    Public Function hasOptionalFreeText() As Boolean Implements IDataName.hasOptionalFreeText
        Dim blnResult As Boolean
        Dim lngBitsum As Long

        lngBitsum = checkNameStatus()

        blnResult = isNameParseable() And _
           ((lngBitsum And dnNameStatus.INFO_FREE_TEXT_PRESENT) = _
            dnNameStatus.INFO_FREE_TEXT_PRESENT)

        Return blnResult
    End Function


    ''' <summary>
    ''' Attempts to rename the IDataName, to the newNameStr. The newNameStr does not need to be valid
    ''' in the sense of the data naming convention, but must be valid in terms of the underlying storage.
    ''' </summary>
    ''' <param name="strNewName">The new Name for the IDataName.</param>
    ''' <remarks>
    ''' Attempts to rename the DataName, to the newNameStr. The newNameStr does not need to be valid
    ''' in the sense of the data naming convention, but must be valid in terms of the underlying storage.
    ''' 
    ''' If the renaming fails for any reason a RenamingDataException is thrown.
    ''' </remarks>
    Public Sub rename(ByVal strNewName As String) Implements IDataName.rename
        Dim strOldName As String

        If m_blnAllowReNaming Then
            strOldName = m_strName
            resetCacheFlags()
            performRename(strNewName)
            RaiseEvent NameChanged(strOldName, Me)
        Else
            Throw New RenamingDataException(Me)
        End If

    End Sub


    ''' <summary>
    ''' Subclasses should overwrite this. It should implenment a method for
    ''' renaming the underlying data object, specific to teh storage method.
    ''' </summary>
    ''' <param name="newNameStr">The new Name for the IDataName.</param>
    ''' <remarks>
    ''' Subclasses should overwrite this. It should implenment a method for
    ''' renaming the underlying data object, specific to teh storage method.
    ''' 
    ''' If the underlying storage is readonly then the implenmenting method
    ''' should throw a RenamingDataException exception.
    ''' 
    ''' End users should not call this method, but use the rename() method 
    ''' instead.
    ''' </remarks>
    Protected MustOverride Sub performRename(ByVal newNameStr As String)

#Region "get methods"
    Public Function getGeoExtentClause() As String Implements IDataName.getGeoExtentClause
        Return getNameClause(CLAUSE_GEOEXTENT)
    End Function

    Public Function getDataCategoryClause() As String Implements IDataName.getDataCategoryClause
        Return getNameClause(CLAUSE_DATACATEGORY)
    End Function

    Public Function getDataThemeClause() As String Implements IDataName.getDataThemeClause
        Return getNameClause(CLAUSE_DATATHEME)
    End Function

    Public Function getDataTypeClause() As String Implements IDataName.getDataTypeClause
        Return getNameClause(CLAUSE_DATATYPE)
    End Function

    Public Function getPermissionsClause() As String Implements IDataName.getPermissionsClause
        Return getNameClause(CLAUSE_PERMISSIONS)
    End Function
    Public Function getScaleClause() As String Implements IDataName.getScaleClause
        Return getNameClause(CLAUSE_SCALE)
    End Function
    Public Function getSourceClause() As String Implements IDataName.getSourceClause
        Return getNameClause(CLAUSE_SOURCE)
    End Function
    Public Function getFreeTextClause() As String Implements IDataName.getFreeTextClause
        Return getNameClause(CLAUSE_FREETEXT)
    End Function

#End Region

#Region "change methods"

    ''' <summary>
    ''' A convenance function, to subsutute the GeoExtent clause of the current name with a new value.
    ''' </summary>
    ''' <param name="strNewGeoExtent">The new, subsutute GeoExtent clause</param>
    ''' <remarks>
    ''' A convenance function, to subsutute the GeoExtent clause of the current name with a new value.
    ''' The new value does not need to be valid in the sense of the data naming convention, but must
    ''' be valid in terms of the underlying storage.
    ''' 
    ''' Throws an RenamingDataException if the IDataName is either un-renamable [test with 
    ''' .isRenameable()] or is not syntaticatally correct [test with .isNameParseable()]
    ''' </remarks>
    Public Sub changeGeoExtentClause(ByVal strNewGeoExtent As String) Implements IDataName.changeGeoExtentClause
        rename(generateNameWithReplacedClause(strNewGeoExtent, CLAUSE_GEOEXTENT))
    End Sub

    ''' <summary>
    ''' A convenance function, to subsutute the DataCategory clause of the current name with a new value.
    ''' </summary>
    ''' <param name="strNewDataCategory">The new, subsutute DataCategory clause</param>
    ''' <remarks>
    ''' A convenance function, to subsutute the DataCategory clause of the current name with a new value.
    ''' The new value does not need to be valid in the sense of the data naming convention, but must
    ''' be valid in terms of the underlying storage.
    ''' 
    ''' Throws an RenamingDataException if the IDataName is either un-renamable [test with 
    ''' .isRenameable()] or is not syntaticatally correct [test with .isNameParseable()]
    ''' </remarks>
    Public Sub changeDataCategoryClause(ByVal strNewDataCategory As String) Implements IDataName.changeDataCategoryClause
        'changeDataCategoryClause = Nothing
        rename(generateNameWithReplacedClause(strNewDataCategory, CLAUSE_DATACATEGORY))
    End Sub


    ''' <summary>
    ''' A convenance function, to subsutute the DataTheme clause of the current name with a new value.
    ''' </summary>
    ''' <param name="strNewDataTheme">The new, subsutute DataTheme clause</param>
    ''' <remarks>
    ''' A convenance function, to subsutute the DataTheme clause of the current name with a new value.
    ''' The new value does not need to be valid in the sense of the data naming convention, but must
    ''' be valid in terms of the underlying storage.
    ''' 
    ''' Throws an RenamingDataException if the IDataName is either un-renamable [test with 
    ''' .isRenameable()] or is not syntaticatally correct [test with .isNameParseable()]
    ''' </remarks>
    Public Sub changeDataThemeClause(ByVal strNewDataTheme As String) Implements IDataName.changeDataThemeClause
        'changeDataThemeClause = Nothing
        rename(generateNameWithReplacedClause(strNewDataTheme, CLAUSE_DATATHEME))
    End Sub

    ''' <summary>
    ''' A convenance function, to subsutute the DataType clause of the current name with a new value.
    ''' </summary>
    ''' <param name="strNewDataType">The new, subsutute DataType clause</param>
    ''' <remarks>
    ''' A convenance function, to subsutute the DataType clause of the current name with a new value.
    ''' The new value does not need to be valid in the sense of the data naming convention, but must
    ''' be valid in terms of the underlying storage.
    ''' 
    ''' Throws an RenamingDataException if the IDataName is either un-renamable [test with 
    ''' .isRenameable()] or is not syntaticatally correct [test with .isNameParseable()]
    ''' </remarks>
    Public Sub changeDataTypeClause(ByVal strNewDataType As String) Implements IDataName.changeDataTypeClause
        'changeDataTypeClause = Nothing
        rename(generateNameWithReplacedClause(strNewDataType, CLAUSE_DATATYPE))
    End Sub

    ''' <summary>
    ''' A convenance function, to subsutute the Scale clause of the current name with a new value.
    ''' </summary>
    ''' <param name="strNewScale">The new, subsutute Scale clause</param>
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
    Public Sub changeScaleClause(ByVal strNewScale As String) Implements IDataName.changeScaleClause
        'changeScaleCodesClause = Nothing
        rename(generateNameWithReplacedClause(strNewScale, CLAUSE_SCALE))
    End Sub



    ''' <summary>
    ''' A convenance function, to subsutute the Source clause of the current name with a new value.
    ''' </summary>
    ''' <param name="strNewSource">The new, subsutute Source clause</param>
    ''' <remarks>
    ''' A convenance function, to subsutute the Source clause of the current name with a new value.
    ''' The new value does not need to be valid in the sense of the data naming convention, but must
    ''' be valid in terms of the underlying storage.
    ''' 
    ''' Throws an RenamingDataException if the IDataName is either un-renamable [test with 
    ''' .isRenameable()] or is not syntaticatally correct [test with .isNameParseable()]
    ''' </remarks>
    Public Sub changeSourceClause(ByVal strNewSource As String) Implements IDataName.changeSourceClause
        'changeSourceCodesClause = Nothing
        rename(generateNameWithReplacedClause(strNewSource, CLAUSE_SOURCE))
    End Sub



    ''' <summary>
    ''' A convenance function, to subsutute the Permissions clause of the current name with a new value.
    ''' </summary>
    ''' <param name="strNewPermissions">The new, subsutute Permissions clause</param>
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
    Public Sub changePermissionsClause(ByVal strNewPermissions As String) Implements IDataName.changePermissionsClause
        'changePermissionsClause = Nothing
        rename(generateNameWithReplacedClause(strNewPermissions, CLAUSE_PERMISSIONS))
    End Sub


    ''' <summary>
    ''' A convenance function, to subsutute the FreeText clause of the current name with a new value.
    ''' </summary>
    ''' <param name="strNewFreeText">The new, subsutute FreeText clause</param>
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
    Public Sub changeFreeTextClause(ByVal strNewFreeText As String) Implements IDataName.changeFreeTextClause
        'changeFreeTextClause = Nothing
        rename(generateNameWithReplacedClause(strNewFreeText, CLAUSE_FREETEXT))
    End Sub

#End Region

    ''' <summary>
    ''' The method does NOT alter the underlying DataName - for that use the rename function. This function is 
    ''' a helper function to help generate new strings, preparing the arguments for the rename function.
    ''' </summary>
    ''' <param name="strNewClauseValue"></param>
    ''' <param name="strClauseName"></param>
    ''' <returns>A String with the strNewClauseValue substituded in the appropriate places</returns>
    ''' <remarks>
    ''' The method does NOT alter the underlying DataName - for that use the rename function. This function is 
    ''' a helper function to help generate new strings, preparing the arguments for the rename function.
    ''' 
    ''' If the strClauseName is for a optional clause that is currently not included it will be inserted into the string.
    ''' If the strClauseName is for a clause that is already present then the old clause will be replaced.
    ''' If the strClauseName is for a optional clause and the strNewClauseValue is equal to Nothing or String.Empty
    ''' then the optional clause will be removed.
    ''' </remarks>
    Private Function generateNameWithReplacedClause(ByVal strNewClauseValue As String, ByVal strClauseName As String) As String
        Dim stbNewVal As New StringBuilder
        Dim lstNameParts As New List(Of String)
        Dim srtClauseIdx As Short

        If Not isNameParseable() Then
            Throw New ErroreousDataNameException(checkNameStatus())
        Else
            lstNameParts.AddRange(Strings.Split(m_strName, "_"))
            srtClauseIdx = getClauseIndex(strClauseName)

            If srtClauseIdx <> -1 Then
                If strNewClauseValue Is Nothing OrElse strNewClauseValue = String.Empty Then
                    'either this is removing an optional clause or there is an par
                    Select Case strClauseName
                        Case CLAUSE_SCALE, CLAUSE_PERMISSIONS, CLAUSE_FREETEXT
                            'Removing an existing optional clause
                            lstNameParts.RemoveAt(srtClauseIdx)
                        Case Else
                            'Trying to remove an compulsory clause
                            Throw New ArgumentException("Cannot remove compulsory clause", strClauseName)
                    End Select
                Else
                    'Name clause exists, replace it
                    lstNameParts.Item(srtClauseIdx) = strNewClauseValue
                End If

            Else
                'Optional Name clause does not exists, insert it
                'check that the new value is not blank. If it is that arguments mean that that
                'a not existant clause should be removed (ie do nothing)
                If strNewClauseValue IsNot Nothing AndAlso strNewClauseValue <> String.Empty Then
                    Select Case strClauseName
                        '  geoextent_datacategory_theme_datatype[_scale]_source[_permission][_FreeText]
                        '  #0        #1           #2    #3       #4     #5      #6           #7
                        Case CLAUSE_SCALE
                            srtClauseIdx = 4

                        Case CLAUSE_PERMISSIONS
                            If hasOptionalScaleClause() Then
                                srtClauseIdx = 6
                            Else
                                srtClauseIdx = 5
                            End If

                        Case CLAUSE_FREETEXT
                            srtClauseIdx = CShort(lstNameParts.Count)

                    End Select

                    lstNameParts.Insert(srtClauseIdx, strNewClauseValue)
                End If
            End If

            'now concatenate it all together
            For Each strNewNamePart In lstNameParts
                If stbNewVal.Length > 0 Then
                    stbNewVal.Append("_")
                End If
                stbNewVal.Append(strNewNamePart)
            Next

        End If

        Return stbNewVal.ToString()
    End Function


    Private Function getNameClause(ByVal strClauseName As String) As String
        Dim strClause As String
        Dim lstNameParts As New List(Of String)
        Dim srtClauseIdx As Short

        If Not isNameParseable() Then
            Throw New ErroreousDataNameException(checkNameStatus())
        Else
            lstNameParts.AddRange(Strings.Split(m_strName, "_"))
            srtClauseIdx = getClauseIndex(strClauseName)

            If srtClauseIdx = -1 Then
                strClause = Nothing
            Else
                strClause = lstNameParts.Item(srtClauseIdx)
            End If

        End If

        Return strClause
    End Function

    ''' <summary>
    ''' Returns a ZERO based index of the named clause for the current IDataName, accounting for 
    ''' the presents or not of the various optional clauses. Will throw an InvalidDataNameException
    ''' if the bitSum indicates that the name is not parseable.
    ''' </summary>
    ''' <param name="strClauseName">The name of the name cluase, normally passed using one of the
    ''' constants with the "CLAUSE_" prefix.
    ''' </param>
    ''' <returns>A ZERO based index of the named clause for the current IDataName, accounting for 
    ''' the presents or not of the various optional clauses. The return value of this method is
    ''' designed to be a suitable argument for an array of strings generated by 
    ''' Strings.Split(m_strName, "_"). If optional clause is not present, the value "-1" will
    ''' be returned.
    ''' </returns>
    ''' <remarks>
    ''' Returns a ZERO based index of the named clause for the current IDataName, accounting for 
    ''' the presents or not of the various optional clauses. Will throw an InvalidDataNameException
    ''' if the bitSum indicates that the name is not parseable.
    ''' 
    ''' The return value of this method is designed to be a suitable argument for an array of
    ''' strings generated by Strings.Split(m_strName, "_")
    ''' 
    ''' If optional clause is not present, the value "-1" will be returned. The caller is responsible
    ''' for checking of a "-1" value prior to passing it to an array.
    ''' </remarks>
    Private Function getClauseIndex(ByVal strClauseName As String) As Short
        Dim srtReturnIdx As Short

        If Not isNameParseable() Then
            Throw New ErroreousDataNameException(checkNameStatus())
        Else

            Select Case strClauseName
                Case CLAUSE_GEOEXTENT
                    srtReturnIdx = 0
                Case CLAUSE_DATACATEGORY
                    srtReturnIdx = 1
                Case CLAUSE_DATATHEME
                    srtReturnIdx = 2
                Case CLAUSE_DATATYPE
                    srtReturnIdx = 3
                Case CLAUSE_SCALE
                    If hasOptionalScaleClause() Then
                        srtReturnIdx = 4
                    Else
                        srtReturnIdx = -1
                    End If
                Case CLAUSE_SOURCE
                    If hasOptionalScaleClause() Then
                        srtReturnIdx = 5
                    Else
                        srtReturnIdx = 4
                    End If
                Case CLAUSE_PERMISSIONS
                    If hasOptionalPermissionClause() Then
                        If hasOptionalScaleClause() Then
                            srtReturnIdx = 6
                        Else
                            srtReturnIdx = 5
                        End If
                    Else
                        srtReturnIdx = -1
                    End If
                Case CLAUSE_FREETEXT
                    If hasOptionalFreeText() Then
                        srtReturnIdx = 5
                        If hasOptionalPermissionClause() Then
                            srtReturnIdx = srtReturnIdx + CShort(1)
                        End If
                        If hasOptionalScaleClause() Then
                            srtReturnIdx = srtReturnIdx + CShort(1)
                        End If
                    Else
                        srtReturnIdx = -1
                    End If

            End Select
        End If
        Return srtReturnIdx
    End Function


    ''' <summary>
    ''' This method checks the syntax of the name in the string. If the name is parsable then the
    ''' getNameValidityStatus() is also called and its results incorporated to give full information
    ''' about the status of the name.
    ''' </summary>
    ''' <param name="strTestName">A string representing the string to be tested.</param>
    ''' <returns>A Long which is the sum of the dnNameStatus emnumeration members.</returns>
    ''' <remarks>
    ''' This method checks the syntax of the name in the string. If the name is parsable then the
    ''' getNameValidityStatus() is also called and its results incorporated to give full information
    ''' about the status of the name.
    ''' 
    ''' Implenemtnation note: This method requires that the name for testing is passed as a string 
    ''' even though it is testing it self. This is to allow the method to be called recurvisly with
    ''' variations on the name (eg substitutin underscourse for hyphens).
    ''' </remarks>
    Private Function getNameSyntaxStatus(ByVal strTestName As String) As Long
        Dim lngResultVal As Long
        Dim lstNameParts As List(Of String)

        lngResultVal = dnNameStatus.UNKNOWN_STATUS

        'Check Zero
        'does it contain hyphens "-" which probably should be underscorces "_"
        If InStr(strTestName, "-") <> 0 Then
            lngResultVal = lngResultVal Or dnNameStatus.SYNTAX_ERROR_CONTAINS_HYPHENS Or _
                           getNameSyntaxStatus(strTestName.Replace("-", "_"))
        ElseIf InStr(strTestName, "__") <> 0 Then
            lngResultVal = lngResultVal Or dnNameStatus.SYNTAX_ERROR_DOUBLE_UNDERSCORE Or _
                           getNameSyntaxStatus(strTestName.Replace("__", "_"))
        Else
            'Check one
            'does at least five components?  5 <= nameParts
            lstNameParts = New List(Of String)(Strings.Split(strTestName, "_"))

            If lstNameParts.Count < 5 Then
                lngResultVal = lngResultVal Or dnNameStatus.SYNTAX_ERROR_TOO_FEW_CLAUSES
            Else
                'Now check the actual validity of the individual clauses
                'lngResultVal = lngResultVal Or getNameValidityStatus(New List(Of String)(nameParts))
                lngResultVal = lngResultVal Or getNameValidityStatus(lstNameParts)
            End If
        End If

        Return lngResultVal

    End Function


    ''' <summary>
    ''' This method tests whether or not each of the individual clauses in a parsable data name are
    ''' contained in the relevant Data Name Clause Lookup tables.
    ''' </summary>
    ''' <param name="lstCurNameParts">An ordered list of some of the clauses of a data name.</param>
    ''' <param name="strClauseName">The name of the next clause to test (ie the clause postition zero).
    ''' Defaults to "Geoextent" ie the first clause of the data name.</param>
    ''' <returns>A Long which is the sum of the dnNameStatus emnumeration members. Only those emumerations
    ''' with the prefix INVALID, WARN, or INFO will be included.</returns>
    ''' <remarks>
    ''' This method tests whether or not each of the individual clauses in a parsable data name are
    ''' contained in the relevant Data Name Clause Lookup tables.
    ''' 
    ''' Implenmentation note: The lstCurNameParts parameter is passed ByRef. The implenmentation
    ''' will consume the List object, removing elements from it as the metod recuses. Therefore the
    ''' caller should not expect to be able to use the object passed to this parameter after this
    ''' method has been called. If for any reason the caller still requires this object they should
    ''' copy it before calling this method.
    ''' 
    ''' Implenemtnation note: This method requires that the name for testing is passed as a string 
    ''' even though it is testing it self. This is to allow the method to be called recurvisly with
    ''' variations on the name.
    ''' </remarks>
    Private Function getNameValidityStatus(ByRef lstCurNameParts As List(Of String), Optional ByVal strClauseName As String = CLAUSE_GEOEXTENT) As Long
        Dim lngResultVal As Long, lngTempResult As Long

        '  geoextent_datacategory_theme_datatype[_scale]_source[_permission][_FreeText]
        '  #1        #2           #3    #4        #5     #6      #7           #8
        'system.console.WriteLine("nameValidityStatus: ")

        Select Case strClauseName
            Case CLAUSE_GEOEXTENT
                If lstCurNameParts.Count < 1 OrElse lstCurNameParts.Item(0) Is Nothing Then
                    lngResultVal = lngResultVal Or dnNameStatus.SYNTAX_ERROR_TOO_FEW_CLAUSES Or dnNameStatus.INVALID_GEOEXTENT
                Else
                    'system.console.WriteLine("CLAUSE_GEOEXTENT: " & curNameParts(0))

                    If Not m_DNCL.isValidGeoextentClause(lstCurNameParts.Item(0)) Then
                        lngResultVal = lngResultVal Or dnNameStatus.INVALID_GEOEXTENT
                    End If

                    lstCurNameParts.RemoveAt(0)
                    lngResultVal = lngResultVal Or getNameValidityStatus(lstCurNameParts, CLAUSE_DATACATEGORY)
                    'Array.Copy(lstCurNameParts, 1, nextNameParts, 0, (lstCurNameParts.Count - 1))
                    'lngResultVal = lngResultVal Or getNameValidityStatus(nextNameParts, CLAUSE_DATACATEGORY)
                End If

            Case CLAUSE_DATACATEGORY
                'Since the DataCategory and DataTheme are nested it makes sence to test the two clauses together.
                'This Select Case is present for consistancy, so that this function proceedes parsing the DataName
                'in the order from left to right.
                If lstCurNameParts.Count < 1 OrElse lstCurNameParts.Item(0) Is Nothing Then
                    lngResultVal = lngResultVal Or dnNameStatus.SYNTAX_ERROR_TOO_FEW_CLAUSES Or dnNameStatus.INVALID_DATACATEGORY
                Else
                    'system.console.WriteLine("CLAUSE_DATACATEGORY: " & curNameParts(0))

                    'Note that we deliberately pass all of the currentNameParts without removing the left end clause.
                    lngResultVal = lngResultVal Or getNameValidityStatus(lstCurNameParts, CLAUSE_DATATHEME)
                End If

            Case CLAUSE_DATATHEME
                'We need a minimum of two clauses here and then must truncate by two clauses before moving on to the
                'next clause
                If lstCurNameParts.Count < 2 OrElse lstCurNameParts.Item(0) Is Nothing Then
                    lngResultVal = lngResultVal Or dnNameStatus.SYNTAX_ERROR_TOO_FEW_CLAUSES Or dnNameStatus.INVALID_DATATHEME
                Else
                    'system.console.WriteLine("CLAUSE_DATATHEME: " & curNameParts(0))

                    'Are the next two clauses valid DataCategory and DataTheme respectively?
                    If Not m_DNCL.isValidDataThemeClause(lstCurNameParts(1), lstCurNameParts.Item(0)) Then
                        lngResultVal = lngResultVal Or dnNameStatus.INVALID_DATATHEME
                        'Check Three.one
                        If Not m_DNCL.isValidDataCategoryClause(lstCurNameParts(1)) Then
                            lngResultVal = lngResultVal Or dnNameStatus.INVALID_DATACATEGORY
                        End If
                    End If
                    'go ahead a check out results from the rest of the string array
                    'Note that we truncate the first TWO left end clauses in this case
                    'Array.Copy(lstCurNameParts, 2, nextNameParts, 0, (lstCurNameParts.Count - 2))
                    lstCurNameParts.RemoveRange(0, 2)
                    lngResultVal = lngResultVal Or getNameValidityStatus(lstCurNameParts, CLAUSE_DATATYPE)

                End If

            Case CLAUSE_DATATYPE
                'NOTE THAT THIS DOES NOT TEST WHETHER THE DATA TYPE ACURATELY REFLECTS THE UNDERLYING DATA!
                If lstCurNameParts.Count < 1 OrElse lstCurNameParts.Item(0) Is Nothing Then
                    lngResultVal = lngResultVal Or dnNameStatus.SYNTAX_ERROR_TOO_FEW_CLAUSES Or dnNameStatus.INVALID_DATATYPE
                Else
                    'system.console.WriteLine("CLAUSE_DATATYPE: " & curNameParts(0))

                    If Not m_DNCL.isValidDataTypeClause(lstCurNameParts.Item(0)) Then
                        'system.console.WriteLine("Not myDNCL.isvalidDataTypeClause(curNameParts(0))")
                        lngResultVal = lngResultVal Or dnNameStatus.INVALID_DATATYPE
                    End If

                    'system.console.WriteLine(" getUnderlyingDataType() " & getUnderlyingDataType() & "  curNameParts(0)  " & curNameParts(0))
                    If Not lstCurNameParts.Item(0).Equals(getUnderlyingDataType()) Then
                        'system.console.WriteLine("Not curNameParts(0).Equals(getUnderlyingDataType()")
                        lngResultVal = lngResultVal Or dnNameStatus.INCORRECT_DATATYPE
                    End If

                    'Array.Copy(lstCurNameParts, 1, nextNameParts, 0, (lstCurNameParts.Count - 1))
                    lstCurNameParts.RemoveAt(0)
                    lngResultVal = lngResultVal Or getNameValidityStatus(lstCurNameParts, CLAUSE_SCALE)
                End If

            Case CLAUSE_SCALE
                If lstCurNameParts.Count < 1 OrElse lstCurNameParts.Item(0) Is Nothing Then
                    'we've got to the end without finding a scale clause which means we have problems!
                    lngResultVal = lngResultVal Or dnNameStatus.INVALID_SOURCE Or dnNameStatus.WARN_MISSING_SCALE_CLAUSE
                Else
                    'system.console.WriteLine("CLAUSE_SCALE: " & curNameParts(0))
                    Dim lstTempNameParts As List(Of String)

                    'This time we make a temporary copy lilst of name parts in order to enable a
                    'forward lookup to the source clause before deciding what to do with the scale clause.
                    lstTempNameParts = New List(Of String)(lstCurNameParts)

                    'go ahead a check out results from the rest of the string array
                    'Array.Copy(lstCurNameParts, 1, nextNameParts, 0, (lstCurNameParts.Count - 1))
                    lstTempNameParts.RemoveAt(0)
                    lngTempResult = getNameValidityStatus(lstTempNameParts, CLAUSE_SOURCE)

                    If m_DNCL.isValidScaleClause(lstCurNameParts.Item(0)) Then
                        'Scale is present which is good, move on to the next thing - source
                        lngResultVal = lngResultVal Or lngTempResult
                    Else
                        If (lngTempResult And dnNameStatus.INVALID_SOURCE) = dnNameStatus.INVALID_SOURCE Then
                            'the next clause isn't a source clause so assume that this one is the
                            'source clase and that there is no scale clause
                            lngResultVal = lngResultVal Or dnNameStatus.WARN_MISSING_SCALE_CLAUSE
                            lngResultVal = lngResultVal Or getNameValidityStatus(lstCurNameParts, CLAUSE_SOURCE)
                        Else
                            'assume that the next clause is the source clause so join that into the result
                            lngResultVal = lngResultVal Or lngTempResult
                        End If
                    End If
                End If

            Case CLAUSE_SOURCE
                If lstCurNameParts.Count < 1 OrElse lstCurNameParts.Item(0) Is Nothing Then
                    'we've got to the end without finding a source clause which means we have problems!
                    lngResultVal = lngResultVal Or dnNameStatus.INVALID_SOURCE
                Else
                    'system.console.WriteLine("CLAUSE_SOURCE: " & curNameParts(0))

                    If Not m_DNCL.isValidSourceClause(lstCurNameParts.Item(0)) Then
                        'Either the source clause is incorrect OR the scale clause has been
                        'incorrectly assigned as missing/present. Either way return an incorrect source error.
                        lngResultVal = lngResultVal Or dnNameStatus.INVALID_SOURCE
                    End If
                    'Whether or not the source clause is correct, since it is not optional, at this point
                    'assume that the next cluase will the the permissions.
                    'Array.Copy(lstCurNameParts, 1, nextNameParts, 0, (lstCurNameParts.Count - 1))
                    lstCurNameParts.RemoveAt(0)
                    lngResultVal = lngResultVal Or getNameValidityStatus(lstCurNameParts, CLAUSE_PERMISSIONS)
                End If

            Case CLAUSE_PERMISSIONS
                If lstCurNameParts.Count < 1 OrElse lstCurNameParts.Item(0) Is Nothing Then
                    'we've got to the end without finding an optional permissions clause.
                    lngResultVal = lngResultVal Or dnNameStatus.WARN_MISSING_PERMISSIONS_CLAUSE
                Else
                    'system.console.WriteLine("CLAUSE_PERMISSIONS: " & curNameParts(0))

                    If m_DNCL.isValidPermissionsClause(lstCurNameParts.Item(0)) Then
                        'Permissions is present which is good, move on to the next thing but we don't care about
                        'short free text now, so we only need to know if there is more text
                        If lstCurNameParts.Count > 1 Then
                            lngResultVal = lngResultVal Or dnNameStatus.INFO_FREE_TEXT_PRESENT
                            ''system.console.WriteLine("free-text A ")
                        End If
                    Else
                        'go ahead a check out results from the rest of the string array
                        lngResultVal = lngResultVal Or dnNameStatus.WARN_MISSING_PERMISSIONS_CLAUSE
                        'Array.Copy(lstCurNameParts, 1, nextNameParts, 0, (lstCurNameParts.Count - 1))
                        lstCurNameParts.RemoveAt(0)
                        lngResultVal = lngResultVal Or getNameValidityStatus(lstCurNameParts, CLAUSE_FREETEXT)
                    End If
                End If

            Case CLAUSE_FREETEXT
                ''system.console.WriteLine("free-text  " & curNameParts(0))

                'If lstCurNameParts.Item(0) Is Nothing OrElse lstCurNameParts.Count < 1 Then
                If lstCurNameParts.Count < 1 OrElse lstCurNameParts.Item(0) Is Nothing Then
                    'we've got to the end without finding any free text
                Else
                    'system.console.WriteLine("CLAUSE_FREETEXT: " & curNameParts(0))

                    If lstCurNameParts.Item(0).Length = 2 Then
                        'two character long free text is present
                        lngResultVal = lngResultVal Or dnNameStatus.WARN_TWO_CHAR_FREE_TEXT Or dnNameStatus.INFO_FREE_TEXT_PRESENT
                        ''system.console.WriteLine("free-text B ")
                    Else
                        lngResultVal = lngResultVal Or dnNameStatus.INFO_FREE_TEXT_PRESENT
                        ''system.console.WriteLine("free-text C " & curNameParts(0) & "  " & curNameParts(0).Length)
                    End If
                End If

        End Select

        Return lngResultVal
    End Function

    Public Function checkPropossedNameStatus(ByVal str_PropossedName As String) As Long Implements IDataName.checkPropossedNameStatus
        Return getNameSyntaxStatus(str_PropossedName)
    End Function

End Class
