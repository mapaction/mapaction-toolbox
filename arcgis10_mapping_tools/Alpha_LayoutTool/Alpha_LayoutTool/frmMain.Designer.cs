namespace Alpha_LayoutTool
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
            this.tbxGlideNumber = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbxSpatialReference = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbxScale = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxMapDocument = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.gbxManual = new System.Windows.Forms.GroupBox();
            this.tbxDataSources = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxMapNumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxSummary = new System.Windows.Forms.TextBox();
            this.lblSummary = new System.Windows.Forms.Label();
            this.tbxTitle = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tspBtnCheckElements = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tspBtnClearForm = new System.Windows.Forms.ToolStripButton();
            this.ttpGlideNumber = new System.Windows.Forms.ToolTip(this.components);
            this.eprMapNumber = new System.Windows.Forms.ErrorProvider(this.components);
            this.eprMapTitle = new System.Windows.Forms.ErrorProvider(this.components);
            this.eprMapDocument = new System.Windows.Forms.ErrorProvider(this.components);
            this.eprMapSummary = new System.Windows.Forms.ErrorProvider(this.components);
            this.eprDataSources = new System.Windows.Forms.ErrorProvider(this.components);
            this.eprSpatialReference = new System.Windows.Forms.ErrorProvider(this.components);
            this.eprScaleText = new System.Windows.Forms.ErrorProvider(this.components);
            this.eprGlideNumber = new System.Windows.Forms.ErrorProvider(this.components);
            this.gbxAutomated.SuspendLayout();
            this.gbxManual.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eprMapNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprMapTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprMapDocument)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprMapSummary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprDataSources)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprSpatialReference)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprScaleText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprGlideNumber)).BeginInit();
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
            this.gbxAutomated.Controls.Add(this.tbxGlideNumber);
            this.gbxAutomated.Controls.Add(this.label9);
            this.gbxAutomated.Controls.Add(this.tbxSpatialReference);
            this.gbxAutomated.Controls.Add(this.label7);
            this.gbxAutomated.Controls.Add(this.tbxScale);
            this.gbxAutomated.Controls.Add(this.label4);
            this.gbxAutomated.Controls.Add(this.tbxMapDocument);
            this.gbxAutomated.Controls.Add(this.label2);
            this.gbxAutomated.Location = new System.Drawing.Point(4, 278);
            this.gbxAutomated.Name = "gbxAutomated";
            this.gbxAutomated.Size = new System.Drawing.Size(352, 177);
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
            this.btnUpdateAll.Image = global::Alpha_LayoutTool.Properties.Resources.import20x20px;
            this.btnUpdateAll.Location = new System.Drawing.Point(120, 24);
            this.btnUpdateAll.Name = "btnUpdateAll";
            this.btnUpdateAll.Size = new System.Drawing.Size(34, 23);
            this.btnUpdateAll.TabIndex = 34;
            this.btnUpdateAll.UseVisualStyleBackColor = true;
            this.btnUpdateAll.Click += new System.EventHandler(this.btnUpdateAll_Click);
            // 
            // btnMapDocument
            // 
            this.btnMapDocument.Image = global::Alpha_LayoutTool.Properties.Resources.refresh17x17px;
            this.btnMapDocument.Location = new System.Drawing.Point(304, 53);
            this.btnMapDocument.Name = "btnMapDocument";
            this.btnMapDocument.Size = new System.Drawing.Size(23, 23);
            this.btnMapDocument.TabIndex = 49;
            this.btnMapDocument.UseVisualStyleBackColor = true;
            this.btnMapDocument.Click += new System.EventHandler(this.btnMapDocument_Click);
            // 
            // btnUpdateScale
            // 
            this.btnUpdateScale.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdateScale.Image")));
            this.btnUpdateScale.Location = new System.Drawing.Point(304, 113);
            this.btnUpdateScale.Name = "btnUpdateScale";
            this.btnUpdateScale.Size = new System.Drawing.Size(23, 23);
            this.btnUpdateScale.TabIndex = 48;
            this.btnUpdateScale.UseVisualStyleBackColor = true;
            this.btnUpdateScale.Click += new System.EventHandler(this.btnUpdateScale_Click);
            // 
            // btnGlideNo
            // 
            this.btnGlideNo.Image = ((System.Drawing.Image)(resources.GetObject("btnGlideNo.Image")));
            this.btnGlideNo.Location = new System.Drawing.Point(304, 143);
            this.btnGlideNo.Name = "btnGlideNo";
            this.btnGlideNo.Size = new System.Drawing.Size(23, 23);
            this.btnGlideNo.TabIndex = 47;
            this.btnGlideNo.UseVisualStyleBackColor = true;
            this.btnGlideNo.Click += new System.EventHandler(this.btnGlideNo_Click);
            // 
            // btnSpatialReference
            // 
            this.btnSpatialReference.Image = global::Alpha_LayoutTool.Properties.Resources.refresh17x17px;
            this.btnSpatialReference.Location = new System.Drawing.Point(304, 82);
            this.btnSpatialReference.Name = "btnSpatialReference";
            this.btnSpatialReference.Size = new System.Drawing.Size(23, 23);
            this.btnSpatialReference.TabIndex = 45;
            this.btnSpatialReference.UseVisualStyleBackColor = true;
            this.btnSpatialReference.Click += new System.EventHandler(this.btnSpatialReference_Click);
            // 
            // tbxGlideNumber
            // 
            this.tbxGlideNumber.Location = new System.Drawing.Point(103, 145);
            this.tbxGlideNumber.Name = "tbxGlideNumber";
            this.tbxGlideNumber.Size = new System.Drawing.Size(195, 20);
            this.tbxGlideNumber.TabIndex = 43;
            this.tbxGlideNumber.TextChanged += new System.EventHandler(this.tbxGlideNumber_TextChanged);
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
            // tbxSpatialReference
            // 
            this.tbxSpatialReference.Location = new System.Drawing.Point(103, 85);
            this.tbxSpatialReference.Name = "tbxSpatialReference";
            this.tbxSpatialReference.Size = new System.Drawing.Size(195, 20);
            this.tbxSpatialReference.TabIndex = 39;
            this.tbxSpatialReference.TextChanged += new System.EventHandler(this.tbxSpatialReference_TextChanged);
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
            // tbxScale
            // 
            this.tbxScale.Location = new System.Drawing.Point(103, 115);
            this.tbxScale.Name = "tbxScale";
            this.tbxScale.Size = new System.Drawing.Size(195, 20);
            this.tbxScale.TabIndex = 36;
            this.tbxScale.TextChanged += new System.EventHandler(this.tbxScale_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 35;
            this.label4.Text = "Scale text";
            // 
            // tbxMapDocument
            // 
            this.tbxMapDocument.Location = new System.Drawing.Point(103, 55);
            this.tbxMapDocument.Name = "tbxMapDocument";
            this.tbxMapDocument.Size = new System.Drawing.Size(195, 20);
            this.tbxMapDocument.TabIndex = 33;
            this.tbxMapDocument.TextChanged += new System.EventHandler(this.tbxMapDocument_TextChanged);
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
            this.btnCancel.Location = new System.Drawing.Point(195, 461);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 42;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(276, 461);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 41;
            this.btnSave.Text = "Update";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // gbxManual
            // 
            this.gbxManual.Controls.Add(this.tbxDataSources);
            this.gbxManual.Controls.Add(this.label5);
            this.gbxManual.Controls.Add(this.tbxMapNumber);
            this.gbxManual.Controls.Add(this.label3);
            this.gbxManual.Controls.Add(this.tbxSummary);
            this.gbxManual.Controls.Add(this.lblSummary);
            this.gbxManual.Controls.Add(this.tbxTitle);
            this.gbxManual.Controls.Add(this.lblTitle);
            this.gbxManual.Location = new System.Drawing.Point(4, 24);
            this.gbxManual.Name = "gbxManual";
            this.gbxManual.Size = new System.Drawing.Size(352, 248);
            this.gbxManual.TabIndex = 40;
            this.gbxManual.TabStop = false;
            this.gbxManual.Text = "Manually set elements";
            // 
            // tbxDataSources
            // 
            this.tbxDataSources.Location = new System.Drawing.Point(103, 148);
            this.tbxDataSources.Multiline = true;
            this.tbxDataSources.Name = "tbxDataSources";
            this.tbxDataSources.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxDataSources.Size = new System.Drawing.Size(224, 58);
            this.tbxDataSources.TabIndex = 9;
            this.tbxDataSources.TextChanged += new System.EventHandler(this.tbxDataSources_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Data sources";
            // 
            // tbxMapNumber
            // 
            this.tbxMapNumber.Location = new System.Drawing.Point(103, 213);
            this.tbxMapNumber.Name = "tbxMapNumber";
            this.tbxMapNumber.Size = new System.Drawing.Size(224, 20);
            this.tbxMapNumber.TabIndex = 7;
            this.tbxMapNumber.TextChanged += new System.EventHandler(this.tbxMapNumber_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 216);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Map number";
            // 
            // tbxSummary
            // 
            this.tbxSummary.Location = new System.Drawing.Point(103, 85);
            this.tbxSummary.Multiline = true;
            this.tbxSummary.Name = "tbxSummary";
            this.tbxSummary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxSummary.Size = new System.Drawing.Size(224, 58);
            this.tbxSummary.TabIndex = 3;
            this.tbxSummary.TextChanged += new System.EventHandler(this.tbxSummary_TextChanged);
            // 
            // lblSummary
            // 
            this.lblSummary.AutoSize = true;
            this.lblSummary.Location = new System.Drawing.Point(6, 85);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new System.Drawing.Size(50, 13);
            this.lblSummary.TabIndex = 2;
            this.lblSummary.Text = "Summary";
            // 
            // tbxTitle
            // 
            this.tbxTitle.Location = new System.Drawing.Point(103, 21);
            this.tbxTitle.Multiline = true;
            this.tbxTitle.Name = "tbxTitle";
            this.tbxTitle.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxTitle.Size = new System.Drawing.Size(224, 58);
            this.tbxTitle.TabIndex = 1;
            this.tbxTitle.TextChanged += new System.EventHandler(this.tbxTitle_TextChanged);
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
            this.tspBtnCheckElements.Image = global::Alpha_LayoutTool.Properties.Resources.gear_icon;
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
            this.tspBtnClearForm.Image = global::Alpha_LayoutTool.Properties.Resources.clear_form_icon;
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
            // eprMapNumber
            // 
            this.eprMapNumber.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.eprMapNumber.ContainerControl = this;
            // 
            // eprMapTitle
            // 
            this.eprMapTitle.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.eprMapTitle.ContainerControl = this;
            // 
            // eprMapDocument
            // 
            this.eprMapDocument.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.eprMapDocument.ContainerControl = this;
            // 
            // eprMapSummary
            // 
            this.eprMapSummary.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.eprMapSummary.ContainerControl = this;
            // 
            // eprDataSources
            // 
            this.eprDataSources.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.eprDataSources.ContainerControl = this;
            // 
            // eprSpatialReference
            // 
            this.eprSpatialReference.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.eprSpatialReference.ContainerControl = this;
            // 
            // eprScaleText
            // 
            this.eprScaleText.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.eprScaleText.ContainerControl = this;
            // 
            // eprGlideNumber
            // 
            this.eprGlideNumber.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.eprGlideNumber.ContainerControl = this;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 489);
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
            ((System.ComponentModel.ISupportInitialize)(this.eprMapNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprMapTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprMapDocument)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprMapSummary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprDataSources)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprSpatialReference)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprScaleText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprGlideNumber)).EndInit();
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
        private System.Windows.Forms.TextBox tbxGlideNumber;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbxSpatialReference;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbxScale;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxMapDocument;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox gbxManual;
        private System.Windows.Forms.TextBox tbxMapNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxSummary;
        private System.Windows.Forms.Label lblSummary;
        private System.Windows.Forms.TextBox tbxTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tspBtnCheckElements;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tspBtnClearForm;
        private System.Windows.Forms.ToolTip ttpGlideNumber;
        private System.Windows.Forms.TextBox tbxDataSources;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ErrorProvider eprMapNumber;
        private System.Windows.Forms.ErrorProvider eprMapTitle;
        private System.Windows.Forms.ErrorProvider eprMapDocument;
        private System.Windows.Forms.ErrorProvider eprMapSummary;
        private System.Windows.Forms.ErrorProvider eprDataSources;
        private System.Windows.Forms.ErrorProvider eprSpatialReference;
        private System.Windows.Forms.ErrorProvider eprScaleText;
        private System.Windows.Forms.ErrorProvider eprGlideNumber;
    }
}