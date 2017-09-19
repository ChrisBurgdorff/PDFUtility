namespace PDFUtility
{
    partial class formMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formMain));
            this.btnAddFilesBates2 = new System.Windows.Forms.Button();
            this.lblEmptyList = new System.Windows.Forms.Label();
            this.lstBatesFiles = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblStartAt = new System.Windows.Forms.Label();
            this.txtStartNumber = new System.Windows.Forms.TextBox();
            this.lblBatesPrefix = new System.Windows.Forms.Label();
            this.chkSubfolders = new System.Windows.Forms.CheckBox();
            this.btnBatesStamp = new System.Windows.Forms.Button();
            this.txtBatesPrefix = new System.Windows.Forms.TextBox();
            this.btnSelectFolderBates = new System.Windows.Forms.Button();
            this.lblFolderBates = new System.Windows.Forms.Label();
            this.txtFolderBates = new System.Windows.Forms.TextBox();
            this.dialogFolderBates = new System.Windows.Forms.FolderBrowserDialog();
            this.dialogFileBates = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNewProject = new System.Windows.Forms.ToolStripButton();
            this.btnOpenProject = new System.Windows.Forms.ToolStripButton();
            this.btnSaveProject = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAddFilesBates = new System.Windows.Forms.ToolStripButton();
            this.btnAddFolderBates = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnOptions = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.progBarBates = new System.Windows.Forms.ToolStripProgressBar();
            this.statusTextBates = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnSelectOutput = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddFilesBates2
            // 
            this.btnAddFilesBates2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddFilesBates2.Location = new System.Drawing.Point(522, 161);
            this.btnAddFilesBates2.Name = "btnAddFilesBates2";
            this.btnAddFilesBates2.Size = new System.Drawing.Size(122, 23);
            this.btnAddFilesBates2.TabIndex = 12;
            this.btnAddFilesBates2.Text = "Add Files";
            this.btnAddFilesBates2.UseVisualStyleBackColor = true;
            this.btnAddFilesBates2.Click += new System.EventHandler(this.btnAddFilesBates2_Click);
            // 
            // lblEmptyList
            // 
            this.lblEmptyList.AutoSize = true;
            this.lblEmptyList.BackColor = System.Drawing.SystemColors.Window;
            this.lblEmptyList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmptyList.Location = new System.Drawing.Point(52, 251);
            this.lblEmptyList.Name = "lblEmptyList";
            this.lblEmptyList.Size = new System.Drawing.Size(429, 20);
            this.lblEmptyList.TabIndex = 11;
            this.lblEmptyList.Text = "Drag and drop files here, or click \"Add Folder\" or \"Add Files\"";
            // 
            // lstBatesFiles
            // 
            this.lstBatesFiles.AllowDrop = true;
            this.lstBatesFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstBatesFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.lstBatesFiles.Location = new System.Drawing.Point(35, 222);
            this.lstBatesFiles.Name = "lstBatesFiles";
            this.lstBatesFiles.Size = new System.Drawing.Size(1007, 241);
            this.lstBatesFiles.TabIndex = 10;
            this.lstBatesFiles.UseCompatibleStateImageBehavior = false;
            this.lstBatesFiles.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lstBatesFiles_ItemDrag);
            this.lstBatesFiles.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstBatesFiles_KeyUp);
            this.lstBatesFiles.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstBatesFiles_MouseDown);
            this.lstBatesFiles.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lstBatesFiles_MouseUp);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "File Number";
            this.columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "File";
            this.columnHeader2.Width = 120;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Pages";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Bates Range";
            this.columnHeader4.Width = 100;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Full Path";
            this.columnHeader5.Width = 150;
            // 
            // lblStartAt
            // 
            this.lblStartAt.AutoSize = true;
            this.lblStartAt.Location = new System.Drawing.Point(75, 171);
            this.lblStartAt.Name = "lblStartAt";
            this.lblStartAt.Size = new System.Drawing.Size(74, 13);
            this.lblStartAt.TabIndex = 8;
            this.lblStartAt.Text = "Start at Bates:";
            // 
            // txtStartNumber
            // 
            this.txtStartNumber.Location = new System.Drawing.Point(155, 171);
            this.txtStartNumber.Name = "txtStartNumber";
            this.txtStartNumber.Size = new System.Drawing.Size(46, 20);
            this.txtStartNumber.TabIndex = 2;
            this.txtStartNumber.Text = "1";
            this.txtStartNumber.TextChanged += new System.EventHandler(this.txtStartNumber_TextChanged);
            // 
            // lblBatesPrefix
            // 
            this.lblBatesPrefix.AutoSize = true;
            this.lblBatesPrefix.Location = new System.Drawing.Point(35, 135);
            this.lblBatesPrefix.Name = "lblBatesPrefix";
            this.lblBatesPrefix.Size = new System.Drawing.Size(114, 13);
            this.lblBatesPrefix.TabIndex = 6;
            this.lblBatesPrefix.Text = "Bates Prefix (Optional):";
            // 
            // chkSubfolders
            // 
            this.chkSubfolders.AutoSize = true;
            this.chkSubfolders.Location = new System.Drawing.Point(787, 93);
            this.chkSubfolders.Name = "chkSubfolders";
            this.chkSubfolders.Size = new System.Drawing.Size(114, 17);
            this.chkSubfolders.TabIndex = 5;
            this.chkSubfolders.Text = "Include Subfolders";
            this.chkSubfolders.UseVisualStyleBackColor = true;
            // 
            // btnBatesStamp
            // 
            this.btnBatesStamp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBatesStamp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBatesStamp.Location = new System.Drawing.Point(897, 469);
            this.btnBatesStamp.Name = "btnBatesStamp";
            this.btnBatesStamp.Size = new System.Drawing.Size(145, 50);
            this.btnBatesStamp.TabIndex = 3;
            this.btnBatesStamp.Text = "Bates Stamp";
            this.btnBatesStamp.UseVisualStyleBackColor = true;
            this.btnBatesStamp.Click += new System.EventHandler(this.btnBatesStamp_Click);
            // 
            // txtBatesPrefix
            // 
            this.txtBatesPrefix.Location = new System.Drawing.Point(155, 135);
            this.txtBatesPrefix.Name = "txtBatesPrefix";
            this.txtBatesPrefix.Size = new System.Drawing.Size(285, 20);
            this.txtBatesPrefix.TabIndex = 1;
            // 
            // btnSelectFolderBates
            // 
            this.btnSelectFolderBates.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSelectFolderBates.Location = new System.Drawing.Point(721, 161);
            this.btnSelectFolderBates.Name = "btnSelectFolderBates";
            this.btnSelectFolderBates.Size = new System.Drawing.Size(123, 20);
            this.btnSelectFolderBates.TabIndex = 4;
            this.btnSelectFolderBates.Text = "Add Folder";
            this.btnSelectFolderBates.UseVisualStyleBackColor = true;
            this.btnSelectFolderBates.Click += new System.EventHandler(this.btnSelectFolderBates_Click);
            // 
            // lblFolderBates
            // 
            this.lblFolderBates.AutoSize = true;
            this.lblFolderBates.Location = new System.Drawing.Point(86, 94);
            this.lblFolderBates.Name = "lblFolderBates";
            this.lblFolderBates.Size = new System.Drawing.Size(71, 13);
            this.lblFolderBates.TabIndex = 1;
            this.lblFolderBates.Text = "Ouput Folder:";
            // 
            // txtFolderBates
            // 
            this.txtFolderBates.Location = new System.Drawing.Point(155, 94);
            this.txtFolderBates.Name = "txtFolderBates";
            this.txtFolderBates.Size = new System.Drawing.Size(285, 20);
            this.txtFolderBates.TabIndex = 0;
            // 
            // dialogFolderBates
            // 
            this.dialogFolderBates.HelpRequest += new System.EventHandler(this.folderBrowserDialog1_HelpRequest);
            // 
            // dialogFileBates
            // 
            this.dialogFileBates.Filter = "PDF files|*.pdf|All files|*.*";
            this.dialogFileBates.Multiselect = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1090, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(36, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNewProject,
            this.btnOpenProject,
            this.btnSaveProject,
            this.toolStripSeparator1,
            this.btnAddFilesBates,
            this.btnAddFolderBates,
            this.toolStripSeparator2,
            this.btnOptions});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1090, 23);
            this.toolStrip1.TabIndex = 13;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnNewProject
            // 
            this.btnNewProject.Image = ((System.Drawing.Image)(resources.GetObject("btnNewProject.Image")));
            this.btnNewProject.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnNewProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewProject.Name = "btnNewProject";
            this.btnNewProject.Size = new System.Drawing.Size(95, 20);
            this.btnNewProject.Text = "New Project";
            // 
            // btnOpenProject
            // 
            this.btnOpenProject.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenProject.Image")));
            this.btnOpenProject.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnOpenProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpenProject.Name = "btnOpenProject";
            this.btnOpenProject.Size = new System.Drawing.Size(57, 20);
            this.btnOpenProject.Text = "Open";
            this.btnOpenProject.Click += new System.EventHandler(this.btnOpenProject_Click);
            // 
            // btnSaveProject
            // 
            this.btnSaveProject.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveProject.Image")));
            this.btnSaveProject.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSaveProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveProject.Name = "btnSaveProject";
            this.btnSaveProject.Size = new System.Drawing.Size(53, 20);
            this.btnSaveProject.Text = "Save";
            this.btnSaveProject.Click += new System.EventHandler(this.btnSaveProject_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
            // 
            // btnAddFilesBates
            // 
            this.btnAddFilesBates.Image = ((System.Drawing.Image)(resources.GetObject("btnAddFilesBates.Image")));
            this.btnAddFilesBates.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAddFilesBates.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddFilesBates.Name = "btnAddFilesBates";
            this.btnAddFilesBates.Size = new System.Drawing.Size(75, 20);
            this.btnAddFilesBates.Text = "Add Files";
            this.btnAddFilesBates.Click += new System.EventHandler(this.btnAddFilesBates_Click_1);
            // 
            // btnAddFolderBates
            // 
            this.btnAddFolderBates.Image = ((System.Drawing.Image)(resources.GetObject("btnAddFolderBates.Image")));
            this.btnAddFolderBates.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAddFolderBates.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddFolderBates.Name = "btnAddFolderBates";
            this.btnAddFolderBates.Size = new System.Drawing.Size(86, 20);
            this.btnAddFolderBates.Text = "Add Folder";
            this.btnAddFolderBates.Click += new System.EventHandler(this.btnAddFolderBates_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 23);
            // 
            // btnOptions
            // 
            this.btnOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnOptions.Image")));
            this.btnOptions.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Size = new System.Drawing.Size(69, 20);
            this.btnOptions.Text = "Options";
            this.btnOptions.Click += new System.EventHandler(this.btnOptions_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progBarBates,
            this.statusTextBates});
            this.statusStrip1.Location = new System.Drawing.Point(0, 528);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1090, 22);
            this.statusStrip1.TabIndex = 14;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // progBarBates
            // 
            this.progBarBates.Name = "progBarBates";
            this.progBarBates.Size = new System.Drawing.Size(100, 16);
            // 
            // statusTextBates
            // 
            this.statusTextBates.Name = "statusTextBates";
            this.statusTextBates.Size = new System.Drawing.Size(110, 17);
            this.statusTextBates.Text = "Add Files to Stamp";
            // 
            // btnSelectOutput
            // 
            this.btnSelectOutput.Location = new System.Drawing.Point(468, 92);
            this.btnSelectOutput.Name = "btnSelectOutput";
            this.btnSelectOutput.Size = new System.Drawing.Size(176, 21);
            this.btnSelectOutput.TabIndex = 15;
            this.btnSelectOutput.Text = "Select Output Folder";
            this.btnSelectOutput.UseVisualStyleBackColor = true;
            this.btnSelectOutput.Click += new System.EventHandler(this.btnSelectOutput_Click);
            // 
            // formMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.MenuBar;
            this.ClientSize = new System.Drawing.Size(1090, 550);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.btnSelectOutput);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnBatesStamp);
            this.Controls.Add(this.lblEmptyList);
            this.Controls.Add(this.btnAddFilesBates2);
            this.Controls.Add(this.lstBatesFiles);
            this.Controls.Add(this.chkSubfolders);
            this.Controls.Add(this.lblStartAt);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.txtStartNumber);
            this.Controls.Add(this.btnSelectFolderBates);
            this.Controls.Add(this.lblBatesPrefix);
            this.Controls.Add(this.txtFolderBates);
            this.Controls.Add(this.txtBatesPrefix);
            this.Controls.Add(this.lblFolderBates);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "formMain";
            this.Text = "PDF Utility";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.FolderBrowserDialog dialogFolderBates;
        private System.Windows.Forms.Button btnSelectFolderBates;
        private System.Windows.Forms.Label lblFolderBates;
        private System.Windows.Forms.TextBox txtFolderBates;
        private System.Windows.Forms.Label lblStartAt;
        private System.Windows.Forms.TextBox txtStartNumber;
        private System.Windows.Forms.Label lblBatesPrefix;
        private System.Windows.Forms.CheckBox chkSubfolders;
        private System.Windows.Forms.Button btnBatesStamp;
        private System.Windows.Forms.TextBox txtBatesPrefix;
        private System.Windows.Forms.ListView lstBatesFiles;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Label lblEmptyList;
        private System.Windows.Forms.Button btnAddFilesBates2;
        private System.Windows.Forms.OpenFileDialog dialogFileBates;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar progBarBates;
        private System.Windows.Forms.ToolStripStatusLabel statusTextBates;
        private System.Windows.Forms.ToolStripButton btnNewProject;
        private System.Windows.Forms.ToolStripButton btnOpenProject;
        private System.Windows.Forms.ToolStripButton btnSaveProject;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnAddFilesBates;
        private System.Windows.Forms.ToolStripButton btnAddFolderBates;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnOptions;
        private System.Windows.Forms.Button btnSelectOutput;
    }
}

