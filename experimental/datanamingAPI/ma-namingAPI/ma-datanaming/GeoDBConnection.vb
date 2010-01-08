Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.DataSourcesGDB

Public Class GeoDBConnection
    Inherits GeoDataSourceConnection

    Private dataNameLookupWorkspace As ESRI.ArcGIS.Geodatabase.IWorkspace

    Sub New()
        Connect()
    End Sub


    Public Overrides Sub Connect()
        MsgBox("GeoDBConnection.Connect() not properly implenmented yet!")

        'Dim propertySet As New ESRI.ArcGIS.esriSystem.PropertySetClass

        'propertySet.SetProperty("SERVER", "cuillin")
        'propertySet.SetProperty("INSTANCE", "esri_sde")
        'propertySet.SetProperty("USER", "scott")
        'propertySet.SetProperty("PASSWORD", "tiger")
        'propertySet.SetProperty("VERSION", "SDE.DEFAULT")
        'propertySet.SetProperty("AUTHENTICATION_MODE", "DBMS")

        Dim workspaceFactory As IWorkspaceFactory
        workspaceFactory = New AccessWorkspaceFactoryClass

        '// Cast for IName.
        'ESRI.ArcGIS.esriSystem.IName name = (IName)workspaceName;

        '//Open a reference to the file geodatabase workspace through the Name object.
        'Dim fGDB_Wor As ESRI.ArcGIS.Geodatabase.IWorkspace

        '//Open another file geodatabase workspace.
        dataNameLookupWorkspace = workspaceFactory.OpenFromFile("D:\\MapAction\\bronze\\data_model\\ProposedNamingConvention\\Propossed-data-naming-conventions_v0.8.mdb", 0)

    End Sub

    Public Overrides Sub Disconnect()
        dataNameLookupWorkspace = Nothing
    End Sub

    Public Overrides Function GetDetails() As String
        GetDetails = "GeoDBConnection.GetDetails() not yet implenmented"
    End Function

    Public Overrides Function GetdataSetList() As List(Of String)
        Dim pDSName As IDatasetName
        Dim pEnumDSName As IEnumDatasetName
        Dim namesList As New List(Of String)

        pEnumDSName = dataNameLookupWorkspace.DatasetNames(esriDatasetType.esriDTAny)
        pDSName = pEnumDSName.Next

        While Not pDSName Is Nothing
            'namesArry.SetValue(pEnumDSName.Name, namesArry.GetLength(1) + 1)
            namesList.Add(pDSName.Name)
            pDSName = pEnumDSName.Next
        End While

        GetdataSetList = namesList
    End Function

    Public Overrides Function GetTable(ByVal tableName As String)

        Dim pEnumDSName As IEnumDataset
        Dim myIDataSet As IDataset
        Dim myObject As Object
        Dim myTable As ITable

        pEnumDSName = dataNameLookupWorkspace.Datasets(esriDatasetType.esriDTTable)

        myObject = Nothing
        myTable = Nothing

        myIDataSet = pEnumDSName.Next

        While Not myIDataSet Is Nothing
            If myIDataSet.BrowseName = tableName Then
                myObject = myIDataSet.FullName.Open()
            End If
            myIDataSet = pEnumDSName.Next
        End While

        If TypeOf myObject Is ITable Then
            myTable = DirectCast(myObject, ITable)
        End If

        myTable.


        While Not pDSName Is Nothing
            'namesArry.SetValue(pEnumDSName.Name, namesArry.GetLength(1) + 1)
            namesList.Add(pDSName.Name)
            pDSName = pEnumDSName.Next
        End While



        '        [Visual Basic 6.0]
        '        Dim pWorkspace As IWorkspace
        '        Dim pFact As IWorkspaceFactory

        '        ' This example uses an SDE connection. This code works the
        '        ' same for any open IWorkspace.

        '        Dim pPropset As IPropertySet
        '        pPropset = New PropertySet
        '        With pPropset
        '            .SetProperty("Server", "fred")
        '            .SetProperty("Instance", "5203")
        '            .SetProperty("Database", "sdedata")
        '            .SetProperty("user", "test")
        '            .SetProperty("password", "test")
        '            .SetProperty("version", "sde.DEFAULT")
        '        End With
        '        pFact = New SdeWorkspaceFactory
        '        pWorkspace = pFact.Open(pPropset, Me.hWnd)
        '        Dim pFeatureWorkspace As IFeatureWorkspace
        '        pFeatureWorkspace = pWorkspace

        '        Dim pTable As ITable
        '        pTable = pFeatureWorkspace.OpenTable("Pavement")

        '        Dim iOIDList() As Long
        '        Dim iOIDListCount As Long

        '        iOIDListCount = 5

        '        ReDim iOIDList(iOIDListCount)
        '        iOIDList(0) = 1
        '        iOIDList(1) = 2
        '        iOIDList(2) = 3
        '        iOIDList(3) = 4
        '        iOIDList(4) = 50

        '        Dim pCursor As ICursor
        '        pCursor = pTable.GetRows(iOIDList, True)
        '        Dim pRow As IRow
        '        pRow = pCursor.NextRow
        '        While Not pRow Is Nothing
        '            Debug.Print(pRow.Value(2))
        '            pRow = pCursor.NextRow
        '        End While


        '[C#]

        '    //ITable GetRows Example

        '    //e.g., nameOfTable = "Owners"
        '    //on ArcSDE use ISqlSyntax::QualifyTableName for fully qualified table names.
        '    public void ITable_GetRows_Example(IWorkspace workspace, string nameOfTable)
        '    {
        '        IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspace;
        '        ITable table = featureWorkspace.OpenTable(nameOfTable);

        '        System.Collections.Generic.List<int> constructOIDList = new System.Collections.Generic.List<int>();
        '        constructOIDList.Add(1);
        '        constructOIDList.Add(2);
        '        constructOIDList.Add(3);
        '        constructOIDList.Add(5);
        '        constructOIDList.Add(8);
        '        int[] oidList = constructOIDList.ToArray();

        '        ICursor cursor = table.GetRows(oidList,false);
        '        IRow row = cursor.NextRow();
        '        while (row != null)
        '        {
        '            Console.WriteLine(row.get_Value(row.Fields.FindField("Name")));
        '            row = cursor.NextRow();
        '        }
        '    }



        GetTable = myTable

    End Function

End Class
