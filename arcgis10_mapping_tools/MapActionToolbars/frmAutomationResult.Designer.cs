namespace MapActionToolbar_Forms
{
    partial class frmAutomationResult
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAutomationResult));
            this.detailGroupBox = new System.Windows.Forms.GroupBox();
            this.automationResultGridView = new System.Windows.Forms.DataGridView();
            this.closeButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.detailGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.automationResultGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // detailGroupBox
            // 
            this.detailGroupBox.Controls.Add(this.automationResultGridView);
            this.detailGroupBox.Location = new System.Drawing.Point(13, 116);
            this.detailGroupBox.Name = "detailGroupBox";
            this.detailGroupBox.Size = new System.Drawing.Size(1318, 192);
            this.detailGroupBox.TabIndex = 0;
            this.detailGroupBox.TabStop = false;
            this.detailGroupBox.Text = "Detail";
            // 
            // automationResultGridView
            // 
            this.automationResultGridView.AllowUserToAddRows = false;
            this.automationResultGridView.AllowUserToDeleteRows = false;
            this.automationResultGridView.AllowUserToResizeColumns = false;
            this.automationResultGridView.AllowUserToResizeRows = false;
            this.automationResultGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.automationResultGridView.Location = new System.Drawing.Point(18, 24);
            this.automationResultGridView.Name = "automationResultGridView";
            this.automationResultGridView.ReadOnly = true;
            this.automationResultGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.automationResultGridView.ShowCellErrors = false;
            this.automationResultGridView.ShowEditingIcon = false;
            this.automationResultGridView.Size = new System.Drawing.Size(1282, 150);
            this.automationResultGridView.TabIndex = 0;
            this.automationResultGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.automationResultGridView_CellContentClick);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(622, 320);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 1;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1318, 88);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Summary";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(85, 20);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(1215, 50);
            this.textBox1.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(18, 20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 50);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // frmAutomationResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1343, 351);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.detailGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAutomationResult";
            this.Text = "Automation Results";
            this.detailGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.automationResultGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox detailGroupBox;
        private System.Windows.Forms.DataGridView automationResultGridView;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBox1;
    }
}