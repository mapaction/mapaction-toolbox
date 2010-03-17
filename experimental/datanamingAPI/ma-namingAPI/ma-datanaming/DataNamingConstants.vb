
Public Module DataNamingConstants

    'Constansts relating to the status of a particular Data Name
    Public Const DATANAME_UNKNOWN_STATUS = 0
    Public Const DATANAME_VALID = -1

    Public Const DATANAME_ERROR = 2
    Public Const DATANAME_WARN = 4
    Public Const DATANAME_INFO = 8

    Public Const DATANAME_ERROR_INVALID_GEOEXTENT = DATANAME_ERROR Or (2 ^ 4)
    Public Const DATANAME_ERROR_INVALID_DATACATEGORY = DATANAME_ERROR Or (2 ^ 5)
    Public Const DATANAME_ERROR_INVALID_DATATHEME = DATANAME_ERROR Or (2 ^ 6)
    Public Const DATANAME_ERROR_INVALID_DATATYPE = DATANAME_ERROR Or (2 ^ 7)
    Public Const DATANAME_ERROR_INCORRECT_DATATYPE = DATANAME_ERROR Or (2 ^ 8)
    Public Const DATANAME_ERROR_INCORRECT_SCALE = DATANAME_ERROR Or (2 ^ 8)
    Public Const DATANAME_ERROR_INCORRECT_SOURCE = DATANAME_ERROR Or (2 ^ 8)
    Public Const DATANAME_ERROR_INCORRECT_PERMISSIONS = DATANAME_ERROR Or (2 ^ 8)
    Public Const DATANAME_ERROR_OTHER_ERROR = DATANAME_ERROR Or (2 ^ 9)
    Public Const DATANAME_ERROR_CONTAINS_HYPHENS = DATANAME_ERROR Or (2 ^ 12)
    Public Const DATANAME_ERROR_TOO_FEW_CLAUSES = DATANAME_ERROR Or (2 ^ 13)

    Public Const DATANAME_WARN_MISSING_SCALE_CLAUSE = DATANAME_WARN Or (2 ^ 14)
    Public Const DATANAME_WARN_MISSING_PERMISSIONS_CLAUSE = DATANAME_WARN Or (2 ^ 15)
    Public Const DATANAME_WARN_TWO_CHAR_FREE_TEXT = DATANAME_WARN Or (2 ^ 16)  '_MAYBE_ERRONEOUS_PERMISSION_CLAUSE

    Public Const DATANAME_INFO_FREE_TEXT_PRESENT = DATANAME_INFO Or (2 ^ 17)

    'Constants relating to the status of an IDataListConnection object
    Public Const DATALIST_TYPE_UNKNOWN = (2 ^ 1)
    Public Const DATALIST_TYPE_GDB = (2 ^ 2)
    Public Const DATALIST_TYPE_DIR = (2 ^ 3)
    Public Const DATALIST_TYPE_MXD = (2 ^ 4)
    Public Const DATALIST_TYPE_MIXED_FILES = (2 ^ 5)


    'Constants relating to the names of table which store all of the data name clauses
    Public Const TABLENAME_GEOEXTENT = "datanaming_clause_geoextent"
    Public Const TABLENAME_DATA_CAT = "datanaming_clause_data_categories"
    Public Const TABLENAME_DATA_THEME = "datanaming_clause_data_theme"
    Public Const TABLENAME_DATA_TYPE = "datanaming_clause_data_type"
    Public Const TABLENAME_SCALE = "datanaming_clause_scale"
    Public Const TABLENAME_SOURCE = "datanaming_clause_source"
    Public Const TABLENAME_PERMISSION = "datanaming_clause_permission"

    Public Const PRI_KEY_COL_NAME = "clause"

    Private allTableNames() = {TABLENAME_GEOEXTENT, _
                               TABLENAME_DATA_CAT, _
                               TABLENAME_DATA_THEME, _
                               TABLENAME_DATA_TYPE, _
                               TABLENAME_SCALE, _
                               TABLENAME_SOURCE, _
                               TABLENAME_PERMISSION}

    Public ReadOnly Property allDataNameTables() As String()
        Get
            allDataNameTables = allTableNames
        End Get
    End Property

    Private allDataColumnGroups As New Hashtable

    Private Sub initialiseDataColumnCollections()
        Dim myDataCols As ArrayList
        Dim myCol As DataColumn

        For Each tableName In allTableNames
            myDataCols = New ArrayList

            'Add same to columns to all tables
            myCol = New DataColumn()
            With myCol
                .ColumnName = "clause"
                .DataType = System.Type.GetType("System.String")
                .MaxLength = 20
                .Unique = True
                .AutoIncrement = False
                .Caption = "clause"
                .ReadOnly = False
            End With
            myDataCols.Add(myCol)

            myCol = New DataColumn()
            With myCol
                .ColumnName = "Description"
                .DataType = System.Type.GetType("System.String")
                .MaxLength = 255
                .Unique = True
                .AutoIncrement = False
                .Caption = "Description"
                .ReadOnly = False
            End With
            myDataCols.Add(myCol)

            'now add the columns specific to particular tables
            Select Case tableName
                Case TABLENAME_GEOEXTENT
                    'Country_or_Continent
                    myCol = New DataColumn()
                    With myCol
                        .ColumnName = "Geography_type"
                        .DataType = System.Type.GetType("System.String")
                        .MaxLength = 255
                        .Unique = False
                        .AutoIncrement = False
                        .Caption = "Geography_type"
                        .ReadOnly = False
                    End With
                    myDataCols.Add(myCol)

                Case TABLENAME_DATA_CAT
                    'Base_or_Situational
                    myCol = New DataColumn()
                    With myCol
                        .ColumnName = "Base_or_Situational"
                        .DataType = System.Type.GetType("System.String")
                        .MaxLength = 11
                        .Unique = False
                        .AutoIncrement = False
                        .Caption = "Base_or_Situational"
                        .ReadOnly = False
                    End With
                    myDataCols.Add(myCol)

                Case TABLENAME_DATA_THEME
                    'Data_Category
                    myCol = New DataColumn()
                    With myCol
                        .ColumnName = "Data_Category"
                        .DataType = System.Type.GetType("System.String")
                        .MaxLength = 20
                        .Unique = False
                        .AutoIncrement = False
                        .Caption = "Data_Category"
                        .ReadOnly = False
                    End With
                    myDataCols.Add(myCol)

                Case TABLENAME_DATA_TYPE
                    'None

                Case TABLENAME_SCALE
                    'Numerical_range, 50
                    myCol = New DataColumn()
                    With myCol
                        .ColumnName = "Numerical_range"
                        .DataType = System.Type.GetType("System.String")
                        .MaxLength = 50
                        .Unique = False
                        .AutoIncrement = False
                        .Caption = "Numerical_range"
                        .ReadOnly = False
                    End With
                    myDataCols.Add(myCol)

                Case TABLENAME_SOURCE
                    'Organisation_Name_if_different 255
                    myCol = New DataColumn()
                    With myCol
                        .ColumnName = "Organisation_Name_if_different"
                        .DataType = System.Type.GetType("System.String")
                        .MaxLength = 255
                        .Unique = False
                        .AutoIncrement = False
                        .Caption = "Organisation_Name_if_different"
                        .ReadOnly = False
                    End With
                    myDataCols.Add(myCol)

                    'Generic_or_Specific 8
                    myCol = New DataColumn()
                    With myCol
                        .ColumnName = "Generic_or_Specific"
                        .DataType = System.Type.GetType("System.String")
                        .MaxLength = 8
                        .Unique = False
                        .AutoIncrement = False
                        .Caption = "Generic_or_Specific"
                        .ReadOnly = False
                    End With
                    myDataCols.Add(myCol)

                    'Org_or_Dataset 12
                    myCol = New DataColumn()
                    With myCol
                        .ColumnName = "Org_or_Dataset"
                        .DataType = System.Type.GetType("System.String")
                        .MaxLength = 12
                        .Unique = False
                        .AutoIncrement = False
                        .Caption = "Org_or_Dataset"
                        .ReadOnly = False
                    End With
                    myDataCols.Add(myCol)

                    'Website 255
                    myCol = New DataColumn()
                    With myCol
                        .ColumnName = "Website"
                        .DataType = System.Type.GetType("System.String")
                        .MaxLength = 255
                        .Unique = False
                        .AutoIncrement = False
                        .Caption = "Website"
                        .ReadOnly = False
                    End With
                    myDataCols.Add(myCol)

                    'Marginalia_Notes 4098
                    myCol = New DataColumn()
                    With myCol
                        .ColumnName = "Marginalia_Notes"
                        .DataType = System.Type.GetType("System.String")
                        .MaxLength = 4098
                        .Unique = False
                        .AutoIncrement = False
                        .Caption = "Marginalia_Notes"
                        .ReadOnly = False
                    End With
                    myDataCols.Add(myCol)

                Case TABLENAME_PERMISSION
                    'Possible_example_datasets 255
                    myCol = New DataColumn()
                    With myCol
                        .ColumnName = "Possible_example_datasets"
                        .DataType = System.Type.GetType("System.String")
                        .MaxLength = 255
                        .Unique = False
                        .AutoIncrement = False
                        .Caption = "Possible_example_datasets"
                        .ReadOnly = False
                    End With
                    myDataCols.Add(myCol)

            End Select

            allDataColumnGroups.Add(tableName, myDataCols)
        Next
    End Sub

    Public ReadOnly Property allDataNameColumns() As Hashtable
        Get
            allDataNameColumns = allDataColumnGroups
        End Get
    End Property

    Public ReadOnly Property table_columns_Geo_Extent() As ArrayList
        Get
            table_columns_Geo_Extent = CType(allDataColumnGroups.Item(TABLENAME_GEOEXTENT), ArrayList)
        End Get
    End Property

    Public ReadOnly Property table_columns_Data_Cat() As ArrayList
        Get
            table_columns_Data_Cat = CType(allDataColumnGroups.Item(TABLENAME_DATA_CAT), ArrayList)
        End Get
    End Property

    Public ReadOnly Property table_columns_Data_Theme() As ArrayList
        Get
            table_columns_Data_Theme = CType(allDataColumnGroups.Item(TABLENAME_DATA_THEME), ArrayList)
        End Get
    End Property

    Public ReadOnly Property table_columns_Data_Type() As ArrayList
        Get
            table_columns_Data_Type = CType(allDataColumnGroups.Item(TABLENAME_DATA_TYPE), ArrayList)
        End Get
    End Property

    Public ReadOnly Property table_columns_Scale() As ArrayList
        Get
            table_columns_Scale = CType(allDataColumnGroups.Item(TABLENAME_SCALE), ArrayList)
        End Get
    End Property

    Public ReadOnly Property table_columns_Source() As ArrayList
        Get
            table_columns_Source = CType(allDataColumnGroups.Item(TABLENAME_SOURCE), ArrayList)
        End Get
    End Property

    Public ReadOnly Property table_columns_Permission() As ArrayList
        Get
            table_columns_Permission = CType(allDataColumnGroups.Item(TABLENAME_PERMISSION), ArrayList)
        End Get
    End Property


    Sub New()
        initialiseDataColumnCollections()
    End Sub

End Module
