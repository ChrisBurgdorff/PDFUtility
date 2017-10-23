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
            this.btnExportHistory = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.colOriginalFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNewFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colOriginalPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNewPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colBatesPrefix = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colBatesRange = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lstHistory
            // 
            this.lstHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colOriginalFile,
            this.colNewFile,
            this.colOriginalPath,
            this.colNewPath,
            this.colBatesPrefix,
            this.colBatesRange,
            this.colDate});
            this.lstHistory.Location = new System.Drawing.Point(12, 12);
            this.lstHistory.Name = "lstHistory";
            this.lstHistory.Size = new System.Drawing.Size(685, 297);
            this.lstHistory.TabIndex = 0;
            this.lstHistory.UseCompatibleStateImageBehavior = false;
            // 
            // btnExportHistory
            // 
            this.btnExportHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportHistory.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExportHistory.Location = new System.Drawing.Point(509, 315);
            this.btnExportHistory.Name = "btnExportHistory";
            this.btnExportHistory.Size = new System.Drawing.Size(93, 36);
            this.btnExportHistory.TabIndex = 1;
            this.btnExportHistory.Text = "Export to Excel";
            this.btnExportHistory.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(622, 315);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 36);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // colOriginalFile
            // 
            this.colOriginalFile.Text = "Original File";
            // 
            // colNewFile
            // 
            this.colNewFile.Text = "New File";
            // 
            // colOriginalPath
            // 
            this.colOriginalPath.Text = "Original Path";
            // 
            // colNewPath
            // 
            this.colNewPath.Text = "New Path";
            // 
            // colBatesPrefix
            // 
            this.colBatesPrefix.Text = "Bates Prefix";
            // 
            // colBatesRange
            // 
            this.colBatesRange.Text = "Bates Range";
            // 
            // colDate
            // 
            this.colDate.Text = "Date";
            // 
            // formHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.MenuBar;
            this.ClientSize = new System.Drawing.Size(715, 371);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnExportHistory);
            this.Controls.Add(this.lstHistory);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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