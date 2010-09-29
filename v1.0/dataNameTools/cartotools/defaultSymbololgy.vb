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
Imports mapaction.datanames.api
Imports System.IO
Imports System.Text

Public Module DefaultSymbololgy

    'Private Shared m_defltSyb As DefaultSymbololgy = Nothing
    Private m_dInfoSymbRoot As DirectoryInfo
    Private m_fInfoSymbDNCLpath As FileInfo

    Private m_dataList As IDataListConnection
    Private m_dncl As IDataNameClauseLookup
    Private m_lstDNames As List(Of IDataName)


    'a private constructor to force the user to get hold of a single instance
    Sub New()
    End Sub

    'Public Shared Function getDefaultSymbololgy() As defaultSymbololgy
    '    If m_defltSyb Is Nothing Then
    '        m_defltSyb = New DefaultSymbololgy()
    '    End If

    '    Return m_defltSyb
    'End Function

    Public Property rootSymbolDir() As DirectoryInfo
        Get
            Return m_dInfoSymbRoot
        End Get
        Set(ByVal dInfoNew As DirectoryInfo)
            If dInfoNew IsNot Nothing AndAlso dInfoNew.Exists Then
                m_dInfoSymbRoot = dInfoNew
            Else
                Throw New DirectoryNotFoundException()
            End If
        End Set
    End Property


    Public Property rootSymbDNCLpath() As FileInfo
        Get
            Return m_fInfoSymbDNCLpath
        End Get
        Set(ByVal fInfoNew As FileInfo)
            If fInfoNew IsNot Nothing AndAlso fInfoNew.Exists AndAlso _
                      (fInfoNew.Extension = ".mdb") Then
                m_fInfoSymbDNCLpath = fInfoNew
            Else
                Throw New DirectoryNotFoundException()
            End If
        End Set
    End Property

    Public Sub setDefaultRootSymbPath(ByRef mxDoc As IMxDocument)
        Dim strPth As String

        strPth = getMARootDir(m_dataList.getPath()).FullName & "\3_Mapping\31_Resources\312_Layer_files"

        MsgBox("Default Root Symbols Directory:" & vbNewLine & strPth)

        m_dInfoSymbRoot = New DirectoryInfo(strPth)

    End Sub

    'Private Sub setDataNameList(ByRef mxDoc As IMxDocument)
    Private Sub setDataNameList(ByRef app As IApplication)
        'Dim dnclFactory As DataNameClauseLookupFactory
        Dim srtAryPath(1) As String
        'dnclFactory = DataNameClauseLookupFactory.getFactory()

        'MsgBox("Started setDataNameList()")
        m_dataList = DataListConnectionFactory.getFactory().createDataListConnection(app)
        'MsgBox("Got m_dataList")

        Try

            m_dncl = m_dataList.getDefaultDataNameClauseLookup()
            'MsgBox("Got m_dataList.getDefaultDataNameClauseLookup()")
        Catch ex1 As Exception
            Try
                srtAryPath(0) = rootSymbDNCLpath.FullName
                'MsgBox(rootSymbDNCLpath.FullName)
                m_dncl = DataNameClauseLookupFactory.createDataNameClauseLookup(dnClauseLookupType.MDB, srtAryPath, False)
                'MsgBox("Got data naming clause")
            Catch ex2 As Exception
                Try
                    'MsgBox("failed to get data naming clause")
                    m_dncl = DataNameClauseLookupFactory.getFallBackDataNameClauseLookup()

                Catch ex3 As Exception
                    Throw ex3
                End Try

            End Try
        End Try

        m_lstDNames = m_dataList.getLayerDataNamesList(m_dncl)

    End Sub

    'Public Function applyDefaultSymbols(ByRef mxDoc As IMxDocument) As String

    Public Function applyDefaultSymbols(ByRef mxDoc As IMxDocument, ByRef app As IApplication) As String
        Dim toc As IContentsView
        Dim stb As New StringBuilder


        Dim blnGotDNCL As Boolean

        Try
            setDataNameList(app)
            blnGotDNCL = True
            If m_dInfoSymbRoot Is Nothing Then
                'stb.AppendLine("Cannot attempt to apply default symbology until root directory is defined")
                setDefaultRootSymbPath(mxDoc)
            End If
        Catch exDNameList As Exception
            stb.AppendLine("Unable to find data name clause lookup tables")
            stb.AppendLine("It may help to save the MXD in the standard location in folder 33_MXD_Maps")
            stb.AppendLine(exDNameList.Message)
            blnGotDNCL = False
        End Try

        If blnGotDNCL Then
            toc = mxDoc.ContentsView(0) ' Display View

            stb.AppendLine("Attempted to apply default symbology for map:")
            stb.AppendLine(m_dataList.getPath().FullName)
            stb.AppendLine()

            applyDefaultSymbols(toc.SelectedItem, stb)
            mxDoc.ActiveView.Refresh()
            toc.Refresh(toc.SelectedItem)
        End If

        System.Console.Write(stb.ToString())

        Return stb.ToString()
    End Function

    Public Function applyDefaultSymbols(ByRef obj As Object, ByRef stb As StringBuilder) As StringBuilder
        'Dim aryTemp As Array
        'Dim colTemp As Collection
        Dim mapTemp As IMap
        Dim lyrTemp As ILayer
        Dim setTemp As ISet
        Dim stbResult As StringBuilder

        'If TypeOf obj Is Array Then
        '    aryTemp = CType(obj, Array)

        '    stb.AppendLine("applyDefaultSymbols obj is Array")
        '    System.Console.WriteLine("applyDefaultSymbols obj is Array")

        '    For i = 0 To (aryTemp.Count - 1)
        '        stbResult = applyDefaultSymbols(aryTemp.Element(i), stb)
        '    Next

        'ElseIf TypeOf obj Is Collection Then
        '    colTemp = CType(obj, Collection)

        '    stb.AppendLine("applyDefaultSymbols obj is Collection")
        '    System.Console.WriteLine("applyDefaultSymbols obj is Collection")

        '    For Each objCurrent In colTemp
        '        stbResult = applyDefaultSymbols(objCurrent, stb)
        '    Next
        If TypeOf obj Is IMap Then
            mapTemp = CType(obj, IMap)

            stb.AppendLine("applyDefaultSymbols obj is IMap")
            System.Console.WriteLine("applyDefaultSymbols obj is IMap")

            stb.AppendFormat("For Data Frame; {0}", mapTemp.Name)
            stb.AppendLine()
            For i = 0 To (mapTemp.LayerCount - 1)
                stbResult = applyDefaultSymbols(mapTemp.Layer(i), stb)
            Next

        ElseIf TypeOf obj Is ILayer Then
            lyrTemp = CType(obj, ILayer)

            stb.AppendLine("applyDefaultSymbols obj is ILayer")
            System.Console.WriteLine("applyDefaultSymbols obj is ILayer")

            stbResult = applyDefaultSymbols(lyrTemp, stb)
        ElseIf TypeOf obj Is ISet Then
            setTemp = CType(obj, ISet)

            stb.AppendLine("applyDefaultSymbols obj is ISet")
            System.Console.WriteLine("applyDefaultSymbols obj is ISet")

            For i = 0 To (setTemp.Count - 1)
                stbResult = applyDefaultSymbols(setTemp.Next, stb)
            Next
            
        Else
            TypeName(obj)
            stb.AppendLine("applyDefaultSymbols obj is unknown: " & TypeName(obj))
            System.Console.WriteLine("applyDefaultSymbols obj is unknown: " & TypeName(obj))

        End If

        Return stbResult
    End Function


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
        If TypeOf lyr Is IDataset Then
            dsTemp = CType(lyr, IDataset)

            For Each dnCurrent In m_lstDNames
                If dnCurrent.isNameParseable() Then

                    dsCurrent = TryCast(dnCurrent.getObject(), IDataset)

                    If dsCurrent IsNot Nothing AndAlso _
                        dsCurrent.Name = dsTemp.Name AndAlso _
                        dsCurrent.Workspace.PathName = dsTemp.Workspace.PathName Then

                        Try
                            fInfoLyr = findLyrFile(dnCurrent.getDataTypeClause(), _
                                                   dnCurrent.getDataCategoryClause(), _
                                                   dnCurrent.getDataThemeClause(), _
                                                   dnCurrent.getSourceClause(), False)


                            catalog.Location = fInfoLyr.Directory.FullName
                            lyrFact.Catalog = catalog

                            fNames = New ESRI.ArcGIS.esriSystem.FileNames
                            fNames.Add(fInfoLyr.FullName)

                            enumGxObj = lyrFact.GetChildren(fInfoLyr.Directory.FullName, fNames)

                            gxObj = enumGxObj.Next
                            gxLayer = CType(gxObj, IGxLayer)

                            gFLayer = CType(gxLayer.Layer, IGeoFeatureLayer)
                            annoLayerPropsColl = gFLayer.AnnotationProperties
                            gfRenderer = gFLayer.Renderer

                            gFLayer = CType(lyr, IGeoFeatureLayer)
                            annoLayerPropsColl = gFLayer.AnnotationProperties
                            gFLayer.Renderer = gfRenderer

                            gFLayer.DisplayAnnotation = True

                            stb.AppendFormat("Applied default symbology to ; {0}", dnCurrent.getNameStr())
                            stb.AppendLine()

                        Catch fnfex As FileNotFoundException
                            'We don't need to do anything here
                            stb.AppendFormat("Unable to find default lyr file for params {0}, {1}, {2}, {3}", _
                                             dnCurrent.getDataTypeClause(), _
                                             dnCurrent.getDataCategoryClause(), _
                                             dnCurrent.getDataThemeClause(), _
                                             dnCurrent.getSourceClause())
                            stb.AppendLine()

                        Catch ex As Exception
                            'We don't need to do anything here
                            stb.AppendFormat("Unknown Error applying default lyr file {0}, {1}, {2}, {3}", _
                                             dnCurrent.getDataTypeClause(), _
                                             dnCurrent.getDataCategoryClause(), _
                                             dnCurrent.getDataThemeClause(), _
                                             dnCurrent.getSourceClause())
                            stb.AppendLine()

                        End Try
                    End If
                Else
                    stb.AppendFormat("Unparsable Name; {0}", dnCurrent.getNameStr())
                    stb.AppendLine()
                End If
            Next
        End If

        Return stb
    End Function

    Public Sub applyDefaultSymbolsDEMO(ByRef mxDoc As IMxDocument)

        'Dim pGxFile As IGxFile
        Dim pGFLayer As IGeoFeatureLayer
        Dim pGxLayer As IGxLayer
        Dim pGxDialog As IGxDialog
        Dim pGxObjFilter As IGxObjectFilter
        Dim pEnumGxObj As IEnumGxObject
        Dim pAnnoLayerPropsColl As IAnnotateLayerPropertiesCollection
        Dim pGxObj As IGxObject
        'Dim pMxDoc As IMxDocument

        'pMxDoc = m_HookHelper.
        If mxDoc.SelectedLayer Is Nothing Then
            MsgBox("Please select feature class to label with .lyr file label classes")
            Exit Sub
        End If
        'mxDoc.FocusMap

        pGxDialog = New GxDialog
        pGxObjFilter = New GxFilterLayers
        pGxDialog.ObjectFilter = pGxObjFilter
        pGxDialog.Title = "Select Layer(.lyr) file"
        pGxDialog.ButtonCaption = "Apply Labels"

        If pGxDialog.DoModalOpen(0, pEnumGxObj) Then
            pGxObj = pEnumGxObj.Next
            pGxLayer = CType(pGxObj, IGxLayer)
        Else
            Exit Sub
        End If
        pGFLayer = CType(pGxLayer.Layer, IGeoFeatureLayer)
        pAnnoLayerPropsColl = pGFLayer.AnnotationProperties

        'Apply label classes  to selected layer in arcmap
        pGFLayer = CType(mxDoc.SelectedLayer, IGeoFeatureLayer)
        pGFLayer.AnnotationProperties = pAnnoLayerPropsColl
        pGFLayer.DisplayAnnotation = True
        mxDoc.ActiveView.Refresh()
        mxDoc.CurrentContentsView.Refresh(pGFLayer)

    End Sub

