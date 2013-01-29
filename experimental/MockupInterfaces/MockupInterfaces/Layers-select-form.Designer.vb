<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form2
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
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.txtBxDNLookupPath = New System.Windows.Forms.TextBox
        Me.ckBxOverrideLookupDB = New System.Windows.Forms.CheckBox
        Me.btnBrowseLookup = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.rdbMiscFiles = New System.Windows.Forms.RadioButton
        Me.rdbSelectMap = New System.Windows.Forms.RadioButton
        Me.rdbSelectGDB = New System.Windows.Forms.RadioButton
        Me.rdbSelectMXD = New System.Windows.Forms.RadioButton
        Me.txtWorkingDirectory = New System.Windows.Forms.TextBox
        Me.btnBrowse = New System.Windows.Forms.Button
        Me.lblFilePath = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.MaskedTextBox1 = New System.Windows.Forms.MaskedTextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.ComboBox7 = New System.Windows.Forms.ComboBox
        Me.ComboBox6 = New System.Windows.Forms.ComboBox
        Me.ComboBox5 = New System.Windows.Forms.ComboBox
        Me.ComboBox4 = New System.Windows.Forms.ComboBox
        Me.ComboBox3 = New System.Windows.Forms.ComboBox
        Me.ComboBox2 = New System.Windows.Forms.ComboBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.ComboBox1 = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.CheckBox1 = New System.Windows.Forms.CheckBox
        Me.ListBox1 = New System.Windows.Forms.ListBox
        Me.Button2 = New System.Windows.Forms.Button
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtBxDNLookupPath)
        Me.GroupBox2.Controls.Add(Me.ckBxOverrideLookupDB)
        Me.GroupBox2.Controls.Add(Me.btnBrowseLookup)
        Me.GroupBox2.Location = New System.Drawing.Point(14, 102)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(710, 75)
        Me.GroupBox2.TabIndex = 30
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Data Name Clause Lookup"
        '
        'txtBxDNLookupPath
        '
        Me.txtBxDNLookupPath.Enabled = False
        Me.txtBxDNLookupPath.Location = New System.Drawing.Point(23, 42)
        Me.txtBxDNLookupPath.Name = "txtBxDNLookupPath"
        Me.txtBxDNLookupPath.ReadOnly = True
        Me.txtBxDNLookupPath.Size = New System.Drawing.Size(579, 20)
        Me.txtBxDNLookupPath.TabIndex = 24
        '
        'ckBxOverrideLookupDB
        '
        Me.ckBxOverrideLookupDB.AutoSize = True
        Me.ckBxOverrideLookupDB.Location = New System.Drawing.Point(23, 19)
        Me.ckBxOverrideLookupDB.Name = "ckBxOverrideLookupDB"
        Me.ckBxOverrideLookupDB.Size = New System.Drawing.Size(72, 17)
        Me.ckBxOverrideLookupDB.TabIndex = 26
        Me.ckBxOverrideLookupDB.Text = "(Override)"
        Me.ckBxOverrideLookupDB.UseVisualStyleBackColor = True
        '
        'btnBrowseLookup
        '
        Me.btnBrowseLookup.Location = New System.Drawing.Point(613, 40)
        Me.btnBrowseLookup.Name = "btnBrowseLookup"
        Me.btnBrowseLookup.Size = New System.Drawing.Size(75, 23)
        Me.btnBrowseLookup.TabIndex = 27
        Me.btnBrowseLookup.Text = "Browse..."
        Me.btnBrowseLookup.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.rdbMiscFiles)
        Me.GroupBox3.Controls.Add(Me.rdbSelectMap)
        Me.GroupBox3.Controls.Add(Me.rdbSelectGDB)
        Me.GroupBox3.Controls.Add(Me.rdbSelectMXD)
        Me.GroupBox3.Controls.Add(Me.txtWorkingDirectory)
        Me.GroupBox3.Controls.Add(Me.btnBrowse)
        Me.GroupBox3.Controls.Add(Me.lblFilePath)
        Me.GroupBox3.Location = New System.Drawing.Point(14, 12)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(710, 84)
        Me.GroupBox3.TabIndex = 29
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Select Source for Layers"
        '
        'rdbMiscFiles
        '
        Me.rdbMiscFiles.AutoSize = True
        Me.rdbMiscFiles.Location = New System.Drawing.Point(305, 19)
        Me.rdbMiscFiles.Name = "rdbMiscFiles"
        Me.rdbMiscFiles.Size = New System.Drawing.Size(153, 17)
        Me.rdbMiscFiles.TabIndex = 6
        Me.rdbMiscFiles.TabStop = True
        Me.rdbMiscFiles.Text = "Layer, Shape or Image files"
        Me.rdbMiscFiles.UseVisualStyleBackColor = True
        '
        'rdbSelectMap
        '
        Me.rdbSelectMap.AutoSize = True
        Me.rdbSelectMap.Enabled = False
        Me.rdbSelectMap.Location = New System.Drawing.Point(23, 19)
        Me.rdbSelectMap.Name = "rdbSelectMap"
        Me.rdbSelectMap.Size = New System.Drawing.Size(83, 17)
        Me.rdbSelectMap.TabIndex = 3
        Me.rdbSelectMap.TabStop = True
        Me.rdbSelectMap.Text = "Current Map"
        Me.rdbSelectMap.UseVisualStyleBackColor = True
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
        'rdbSelectMXD
        '
        Me.rdbSelectMXD.AutoSize = True
        Me.rdbSelectMXD.Location = New System.Drawing.Point(123, 19)
        Me.rdbSelectMXD.Name = "rdbSelectMXD"
        Me.rdbSelectMXD.Size = New System.Drawing.Size(49, 17)
        Me.rdbSelectMXD.TabIndex = 4
        Me.rdbSelectMXD.TabStop = True
        Me.rdbSelectMXD.Text = "MXD"
        Me.rdbSelectMXD.UseVisualStyleBackColor = True
        '
        'txtWorkingDirectory
        '
        Me.txtWorkingDirectory.Location = New System.Drawing.Point(23, 57)
        Me.txtWorkingDirectory.Name = "txtWorkingDirectory"
        Me.txtWorkingDirectory.ReadOnly = True
        Me.txtWorkingDirectory.Size = New System.Drawing.Size(579, 20)
        Me.txtWorkingDirectory.TabIndex = 17
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(615, 57)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(75, 23)
        Me.btnBrowse.TabIndex = 16
        Me.btnBrowse.Text = "Browse..."
        Me.btnBrowse.UseVisualStyleBackColor = True
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
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.MaskedTextBox1)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.ComboBox7)
        Me.GroupBox1.Controls.Add(Me.ComboBox6)
        Me.GroupBox1.Controls.Add(Me.ComboBox5)
        Me.GroupBox1.Controls.Add(Me.ComboBox4)
        Me.GroupBox1.Controls.Add(Me.ComboBox3)
        Me.GroupBox1.Controls.Add(Me.ComboBox2)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.ComboBox1)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(14, 183)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(710, 169)
        Me.GroupBox1.TabIndex = 31
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Create Name"
        '
        'MaskedTextBox1
        '
        Me.MaskedTextBox1.Location = New System.Drawing.Point(23, 134)
        Me.MaskedTextBox1.Name = "MaskedTextBox1"
        Me.MaskedTextBox1.Size = New System.Drawing.Size(665, 20)
        Me.MaskedTextBox1.TabIndex = 15
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(23, 105)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(98, 37)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "free text"
        '
        'ComboBox7
        '
        Me.ComboBox7.FormattingEnabled = True
        Me.ComboBox7.Location = New System.Drawing.Point(118, 68)
        Me.ComboBox7.Name = "ComboBox7"
        Me.ComboBox7.Size = New System.Drawing.Size(61, 21)
        Me.ComboBox7.TabIndex = 13
        '
        'ComboBox6
        '
        Me.ComboBox6.FormattingEnabled = True
        Me.ComboBox6.Location = New System.Drawing.Point(213, 68)
        Me.ComboBox6.Name = "ComboBox6"
        Me.ComboBox6.Size = New System.Drawing.Size(61, 21)
        Me.ComboBox6.TabIndex = 12
        '
        'ComboBox5
        '
        Me.ComboBox5.FormattingEnabled = True
        Me.ComboBox5.Location = New System.Drawing.Point(308, 68)
        Me.ComboBox5.Name = "ComboBox5"
        Me.ComboBox5.Size = New System.Drawing.Size(61, 21)
        Me.ComboBox5.TabIndex = 11
        '
        'ComboBox4
        '
        Me.ComboBox4.FormattingEnabled = True
        Me.ComboBox4.Location = New System.Drawing.Point(403, 68)
        Me.ComboBox4.Name = "ComboBox4"
        Me.ComboBox4.Size = New System.Drawing.Size(61, 21)
        Me.ComboBox4.TabIndex = 10
        '
        'ComboBox3
        '
        Me.ComboBox3.FormattingEnabled = True
        Me.ComboBox3.Location = New System.Drawing.Point(498, 68)
        Me.ComboBox3.Name = "ComboBox3"
        Me.ComboBox3.Size = New System.Drawing.Size(61, 21)
        Me.ComboBox3.TabIndex = 9
        '
        'ComboBox2
        '
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Location = New System.Drawing.Point(593, 68)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(61, 21)
        Me.ComboBox2.TabIndex = 8
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(590, 28)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(98, 37)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "permissions"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(495, 28)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(61, 37)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "source"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(400, 28)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(62, 37)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "scale"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(305, 28)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 37)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "data type"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(210, 28)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 37)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "data theme"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(115, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 37)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "data category"
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(23, 68)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(61, 21)
        Me.ComboBox1.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(23, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 37)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "geoextent"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.ListBox1)
        Me.GroupBox4.Controls.Add(Me.CheckBox1)
        Me.GroupBox4.Controls.Add(Me.Button1)
        Me.GroupBox4.Location = New System.Drawing.Point(14, 358)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(710, 149)
        Me.GroupBox4.TabIndex = 33
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Output"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(615, 101)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 38)
        Me.Button1.TabIndex = 33
        Me.Button1.Text = "Copy to clipboard"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(23, 20)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(116, 17)
        Me.CheckBox1.TabIndex = 34
        Me.CheckBox1.Text = "Include pathnames"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(23, 44)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(579, 95)
        Me.ListBox1.TabIndex = 35
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(629, 521)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 34
        Me.Button2.Text = "OK/Close"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Form2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(736, 556)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox3)
        Me.Name = "Form2"
        Me.Text = "Select from list of layers"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtBxDNLookupPath As System.Windows.Forms.TextBox
    Friend WithEvents ckBxOverrideLookupDB As System.Windows.Forms.CheckBox
    Friend WithEvents btnBrowseLookup As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents rdbMiscFiles As System.Windows.Forms.RadioButton
    Friend WithEvents rdbSelectMap As System.Windows.Forms.RadioButton
    Friend WithEvents rdbSelectGDB As System.Windows.Forms.RadioButton
    Friend WithEvents rdbSelectMXD As System.Windows.Forms.RadioButton
    Friend WithEvents txtWorkingDirectory As System.Windows.Forms.TextBox
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents lblFilePath As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ComboBox7 As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox6 As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox5 As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox4 As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox3 As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents MaskedTextBox1 As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents Button2 As System.Windows.Forms.Button

End Class
