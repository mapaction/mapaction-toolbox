Imports System.IO

''' <summary>
''' A helper class, used to sort FileInfo objects.
''' </summary>
''' <remarks>
''' A helper class, used to sort FileInfo objects.
''' </remarks>
Friend Class FileInfoComparer
    Implements IComparer

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
        Dim fInfo1 As FileInfo = CType(x, FileInfo)
        Dim fInfo2 As FileInfo = CType(y, FileInfo)

        Return fInfo1.Name.CompareTo(fInfo2.Name)
    End Function
End Class
