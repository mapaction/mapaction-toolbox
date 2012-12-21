namespace Prototype1_DatasetRename
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnRename = new System.Windows.Forms.Button();
            this.tbxInputPath = new System.Windows.Forms.TextBox();
            this.tbxNewName = new System.Windows.Forms.TextBox();
            this.btnInputPath = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Input file";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "New name";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(132, 64);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnRename
            // 
            this.btnRename.Location = new System.Drawing.Point(213, 64);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(75, 23);
            this.btnRename.TabIndex = 3;
            this.btnRename.Text = "Rename";
            this.btnRename.UseVisualStyleBackColor = true;
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // tbxInputPath
            // 
            this.tbxInputPath.Location = new System.Drawing.Point(74, 12);
            this.tbxInputPath.Name = "tbxInputPath";
            this.tbxInputPath.Size = new System.Drawing.Size(194, 20);
            this.tbxInputPath.TabIndex = 4;
            // 
            // tbxNewName
            // 
            this.tbxNewName.Location = new System.Drawing.Point(74, 38);
            this.tbxNewName.Name = "tbxNewName";
            this.tbxNewName.Size = new System.Drawing.Size(194, 20);
            this.tbxNewName.TabIndex = 5;
            // 
            // btnInputPath
            // 
            this.btnInputPath.Location = new System.Drawing.Point(275, 12);
            this.btnInputPath.Name = "btnInputPath";
            this.btnInputPath.Size = new System.Drawing.Size(25, 20);
            this.btnInputPath.TabIndex = 6;
            this.btnInputPath.Text = "...";
            this.btnInputPath.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 96);
            this.Controls.Add(this.btnInputPath);
            this.Controls.Add(this.tbxNewName);
            this.Controls.Add(this.tbxInputPath);
            this.Controls.Add(this.btnRename);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.Text = "Rename dataset";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnRename;
        private System.Windows.Forms.TextBox tbxInputPath;
        private System.Windows.Forms.TextBox tbxNewName;
        private System.Windows.Forms.Button btnInputPath;
    }
}