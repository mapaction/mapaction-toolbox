<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DataNamesGridView
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.datGV = New System.Windows.Forms.DataGridView
        Me.clmDataName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.clmStatusIcon = New System.Windows.Forms.DataGridViewImageColumn
        Me.clmComments = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.clmPath = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.datGV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'datGV
        '
        Me.datGV.AllowUserToAddRows = False
        Me.datGV.AllowUserToDeleteRows = False
        Me.datGV.AllowUserToOrderColumns = True
        Me.datGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.datGV.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.datGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.datGV.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.clmDataName, Me.clmStatusIcon, Me.clmComments, Me.clmPath})
        Me.datGV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.datGV.Location = New System.Drawing.Point(0, 0)
        Me.datGV.Name = "datGV"
        Me.datGV.ReadOnly = True
        Me.datGV.Size = New System.Drawing.Size(795, 335)
        Me.datGV.TabIndex = 0
        '
        'clmDataName
        '
        Me.clmDataName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.clmDataName.FillWeight = 250.0!
        Me.clmDataName.HeaderText = "Data Name"
        Me.clmDataName.Name = "clmDataName"
        Me.clmDataName.ReadOnly = True
        Me.clmDataName.ToolTipText = "The name of the file or feature class"
        Me.clmDataName.Width = 86
        '
        'clmStatusIcon
        '
        Me.clmStatusIcon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.clmStatusIcon.FillWeight = 50.0!
        Me.clmStatusIcon.HeaderText = "Status"
        Me.clmStatusIcon.Name = "clmStatusIcon"
        Me.clmStatusIcon.ReadOnly = True
        Me.clmStatusIcon.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.clmStatusIcon.Width = 43
        '
        'clmComments
        '
        Me.clmComments.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.clmComments.DefaultCellStyle = DataGridViewCellStyle1
        Me.clmComments.FillWeight = 350.0!
        Me.clmComments.HeaderText = "Errors, Warnings and Information"
        Me.clmComments.MinimumWidth = 100
        Me.clmComments.Name = "clmComments"
        Me.clmComments.ReadOnly = True
        Me.clmComments.Width = 123
        '
        'clmPath
        '
        Me.clmPath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.clmPath.HeaderText = "Path"
        Me.clmPath.Name = "clmPath"
        Me.clmPath.ReadOnly = True
        Me.clmPath.Width = 526
        '
        'DataNamesGridView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.datGV)
        Me.Name = "DataNamesGridView"
        Me.Size = New System.Drawing.Size(795, 335)
        CType(Me.datGV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents datGV As System.Windows.Forms.DataGridView
    Friend WithEvents clmDataName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents clmStatusIcon As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents clmComments As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents clmPath As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
