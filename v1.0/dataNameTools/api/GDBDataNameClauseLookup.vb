Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.DataSourcesGDB
Imports System.IO

''' <summary>
''' Provides a specfic implenmentation of the IDataNameClauseLookup, based on storing the
''' Data Name Clause Lookup Tables in any form of ESRI GDB (file, personal, SDE etc)
''' </summary>
''' <remarks>
''' Provides a specfic implenmentation of the IDataNameClauseLookup, based on storing the
''' Data Name Clause Lookup Tables in any form of ESRI GDB (file, personal, SDE etc)
'''
''' There is no public constructor for this class. New instances should be generated using 
''' the XXXX factory class.
''' </remarks>
Public Class GDBDataNameClauseLookup
    Inherits AbstractDataNameClauseLookup

    'ESRI.ArcGIS.Utility.Converter.ToDataSet
    Private m_wkspDataNameLookup As ESRI.ArcGIS.Geodatabase.IWorkspace = Nothing

    ''' <summary>
    ''' The constuctor. This should only be call from within the relevant 
    ''' factory class.
    ''' </summary>
    ''' <param name="dnlw"></param>
    ''' <remarks>
    ''' The constuctor. This should only be call from within the relevant 
    ''' factory class.
    ''' </remarks>
    Protected Friend Sub New(ByRef dnlw As ESRI.ArcGIS.Geodatabase.IWorkspace)
        m_wkspDataNameLookup = dnlw
        initialiseAllTables()
    End Sub


    ''' <summary>
    ''' The constuctor. This should only be call from within the relevant 
    ''' factory class.
    ''' </summary>
    ''' <param name="strPathName">A string presenting the path </param>
    ''' <remarks>
    ''' The constuctor. This should only be call from within the relevant 
    ''' factory class.
    ''' </remarks>
    Protected Friend Sub New(ByVal strPathName As String)
        'ESRI.ArcGIS.Geodatabase.IWorkspace()
        m_wkspDataNameLookup = getESRIWorkspaceFromFile(strPathName)
        initialiseAllTables()
    End Sub


    'todo HIGH: rewrite this using the ESRI OLEDB Datasource driver.
    Protected Overrides Function openTable(ByVal strTableName As String) As DataTable
        Dim enumDS As IEnumDataset
        Dim ds As IDataset
        Dim obj As Object
        Dim esriTbl As ITable
        Dim qryFtr As IQueryFilter2
        Dim rs As IRecordSet
        Dim dtb As DataTable

        enumDS = m_wkspDataNameLookup.Datasets(esriDatasetType.esriDTTable)

        obj = Nothing
        esriTbl = Nothing

        ds = enumDS.Next

        While Not ds Is Nothing
            'System.Console.WriteLine("Name:" & myIDataSet.Name & "    searchName: " & tableName)
            If ds.Name = strTableName Then
                'System.Console.WriteLine("Found searchName: " & tableName)
                obj = ds
            End If
            ds = enumDS.Next()
        End While

        If obj Is Nothing Then
            'The table was not found
            Throw New ArgumentException("tablename: " & strTableName & " not found in DB: " & getDetails())
        Else
            'System.Console.WriteLine("Found myObject: ")
            If TypeOf obj Is ITable Then
                esriTbl = DirectCast(obj, ITable)
                'System.Console.WriteLine("Found myESRITable: ")
            End If

            rs = New RecordSet
            Dim rsi As IRecordSetInit
            rsi = New RecordSet

            qryFtr = New QueryFilter()
            qryFtr.WhereClause = String.Empty

            'todo LOW: check whether or not passing a Null to IRecordSetInit.SetSourceTable
            'rsi.SetSourceTable(myESRITable, DBNull.Value)
            rsi.SetSourceTable(esriTbl, qryFtr)

            rs = DirectCast(rsi, IRecordSet)
            Dim myDTC As DataTableCollection
            myDTC = ESRI.ArcGIS.Utility.Converter.ToDataSet(rs).Tables()
            'For i As Short = 0 To (myDotNETDataTableCol.Count - 1) Step 1
            '    System.Console.WriteLine("table Name " & myDotNETDataTableCol.Item(i).ToString())

            '    myDotNETDataTableCol.Item(i).WriteXml(System.Console.OpenStandardOutput)

            'Next
            If myDTC.Count > 0 Then
                dtb = myDTC.Item(0)
            Else
                'The table was not found
                Throw New ArgumentException("tablename: " & strTableName & " not found in DB: " & getDetails())
            End If
        End If

        Return dtb
    End Function


    ''' <summary>
    ''' Returns the Path name used to connect to the GDB.
    ''' </summary>
    ''' <returns>The Path name used to connect to the GDB.</returns>
    ''' <remarks>
    ''' Returns the Path name used to connect to the GDB.
    ''' </remarks>
    Public Overrides Function getDetails() As String
        Return m_wkspDataNameLookup.PathName
    End Function

    ''' <summary>
    ''' Returns the Path name used to connect to the GDB.
    ''' </summary>
    ''' <returns>The Path name used to connect to the GDB.</returns>
    ''' <remarks>
    ''' Returns the Path name used to connect to the GDB.
    ''' </remarks>
    Public Overrides Function getPath() As fileinfo
        Return New FileInfo(m_wkspDataNameLookup.PathName)
    End Function

End Class
