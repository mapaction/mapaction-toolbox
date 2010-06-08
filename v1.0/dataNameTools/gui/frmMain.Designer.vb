<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.btnStartNameChk = New System.Windows.Forms.Button
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.picBxReadiness = New System.Windows.Forms.PictureBox
        Me.pnlTop = New System.Windows.Forms.Panel
        Me.dListSelectorPnl = New mapaction.datanames.gui.DataListSelectorPanel
        Me.dNamesGridView = New mapaction.datanames.gui.DataNamesGridView
        Me.pnlMiddle = New System.Windows.Forms.Panel
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.picBxReadiness, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTop.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnStartNameChk
        '
        Me.btnStartNameChk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnStartNameChk.Enabled = False
        Me.btnStartNameChk.Location = New System.Drawing.Point(8, 93)
        Me.btnStartNameChk.Margin = New System.Windows.Forms.Padding(8)
        Me.btnStartNameChk.Name = "btnStartNameChk"
        Me.btnStartNameChk.Size = New System.Drawing.Size(84, 69)
        Me.btnStartNameChk.TabIndex = 2
        Me.btnStartNameChk.Text = "Check Data Names"
        Me.btnStartNameChk.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.btnStartNameChk, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.picBxReadiness, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(659, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(100, 170)
        Me.TableLayoutPanel1.TabIndex = 5
        '
        'picBxReadiness
        '
        Me.picBxReadiness.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picBxReadiness.ErrorImage = Global.mapaction.datanames.gui.My.Resources.Resources.icoTrafficLightRed
        Me.picBxReadiness.Image = Global.mapaction.datanames.gui.My.Resources.Resources.icoTrafficLightRed
        Me.picBxReadiness.InitialImage = Global.mapaction.datanames.gui.My.Resources.Resources.icoTrafficLightRed
        Me.picBxReadiness.Location = New System.Drawing.Point(3, 3)
        Me.picBxReadiness.Name = "picBxReadiness"
        Me.picBxReadiness.Size = New System.Drawing.Size(94, 79)
        Me.picBxReadiness.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.picBxReadiness.TabIndex = 3
        Me.picBxReadiness.TabStop = False
        '
        'pnlTop
        '
        Me.pnlTop.Controls.Add(Me.dListSelectorPnl)
        Me.pnlTop.Controls.Add(Me.TableLayoutPanel1)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(759, 170)
        Me.pnlTop.TabIndex = 6
        '
        'dListSelectorPnl
        '
        Me.dListSelectorPnl.AutoSize = True
        Me.dListSelectorPnl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dListSelectorPnl.Location = New System.Drawing.Point(0, 0)
        Me.dListSelectorPnl.MaximumSize = New System.Drawing.Size(1000, 170)
        Me.dListSelectorPnl.MinimumSize = New System.Drawing.Size(510, 170)
        Me.dListSelectorPnl.Name = "dListSelectorPnl"
        Me.dListSelectorPnl.Size = New System.Drawing.Size(659, 170)
        Me.dListSelectorPnl.TabIndex = 0
        '
        'dNamesGridView
        '
        Me.dNamesGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dNamesGridView.Location = New System.Drawing.Point(0, 170)
        Me.dNamesGridView.Name = "dNamesGridView"
        Me.dNamesGridView.Size = New System.Drawing.Size(759, 563)
        Me.dNamesGridView.TabIndex = 2
        '
        'pnlMiddle
        '
        Me.pnlMiddle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMiddle.Location = New System.Drawing.Point(0, 170)
        Me.pnlMiddle.Name = "pnlMiddle"
        Me.pnlMiddle.Size = New System.Drawing.Size(759, 563)
        Me.pnlMiddle.TabIndex = 7
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(759, 733)
        Me.Controls.Add(Me.dNamesGridView)
        Me.Controls.Add(Me.pnlMiddle)
        Me.Controls.Add(Me.pnlTop)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMain"
        Me.Text = "Data Name Checker"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.picBxReadiness, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents dListSelectorPnl As mapaction.datanames.gui.DataListSelectorPanel
    Private WithEvents dNamesGridView As mapaction.datanames.gui.DataNamesGridView
    Private WithEvents btnStartNameChk As System.Windows.Forms.Button
    Private WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Private WithEvents pnlTop As System.Windows.Forms.Panel
    Private WithEvents pnlMiddle As System.Windows.Forms.Panel
    Private WithEvents picBxReadiness As System.Windows.Forms.PictureBox

End Class
