Public MustInherit Class AbstractDataName
    Implements IDataName

    Protected Friend myNameStr As String
    Protected Friend myDNCL As IDataNameClauseLookup

    

    Public Function getNameStr() As String Implements IDataName.getNameStr
        getNameStr = myNameStr
    End Function

    Public MustOverride Function getPathStr() As String Implements IDataName.getPathStr

    Public Function getNameAndFullPathStr() As String Implements IDataName.getNameAndFullPathStr
        getNameAndFullPathStr = getPathStr() & "\" & getNameStr()
    End Function

    
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsNameValid() As Integer Implements IDataName.IsNameValid
        IsNameValid = myDNCL.getNameStatus(myNameStr)
    End Function

    Public MustOverride Function isRenameable() As Boolean Implements IDataName.isRenameable

    Public Function hasOptionalScaleClause() As Boolean Implements IDataName.hasOptionalScaleClause
        Dim bitSum As Integer
        Dim myResult As Boolean

        bitSum = IsNameValid()

        myResult = (Not (bitSum And DATANAME_ERROR) = DATANAME_ERROR) And _
           (Not (bitSum And DATANAME_WARN_MISSING_SCALE_CLAUSE) = DATANAME_WARN_MISSING_SCALE_CLAUSE)

        hasOptionalScaleClause = myResult
    End Function

    Public Function hasOptionalPermissionClause() As Boolean Implements IDataName.hasOptionalPermissionClause
        Dim bitSum As Integer
        Dim myResult As Boolean

        bitSum = IsNameValid()

        myResult = (Not (bitSum And DATANAME_ERROR) = DATANAME_ERROR) And _
           (Not (bitSum And DATANAME_WARN_MISSING_PERMISSIONS_CLAUSE) = DATANAME_WARN_MISSING_PERMISSIONS_CLAUSE)

        hasOptionalPermissionClause = myResult
    End Function

    Public Function hasOptionalFreeText() As Boolean Implements IDataName.hasOptionalFreeText
        Dim bitSum As Integer
        Dim myResult As Boolean

        bitSum = IsNameValid()

        myResult = (Not (bitSum And DATANAME_ERROR) = DATANAME_ERROR) And _
           ((bitSum And DATANAME_INFO_FREE_TEXT_PRESENT) = DATANAME_INFO_FREE_TEXT_PRESENT)

        hasOptionalFreeText = myResult
    End Function


    '
    '
    '
    Public Function changeGeoExtentClause(ByVal newGeoExtent As String) As Integer Implements IDataName.changeGeoExtentClause
        'changeGeoExtentClause = Nothing
    End Function

    Public Function changeDataCategoryClause(ByVal newDataCategory As String) As Integer Implements IDataName.changeDataCategoryClause
        'changeDataCategoryClause = Nothing
    End Function

    Public Function changeDataThemeClause(ByVal newDataTheme As String) As Integer Implements IDataName.changeDataThemeClause
        'changeDataThemeClause = Nothing
    End Function

    Public Function changeDataTypeClause(ByVal newDataType As String) As Integer Implements IDataName.changeDataTypeClause
        'changeDataTypeClause = Nothing
    End Function

    Function changeScaleCodesClause(ByVal newDataTheme As String) As Integer Implements IDataName.changeScaleCodesClause
        'changeScaleCodesClause = Nothing
    End Function

    Function changeSourceCodesClause(ByVal newDataTheme As String) As Integer Implements IDataName.changeSourceCodesClause
        'changeSourceCodesClause = Nothing
    End Function

    Function changePermissionsClause(ByVal newDataTheme As String) As Integer Implements IDataName.changePermissionsClause
        'changePermissionsClause = Nothing
    End Function

    Function changeFreeTextClause(ByVal newDataTheme As String) As Integer Implements IDataName.changeFreeTextClause
        'changeFreeTextClause = Nothing
    End Function


    Public Sub New(ByVal theName As String, ByRef theDNCL As IDataNameClauseLookup)
        myNameStr = theName
        myDNCL = theDNCL
    End Sub
End Class
