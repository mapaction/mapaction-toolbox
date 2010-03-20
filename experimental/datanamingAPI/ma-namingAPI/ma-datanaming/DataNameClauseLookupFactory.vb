
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
        Dim theDataNameClauseLookup As IDataNameClauseLookup

        Select Case dnclType
            Case DATACLAUSE_LOOKUP_MDB
                theDataNameClauseLookup = New MDBDataNameClauseLookup(args)
            Case Else
                Throw New ArgumentException("DataNameClauseLookup of type " & dnclType & " not recgonised.")
        End Select

        Return theDataNameClauseLookup
    End Function

End Class
