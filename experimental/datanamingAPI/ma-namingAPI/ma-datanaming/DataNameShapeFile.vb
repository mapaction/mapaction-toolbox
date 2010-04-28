
Public Class DataNameShapeFile
    Inherits AbstractDataName

    Public Overrides Function isRenameable() As Boolean

    End Function

    Public Overrides Function getPathStr() As String
        Return Nothing
    End Function


    Public Sub New(ByVal theName As String, ByRef theDNCL As IDataNameClauseLookup)
        MyBase.New(theName, theDNCL)
    End Sub

End Class
