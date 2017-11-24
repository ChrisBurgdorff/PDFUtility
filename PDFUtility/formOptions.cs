using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PDFUtility;
using iTextSharp.text.pdf;
using System.IO;

namespace PDFUtilityOptions
{
    public partial class formOptions : Form
    {
        private void DrawLocationPanelLines(PaintEventArgs e)
        {
            //This doesn't work
            var pen = new Pen(Color.Gray, 3);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            //graph.DrawLine(pen, point1, point2);
            // Create points that define line.
            Point point1 = new Point(100, 100);
            Point point2 = new Point(500, 100);

            // Draw line to screen.
            e.Graphics.DrawLine(pen, point1, point2);
        }
        private void InitializeOptions()
        {
            this.Size = new Size(719, 617);
            float transparency;
            PDFUtility.StampLocation stampLocation = Globals.stampLocation;
            chkFolderStructure.Visible = false;
            chkIgnoreAlerts.Visible = false;
            chkSmartImages.Visible = false;
            txtDelimeter.Visible = false;
            lblDelimeter.Visible = false;
            btnCheckExtensions.Visible = false;
            chkConvertOnly.Visible = false;
            chkFolderStructure.Checked = Globals.matchFolderStructure;
            switch (stampLocation)
            {
                case PDFUtility.StampLocation.LOWER_RIGHT:
                    comboBoxLocation.SelectedIndex = 0;
                    break;
                case PDFUtility.StampLocation.LOWER_LEFT:
                    comboBoxLocation.SelectedIndex = 1;
                    break;
                case PDFUtility.StampLocation.UPPER_RIGHT:
                    comboBoxLocation.SelectedIndex = 2;
                    break;
                case PDFUtility.StampLocation.UPPER_LEFT:
                    comboBoxLocation.SelectedIndex = 3;
                    break;
                case PDFUtility.StampLocation.CENTER:
                    comboBoxLocation.SelectedIndex = 4;
                    break;
                case PDFUtility.StampLocation.CENTER_BOTTOM:
                    comboBoxLocation.SelectedIndex = 5;
                    break;
                case PDFUtility.StampLocation.CENTER_TOP:
                    comboBoxLocation.SelectedIndex = 6;
                    break;
            }
            transparency = Globals.stampTransparency;
            //MessageBox.Show((transparency * 100).ToString());
            trackTransparency.Value = Convert.ToInt32(transparency * 100);
            //SetStampLocation();
            iTextSharp.text.Font projectFont = Globals.font;
            switch (projectFont.Familyname)
            {
                case "Courier":
                    comboBoxFont.SelectedIndex = 0;
                    break;
                case "Helvetica":
                    comboBoxFont.SelectedIndex = 1;
                    break;
                case "Times":
                    comboBoxFont.SelectedIndex = 2;
                    break;
            }
            switch (Globals.bold)
            {
                case false:
                    chkBold.Checked = false;
                    break;
                case true:
                    chkBold.Checked = true;
                    break;
            }
            switch (Globals.italic)
            {
                case false:
                    chkItalic.Checked = false;
                    break;
                case true:
                    chkItalic.Checked = true;
                    break;
            }
            int fontSize = Convert.ToInt32(projectFont.Size);
            if (fontSize >= 8)
            {
                comboBoxFontSize.SelectedIndex = fontSize - 8;
            }
            else
            {
                comboBoxFontSize.SelectedIndex = 0;
            }
            switch (Globals.smartStamp)
            {
                case false:
                    chkSmartStamp.Checked = false;
                    break;
                case true:
                    chkSmartStamp.Checked = true;
                    break;
            }
        }
        public formOptions()
        {
            InitializeComponent();
            this.Text = Globals.appName + " - " + "Options";
            InitializeOptions();
        }

        private void formOptions_Load(object sender, EventArgs e)
        {
            //Set all option values
            trackTransparency.Value = Convert.ToInt32(Globals.stampTransparency * 100);
        }

