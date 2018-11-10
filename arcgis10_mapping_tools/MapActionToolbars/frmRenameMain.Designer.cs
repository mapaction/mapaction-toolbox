namespace MapActionToolbars
{
    partial class frmRenameMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRenameMain));
            this.btnRename = new System.Windows.Forms.Button();
            this.cboGeoExtent = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tbxGeoExtent = new System.Windows.Forms.TextBox();
            this.chkGeoExtent = new System.Windows.Forms.CheckBox();
            this.chkSource = new System.Windows.Forms.CheckBox();
            this.chkScale = new System.Windows.Forms.CheckBox();
            this.chkType = new System.Windows.Forms.CheckBox();
            this.chkTheme = new System.Windows.Forms.CheckBox();
            this.chkCategory = new System.Windows.Forms.CheckBox();
            this.tbxSource = new System.Windows.Forms.TextBox();
            this.tbxScale = new System.Windows.Forms.TextBox();
            this.tbxType = new System.Windows.Forms.TextBox();
            this.tbxTheme = new System.Windows.Forms.TextBox();
            this.tbxCategory = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tbxPermission = new System.Windows.Forms.TextBox();
            this.chkPermission = new System.Windows.Forms.CheckBox();
            this.tbxFreeText = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblReviewLayerName = new System.Windows.Forms.Label();
            this.btnPermissionHelp = new System.Windows.Forms.Button();
            this.cboPermission = new System.Windows.Forms.ComboBox();
            this.cboSource = new System.Windows.Forms.ComboBox();
            this.cboScale = new System.Windows.Forms.ComboBox();
            this.cboTheme = new System.Windows.Forms.ComboBox();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbxPathToShapeFile = new System.Windows.Forms.TextBox();
            this.gbxCrashMoveFolder = new System.Windows.Forms.GroupBox();
            this.btnNavigateToShapeFile = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.gbxCrashMoveFolder.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRename
            // 
            this.btnRename.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRename.Location = new System.Drawing.Point(257, 405);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(75, 24);
            this.btnRename.TabIndex = 1;
            this.btnRename.Text = "Rename";
            this.btnRename.UseVisualStyleBackColor = true;
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // cboGeoExtent
            // 
            this.cboGeoExtent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGeoExtent.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboGeoExtent.FormattingEnabled = true;
            this.cboGeoExtent.Location = new System.Drawing.Point(78, 39);
            this.cboGeoExtent.Name = "cboGeoExtent";
            this.cboGeoExtent.Size = new System.Drawing.Size(405, 21);
            this.cboGeoExtent.TabIndex = 0;
            this.cboGeoExtent.DropDownClosed += new System.EventHandler(this.cboGeoExtent_DropDownClosed);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "GeoExtent";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Category";
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(574, 405);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 24);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tbxGeoExtent
            // 
            this.tbxGeoExtent.Enabled = false;
            this.tbxGeoExtent.Location = new System.Drawing.Point(529, 38);
            this.tbxGeoExtent.Name = "tbxGeoExtent";
            this.tbxGeoExtent.Size = new System.Drawing.Size(125, 20);
            this.tbxGeoExtent.TabIndex = 1;
            this.tbxGeoExtent.TextChanged += new System.EventHandler(this.tbxGeoExtent_TextChanged);
            this.tbxGeoExtent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxGeoExtent_KeyPress);
            // 
            // chkGeoExtent
            // 
            this.chkGeoExtent.AutoSize = true;
            this.chkGeoExtent.Location = new System.Drawing.Point(508, 43);
            this.chkGeoExtent.Name = "chkGeoExtent";
            this.chkGeoExtent.Size = new System.Drawing.Size(15, 14);
            this.chkGeoExtent.TabIndex = 13;
            this.chkGeoExtent.UseVisualStyleBackColor = true;
            this.chkGeoExtent.CheckedChanged += new System.EventHandler(this.chkGeoExtent_CheckedChanged);
            // 
            // chkSource
            // 
            this.chkSource.AutoSize = true;
            this.chkSource.Location = new System.Drawing.Point(508, 176);
            this.chkSource.Name = "chkSource";
            this.chkSource.Size = new System.Drawing.Size(15, 14);
            this.chkSource.TabIndex = 14;
            this.chkSource.UseVisualStyleBackColor = true;
            this.chkSource.CheckedChanged += new System.EventHandler(this.chkSource_CheckedChanged);
            // 
            // chkScale
            // 
            this.chkScale.AutoSize = true;
            this.chkScale.Location = new System.Drawing.Point(508, 150);
            this.chkScale.Name = "chkScale";
            this.chkScale.Size = new System.Drawing.Size(15, 14);
            this.chkScale.TabIndex = 15;
            this.chkScale.UseVisualStyleBackColor = true;
            this.chkScale.CheckedChanged += new System.EventHandler(this.chkScale_CheckedChanged);
            // 
            // chkType
            // 
            this.chkType.AutoSize = true;
            this.chkType.Location = new System.Drawing.Point(508, 122);
            this.chkType.Name = "chkType";
            this.chkType.Size = new System.Drawing.Size(15, 14);
            this.chkType.TabIndex = 16;
            this.chkType.UseVisualStyleBackColor = true;
            this.chkType.CheckedChanged += new System.EventHandler(this.chkType_CheckedChanged);
            // 
            // chkTheme
            // 
            this.chkTheme.AutoSize = true;
            this.chkTheme.Location = new System.Drawing.Point(508, 95);
            this.chkTheme.Name = "chkTheme";
            this.chkTheme.Size = new System.Drawing.Size(15, 14);
            this.chkTheme.TabIndex = 17;
            this.chkTheme.UseVisualStyleBackColor = true;
            this.chkTheme.CheckedChanged += new System.EventHandler(this.chkTheme_CheckedChanged);
            // 
            // chkCategory
            // 
            this.chkCategory.AutoSize = true;
            this.chkCategory.Location = new System.Drawing.Point(508, 71);
            this.chkCategory.Name = "chkCategory";
            this.chkCategory.Size = new System.Drawing.Size(15, 14);
            this.chkCategory.TabIndex = 18;
            this.chkCategory.UseVisualStyleBackColor = true;
            this.chkCategory.CheckedChanged += new System.EventHandler(this.chkCategory_CheckedChanged);
            // 
            // tbxSource
            // 
            this.tbxSource.Enabled = false;
            this.tbxSource.Location = new System.Drawing.Point(529, 171);
            this.tbxSource.Name = "tbxSource";
            this.tbxSource.Size = new System.Drawing.Size(125, 20);
            this.tbxSource.TabIndex = 11;
            this.tbxSource.TextChanged += new System.EventHandler(this.tbxSource_TextChanged);
            this.tbxSource.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxGeoExtent_KeyPress);
            // 
            // tbxScale
            // 
            this.tbxScale.Enabled = false;
            this.tbxScale.Location = new System.Drawing.Point(529, 145);
            this.tbxScale.Name = "tbxScale";
            this.tbxScale.Size = new System.Drawing.Size(125, 20);
            this.tbxScale.TabIndex = 9;
            this.tbxScale.TextChanged += new System.EventHandler(this.tbxScale_TextChanged);
            this.tbxScale.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxGeoExtent_KeyPress);
            // 
            // tbxType
            // 
            this.tbxType.Enabled = false;
            this.tbxType.Location = new System.Drawing.Point(529, 117);
            this.tbxType.Name = "tbxType";
            this.tbxType.Size = new System.Drawing.Size(125, 20);
            this.tbxType.TabIndex = 7;
            this.tbxType.TextChanged += new System.EventHandler(this.tbxType_TextChanged);
            this.tbxType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxGeoExtent_KeyPress);
            // 
            // tbxTheme
            // 
            this.tbxTheme.Enabled = false;
            this.tbxTheme.Location = new System.Drawing.Point(529, 90);
            this.tbxTheme.Name = "tbxTheme";
            this.tbxTheme.Size = new System.Drawing.Size(125, 20);
            this.tbxTheme.TabIndex = 5;
            this.tbxTheme.TextChanged += new System.EventHandler(this.tbxTheme_TextChanged);
            this.tbxTheme.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxGeoExtent_KeyPress);
            // 
            // tbxCategory
            // 
            this.tbxCategory.Enabled = false;
            this.tbxCategory.Location = new System.Drawing.Point(529, 66);
            this.tbxCategory.Name = "tbxCategory";
            this.tbxCategory.Size = new System.Drawing.Size(125, 20);
            this.tbxCategory.TabIndex = 3;
            this.tbxCategory.TextChanged += new System.EventHandler(this.tbxCategory_TextChanged);
            this.tbxCategory.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxGeoExtent_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "Theme";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 120);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 27;
            this.label6.Text = "Geometry";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 148);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 28;
            this.label7.Text = "Scale";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 174);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 13);
            this.label8.TabIndex = 29;
            this.label8.Text = "Source";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 229);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 13);
            this.label9.TabIndex = 37;
            this.label9.Text = "Free text";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(15, 201);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 13);
            this.label10.TabIndex = 36;
            this.label10.Text = "Permission";
            // 
            // tbxPermission
            // 
            this.tbxPermission.Enabled = false;
            this.tbxPermission.Location = new System.Drawing.Point(529, 197);
            this.tbxPermission.Name = "tbxPermission";
            this.tbxPermission.Size = new System.Drawing.Size(125, 20);
            this.tbxPermission.TabIndex = 13;
            this.tbxPermission.TextChanged += new System.EventHandler(this.tbxPermission_TextChanged);
            this.tbxPermission.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxGeoExtent_KeyPress);
            // 
            // chkPermission
            // 
            this.chkPermission.AutoSize = true;
            this.chkPermission.Location = new System.Drawing.Point(508, 202);
            this.chkPermission.Name = "chkPermission";
            this.chkPermission.Size = new System.Drawing.Size(15, 14);
            this.chkPermission.TabIndex = 33;
            this.chkPermission.UseVisualStyleBackColor = true;
            this.chkPermission.CheckedChanged += new System.EventHandler(this.chkPermission_CheckedChanged);
            // 
            // tbxFreeText
            // 
            this.tbxFreeText.Location = new System.Drawing.Point(78, 226);
            this.tbxFreeText.Name = "tbxFreeText";
            this.tbxFreeText.Size = new System.Drawing.Size(405, 20);
            this.tbxFreeText.TabIndex = 14;
            this.tbxFreeText.TextChanged += new System.EventHandler(this.tbxFreeText_TextChanged);
            this.tbxFreeText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxFreeText_KeyPress);
            this.tbxFreeText.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.tbxFreeText_PreviewKeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblReviewLayerName);
            this.groupBox1.Controls.Add(this.btnPermissionHelp);
            this.groupBox1.Controls.Add(this.cboPermission);
            this.groupBox1.Controls.Add(this.cboSource);
            this.groupBox1.Controls.Add(this.cboScale);
            this.groupBox1.Controls.Add(this.cboTheme);
            this.groupBox1.Controls.Add(this.cboCategory);
            this.groupBox1.Controls.Add(this.cboType);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cboGeoExtent);
            this.groupBox1.Controls.Add(this.tbxPermission);
            this.groupBox1.Controls.Add(this.tbxFreeText);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.chkPermission);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tbxGeoExtent);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.chkGeoExtent);
            this.groupBox1.Controls.Add(this.tbxCategory);
            this.groupBox1.Controls.Add(this.chkSource);
            this.groupBox1.Controls.Add(this.tbxTheme);
            this.groupBox1.Controls.Add(this.chkScale);
            this.groupBox1.Controls.Add(this.tbxType);
            this.groupBox1.Controls.Add(this.chkType);
            this.groupBox1.Controls.Add(this.tbxScale);
            this.groupBox1.Controls.Add(this.chkTheme);
            this.groupBox1.Controls.Add(this.tbxSource);
            this.groupBox1.Controls.Add(this.chkCategory);
            this.groupBox1.Location = new System.Drawing.Point(12, 99);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(671, 300);
            this.groupBox1.TabIndex = 38;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Name elements";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(15, 260);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(108, 13);
            this.label11.TabIndex = 48;
            this.label11.Text = "Layer name preview: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(561, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 47;
            this.label5.Text = "Custom values";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(196, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 13);
            this.label1.TabIndex = 46;
            this.label1.Text = "Description of preset values";
            // 
            // lblReviewLayerName
            // 
            this.lblReviewLayerName.AutoSize = true;
            this.lblReviewLayerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReviewLayerName.Location = new System.Drawing.Point(129, 256);
            this.lblReviewLayerName.Name = "lblReviewLayerName";
            this.lblReviewLayerName.Size = new System.Drawing.Size(0, 13);
            this.lblReviewLayerName.TabIndex = 45;
            // 
            // btnPermissionHelp
            // 
            this.btnPermissionHelp.Location = new System.Drawing.Point(2, 198);
            this.btnPermissionHelp.Name = "btnPermissionHelp";
            this.btnPermissionHelp.Size = new System.Drawing.Size(14, 20);
            this.btnPermissionHelp.TabIndex = 43;
            this.btnPermissionHelp.Text = "i";
            this.btnPermissionHelp.UseVisualStyleBackColor = true;
            this.btnPermissionHelp.Click += new System.EventHandler(this.btnPermissionHelp_Click);
            // 
            // cboPermission
            // 
            this.cboPermission.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPermission.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPermission.FormattingEnabled = true;
            this.cboPermission.Location = new System.Drawing.Point(78, 197);
            this.cboPermission.Name = "cboPermission";
            this.cboPermission.Size = new System.Drawing.Size(405, 21);
            this.cboPermission.TabIndex = 12;
            this.cboPermission.DropDownClosed += new System.EventHandler(this.cboPermission_DropDownClosed);
            // 
            // cboSource
            // 
            this.cboSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSource.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSource.FormattingEnabled = true;
            this.cboSource.Location = new System.Drawing.Point(78, 171);
            this.cboSource.Name = "cboSource";
            this.cboSource.Size = new System.Drawing.Size(405, 21);
            this.cboSource.TabIndex = 10;
            this.cboSource.DropDownClosed += new System.EventHandler(this.cboSource_DropDownClosed);
            // 
            // cboScale
            // 
            this.cboScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboScale.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboScale.FormattingEnabled = true;
            this.cboScale.Location = new System.Drawing.Point(78, 145);
            this.cboScale.Name = "cboScale";
            this.cboScale.Size = new System.Drawing.Size(405, 21);
            this.cboScale.TabIndex = 8;
            this.cboScale.DropDownClosed += new System.EventHandler(this.cboScale_DropDownClosed);
            // 
            // cboTheme
            // 
            this.cboTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTheme.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTheme.FormattingEnabled = true;
            this.cboTheme.Location = new System.Drawing.Point(78, 91);
            this.cboTheme.Name = "cboTheme";
            this.cboTheme.Size = new System.Drawing.Size(405, 21);
            this.cboTheme.TabIndex = 4;
            this.cboTheme.DropDownClosed += new System.EventHandler(this.cboTheme_DropDownClosed);
            // 
            // cboCategory
            // 
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCategory.FormattingEnabled = true;
            this.cboCategory.Location = new System.Drawing.Point(78, 64);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(405, 21);
            this.cboCategory.TabIndex = 2;
            this.cboCategory.DropDownClosed += new System.EventHandler(this.cboCategory_DropDownClosed);
            // 
            // cboType
            // 
            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboType.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboType.FormattingEnabled = true;
            this.cboType.Location = new System.Drawing.Point(78, 117);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(405, 21);
            this.cboType.TabIndex = 6;
            this.cboType.DropDownClosed += new System.EventHandler(this.cboType_DropDownClosed);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(27, 340);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(0, 13);
            this.label12.TabIndex = 39;
            // 
            // tbxPathToShapeFile
            // 
            this.tbxPathToShapeFile.Enabled = false;
            this.tbxPathToShapeFile.Location = new System.Drawing.Point(96, 20);
            this.tbxPathToShapeFile.Name = "tbxPathToShapeFile";
            this.tbxPathToShapeFile.Size = new System.Drawing.Size(489, 20);
            this.tbxPathToShapeFile.TabIndex = 0;
            // 
            // gbxCrashMoveFolder
            // 
            this.gbxCrashMoveFolder.Controls.Add(this.btnNavigateToShapeFile);
            this.gbxCrashMoveFolder.Controls.Add(this.tbxPathToShapeFile);
            this.gbxCrashMoveFolder.Controls.Add(this.label13);
            this.gbxCrashMoveFolder.Location = new System.Drawing.Point(16, 27);
            this.gbxCrashMoveFolder.Name = "gbxCrashMoveFolder";
            this.gbxCrashMoveFolder.Size = new System.Drawing.Size(667, 55);
            this.gbxCrashMoveFolder.TabIndex = 40;
            this.gbxCrashMoveFolder.TabStop = false;
            this.gbxCrashMoveFolder.Text = "Shape file";
            // 
            // btnNavigateToShapeFile
            // 
            this.btnNavigateToShapeFile.Location = new System.Drawing.Point(609, 20);
            this.btnNavigateToShapeFile.Name = "btnNavigateToShapeFile";
            this.btnNavigateToShapeFile.Size = new System.Drawing.Size(33, 23);
            this.btnNavigateToShapeFile.TabIndex = 18;
            this.btnNavigateToShapeFile.Text = "...";
            this.btnNavigateToShapeFile.UseVisualStyleBackColor = true;
            this.btnNavigateToShapeFile.Click += new System.EventHandler(this.btnPathToExistingXml_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(30, 24);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(47, 13);
            this.label13.TabIndex = 19;
            this.label13.Text = "Set path";
            // 
            // frmRenameMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 440);
            this.Controls.Add(this.gbxCrashMoveFolder);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnRename);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRenameMain";
            this.Text = "MapAction Dataset Rename Tool";
            this.Load += new System.EventHandler(this.frmMain_Load_1);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbxCrashMoveFolder.ResumeLayout(false);
            this.gbxCrashMoveFolder.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRename;
        private System.Windows.Forms.ComboBox cboGeoExtent;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox tbxGeoExtent;
        private System.Windows.Forms.CheckBox chkGeoExtent;
        private System.Windows.Forms.CheckBox chkSource;
        private System.Windows.Forms.CheckBox chkScale;
        private System.Windows.Forms.CheckBox chkType;
        private System.Windows.Forms.CheckBox chkTheme;
        private System.Windows.Forms.CheckBox chkCategory;
        private System.Windows.Forms.TextBox tbxSource;
        private System.Windows.Forms.TextBox tbxScale;
        private System.Windows.Forms.TextBox tbxType;
        private System.Windows.Forms.TextBox tbxTheme;
        private System.Windows.Forms.TextBox tbxCategory;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbxPermission;
        private System.Windows.Forms.CheckBox chkPermission;
        private System.Windows.Forms.TextBox tbxFreeText;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboPermission;
        private System.Windows.Forms.ComboBox cboSource;
        private System.Windows.Forms.ComboBox cboScale;
        private System.Windows.Forms.ComboBox cboTheme;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.Button btnPermissionHelp;
        private System.Windows.Forms.Label lblReviewLayerName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbxPathToShapeFile;
        private System.Windows.Forms.GroupBox gbxCrashMoveFolder;
        private System.Windows.Forms.Button btnNavigateToShapeFile;
        private System.Windows.Forms.Label label13;

    }
}