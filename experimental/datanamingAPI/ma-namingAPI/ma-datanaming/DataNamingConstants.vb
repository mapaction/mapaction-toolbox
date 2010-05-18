
'todo LOW: Substainial parts of this module should be converted to read values from an .ini file or simular


Public Enum dnNameStatus As Long
    'Constansts relating to the status of a particular Data Name
    UNKNOWN_STATUS = 0
    'Removed the "is valid" flag as depending on context this is any 
    'combination of "not DATANAME_INVALID", "not DATANAME_SYNTAX_ERROR" and maybe "not DATANAME_WARN"
    'Public Const DATANAME_VALID As Long = (2 ^ 20)

    ' Overall Status
    ' Start at 2^1
    INVALID = 2
    SYNTAX_ERROR = 4
    WARN = 8
    INFO = 16

    ' Invalid....
    ' Start at 2^10
    INVALID_GEOEXTENT = INVALID Or (2 ^ 10)
    INVALID_DATACATEGORY = INVALID Or (2 ^ 11)
    INVALID_DATATHEME = INVALID Or (2 ^ 12)
    INVALID_DATATYPE = INVALID Or (2 ^ 13)
    INCORRECT_DATATYPE = INVALID Or (2 ^ 14)
    INVALID_SCALE = INVALID Or (2 ^ 15)
    INVALID_SOURCE = INVALID Or (2 ^ 16)
    INVALID_PERMISSIONS = INVALID Or (2 ^ 17)

    ' Syntax Errors
    ' Start at 2^20
    SYNTAX_ERROR_OTHER = SYNTAX_ERROR Or (2 ^ 20)
    SYNTAX_ERROR_CONTAINS_HYPHENS = SYNTAX_ERROR Or (2 ^ 21)
    SYNTAX_ERROR_TOO_FEW_CLAUSES = SYNTAX_ERROR Or (2 ^ 22)
    SYNTAX_ERROR_DOUBLE_UNDERSCORE = SYNTAX_ERROR Or (2 ^ 23)

    ' Warnings
    ' Start at 2^30
    WARN_MISSING_SCALE_CLAUSE = WARN Or (2 ^ 30)
    WARN_MISSING_PERMISSIONS_CLAUSE = WARN Or (2 ^ 31)
    WARN_TWO_CHAR_FREE_TEXT = WARN Or (2 ^ 32)  '_MAYBE_ERRONEOUS_PERMISSION_CLAUSE

    ' Info
    ' Start at 2^40
    INFO_FREE_TEXT_PRESENT = INFO Or (2 ^ 40)

End Enum

Public Enum dnListType As Integer
    'Constants relating to the status of an IDataListConnection object
    UNKNOWN = (2 ^ 1)
    GDB = (2 ^ 2)
    DIR = (2 ^ 3)
    MXD = (2 ^ 4)
    MIXED_FILES = (2 ^ 5)
End Enum

Public Enum dnClauseLookupType As Integer
    'Constants relating to the type of DataNameClauseLookup used
    MDB = (2 ^ 1)
    ESRI_GDB = (2 ^ 2)
End Enum

