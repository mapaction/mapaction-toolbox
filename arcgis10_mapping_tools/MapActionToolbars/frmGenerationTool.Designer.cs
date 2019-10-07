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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblProductClassification = new System.Windows.Forms.Label();
            this.cboClassification = new System.Windows.Forms.ComboBox();
            this.tbxGeoExtent = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cboProductType = new System.Windows.Forms.ComboBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblProductClassification);
            this.groupBox1.Controls.Add(this.cboClassification);
            this.groupBox1.Controls.Add(this.tbxGeoExtent);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cboProductType);
            this.groupBox1.Location = new System.Drawing.Point(18, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(650, 147);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Generate";
            // 
            // lblProductClassification
            // 
            this.lblProductClassification.AutoSize = true;
            this.lblProductClassification.Location = new System.Drawing.Point(30, 69);
            this.lblProductClassification.Name = "lblProductClassification";
            this.lblProductClassification.Size = new System.Drawing.Size(68, 13);
            this.lblProductClassification.TabIndex = 7;
            this.lblProductClassification.Text = "Classification";
            this.lblProductClassification.Click += new System.EventHandler(this.label3_Click);
            // 
            // cboClassification
            // 
            this.cboClassification.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClassification.FormattingEnabled = true;
            this.cboClassification.Location = new System.Drawing.Point(125, 65);
            this.cboClassification.Name = "cboClassification";
            this.cboClassification.Size = new System.Drawing.Size(482, 21);
            this.cboClassification.TabIndex = 6;
            this.cboClassification.SelectedIndexChanged += new System.EventHandler(this.cbxClassification_SelectedIndexChanged);
            // 
            // tbxGeoExtent
            // 
            this.tbxGeoExtent.Location = new System.Drawing.Point(125, 27);
            this.tbxGeoExtent.Name = "tbxGeoExtent";
            this.tbxGeoExtent.ReadOnly = true;
            this.tbxGeoExtent.Size = new System.Drawing.Size(222, 20);
            this.tbxGeoExtent.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Product Type";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Country";
            // 
            // cboProductType
            // 
            this.cboProductType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProductType.FormattingEnabled = true;
            this.cboProductType.Location = new System.Drawing.Point(125, 101);
            this.cboProductType.Name = "cboProductType";
            this.cboProductType.Size = new System.Drawing.Size(482, 21);
            this.cboProductType.TabIndex = 0;
            this.cboProductType.SelectedIndexChanged += new System.EventHandler(this.cbxProductType_SelectedIndexChanged);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(229, 175);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.TabIndex = 1;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(379, 175);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 2;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // frmGenerationTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 220);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmGenerationTool";
            this.Text = "Map Automation Tool";
            this.Load += new System.EventHandler(this.frmGenerationTool_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboProductType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxGeoExtent;
        private System.Windows.Forms.Label lblProductClassification;
        private System.Windows.Forms.ComboBox cboClassification;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button Cancel;
    }
}