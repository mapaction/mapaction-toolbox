Imports System.Windows.Forms

Public Class DataRenameDialog

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Dim intCursorLoc As Integer

        intCursorLoc = TextBox1.SelectionStart
        'TextBox1.
        Label1.Text = sender.GetType.Name
        System.Console.WriteLine("sender.GetType = " & sender.GetType.Name)
        System.Console.WriteLine("e.GetType = " & e.GetType.Name)
        System.Console.WriteLine("TextBox1.SelectedText = " & TextBox1.SelectedText)
        System.Console.WriteLine("TextBox1.SelectionLength = " & TextBox1.SelectionLength)
        System.Console.WriteLine("TextBox1.SelectionStart = " & TextBox1.SelectionStart)
        TextBox1.Text = removeIllegalCharsFromString(TextBox1.Text)

        TextBox1.SelectionStart = intCursorLoc

    End Sub

    Private Function removeIllegalCharsFromString(ByRef strEnteredText As String) As String
        Dim strResult As String

        strResult = strEnteredText
        strResult = strResult.Replace(" ", "_")
        strResult = strResult.Replace("-", "_")
        strResult = strResult.Replace("__", "_")

        Return strResult
    End Function
End Class
