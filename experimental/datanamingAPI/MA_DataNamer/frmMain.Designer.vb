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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.lblFilePath = New System.Windows.Forms.Label
        Me.btnProcess = New System.Windows.Forms.Button
        Me.rdbSelectCurMap = New System.Windows.Forms.RadioButton
        Me.rdbSelectGDB = New System.Windows.Forms.RadioButton
        Me.pgbProgressBar = New System.Windows.Forms.ProgressBar
        Me.btnClose = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.ckBxRecurse = New System.Windows.Forms.CheckBox
        Me.rdbMiscFiles = New System.Windows.Forms.RadioButton
        Me.rdbSelectMXD = New System.Windows.Forms.RadioButton
        Me.txtWorkingDirectory = New System.Windows.Forms.TextBox
        Me.btnBrowse = New System.Windows.Forms.Button
        Me.lblProgress = New System.Windows.Forms.Label
        Me.datGV_NameList = New System.Windows.Forms.DataGridView
        Me.clmFileName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.clmStatus = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.clmWarning = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.clmError = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.clmStatusIcon = New System.Windows.Forms.DataGridViewImageColumn
        Me.clmComments = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.clmRename = New System.Windows.Forms.DataGridViewButtonColumn
        Me.OpenFileDialog = New System.Windows.Forms.OpenFileDialog
        Me.timerCheckForMXD = New System.Windows.Forms.Timer(Me.components)
        Me.lblDataStatus = New System.Windows.Forms.Label
        Me.txtBxDNLookupPath = New System.Windows.Forms.TextBox
        Me.ckBxOverrideLookupDB = New System.Windows.Forms.CheckBox
        Me.btnBrowseLookup = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.DataGridViewImageColumn1 = New System.Windows.Forms.DataGridViewImageColumn
        Me.imgLatestDataLoaded = New System.Windows.Forms.PictureBox
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.GroupBox1.SuspendLayout()
        CType(Me.datGV_NameList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.imgLatestDataLoaded, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblFilePath
        '
        Me.lblFilePath.AutoSize = True
        Me.lblFilePath.Location = New System.Drawing.Point(6, 41)
        Me.lblFilePath.Name = "lblFilePath"
        Me.lblFilePath.Size = New System.Drawing.Size(47, 13)
        Me.lblFilePath.TabIndex = 19
        Me.lblFilePath.Text = "File path"
        '
        'btnProcess
        '
        Me.btnProcess.BackColor = System.Drawing.SystemColors.Control
        Me.btnProcess.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.btnProcess.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btnProcess.Enabled = False
        Me.btnProcess.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnProcess.Location = New System.Drawing.Point(633, 146)
        Me.btnProcess.Margin = New System.Windows.Forms.Padding(10)
        Me.btnProcess.Name = "btnProcess"
        Me.btnProcess.Size = New System.Drawing.Size(80, 23)
        Me.btnProcess.TabIndex = 14
        Me.btnProcess.Text = "PROCESS"
        Me.btnProcess.UseVisualStyleBackColor = False
        '
        'rdbSelectCurMap
        '
        Me.rdbSelectCurMap.AutoSize = True
        Me.rdbSelectCurMap.Enabled = False
        Me.rdbSelectCurMap.Location = New System.Drawing.Point(9, 19)
        Me.rdbSelectCurMap.Name = "rdbSelectCurMap"
        Me.rdbSelectCurMap.Size = New System.Drawing.Size(83, 17)
        Me.rdbSelectCurMap.TabIndex = 3
        Me.rdbSelectCurMap.TabStop = True
        Me.rdbSelectCurMap.Text = "Current Map"
        Me.rdbSelectCurMap.UseVisualStyleBackColor = True
        '
        'rdbSelectGDB
        '
        Me.rdbSelectGDB.AutoSize = True
        Me.rdbSelectGDB.Location = New System.Drawing.Point(193, 19)
        Me.rdbSelectGDB.Name = "rdbSelectGDB"
        Me.rdbSelectGDB.Size = New System.Drawing.Size(91, 17)
        Me.rdbSelectGDB.TabIndex = 5
        Me.rdbSelectGDB.TabStop = True
        Me.rdbSelectGDB.Text = "GeoDatabase"
        Me.rdbSelectGDB.UseVisualStyleBackColor = True
        '
        'pgbProgressBar
        '
        Me.pgbProgressBar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pgbProgressBar.Location = New System.Drawing.Point(57, 3)
        Me.pgbProgressBar.Name = "pgbProgressBar"
        Me.pgbProgressBar.Size = New System.Drawing.Size(297, 13)
        Me.pgbProgressBar.TabIndex = 18
        '
        'btnClose
        '
        Me.btnClose.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnClose.Location = New System.Drawing.Point(633, 10)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(10)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(80, 23)
        Me.btnClose.TabIndex = 13
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ckBxRecurse)
        Me.GroupBox1.Controls.Add(Me.rdbMiscFiles)
        Me.GroupBox1.Controls.Add(Me.rdbSelectCurMap)
        Me.GroupBox1.Controls.Add(Me.rdbSelectGDB)
        Me.GroupBox1.Controls.Add(Me.rdbSelectMXD)
        Me.GroupBox1.Controls.Add(Me.txtWorkingDirectory)
        Me.GroupBox1.Controls.Add(Me.btnBrowse)
        Me.GroupBox1.Controls.Add(Me.lblFilePath)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(617, 83)
        Me.GroupBox1.TabIndex = 15
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Select Source for Layers"
        '
        'ckBxRecurse
        '
        Me.ckBxRecurse.AutoSize = True
        Me.ckBxRecurse.Checked = True
        Me.ckBxRecurse.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ckBxRecurse.Location = New System.Drawing.Point(521, 19)
        Me.ckBxRecurse.Name = "ckBxRecurse"
        Me.ckBxRecurse.Size = New System.Drawing.Size(66, 17)
        Me.ckBxRecurse.TabIndex = 20
        Me.ckBxRecurse.Text = "Recurse"
        Me.ckBxRecurse.UseVisualStyleBackColor = True
        '
        'rdbMiscFiles
        '
        Me.rdbMiscFiles.AutoSize = True
        Me.rdbMiscFiles.Checked = True
        Me.rdbMiscFiles.Location = New System.Drawing.Point(306, 19)
        Me.rdbMiscFiles.Name = "rdbMiscFiles"
        Me.rdbMiscFiles.Size = New System.Drawing.Size(67, 17)
        Me.rdbMiscFiles.TabIndex = 6
        Me.rdbMiscFiles.TabStop = True
        Me.rdbMiscFiles.Text = "Directory"
        Me.rdbMiscFiles.UseVisualStyleBackColor = True
        '
        'rdbSelectMXD
        '
        Me.rdbSelectMXD.AutoSize = True
        Me.rdbSelectMXD.Location = New System.Drawing.Point(121, 19)
        Me.rdbSelectMXD.Name = "rdbSelectMXD"
        Me.rdbSelectMXD.Size = New System.Drawing.Size(49, 17)
        Me.rdbSelectMXD.TabIndex = 4
        Me.rdbSelectMXD.TabStop = True
        Me.rdbSelectMXD.Text = "MXD"
        Me.rdbSelectMXD.UseVisualStyleBackColor = True
        '
        'txtWorkingDirectory
        '
        Me.txtWorkingDirectory.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtWorkingDirectory.Location = New System.Drawing.Point(9, 57)
        Me.txtWorkingDirectory.Name = "txtWorkingDirectory"
        Me.txtWorkingDirectory.ReadOnly = True
        Me.txtWorkingDirectory.Size = New System.Drawing.Size(497, 20)
        Me.txtWorkingDirectory.TabIndex = 17
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(521, 55)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(75, 23)
        Me.btnBrowse.TabIndex = 16
        Me.btnBrowse.Text = "Browse..."
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'lblProgress
        '
        Me.lblProgress.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblProgress.AutoSize = True
        Me.lblProgress.Location = New System.Drawing.Point(3, 6)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(48, 13)
        Me.lblProgress.TabIndex = 20
        Me.lblProgress.Text = "Progress"
        '
        'datGV_NameList
        '
        Me.datGV_NameList.AllowUserToAddRows = False
        Me.datGV_NameList.AllowUserToDeleteRows = False
        Me.datGV_NameList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.datGV_NameList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.datGV_NameList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.clmFileName, Me.clmStatus, Me.clmWarning, Me.clmError, Me.clmStatusIcon, Me.clmComments, Me.clmRename})
        Me.datGV_NameList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.datGV_NameList.Location = New System.Drawing.Point(0, 0)
        Me.datGV_NameList.Name = "datGV_NameList"
        Me.datGV_NameList.ReadOnly = True
        Me.datGV_NameList.RowHeadersVisible = False
        Me.datGV_NameList.Size = New System.Drawing.Size(723, 122)
        Me.datGV_NameList.TabIndex = 21
        '
        'clmFileName
        '
        Me.clmFileName.FillWeight = 250.0!
        Me.clmFileName.HeaderText = "File Name"
        Me.clmFileName.Name = "clmFileName"
        Me.clmFileName.ReadOnly = True
        Me.clmFileName.ToolTipText = "The file name"
        Me.clmFileName.Width = 250
        '
        'clmStatus
        '
        Me.clmStatus.FillWeight = 25.0!
        Me.clmStatus.HeaderText = "V"
        Me.clmStatus.Name = "clmStatus"
        Me.clmStatus.ReadOnly = True
        Me.clmStatus.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.clmStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.clmStatus.ToolTipText = "Valid: This is a valid file name"
        Me.clmStatus.Visible = False
        Me.clmStatus.Width = 25
        '
        'clmWarning
        '
        Me.clmWarning.FillWeight = 25.0!
        Me.clmWarning.HeaderText = "W"
        Me.clmWarning.Name = "clmWarning"
        Me.clmWarning.ReadOnly = True
        Me.clmWarning.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.clmWarning.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.clmWarning.ToolTipText = "Warning: This is not the correct file name format"
        Me.clmWarning.Visible = False
        Me.clmWarning.Width = 25
        '
        'clmError
        '
        Me.clmError.FillWeight = 25.0!
        Me.clmError.HeaderText = "E"
        Me.clmError.Name = "clmError"
        Me.clmError.ReadOnly = True
        Me.clmError.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.clmError.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.clmError.ToolTipText = "Error: This is not correct and needs amending"
        Me.clmError.Visible = False
        Me.clmError.Width = 25
        '
        'clmStatusIcon
        '
        Me.clmStatusIcon.FillWeight = 50.0!
        Me.clmStatusIcon.HeaderText = "Status"
        Me.clmStatusIcon.Image = CType(resources.GetObject("clmStatusIcon.Image"), System.Drawing.Image)
        Me.clmStatusIcon.Name = "clmStatusIcon"
        Me.clmStatusIcon.ReadOnly = True
        Me.clmStatusIcon.ToolTipText = "File status: Valid, Warning, Error"
        Me.clmStatusIcon.Width = 50
        '
        'clmComments
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.clmComments.DefaultCellStyle = DataGridViewCellStyle3
        Me.clmComments.FillWeight = 350.0!
        Me.clmComments.HeaderText = "Comments, Warnings and Errors"
        Me.clmComments.MinimumWidth = 250
        Me.clmComments.Name = "clmComments"
        Me.clmComments.ReadOnly = True
        Me.clmComments.Width = 350
        '
        'clmRename
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.clmRename.DefaultCellStyle = DataGridViewCellStyle4
        Me.clmRename.DividerWidth = 10
        Me.clmRename.FillWeight = 75.0!
        Me.clmRename.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.clmRename.HeaderText = ""
        Me.clmRename.Name = "clmRename"
        Me.clmRename.ReadOnly = True
        Me.clmRename.Text = "Rename.."
        Me.clmRename.ToolTipText = "Press to rename the file"
        Me.clmRename.UseColumnTextForButtonValue = True
        Me.clmRename.Width = 75
        '
        'OpenFileDialog
        '
        Me.OpenFileDialog.Title = "Hello World"
        '
        'timerCheckForMXD
        '
        Me.timerCheckForMXD.Interval = 100000
        '
        'lblDataStatus
        '
        Me.lblDataStatus.AutoSize = True
        Me.lblDataStatus.Location = New System.Drawing.Point(662, 93)
        Me.lblDataStatus.Name = "lblDataStatus"
        Me.lblDataStatus.Size = New System.Drawing.Size(85, 13)
        Me.lblDataStatus.TabIndex = 23
        Me.lblDataStatus.Text = "Current data fine"
        Me.lblDataStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtBxDNLookupPath
        '
        Me.txtBxDNLookupPath.Enabled = False
        Me.txtBxDNLookupPath.Location = New System.Drawing.Point(9, 42)
        Me.txtBxDNLookupPath.Name = "txtBxDNLookupPath"
        Me.txtBxDNLookupPath.ReadOnly = True
        Me.txtBxDNLookupPath.Size = New System.Drawing.Size(497, 20)
        Me.txtBxDNLookupPath.TabIndex = 24
        '
        'ckBxOverrideLookupDB
        '
        Me.ckBxOverrideLookupDB.AutoSize = True
        Me.ckBxOverrideLookupDB.Location = New System.Drawing.Point(17, 19)
        Me.ckBxOverrideLookupDB.Name = "ckBxOverrideLookupDB"
        Me.ckBxOverrideLookupDB.Size = New System.Drawing.Size(72, 17)
        Me.ckBxOverrideLookupDB.TabIndex = 26
        Me.ckBxOverrideLookupDB.Text = "(Override)"
        Me.ckBxOverrideLookupDB.UseVisualStyleBackColor = True
        '
        'btnBrowseLookup
        '
        Me.btnBrowseLookup.Enabled = False
        Me.btnBrowseLookup.Location = New System.Drawing.Point(521, 42)
        Me.btnBrowseLookup.Name = "btnBrowseLookup"
        Me.btnBrowseLookup.Size = New System.Drawing.Size(75, 23)
        Me.btnBrowseLookup.TabIndex = 27
        Me.btnBrowseLookup.Text = "Browse..."
        Me.btnBrowseLookup.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtBxDNLookupPath)
        Me.GroupBox2.Controls.Add(Me.ckBxOverrideLookupDB)
        Me.GroupBox2.Controls.Add(Me.btnBrowseLookup)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(3, 92)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(617, 84)
        Me.GroupBox2.TabIndex = 28
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Data Name Clause Lookup"
        '
        'DataGridViewImageColumn1
        '
        Me.DataGridViewImageColumn1.HeaderText = "Status"
        Me.DataGridViewImageColumn1.Image = Global.MA_DataNamer.My.Resources.Resources.Yes
        Me.DataGridViewImageColumn1.Name = "DataGridViewImageColumn1"
        Me.DataGridViewImageColumn1.ReadOnly = True
        Me.DataGridViewImageColumn1.ToolTipText = "File status: Valid, Warning, Error"
        '
        'imgLatestDataLoaded
        '
        Me.imgLatestDataLoaded.Dock = System.Windows.Forms.DockStyle.Fill
        Me.imgLatestDataLoaded.ErrorImage = CType(resources.GetObject("imgLatestDataLoaded.ErrorImage"), System.Drawing.Image)
        Me.imgLatestDataLoaded.Image = Global.MA_DataNamer.My.Resources.Resources.TrafficLightRed2
        Me.imgLatestDataLoaded.InitialImage = CType(resources.GetObject("imgLatestDataLoaded.InitialImage"), System.Drawing.Image)
        Me.imgLatestDataLoaded.Location = New System.Drawing.Point(626, 3)
        Me.imgLatestDataLoaded.Name = "imgLatestDataLoaded"
        Me.imgLatestDataLoaded.Size = New System.Drawing.Size(94, 83)
        Me.imgLatestDataLoaded.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.imgLatestDataLoaded.TabIndex = 22
        Me.imgLatestDataLoaded.TabStop = False
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox2, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.imgLatestDataLoaded, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.btnProcess, 1, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(723, 179)
        Me.TableLayoutPanel1.TabIndex = 29
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.lblProgress)
        Me.FlowLayoutPanel1.Controls.Add(Me.pgbProgressBar)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(3, 3)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(617, 40)
        Me.FlowLayoutPanel1.TabIndex = 30
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.btnClose, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.FlowLayoutPanel1, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 301)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(723, 46)
        Me.TableLayoutPanel2.TabIndex = 31
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.datGV_NameList)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 179)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(723, 122)
        Me.Panel1.TabIndex = 32
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(723, 347)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.lblDataStatus)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMain"
        Me.Text = "MapAction Data Namer"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.datGV_NameList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.imgLatestDataLoaded, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.FlowLayoutPanel1.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblFilePath As System.Windows.Forms.Label
    Friend WithEvents btnProcess As System.Windows.Forms.Button
    Friend WithEvents rdbSelectCurMap As System.Windows.Forms.RadioButton
    Friend WithEvents rdbSelectGDB As System.Windows.Forms.RadioButton
    Friend WithEvents pgbProgressBar As System.Windows.Forms.ProgressBar
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rdbSelectMXD As System.Windows.Forms.RadioButton
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents txtWorkingDirectory As System.Windows.Forms.TextBox
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents datGV_NameList As System.Windows.Forms.DataGridView
    Friend WithEvents OpenFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents timerCheckForMXD As System.Windows.Forms.Timer
    Friend WithEvents DataGridViewImageColumn1 As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents imgLatestDataLoaded As System.Windows.Forms.PictureBox
    Friend WithEvents lblDataStatus As System.Windows.Forms.Label
    Friend WithEvents txtBxDNLookupPath As System.Windows.Forms.TextBox
    Friend WithEvents ckBxOverrideLookupDB As System.Windows.Forms.CheckBox
    Friend WithEvents btnBrowseLookup As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents rdbMiscFiles As System.Windows.Forms.RadioButton
    Friend WithEvents ckBxRecurse As System.Windows.Forms.CheckBox
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents clmFileName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents clmStatus As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents clmWarning As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents clmError As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents clmStatusIcon As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents clmComments As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents clmRename As System.Windows.Forms.DataGridViewButtonColumn

End Class
