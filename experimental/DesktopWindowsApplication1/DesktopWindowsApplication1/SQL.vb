Imports MySql.Data.MySqlClient
Module SQL 'Setup a new MySQLConnection 
    'Dim Command so we can execute sql commands 
    Dim Command As New MySqlCommand
    Dim Connection As New MySqlConnection("server=" & Form1.txtServer.Text & ";user id=" & Form1.txtUserName.Text & "; password=" & Form1.txtPassword.Text & "; port=3306; database=" & Form1.txtDatabase.Text & "; pooling=false")


    Public Sub ConnecttoDB()
        Try
            Connection.Open()
            MsgBox("Connected to: " & Form1.txtServer.Text)
        Catch ex As MySqlException
            MsgBox("Grrr" & ex.Message)
        End Try
    End Sub

    Public Sub GetData(ByVal Table As String, ByRef Output As DataTable)
        Try 'Setting up a new command to grab all the data from the table the user inputs, limits the items to 100 so it doesnt laggg 
            Command = New MySqlCommand("SELECT * FROM `" & Table & "` LIMIT 100;", Connection) 'this fills all the data that was inside the command that was executed, into MySqlA 
            Dim MySqlA As New MySqlDataAdapter(Command) 'puts all the info from the database into the datatable 
            MySqlA.Fill(Output) 'if the rows count = 0 then that means there is no data so set the datatable to = nothing 
            If Output.Rows.Count = 0 Then
                Output = Nothing
            End If
        Catch e As MySqlException
            MsgBox(e.Message)
        End Try
    End Sub

End Module
