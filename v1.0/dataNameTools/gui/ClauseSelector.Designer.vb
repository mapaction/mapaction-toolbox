<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ClauseSelector
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
        Me.components = New System.ComponentModel.Container
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.dgv = New System.Windows.Forms.DataGridView
        Me.ClauseDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DescriptionDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.BaseorSituationalDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DatanamingclausedatacategoriesBindingSource2 = New System.Windows.Forms.BindingSource(Me.components)
        Me.Fallbackdatanamingconventionsv10DataSetBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me._fall_back_data_naming_conventions_v1_0DataSet = New mapaction.datanames.gui._fall_back_data_naming_conventions_v1_0DataSet
        Me.pnlDropDownContainor = New System.Windows.Forms.Panel
        Me.DatanamingclausedatacategoriesBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Datanaming_clause_data_categoriesTableAdapter = New mapaction.datanames.gui._fall_back_data_naming_conventions_v1_0DataSetTableAdapters.datanaming_clause_data_categoriesTableAdapter
        Me.DatanamingclausedatacategoriesBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.ToolStripDropDown1 = New System.Windows.Forms.ToolStripDropDown
        Me.ToolStripDropDownMenu1 = New System.Windows.Forms.ToolStripDropDownMenu
        Me.ToolStripDropDownMenu2 = New System.Windows.Forms.ToolStripDropDownMenu
        Me.ToolStripDropDown2 = New System.Windows.Forms.ToolStripDropDown
        Me.OneToolStripButton = New System.Windows.Forms.ToolStripButton
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DatanamingclausedatacategoriesBindingSource2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fallbackdatanamingconventionsv10DataSetBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._fall_back_data_naming_conventions_v1_0DataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDropDownContainor.SuspendLayout()
        CType(Me.DatanamingclausedatacategoriesBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DatanamingclausedatacategoriesBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStripDropDown1.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgv
        '
        Me.dgv.AllowUserToDeleteRows = False
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.dgv.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        Me.dgv.AutoGenerateColumns = False
        Me.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ClauseDataGridViewTextBoxColumn, Me.DescriptionDataGridViewTextBoxColumn, Me.BaseorSituationalDataGridViewTextBoxColumn})
        Me.dgv.DataSource = Me.DatanamingclausedatacategoriesBindingSource2
        Me.dgv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgv.Location = New System.Drawing.Point(0, 0)
        Me.dgv.Name = "dgv"
        Me.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv.Size = New System.Drawing.Size(468, 378)
        Me.dgv.TabIndex = 0
        '
        'ClauseDataGridViewTextBoxColumn
        '
        Me.ClauseDataGridViewTextBoxColumn.DataPropertyName = "clause"
        Me.ClauseDataGridViewTextBoxColumn.HeaderText = "clause"
        Me.ClauseDataGridViewTextBoxColumn.Name = "ClauseDataGridViewTextBoxColumn"
        '
        'DescriptionDataGridViewTextBoxColumn
        '
        Me.DescriptionDataGridViewTextBoxColumn.DataPropertyName = "Description"
        Me.DescriptionDataGridViewTextBoxColumn.HeaderText = "Description"
        Me.DescriptionDataGridViewTextBoxColumn.Name = "DescriptionDataGridViewTextBoxColumn"
        '
        'BaseorSituationalDataGridViewTextBoxColumn
        '
        Me.BaseorSituationalDataGridViewTextBoxColumn.DataPropertyName = "Base_or_Situational"
        Me.BaseorSituationalDataGridViewTextBoxColumn.HeaderText = "Base_or_Situational"
        Me.BaseorSituationalDataGridViewTextBoxColumn.Name = "BaseorSituationalDataGridViewTextBoxColumn"
        '
        'DatanamingclausedatacategoriesBindingSource2
        '
        Me.DatanamingclausedatacategoriesBindingSource2.DataMember = "datanaming_clause_data_categories"
        Me.DatanamingclausedatacategoriesBindingSource2.DataSource = Me.Fallbackdatanamingconventionsv10DataSetBindingSource
        '
        'Fallbackdatanamingconventionsv10DataSetBindingSource
        '
        Me.Fallbackdatanamingconventionsv10DataSetBindingSource.DataSource = Me._fall_back_data_naming_conventions_v1_0DataSet
        Me.Fallbackdatanamingconventionsv10DataSetBindingSource.Position = 0
        '
        '_fall_back_data_naming_conventions_v1_0DataSet
        '
        Me._fall_back_data_naming_conventions_v1_0DataSet.DataSetName = "_fall_back_data_naming_conventions_v1_0DataSet"
        Me._fall_back_data_naming_conventions_v1_0DataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'pnlDropDownContainor
        '
        Me.pnlDropDownContainor.BackColor = System.Drawing.SystemColors.Control
        Me.pnlDropDownContainor.Controls.Add(Me.dgv)
        Me.pnlDropDownContainor.Location = New System.Drawing.Point(148, 118)
        Me.pnlDropDownContainor.Name = "pnlDropDownContainor"
        Me.pnlDropDownContainor.Size = New System.Drawing.Size(468, 378)
        Me.pnlDropDownContainor.TabIndex = 1
        '
        'Datanaming_clause_data_categoriesTableAdapter
        '
        Me.Datanaming_clause_data_categoriesTableAdapter.ClearBeforeFill = True
        '
        'DatanamingclausedatacategoriesBindingSource1
        '
        Me.DatanamingclausedatacategoriesBindingSource1.DataMember = "datanaming_clause_data_categories"
        Me.DatanamingclausedatacategoriesBindingSource1.DataSource = Me.Fallbackdatanamingconventionsv10DataSetBindingSource
        '
        'ToolStripDropDown1
        '
        Me.ToolStripDropDown1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OneToolStripButton})
        Me.ToolStripDropDown1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ToolStripDropDown1.Name = "ToolStripDropDown1"
        Me.ToolStripDropDown1.Size = New System.Drawing.Size(31, 24)
        '
        'ToolStripDropDownMenu1
        '
        Me.ToolStripDropDownMenu1.Name = "ToolStripDropDownMenu1"
        Me.ToolStripDropDownMenu1.Size = New System.Drawing.Size(61, 4)
        '
        'ToolStripDropDownMenu2
        '
        Me.ToolStripDropDownMenu2.Name = "ToolStripDropDownMenu2"
        Me.ToolStripDropDownMenu2.Size = New System.Drawing.Size(61, 4)
        '
        'ToolStripDropDown2
        '
        Me.ToolStripDropDown2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ToolStripDropDown2.Name = "ToolStripDropDown2"
        Me.ToolStripDropDown2.Size = New System.Drawing.Size(2, 4)
        '
        'OneToolStripButton
        '
        Me.OneToolStripButton.Name = "OneToolStripButton"
        Me.OneToolStripButton.Size = New System.Drawing.Size(29, 17)
        Me.OneToolStripButton.Text = "one"
        '
        'ClauseSelector
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Maroon
        Me.Controls.Add(Me.pnlDropDownContainor)
        Me.Name = "ClauseSelector"
        Me.Size = New System.Drawing.Size(468, 378)
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DatanamingclausedatacategoriesBindingSource2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fallbackdatanamingconventionsv10DataSetBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._fall_back_data_naming_conventions_v1_0DataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDropDownContainor.ResumeLayout(False)
        CType(Me.DatanamingclausedatacategoriesBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DatanamingclausedatacategoriesBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStripDropDown1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
    Friend WithEvents pnlDropDownContainor As System.Windows.Forms.Panel
    Friend WithEvents DatanamingclausedatacategoriesBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Datanaming_clause_data_categoriesTableAdapter As mapaction.datanames.gui._fall_back_data_naming_conventions_v1_0DataSetTableAdapters.datanaming_clause_data_categoriesTableAdapter
    Friend WithEvents ClauseDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DescriptionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BaseorSituationalDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DatanamingclausedatacategoriesBindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents Fallbackdatanamingconventionsv10DataSetBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents _fall_back_data_naming_conventions_v1_0DataSet As mapaction.datanames.gui._fall_back_data_naming_conventions_v1_0DataSet
    Friend WithEvents DatanamingclausedatacategoriesBindingSource2 As System.Windows.Forms.BindingSource
    Friend WithEvents ToolStripDropDown1 As System.Windows.Forms.ToolStripDropDown
    Friend WithEvents ToolStripDropDownMenu1 As System.Windows.Forms.ToolStripDropDownMenu
    Friend WithEvents ToolStripDropDownMenu2 As System.Windows.Forms.ToolStripDropDownMenu
    Friend WithEvents OneToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripDropDown2 As System.Windows.Forms.ToolStripDropDown

End Class
