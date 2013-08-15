Imports mapaction.datanames.api
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Carto
Imports System.Text

Public Class CartoDefaultApplier

    Dim m_app As IApplication


    'Todo work out what needs to go here
    Sub New()
    End Sub

    Function setDefaults(ByRef mxDoc As IMxDocument) As StringBuilder
        Return setDefaults(mxDoc, True)
    End Function

    Function setDefaults(ByRef mxDoc As IMxDocument, ByVal blnSelectedLyrsOnly As Boolean) As StringBuilder

    End Function

    'todo is this actually the list/array type I want here?
    Function setDefaults(ByRef layers As ILayer()) As StringBuilder

    End Function



End Class
