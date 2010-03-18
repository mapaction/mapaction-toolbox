
Public Class DataNameClauseLookupFactory
    Shared myFactory As DataNameClauseLookupFactory

    Private Sub New()
    End Sub

    Public Shared Function getFactory() As DataNameClauseLookupFactory
        If myFactory Is Nothing Then
            myFactory = New DataNameClauseLookupFactory
        End If

        getFactory = myFactory
    End Function

    Public Function createDataNameClauseLookup(ByVal dnclType As Integer, Optional ByRef args As String() = Nothing) As IDataNameClauseLookup

    End Function

End Class
