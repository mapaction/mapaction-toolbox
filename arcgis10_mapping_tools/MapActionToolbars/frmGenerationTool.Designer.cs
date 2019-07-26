namespace MapActionToolbars
{
    partial class frmGenerationTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGenerationTool));
            this.gbxCrashMoveFolder = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkGeoExtent = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxGeoExtent = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cboProductType = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.gbxCrashMoveFolder.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxCrashMoveFolder
            // 
            this.gbxCrashMoveFolder.Controls.Add(this.label2);
            this.gbxCrashMoveFolder.Controls.Add(this.chkGeoExtent);
            this.gbxCrashMoveFolder.Controls.Add(this.label1);
            this.gbxCrashMoveFolder.Controls.Add(this.tbxGeoExtent);
            this.gbxCrashMoveFolder.Controls.Add(this.label13);
            this.gbxCrashMoveFolder.Location = new System.Drawing.Point(18, 19);
            this.gbxCrashMoveFolder.Name = "gbxCrashMoveFolder";
            this.gbxCrashMoveFolder.Size = new System.Drawing.Size(537, 99);
            this.gbxCrashMoveFolder.TabIndex = 41;
            this.gbxCrashMoveFolder.TabStop = false;
            this.gbxCrashMoveFolder.Text = "Generate";
            this.gbxCrashMoveFolder.Enter += new System.EventHandler(this.gbxCrashMoveFolder_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(394, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Refresh Data";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // chkGeoExtent
            // 
            this.chkGeoExtent.AutoSize = true;
            this.chkGeoExtent.Location = new System.Drawing.Point(474, 31);
            this.chkGeoExtent.Name = "chkGeoExtent";
            this.chkGeoExtent.Size = new System.Drawing.Size(15, 14);
            this.chkGeoExtent.TabIndex = 22;
            this.chkGeoExtent.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Country";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // tbxGeoExtent
            // 
            this.tbxGeoExtent.Enabled = false;
            this.tbxGeoExtent.Location = new System.Drawing.Point(107, 28);
            this.tbxGeoExtent.Name = "tbxGeoExtent";
            this.tbxGeoExtent.Size = new System.Drawing.Size(195, 20);
            this.tbxGeoExtent.TabIndex = 20;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(30, 63);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(71, 13);
            this.label13.TabIndex = 19;
            this.label13.Text = "Product Type";
            // 
            // cboProductType
            // 
            this.cboProductType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProductType.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboProductType.FormattingEnabled = true;
            this.cboProductType.Location = new System.Drawing.Point(125, 77);
            this.cboProductType.Name = "cboProductType";
            this.cboProductType.Size = new System.Drawing.Size(405, 21);
            this.cboProductType.TabIndex = 42;
            this.cboProductType.SelectedIndexChanged += new System.EventHandler(this.cboProductType_SelectedIndexChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(348, 130);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 24);
            this.btnCancel.TabIndex = 43;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerate.Location = new System.Drawing.Point(152, 130);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(85, 24);
            this.btnGenerate.TabIndex = 44;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // frmGenerationTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 165);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.cboProductType);
            this.Controls.Add(this.gbxCrashMoveFolder);
            //this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmGenerationTool";
            this.Text = "Map Generation Tool";
            this.Load += new System.EventHandler(this.frmGenerationTool_Load);
            this.gbxCrashMoveFolder.ResumeLayout(false);
            this.gbxCrashMoveFolder.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxCrashMoveFolder;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cboProductType;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxGeoExtent;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkGeoExtent;
    }
}