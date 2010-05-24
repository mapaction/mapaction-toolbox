Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry

Public Class DataNameESRIFeatureClass
    Inherits AbstractDataName

    Private myDataSet As IDataset

    Protected Friend Sub New(ByVal ds As IDataset, ByRef theDNCL As IDataNameClauseLookup, ByVal allowReNames As Boolean)
        MyBase.new(removePrefixFromBrowseName(ds.BrowseName), theDNCL, allowReNames)
        myDataSet = ds
        System.Console.WriteLine("Testing ds.BrowseName: " & ds.BrowseName)
    End Sub

    Protected Friend Overrides Function renamePossible() As Boolean
        Return myDataSet.CanRename()
    End Function

    Private Shared Function removePrefixFromBrowseName(ByVal bName As String) As String
        If bName.Contains(".") Then
            Return bName.Substring(bName.LastIndexOf(".") + 1)
        Else
            Return bName
        End If
    End Function

    Public Overrides Function getPathStr() As String
        Return myDataSet.Workspace.PathName
    End Function

    'todo Implenment this!
    ''' <summary>
    ''' Returns the fully qualified IDataName as a String if possible.
    ''' </summary>
    ''' <returns>
    ''' a string of the current IDataName's the fully qualified name. Or is a path is not
    ''' available then just the fully qualified name is returned (ie including the filename
    ''' extension [eg ".shp"] the RDBMS database or RDBMS user name prefixes
    ''' [eg "mapaction.sde."]
    ''' </returns>
    ''' <remarks>
    ''' Returns getPathStr() + "\" + getNameStr()
    ''' Returns the fully qualified IDataName as a String, if a suitable meaning of
    ''' path is applicable. If there is no easy or meaningful sense of a path (eg for a 
    ''' RDBMS) then the fully qualified name is returned (ie including the filename
    ''' extension [eg ".shp"] the RDBMS database or RDBMS user name prefixes
    ''' [eg "mapaction.sde."].
    ''' </remarks>
    Public Overrides Function getNameAndFullPathStr() As String

    End Function


    Protected Friend Overrides Function getUnderlyingDataType() As String
        Dim typeStr As String
        Dim fc As IFeatureClass

        If TypeOf myDataSet Is IFeatureClass Then
            fc = DirectCast(myDataSet, IFeatureClass)
            Select Case fc.ShapeType
                Case esriGeometryType.esriGeometryPoint, _
                         esriGeometryType.esriGeometryMultipoint
                    typeStr = DATATYPE_CLAUSE_POINT

                Case esriGeometryType.esriGeometryLine, _
                        esriGeometryType.esriGeometryCircularArc, _
                        esriGeometryType.esriGeometryEllipticArc, _
                        esriGeometryType.esriGeometryBezier3Curve, _
                        esriGeometryType.esriGeometryPath, _
                        esriGeometryType.esriGeometryPolyline, _
                        esriGeometryType.esriGeometryRay
                    typeStr = DATATYPE_CLAUSE_LINE

                Case esriGeometryType.esriGeometryRing, _
                        esriGeometryType.esriGeometryPolygon, _
                        esriGeometryType.esriGeometryEnvelope, _
                        esriGeometryType.esriGeometryMultiPatch, _
                        esriGeometryType.esriGeometryTriangleStrip, _
                        esriGeometryType.esriGeometryTriangleFan, _
                        esriGeometryType.esriGeometryTriangles
                    typeStr = DATATYPE_CLAUSE_POLYGON
                Case Else
                    typeStr = DATATYPE_CLAUSE_UNKNOWN
            End Select

        ElseIf TypeOf myDataSet Is IRaster Then
            typeStr = DATATYPE_CLAUSE_RASTER

        ElseIf TypeOf myDataSet Is IRasterCatalog Then
            typeStr = DATATYPE_CLAUSE_RASTER_CATALOG

        ElseIf TypeOf myDataSet Is ITin Then
            typeStr = DATATYPE_CLAUSE_TIN

        ElseIf TypeOf myDataSet Is ITable Then
            typeStr = DATATYPE_CLAUSE_TABLE

        Else
            typeStr = DATATYPE_CLAUSE_UNKNOWN

        End If

        Return typeStr
    End Function

    Public Overrides Sub performRename(ByVal newNameStr As String)
        If Not isRenameable() Then
            Throw New RenamingDataException(Me)
        Else
            myDataSet.Rename(newNameStr)
        End If
    End Sub

    '    '
    '    'AccessWorkspaceFactory
    '    '
    '    'Create an Access workspace factory.
    '    IWorkspaceFactory workspaceFactory = new AccessWorkspaceFactoryClass();

    '    '// Create a new Access workspace\personal geodatabase.
    '    IWorkspaceName workspaceName = workspaceFactory.Create(  "C:\\temp\\", "MyPGDB.mdb", null, 0);

    '    '// Cast for IName.
    '    IName name = (IName)workspaceName;

    '    '//Open a reference to the Access workspace through the Name object.
    '    IWorkspace pGDB_Wor = (IWorkspace)name.Open();

    '    '//Open another Access workspace.
    '    pGDB_Wor = workspaceFactory.OpenFromFile("C:\\temp\\us_states.mdb", 0);


    '    '
    '    'FileGDBWorkspaceFactory
    '    '
    '    '// Create a file geodatabase workspace factory.
    'ESRI.ArcGIS.Geodatabase.IWorkspaceFactory workspaceFactory = new
    '  FileGDBWorkspaceFactoryClass();

    '// Create a new file geodatabase.
    'ESRI.ArcGIS.Geodatabase.IWorkspaceName workspaceName = workspaceFactory.Create(
    '  "C:\\temp\\", "MyFGDB.gdb", null, 0);

    '// Cast for IName.
    'ESRI.ArcGIS.esriSystem.IName name = (IName)workspaceName;

    '//Open a reference to the file geodatabase workspace through the Name object.
    'ESRI.ArcGIS.Geodatabase.IWorkspace fGDB_Wor = (IWorkspace)name.Open();

    '//Open another file geodatabase workspace.
    'fGDB_Wor = workspaceFactory.OpenFromFile("C:\\temp\\us_states.gdb", 0);

    '    SdeWorkspaceFactory
    '    // Gets a reference to an existing workspace via a set of connection properties.
    'public ESRI.ArcGIS.Geodatabase.IWorkspace arcSDEWorkspaceOpen(String server,
    '  String instance, String user, String password, String database, String
    '  version)
    '{
    '  try
    '  {
    '    // Create and populate the property set.
    '    ESRI.ArcGIS.esriSystem.IPropertySet propertySet = new
    '      ESRI.ArcGIS.esriSystem.PropertySetClass();
    '    propertySet.SetProperty("SERVER", server);
    '    propertySet.SetProperty("INSTANCE", instance);
    '    propertySet.SetProperty("DATABASE", database);
    '    propertySet.SetProperty("USER", user);
    '    propertySet.SetProperty("PASSWORD", password);
    '    propertySet.SetProperty("VERSION", version);

    '    ESRI.ArcGIS.Geodatabase.IWorkspaceFactory2 workspaceFactory;
    '    workspaceFactory = (ESRI.ArcGIS.Geodatabase.IWorkspaceFactory2)new
    '      SdeWorkspaceFactoryClass();
    '    return workspaceFactory.Open(propertySet, 0);
    '  }
    '  catch (Exception e)
    '  {
    '    throw new Exception(String.Format("arcSDEWorkspaceOpen: {0}", e.Message), e)
    '      ;
    '  }
    '}



    '    DataServerManager
    '    // Create a DataServerManager object.
    'IDataServerManager dataserverManager = new DataServerManagerClass();

    '// Set the server name and connect to the server.
    'dataserverManager.ServerName = "tivo\\sqlexpress";
    'dataserverManager.Connect();

    '// Open one of the geodatabases in the database server.
    'IDataServerManagerAdmin dataservermanagerAdmin = (IDataServerManagerAdmin)
    '  dataserverManager;
    'ESRI.ArcGIS.Geodatabase.IWorkspaceName workspaceName =
    '  dataservermanagerAdmin.CreateWorkspaceName("sewer", "VERSION", "dbo.Default");
    'ESRI.ArcGIS.esriSystem.IName name = (IName)workspaceName;
    'ESRI.ArcGIS.Geodatabase.IWorkspace GDB_wor = (IWorkspace)name.Open();


End Class
