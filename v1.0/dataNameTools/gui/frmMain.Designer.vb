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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.btnStartNameChk = New System.Windows.Forms.Button
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.pnlTop = New System.Windows.Forms.Panel
        Me.dListSelectorPnl = New mapaction.datanames.gui.DataListSelectorPanel
        Me.pnlMiddle = New System.Windows.Forms.Panel
        Me.ToolStripContainer1 = New System.Windows.Forms.ToolStripContainer
        Me.m_statusStrip = New System.Windows.Forms.StatusStrip
        Me.m_tssLabel = New System.Windows.Forms.ToolStripStatusLabel
        Me.m_tsProgBar = New System.Windows.Forms.ToolStripProgressBar
        Me.m_tssProgLbl = New System.Windows.Forms.ToolStripStatusLabel
        Me.m_tmrErrorDisplay = New System.Windows.Forms.Timer(Me.components)
        Me.dNamesGridView = New mapaction.datanames.gui.DataNamesGridView
        Me.TableLayoutPanel1.SuspendLayout()
        Me.pnlTop.SuspendLayout()
        Me.ToolStripContainer1.ContentPanel.SuspendLayout()
        Me.ToolStripContainer1.SuspendLayout()
        Me.m_statusStrip.SuspendLayout()
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
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(659, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(100, 170)
        Me.TableLayoutPanel1.TabIndex = 5
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
        Me.dListSelectorPnl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dListSelectorPnl.Location = New System.Drawing.Point(0, 0)
        Me.dListSelectorPnl.MaximumSize = New System.Drawing.Size(1000, 170)
        Me.dListSelectorPnl.MinimumSize = New System.Drawing.Size(450, 170)
        Me.dListSelectorPnl.Name = "dListSelectorPnl"
        Me.dListSelectorPnl.Size = New System.Drawing.Size(659, 170)
        Me.dListSelectorPnl.TabIndex = 6
        '
        'pnlMiddle
        '
        Me.pnlMiddle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMiddle.Location = New System.Drawing.Point(0, 170)
        Me.pnlMiddle.Name = "pnlMiddle"
        Me.pnlMiddle.Size = New System.Drawing.Size(759, 331)
        Me.pnlMiddle.TabIndex = 7
        '
        'ToolStripContainer1
        '
        '
        'ToolStripContainer1.ContentPanel
        '
        Me.ToolStripContainer1.ContentPanel.Controls.Add(Me.m_statusStrip)
        Me.ToolStripContainer1.ContentPanel.Size = New System.Drawing.Size(759, 33)
        Me.ToolStripContainer1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ToolStripContainer1.LeftToolStripPanelVisible = False
        Me.ToolStripContainer1.Location = New System.Drawing.Point(0, 501)
        Me.ToolStripContainer1.Name = "ToolStripContainer1"
        Me.ToolStripContainer1.RightToolStripPanelVisible = False
        Me.ToolStripContainer1.Size = New System.Drawing.Size(759, 33)
        Me.ToolStripContainer1.TabIndex = 0
        Me.ToolStripContainer1.Text = "ToolStripContainer1"
        Me.ToolStripContainer1.TopToolStripPanelVisible = False
        '
        'm_statusStrip
        '
        Me.m_statusStrip.Dock = System.Windows.Forms.DockStyle.Fill
        Me.m_statusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.m_tssLabel, Me.m_tsProgBar, Me.m_tssProgLbl})
        Me.m_statusStrip.Location = New System.Drawing.Point(0, 0)
        Me.m_statusStrip.Name = "m_statusStrip"
        Me.m_statusStrip.Size = New System.Drawing.Size(759, 33)
        Me.m_statusStrip.TabIndex = 0
        Me.m_statusStrip.Text = "StatusStrip1"
        '
        'm_tssLabel
        '
        Me.m_tssLabel.Name = "m_tssLabel"
        Me.m_tssLabel.Size = New System.Drawing.Size(99, 28)
        Me.m_tssLabel.Text = "Data name checker"
        Me.m_tssLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'm_tsProgBar
        '
        Me.m_tsProgBar.Name = "m_tsProgBar"
        Me.m_tsProgBar.Size = New System.Drawing.Size(200, 27)
        Me.m_tsProgBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.m_tsProgBar.Tag = ""
        Me.m_tsProgBar.Visible = False
        '
        'm_tssProgLbl
        '
        Me.m_tssProgLbl.Name = "m_tssProgLbl"
        Me.m_tssProgLbl.Size = New System.Drawing.Size(74, 28)
        Me.m_tssProgLbl.Text = "progress label"
        Me.m_tssProgLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.m_tssProgLbl.Visible = False
        '
        'm_tmrErrorDisplay
        '
        Me.m_tmrErrorDisplay.Interval = 5000
        '
        'dNamesGridView
        '
        Me.dNamesGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dNamesGridView.Location = New System.Drawing.Point(0, 170)
        Me.dNamesGridView.Name = "dNamesGridView"
        Me.dNamesGridView.Size = New System.Drawing.Size(759, 331)
        Me.dNamesGridView.TabIndex = 2
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(759, 534)
        Me.Controls.Add(Me.dNamesGridView)
        Me.Controls.Add(Me.pnlMiddle)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.ToolStripContainer1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMain"
        Me.Text = "Data Name Checker"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.pnlTop.ResumeLayout(False)
        Me.ToolStripContainer1.ContentPanel.ResumeLayout(False)
        Me.ToolStripContainer1.ContentPanel.PerformLayout()
        Me.ToolStripContainer1.ResumeLayout(False)
        Me.ToolStripContainer1.PerformLayout()
        Me.m_statusStrip.ResumeLayout(False)
        Me.m_statusStrip.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents dNamesGridView As mapaction.datanames.gui.DataNamesGridView
    Private WithEvents btnStartNameChk As System.Windows.Forms.Button
    Private WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Private WithEvents pnlTop As System.Windows.Forms.Panel
    Private WithEvents pnlMiddle As System.Windows.Forms.Panel
    Friend WithEvents dListSelectorPnl As mapaction.datanames.gui.DataListSelectorPanel
    Friend WithEvents ToolStripContainer1 As System.Windows.Forms.ToolStripContainer
    Friend WithEvents m_statusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents m_tmrErrorDisplay As System.Windows.Forms.Timer
    Friend WithEvents m_tssLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents m_tsProgBar As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents m_tssProgLbl As System.Windows.Forms.ToolStripStatusLabel

End Class
