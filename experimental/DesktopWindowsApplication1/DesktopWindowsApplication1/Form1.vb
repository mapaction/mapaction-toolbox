Imports MySql.Data.MySqlClient

Public Class Form1
    Private Sub btnConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConnect.Click
        ConnecttoDB()
        'Try
        '    Dim Connection As New MySqlConnection("server=" & txtServer.Text & ";user id=" & txtUserName.Text & "; password=" & txtPassword.Text & "; port=3306; database=" & txtDatabase.Text & "; pooling=false")
        '    Connection.Open()
        '    MsgBox("Connected to: " & txtServer.Text)
        'Catch ex As MySqlException
        '    MsgBox(ex.Message)
        'End Try
    End Sub
End Class