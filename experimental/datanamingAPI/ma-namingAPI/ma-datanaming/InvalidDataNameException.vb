
Public Class InvalidDataNameException
    Inherits Exception

    Private myNameStatus As Long

    Protected Friend Sub New(ByVal bitSum As Long)
        MyBase.New(getDescriptionFromStatus(bitSum))
        myNameStatus = bitSum
    End Sub

    Protected Friend Sub New(ByVal description As String, ByVal bitSum As Long, ByRef innerEx As Exception)
        MyBase.New(description, innerEx)
        myNameStatus = bitSum
    End Sub

    Public Function getNameStatus() As Long
        Return myNameStatus
    End Function

    Private Shared Function getDescriptionFromStatus(ByVal bitsum As Long) As String
        Dim returnVal As String = ""

        For Each statusStr In AbstractDataNameClauseLookup.getDataNamingStatusStrings(bitsum)
            returnVal = returnVal & statusStr & vbNewLine
        Next

        Return returnVal
    End Function

    '    AbstractDataNameClauseLookup.getDataNamingStatusStrings(status)

End Class
