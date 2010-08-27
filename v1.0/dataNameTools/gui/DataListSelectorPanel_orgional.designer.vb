<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DataListSelectorPanel_orgional
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
        Me.grpBxDataList = New System.Windows.Forms.GroupBox
        Me.tblPnlDataList = New System.Windows.Forms.TableLayoutPanel
        Me.flwPnlListType = New System.Windows.Forms.FlowLayoutPanel
        Me.radBtnCurMxDoc = New System.Windows.Forms.RadioButton
        Me.radBtnMXD = New System.Windows.Forms.RadioButton
        Me.radBtnGDB = New System.Windows.Forms.RadioButton
        Me.radBtnDirectory = New System.Windows.Forms.RadioButton
        Me.chkBxRecurse = New System.Windows.Forms.CheckBox
        Me.btnBrowseDataList = New System.Windows.Forms.Button
        Me.txtBoxDataList = New System.Windows.Forms.TextBox
        Me.tblPnlOverall = New System.Windows.Forms.TableLayoutPanel
        Me.grpBoxDNCLT = New System.Windows.Forms.GroupBox
        Me.tblPnlDNCLTall = New System.Windows.Forms.TableLayoutPanel
        Me.tblPnlDNCLTupper = New System.Windows.Forms.TableLayoutPanel
        Me.chkBxOverrideDNCLT = New System.Windows.Forms.CheckBox
        Me.flwPlnDCNLtype = New System.Windows.Forms.FlowLayoutPanel
        Me.radBtnDNCL_GDB = New System.Windows.Forms.RadioButton
        Me.radBtnDCNL_MDB = New System.Windows.Forms.RadioButton
        Me.tblPnlDNCLTlower = New System.Windows.Forms.TableLayoutPanel
        Me.btnBrowseDNCLT = New System.Windows.Forms.Button
        Me.txtBoxDNCL = New System.Windows.Forms.TextBox
        Me.grpBxDataList.SuspendLayout()
        Me.tblPnlDataList.SuspendLayout()
        Me.flwPnlListType.SuspendLayout()
        Me.tblPnlOverall.SuspendLayout()
        Me.grpBoxDNCLT.SuspendLayout()
        Me.tblPnlDNCLTall.SuspendLayout()
        Me.tblPnlDNCLTupper.SuspendLayout()
        Me.flwPlnDCNLtype.SuspendLayout()
        Me.tblPnlDNCLTlower.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpBxDataList
        '
        Me.grpBxDataList.Controls.Add(Me.tblPnlDataList)
        Me.grpBxDataList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpBxDataList.Location = New System.Drawing.Point(3, 3)
        Me.grpBxDataList.Name = "grpBxDataList"
        Me.grpBxDataList.Size = New System.Drawing.Size(444, 79)
        Me.grpBxDataList.TabIndex = 0
        Me.grpBxDataList.TabStop = False
        Me.grpBxDataList.Text = "Data Sources"
        '
        'tblPnlDataList
        '
        Me.tblPnlDataList.ColumnCount = 2
        Me.tblPnlDataList.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblPnlDataList.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115.0!))
        Me.tblPnlDataList.Controls.Add(Me.flwPnlListType, 0, 0)
        Me.tblPnlDataList.Controls.Add(Me.chkBxRecurse, 1, 0)
        Me.tblPnlDataList.Controls.Add(Me.btnBrowseDataList, 1, 1)
        Me.tblPnlDataList.Controls.Add(Me.txtBoxDataList, 0, 1)
        Me.tblPnlDataList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblPnlDataList.Location = New System.Drawing.Point(3, 16)
        Me.tblPnlDataList.Name = "tblPnlDataList"
        Me.tblPnlDataList.RowCount = 2
        Me.tblPnlDataList.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23.0!))
        Me.tblPnlDataList.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tblPnlDataList.Size = New System.Drawing.Size(438, 60)
        Me.tblPnlDataList.TabIndex = 0
        '
        'flwPnlListType
        '
        Me.flwPnlListType.Controls.Add(Me.radBtnCurMxDoc)
        Me.flwPnlListType.Controls.Add(Me.radBtnMXD)
        Me.flwPnlListType.Controls.Add(Me.radBtnGDB)
        Me.flwPnlListType.Controls.Add(Me.radBtnDirectory)
        Me.flwPnlListType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flwPnlListType.Location = New System.Drawing.Point(0, 0)
        Me.flwPnlListType.Margin = New System.Windows.Forms.Padding(0)
        Me.flwPnlListType.Name = "flwPnlListType"
        Me.flwPnlListType.Size = New System.Drawing.Size(323, 23)
        Me.flwPnlListType.TabIndex = 0
        '
        'radBtnCurMxDoc
        '
        Me.radBtnCurMxDoc.AutoSize = True
        Me.radBtnCurMxDoc.Location = New System.Drawing.Point(8, 3)
        Me.radBtnCurMxDoc.Margin = New System.Windows.Forms.Padding(8, 3, 3, 3)
        Me.radBtnCurMxDoc.Name = "radBtnCurMxDoc"
        Me.radBtnCurMxDoc.Size = New System.Drawing.Size(83, 17)
        Me.radBtnCurMxDoc.TabIndex = 0
        Me.radBtnCurMxDoc.TabStop = True
        Me.radBtnCurMxDoc.Text = "Current Map"
        Me.radBtnCurMxDoc.UseVisualStyleBackColor = True
        '
        'radBtnMXD
        '
        Me.radBtnMXD.AutoSize = True
        Me.radBtnMXD.Location = New System.Drawing.Point(94, 3)
        Me.radBtnMXD.Margin = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.radBtnMXD.Name = "radBtnMXD"
        Me.radBtnMXD.Size = New System.Drawing.Size(49, 17)
        Me.radBtnMXD.TabIndex = 1
        Me.radBtnMXD.TabStop = True
        Me.radBtnMXD.Text = "MXD"
        Me.radBtnMXD.UseVisualStyleBackColor = True
        '
        'radBtnGDB
        '
        Me.radBtnGDB.AutoSize = True
        Me.radBtnGDB.Location = New System.Drawing.Point(149, 3)
        Me.radBtnGDB.Name = "radBtnGDB"
        Me.radBtnGDB.Size = New System.Drawing.Size(89, 17)
        Me.radBtnGDB.TabIndex = 3
        Me.radBtnGDB.TabStop = True
        Me.radBtnGDB.Text = "Geodatabase"
        Me.radBtnGDB.UseVisualStyleBackColor = True
        '
        'radBtnDirectory
        '
        Me.radBtnDirectory.AutoSize = True
        Me.radBtnDirectory.Location = New System.Drawing.Point(241, 3)
        Me.radBtnDirectory.Margin = New System.Windows.Forms.Padding(0, 3, 8, 3)
        Me.radBtnDirectory.Name = "radBtnDirectory"
        Me.radBtnDirectory.Size = New System.Drawing.Size(67, 17)
        Me.radBtnDirectory.TabIndex = 2
        Me.radBtnDirectory.TabStop = True
        Me.radBtnDirectory.Text = "Directory"
        Me.radBtnDirectory.UseVisualStyleBackColor = True
        '
        'chkBxRecurse
        '
        Me.chkBxRecurse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chkBxRecurse.Location = New System.Drawing.Point(331, 3)
        Me.chkBxRecurse.Margin = New System.Windows.Forms.Padding(8, 3, 8, 3)
        Me.chkBxRecurse.Name = "chkBxRecurse"
        Me.chkBxRecurse.Size = New System.Drawing.Size(99, 17)
        Me.chkBxRecurse.TabIndex = 1
        Me.chkBxRecurse.Text = "Recurse"
        Me.chkBxRecurse.UseVisualStyleBackColor = True
        '
        'btnBrowseDataList
        '
        Me.btnBrowseDataList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnBrowseDataList.Location = New System.Drawing.Point(331, 31)
        Me.btnBrowseDataList.Margin = New System.Windows.Forms.Padding(8)
        Me.btnBrowseDataList.Name = "btnBrowseDataList"
        Me.btnBrowseDataList.Size = New System.Drawing.Size(99, 21)
        Me.btnBrowseDataList.TabIndex = 2
        Me.btnBrowseDataList.Text = "Browse..."
        Me.btnBrowseDataList.UseVisualStyleBackColor = True
        '
        'txtBoxDataList
        '
        Me.txtBoxDataList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtBoxDataList.Location = New System.Drawing.Point(8, 31)
        Me.txtBoxDataList.Margin = New System.Windows.Forms.Padding(8)
        Me.txtBoxDataList.Name = "txtBoxDataList"
        Me.txtBoxDataList.Size = New System.Drawing.Size(307, 20)
        Me.txtBoxDataList.TabIndex = 3
        '
        'tblPnlOverall
        '
        Me.tblPnlOverall.ColumnCount = 1
        Me.tblPnlOverall.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblPnlOverall.Controls.Add(Me.grpBoxDNCLT, 0, 1)
        Me.tblPnlOverall.Controls.Add(Me.grpBxDataList, 0, 0)
        Me.tblPnlOverall.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblPnlOverall.Location = New System.Drawing.Point(0, 0)
        Me.tblPnlOverall.Name = "tblPnlOverall"
        Me.tblPnlOverall.RowCount = 2
        Me.tblPnlOverall.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 85.0!))
        Me.tblPnlOverall.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblPnlOverall.Size = New System.Drawing.Size(450, 170)
        Me.tblPnlOverall.TabIndex = 1
        '
        'grpBoxDNCLT
        '
        Me.grpBoxDNCLT.Controls.Add(Me.tblPnlDNCLTall)
        Me.grpBoxDNCLT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpBoxDNCLT.Enabled = False
        Me.grpBoxDNCLT.Location = New System.Drawing.Point(3, 88)
        Me.grpBoxDNCLT.Name = "grpBoxDNCLT"
        Me.grpBoxDNCLT.Size = New System.Drawing.Size(444, 79)
        Me.grpBoxDNCLT.TabIndex = 1
        Me.grpBoxDNCLT.TabStop = False
        Me.grpBoxDNCLT.Text = "Data Name Clause Lookup Tables"
        '
        'tblPnlDNCLTall
        '
        Me.tblPnlDNCLTall.ColumnCount = 1
        Me.tblPnlDNCLTall.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblPnlDNCLTall.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblPnlDNCLTall.Controls.Add(Me.tblPnlDNCLTupper, 0, 0)
        Me.tblPnlDNCLTall.Controls.Add(Me.tblPnlDNCLTlower, 0, 1)
        Me.tblPnlDNCLTall.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblPnlDNCLTall.Location = New System.Drawing.Point(3, 16)
        Me.tblPnlDNCLTall.Name = "tblPnlDNCLTall"
        Me.tblPnlDNCLTall.RowCount = 2
        Me.tblPnlDNCLTall.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23.0!))
        Me.tblPnlDNCLTall.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblPnlDNCLTall.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblPnlDNCLTall.Size = New System.Drawing.Size(438, 60)
        Me.tblPnlDNCLTall.TabIndex = 5
        '
        'tblPnlDNCLTupper
        '
        Me.tblPnlDNCLTupper.ColumnCount = 2
        Me.tblPnlDNCLTupper.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.17809!))
        Me.tblPnlDNCLTupper.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.82192!))
        Me.tblPnlDNCLTupper.Controls.Add(Me.chkBxOverrideDNCLT, 0, 0)
        Me.tblPnlDNCLTupper.Controls.Add(Me.flwPlnDCNLtype, 1, 0)
        Me.tblPnlDNCLTupper.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblPnlDNCLTupper.Location = New System.Drawing.Point(0, 0)
        Me.tblPnlDNCLTupper.Margin = New System.Windows.Forms.Padding(0)
        Me.tblPnlDNCLTupper.Name = "tblPnlDNCLTupper"
        Me.tblPnlDNCLTupper.RowCount = 1
        Me.tblPnlDNCLTupper.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblPnlDNCLTupper.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23.0!))
        Me.tblPnlDNCLTupper.Size = New System.Drawing.Size(438, 23)
        Me.tblPnlDNCLTupper.TabIndex = 0
        '
        'chkBxOverrideDNCLT
        '
        Me.chkBxOverrideDNCLT.AutoSize = True
        Me.chkBxOverrideDNCLT.Location = New System.Drawing.Point(8, 3)
        Me.chkBxOverrideDNCLT.Margin = New System.Windows.Forms.Padding(8, 3, 3, 3)
        Me.chkBxOverrideDNCLT.Name = "chkBxOverrideDNCLT"
        Me.chkBxOverrideDNCLT.Size = New System.Drawing.Size(254, 17)
        Me.chkBxOverrideDNCLT.TabIndex = 4
        Me.chkBxOverrideDNCLT.Text = "Override default data name clause lookup tables"
        Me.chkBxOverrideDNCLT.UseVisualStyleBackColor = True
        '
        'flwPlnDCNLtype
        '
        Me.flwPlnDCNLtype.Controls.Add(Me.radBtnDNCL_GDB)
        Me.flwPlnDCNLtype.Controls.Add(Me.radBtnDCNL_MDB)
        Me.flwPlnDCNLtype.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flwPlnDCNLtype.Enabled = False
        Me.flwPlnDCNLtype.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft
        Me.flwPlnDCNLtype.Location = New System.Drawing.Point(303, 0)
        Me.flwPlnDCNLtype.Margin = New System.Windows.Forms.Padding(0)
        Me.flwPlnDCNLtype.Name = "flwPlnDCNLtype"
        Me.flwPlnDCNLtype.Size = New System.Drawing.Size(135, 23)
        Me.flwPlnDCNLtype.TabIndex = 5
        '
        'radBtnDNCL_GDB
        '
        Me.radBtnDNCL_GDB.AutoSize = True
        Me.radBtnDNCL_GDB.Location = New System.Drawing.Point(84, 3)
        Me.radBtnDNCL_GDB.Name = "radBtnDNCL_GDB"
        Me.radBtnDNCL_GDB.Size = New System.Drawing.Size(48, 17)
        Me.radBtnDNCL_GDB.TabIndex = 0
        Me.radBtnDNCL_GDB.TabStop = True
        Me.radBtnDNCL_GDB.Text = "GDB"
        Me.radBtnDNCL_GDB.UseVisualStyleBackColor = True
        '
        'radBtnDCNL_MDB
        '
        Me.radBtnDCNL_MDB.AutoSize = True
        Me.radBtnDCNL_MDB.Location = New System.Drawing.Point(18, 3)
        Me.radBtnDCNL_MDB.Name = "radBtnDCNL_MDB"
        Me.radBtnDCNL_MDB.Size = New System.Drawing.Size(60, 17)
        Me.radBtnDCNL_MDB.TabIndex = 1
        Me.radBtnDCNL_MDB.TabStop = True
        Me.radBtnDCNL_MDB.Text = "Access"
        Me.radBtnDCNL_MDB.UseVisualStyleBackColor = True
        '
        'tblPnlDNCLTlower
        '
        Me.tblPnlDNCLTlower.ColumnCount = 2
        Me.tblPnlDNCLTlower.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblPnlDNCLTlower.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115.0!))
        Me.tblPnlDNCLTlower.Controls.Add(Me.btnBrowseDNCLT, 1, 0)
        Me.tblPnlDNCLTlower.Controls.Add(Me.txtBoxDNCL, 0, 0)
        Me.tblPnlDNCLTlower.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblPnlDNCLTlower.Location = New System.Drawing.Point(0, 23)
        Me.tblPnlDNCLTlower.Margin = New System.Windows.Forms.Padding(0)
        Me.tblPnlDNCLTlower.Name = "tblPnlDNCLTlower"
        Me.tblPnlDNCLTlower.RowCount = 1
        Me.tblPnlDNCLTlower.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tblPnlDNCLTlower.Size = New System.Drawing.Size(438, 37)
        Me.tblPnlDNCLTlower.TabIndex = 0
        '
        'btnBrowseDNCLT
        '
        Me.btnBrowseDNCLT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnBrowseDNCLT.Location = New System.Drawing.Point(331, 8)
        Me.btnBrowseDNCLT.Margin = New System.Windows.Forms.Padding(8)
        Me.btnBrowseDNCLT.Name = "btnBrowseDNCLT"
        Me.btnBrowseDNCLT.Size = New System.Drawing.Size(99, 21)
        Me.btnBrowseDNCLT.TabIndex = 2
        Me.btnBrowseDNCLT.Text = "Browse..."
        Me.btnBrowseDNCLT.UseVisualStyleBackColor = True
        '
        'txtBoxDNCL
        '
        Me.txtBoxDNCL.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtBoxDNCL.Location = New System.Drawing.Point(8, 8)
        Me.txtBoxDNCL.Margin = New System.Windows.Forms.Padding(8)
        Me.txtBoxDNCL.Name = "txtBoxDNCL"
        Me.txtBoxDNCL.ReadOnly = True
        Me.txtBoxDNCL.Size = New System.Drawing.Size(307, 20)
        Me.txtBoxDNCL.TabIndex = 3
        '
        'DataListSelectorPanel_orgional
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.tblPnlOverall)
        Me.MaximumSize = New System.Drawing.Size(1000, 170)
        Me.MinimumSize = New System.Drawing.Size(450, 170)
        Me.Name = "DataListSelectorPanel_orgional"
        Me.Size = New System.Drawing.Size(450, 170)
        Me.grpBxDataList.ResumeLayout(False)
        Me.tblPnlDataList.ResumeLayout(False)
        Me.tblPnlDataList.PerformLayout()
        Me.flwPnlListType.ResumeLayout(False)
        Me.flwPnlListType.PerformLayout()
        Me.tblPnlOverall.ResumeLayout(False)
        Me.grpBoxDNCLT.ResumeLayout(False)
        Me.tblPnlDNCLTall.ResumeLayout(False)
        Me.tblPnlDNCLTupper.ResumeLayout(False)
        Me.tblPnlDNCLTupper.PerformLayout()
        Me.flwPlnDCNLtype.ResumeLayout(False)
        Me.flwPlnDCNLtype.PerformLayout()
        Me.tblPnlDNCLTlower.ResumeLayout(False)
        Me.tblPnlDNCLTlower.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents tblPnlOverall As System.Windows.Forms.TableLayoutPanel
    Private WithEvents grpBoxDNCLT As System.Windows.Forms.GroupBox
    Private WithEvents tblPnlDNCLTlower As System.Windows.Forms.TableLayoutPanel
    Private WithEvents btnBrowseDNCLT As System.Windows.Forms.Button
    Private WithEvents txtBoxDNCL As System.Windows.Forms.TextBox
    Private WithEvents chkBxOverrideDNCLT As System.Windows.Forms.CheckBox
    Private WithEvents radBtnGDB As System.Windows.Forms.RadioButton
    Private WithEvents grpBxDataList As System.Windows.Forms.GroupBox
    Private WithEvents tblPnlDataList As System.Windows.Forms.TableLayoutPanel
    Private WithEvents flwPnlListType As System.Windows.Forms.FlowLayoutPanel
    Private WithEvents radBtnCurMxDoc As System.Windows.Forms.RadioButton
    Private WithEvents radBtnMXD As System.Windows.Forms.RadioButton
    Private WithEvents radBtnDirectory As System.Windows.Forms.RadioButton
    Private WithEvents chkBxRecurse As System.Windows.Forms.CheckBox
    Private WithEvents btnBrowseDataList As System.Windows.Forms.Button
    Private WithEvents txtBoxDataList As System.Windows.Forms.TextBox
    Private WithEvents tblPnlDNCLTall As System.Windows.Forms.TableLayoutPanel
    Private WithEvents tblPnlDNCLTupper As System.Windows.Forms.TableLayoutPanel
    Private WithEvents flwPlnDCNLtype As System.Windows.Forms.FlowLayoutPanel
    Private WithEvents radBtnDNCL_GDB As System.Windows.Forms.RadioButton
    Private WithEvents radBtnDCNL_MDB As System.Windows.Forms.RadioButton

End Class
