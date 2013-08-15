Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI

Public Class DataListSelectorPanel




    Private Function isParentArcMap() As Boolean
        Dim myApp As IApplication
        Dim returnVal As Boolean

        Try
            myApp = New AppRef()
            returnVal = True
        Catch ex As Exception
            returnVal = False
        End Try

        Return returnVal
    End Function

    Private Function getParentArcMap() As IApplication
        Dim myApp As IApplication

        Try
            myApp = New AppRef()
        Catch ex As Exception
            myApp = Nothing
        End Try

        Return myApp
    End Function


End Class
