<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DataRenameDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DataRenameDialog))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.OK_Button = New System.Windows.Forms.Button
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.m_grpBxOldName = New System.Windows.Forms.GroupBox
        Me.m_grpBxPatternLbl = New System.Windows.Forms.GroupBox
        Me.m_LblPatternText = New System.Windows.Forms.Label
        Me.m_grpBxNewName = New System.Windows.Forms.GroupBox
        Me.m_picBxPropNameStatus = New System.Windows.Forms.PictureBox
        Me.m_lblProposedNameStatus = New System.Windows.Forms.Label
        Me.m_lblOldName = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel
        Me.m_freeTxtPnl = New mapaction.datanames.gui.FreeTextPanel
        Me.TableLayoutPanel1.SuspendLayout()
        Me.m_grpBxOldName.SuspendLayout()
        Me.m_grpBxPatternLbl.SuspendLayout()
        Me.m_grpBxNewName.SuspendLayout()
        CType(Me.m_picBxPropNameStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(556, 352)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'm_grpBxOldName
        '
        Me.m_grpBxOldName.Controls.Add(Me.m_lblOldName)
        Me.m_grpBxOldName.Dock = System.Windows.Forms.DockStyle.Top
        Me.m_grpBxOldName.Location = New System.Drawing.Point(0, 0)
        Me.m_grpBxOldName.Name = "m_grpBxOldName"
        Me.m_grpBxOldName.Size = New System.Drawing.Size(708, 44)
        Me.m_grpBxOldName.TabIndex = 5
        Me.m_grpBxOldName.TabStop = False
        Me.m_grpBxOldName.Text = "Origional Name"
        '
        'm_grpBxPatternLbl
        '
        Me.m_grpBxPatternLbl.AutoSize = True
        Me.m_grpBxPatternLbl.Controls.Add(Me.m_LblPatternText)
        Me.m_grpBxPatternLbl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.m_grpBxPatternLbl.Location = New System.Drawing.Point(0, 0)
        Me.m_grpBxPatternLbl.Name = "m_grpBxPatternLbl"
        Me.m_grpBxPatternLbl.Size = New System.Drawing.Size(708, 39)
        Me.m_grpBxPatternLbl.TabIndex = 2
        Me.m_grpBxPatternLbl.TabStop = False
        Me.m_grpBxPatternLbl.Text = "Data Naming Convention"
        '
        'm_LblPatternText
        '
        Me.m_LblPatternText.Dock = System.Windows.Forms.DockStyle.Fill
        Me.m_LblPatternText.Font = New System.Drawing.Font("Courier New", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.m_LblPatternText.ForeColor = System.Drawing.Color.Black
        Me.m_LblPatternText.Location = New System.Drawing.Point(3, 16)
        Me.m_LblPatternText.Name = "m_LblPatternText"
        Me.m_LblPatternText.Size = New System.Drawing.Size(702, 20)
        Me.m_LblPatternText.TabIndex = 0
        Me.m_LblPatternText.Text = "[geoextent]_[data-category]_[data-theme]_[geometry-type]_[scale]_[source]_[permis" & _
            "sions]" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.m_LblPatternText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'm_grpBxNewName
        '
        Me.m_grpBxNewName.Controls.Add(Me.m_freeTxtPnl)
        Me.m_grpBxNewName.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.m_grpBxNewName.Location = New System.Drawing.Point(0, 83)
        Me.m_grpBxNewName.Name = "m_grpBxNewName"
        Me.m_grpBxNewName.Size = New System.Drawing.Size(708, 73)
        Me.m_grpBxNewName.TabIndex = 1
        Me.m_grpBxNewName.TabStop = False
        Me.m_grpBxNewName.Text = "New Name"
        '
        'm_picBxPropNameStatus
        '
        Me.m_picBxPropNameStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.m_picBxPropNameStatus.Location = New System.Drawing.Point(3, 3)
        Me.m_picBxPropNameStatus.Name = "m_picBxPropNameStatus"
        Me.m_picBxPropNameStatus.Size = New System.Drawing.Size(44, 219)
        Me.m_picBxPropNameStatus.TabIndex = 0
        Me.m_picBxPropNameStatus.TabStop = False
        '
        'm_lblProposedNameStatus
        '
        Me.m_lblProposedNameStatus.AutoSize = True
        Me.m_lblProposedNameStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.m_lblProposedNameStatus.Location = New System.Drawing.Point(53, 0)
        Me.m_lblProposedNameStatus.Name = "m_lblProposedNameStatus"
        Me.m_lblProposedNameStatus.Size = New System.Drawing.Size(652, 225)
        Me.m_lblProposedNameStatus.TabIndex = 0
        Me.m_lblProposedNameStatus.Text = "Proposed Name Status"
        '
        'm_lblOldName
        '
        Me.m_lblOldName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.m_lblOldName.Font = New System.Drawing.Font("Courier New", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.m_lblOldName.Location = New System.Drawing.Point(3, 16)
        Me.m_lblOldName.Name = "m_lblOldName"
        Me.m_lblOldName.Size = New System.Drawing.Size(702, 25)
        Me.m_lblOldName.TabIndex = 0
        Me.m_lblOldName.Text = "Old Name"
        Me.m_lblOldName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.m_grpBxOldName)
        Me.Panel1.Controls.Add(Me.m_grpBxNewName)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.MaximumSize = New System.Drawing.Size(2500, 172)
        Me.Panel1.MinimumSize = New System.Drawing.Size(325, 156)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(708, 156)
        Me.Panel1.TabIndex = 8
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.m_grpBxPatternLbl)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 44)
        Me.Panel2.MaximumSize = New System.Drawing.Size(2500, 58)
        Me.Panel2.MinimumSize = New System.Drawing.Size(320, 38)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(708, 39)
        Me.Panel2.TabIndex = 9
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel3, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(714, 393)
        Me.TableLayoutPanel2.TabIndex = 9
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 2
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel3.Controls.Add(Me.m_picBxPropNameStatus, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.m_lblProposedNameStatus, 1, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 165)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(708, 225)
        Me.TableLayoutPanel3.TabIndex = 10
        '
        'm_freeTxtPnl
        '
        Me.m_freeTxtPnl.AllowPrefixedNumbers = True
        Me.m_freeTxtPnl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.m_freeTxtPnl.Location = New System.Drawing.Point(3, 16)
        Me.m_freeTxtPnl.MinimumSize = New System.Drawing.Size(100, 52)
        Me.m_freeTxtPnl.Name = "m_freeTxtPnl"
        Me.m_freeTxtPnl.Size = New System.Drawing.Size(702, 54)
        Me.m_freeTxtPnl.TabIndex = 4
        '
        'DataRenameDialog
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(714, 393)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(630, 300)
        Me.Name = "DataRenameDialog"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Rename Data..."
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.m_grpBxOldName.ResumeLayout(False)
        Me.m_grpBxPatternLbl.ResumeLayout(False)
        Me.m_grpBxNewName.ResumeLayout(False)
        CType(Me.m_picBxPropNameStatus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents m_freeTxtPnl As mapaction.datanames.gui.FreeTextPanel
    Private WithEvents m_LblPatternText As System.Windows.Forms.Label
    Private WithEvents m_grpBxPatternLbl As System.Windows.Forms.GroupBox
    Private WithEvents m_grpBxOldName As System.Windows.Forms.GroupBox
    Private WithEvents m_grpBxNewName As System.Windows.Forms.GroupBox
    Friend WithEvents m_lblProposedNameStatus As System.Windows.Forms.Label
    Friend WithEvents m_picBxPropNameStatus As System.Windows.Forms.PictureBox
    Private WithEvents m_lblOldName As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel

End Class
