<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class testForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ClauseSelector1 = New mapaction.datanames.gui.ClauseSelector
        Me.SuspendLayout()
        '
        'ClauseSelector1
        '
        Me.ClauseSelector1.BackColor = System.Drawing.Color.Maroon
        Me.ClauseSelector1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ClauseSelector1.Location = New System.Drawing.Point(0, 0)
        Me.ClauseSelector1.Name = "ClauseSelector1"
        Me.ClauseSelector1.Size = New System.Drawing.Size(292, 273)
        Me.ClauseSelector1.TabIndex = 0
        '
        'testForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(292, 273)
        Me.Controls.Add(Me.ClauseSelector1)
        Me.Name = "testForm"
        Me.Text = "testForm"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ClauseSelector1 As mapaction.datanames.gui.ClauseSelector
End Class
