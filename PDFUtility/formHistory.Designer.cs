namespace PDFUtilityHistory
{
    partial class formHistory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formHistory));
            this.lstHistory = new System.Windows.Forms.ListView();
            this.colOriginalFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNewFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colOriginalPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNewPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colBatesPrefix = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colBatesRange = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnExportHistory = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstHistory
            // 
            this.lstHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstHistory.AutoArrange = false;
            this.lstHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colOriginalFile,
            this.colNewFile,
            this.colOriginalPath,
            this.colNewPath,
            this.colBatesPrefix,
            this.colBatesRange,
            this.colDate});
            this.lstHistory.FullRowSelect = true;
            this.lstHistory.Location = new System.Drawing.Point(12, 12);
            this.lstHistory.Name = "lstHistory";
            this.lstHistory.Size = new System.Drawing.Size(1026, 398);
            this.lstHistory.TabIndex = 0;
            this.lstHistory.UseCompatibleStateImageBehavior = false;
            this.lstHistory.View = System.Windows.Forms.View.Details;
            // 
            // colOriginalFile
            // 
            this.colOriginalFile.Text = "Original File";
            this.colOriginalFile.Width = 91;
            // 
            // colNewFile
            // 
            this.colNewFile.Text = "New File";
            this.colNewFile.Width = 73;
            // 
            // colOriginalPath
            // 
            this.colOriginalPath.Text = "Original Path";
            this.colOriginalPath.Width = 287;
            // 
            // colNewPath
            // 
            this.colNewPath.Text = "New Path";
            this.colNewPath.Width = 237;
            // 
            // colBatesPrefix
            // 
            this.colBatesPrefix.Text = "Bates Prefix";
            this.colBatesPrefix.Width = 116;
            // 
            // colBatesRange
            // 
            this.colBatesRange.Text = "Bates Range";
            this.colBatesRange.Width = 114;
            // 
            // colDate
            // 
            this.colDate.Text = "Date";
            this.colDate.Width = 89;
            // 
            // btnExportHistory
            // 
            this.btnExportHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportHistory.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExportHistory.Location = new System.Drawing.Point(831, 427);
            this.btnExportHistory.Name = "btnExportHistory";
            this.btnExportHistory.Size = new System.Drawing.Size(93, 36);
            this.btnExportHistory.TabIndex = 1;
            this.btnExportHistory.Text = "Export to Excel";
            this.btnExportHistory.UseVisualStyleBackColor = true;
            this.btnExportHistory.Click += new System.EventHandler(this.btnExportHistory_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(945, 427);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(93, 36);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // formHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.MenuBar;
            this.ClientSize = new System.Drawing.Size(1056, 483);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnExportHistory);
            this.Controls.Add(this.lstHistory);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(303, 283);
            this.Name = "formHistory";
            this.Text = "Project History";
            this.Load += new System.EventHandler(this.formHistory_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstHistory;
        private System.Windows.Forms.Button btnExportHistory;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ColumnHeader colOriginalFile;
        private System.Windows.Forms.ColumnHeader colNewFile;
        private System.Windows.Forms.ColumnHeader colOriginalPath;
        private System.Windows.Forms.ColumnHeader colNewPath;
        private System.Windows.Forms.ColumnHeader colBatesPrefix;
        private System.Windows.Forms.ColumnHeader colBatesRange;
        private System.Windows.Forms.ColumnHeader colDate;
    }
}