#Region "findLyrFile methods"
    '
    '<symbolsroot>\<datacategory>\<datatheme>\<source>_<datatype>.lyr
    '<symbolsroot>\<datacategory>\<datatheme>_<datatype>.lyr
    '<symbolsroot>\<datacategory>_<datatype>.lyr
    '
    'A background layer and/or label expression file will be searched for in the following order:
    '
    '<symbolsroot>\<datacategory>\<datatheme>\<source>_<datatype>_bg.lyr
    '<symbolsroot>\<datacategory>\<datatheme>_<datatype>_bg.lyr
    '<symbolsroot>\<datacategory>_<datatype>_bg.lyr
    '
    Private Function findLyrFile(ByVal strDataType As String, _
                                    ByVal strDataCat As String, _
                                    ByVal strDataTheme As String, _
                                    ByVal strSource As String, _
                                    ByVal blnGetBackgroundLyrs As Boolean) As FileInfo

        Dim lstStrTestPaths As New List(Of String)

        Dim strAryParams(8) As String
        Dim fInfoTest As FileInfo = Nothing
        Dim fInfoResult As FileInfo = Nothing

        If m_dInfoSymbRoot Is Nothing Then
            Throw New ArgumentException("Cannot search for layer files until the root directory has been saved")
        Else
            strAryParams(0) = m_dInfoSymbRoot.FullName
            strAryParams(1) = Path.DirectorySeparatorChar
            strAryParams(2) = ".lyr"
            If blnGetBackgroundLyrs Then
                strAryParams(3) = "_bg"
            Else
                strAryParams(3) = String.Empty
            End If

            strAryParams(4) = strDataType
            strAryParams(5) = strDataCat
            strAryParams(6) = strDataTheme

            If strSource Is Nothing Then
                strAryParams(7) = String.Empty
            Else
                strAryParams(7) = strSource
            End If

            'A background layer and/or label expression file will be searched for in the following order:
            '
            '<symbolsroot>\<datacategory>\<datatheme>\<source>_<datatype>_bg.lyr
            lstStrTestPaths.Add(String.Format("{0}{1}{5}{1}{6}{1}{7}_{4}{3}{2}", strAryParams))

            '<symbolsroot>\<datacategory>\<datatheme>_<datatype>_bg.lyr
            lstStrTestPaths.Add(String.Format("{0}{1}{5}{1}{6}_{4}{3}{2}", strAryParams))

            '<symbolsroot>\<datacategory>_<datatype>_bg.lyr
            lstStrTestPaths.Add(String.Format("{0}{1}{5}_{4}{3}{2}", strAryParams))

            For Each strFilePath In lstStrTestPaths
                'MsgBox("test path : " & strFilePath)

                If fInfoResult Is Nothing Then
                    fInfoTest = New FileInfo(strFilePath)

                    If fInfoTest.Exists() Then
                        fInfoResult = fInfoTest
                    End If
                End If
            Next

            ''
            If fInfoResult Is Nothing Then
                Throw New FileNotFoundException()
            End If
        End If

        Return fInfoResult
    End Function

    Private Function findLyrFile(ByVal strDataType As String, _
                                    ByVal strDataCat As String, _
                                    ByVal strDataTheme As String) As FileInfo
        Return findLyrFile(strDataType, strDataCat, strDataTheme, Nothing, False)
    End Function

    Private Function findLyrFile(ByVal strDataType As String, _
                                  ByVal strDataCat As String, _
                                  ByVal strDataTheme As String, _
                                  ByVal blnGetBackgroundLyrs As Boolean) As FileInfo
        Return findLyrFile(strDataType, strDataCat, strDataTheme, Nothing, blnGetBackgroundLyrs)
    End Function

    Private Function findLyrFile(ByVal strDataType As String, _
                                   ByVal strDataCat As String, _
                                   ByVal strDataTheme As String, _
                                   ByVal strSource As String) As FileInfo
        Return findLyrFile(strDataType, strDataCat, strDataTheme, strSource, False)
    End Function
#End Region

End Module
