namespace GetPublicKey
{
    partial class Form1
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
            this.BrowsePath = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.CloseForm = new System.Windows.Forms.Button();
            this.BrowseDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // BrowsePath
            // 
            this.BrowsePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BrowsePath.Location = new System.Drawing.Point(397, 12);
            this.BrowsePath.Name = "BrowsePath";
            this.BrowsePath.Size = new System.Drawing.Size(75, 23);
            this.BrowsePath.TabIndex = 0;
            this.BrowsePath.Text = "Browse";
            this.BrowsePath.UseVisualStyleBackColor = true;
            this.BrowsePath.Click += new System.EventHandler(this.BrowsePath_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 41);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(460, 223);
            this.textBox1.TabIndex = 1;
            // 
            // CloseForm
            // 
            this.CloseForm.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseForm.Location = new System.Drawing.Point(397, 336);
            this.CloseForm.Name = "CloseForm";
            this.CloseForm.Size = new System.Drawing.Size(75, 23);
            this.CloseForm.TabIndex = 2;
            this.CloseForm.Text = "Close";
            this.CloseForm.UseVisualStyleBackColor = true;
            // 
            // BrowseDialog
            // 
            this.BrowseDialog.DefaultExt = "snk";
            this.BrowseDialog.Filter = "Assembly (*.dll, *.exe)|*.dll,*.exe|Public Key (*.pub)|*.pub|Key Pair (*.snk)|*.s" +
                "nk";
            this.BrowseDialog.FilterIndex = 2;
            this.BrowseDialog.Title = "Select file to obtain public key token from";
            // 
            // Form1
            // 
            this.AcceptButton = this.BrowsePath;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CloseForm;
            this.ClientSize = new System.Drawing.Size(484, 371);
            this.Controls.Add(this.CloseForm);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.BrowsePath);
            this.Name = "Form1";
            this.Text = "GetPublicKey";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BrowsePath;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button CloseForm;
        private System.Windows.Forms.OpenFileDialog BrowseDialog;
    }
}

