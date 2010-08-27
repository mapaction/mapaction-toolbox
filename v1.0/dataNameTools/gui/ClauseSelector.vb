Public Class ClauseSelector

    'todo: move this to use ToolStripDropDown class

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        setMode(False)
    End Sub

    Private Sub setMode(ByRef isMultiSelect As Boolean)
        If isMultiSelect Then
            dgv.MultiSelect = True
        Else
            dgv.MultiSelect = False
        End If

        dgv.Columns.Item(0).DefaultCellStyle.BackColor = Color.LightSeaGreen
        'dgv.Columns.Item(0).DefaultCellStyle.BackColor = Color.LightSeaGreen
    End Sub


End Class