        private void SetStampLocation()
        {
            //PDFUtility.Location stampLocation = Globals.stampLocation;
            List<Control> itemsToRemove = new List<Control>();
            foreach (Control ctrl in pnlLocation.Controls)
            {
                if (ctrl.Tag != null && ctrl.Tag.ToString() == "example")
                {
                    itemsToRemove.Add(ctrl);
                }
            }

            foreach (Control ctrl in itemsToRemove)
            {
                Controls.Remove(ctrl);
                ctrl.Dispose();
            }

            TransLabel batesExample = new TransLabel();
            batesExample.Text = "BAT 001";
            batesExample.Parent = pnlLocation;
            batesExample.Tag = "example";
            batesExample.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
            //batesExample.Font = new Font(this.Font, );

            batesExample.Show();
            switch (comboBoxLocation.SelectedIndex)
            {
                case 0:
                    batesExample.Location = new Point(139, 288);
                    batesExample.Refresh();
                    break;
                case 1:
                    batesExample.Location = new Point(3, 288);
                    break;
                case 2:
                    batesExample.Location = new Point(139, 7);
                    break;
                case 3:
                    batesExample.Location = new Point(3, 7);
                    break;
                case 4:
                    batesExample.Location = new Point(74, 146);
                    break;
                case 5:
                    batesExample.Location = new Point(74, 288);
                    break;
                case 6:
                    batesExample.Location = new Point(74, 7);
                    break;
            }

            //Reset tranparency
            float newTransparency = trackTransparency.Value;
            int alpha = Convert.ToInt32((newTransparency / 100) * 255);
            alpha = Math.Min(alpha, 255);
            alpha = Math.Max(alpha, 0);
            //batesExample.Transparency = alpha;
            foreach (TransLabel label in pnlLocation.Controls)
            {
                label.Transparency = alpha;
            }
        }

        private void btnSelectFont_Click(object sender, EventArgs e)
        {
            //DEPRECATED
            if (dialogFontBates.ShowDialog() == DialogResult.OK)
            {
                //set font
            }
        }

