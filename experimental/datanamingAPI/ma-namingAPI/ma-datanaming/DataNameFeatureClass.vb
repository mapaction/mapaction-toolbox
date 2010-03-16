
Public Class DataNameFeatureClass
    Inherits AbstractDataName

    Public Overrides Function isEditable() As Boolean

    End Function

    Public Overrides Function getPathStr() As String

    End Function

    Public Sub New(ByVal theName As String, ByRef theDNCL As IDataNameClauseLookup)
        MyBase.New(theName, theDNCL)
    End Sub

End Class
