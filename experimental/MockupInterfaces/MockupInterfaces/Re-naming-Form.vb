Public Class Form1

 

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the '_data_naming_conventions_beta_v0_8DataSet.datanaming_clause_geoextent' table. You can move, or remove it, as needed.
        Me.Datanaming_clause_geoextentTableAdapter.Fill(Me._data_naming_conventions_beta_v0_8DataSet.datanaming_clause_geoextent)
        'TODO: This line of code loads data into the '_data_naming_conventions_beta_v0_8DataSet.datanaming_clause_geoextent' table. You can move, or remove it, as needed.
        Me.Datanaming_clause_geoextentTableAdapter.Fill(Me._data_naming_conventions_beta_v0_8DataSet.datanaming_clause_geoextent)

    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub


    Private Sub MaskedTextBox1_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs)

    End Sub

 
End Class