        private void trackTransparency_ValueChanged(object sender, EventArgs e)
        {
            float newTransparency = trackTransparency.Value;
            int alpha = Convert.ToInt32((newTransparency / 100) * 255);
            alpha = Math.Min(alpha, 255);
            alpha = Math.Max(alpha, 0);
            //batesExample.Transparency = alpha;
            foreach (TransLabel label in pnlLocation.Controls)
            {
                label.Transparency = alpha;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //Save all selected options
            float transparency;
            transparency = (float)trackTransparency.Value / 100;
            Globals.stampTransparency = transparency;
            Globals.matchFolderStructure = chkFolderStructure.Checked;
            
            switch (comboBoxLocation.SelectedIndex)
            {
                case 0:
                    Globals.stampLocation = PDFUtility.StampLocation.LOWER_RIGHT;
                    break;
                case 1:
                    Globals.stampLocation = PDFUtility.StampLocation.LOWER_LEFT;
                    break;
                case 2:
                    Globals.stampLocation = PDFUtility.StampLocation.UPPER_RIGHT;
                    break;
                case 3:
                    Globals.stampLocation = PDFUtility.StampLocation.UPPER_LEFT;
                    break;
                case 4:
                    Globals.stampLocation = PDFUtility.StampLocation.CENTER;
                    break;
                case 5:
                    Globals.stampLocation = PDFUtility.StampLocation.CENTER_BOTTOM;
                    break;
                case 6:
                    Globals.stampLocation = PDFUtility.StampLocation.CENTER_TOP;
                    break;
            }
            switch (chkSmartStamp.Checked)
            {
                case false:
                    Globals.smartStamp = false;
                    break;
                case true:
                    Globals.smartStamp = true;
                    break;
            }
            bool bold = chkBold.Checked;
            bool italic = chkItalic.Checked;
            //Start here WTF!
            //iTextSharp.text.Font newFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily., 15f, FontStyle.)
            //Bold = 1, Italic = 2, regular = 0;
            int fontSize = comboBoxFontSize.SelectedIndex + 8;
            iTextSharp.text.pdf.BaseFont baseFont = BaseFont.CreateFont();
            switch (comboBoxFont.SelectedIndex)
            {
                case 0:
                    //Courier
                    if (bold && italic)
                    {
                        baseFont = BaseFont.CreateFont(BaseFont.COURIER_BOLDOBLIQUE, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    }
                    else if (bold)
                    {
                        baseFont = BaseFont.CreateFont(BaseFont.COURIER_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    }
                    else if (italic)
                    {
                        baseFont = BaseFont.CreateFont(BaseFont.COURIER_OBLIQUE, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    }
                    else
                    {
                        baseFont = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    }
                    break;
                case 1:
                    //Helvetica
                    if (bold && italic)
                    {
                        baseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLDOBLIQUE, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    }
                    else if (bold)
                    {
                        baseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    }
                    else if (italic)
                    {
                        baseFont = BaseFont.CreateFont(BaseFont.HELVETICA_OBLIQUE, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    }
                    else
                    {
                        baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    }
                    break;
                case 2:
                    //Times New Roman
                    if (bold && italic)
                    {
                        baseFont = BaseFont.CreateFont(BaseFont.TIMES_BOLDITALIC, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    }
                    else if (bold)
                    {
                        baseFont = BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    }
                    else if (italic)
                    {
                        baseFont = BaseFont.CreateFont(BaseFont.TIMES_ITALIC, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    }
                    else
                    {
                        baseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    }
                    break;
            }
            //var base1 = BaseFont.CreateFont();
            //var baseFont =  BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

            Globals.convertOnly = chkConvertOnly.Checked;
            Globals.ignoreAlerts = chkIgnoreAlerts.Checked;
            Globals.smartImages = chkSmartImages.Checked;
            if (txtDelimeter.Text != "")
            {
                Globals.delimeter = txtDelimeter.Text;
            }
            else
            {
                Globals.delimeter = " ";
            }

            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, fontSize);
            Globals.font = font;
            Globals.bold = bold;
            Globals.italic = italic;
            this.Close();
        }

        private void lblSmartStamp_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetStampLocation();
        }

        private void SetFontStyle()
        {
            if (chkBold.Checked && chkItalic.Checked)
            {
                lblSampleText.Font = new Font(lblSampleText.Font, FontStyle.Bold | FontStyle.Italic);
            }
            else if (chkBold.Checked && !chkItalic.Checked)
            {
                lblSampleText.Font = new Font(lblSampleText.Font, FontStyle.Bold);
            }
            else if (!chkBold.Checked && chkItalic.Checked)
            {
                lblSampleText.Font = new Font(lblSampleText.Font, FontStyle.Italic);
            }
            else
            {
                lblSampleText.Font = new Font(lblSampleText.Font, FontStyle.Regular);
            }
        }

        private void comboBoxFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxFont.SelectedIndex)
            {
                case 0:
                    lblSampleText.Font = new Font("Courier", comboBoxFontSize.SelectedIndex + 8);
                    break;
                case 1:
                    lblSampleText.Font = new Font("Helvetica", comboBoxFontSize.SelectedIndex + 8);
                    break;
                case 2:
                    lblSampleText.Font = new Font("Times New Roman", comboBoxFontSize.SelectedIndex + 8);
                    break;
            }
        }

        private void comboBoxFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxFont.SelectedIndex)
            {
                case 0:
                    lblSampleText.Font = new Font("Courier", comboBoxFontSize.SelectedIndex + 8);
                    break;
                case 1:
                    lblSampleText.Font = new Font("Helvetica", comboBoxFontSize.SelectedIndex + 8);
                    break;
                case 2:
                    lblSampleText.Font = new Font("Times New Roman", comboBoxFontSize.SelectedIndex + 8);
                    break;
            }            
        }

        private void chkBold_CheckedChanged(object sender, EventArgs e)
        {
            SetFontStyle();
        }

        private void chkItalic_CheckedChanged(object sender, EventArgs e)
        {
            SetFontStyle();
        }

        private void btnResetDefault_Click(object sender, EventArgs e)
        {
            comboBoxLocation.SelectedIndex = 0;
            comboBoxFont.SelectedIndex = 1;
            comboBoxFontSize.SelectedIndex = 7;
            chkBold.Checked = true;
            chkItalic.Checked = false;
            chkSmartStamp.Checked = false;
            trackTransparency.Value = 100;
        }

        private void btnAdvanced_Click(object sender, EventArgs e)
        {
            if (btnAdvanced.Text == "Show Advanced Options")
            {
                this.Size = new Size(890, 617);
                chkFolderStructure.Visible = true;
                chkIgnoreAlerts.Visible = true;
                chkSmartImages.Visible = true;
                txtDelimeter.Visible = true;
                lblDelimeter.Visible = true;
                btnCheckExtensions.Visible = true;
                chkConvertOnly.Visible = true;
                btnAdvanced.Text = "Hide Advanced Options";
            }
            else
            {
                this.Size = new Size(719, 617);
                chkConvertOnly.Visible = false;
                chkFolderStructure.Visible = false;
                chkIgnoreAlerts.Visible = false;
                chkSmartImages.Visible = false;
                txtDelimeter.Visible = false;
                lblDelimeter.Visible = false;
                btnCheckExtensions.Visible = false;
                btnAdvanced.Text = "Show Advanced Options";
            }
        }

        private void btnCheckExtensions_Click(object sender, EventArgs e)
        {
            /*List of acceptable file extensions:
         * bmp, doc, docb, docm, docx, dot, dotm, dotx, gif, jpeg, jpg, pdf, png, tif, tiff, .txt
            */
            FolderBrowserDialog dialogFolder = new FolderBrowserDialog();
            dialogFolder.Description = "Select Folder To Verify:";
            List<string> badFiles = new List<string>();
            string badFilesList = "";
            if (dialogFolder.ShowDialog() == DialogResult.OK)
            {
                var folder = dialogFolder.SelectedPath;
                //NEW
                string[] files;
                files = Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories);
                for (int i = 0; i < files.Length; i++)
                {
                    if (!(Path.GetExtension(files[i]) == ".bmp" || Path.GetExtension(files[i]) == ".doc" || Path.GetExtension(files[i]) == ".docb" || Path.GetExtension(files[i]) == ".docm" ||
                        Path.GetExtension(files[i]) == ".docx" || Path.GetExtension(files[i]) == ".dot" || Path.GetExtension(files[i]) == ".dotm" || Path.GetExtension(files[i]) == ".dotx" ||
                        Path.GetExtension(files[i]) == ".gif" || Path.GetExtension(files[i]) == ".jpeg" || Path.GetExtension(files[i]) == ".jpg" || Path.GetExtension(files[i]) == ".pdf" ||
                        Path.GetExtension(files[i]) == ".png" || Path.GetExtension(files[i]) == ".tif" || Path.GetExtension(files[i]) == ".tiff" || Path.GetExtension(files[i]) == ".txt"))
                    {
                        badFiles.Add(files[i]);
                    }
                }
                if (badFiles.Count == 0)
                {
                    MessageBox.Show("These files are all of the correct format", Globals.appName, MessageBoxButtons.OK);
                }
                else
                {
                    string[] badFilesArray = badFiles.ToArray();
                    for (int j = 0; j < badFilesArray.Length; j++)
                    {
                        badFilesList = badFilesList + badFilesArray[j] + "\n";
                    }
                    MessageBox.Show("The following files will not be stamped: \n" + badFilesList, Globals.appName, MessageBoxButtons.OK);
                }
            }
        }
    }
}
