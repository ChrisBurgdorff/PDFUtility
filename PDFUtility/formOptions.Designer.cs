namespace PDFUtilityOptions
{
    partial class formOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formOptions));
            this.dialogFontBates = new System.Windows.Forms.FontDialog();
            this.pnlLocation = new System.Windows.Forms.Panel();
            this.trackTransparency = new System.Windows.Forms.TrackBar();
            this.lblSampleText = new System.Windows.Forms.Label();
            this.lblTransparency = new System.Windows.Forms.Label();
            this.comboBoxLocation = new System.Windows.Forms.ComboBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.lblTransparencyNumber = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkSmartStamp = new System.Windows.Forms.CheckBox();
            this.lblSmartStamp = new System.Windows.Forms.Label();
            this.comboBoxFont = new System.Windows.Forms.ComboBox();
            this.lblFont = new System.Windows.Forms.Label();
            this.chkBold = new System.Windows.Forms.CheckBox();
            this.chkItalic = new System.Windows.Forms.CheckBox();
            this.btnResetDefault = new System.Windows.Forms.Button();
            this.comboBoxFontSize = new System.Windows.Forms.ComboBox();
            this.lblSize = new System.Windows.Forms.Label();
            this.groupBoxFont = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackTransparency)).BeginInit();
            this.groupBoxFont.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlLocation
            // 
            this.pnlLocation.BackColor = System.Drawing.Color.White;
            this.pnlLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLocation.Location = new System.Drawing.Point(475, 89);
            this.pnlLocation.Name = "pnlLocation";
            this.pnlLocation.Size = new System.Drawing.Size(204, 312);
            this.pnlLocation.TabIndex = 0;
            // 
            // trackTransparency
            // 
            this.trackTransparency.Location = new System.Drawing.Point(41, 258);
            this.trackTransparency.Maximum = 100;
            this.trackTransparency.Name = "trackTransparency";
            this.trackTransparency.Size = new System.Drawing.Size(215, 45);
            this.trackTransparency.TabIndex = 2;
            this.trackTransparency.TickFrequency = 10;
            this.trackTransparency.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackTransparency.Value = 100;
            this.trackTransparency.ValueChanged += new System.EventHandler(this.trackTransparency_ValueChanged);
            // 
            // lblSampleText
            // 
            this.lblSampleText.AutoSize = true;
            this.lblSampleText.Location = new System.Drawing.Point(88, 87);
            this.lblSampleText.Name = "lblSampleText";
            this.lblSampleText.Size = new System.Drawing.Size(73, 13);
            this.lblSampleText.TabIndex = 3;
            this.lblSampleText.Text = "Aa Bb Cc 123";
            // 
            // lblTransparency
            // 
            this.lblTransparency.AutoSize = true;
            this.lblTransparency.Location = new System.Drawing.Point(109, 306);
            this.lblTransparency.Name = "lblTransparency";
            this.lblTransparency.Size = new System.Drawing.Size(72, 13);
            this.lblTransparency.TabIndex = 4;
            this.lblTransparency.Text = "Transparency";
            // 
            // comboBoxLocation
            // 
            this.comboBoxLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLocation.FormattingEnabled = true;
            this.comboBoxLocation.Items.AddRange(new object[] {
            "Lower Right",
            "Lower Left",
            "Upper Right",
            "Upper Left",
            "Center",
            "Center Bottom",
            "Center Top"});
            this.comboBoxLocation.Location = new System.Drawing.Point(112, 37);
            this.comboBoxLocation.Name = "comboBoxLocation";
            this.comboBoxLocation.Size = new System.Drawing.Size(150, 21);
            this.comboBoxLocation.TabIndex = 5;
            this.comboBoxLocation.SelectedIndexChanged += new System.EventHandler(this.comboBoxLocation_SelectedIndexChanged);
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(14, 37);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(84, 13);
            this.lblLocation.TabIndex = 6;
            this.lblLocation.Text = "Stamp Location:";
            // 
            // lblTransparencyNumber
            // 
            this.lblTransparencyNumber.AutoSize = true;
            this.lblTransparencyNumber.Location = new System.Drawing.Point(196, 306);
            this.lblTransparencyNumber.Name = "lblTransparencyNumber";
            this.lblTransparencyNumber.Size = new System.Drawing.Size(13, 13);
            this.lblTransparencyNumber.TabIndex = 7;
            this.lblTransparencyNumber.Text = "1";
            this.lblTransparencyNumber.Visible = false;
            // 
            // btnOK
            // 
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOK.Location = new System.Drawing.Point(497, 507);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(77, 45);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(602, 509);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(77, 43);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkSmartStamp
            // 
            this.chkSmartStamp.AutoSize = true;
            this.chkSmartStamp.Location = new System.Drawing.Point(17, 373);
            this.chkSmartStamp.Name = "chkSmartStamp";
            this.chkSmartStamp.Size = new System.Drawing.Size(122, 17);
            this.chkSmartStamp.TabIndex = 10;
            this.chkSmartStamp.Text = "Enable Smart Stamp";
            this.chkSmartStamp.UseVisualStyleBackColor = true;
            // 
            // lblSmartStamp
            // 
            this.lblSmartStamp.AutoSize = true;
            this.lblSmartStamp.Location = new System.Drawing.Point(14, 402);
            this.lblSmartStamp.MaximumSize = new System.Drawing.Size(250, 0);
            this.lblSmartStamp.Name = "lblSmartStamp";
            this.lblSmartStamp.Size = new System.Drawing.Size(231, 39);
            this.lblSmartStamp.TabIndex = 11;
            this.lblSmartStamp.Text = "Smart Stamp ensures you don\'t stamp over any content by creating a border around " +
    "existing content and placing the stamp in that border.";
            this.lblSmartStamp.Click += new System.EventHandler(this.lblSmartStamp_Click);
            // 
            // comboBoxFont
            // 
            this.comboBoxFont.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFont.FormattingEnabled = true;
            this.comboBoxFont.Items.AddRange(new object[] {
            "Courier",
            "Helvetica",
            "Times New Roman"});
            this.comboBoxFont.Location = new System.Drawing.Point(91, 19);
            this.comboBoxFont.Name = "comboBoxFont";
            this.comboBoxFont.Size = new System.Drawing.Size(150, 21);
            this.comboBoxFont.TabIndex = 12;
            this.comboBoxFont.SelectedIndexChanged += new System.EventHandler(this.comboBoxFont_SelectedIndexChanged);
            // 
            // lblFont
            // 
            this.lblFont.AutoSize = true;
            this.lblFont.Location = new System.Drawing.Point(21, 22);
            this.lblFont.Name = "lblFont";
            this.lblFont.Size = new System.Drawing.Size(64, 13);
            this.lblFont.TabIndex = 13;
            this.lblFont.Text = "Select Font:";
            // 
            // chkBold
            // 
            this.chkBold.AutoSize = true;
            this.chkBold.Location = new System.Drawing.Point(91, 51);
            this.chkBold.Name = "chkBold";
            this.chkBold.Size = new System.Drawing.Size(47, 17);
            this.chkBold.TabIndex = 14;
            this.chkBold.Text = "Bold";
            this.chkBold.UseVisualStyleBackColor = true;
            this.chkBold.CheckedChanged += new System.EventHandler(this.chkBold_CheckedChanged);
            // 
            // chkItalic
            // 
            this.chkItalic.AutoSize = true;
            this.chkItalic.Location = new System.Drawing.Point(144, 51);
            this.chkItalic.Name = "chkItalic";
            this.chkItalic.Size = new System.Drawing.Size(48, 17);
            this.chkItalic.TabIndex = 15;
            this.chkItalic.Text = "Italic";
            this.chkItalic.UseVisualStyleBackColor = true;
            this.chkItalic.CheckedChanged += new System.EventHandler(this.chkItalic_CheckedChanged);
            // 
            // btnResetDefault
            // 
            this.btnResetDefault.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnResetDefault.Location = new System.Drawing.Point(13, 509);
            this.btnResetDefault.Name = "btnResetDefault";
            this.btnResetDefault.Size = new System.Drawing.Size(143, 47);
            this.btnResetDefault.TabIndex = 16;
            this.btnResetDefault.Text = "Reset to Default";
            this.btnResetDefault.UseVisualStyleBackColor = true;
            this.btnResetDefault.Click += new System.EventHandler(this.btnResetDefault_Click);
            // 
            // comboBoxFontSize
            // 
            this.comboBoxFontSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFontSize.FormattingEnabled = true;
            this.comboBoxFontSize.Items.AddRange(new object[] {
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32"});
            this.comboBoxFontSize.Location = new System.Drawing.Point(294, 24);
            this.comboBoxFontSize.Name = "comboBoxFontSize";
            this.comboBoxFontSize.Size = new System.Drawing.Size(61, 21);
            this.comboBoxFontSize.TabIndex = 17;
            this.comboBoxFontSize.SelectedIndexChanged += new System.EventHandler(this.comboBoxFontSize_SelectedIndexChanged);
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new System.Drawing.Point(258, 27);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(30, 13);
            this.lblSize.TabIndex = 18;
            this.lblSize.Text = "Size:";
            // 
            // groupBoxFont
            // 
            this.groupBoxFont.Controls.Add(this.comboBoxFont);
            this.groupBoxFont.Controls.Add(this.lblSize);
            this.groupBoxFont.Controls.Add(this.comboBoxFontSize);
            this.groupBoxFont.Controls.Add(this.lblFont);
            this.groupBoxFont.Controls.Add(this.chkItalic);
            this.groupBoxFont.Controls.Add(this.chkBold);
            this.groupBoxFont.Controls.Add(this.lblSampleText);
            this.groupBoxFont.Location = new System.Drawing.Point(17, 89);
            this.groupBoxFont.Name = "groupBoxFont";
            this.groupBoxFont.Size = new System.Drawing.Size(392, 142);
            this.groupBoxFont.TabIndex = 19;
            this.groupBoxFont.TabStop = false;
            this.groupBoxFont.Text = "Font";
            // 
            // formOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.MenuBar;
            this.ClientSize = new System.Drawing.Size(703, 580);
            this.Controls.Add(this.groupBoxFont);
            this.Controls.Add(this.btnResetDefault);
            this.Controls.Add(this.comboBoxLocation);
            this.Controls.Add(this.lblSmartStamp);
            this.Controls.Add(this.lblLocation);
            this.Controls.Add(this.chkSmartStamp);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblTransparencyNumber);
            this.Controls.Add(this.lblTransparency);
            this.Controls.Add(this.trackTransparency);
            this.Controls.Add(this.pnlLocation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "formOptions";
            this.Text = "formOptions";
            this.Load += new System.EventHandler(this.formOptions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackTransparency)).EndInit();
            this.groupBoxFont.ResumeLayout(false);
            this.groupBoxFont.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FontDialog dialogFontBates;
        private System.Windows.Forms.Panel pnlLocation;
        private System.Windows.Forms.TrackBar trackTransparency;
        private System.Windows.Forms.Label lblSampleText;
        private System.Windows.Forms.Label lblTransparency;
        private System.Windows.Forms.ComboBox comboBoxLocation;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Label lblTransparencyNumber;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkSmartStamp;
        private System.Windows.Forms.Label lblSmartStamp;
        private System.Windows.Forms.ComboBox comboBoxFont;
        private System.Windows.Forms.Label lblFont;
        private System.Windows.Forms.CheckBox chkBold;
        private System.Windows.Forms.CheckBox chkItalic;
        private System.Windows.Forms.Button btnResetDefault;
        private System.Windows.Forms.ComboBox comboBoxFontSize;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.GroupBox groupBoxFont;
    }
}