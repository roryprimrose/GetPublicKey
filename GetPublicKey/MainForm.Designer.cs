namespace GetPublicKey
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.BrowsePath = new System.Windows.Forms.Button();
            this.PublicKeyOutput = new System.Windows.Forms.TextBox();
            this.CloseForm = new System.Windows.Forms.Button();
            this.BrowseDialog = new System.Windows.Forms.OpenFileDialog();
            this.IncludeNamespace = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CopyClipboard = new System.Windows.Forms.Button();
            this.WebLink = new System.Windows.Forms.LinkLabel();
            this.TraceOutput = new System.Windows.Forms.TextBox();
            this.ShowLog = new System.Windows.Forms.CheckBox();
            this.FailureMessage = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.FailureMessage)).BeginInit();
            this.SuspendLayout();
            // 
            // BrowsePath
            // 
            this.BrowsePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BrowsePath.Location = new System.Drawing.Point(668, 12);
            this.BrowsePath.Name = "BrowsePath";
            this.BrowsePath.Size = new System.Drawing.Size(75, 23);
            this.BrowsePath.TabIndex = 0;
            this.BrowsePath.Text = "Browse";
            this.BrowsePath.UseVisualStyleBackColor = true;
            this.BrowsePath.Click += new System.EventHandler(this.BrowsePath_Click);
            // 
            // PublicKeyOutput
            // 
            this.PublicKeyOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.PublicKeyOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.PublicKeyOutput.Location = new System.Drawing.Point(12, 41);
            this.PublicKeyOutput.Multiline = true;
            this.PublicKeyOutput.Name = "PublicKeyOutput";
            this.PublicKeyOutput.ReadOnly = true;
            this.PublicKeyOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.PublicKeyOutput.Size = new System.Drawing.Size(731, 165);
            this.PublicKeyOutput.TabIndex = 1;
            // 
            // CloseForm
            // 
            this.CloseForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseForm.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseForm.Location = new System.Drawing.Point(668, 426);
            this.CloseForm.Name = "CloseForm";
            this.CloseForm.Size = new System.Drawing.Size(75, 23);
            this.CloseForm.TabIndex = 5;
            this.CloseForm.Text = "Close";
            this.CloseForm.UseVisualStyleBackColor = true;
            this.CloseForm.Click += new System.EventHandler(this.CloseForm_Click);
            // 
            // BrowseDialog
            // 
            this.BrowseDialog.DefaultExt = "snk";
            this.BrowseDialog.Filter = "All Files (*.dll; *.exe; *.snk; *.pub)|*.dll;*.exe;*.snk;*.pub|Assembly (*.dll; *" +
                ".exe)|*.dll;*.exe|Public Key (*.pub)|*.pub|Key Pair (*.snk)|*.snk";
            this.BrowseDialog.Title = "Select file to obtain public key token from";
            // 
            // IncludeNamespace
            // 
            this.IncludeNamespace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.IncludeNamespace.AutoSize = true;
            this.IncludeNamespace.Location = new System.Drawing.Point(15, 216);
            this.IncludeNamespace.Name = "IncludeNamespace";
            this.IncludeNamespace.Size = new System.Drawing.Size(119, 17);
            this.IncludeNamespace.TabIndex = 3;
            this.IncludeNamespace.Text = "Include namespace";
            this.IncludeNamespace.UseVisualStyleBackColor = true;
            this.IncludeNamespace.CheckedChanged += new System.EventHandler(this.IncludeNamespace_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(277, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Click browse to select a file to extract the public key from:";
            // 
            // CopyClipboard
            // 
            this.CopyClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CopyClipboard.Location = new System.Drawing.Point(140, 212);
            this.CopyClipboard.Name = "CopyClipboard";
            this.CopyClipboard.Size = new System.Drawing.Size(114, 23);
            this.CopyClipboard.TabIndex = 2;
            this.CopyClipboard.Text = "Copy to clipboard";
            this.CopyClipboard.UseVisualStyleBackColor = true;
            this.CopyClipboard.Click += new System.EventHandler(this.CopyClipboard_Click);
            // 
            // WebLink
            // 
            this.WebLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.WebLink.AutoSize = true;
            this.WebLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.WebLink.Location = new System.Drawing.Point(12, 439);
            this.WebLink.Name = "WebLink";
            this.WebLink.Size = new System.Drawing.Size(132, 13);
            this.WebLink.TabIndex = 6;
            this.WebLink.TabStop = true;
            this.WebLink.Text = "http://www.neovolve.com";
            this.WebLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.WebLink_LinkClicked);
            // 
            // TraceOutput
            // 
            this.TraceOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TraceOutput.Location = new System.Drawing.Point(15, 241);
            this.TraceOutput.Multiline = true;
            this.TraceOutput.Name = "TraceOutput";
            this.TraceOutput.ReadOnly = true;
            this.TraceOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TraceOutput.Size = new System.Drawing.Size(728, 179);
            this.TraceOutput.TabIndex = 7;
            // 
            // ShowLog
            // 
            this.ShowLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ShowLog.AutoSize = true;
            this.ShowLog.Checked = true;
            this.ShowLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowLog.Location = new System.Drawing.Point(673, 212);
            this.ShowLog.Name = "ShowLog";
            this.ShowLog.Size = new System.Drawing.Size(70, 17);
            this.ShowLog.TabIndex = 8;
            this.ShowLog.Text = "Show log";
            this.ShowLog.UseVisualStyleBackColor = true;
            this.ShowLog.CheckedChanged += new System.EventHandler(this.ShowLog_CheckedChanged);
            // 
            // FailureMessage
            // 
            this.FailureMessage.ContainerControl = this;
            // 
            // MainForm
            // 
            this.AcceptButton = this.BrowsePath;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CloseForm;
            this.ClientSize = new System.Drawing.Size(755, 461);
            this.Controls.Add(this.ShowLog);
            this.Controls.Add(this.TraceOutput);
            this.Controls.Add(this.WebLink);
            this.Controls.Add(this.CopyClipboard);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.IncludeNamespace);
            this.Controls.Add(this.CloseForm);
            this.Controls.Add(this.PublicKeyOutput);
            this.Controls.Add(this.BrowsePath);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(397, 241);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GetPublicKey";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FailureMessage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BrowsePath;
        private System.Windows.Forms.TextBox PublicKeyOutput;
        private System.Windows.Forms.Button CloseForm;
        private System.Windows.Forms.OpenFileDialog BrowseDialog;
        private System.Windows.Forms.CheckBox IncludeNamespace;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button CopyClipboard;
        private System.Windows.Forms.LinkLabel WebLink;
        private System.Windows.Forms.TextBox TraceOutput;
        private System.Windows.Forms.CheckBox ShowLog;
        private System.Windows.Forms.ErrorProvider FailureMessage;
    }
}

