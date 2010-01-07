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
        Dim namesArry As New List(Of String)

        pEnumDSName = dataNameLookupWorkspace.DatasetNames(esriDatasetType.esriDTAny)
        pDSName = pEnumDSName.Next

        While Not pDSName Is Nothing
            'namesArry.SetValue(pEnumDSName.Name, namesArry.GetLength(1) + 1)
            namesArry.Add(pDSName.Name)
            pDSName = pEnumDSName.Next
        End While

        GetdataSetList = namesArry
    End Function

    Public Overrides Function GetTable(ByVal tableName As String)

        Dim pEnumDSName As IEnumDataset
        Dim myIDataSet As IDataset
        Dim table As Object

        pEnumDSName = dataNameLookupWorkspace.Datasets(esriDatasetType.esriDTTable)

        table = Nothing

        myIDataSet = pEnumDSName.Next


        While Not myIDataSet Is Nothing
            If myIDataSet.BrowseName = tableName Then
                table = myIDataSet.FullName.Open()
            End If
            myIDataSet = pEnumDSName.Next
        End While

        System.Console.WriteLine(table.GetType())

        GetTable = table

    End Function

End Class
