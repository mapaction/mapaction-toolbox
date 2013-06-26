Public Class Form2

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim Output As New DataTable
        If TextBox1.Text = "" Then
            MsgBox("Select a table to get the data from")
            Exit Sub
        End If
        DataGridView1.DataSource = Nothing

        GetData(TextBox1.Text, Output)
        If Output Is Nothing Then
            MsgBox("No data")
        Else
            DataGridView1.DataSource = Output
        End If
    End Sub

End Class
