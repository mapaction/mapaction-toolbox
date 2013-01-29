namespace Prototype1_LayoutTool
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.gbxAutomated = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnUpdateAll = new System.Windows.Forms.Button();
            this.btnMapDocument = new System.Windows.Forms.Button();
            this.btnUpdateScale = new System.Windows.Forms.Button();
            this.btnGlideNo = new System.Windows.Forms.Button();
            this.btnSpatialReference = new System.Windows.Forms.Button();
            this.txtGlideNumber = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSpatialReference = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtScale = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMapDocument = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.gbxManual = new System.Windows.Forms.GroupBox();
            this.txtMapNumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSummary = new System.Windows.Forms.TextBox();
            this.lblSummary = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tspBtnCheckElements = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tspBtnClearForm = new System.Windows.Forms.ToolStripButton();
            this.ttpGlideNumber = new System.Windows.Forms.ToolTip(this.components);
            this.gbxAutomated.SuspendLayout();
            this.gbxManual.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxAutomated
            // 
            this.gbxAutomated.Controls.Add(this.label1);
            this.gbxAutomated.Controls.Add(this.btnUpdateAll);
            this.gbxAutomated.Controls.Add(this.btnMapDocument);
            this.gbxAutomated.Controls.Add(this.btnUpdateScale);
            this.gbxAutomated.Controls.Add(this.btnGlideNo);
            this.gbxAutomated.Controls.Add(this.btnSpatialReference);
            this.gbxAutomated.Controls.Add(this.txtGlideNumber);
            this.gbxAutomated.Controls.Add(this.label9);
            this.gbxAutomated.Controls.Add(this.txtSpatialReference);
            this.gbxAutomated.Controls.Add(this.label7);
            this.gbxAutomated.Controls.Add(this.txtScale);
            this.gbxAutomated.Controls.Add(this.label4);
            this.gbxAutomated.Controls.Add(this.txtMapDocument);
            this.gbxAutomated.Controls.Add(this.label2);
            this.gbxAutomated.Location = new System.Drawing.Point(4, 260);
            this.gbxAutomated.Name = "gbxAutomated";
            this.gbxAutomated.Size = new System.Drawing.Size(352, 185);
            this.gbxAutomated.TabIndex = 43;
            this.gbxAutomated.TabStop = false;
            this.gbxAutomated.Text = "Automate update";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 50;
            this.label1.Text = "Update all elements";
            // 
            // btnUpdateAll
            // 
            this.btnUpdateAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateAll.Image = global::Prototype1_LayoutTool.Properties.Resources.import20x20px;
            this.btnUpdateAll.Location = new System.Drawing.Point(120, 24);
            this.btnUpdateAll.Name = "btnUpdateAll";
            this.btnUpdateAll.Size = new System.Drawing.Size(34, 23);
            this.btnUpdateAll.TabIndex = 34;
            this.btnUpdateAll.UseVisualStyleBackColor = true;
            this.btnUpdateAll.Click += new System.EventHandler(this.btnUpdateAll_Click);
            // 
            // btnMapDocument
            // 
            this.btnMapDocument.Image = global::Prototype1_LayoutTool.Properties.Resources.refresh17x17px;
            this.btnMapDocument.Location = new System.Drawing.Point(324, 52);
            this.btnMapDocument.Name = "btnMapDocument";
            this.btnMapDocument.Size = new System.Drawing.Size(23, 23);
            this.btnMapDocument.TabIndex = 49;
            this.btnMapDocument.UseVisualStyleBackColor = true;
            this.btnMapDocument.Click += new System.EventHandler(this.btnMapDocument_Click);
            // 
            // btnUpdateScale
            // 
            this.btnUpdateScale.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdateScale.Image")));
            this.btnUpdateScale.Location = new System.Drawing.Point(324, 112);
            this.btnUpdateScale.Name = "btnUpdateScale";
            this.btnUpdateScale.Size = new System.Drawing.Size(23, 23);
            this.btnUpdateScale.TabIndex = 48;
            this.btnUpdateScale.UseVisualStyleBackColor = true;
            this.btnUpdateScale.Click += new System.EventHandler(this.btnUpdateScale_Click);
            // 
            // btnGlideNo
            // 
            this.btnGlideNo.Image = ((System.Drawing.Image)(resources.GetObject("btnGlideNo.Image")));
            this.btnGlideNo.Location = new System.Drawing.Point(324, 142);
            this.btnGlideNo.Name = "btnGlideNo";
            this.btnGlideNo.Size = new System.Drawing.Size(23, 23);
            this.btnGlideNo.TabIndex = 47;
            this.btnGlideNo.UseVisualStyleBackColor = true;
            this.btnGlideNo.Click += new System.EventHandler(this.btnGlideNo_Click);
            // 
            // btnSpatialReference
            // 
            this.btnSpatialReference.Image = global::Prototype1_LayoutTool.Properties.Resources.refresh17x17px;
            this.btnSpatialReference.Location = new System.Drawing.Point(324, 82);
            this.btnSpatialReference.Name = "btnSpatialReference";
            this.btnSpatialReference.Size = new System.Drawing.Size(23, 23);
            this.btnSpatialReference.TabIndex = 45;
            this.btnSpatialReference.UseVisualStyleBackColor = true;
            this.btnSpatialReference.Click += new System.EventHandler(this.btnSpatialReference_Click);
            // 
            // txtGlideNumber
            // 
            this.txtGlideNumber.Location = new System.Drawing.Point(103, 145);
            this.txtGlideNumber.Name = "txtGlideNumber";
            this.txtGlideNumber.Size = new System.Drawing.Size(213, 20);
            this.txtGlideNumber.TabIndex = 43;
            this.txtGlideNumber.TextChanged += new System.EventHandler(this.txtGlideNumber_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 149);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 13);
            this.label9.TabIndex = 42;
            this.label9.Text = "Glide number";
            // 
            // txtSpatialReference
            // 
            this.txtSpatialReference.Location = new System.Drawing.Point(103, 85);
            this.txtSpatialReference.Name = "txtSpatialReference";
            this.txtSpatialReference.Size = new System.Drawing.Size(213, 20);
            this.txtSpatialReference.TabIndex = 39;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 90);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 13);
            this.label7.TabIndex = 38;
            this.label7.Text = "Spatial reference";
            // 
            // txtScale
            // 
            this.txtScale.Location = new System.Drawing.Point(103, 115);
            this.txtScale.Name = "txtScale";
            this.txtScale.Size = new System.Drawing.Size(213, 20);
            this.txtScale.TabIndex = 36;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 35;
            this.label4.Text = "Scale";
            // 
            // txtMapDocument
            // 
            this.txtMapDocument.Location = new System.Drawing.Point(103, 55);
            this.txtMapDocument.Name = "txtMapDocument";
            this.txtMapDocument.Size = new System.Drawing.Size(213, 20);
            this.txtMapDocument.TabIndex = 33;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Map document";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(195, 452);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 42;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(276, 452);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 41;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // gbxManual
            // 
            this.gbxManual.Controls.Add(this.txtMapNumber);
            this.gbxManual.Controls.Add(this.label3);
            this.gbxManual.Controls.Add(this.txtSummary);
            this.gbxManual.Controls.Add(this.lblSummary);
            this.gbxManual.Controls.Add(this.txtTitle);
            this.gbxManual.Controls.Add(this.lblTitle);
            this.gbxManual.Location = new System.Drawing.Point(4, 24);
            this.gbxManual.Name = "gbxManual";
            this.gbxManual.Size = new System.Drawing.Size(352, 224);
            this.gbxManual.TabIndex = 40;
            this.gbxManual.TabStop = false;
            this.gbxManual.Text = "Manually set elements";
            // 
            // txtMapNumber
            // 
            this.txtMapNumber.Location = new System.Drawing.Point(103, 193);
            this.txtMapNumber.Name = "txtMapNumber";
            this.txtMapNumber.Size = new System.Drawing.Size(213, 20);
            this.txtMapNumber.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 196);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Map number";
            // 
            // txtSummary
            // 
            this.txtSummary.Location = new System.Drawing.Point(103, 104);
            this.txtSummary.Multiline = true;
            this.txtSummary.Name = "txtSummary";
            this.txtSummary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSummary.Size = new System.Drawing.Size(241, 83);
            this.txtSummary.TabIndex = 3;
            // 
            // lblSummary
            // 
            this.lblSummary.AutoSize = true;
            this.lblSummary.Location = new System.Drawing.Point(6, 104);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new System.Drawing.Size(50, 13);
            this.lblSummary.TabIndex = 2;
            this.lblSummary.Text = "Summary";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(103, 21);
            this.txtTitle.Multiline = true;
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTitle.Size = new System.Drawing.Size(241, 74);
            this.txtTitle.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(6, 24);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(27, 13);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Title";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspBtnCheckElements,
            this.toolStripSeparator1,
            this.tspBtnClearForm});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip1.Size = new System.Drawing.Size(365, 25);
            this.toolStrip1.TabIndex = 44;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tspBtnCheckElements
            // 
            this.tspBtnCheckElements.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tspBtnCheckElements.Image = global::Prototype1_LayoutTool.Properties.Resources.gear_icon;
            this.tspBtnCheckElements.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspBtnCheckElements.Name = "tspBtnCheckElements";
            this.tspBtnCheckElements.Size = new System.Drawing.Size(23, 22);
            this.tspBtnCheckElements.Text = "Check template elements";
            this.tspBtnCheckElements.Click += new System.EventHandler(this.tspBtnCheckElements_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tspBtnClearForm
            // 
            this.tspBtnClearForm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tspBtnClearForm.Image = global::Prototype1_LayoutTool.Properties.Resources.clear_form_icon;
            this.tspBtnClearForm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspBtnClearForm.Name = "tspBtnClearForm";
            this.tspBtnClearForm.Size = new System.Drawing.Size(23, 22);
            this.tspBtnClearForm.Text = "Clear form";
            this.tspBtnClearForm.Click += new System.EventHandler(this.tspBtnClearForm_Click);
            // 
            // ttpGlideNumber
            // 
            this.ttpGlideNumber.AutomaticDelay = 50;
            this.ttpGlideNumber.AutoPopDelay = 10000;
            this.ttpGlideNumber.InitialDelay = 50;
            this.ttpGlideNumber.IsBalloon = true;
            this.ttpGlideNumber.ReshowDelay = 10;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 480);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.gbxAutomated);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.gbxManual);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "Layout Tool";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.gbxAutomated.ResumeLayout(false);
            this.gbxAutomated.PerformLayout();
            this.gbxManual.ResumeLayout(false);
            this.gbxManual.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxAutomated;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnUpdateAll;
        private System.Windows.Forms.Button btnMapDocument;
        private System.Windows.Forms.Button btnUpdateScale;
        private System.Windows.Forms.Button btnGlideNo;
        private System.Windows.Forms.Button btnSpatialReference;
        private System.Windows.Forms.TextBox txtGlideNumber;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtSpatialReference;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtScale;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMapDocument;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox gbxManual;
        private System.Windows.Forms.TextBox txtMapNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSummary;
        private System.Windows.Forms.Label lblSummary;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tspBtnCheckElements;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tspBtnClearForm;
        private System.Windows.Forms.ToolTip ttpGlideNumber;
    }
}