Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.DataSourcesGDB

Public Class GeoDBDataNameClauseLookup
    Inherits AbstractDataNameClauseLookup

    'ESRI.ArcGIS.Utility.Converter.ToDataSet
    Private dataNameLookupWorkspace As ESRI.ArcGIS.Geodatabase.IWorkspace = Nothing

    Protected Friend Sub New(ByRef dnlw As ESRI.ArcGIS.Geodatabase.IWorkspace)
        dataNameLookupWorkspace = dnlw
        initialiseAllTables()
    End Sub

    Protected Friend Sub New(ByVal pathName As String)
        'ESRI.ArcGIS.Geodatabase.IWorkspace()
        dataNameLookupWorkspace = getESRIWorkspaceFromFile(pathName)
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

        pEnumDSName = dataNameLookupWorkspace.Datasets(esriDatasetType.esriDTTable)

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
        isWriteable = False
    End Function

    Public Overrides Function getDetails() As String
        Return dataNameLookupWorkspace.PathName
    End Function

End Class
