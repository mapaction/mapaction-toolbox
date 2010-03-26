
Public Module DataNamingConstants

    'Constansts relating to the status of a particular Data Name
    Public Const DATANAME_UNKNOWN_STATUS As Long = 0
    Public Const DATANAME_VALID As Long = (2 ^ 20)

    Public Const DATANAME_ERROR As Long = 2
    Public Const DATANAME_WARN As Long = 4
    Public Const DATANAME_INFO As Long = 8

    Public Const DATANAME_ERROR_INVALID_GEOEXTENT As Long = DATANAME_ERROR Or (2 ^ 4)
    Public Const DATANAME_ERROR_INVALID_DATACATEGORY As Long = DATANAME_ERROR Or (2 ^ 5)
    Public Const DATANAME_ERROR_INVALID_DATATHEME As Long = DATANAME_ERROR Or (2 ^ 6)
    Public Const DATANAME_ERROR_INVALID_DATATYPE As Long = DATANAME_ERROR Or (2 ^ 7)
    Public Const DATANAME_ERROR_INCORRECT_DATATYPE As Long = DATANAME_ERROR Or (2 ^ 8)
    Public Const DATANAME_ERROR_INCORRECT_SCALE As Long = DATANAME_ERROR Or (2 ^ 9)
    Public Const DATANAME_ERROR_INCORRECT_SOURCE As Long = DATANAME_ERROR Or (2 ^ 10)
    Public Const DATANAME_ERROR_INCORRECT_PERMISSIONS As Long = DATANAME_ERROR Or (2 ^ 11)
    Public Const DATANAME_ERROR_OTHER_ERROR As Long = DATANAME_ERROR Or (2 ^ 12)
    Public Const DATANAME_ERROR_CONTAINS_HYPHENS As Long = DATANAME_ERROR Or (2 ^ 13)
    Public Const DATANAME_ERROR_TOO_FEW_CLAUSES As Long = DATANAME_ERROR Or (2 ^ 14)

    Public Const DATANAME_WARN_MISSING_SCALE_CLAUSE As Long = DATANAME_WARN Or (2 ^ 15)
    Public Const DATANAME_WARN_MISSING_PERMISSIONS_CLAUSE As Long = DATANAME_WARN Or (2 ^ 16)
    Public Const DATANAME_WARN_TWO_CHAR_FREE_TEXT As Long = DATANAME_WARN Or (2 ^ 17)  '_MAYBE_ERRONEOUS_PERMISSION_CLAUSE

    Public Const DATANAME_INFO_FREE_TEXT_PRESENT As Long = DATANAME_INFO Or (2 ^ 18)

    Private dataNameStrMessages As Hashtable

    'Constants relating to the status of an IDataListConnection object
    Public Const DATALIST_TYPE_UNKNOWN = (2 ^ 1)
    Public Const DATALIST_TYPE_GDB = (2 ^ 2)
    Public Const DATALIST_TYPE_DIR = (2 ^ 3)
    Public Const DATALIST_TYPE_MXD = (2 ^ 4)
    Public Const DATALIST_TYPE_MIXED_FILES = (2 ^ 5)

    'Constants relating to the type of DataNameClauseLookup used
    Public Const DATACLAUSE_LOOKUP_MDB = (2 ^ 1)
    Public Const DATACLAUSE_LOOKUP_ESRI_GDB = (2 ^ 2)


    'Constants relating to the names of table which store all of the data name clauses
    Public Const TABLENAME_GEOEXTENT As String = "datanaming_clause_geoextent"
    Public Const TABLENAME_DATA_CAT As String = "datanaming_clause_data_categories"
    Public Const TABLENAME_DATA_THEME As String = "datanaming_clause_data_theme"
    Public Const TABLENAME_DATA_TYPE As String = "datanaming_clause_data_type"
    Public Const TABLENAME_SCALE As String = "datanaming_clause_scale"
    Public Const TABLENAME_SOURCE As String = "datanaming_clause_source"
    Public Const TABLENAME_PERMISSION As String = "datanaming_clause_permission"

    Public Const PRI_KEY_COL_NAME = "clause"

    Private allTableNames = New String() {CType(TABLENAME_GEOEXTENT, String), _
                           CType(TABLENAME_DATA_CAT, String), _
                           CType(TABLENAME_DATA_THEME, String), _
                           CType(TABLENAME_DATA_TYPE, String), _
                           CType(TABLENAME_SCALE, String), _
                           CType(TABLENAME_SOURCE, String), _
                           CType(TABLENAME_PERMISSION, String)}

    Public ReadOnly Property allDataNameTables() As String()
        Get
            'Dim allTableNames() As String

            'Dim Test() As Integer
            ''declaring a Test array
            'Test=New Integer(){1,3,5,7,9,} 

            allDataNameTables = allTableNames
        End Get
    End Property

    Public ReadOnly Property allDataNameStrMessages() As Hashtable
        Get
            allDataNameStrMessages = dataNameStrMessages
        End Get
    End Property

    Private allDataColumnGroups As New Hashtable

    Public Sub initialiseDataNameStrMessages()
        dataNameStrMessages = New Hashtable(20)
        'dataNameStrMessages
        dataNameStrMessages.Add(DATANAME_ERROR_INVALID_GEOEXTENT, "ERROR: GeoExtent Clause not in list of recognised clauses")
        dataNameStrMessages.Add(DATANAME_ERROR_INVALID_DATACATEGORY, "ERROR: Data Category Clause not in list of recognised clauses")
        dataNameStrMessages.Add(DATANAME_ERROR_INVALID_DATATHEME, "ERROR: Data Theme not regonised, or not valid for Data Category")
        dataNameStrMessages.Add(DATANAME_ERROR_INVALID_DATATYPE, "ERROR: Data Type Clause not in list of recognised clauses")
        dataNameStrMessages.Add(DATANAME_ERROR_INCORRECT_DATATYPE, "ERROR: Data Type Clause does not match underlying data type")
        dataNameStrMessages.Add(DATANAME_ERROR_INCORRECT_SCALE, "ERROR: Data Scale Clause Clause not in list of recognised clauses")
        dataNameStrMessages.Add(DATANAME_ERROR_INCORRECT_SOURCE, "ERROR: Source Clause not in list of recognised clauses")
        dataNameStrMessages.Add(DATANAME_ERROR_INCORRECT_PERMISSIONS, "ERROR: Permissions Clause not in list of recognised clauses")
        dataNameStrMessages.Add(DATANAME_ERROR_OTHER_ERROR, "ERROR: General Error phasing data name")
        dataNameStrMessages.Add(DATANAME_ERROR_CONTAINS_HYPHENS, "ERROR: Data Name contains hyphens")
        dataNameStrMessages.Add(DATANAME_ERROR_TOO_FEW_CLAUSES, "ERROR: Too few clauses in Data Name")

        dataNameStrMessages.Add(DATANAME_WARN_MISSING_SCALE_CLAUSE, "WARNING: Optional Scale Clause not present")
        dataNameStrMessages.Add(DATANAME_WARN_MISSING_PERMISSIONS_CLAUSE, "WARNING: Optional Permissions Clause not present")
        dataNameStrMessages.Add(DATANAME_WARN_TWO_CHAR_FREE_TEXT, "WARNING: Two charater long free text, could be misformed permissions clause")

        dataNameStrMessages.Add(DATANAME_INFO_FREE_TEXT_PRESENT, "INFO: Free text clause is present")

        dataNameStrMessages.Add(DATANAME_VALID, "Data Name parsed correctly")

    End Sub

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
        initialiseDataNameStrMessages()
    End Sub

End Module
