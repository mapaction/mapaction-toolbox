
Public MustInherit Class AbstractDataNameClauseLookup
    Implements IDataNameClauseLookup

    Private myGeoExtentTable As DataTable
    Private myDataCategoriesTable As DataTable
    Private myDataThemeTable As DataTable
    Private myDataTypeTable As DataTable
    Private myScaleTable As DataTable
    Private mySourceTable As DataTable
    Private myPermissionTable As DataTable

    Protected Sub New()
        initialiseAllTables()
    End Sub

    Protected Sub initialiseAllTables()
        Dim myTable As DataTable
        Dim myColArrayList As ArrayList

        For Each tableName In allDataNameTables
            myTable = openTable(tableName)
            myColArrayList = New ArrayList

            For Each col In myTable.Columns
                myColArrayList.Add(col)
            Next

            If Not doDataColumnsMatch(CType(allDataNameColumns.Item(tableName), ArrayList), myColArrayList) Then
                'Raise an exception
            Else
                Select Case tableName
                    Case TABLENAME_GEOEXTENT
                        myGeoExtentTable = myTable

                    Case TABLENAME_DATA_CAT
                        myDataCategoriesTable = myTable

                    Case TABLENAME_DATA_THEME
                        myDataThemeTable = myTable

                    Case TABLENAME_DATA_TYPE
                        myDataTypeTable = myTable

                    Case TABLENAME_SCALE
                        myScaleTable = myTable

                    Case TABLENAME_SOURCE
                        mySourceTable = myTable

                    Case TABLENAME_PERMISSION
                        myPermissionTable = myTable

                End Select
            End If
        Next
    End Sub

    Protected MustOverride Function openTable(ByVal tableName As String) As DataTable

    Public MustOverride Function isWriteable() As Boolean Implements IDataNameClauseLookup.isWriteable

    ''' <summary>
    ''' Is this really the best way to check the schema of the DB?
    ''' Columns must be in the same order.
    ''' </summary>
    ''' <param name="dcal1"></param>
    ''' <param name="dcal2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function doDataColumnsMatch(ByRef dcal1 As ArrayList, ByRef dcal2 As ArrayList) As Boolean
        Dim returnRes As Boolean
        Dim curIndx As Integer
        Dim dc1 As DataColumn, dc2 As DataColumn

        'assume true until proved otherwise
        returnRes = True

        If dcal1.Count <> dcal2.Count Then
            returnRes = False
        Else
            For curIndx = 0 To (dcal1.Count - 1)
                dc1 = CType(dcal1.Item(curIndx), DataColumn)
                dc2 = CType(dcal2.Item(curIndx), DataColumn)

                If dc1.ColumnName <> dc2.ColumnName _
                Or dc1.DataType.FullName <> dc2.DataType.FullName _
                Or dc1.MaxLength <> dc2.MaxLength _
                Or dc1.Unique <> dc2.Unique _
                Or dc1.AutoIncrement <> dc2.AutoIncrement _
                Or dc1.Caption <> dc2.Caption _
                Or dc1.ReadOnly <> dc2.ReadOnly Then
                    returnRes = False
                End If
            Next
        End If

        doDataColumnsMatch = returnRes
    End Function


    Private Function getListFromTable(ByRef theDataTable As DataTable) As List(Of String)
        Dim dtr As DataTableReader
        Dim pkIdx As Integer
        Dim myList As New List(Of String)

        dtr = theDataTable.CreateDataReader(PRI_KEY_COL_NAME)
        pkIdx = theDataTable.Columns.IndexOf(PRI_KEY_COL_NAME)

        While dtr.HasRows
            myList.Add(dtr.GetString(pkIdx))
            dtr.NextResult()
        End While

        getListFromTable = myList
    End Function

    Private Function isStrInPriKeyCol(ByRef theStr As String, ByRef theDataTable As DataTable) As Boolean
        Dim dtr As DataTableReader
        Dim pkIdx As Integer
        Dim strPresent As Boolean

        strPresent = False

        dtr = theDataTable.CreateDataReader(PRI_KEY_COL_NAME)
        pkIdx = theDataTable.Columns.IndexOf(PRI_KEY_COL_NAME)

        While dtr.HasRows And Not strPresent
            If dtr.GetString(pkIdx) = theStr Then
                strPresent = True
            Else
                dtr.NextResult()
            End If
        End While

        isStrInPriKeyCol = strPresent
    End Function

    Public Function getDataCategoryList() As List(Of String) Implements IDataNameClauseLookup.getDataCategoryList
        getDataCategoryList = getListFromTable(myDataCategoriesTable)
    End Function

    Public Function getDataCategoryTable() As DataTable Implements IDataNameClauseLookup.getDataCategoryTable
        getDataCategoryTable = myDataCategoriesTable
    End Function

    Public Function getDataThemeList() As List(Of String) Implements IDataNameClauseLookup.getDataThemeList
        getDataThemeList = getListFromTable(myDataThemeTable)
    End Function

    Public Function getDataThemeTable() As DataTable Implements IDataNameClauseLookup.getDataThemeTable
        getDataThemeTable = myDataThemeTable
    End Function

    Public Function getDataTypeList() As List(Of String) Implements IDataNameClauseLookup.getDataTypeList
        getDataTypeList = getListFromTable(myDataTypeTable)
    End Function

    Public Function getDataTypeTable() As DataTable Implements IDataNameClauseLookup.getDataTypeTable
        getDataTypeTable = myDataTypeTable
    End Function

    Public Function getGeoExtentList() As List(Of String) Implements IDataNameClauseLookup.getGeoExtentList
        getGeoExtentList = getListFromTable(myGeoExtentTable)
    End Function

    Public Function getGeoExtentTable() As DataTable Implements IDataNameClauseLookup.getGeoExtentTable
        getGeoExtentTable = myGeoExtentTable
    End Function

    Public Function getPermissionsList() As List(Of String) Implements IDataNameClauseLookup.getPermissionsList
        getPermissionsList = getListFromTable(myPermissionTable)
    End Function

    Public Function getPermissionsTable() As DataTable Implements IDataNameClauseLookup.getPermissionsTable
        getPermissionsTable = myPermissionTable
    End Function

    Public Function getScaleCodesList() As List(Of String) Implements IDataNameClauseLookup.getScaleCodesList
        getScaleCodesList = getListFromTable(myScaleTable)
    End Function

    Public Function getScaleCodesTable() As DataTable Implements IDataNameClauseLookup.getScaleCodesTable
        getScaleCodesTable = myScaleTable
    End Function

    Public Function getSourceCodesList() As List(Of String) Implements IDataNameClauseLookup.getSourceCodesList
        getSourceCodesList = getListFromTable(mySourceTable)
    End Function

    Public Function getSourceCodesTable() As DataTable Implements IDataNameClauseLookup.getSourceCodesTable
        getSourceCodesTable = mySourceTable
    End Function

    Public Function isvalidDataCategoryClause(ByVal testDataCatClause As String) As Boolean Implements IDataNameClauseLookup.isvalidDataCategoryClause
        isvalidDataCategoryClause = isStrInPriKeyCol(testDataCatClause, myDataCategoriesTable)
    End Function

    Public MustOverride Function isvalidDataThemeClause(ByVal testDataThemeClause As String, ByVal testDataCatClause As String) As Boolean Implements IDataNameClauseLookup.isvalidDataThemeClause

    Public Function isvalidDataTypeClause(ByVal testDataTypeClause As String) As Boolean Implements IDataNameClauseLookup.isvalidDataTypeClause
        isvalidDataTypeClause = isStrInPriKeyCol(testDataTypeClause, myDataTypeTable)
    End Function

    Public Function isvalidGeoextentClause(ByVal testGeoExtentClause As String) As Boolean Implements IDataNameClauseLookup.isvalidGeoextentClause
        isvalidGeoextentClause = isStrInPriKeyCol(testGeoExtentClause, myGeoExtentTable)
    End Function

    Public Function isvalidPermissionsClause(ByVal testPermissionsClause As String) As Boolean Implements IDataNameClauseLookup.isvalidPermissionsClause
        isvalidPermissionsClause = isStrInPriKeyCol(testPermissionsClause, myPermissionTable)
    End Function

    Public Function isvalidScaleClause(ByVal testScaleClause As String) As Boolean Implements IDataNameClauseLookup.isvalidScaleClause
        isvalidScaleClause = isStrInPriKeyCol(testScaleClause, myScaleTable)
    End Function

    Public Function isvalidSourceClause(ByVal testSourceClause As String) As Boolean Implements IDataNameClauseLookup.isvalidSourceClause
        isvalidSourceClause = isStrInPriKeyCol(testSourceClause, mySourceTable)
    End Function

End Class
