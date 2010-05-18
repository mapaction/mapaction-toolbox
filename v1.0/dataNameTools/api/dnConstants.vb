' This file contains serveral Enumerations and a Module which collectively define
' all of the constant values used in teh datanaming API.

'todo LOW: Substainial parts of this module should be converted to read values from an .ini file or simular

''' <summary>
''' BitFlags relating to the status of a particular Data Name. The status of a name
''' is retrived by using IDataName.checkNameStatus() or one of the convenence wrapper
''' methods.
''' </summary>
''' <remarks>
''' BitFlags relating to the status of a particular Data Name. The status of a name
''' is retrived by using IDataName.checkNameStatus() or one of the convenence wrapper
''' methods.
''' 
''' The status flags are arranged in four categories:
'''     INVALID
'''     SYNTAX_ERROR 
'''     WARN 
'''     INFO
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
<FlagsAttribute()> _
Public Enum dnNameStatus As Long

    ' Overall Status
    ' Start at 2^1
    UNKNOWN_STATUS = 0
    INVALID = 2
    SYNTAX_ERROR = 4
    WARN = 8
    INFO = 16

    ' Invalid....
    ' Start at 2^10
    INVALID_GEOEXTENT = INVALID Or CLng(2 ^ 10)
    INVALID_DATACATEGORY = INVALID Or CLng(2 ^ 11)
    INVALID_DATATHEME = INVALID Or CLng(2 ^ 12)
    INVALID_DATATYPE = INVALID Or CLng(2 ^ 13)
    INCORRECT_DATATYPE = INVALID Or CLng(2 ^ 14)
    INVALID_SCALE = INVALID Or CLng(2 ^ 15)
    INVALID_SOURCE = INVALID Or CLng(2 ^ 16)
    INVALID_PERMISSIONS = INVALID Or CLng(2 ^ 17)

    ' Syntax Errors
    ' Start at 2^20
    SYNTAX_ERROR_OTHER = SYNTAX_ERROR Or CLng(2 ^ 20)
    SYNTAX_ERROR_CONTAINS_HYPHENS = SYNTAX_ERROR Or CLng(2 ^ 21)
    SYNTAX_ERROR_TOO_FEW_CLAUSES = SYNTAX_ERROR Or CLng(2 ^ 22)
    SYNTAX_ERROR_DOUBLE_UNDERSCORE = SYNTAX_ERROR Or CLng(2 ^ 23)

    ' Warnings
    ' Start at 2^30
    WARN_MISSING_SCALE_CLAUSE = WARN Or CLng(2 ^ 30)
    WARN_MISSING_PERMISSIONS_CLAUSE = WARN Or CLng(2 ^ 31)
    WARN_TWO_CHAR_FREE_TEXT = WARN Or CLng(2 ^ 32)  '_MAYBE_ERRONEOUS_PERMISSION_CLAUSE

    ' Info
    ' Start at 2^40
    INFO_FREE_TEXT_PRESENT = INFO Or CLng(2 ^ 40)

End Enum

''' <summary>
''' Describes the physical type of an IDataListConnection object.
''' </summary>
''' <remarks>
''' Describes the physical type of an IDataListConnection object.
''' </remarks>
Public Enum dnListType As Integer
    'Constants relating to the status of an IDataListConnection object
    UNKNOWN = CInt(2 ^ 1)
    GDB = CInt(2 ^ 2)
    DIR = CInt(2 ^ 3)
    MXD = CInt(2 ^ 4)
    MIXED_FILES = CInt(2 ^ 5)
End Enum

''' <summary>
''' Describes the physical type of an IDataNameClauseLookup object.
''' </summary>
''' <remarks>
''' Describes the physical type of an IDataNameClauseLookup object.
''' </remarks>
Public Enum dnClauseLookupType As Integer
    'Constants relating to the type of IDataNameClauseLookup used
    MDB = CInt(2 ^ 1)
    ESRI_GDB = CInt(2 ^ 2)
End Enum


Public Module DataNamingConstants


    'Names of the different clauses in a data name
    Public Const CLAUSE_GEOEXTENT As String = "geoextent"
    Public Const CLAUSE_DATACATEGORY As String = "datacategory"
    Public Const CLAUSE_DATATHEME As String = "datatheme"
    Public Const CLAUSE_DATATYPE As String = "datatype"
    Public Const CLAUSE_SCALE As String = "scale"
    Public Const CLAUSE_SOURCE As String = "source"
    Public Const CLAUSE_PERMISSIONS As String = "permission"
    Public Const CLAUSE_FREETEXT As String = "freetext"

    'Constants relating to the names of table which store all of the data name clauses
    Public Const TABLENAME_GEOEXTENT As String = "datanaming_clause_geoextent"
    Public Const TABLENAME_DATA_CAT As String = "datanaming_clause_data_categories"
    Public Const TABLENAME_DATA_THEME As String = "datanaming_clause_data_theme"
    Public Const TABLENAME_DATA_TYPE As String = "datanaming_clause_data_type"
    Public Const TABLENAME_SCALE As String = "datanaming_clause_scale"
    Public Const TABLENAME_SOURCE As String = "datanaming_clause_source"
    Public Const TABLENAME_PERMISSION As String = "datanaming_clause_permission"

    'Common name for Primary Key for all data name clause tables
    Public Const PRI_KEY_COL_NAME As String = "clause"

    'An array of all of the data name clause tables names
    Private m_strAryTableNames() As String = _
                New String() {CStr(TABLENAME_GEOEXTENT), _
                              CStr(TABLENAME_DATA_CAT), _
                              CStr(TABLENAME_DATA_THEME), _
                              CStr(TABLENAME_DATA_TYPE), _
                              CStr(TABLENAME_SCALE), _
                              CStr(TABLENAME_SOURCE), _
                              CStr(TABLENAME_PERMISSION)}

    'A collection of "user readable" messages describing the dataname status
    Private m_htbDNStatusStrMessages As Hashtable

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

    ''' <summary>
    ''' An array of all of the data name clause tables names.
    ''' </summary>
    ''' <value></value>
    ''' <returns>An array of all of the data name clause tables names.
    ''' </returns>
    ''' <remarks>An array of all of the data name clause tables names.
    ''' </remarks>
    Public ReadOnly Property g_strAryClauseTableNames() As String()
        Get
            Return m_strAryTableNames
        End Get
    End Property

    ''' <summary>
    ''' A collection of "user readable" messages describing the dataname status.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>
    ''' A collection of "user readable" messages describing the dataname status.
    ''' 
    ''' The Hashtable uses the dnNameStatus enumeration as the keys.
    ''' </remarks>
    Public ReadOnly Property g_htbDNStatusStrMessages() As Hashtable
        Get
            Return m_htbDNStatusStrMessages
        End Get
    End Property

    Private m_htbAllDataColumnGroups As New Hashtable

    ''' <summary>
    ''' A structured collection of the DataColumns which describe all of the dataname 
    ''' clause tables
    ''' </summary>
    ''' <value>A Hashtable. The Hashtable has entry for each dataname clause table, with
    ''' the table name as the key. In each case the value is an ArrayList, of DataColumn 
    ''' objects describing that table.
    ''' </value>
    ''' <returns></returns>
    ''' <remarks>
    ''' A structured collection of the DataColumns which describe all of the dataname 
    ''' clause tables.
    ''' 
    ''' The returned values is a Hashtable. The Hashtable has entry for each dataname 
    ''' clause table, with the table name as the key. In each case the value is an 
    ''' ArrayList, of DataColumn objects describing that table.
    ''' </remarks>
    Public ReadOnly Property g_htbAllDataNameColumns() As Hashtable
        Get
            Return m_htbAllDataColumnGroups
        End Get
    End Property

    Sub New()
        initialiseDataColumnCollections()
        initialiseDNStatusStrMessages()
    End Sub

    Private Sub initialiseDNStatusStrMessages()
        m_htbDNStatusStrMessages = New Hashtable(20)
        m_htbDNStatusStrMessages.Add(dnNameStatus.INVALID_GEOEXTENT, "INVALID NAME: GeoExtent Clause not in list of recognised clauses")
        m_htbDNStatusStrMessages.Add(dnNameStatus.INVALID_DATACATEGORY, "INVALID NAME: Data Category Clause not in list of recognised clauses")
        m_htbDNStatusStrMessages.Add(dnNameStatus.INVALID_DATATHEME, "INVALID NAME: Data Theme not regonised, or not valid for Data Category")
        m_htbDNStatusStrMessages.Add(dnNameStatus.INVALID_DATATYPE, "INVALID NAME: Data Type Clause not in list of recognised clauses")
        m_htbDNStatusStrMessages.Add(dnNameStatus.INCORRECT_DATATYPE, "ERROR: Data Type Clause does not match underlying data type")
        m_htbDNStatusStrMessages.Add(dnNameStatus.INVALID_SCALE, "INVALID NAME: Data Scale Clause Clause not in list of recognised clauses")
        m_htbDNStatusStrMessages.Add(dnNameStatus.INVALID_SOURCE, "INVALID NAME: Source Clause not in list of recognised clauses")
        m_htbDNStatusStrMessages.Add(dnNameStatus.INVALID_PERMISSIONS, "INVALID NAME: Permissions Clause not in list of recognised clauses")
        m_htbDNStatusStrMessages.Add(dnNameStatus.SYNTAX_ERROR_OTHER, "SYNTAX ERROR: General Error phasing data name")
        m_htbDNStatusStrMessages.Add(dnNameStatus.SYNTAX_ERROR_CONTAINS_HYPHENS, "SYNTAX ERROR: Data Name contains hyphens")
        m_htbDNStatusStrMessages.Add(dnNameStatus.SYNTAX_ERROR_TOO_FEW_CLAUSES, "SYNTAX ERROR: Too few clauses in Data Name")
        m_htbDNStatusStrMessages.Add(dnNameStatus.SYNTAX_ERROR_DOUBLE_UNDERSCORE, "SYNTAX ERROR: Two consequtive undersource present")
        m_htbDNStatusStrMessages.Add(dnNameStatus.WARN_MISSING_SCALE_CLAUSE, "WARNING: Optional Scale Clause not present")
        m_htbDNStatusStrMessages.Add(dnNameStatus.WARN_MISSING_PERMISSIONS_CLAUSE, "WARNING: Optional Permissions Clause not present")
        m_htbDNStatusStrMessages.Add(dnNameStatus.WARN_TWO_CHAR_FREE_TEXT, "WARNING: Two charater long free text, could be misformed permissions clause")
        m_htbDNStatusStrMessages.Add(dnNameStatus.INFO_FREE_TEXT_PRESENT, "INFO: Free text clause is present")
    End Sub

    ''' <summary>
    ''' Creates a reference set of DataColumn objects for comparing any "live" tables against.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub initialiseDataColumnCollections()
        'todo LOW: This would problably be better implenmented as reading in an xml file for  something.
        'todo LOW: Addtionally a (strongly typed) List(Of DataColumns) would problably be better than 
        'an ArrayList for this purpose.
        Dim arylDataCols As ArrayList
        Dim dclCurrent As DataColumn

        For Each strTableName In m_strAryTableNames
            arylDataCols = New ArrayList

            'Add same to columns to all tables
            dclCurrent = New DataColumn()
            With dclCurrent
                .ColumnName = "clause"
                .DataType = System.Type.GetType("System.String")
                .MaxLength = 20
                .Unique = True
                .AutoIncrement = False
                .Caption = "clause"
                .ReadOnly = False
            End With
            arylDataCols.Add(dclCurrent)

            dclCurrent = New DataColumn()
            With dclCurrent
                .ColumnName = "Description"
                .DataType = System.Type.GetType("System.String")
                .MaxLength = 255
                .Unique = True
                .AutoIncrement = False
                .Caption = "Description"
                .ReadOnly = False
            End With
            arylDataCols.Add(dclCurrent)

            'now add the columns specific to particular tables
            Select Case strTableName
                Case TABLENAME_GEOEXTENT
                    'Country_or_Continent
                    dclCurrent = New DataColumn()
                    With dclCurrent
                        .ColumnName = "Geography_type"
                        .DataType = System.Type.GetType("System.String")
                        .MaxLength = 255
                        .Unique = False
                        .AutoIncrement = False
                        .Caption = "Geography_type"
                        .ReadOnly = False
                    End With
                    arylDataCols.Add(dclCurrent)

                Case TABLENAME_DATA_CAT
                    'Base_or_Situational
                    dclCurrent = New DataColumn()
                    With dclCurrent
                        .ColumnName = "Base_or_Situational"
                        .DataType = System.Type.GetType("System.String")
                        .MaxLength = 11
                        .Unique = False
                        .AutoIncrement = False
                        .Caption = "Base_or_Situational"
                        .ReadOnly = False
                    End With
                    arylDataCols.Add(dclCurrent)

                Case TABLENAME_DATA_THEME
                    'Data_Category
                    dclCurrent = New DataColumn()
                    With dclCurrent
                        .ColumnName = "Data_Category"
                        .DataType = System.Type.GetType("System.String")
                        .MaxLength = 20
                        .Unique = False
                        .AutoIncrement = False
                        .Caption = "Data_Category"
                        .ReadOnly = False
                    End With
                    arylDataCols.Add(dclCurrent)

                Case TABLENAME_DATA_TYPE
                    'None

                Case TABLENAME_SCALE
                    'Numerical_range, 50
                    dclCurrent = New DataColumn()
                    With dclCurrent
                        .ColumnName = "Numerical_range"
                        .DataType = System.Type.GetType("System.String")
                        .MaxLength = 50
                        .Unique = False
                        .AutoIncrement = False
                        .Caption = "Numerical_range"
                        .ReadOnly = False
                    End With
                    arylDataCols.Add(dclCurrent)

                Case TABLENAME_SOURCE
                    'Organisation_Name_if_different 255
                    dclCurrent = New DataColumn()
                    With dclCurrent
                        .ColumnName = "Organisation_Name_if_different"
                        .DataType = System.Type.GetType("System.String")
                        .MaxLength = 255
                        .Unique = False
                        .AutoIncrement = False
                        .Caption = "Organisation_Name_if_different"
                        .ReadOnly = False
                    End With
                    arylDataCols.Add(dclCurrent)

                    'Generic_or_Specific 8
                    dclCurrent = New DataColumn()
                    With dclCurrent
                        .ColumnName = "Generic_or_Specific"
                        .DataType = System.Type.GetType("System.String")
                        .MaxLength = 8
                        .Unique = False
                        .AutoIncrement = False
                        .Caption = "Generic_or_Specific"
                        .ReadOnly = False
                    End With
                    arylDataCols.Add(dclCurrent)

                    'Org_or_Dataset 12
                    dclCurrent = New DataColumn()
                    With dclCurrent
                        .ColumnName = "Org_or_Dataset"
                        .DataType = System.Type.GetType("System.String")
                        .MaxLength = 12
                        .Unique = False
                        .AutoIncrement = False
                        .Caption = "Org_or_Dataset"
                        .ReadOnly = False
                    End With
                    arylDataCols.Add(dclCurrent)

                    'Website 255
                    dclCurrent = New DataColumn()
                    With dclCurrent
                        .ColumnName = "Website"
                        .DataType = System.Type.GetType("System.String")
                        .MaxLength = 255
                        .Unique = False
                        .AutoIncrement = False
                        .Caption = "Website"
                        .ReadOnly = False
                    End With
                    arylDataCols.Add(dclCurrent)

                    'Marginalia_Notes 4098
                    dclCurrent = New DataColumn()
                    With dclCurrent
                        .ColumnName = "Marginalia_Notes"
                        .DataType = System.Type.GetType("System.String")
                        .MaxLength = 4098
                        .Unique = False
                        .AutoIncrement = False
                        .Caption = "Marginalia_Notes"
                        .ReadOnly = False
                    End With
                    arylDataCols.Add(dclCurrent)

                Case TABLENAME_PERMISSION
                    'Possible_example_datasets 255
                    dclCurrent = New DataColumn()
                    With dclCurrent
                        .ColumnName = "Possible_example_datasets"
                        .DataType = System.Type.GetType("System.String")
                        .MaxLength = 255
                        .Unique = False
                        .AutoIncrement = False
                        .Caption = "Possible_example_datasets"
                        .ReadOnly = False
                    End With
                    arylDataCols.Add(dclCurrent)

            End Select

            m_htbAllDataColumnGroups.Add(strTableName, arylDataCols)
        Next
    End Sub

End Module