Public Module DataNamingConstants

    'Clauses in a data name
    Public Const CLAUSE_GEOEXTENT As String = "geoextent"
    Public Const CLAUSE_DATACATEGORY As String = "datacategory"
    Public Const CLAUSE_DATATHEME As String = "datatheme"
    Public Const CLAUSE_DATATYPE As String = "datatype"
    Public Const CLAUSE_SCALE As String = "scale"
    Public Const CLAUSE_SOURCE As String = "source"
    Public Const CLAUSE_PERMISSIONS As String = "permission"
    Public Const CLAUSE_FREETEXT As String = "freetext"

    

    Private dataNameStrMessages As Hashtable

    

    'Constants relating to the names of table which store all of the data name clauses
    Public Const TABLENAME_GEOEXTENT As String = "datanaming_clause_geoextent"
    Public Const TABLENAME_DATA_CAT As String = "datanaming_clause_data_categories"
    Public Const TABLENAME_DATA_THEME As String = "datanaming_clause_data_theme"
    Public Const TABLENAME_DATA_TYPE As String = "datanaming_clause_data_type"
    Public Const TABLENAME_SCALE As String = "datanaming_clause_scale"
    Public Const TABLENAME_SOURCE As String = "datanaming_clause_source"
    Public Const TABLENAME_PERMISSION As String = "datanaming_clause_permission"

    Public Const PRI_KEY_COL_NAME As String = "clause"

    Private allTableNames() As String = New String() {CType(TABLENAME_GEOEXTENT, String), _
                                                       CType(TABLENAME_DATA_CAT, String), _
                                                       CType(TABLENAME_DATA_THEME, String), _
                                                       CType(TABLENAME_DATA_TYPE, String), _
                                                       CType(TABLENAME_SCALE, String), _
                                                       CType(TABLENAME_SOURCE, String), _
                                                       CType(TABLENAME_PERMISSION, String)}

    'todo LOW: move these three values to an ini file or the registary etc..
    Public Const MA_DIR_STRUCT_DATA_DIR As String = "2_Active_Data"
    Public Const MA_DIR_STRUCT_MXD_DIR As String = "33_MXD_Maps"
    Public Const MA_DIR_STRUCT_PATH_FROM_MXD_TO_DATA_DIR As String = "..\..\" & MA_DIR_STRUCT_DATA_DIR

    'todo LOW: move these three values to an ini file or the registary etc..
    Public Const MDB_DATACLAUSE_FILE_PREFIX As String = "data_naming_conventions_v"

    'todo LOW: move these three values to an ini file or the registary etc..
    Public Const DATATYPE_CLAUSE_POINT As String = "pt"
    Public Const DATATYPE_CLAUSE_LINE As String = "ln"
    Public Const DATATYPE_CLAUSE_POLYGON As String = "py"
    Public Const DATATYPE_CLAUSE_RASTER As String = "ras"
    Public Const DATATYPE_CLAUSE_RASTER_CATALOG As String = "rca"
    Public Const DATATYPE_CLAUSE_TABLE As String = "tab"
    Public Const DATATYPE_CLAUSE_TIN As String = "tin"
    Public Const DATATYPE_CLAUSE_WMS As String = "wms"
    Public Const DATATYPE_CLAUSE_WFS As String = "wfs"
    'todo LOW: datatype unknown
    Public Const DATATYPE_CLAUSE_UNKNOWN As String = "unkwn"

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
        dataNameStrMessages.Add(dnNameStatus.INVALID_GEOEXTENT, "INVALID NAME: GeoExtent Clause not in list of recognised clauses")
        dataNameStrMessages.Add(dnNameStatus.INVALID_DATACATEGORY, "INVALID NAME: Data Category Clause not in list of recognised clauses")
        dataNameStrMessages.Add(dnNameStatus.INVALID_DATATHEME, "INVALID NAME: Data Theme not regonised, or not valid for Data Category")
        dataNameStrMessages.Add(dnNameStatus.INVALID_DATATYPE, "INVALID NAME: Data Type Clause not in list of recognised clauses")
        dataNameStrMessages.Add(dnNameStatus.INCORRECT_DATATYPE, "ERROR: Data Type Clause does not match underlying data type")
        dataNameStrMessages.Add(dnNameStatus.INVALID_SCALE, "INVALID NAME: Data Scale Clause Clause not in list of recognised clauses")
        dataNameStrMessages.Add(dnNameStatus.INVALID_SOURCE, "INVALID NAME: Source Clause not in list of recognised clauses")
        dataNameStrMessages.Add(dnNameStatus.INVALID_PERMISSIONS, "INVALID NAME: Permissions Clause not in list of recognised clauses")
        dataNameStrMessages.Add(dnNameStatus.SYNTAX_ERROR_OTHER, "SYNTAX ERROR: General Error phasing data name")
        dataNameStrMessages.Add(dnNameStatus.SYNTAX_ERROR_CONTAINS_HYPHENS, "SYNTAX ERROR: Data Name contains hyphens")
        dataNameStrMessages.Add(dnNameStatus.SYNTAX_ERROR_TOO_FEW_CLAUSES, "SYNTAX ERROR: Too few clauses in Data Name")
        dataNameStrMessages.Add(dnNameStatus.SYNTAX_ERROR_DOUBLE_UNDERSCORE, "SYNTAX ERROR: Two consequtive undersource present")
        dataNameStrMessages.Add(dnNameStatus.WARN_MISSING_SCALE_CLAUSE, "WARNING: Optional Scale Clause not present")
        dataNameStrMessages.Add(dnNameStatus.WARN_MISSING_PERMISSIONS_CLAUSE, "WARNING: Optional Permissions Clause not present")
        dataNameStrMessages.Add(dnNameStatus.WARN_TWO_CHAR_FREE_TEXT, "WARNING: Two charater long free text, could be misformed permissions clause")

        dataNameStrMessages.Add(dnNameStatus.INFO_FREE_TEXT_PRESENT, "INFO: Free text clause is present")
    End Sub

    Private Sub initialiseDataColumnCollections()
        'todo LOW: This would problably be better implenmented as reading in an xml file for  something.
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


