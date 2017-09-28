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
            this.btnSelectFont = new System.Windows.Forms.Button();
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
            ((System.ComponentModel.ISupportInitialize)(this.trackTransparency)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlLocation
            // 
            this.pnlLocation.BackColor = System.Drawing.Color.White;
            this.pnlLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLocation.Location = new System.Drawing.Point(12, 21);
            this.pnlLocation.Name = "pnlLocation";
            this.pnlLocation.Size = new System.Drawing.Size(174, 162);
            this.pnlLocation.TabIndex = 0;
            // 
            // btnSelectFont
            // 
            this.btnSelectFont.Location = new System.Drawing.Point(378, 97);
            this.btnSelectFont.Name = "btnSelectFont";
            this.btnSelectFont.Size = new System.Drawing.Size(133, 50);
            this.btnSelectFont.TabIndex = 1;
            this.btnSelectFont.Text = "Select Font";
            this.btnSelectFont.UseVisualStyleBackColor = true;
            this.btnSelectFont.Click += new System.EventHandler(this.btnSelectFont_Click);
            // 
            // trackTransparency
            // 
            this.trackTransparency.Location = new System.Drawing.Point(351, 245);
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
            this.lblSampleText.Location = new System.Drawing.Point(311, 188);
            this.lblSampleText.Name = "lblSampleText";
            this.lblSampleText.Size = new System.Drawing.Size(73, 13);
            this.lblSampleText.TabIndex = 3;
            this.lblSampleText.Text = "Aa Bb Cc 123";
            // 
            // lblTransparency
            // 
            this.lblTransparency.AutoSize = true;
            this.lblTransparency.Location = new System.Drawing.Point(419, 293);
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
            this.comboBoxLocation.Location = new System.Drawing.Point(132, 245);
            this.comboBoxLocation.Name = "comboBoxLocation";
            this.comboBoxLocation.Size = new System.Drawing.Size(150, 21);
            this.comboBoxLocation.TabIndex = 5;
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(75, 248);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(51, 13);
            this.lblLocation.TabIndex = 6;
            this.lblLocation.Text = "Location:";
            // 
            // lblTransparencyNumber
            // 
            this.lblTransparencyNumber.AutoSize = true;
            this.lblTransparencyNumber.Location = new System.Drawing.Point(498, 293);
            this.lblTransparencyNumber.Name = "lblTransparencyNumber";
            this.lblTransparencyNumber.Size = new System.Drawing.Size(13, 13);
            this.lblTransparencyNumber.TabIndex = 7;
            this.lblTransparencyNumber.Text = "1";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(434, 341);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(67, 45);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(564, 342);
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
            this.chkSmartStamp.Location = new System.Drawing.Point(132, 289);
            this.chkSmartStamp.Name = "chkSmartStamp";
            this.chkSmartStamp.Size = new System.Drawing.Size(122, 17);
            this.chkSmartStamp.TabIndex = 10;
            this.chkSmartStamp.Text = "Enable Smart Stamp";
            this.chkSmartStamp.UseVisualStyleBackColor = true;
            // 
            // lblSmartStamp
            // 
            this.lblSmartStamp.AutoSize = true;
            this.lblSmartStamp.Location = new System.Drawing.Point(129, 319);
            this.lblSmartStamp.MaximumSize = new System.Drawing.Size(250, 0);
            this.lblSmartStamp.Name = "lblSmartStamp";
            this.lblSmartStamp.Size = new System.Drawing.Size(231, 39);
            this.lblSmartStamp.TabIndex = 11;
            this.lblSmartStamp.Text = "Smart Stamp ensures you don\'t stamp over any content by creating a border around " +
    "existing content and placing the stamp in that border.";
            // 
            // formOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.MenuBar;
            this.ClientSize = new System.Drawing.Size(701, 418);
            this.Controls.Add(this.lblSmartStamp);
            this.Controls.Add(this.chkSmartStamp);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblTransparencyNumber);
            this.Controls.Add(this.lblLocation);
            this.Controls.Add(this.comboBoxLocation);
            this.Controls.Add(this.lblTransparency);
            this.Controls.Add(this.lblSampleText);
            this.Controls.Add(this.trackTransparency);
            this.Controls.Add(this.btnSelectFont);
            this.Controls.Add(this.pnlLocation);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "formOptions";
            this.Text = "formOptions";
            this.Load += new System.EventHandler(this.formOptions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackTransparency)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FontDialog dialogFontBates;
        private System.Windows.Forms.Panel pnlLocation;
        private System.Windows.Forms.Button btnSelectFont;
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
    }
}