Module ModRenamer
    Public Function Renamer(ByVal strOriginalFileName As String) As String
        'Andy Smith: The user has pressed the rename button and the old file name
        'is passed into this function.  This function takes the old name and gives it the new name
        Renamer = strOriginalFileName & "ReNamed"
    End Function


    Public Function CheckForValidFileName(ByVal strOriginalFileName As String) As String
        'Andy Smith: This is where the checking code goes
        If strOriginalFileName = "wes_rds_WNMA_ln.shp" Then    'This is a good file name
            CheckForValidFileName = "Valid"
        ElseIf strOriginalFileName = "wes_stl_check_WNMA_py.shp" Then    'This is not quite right but it'll do
            CheckForValidFileName = "Warning"
            'ElseIf strOriginalFileName = "This is not right and needs amending" Then
        ElseIf strOriginalFileName <> "" Then
            CheckForValidFileName = "Error"
        Else
            MsgBox("Not sure about the file name: " & strOriginalFileName)
        End If
    End Function
End Module