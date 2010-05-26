Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.DataSourcesGDB

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

    Protected Overrides Function openTable(ByVal tableName As String) As DataTable
        Dim pEnumDSName As IEnumDataset
        Dim myIDataSet As IDataset
        Dim myObject As Object
        Dim myESRITable As ITable
        Dim myQueryFilter As IQueryFilter2
        Dim myRecordSet As IRecordSet
        Dim myDotNETTable As DataTable

        pEnumDSName = m_wkspDataNameLookup.Datasets(esriDatasetType.esriDTTable)

        myObject = Nothing
        myESRITable = Nothing

        myIDataSet = pEnumDSName.Next

        While Not myIDataSet Is Nothing
            'System.Console.WriteLine("Name:" & myIDataSet.Name & "    searchName: " & tableName)
            If myIDataSet.Name = tableName Then
                'System.Console.WriteLine("Found searchName: " & tableName)
                myObject = myIDataSet
            End If
            myIDataSet = pEnumDSName.Next()
        End While

        If myObject Is Nothing Then
            'The table was not found
            Throw New ArgumentException("tablename: " & tableName & " not found in DB: " & getDetails())
        Else
            'System.Console.WriteLine("Found myObject: ")
            If TypeOf myObject Is ITable Then
                myESRITable = DirectCast(myObject, ITable)
                'System.Console.WriteLine("Found myESRITable: ")
            End If

            myRecordSet = New RecordSet
            Dim rsi As IRecordSetInit
            rsi = New RecordSet

            myQueryFilter = New QueryFilter()
            myQueryFilter.WhereClause = ""

            'todo LOW: check whether or not passing a Null to IRecordSetInit.SetSourceTable
            'rsi.SetSourceTable(myESRITable, DBNull.Value)
            rsi.SetSourceTable(myESRITable, myQueryFilter)

            myRecordSet = DirectCast(rsi, IRecordSet)
            Dim myDTC As DataTableCollection
            myDTC = ESRI.ArcGIS.Utility.Converter.ToDataSet(myRecordSet).Tables()
            'For i As Short = 0 To (myDotNETDataTableCol.Count - 1) Step 1
            '    System.Console.WriteLine("table Name " & myDotNETDataTableCol.Item(i).ToString())

            '    myDotNETDataTableCol.Item(i).WriteXml(System.Console.OpenStandardOutput)

            'Next
            If myDTC.Count > 0 Then
                myDotNETTable = myDTC.Item(0)
            Else
                'The table was not found
                Throw New ArgumentException("tablename: " & tableName & " not found in DB: " & getDetails())
            End If
        End If

        Return myDotNETTable
    End Function

    Public Overrides Function isWriteable() As Boolean
        Return False
    End Function

    Public Overrides Function getDetails() As String
        Return m_wkspDataNameLookup.PathName
    End Function

End Class
