using System.Diagnostics;
using System.Collections.Generic;
using System;

namespace MapActionToolbars
{
    partial class frmLayoutMain
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
        private List<MapAction.LanguageConfig> languageDictionary;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLayoutMain));
            this.gbxAutomated = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnUpdateAll = new System.Windows.Forms.Button();
            this.btnGlideNo = new System.Windows.Forms.Button();
            this.btnSpatialReference = new System.Windows.Forms.Button();
            this.tbxGlideNumber = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbxSpatialReference = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.gbxManual = new System.Windows.Forms.GroupBox();
            this.nudVersionNumber = new System.Windows.Forms.NumericUpDown();
            this.labelVersionNumber = new System.Windows.Forms.Label();
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
            this.eprMapNumberError = new System.Windows.Forms.ErrorProvider(this.components);
            this.eprMapTitle = new System.Windows.Forms.ErrorProvider(this.components);
            this.eprMapDocumentWarning = new System.Windows.Forms.ErrorProvider(this.components);
            this.eprMapSummary = new System.Windows.Forms.ErrorProvider(this.components);
            this.eprDataSources = new System.Windows.Forms.ErrorProvider(this.components);
            this.eprSpatialReferenceWarning = new System.Windows.Forms.ErrorProvider(this.components);
            this.eprGlideNumberWarning = new System.Windows.Forms.ErrorProvider(this.components);
            this.eprMapNumberWarning = new System.Windows.Forms.ErrorProvider(this.components);
            this.eprGlideNumberError = new System.Windows.Forms.ErrorProvider(this.components);
            this.eprSpatialReferenceError = new System.Windows.Forms.ErrorProvider(this.components);
            this.eprScaleTextWarning = new System.Windows.Forms.ErrorProvider(this.components);
            this.eprMapDocumentError = new System.Windows.Forms.ErrorProvider(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboLabelLanguage = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btnUpdateTimezone = new System.Windows.Forms.Button();
            this.tbxTimezone = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnUpdateAllTab2 = new System.Windows.Forms.Button();
            this.btnUpdateDisclaimer = new System.Windows.Forms.Button();
            this.btnUpdateProducedBy = new System.Windows.Forms.Button();
            this.btnUpdateDonorCredits = new System.Windows.Forms.Button();
            this.tbxMapProducer = new System.Windows.Forms.TextBox();
            this.lblOrganisation = new System.Windows.Forms.Label();
            this.tbxDonorCredit = new System.Windows.Forms.TextBox();
            this.lblDonorCredits = new System.Windows.Forms.Label();
            this.tbxDisclaimer = new System.Windows.Forms.TextBox();
            this.lblDisclaimer = new System.Windows.Forms.Label();
            this.eprDisclaimerWarning = new System.Windows.Forms.ErrorProvider(this.components);
            this.eprDisclaimerError = new System.Windows.Forms.ErrorProvider(this.components);
            this.eprDonorWarning = new System.Windows.Forms.ErrorProvider(this.components);
            this.eprDonorError = new System.Windows.Forms.ErrorProvider(this.components);
            this.eprProducedByWarning = new System.Windows.Forms.ErrorProvider(this.components);
            this.eprTimezoneWarning = new System.Windows.Forms.ErrorProvider(this.components);
            this.eprProducedByError = new System.Windows.Forms.ErrorProvider(this.components);
            this.eprTimezoneError = new System.Windows.Forms.ErrorProvider(this.components);
            this.gbxAutomated.SuspendLayout();
            this.gbxManual.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudVersionNumber)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eprMapNumberError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprMapTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprMapDocumentWarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprMapSummary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprDataSources)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprSpatialReferenceWarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprGlideNumberWarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprMapNumberWarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprGlideNumberError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprSpatialReferenceError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprScaleTextWarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprMapDocumentError)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eprDisclaimerWarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprDisclaimerError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprDonorWarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprDonorError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprProducedByWarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprTimezoneWarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprProducedByError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprTimezoneError)).BeginInit();
            this.SuspendLayout();
            // 
            // gbxAutomated
            // 
            this.gbxAutomated.Controls.Add(this.label1);
            this.gbxAutomated.Controls.Add(this.btnUpdateAll);
            this.gbxAutomated.Controls.Add(this.btnGlideNo);
            this.gbxAutomated.Controls.Add(this.btnSpatialReference);
            this.gbxAutomated.Controls.Add(this.tbxGlideNumber);
            this.gbxAutomated.Controls.Add(this.label9);
            this.gbxAutomated.Controls.Add(this.tbxSpatialReference);
            this.gbxAutomated.Controls.Add(this.label7);
            this.gbxAutomated.Location = new System.Drawing.Point(6, 266);
            this.gbxAutomated.Name = "gbxAutomated";
            this.gbxAutomated.Size = new System.Drawing.Size(352, 177);
            this.gbxAutomated.TabIndex = 43;
            this.gbxAutomated.TabStop = false;
            this.gbxAutomated.Text = "Automate update";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 50;
            this.label1.Text = "Refresh all from map";
            // 
            // btnUpdateAll
            // 
            this.btnUpdateAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateAll.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdateAll.Image")));
            this.btnUpdateAll.Location = new System.Drawing.Point(115, 24);
            this.btnUpdateAll.Name = "btnUpdateAll";
            this.btnUpdateAll.Size = new System.Drawing.Size(34, 23);
            this.btnUpdateAll.TabIndex = 5;
            this.btnUpdateAll.UseVisualStyleBackColor = true;
            this.btnUpdateAll.Click += new System.EventHandler(this.btnUpdateAll_Click);
            // 
            // btnGlideNo
            // 
            this.btnGlideNo.Image = ((System.Drawing.Image)(resources.GetObject("btnGlideNo.Image")));
            this.btnGlideNo.Location = new System.Drawing.Point(304, 81);
            this.btnGlideNo.Name = "btnGlideNo";
            this.btnGlideNo.Size = new System.Drawing.Size(23, 23);
            this.btnGlideNo.TabIndex = 13;
            this.btnGlideNo.UseVisualStyleBackColor = true;
            this.btnGlideNo.Click += new System.EventHandler(this.btnGlideNo_Click);
            // 
            // btnSpatialReference
            // 
            this.btnSpatialReference.Image = ((System.Drawing.Image)(resources.GetObject("btnSpatialReference.Image")));
            this.btnSpatialReference.Location = new System.Drawing.Point(304, 51);
            this.btnSpatialReference.Name = "btnSpatialReference";
            this.btnSpatialReference.Size = new System.Drawing.Size(23, 23);
            this.btnSpatialReference.TabIndex = 9;
            this.btnSpatialReference.UseVisualStyleBackColor = true;
            this.btnSpatialReference.Click += new System.EventHandler(this.btnSpatialReference_Click);
            // 
            // tbxGlideNumber
            // 
            this.tbxGlideNumber.Location = new System.Drawing.Point(103, 83);
            this.tbxGlideNumber.Name = "tbxGlideNumber";
            this.tbxGlideNumber.Size = new System.Drawing.Size(195, 20);
            this.tbxGlideNumber.TabIndex = 12;
            this.tbxGlideNumber.TextChanged += new System.EventHandler(this.tbxGlideNumber_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 87);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 13);
            this.label9.TabIndex = 42;
            this.label9.Text = "Glide number";
            // 
            // tbxSpatialReference
            // 
            this.tbxSpatialReference.Location = new System.Drawing.Point(103, 54);
            this.tbxSpatialReference.Name = "tbxSpatialReference";
            this.tbxSpatialReference.Size = new System.Drawing.Size(195, 20);
            this.tbxSpatialReference.TabIndex = 8;
            this.tbxSpatialReference.TextChanged += new System.EventHandler(this.tbxSpatialReference_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 59);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 13);
            this.label7.TabIndex = 38;
            this.label7.Text = "Spatial reference";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(299, 515);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(218, 515);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "Update";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // gbxManual
            // 
            this.gbxManual.Controls.Add(this.nudVersionNumber);
            this.gbxManual.Controls.Add(this.labelVersionNumber);
            this.gbxManual.Controls.Add(this.tbxDataSources);
            this.gbxManual.Controls.Add(this.label5);
            this.gbxManual.Controls.Add(this.tbxMapNumber);
            this.gbxManual.Controls.Add(this.label3);
            this.gbxManual.Controls.Add(this.tbxSummary);
            this.gbxManual.Controls.Add(this.lblSummary);
            this.gbxManual.Controls.Add(this.tbxTitle);
            this.gbxManual.Controls.Add(this.lblTitle);
            this.gbxManual.Location = new System.Drawing.Point(6, 12);
            this.gbxManual.Name = "gbxManual";
            this.gbxManual.Size = new System.Drawing.Size(352, 248);
            this.gbxManual.TabIndex = 40;
            this.gbxManual.TabStop = false;
            this.gbxManual.Text = "Manually set elements";
            // 
            // nudVersionNumber
            // 
            this.nudVersionNumber.Location = new System.Drawing.Point(263, 213);
            this.nudVersionNumber.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudVersionNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudVersionNumber.Name = "nudVersionNumber";
            this.nudVersionNumber.Size = new System.Drawing.Size(64, 20);
            this.nudVersionNumber.TabIndex = 31;
            this.nudVersionNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labelVersionNumber
            // 
            this.labelVersionNumber.AutoSize = true;
            this.labelVersionNumber.Location = new System.Drawing.Point(216, 215);
            this.labelVersionNumber.Name = "labelVersionNumber";
            this.labelVersionNumber.Size = new System.Drawing.Size(42, 13);
            this.labelVersionNumber.TabIndex = 32;
            this.labelVersionNumber.Text = "Version";
            // 
            // tbxDataSources
            // 
            this.tbxDataSources.Location = new System.Drawing.Point(103, 148);
            this.tbxDataSources.Multiline = true;
            this.tbxDataSources.Name = "tbxDataSources";
            this.tbxDataSources.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxDataSources.Size = new System.Drawing.Size(224, 58);
            this.tbxDataSources.TabIndex = 3;
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
            this.tbxMapNumber.Size = new System.Drawing.Size(107, 20);
            this.tbxMapNumber.TabIndex = 4;
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
            this.tbxSummary.TabIndex = 2;
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
            this.toolStrip1.Size = new System.Drawing.Size(397, 25);
            this.toolStrip1.TabIndex = 44;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tspBtnCheckElements
            // 
            this.tspBtnCheckElements.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tspBtnCheckElements.Image = ((System.Drawing.Image)(resources.GetObject("tspBtnCheckElements.Image")));
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
            this.tspBtnClearForm.Image = ((System.Drawing.Image)(resources.GetObject("tspBtnClearForm.Image")));
            this.tspBtnClearForm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspBtnClearForm.Name = "tspBtnClearForm";
            this.tspBtnClearForm.Size = new System.Drawing.Size(23, 22);
            this.tspBtnClearForm.Text = "Clear form";
            this.tspBtnClearForm.ToolTipText = "Clear form";
            this.tspBtnClearForm.Click += new System.EventHandler(this.tspBtnClearForm_Click);
            // 
            // eprMapNumberError
            // 
            this.eprMapNumberError.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.eprMapNumberError.ContainerControl = this;
            // 
            // eprMapTitle
            // 
            this.eprMapTitle.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.eprMapTitle.ContainerControl = this;
            this.eprMapTitle.Icon = ((System.Drawing.Icon)(resources.GetObject("eprMapTitle.Icon")));
            // 
            // eprMapDocumentWarning
            // 
            this.eprMapDocumentWarning.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.eprMapDocumentWarning.ContainerControl = this;
            this.eprMapDocumentWarning.Icon = ((System.Drawing.Icon)(resources.GetObject("eprMapDocumentWarning.Icon")));
            // 
            // eprMapSummary
            // 
            this.eprMapSummary.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.eprMapSummary.ContainerControl = this;
            this.eprMapSummary.Icon = ((System.Drawing.Icon)(resources.GetObject("eprMapSummary.Icon")));
            // 
            // eprDataSources
            // 
            this.eprDataSources.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.eprDataSources.ContainerControl = this;
            this.eprDataSources.Icon = ((System.Drawing.Icon)(resources.GetObject("eprDataSources.Icon")));
            // 
            // eprSpatialReferenceWarning
            // 
            this.eprSpatialReferenceWarning.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.eprSpatialReferenceWarning.ContainerControl = this;
            this.eprSpatialReferenceWarning.Icon = ((System.Drawing.Icon)(resources.GetObject("eprSpatialReferenceWarning.Icon")));
            // 
            // eprGlideNumberWarning
            // 
            this.eprGlideNumberWarning.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.eprGlideNumberWarning.ContainerControl = this;
            this.eprGlideNumberWarning.Icon = ((System.Drawing.Icon)(resources.GetObject("eprGlideNumberWarning.Icon")));
            // 
            // eprMapNumberWarning
            // 
            this.eprMapNumberWarning.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.eprMapNumberWarning.ContainerControl = this;
            this.eprMapNumberWarning.Icon = ((System.Drawing.Icon)(resources.GetObject("eprMapNumberWarning.Icon")));
            // 
            // eprGlideNumberError
            // 
            this.eprGlideNumberError.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.eprGlideNumberError.ContainerControl = this;
            // 
            // eprSpatialReferenceError
            // 
            this.eprSpatialReferenceError.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.eprSpatialReferenceError.ContainerControl = this;
            // 
            // eprScaleTextWarning
            // 
            this.eprScaleTextWarning.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.eprScaleTextWarning.ContainerControl = this;
            this.eprScaleTextWarning.Icon = ((System.Drawing.Icon)(resources.GetObject("eprScaleTextWarning.Icon")));
            // 
            // eprMapDocumentError
            // 
            this.eprMapDocumentError.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.eprMapDocumentError.ContainerControl = this;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 28);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(373, 481);
            this.tabControl1.TabIndex = 45;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gbxAutomated);
            this.tabPage1.Controls.Add(this.gbxManual);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(365, 455);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Common";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(365, 455);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Standard";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboLabelLanguage);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.btnUpdateTimezone);
            this.groupBox1.Controls.Add(this.tbxTimezone);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.btnUpdateAllTab2);
            this.groupBox1.Controls.Add(this.btnUpdateDisclaimer);
            this.groupBox1.Controls.Add(this.btnUpdateProducedBy);
            this.groupBox1.Controls.Add(this.btnUpdateDonorCredits);
            this.groupBox1.Controls.Add(this.tbxMapProducer);
            this.groupBox1.Controls.Add(this.lblOrganisation);
            this.groupBox1.Controls.Add(this.tbxDonorCredit);
            this.groupBox1.Controls.Add(this.lblDonorCredits);
            this.groupBox1.Controls.Add(this.tbxDisclaimer);
            this.groupBox1.Controls.Add(this.lblDisclaimer);
            this.groupBox1.Location = new System.Drawing.Point(7, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(352, 311);
            this.groupBox1.TabIndex = 41;
            this.groupBox1.TabStop = false;
            // 
            // cboLabelLanguage
            // 
            this.cboLabelLanguage.AccessibleDescription = "";
            this.cboLabelLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLabelLanguage.Location = new System.Drawing.Point(94, 273);
            this.cboLabelLanguage.Name = "cboLabelLanguage";
            this.cboLabelLanguage.Size = new System.Drawing.Size(201, 21);
            this.cboLabelLanguage.TabIndex = 114;
            this.cboLabelLanguage.SelectedIndexChanged += new System.EventHandler(this.cboLabelLanguage_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 277);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(84, 13);
            this.label12.TabIndex = 113;
            this.label12.Text = "Label Language";
            // 
            // btnUpdateTimezone
            // 
            this.btnUpdateTimezone.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdateTimezone.Image")));
            this.btnUpdateTimezone.Location = new System.Drawing.Point(304, 239);
            this.btnUpdateTimezone.Name = "btnUpdateTimezone";
            this.btnUpdateTimezone.Size = new System.Drawing.Size(23, 23);
            this.btnUpdateTimezone.TabIndex = 109;
            this.btnUpdateTimezone.UseVisualStyleBackColor = true;
            this.btnUpdateTimezone.Click += new System.EventHandler(this.btnUpdateTimezone_Click);
            // 
            // tbxTimezone
            // 
            this.tbxTimezone.Location = new System.Drawing.Point(91, 241);
            this.tbxTimezone.Name = "tbxTimezone";
            this.tbxTimezone.Size = new System.Drawing.Size(207, 20);
            this.tbxTimezone.TabIndex = 108;
            this.tbxTimezone.TextChanged += new System.EventHandler(this.tbxTimezone_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 244);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 55;
            this.label8.Text = "Time zone";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 13);
            this.label6.TabIndex = 54;
            this.label6.Text = "Refresh all from map";
            // 
            // btnUpdateAllTab2
            // 
            this.btnUpdateAllTab2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateAllTab2.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdateAllTab2.Image")));
            this.btnUpdateAllTab2.Location = new System.Drawing.Point(120, 19);
            this.btnUpdateAllTab2.Name = "btnUpdateAllTab2";
            this.btnUpdateAllTab2.Size = new System.Drawing.Size(34, 23);
            this.btnUpdateAllTab2.TabIndex = 101;
            this.btnUpdateAllTab2.UseVisualStyleBackColor = true;
            this.btnUpdateAllTab2.Click += new System.EventHandler(this.btnUpdateAllTab2_Click);
            // 
            // btnUpdateDisclaimer
            // 
            this.btnUpdateDisclaimer.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdateDisclaimer.Image")));
            this.btnUpdateDisclaimer.Location = new System.Drawing.Point(304, 67);
            this.btnUpdateDisclaimer.Name = "btnUpdateDisclaimer";
            this.btnUpdateDisclaimer.Size = new System.Drawing.Size(23, 23);
            this.btnUpdateDisclaimer.TabIndex = 103;
            this.btnUpdateDisclaimer.UseVisualStyleBackColor = true;
            this.btnUpdateDisclaimer.Click += new System.EventHandler(this.btnUpdateDisclaimer_Click);
            // 
            // btnUpdateProducedBy
            // 
            this.btnUpdateProducedBy.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdateProducedBy.Image")));
            this.btnUpdateProducedBy.Location = new System.Drawing.Point(304, 195);
            this.btnUpdateProducedBy.Name = "btnUpdateProducedBy";
            this.btnUpdateProducedBy.Size = new System.Drawing.Size(23, 23);
            this.btnUpdateProducedBy.TabIndex = 107;
            this.btnUpdateProducedBy.UseVisualStyleBackColor = true;
            this.btnUpdateProducedBy.Click += new System.EventHandler(this.btnUpdateOrganisationDetails_Click);
            // 
            // btnUpdateDonorCredits
            // 
            this.btnUpdateDonorCredits.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdateDonorCredits.Image")));
            this.btnUpdateDonorCredits.Location = new System.Drawing.Point(304, 131);
            this.btnUpdateDonorCredits.Name = "btnUpdateDonorCredits";
            this.btnUpdateDonorCredits.Size = new System.Drawing.Size(23, 23);
            this.btnUpdateDonorCredits.TabIndex = 105;
            this.btnUpdateDonorCredits.UseVisualStyleBackColor = true;
            this.btnUpdateDonorCredits.Click += new System.EventHandler(this.btnUpdateDonorCredits_Click);
            // 
            // tbxMapProducer
            // 
            this.tbxMapProducer.Location = new System.Drawing.Point(91, 177);
            this.tbxMapProducer.Multiline = true;
            this.tbxMapProducer.Name = "tbxMapProducer";
            this.tbxMapProducer.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxMapProducer.Size = new System.Drawing.Size(207, 58);
            this.tbxMapProducer.TabIndex = 106;
            this.tbxMapProducer.TextChanged += new System.EventHandler(this.tbxMapProducer_TextChanged);
            // 
            // lblOrganisation
            // 
            this.lblOrganisation.AutoSize = true;
            this.lblOrganisation.Location = new System.Drawing.Point(6, 180);
            this.lblOrganisation.Name = "lblOrganisation";
            this.lblOrganisation.Size = new System.Drawing.Size(73, 13);
            this.lblOrganisation.TabIndex = 8;
            this.lblOrganisation.Text = "Map producer";
            // 
            // tbxDonorCredit
            // 
            this.tbxDonorCredit.Location = new System.Drawing.Point(91, 113);
            this.tbxDonorCredit.Multiline = true;
            this.tbxDonorCredit.Name = "tbxDonorCredit";
            this.tbxDonorCredit.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxDonorCredit.Size = new System.Drawing.Size(207, 58);
            this.tbxDonorCredit.TabIndex = 104;
            this.tbxDonorCredit.TextChanged += new System.EventHandler(this.tbxDonorCredit_TextChanged);
            // 
            // lblDonorCredits
            // 
            this.lblDonorCredits.AutoSize = true;
            this.lblDonorCredits.Location = new System.Drawing.Point(6, 113);
            this.lblDonorCredits.Name = "lblDonorCredits";
            this.lblDonorCredits.Size = new System.Drawing.Size(70, 13);
            this.lblDonorCredits.TabIndex = 2;
            this.lblDonorCredits.Text = "Donor credits";
            // 
            // tbxDisclaimer
            // 
            this.tbxDisclaimer.Location = new System.Drawing.Point(91, 49);
            this.tbxDisclaimer.Multiline = true;
            this.tbxDisclaimer.Name = "tbxDisclaimer";
            this.tbxDisclaimer.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxDisclaimer.Size = new System.Drawing.Size(207, 58);
            this.tbxDisclaimer.TabIndex = 102;
            this.tbxDisclaimer.TextChanged += new System.EventHandler(this.tbxDisclaimer_TextChanged);
            // 
            // lblDisclaimer
            // 
            this.lblDisclaimer.AutoSize = true;
            this.lblDisclaimer.Location = new System.Drawing.Point(6, 52);
            this.lblDisclaimer.Name = "lblDisclaimer";
            this.lblDisclaimer.Size = new System.Drawing.Size(55, 13);
            this.lblDisclaimer.TabIndex = 0;
            this.lblDisclaimer.Text = "Disclaimer";
            // 
            // eprDisclaimerWarning
            // 
            this.eprDisclaimerWarning.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.eprDisclaimerWarning.ContainerControl = this;
            this.eprDisclaimerWarning.Icon = ((System.Drawing.Icon)(resources.GetObject("eprDisclaimerWarning.Icon")));
            // 
            // eprDisclaimerError
            // 
            this.eprDisclaimerError.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.eprDisclaimerError.ContainerControl = this;
            // 
            // eprDonorWarning
            // 
            this.eprDonorWarning.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.eprDonorWarning.ContainerControl = this;
            this.eprDonorWarning.Icon = ((System.Drawing.Icon)(resources.GetObject("eprDonorWarning.Icon")));
            // 
            // eprDonorError
            // 
            this.eprDonorError.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.eprDonorError.ContainerControl = this;
            // 
            // eprProducedByWarning
            // 
            this.eprProducedByWarning.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.eprProducedByWarning.ContainerControl = this;
            this.eprProducedByWarning.Icon = ((System.Drawing.Icon)(resources.GetObject("eprProducedByWarning.Icon")));
            // 
            // eprTimezoneWarning
            // 
            this.eprTimezoneWarning.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.eprTimezoneWarning.ContainerControl = this;
            this.eprTimezoneWarning.Icon = ((System.Drawing.Icon)(resources.GetObject("eprTimezoneWarning.Icon")));
            // 
            // eprProducedByError
            // 
            this.eprProducedByError.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.eprProducedByError.ContainerControl = this;
            // 
            // eprTimezoneError
            // 
            this.eprTimezoneError.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.eprTimezoneError.ContainerControl = this;
            // 
            // frmLayoutMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 544);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLayoutMain";
            this.Text = "Layout Tool";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.gbxAutomated.ResumeLayout(false);
            this.gbxAutomated.PerformLayout();
            this.gbxManual.ResumeLayout(false);
            this.gbxManual.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudVersionNumber)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eprMapNumberError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprMapTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprMapDocumentWarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprMapSummary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprDataSources)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprSpatialReferenceWarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprGlideNumberWarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprMapNumberWarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprGlideNumberError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprSpatialReferenceError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprScaleTextWarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprMapDocumentError)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eprDisclaimerWarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprDisclaimerError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprDonorWarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprDonorError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprProducedByWarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprTimezoneWarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprProducedByError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eprTimezoneError)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxAutomated;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnUpdateAll;
        private System.Windows.Forms.Button btnGlideNo;
        private System.Windows.Forms.Button btnSpatialReference;
        private System.Windows.Forms.TextBox tbxGlideNumber;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbxSpatialReference;
        private System.Windows.Forms.Label label7;
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
        private System.Windows.Forms.TextBox tbxDataSources;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ErrorProvider eprMapNumberError;
        private System.Windows.Forms.ErrorProvider eprMapTitle;
        private System.Windows.Forms.ErrorProvider eprMapDocumentWarning;
        private System.Windows.Forms.ErrorProvider eprMapSummary;
        private System.Windows.Forms.ErrorProvider eprDataSources;
        private System.Windows.Forms.ErrorProvider eprSpatialReferenceWarning;
        private System.Windows.Forms.ErrorProvider eprGlideNumberWarning;
        private System.Windows.Forms.ErrorProvider eprMapNumberWarning;
        private System.Windows.Forms.ErrorProvider eprGlideNumberError;
        private System.Windows.Forms.ErrorProvider eprSpatialReferenceError;
        private System.Windows.Forms.ErrorProvider eprScaleTextWarning;
        private System.Windows.Forms.ErrorProvider eprMapDocumentError;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbxDonorCredit;
        private System.Windows.Forms.Label lblDonorCredits;
        private System.Windows.Forms.TextBox tbxDisclaimer;
        private System.Windows.Forms.Label lblDisclaimer;
        private System.Windows.Forms.Label lblOrganisation;
        private System.Windows.Forms.Button btnUpdateDisclaimer;
        private System.Windows.Forms.Button btnUpdateProducedBy;
        private System.Windows.Forms.Button btnUpdateDonorCredits;
        private System.Windows.Forms.TextBox tbxMapProducer;
        private System.Windows.Forms.TextBox tbxTimezone;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnUpdateAllTab2;
        private System.Windows.Forms.Button btnUpdateTimezone;
        private System.Windows.Forms.ErrorProvider eprDisclaimerWarning;
        private System.Windows.Forms.ErrorProvider eprDisclaimerError;
        private System.Windows.Forms.ErrorProvider eprDonorWarning;
        private System.Windows.Forms.ErrorProvider eprDonorError;
        private System.Windows.Forms.ErrorProvider eprProducedByWarning;
        private System.Windows.Forms.ErrorProvider eprTimezoneWarning;
        private System.Windows.Forms.ErrorProvider eprProducedByError;
        private System.Windows.Forms.ErrorProvider eprTimezoneError;
        private System.Windows.Forms.ComboBox cboLabelLanguage;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown nudVersionNumber;
        private System.Windows.Forms.Label labelVersionNumber;
    }
}