namespace PDFUtility
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
            this.dialogFontBates = new System.Windows.Forms.FontDialog();
            this.pnlLocation = new System.Windows.Forms.Panel();
            this.btnSelectFont = new System.Windows.Forms.Button();
            this.trackTransparency = new System.Windows.Forms.TrackBar();
            this.lblSampleText = new System.Windows.Forms.Label();
            this.lblTransparency = new System.Windows.Forms.Label();
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
            // formOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.MenuBar;
            this.ClientSize = new System.Drawing.Size(701, 418);
            this.Controls.Add(this.lblTransparency);
            this.Controls.Add(this.lblSampleText);
            this.Controls.Add(this.trackTransparency);
            this.Controls.Add(this.btnSelectFont);
            this.Controls.Add(this.pnlLocation);
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
    }
}