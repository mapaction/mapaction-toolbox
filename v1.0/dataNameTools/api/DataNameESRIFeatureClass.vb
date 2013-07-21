'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''Copyright (C) 2010 MapAction UK Charity No. 1075977
''
''www.mapaction.org
''
''This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 3 of the License, or (at your option) any later version.
''
''This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
''
''You should have received a copy of the GNU General Public License along with this program; if not, see <http://www.gnu.org/licenses>.
''
''Additional permission under GNU GPL version 3 section 7
''
''If you modify this Program, or any covered work, by linking or combining it with 
''ESRI ArcGIS Desktop Products (ArcView, ArcEditor, ArcInfo, ArcEngine Runtime and ArcEngine Developer Kit) (or a modified version of that library), containing parts covered by the terms of ESRI's single user or concurrent use license, the licensors of this Program grant you additional permission to convey the resulting work.
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Carto
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
        Dim strResult As String
        strBrowseName = ds.BrowseName

        If ds.Workspace.Type <> esriWorkspaceType.esriFileSystemWorkspace And _
            strBrowseName.Contains(".") Then
            strResult = strBrowseName.Substring(strBrowseName.LastIndexOf(".") + 1)
        Else
            strResult = strBrowseName
        End If

        Return strResult
    End Function

    Public Overrides Function getNameStr() As String
        Dim strResult As String

        If m_DataSet.Type = esriDatasetType.esriDTRasterDataset And m_strName.Contains(".") Then
            strResult = m_strName.Substring(0, Math.Min(m_strName.LastIndexOf("."), m_strName.Length - 1))
        Else
            strResult = m_strName
        End If

        Return strResult
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
    Public Overrides Function getUnderlyingDataType() As String
        Dim strType As String
        Dim fc As IFeatureClass

        'check to see if there is an under lying Feature Class
        'If TypeOf m_DataSet Is IFeatureClass Then
        '    'This works for ArcCatalog
        '    fc = DirectCast(m_DataSet, IFeatureClass)
        'ElseIf TypeOf m_DataSet Is ESRI.ArcGIS.Carto.IFeatureLayer Then
        '    'This works for ArcMap
        '    fc = DirectCast(m_DataSet, IFeatureLayer).FeatureClass
        'Else
        '    fc = Nothing
        'End If

        ''MsgBox("m_DataSet.type = " & m_DataSet.Type & " cast " & (TypeOf m_DataSet Is IFeatureClass) & "  " & _
        ''       (TypeOf m_DataSet Is ESRI.ArcGIS.Carto.IFeatureLayer))

        'If fc IsNot Nothing Then
        '    'fc = DirectCast(m_DataSet, IFeatureClass)
        '    'MsgBox("fc.type = " & fc.ShapeType)

        '    Select Case fc.ShapeType
        '        Case esriGeometryType.esriGeometryPoint, _
        '                 esriGeometryType.esriGeometryMultipoint
        '            strType = DATATYPE_CLAUSE_POINT

        '        Case esriGeometryType.esriGeometryLine, _
        '                esriGeometryType.esriGeometryCircularArc, _
        '                esriGeometryType.esriGeometryEllipticArc, _
        '                esriGeometryType.esriGeometryBezier3Curve, _
        '                esriGeometryType.esriGeometryPath, _
        '                esriGeometryType.esriGeometryPolyline, _
        '                esriGeometryType.esriGeometryRay
        '            strType = DATATYPE_CLAUSE_LINE

        '        Case esriGeometryType.esriGeometryRing, _
        '                esriGeometryType.esriGeometryPolygon, _
        '                esriGeometryType.esriGeometryEnvelope, _
        '                esriGeometryType.esriGeometryMultiPatch, _
        '                esriGeometryType.esriGeometryTriangleStrip, _
        '                esriGeometryType.esriGeometryTriangleFan, _
        '                esriGeometryType.esriGeometryTriangles
        '            strType = DATATYPE_CLAUSE_POLYGON
        '        Case Else
        '            strType = DATATYPE_CLAUSE_UNKNOWN
        '    End Select

        'ElseIf TypeOf m_DataSet Is IRaster Then
        '    strType = DATATYPE_CLAUSE_RASTER

        'ElseIf TypeOf m_DataSet Is IRasterCatalog Then
        '    strType = DATATYPE_CLAUSE_RASTER_CATALOG

        'ElseIf TypeOf m_DataSet Is ITin Then
        '    strType = DATATYPE_CLAUSE_TIN

        'ElseIf TypeOf m_DataSet Is ITable Then
        '    strType = DATATYPE_CLAUSE_TABLE

        'End If


        Select Case m_DataSet.Type
            Case esriDatasetType.esriDTFeatureClass
                'check to see if there is an under lying Feature Class
                If TypeOf m_DataSet Is IFeatureClass Then
                    'This works for ArcCatalog
                    fc = DirectCast(m_DataSet, IFeatureClass)
                ElseIf TypeOf m_DataSet Is ESRI.ArcGIS.Carto.IFeatureLayer Then
                    'This works for ArcMap
                    fc = DirectCast(m_DataSet, IFeatureLayer).FeatureClass
                Else
                    fc = Nothing
                End If

                'MsgBox("m_DataSet.type = " & m_DataSet.Type & " cast " & (TypeOf m_DataSet Is IFeatureClass) & "  " & _
                '       (TypeOf m_DataSet Is ESRI.ArcGIS.Carto.IFeatureLayer))
                If fc IsNot Nothing Then
                    'fc = DirectCast(m_DataSet, IFeatureClass)
                    'MsgBox("fc.type = " & fc.ShapeType)

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

                Else
                    strType = DATATYPE_CLAUSE_UNKNOWN
                End If
            Case esriDatasetType.esriDTRasterCatalog
                strType = DATATYPE_CLAUSE_RASTER_CATALOG
            Case esriDatasetType.esriDTRasterDataset
                strType = DATATYPE_CLAUSE_RASTER
            Case esriDatasetType.esriDTTin
                strType = DATATYPE_CLAUSE_TIN
            Case esriDatasetType.esriDTText
                strType = DATATYPE_CLAUSE_TEXT
            Case esriDatasetType.esriDTTable
                strType = DATATYPE_CLAUSE_TABLE
            Case Else
                strType = DATATYPE_CLAUSE_UNKNOWN
        End Select

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
    Protected Overrides Sub performRename(ByVal strNewName As String)
        If Not isRenameable() Then
            Throw New RenamingDataException(Me)
        Else
            m_DataSet.Rename(strNewName)
            m_strName = removePrefixFromBrowseName(m_DataSet)
        End If
    End Sub

End Class
