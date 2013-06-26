namespace Prototype1_ConfigTool
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnPathToExistingXml = new System.Windows.Forms.Button();
            this.tbxPathToExistingXml = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdoNewXml = new System.Windows.Forms.RadioButton();
            this.rdoPathXml = new System.Windows.Forms.RadioButton();
            this.rdoEditXml = new System.Windows.Forms.RadioButton();
            this.btnGetEditXmlPath = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btnCreateNewXmlDoc = new System.Windows.Forms.Button();
            this.lblCreateNewXml = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(356, 134);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(272, 134);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnPathToExistingXml);
            this.groupBox1.Controls.Add(this.tbxPathToExistingXml);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.btnGetEditXmlPath);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btnCreateNewXmlDoc);
            this.groupBox1.Controls.Add(this.lblCreateNewXml);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(425, 116);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Operation XML";
            // 
            // btnPathToExistingXml
            // 
            this.btnPathToExistingXml.Location = new System.Drawing.Point(386, 20);
            this.btnPathToExistingXml.Name = "btnPathToExistingXml";
            this.btnPathToExistingXml.Size = new System.Drawing.Size(33, 23);
            this.btnPathToExistingXml.TabIndex = 18;
            this.btnPathToExistingXml.Text = "...";
            this.btnPathToExistingXml.UseVisualStyleBackColor = true;
            this.btnPathToExistingXml.Click += new System.EventHandler(this.btnPathToExistingXml_Click);
            // 
            // tbxPathToExistingXml
            // 
            this.tbxPathToExistingXml.Location = new System.Drawing.Point(141, 22);
            this.tbxPathToExistingXml.Name = "tbxPathToExistingXml";
            this.tbxPathToExistingXml.Size = new System.Drawing.Size(239, 20);
            this.tbxPathToExistingXml.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Path to config file";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rdoNewXml);
            this.panel1.Controls.Add(this.rdoPathXml);
            this.panel1.Controls.Add(this.rdoEditXml);
            this.panel1.Location = new System.Drawing.Point(6, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(26, 80);
            this.panel1.TabIndex = 16;
            // 
            // rdoNewXml
            // 
            this.rdoNewXml.AutoSize = true;
            this.rdoNewXml.Location = new System.Drawing.Point(8, 64);
            this.rdoNewXml.Name = "rdoNewXml";
            this.rdoNewXml.Size = new System.Drawing.Size(14, 13);
            this.rdoNewXml.TabIndex = 15;
            this.rdoNewXml.UseVisualStyleBackColor = true;
            this.rdoNewXml.CheckedChanged += new System.EventHandler(this.rdoNewXml_CheckedChanged);
            // 
            // rdoPathXml
            // 
            this.rdoPathXml.AutoSize = true;
            this.rdoPathXml.Checked = true;
            this.rdoPathXml.Location = new System.Drawing.Point(8, 3);
            this.rdoPathXml.Name = "rdoPathXml";
            this.rdoPathXml.Size = new System.Drawing.Size(14, 13);
            this.rdoPathXml.TabIndex = 10;
            this.rdoPathXml.TabStop = true;
            this.rdoPathXml.UseVisualStyleBackColor = true;
            this.rdoPathXml.CheckedChanged += new System.EventHandler(this.rdoPathXml_CheckedChanged);
            // 
            // rdoEditXml
            // 
            this.rdoEditXml.AutoSize = true;
            this.rdoEditXml.Location = new System.Drawing.Point(8, 34);
            this.rdoEditXml.Name = "rdoEditXml";
            this.rdoEditXml.Size = new System.Drawing.Size(14, 13);
            this.rdoEditXml.TabIndex = 14;
            this.rdoEditXml.UseVisualStyleBackColor = true;
            this.rdoEditXml.CheckedChanged += new System.EventHandler(this.rdoEditXml_CheckedChanged);
            // 
            // btnGetEditXmlPath
            // 
            this.btnGetEditXmlPath.Enabled = false;
            this.btnGetEditXmlPath.Location = new System.Drawing.Point(141, 48);
            this.btnGetEditXmlPath.Name = "btnGetEditXmlPath";
            this.btnGetEditXmlPath.Size = new System.Drawing.Size(68, 23);
            this.btnGetEditXmlPath.TabIndex = 3;
            this.btnGetEditXmlPath.Text = "Edit";
            this.btnGetEditXmlPath.UseVisualStyleBackColor = true;
            this.btnGetEditXmlPath.Click += new System.EventHandler(this.btnGetEditXmlPath_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(38, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Edit in use xml";
            // 
            // btnCreateNewXmlDoc
            // 
            this.btnCreateNewXmlDoc.Enabled = false;
            this.btnCreateNewXmlDoc.Location = new System.Drawing.Point(141, 78);
            this.btnCreateNewXmlDoc.Name = "btnCreateNewXmlDoc";
            this.btnCreateNewXmlDoc.Size = new System.Drawing.Size(68, 23);
            this.btnCreateNewXmlDoc.TabIndex = 1;
            this.btnCreateNewXmlDoc.Text = "Create";
            this.btnCreateNewXmlDoc.UseVisualStyleBackColor = true;
            this.btnCreateNewXmlDoc.Click += new System.EventHandler(this.btnCreateNewXmlDoc_Click);
            // 
            // lblCreateNewXml
            // 
            this.lblCreateNewXml.AutoSize = true;
            this.lblCreateNewXml.Location = new System.Drawing.Point(38, 83);
            this.lblCreateNewXml.Name = "lblCreateNewXml";
            this.lblCreateNewXml.Size = new System.Drawing.Size(77, 13);
            this.lblCreateNewXml.TabIndex = 8;
            this.lblCreateNewXml.Text = "Create new file";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 167);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "Configuration Tool";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnPathToExistingXml;
        private System.Windows.Forms.TextBox tbxPathToExistingXml;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rdoPathXml;
        private System.Windows.Forms.RadioButton rdoEditXml;
        private System.Windows.Forms.Button btnGetEditXmlPath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnCreateNewXmlDoc;
        private System.Windows.Forms.Label lblCreateNewXml;
        private System.Windows.Forms.RadioButton rdoNewXml;
    }
}