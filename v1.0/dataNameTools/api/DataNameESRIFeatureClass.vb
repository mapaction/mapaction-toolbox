Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports System.IO

''' <summary>
''' Provides a specfic implenmentation of the IDataName, based on ESRI 
''' FeatureClasses in their various guises (eg shapefiles or members of
''' a GDB).
''' </summary>
''' <remarks>
''' Provides a specfic implenmentation of the IDataName, based on ESRI 
''' FeatureClasses in their various guises (eg shapefiles or members of
''' a GDB).
'''
''' There is no public constructor for this class. New instances should
''' be generated using the getLayerDataNamesList() method of a relevant
''' IDataListConnection object.
'''  </remarks>
Public Class DataNameESRIFeatureClass
    Inherits AbstractDataName

    Private m_DataSet As IDataset

    ''' <summary>
    ''' Create a new IDataName for the specified ESRI.ArcGIS.Geodatabase.IDataset.
    ''' </summary>
    ''' <param name="ds">A IDataset object</param>
    ''' <param name="dncl">The DataNameLookupTable against which the validity
    ''' of the name will be checked.</param>
    ''' <param name="blnAllowRenames">Allows the caller to create the object in readonly 
    ''' mode. Useful if the IDataListConnection would need to be modified to accomidate
    ''' renamed IDataName objects</param>
    ''' <remarks>
    ''' Create a new IDataName for the specified ESRI.ArcGIS.Geodatabase.IDataset.
    ''' </remarks>
    Protected Friend Sub New(ByVal ds As IDataset, ByRef dncl As IDataNameClauseLookup, ByVal blnAllowReNames As Boolean)
        MyBase.new(removePrefixFromBrowseName(ds), dncl, blnAllowReNames)
        m_DataSet = ds
        'System.Console.WriteLine("Testing ds.BrowseName: " & ds.BrowseName)
        'ds.
        'Dim a As ESRI.ArcGIS.Geodatabase.esriWorkspaceType
        'a = 
        'ds.Workspace.Type
    End Sub

    ''' <summary>
    ''' Returns a runtime value indicating whether or not the underlying feature class
    ''' renamed or not. This is dependant on whether or not the feature class is 
    ''' readonly or not and if relevant whether there are any other locks held on the 
    ''' GDB.
    ''' </summary>
    ''' <returns>
    ''' TRUE = The feature class can be renamed.
    ''' FALSE = Attempting to rename the feature class will cause an exception to be raised.
    ''' </returns>
    ''' <remarks>
    ''' Returns a runtime value indicating whether or not the underlying feature class
    ''' renamed or not. This is dependant on whether or not the feature class is 
    ''' readonly or not and if relevant whether there are any other locks held on the 
    ''' GDB.
    ''' 
    ''' The runtime conbination of being both *allowed* and *possible* are combined by 
    ''' the isRenameable() method. End users should call this method.
    ''' </remarks>
    Protected Friend Overrides Function isRenamePossible() As Boolean
        Return m_DataSet.CanRename()
    End Function


    ''' <summary>
    ''' A private helper function to remove and RDBMS user, schema and/or database prefixes
    ''' from the Name string.
    ''' </summary>
    ''' <param name="ds">The IDataset representing of the table/featureclass as it appears in the RDMS system.
    ''' </param>
    ''' <returns>The name of the table with the RDBMS user, schema and/or database prefixes
    ''' removed if necessary.</returns>
    ''' <remarks>
    ''' A private helper function to remove and RDBMS user, schema and/or database prefixes
    ''' from the Name string.
    ''' </remarks>
    Private Shared Function removePrefixFromBrowseName(ByVal ds As IDataset) As String
        Dim strBrowseName As String
        strBrowseName = ds.BrowseName

        If ds.Workspace.Type <> esriWorkspaceType.esriFileSystemWorkspace And _
            strBrowseName.Contains(".") Then
            Return strBrowseName.Substring(strBrowseName.LastIndexOf(".") + 1)
        Else
            Return strBrowseName
        End If
    End Function

    ''' <summary>
    ''' Returns the path of the directory containing the current file as a String.
    ''' Should not include trailing slash or backslash.
    ''' </summary>
    ''' <returns>
    ''' A string of the path of the directory containing the current file. 
    ''' </returns>
    ''' <remarks>
    ''' Returns the path of the directory containing the current file as a String.
    ''' Should not include trailing slash or backslash.
    ''' </remarks>
    Public Overrides Function getPathStr() As String
        Dim strPath As String

        strPath = m_DataSet.Workspace.PathName

        If strPath IsNot Nothing AndAlso strPath.EndsWith(System.IO.Path.DirectorySeparatorChar) Then
            strPath.Remove(strPath.LastIndexOf(System.IO.Path.DirectorySeparatorChar))
        End If

        Return strPath
    End Function

    ''' <summary>
    ''' Returns the underlying IDataSet Object.
    ''' </summary>
    ''' <returns>A object appropriate for the particular implenmentation</returns>
    ''' <remarks>
    ''' Returns the underlying IDataSet Object.
    ''' </remarks>
    Public Overrides Function getObject() As Object
        Return m_DataSet
    End Function

    ''' <summary>
    ''' Returns the fully qualified path and name of the current feature class as a String.
    ''' Should not include trailing slash or backslash.
    ''' </summary>
    ''' <returns>
    ''' A string of the fully qualified path and name of the current feature class. 
    ''' </returns>
    ''' <remarks>
    ''' Returns the fully qualified path and name of the current feature class as a String.
    ''' Should not include trailing slash or backslash. If there is no easy or meaningful 
    ''' sense of a path (eg for a RDBMS) then the fully qualified name is returned (ie 
    ''' including the filename extension [eg ".shp"] the RDBMS database or RDBMS user 
    ''' name prefixes [eg "mapaction.sde."].
    ''' </remarks>
    Public Overrides Function getNameAndFullPathStr() As String
        Dim strPath As String

        strPath = getPathStr()

        If strPath Is Nothing Then
            Return m_DataSet.FullName.NameString
        Else
            Return strPath & System.IO.Path.DirectorySeparatorChar & m_DataSet.FullName.NameString
        End If
    End Function

    ''' <summary>
    ''' This method returns a string which represents the underlying geographical data type.
    ''' </summary>
    ''' <returns>One of the constants with the prefix "DATATYPE_CLAUSE_"</returns>
    ''' <remarks>
    ''' This method returns a string which represents the underlying geographical data type.
    ''' 
    ''' This method is called within the checkNameStatus() method to ensure
    ''' that the type specified in the data name matches the underlying geographical type.
    ''' </remarks>
    Protected Friend Overrides Function getUnderlyingDataType() As String
        Dim strType As String
        Dim fc As IFeatureClass

        If TypeOf m_DataSet Is IFeatureClass Then
            fc = DirectCast(m_DataSet, IFeatureClass)
            Select Case fc.ShapeType
                Case esriGeometryType.esriGeometryPoint, _
                         esriGeometryType.esriGeometryMultipoint
                    strType = DATATYPE_CLAUSE_POINT

                Case esriGeometryType.esriGeometryLine, _
                        esriGeometryType.esriGeometryCircularArc, _
                        esriGeometryType.esriGeometryEllipticArc, _
                        esriGeometryType.esriGeometryBezier3Curve, _
                        esriGeometryType.esriGeometryPath, _
                        esriGeometryType.esriGeometryPolyline, _
                        esriGeometryType.esriGeometryRay
                    strType = DATATYPE_CLAUSE_LINE

                Case esriGeometryType.esriGeometryRing, _
                        esriGeometryType.esriGeometryPolygon, _
                        esriGeometryType.esriGeometryEnvelope, _
                        esriGeometryType.esriGeometryMultiPatch, _
                        esriGeometryType.esriGeometryTriangleStrip, _
                        esriGeometryType.esriGeometryTriangleFan, _
                        esriGeometryType.esriGeometryTriangles
                    strType = DATATYPE_CLAUSE_POLYGON
                Case Else
                    strType = DATATYPE_CLAUSE_UNKNOWN
            End Select

        ElseIf TypeOf m_DataSet Is IRaster Then
            strType = DATATYPE_CLAUSE_RASTER

        ElseIf TypeOf m_DataSet Is IRasterCatalog Then
            strType = DATATYPE_CLAUSE_RASTER_CATALOG

        ElseIf TypeOf m_DataSet Is ITin Then
            strType = DATATYPE_CLAUSE_TIN

        ElseIf TypeOf m_DataSet Is ITable Then
            strType = DATATYPE_CLAUSE_TABLE

        Else
            strType = DATATYPE_CLAUSE_UNKNOWN

        End If

        Return strType
    End Function


    ''' <summary>
    ''' Renames the underlying feature class with the new name. The new name does not
    ''' have to be valid or even parsable as an IDataName, but must be acceptable
    ''' for the underlying storage (filesystem or DB).
    ''' </summary>
    ''' <param name="strNewName">The new Name for the feature class.</param>
    ''' <remarks>
    ''' Renames the underlying feature class with the new name. The new name does not
    ''' have to be valid or even parsable as an IDataName, but must be acceptable
    ''' for the underlying storage (filesystem or DB).
    ''' 
    ''' If the underlying storage is readonly then the implenmenting method
    ''' will throw a RenamingDataException exception.
    ''' 
    ''' End users should not call this method, but use the rename() method 
    ''' instead.
    ''' </remarks>
    Public Overrides Sub performRename(ByVal strNewName As String)
        If Not isRenameable() Then
            Throw New RenamingDataException(Me)
        Else
            m_DataSet.Rename(strNewName)
            m_strName = m_DataSet.Name
        End If
    End Sub

End Class
