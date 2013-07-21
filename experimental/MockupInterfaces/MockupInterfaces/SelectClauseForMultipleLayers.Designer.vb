<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SelectClauseForMultipleLayers
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me._data_naming_conventions_beta_v0_8DataSet = New WindowsApplication1._data_naming_conventions_beta_v0_8DataSet
        Me.Datanamingconventionsbetav08DataSetBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DatanamingclausegeoextentBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Datanaming_clause_geoextentTableAdapter = New WindowsApplication1._data_naming_conventions_beta_v0_8DataSetTableAdapters.datanaming_clause_geoextentTableAdapter
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button3 = New System.Windows.Forms.Button
        Me.selected = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.ClauseDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DescriptionDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GeographytypeDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._data_naming_conventions_beta_v0_8DataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Datanamingconventionsbetav08DataSetBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DatanamingclausegeoextentBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToOrderColumns = True
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.selected, Me.ClauseDataGridViewTextBoxColumn, Me.DescriptionDataGridViewTextBoxColumn, Me.GeographytypeDataGridViewTextBoxColumn})
        Me.DataGridView1.DataSource = Me.DatanamingclausegeoextentBindingSource
        Me.DataGridView1.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(395, 234)
        Me.DataGridView1.TabIndex = 0
        '
        '_data_naming_conventions_beta_v0_8DataSet
        '
        Me._data_naming_conventions_beta_v0_8DataSet.DataSetName = "_data_naming_conventions_beta_v0_8DataSet"
        Me._data_naming_conventions_beta_v0_8DataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Datanamingconventionsbetav08DataSetBindingSource
        '
        Me.Datanamingconventionsbetav08DataSetBindingSource.DataSource = Me._data_naming_conventions_beta_v0_8DataSet
        Me.Datanamingconventionsbetav08DataSetBindingSource.Position = 0
        '
        'DatanamingclausegeoextentBindingSource
        '
        Me.DatanamingclausegeoextentBindingSource.DataMember = "datanaming_clause_geoextent"
        Me.DatanamingclausegeoextentBindingSource.DataSource = Me.Datanamingconventionsbetav08DataSetBindingSource
        '
        'Datanaming_clause_geoextentTableAdapter
        '
        Me.Datanaming_clause_geoextentTableAdapter.ClearBeforeFill = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(4, 240)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(47, 23)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "All"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(57, 240)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(45, 23)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "None"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(108, 240)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(105, 23)
        Me.Button3.TabIndex = 3
        Me.Button3.Text = "Inverse Selection"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'selected
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.NullValue = False
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.selected.DefaultCellStyle = DataGridViewCellStyle3
        Me.selected.HeaderText = "Select"
        Me.selected.Name = "selected"
        Me.selected.ReadOnly = True
        Me.selected.Width = 50
        '
        'ClauseDataGridViewTextBoxColumn
        '
        Me.ClauseDataGridViewTextBoxColumn.DataPropertyName = "clause"
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Blue
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White
        Me.ClauseDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle4
        Me.ClauseDataGridViewTextBoxColumn.HeaderText = "clause"
        Me.ClauseDataGridViewTextBoxColumn.Name = "ClauseDataGridViewTextBoxColumn"
        Me.ClauseDataGridViewTextBoxColumn.ReadOnly = True
        '
        'DescriptionDataGridViewTextBoxColumn
        '
        Me.DescriptionDataGridViewTextBoxColumn.DataPropertyName = "Description"
        Me.DescriptionDataGridViewTextBoxColumn.HeaderText = "Description"
        Me.DescriptionDataGridViewTextBoxColumn.Name = "DescriptionDataGridViewTextBoxColumn"
        Me.DescriptionDataGridViewTextBoxColumn.ReadOnly = True
        '
        'GeographytypeDataGridViewTextBoxColumn
        '
        Me.GeographytypeDataGridViewTextBoxColumn.DataPropertyName = "Geography_type"
        Me.GeographytypeDataGridViewTextBoxColumn.HeaderText = "Geography_type"
        Me.GeographytypeDataGridViewTextBoxColumn.Name = "GeographytypeDataGridViewTextBoxColumn"
        Me.GeographytypeDataGridViewTextBoxColumn.ReadOnly = True
        '
        'UserControl2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "UserControl2"
        Me.Size = New System.Drawing.Size(398, 269)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._data_naming_conventions_beta_v0_8DataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Datanamingconventionsbetav08DataSetBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DatanamingclausegeoextentBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents DatanamingclausegeoextentBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Datanamingconventionsbetav08DataSetBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents _data_naming_conventions_beta_v0_8DataSet As WindowsApplication1._data_naming_conventions_beta_v0_8DataSet
    Friend WithEvents Datanaming_clause_geoextentTableAdapter As WindowsApplication1._data_naming_conventions_beta_v0_8DataSetTableAdapters.datanaming_clause_geoextentTableAdapter
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents selected As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents ClauseDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DescriptionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GeographytypeDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
