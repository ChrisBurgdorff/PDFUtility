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
            float transparency;
            PDFUtility.Location stampLocation = Globals.stampLocation;

            switch (stampLocation)
            {
                case PDFUtility.Location.LOWER_RIGHT:
                    comboBoxLocation.SelectedIndex = 0;
                    break;
                case PDFUtility.Location.LOWER_LEFT:
                    comboBoxLocation.SelectedIndex = 1;
                    break;
                case PDFUtility.Location.UPPER_RIGHT:
                    comboBoxLocation.SelectedIndex = 2;
                    break;
                case PDFUtility.Location.UPPER_LEFT:
                    comboBoxLocation.SelectedIndex = 3;
                    break;
                case PDFUtility.Location.CENTER:
                    comboBoxLocation.SelectedIndex = 4;
                    break;
                case PDFUtility.Location.CENTER_BOTTOM:
                    comboBoxLocation.SelectedIndex = 5;
                    break;
                case PDFUtility.Location.CENTER_TOP:
                    comboBoxLocation.SelectedIndex = 6;
                    break;
            }
            transparency = Globals.stampTransparency;
            //MessageBox.Show((transparency * 100).ToString());
            trackTransparency.Value = Convert.ToInt32(transparency * 100);
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

        private void btnSelectFont_Click(object sender, EventArgs e)
        {
            if (dialogFontBates.ShowDialog() == DialogResult.OK)
            {
                //set font
            }
        }

        private void trackTransparency_ValueChanged(object sender, EventArgs e)
        {
            float newTransparency = trackTransparency.Value;
            int alpha = Convert.ToInt32((newTransparency / 100) * 255);
            //MessageBox.Show(alpha.ToString());
            //lblSampleText.ForeColor = Color.FromArgb(alpha, Color.Black);
            //lblSampleText.ForeColor = Color.Blue;
            lblTransparencyNumber.Text = Math.Round( (newTransparency / 100), 2).ToString();
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
            switch (comboBoxLocation.SelectedIndex)
            {
                case 0:
                    Globals.stampLocation = PDFUtility.Location.LOWER_RIGHT;
                    break;
                case 1:
                    Globals.stampLocation = PDFUtility.Location.LOWER_LEFT;
                    break;
                case 2:
                    Globals.stampLocation = PDFUtility.Location.UPPER_RIGHT;
                    break;
                case 3:
                    Globals.stampLocation = PDFUtility.Location.UPPER_LEFT;
                    break;
                case 4:
                    Globals.stampLocation = PDFUtility.Location.CENTER;
                    break;
                case 5:
                    Globals.stampLocation = PDFUtility.Location.CENTER_BOTTOM;
                    break;
                case 6:
                    Globals.stampLocation = PDFUtility.Location.CENTER_TOP;
                    break;
            }
            this.Close();
        }
    }
}
