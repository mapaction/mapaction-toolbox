
Public Class LookupTableException
    Inherits Exception

    Protected Friend Sub New(ByVal description As String)
        MyBase.New(description)
    End Sub

    Protected Friend Sub New(ByVal description As String, ByRef innerEx As Exception)
        MyBase.New(description, innerEx)
    End Sub
End Class
