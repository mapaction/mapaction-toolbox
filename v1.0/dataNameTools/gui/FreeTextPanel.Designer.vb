<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FreeTextPanel
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.m_txtBx = New System.Windows.Forms.TextBox
        Me.m_tblPnl = New System.Windows.Forms.TableLayoutPanel
        Me.m_lblWarning = New System.Windows.Forms.Label
        Me.m_tblPnl.SuspendLayout()
        Me.SuspendLayout()
        '
        'm_txtBx
        '
        Me.m_txtBx.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower
        Me.m_txtBx.Dock = System.Windows.Forms.DockStyle.Fill
        Me.m_txtBx.Location = New System.Drawing.Point(3, 3)
        Me.m_txtBx.Name = "m_txtBx"
        Me.m_txtBx.Size = New System.Drawing.Size(94, 20)
        Me.m_txtBx.TabIndex = 3
        '
        'm_tblPnl
        '
        Me.m_tblPnl.AutoSize = True
        Me.m_tblPnl.ColumnCount = 1
        Me.m_tblPnl.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.m_tblPnl.Controls.Add(Me.m_txtBx, 0, 0)
        Me.m_tblPnl.Controls.Add(Me.m_lblWarning, 0, 1)
        Me.m_tblPnl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.m_tblPnl.Location = New System.Drawing.Point(0, 0)
        Me.m_tblPnl.Name = "m_tblPnl"
        Me.m_tblPnl.RowCount = 2
        Me.m_tblPnl.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.m_tblPnl.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.m_tblPnl.Size = New System.Drawing.Size(100, 52)
        Me.m_tblPnl.TabIndex = 4
        '
        'm_lblWarning
        '
        Me.m_lblWarning.AutoSize = True
        Me.m_lblWarning.Dock = System.Windows.Forms.DockStyle.Fill
        Me.m_lblWarning.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.m_lblWarning.ForeColor = System.Drawing.Color.Red
        Me.m_lblWarning.Location = New System.Drawing.Point(3, 29)
        Me.m_lblWarning.Margin = New System.Windows.Forms.Padding(3)
        Me.m_lblWarning.Name = "m_lblWarning"
        Me.m_lblWarning.Size = New System.Drawing.Size(94, 20)
        Me.m_lblWarning.TabIndex = 4
        Me.m_lblWarning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FreeTextPanel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.m_tblPnl)
        Me.MinimumSize = New System.Drawing.Size(100, 52)
        Me.Name = "FreeTextPanel"
        Me.Size = New System.Drawing.Size(100, 52)
        Me.m_tblPnl.ResumeLayout(False)
        Me.m_tblPnl.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents m_lblWarning As System.Windows.Forms.Label
    Private WithEvents m_txtBx As System.Windows.Forms.TextBox
    Private WithEvents m_tblPnl As System.Windows.Forms.TableLayoutPanel

End Class
