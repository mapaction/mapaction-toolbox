Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Catalog
Imports ESRI.ArcGIS.CatalogUI
Imports ESRI.ArcGIS.Geodatabase
Imports System.IO
Imports System.Text

Public MustInherit Class AbstractCartoElement

    Public Function applyDefaultSymbols(ByRef lyr As ILayer, ByRef stb As StringBuilder) As StringBuilder
        Dim dsTemp As IDataset
        Dim dsCurrent As IDataset
        Dim fInfoLyr As FileInfo
        'Dim wkspFactory As IWorkspaceFactory2
        'Dim wksp As IWorkspace
        Dim lyrFact As GxLayerFactory
        Dim catalog As IGxCatalog
        Dim fNames As ESRI.ArcGIS.esriSystem.IFileNames

        Dim gxLayer As IGxLayer
        Dim enumGxObj As IEnumGxObject
        Dim gxObj As IGxObject
        Dim gFLayer As IGeoFeatureLayer
        Dim annoLayerPropsColl As IAnnotateLayerPropertiesCollection
        Dim gfRenderer As IFeatureRenderer

        lyrFact = New GxLayerFactory
        catalog = New GxCatalog
        'wkspFactory = New ESRI.ArcGIS.DataSourcesFile.ShapefileWorkspaceFactory()

        'now find this lyr in the dataname list!
        'lyrTemp()
        'If TypeOf lyr Is IDataset Then
        '    dsTemp = CType(lyr, IDataset)

        '    For Each dnCurrent In m_lstDNames
        '        If dnCurrent.isNameParseable() Then

        '            dsCurrent = TryCast(dnCurrent.getObject(), IDataset)

        '            If dsCurrent IsNot Nothing AndAlso _
        '                dsCurrent.Name = dsTemp.Name AndAlso _
        '                dsCurrent.Workspace.PathName = dsTemp.Workspace.PathName Then

        '                Try
        '                    fInfoLyr = findLyrFile(dnCurrent.getDataTypeClause(), _
        '                                           dnCurrent.getDataCategoryClause(), _
        '                                           dnCurrent.getDataThemeClause(), _
        '                                           dnCurrent.getSourceClause(), False)


        '                    catalog.Location = fInfoLyr.Directory.FullName
        '                    lyrFact.Catalog = catalog

        '                    fNames = New ESRI.ArcGIS.esriSystem.FileNames
        '                    fNames.Add(fInfoLyr.FullName)

        '                    enumGxObj = lyrFact.GetChildren(fInfoLyr.Directory.FullName, fNames)

        '                    gxObj = enumGxObj.Next
        '                    gxLayer = CType(gxObj, IGxLayer)

        '                    gFLayer = CType(gxLayer.Layer, IGeoFeatureLayer)
        '                    annoLayerPropsColl = gFLayer.AnnotationProperties
        '                    gfRenderer = gFLayer.Renderer

        '                    gFLayer = CType(lyr, IGeoFeatureLayer)
        '                    annoLayerPropsColl = gFLayer.AnnotationProperties
        '                    gFLayer.Renderer = gfRenderer

        '                    gFLayer.DisplayAnnotation = True

        '                    stb.AppendFormat("Applied default symbology to ; {0}", dnCurrent.getNameStr())
        '                    stb.AppendLine()

        '                Catch fnfex As FileNotFoundException
        '                    'We don't need to do anything here
        '                    stb.AppendFormat("Unable to find default lyr file for params {0}, {1}, {2}, {3}", _
        '                                     dnCurrent.getDataTypeClause(), _
        '                                     dnCurrent.getDataCategoryClause(), _
        '                                     dnCurrent.getDataThemeClause(), _
        '                                     dnCurrent.getSourceClause())
        '                    stb.AppendLine()

        '                Catch ex As Exception
        '                    'We don't need to do anything here
        '                    stb.AppendFormat("Unknown Error applying default lyr file {0}, {1}, {2}, {3}", _
        '                                     dnCurrent.getDataTypeClause(), _
        '                                     dnCurrent.getDataCategoryClause(), _
        '                                     dnCurrent.getDataThemeClause(), _
        '                                     dnCurrent.getSourceClause())
        '                    stb.AppendLine()

        '                End Try
        '            End If
        '        Else
        '            stb.AppendFormat("Unparsable Name; {0}", dnCurrent.getNameStr())
        '            stb.AppendLine()
        '        End If
        '    Next
        'End If

        Return stb
    End Function




End Class
