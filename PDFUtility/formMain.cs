using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections;
using Newtonsoft.Json;
using Microsoft.Office.Interop.Excel;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using Microsoft.Office.Interop.Word;
using Spire.Doc;
using WordToPDF;

namespace PDFUtility
{
    #region enums
    public enum StampLocation
    {
        LOWER_RIGHT,
        LOWER_LEFT,
        UPPER_RIGHT,
        UPPER_LEFT,
        CENTER,
        CENTER_BOTTOM,
        CENTER_TOP
    }

    public enum Activity
    {
        ADDING_FILES,
        BATES_STAMPING
    }

    public enum BatesFont
    {
        TIMES_NEW_ROMAN,
        COURIER,
        HELVETICA
    }
    #endregion

    public partial class formMain : Form
    {
        //MAIN TODO LIST
        //Advanced Options - Rename files, increment prefix
        //Fonts not saving - done, not tested
        //Test file6.pdf last page not printing in correct spot - Possibly fixed
        //Track whenever unsaved changes made to file (for disabling and enabling save and save as buttons) - might not need this
        //Fix and test new save/load function
        //Add image file formats (word docs possibly as well) - done not tested
        //Help file
        //Add message box warning when user selects SmartStamp - Don't Need
        //Export history to Excel - Done
        //Add hot keys
        //Constant number of digits - done not tested
        //Keep track of shit for undo
        //Ask for feature ideas
        //Create installer
        //Smart Stamping for Images??? - Done
        //Excel format everything as text first... Fucking Excel - Done
        //Look into why progress bar not showing right percentage
        //Associate file extension and give icon
        //Save Shit to stamp - done, not tested
        //Get it to not write over shit
        //Buy template and make landing page - done
        //sign up for software marketplace
        //put online and make thousands of dollars!!
        /*
         Changes made: options changed, files added, folders added, drag and drop, bates number changed, bates prefix changed, anything stamped - don't need this
             */
        /*List of acceptable file extensions:
         * bmp, doc, docb, docm, docx, dot, dotm, dotx, gif, jpeg, jpg, pdf, png, tif, tiff
            */
        #region Form
        public formMain()
        {
            InitializeComponent();
            Globals.appName = "Bates Plus";
            InitializeListView();
            this.lstBatesFiles.DragDrop += new
                System.Windows.Forms.DragEventHandler(this.lstBatesFiles_DragDrop);
            this.lstBatesFiles.DragEnter += new
                System.Windows.Forms.DragEventHandler(this.lstBatesFiles_DragEnter);
            ResizeListViewColumns(lstBatesFiles);
            //btnAddFilesBates2.Visible = false;
            //btnSelectFolderBates.Visible = false;
            chkSubfolders.Visible = false;
            this.Text = Globals.appName + " - " + Globals.currentProject;
            txtFolderBates.Text = Globals.outputFolder;
                        
        }
        private void formMain_Load(object sender, EventArgs e)
        {
            InitializeSaveFile();
        }
        #endregion

        #region Helper Functions
        /*I don't think I need this
        private void ChangesMade()
        {
            Globals.isCurrent = false;
            if (Globals.isSaved)
            {
                saveAsToolStripMenuItem.Enabled = true;
                saveToolStripMenuItem.Enabled = true;
            }
            else
            {
                saveAsToolStripMenuItem.Enabled = true;
                saveToolStripMenuItem.Enabled = false;
            }
        }*/

        private void EmptyList(bool isEmpty)
        {
            if (isEmpty)
            {
                lblEmptyList.Visible = true;
                lblEmptyList.Enabled = true;
                btnBatesStamp.Enabled = false;
                btnExportExcel.Enabled = false;
            }
            else
            {
                lblEmptyList.Visible = false;
                lblEmptyList.Enabled = false;
                btnBatesStamp.Enabled = true;
                btnExportExcel.Enabled = true;
            }
        }

        private string ConstantNumber(int number, int digits)
        {
            string newNumber = "";
            for (int i = 0; i < digits - number.ToString().Length; i++)
            {
                newNumber = newNumber + "0";
            }
            newNumber = newNumber + number.ToString();
            return newNumber;
        }

        private void FixFileList()
        {
            int fileNum = 1;
            int startingBates = 1;
            int endingBates;
            int numPages;
            int numDigits = txtStartNumber.Text.Length;
            if (numDigits == 0)
                numDigits = 1;

            if (int.TryParse(txtStartNumber.Text, out startingBates)) { } //Puts int value of text from start number field in startingBates
            else
            { //if empty string or non-int make everything 1, which is default
                startingBates = 1;
                //txtStartNumber.Text = "1";
            }
            for (int i = 0; i < lstBatesFiles.Items.Count; i++)
            {
                lstBatesFiles.Items[i].SubItems[0].Text = fileNum.ToString();
                fileNum++;
                numPages = 0;
                if (int.TryParse(lstBatesFiles.Items[i].SubItems[2].Text, out numPages)) { }
                if (numPages != 0)
                {
                    endingBates = startingBates + numPages - 1;
                    lstBatesFiles.Items[i].SubItems[3].Text = ConstantNumber(startingBates, numDigits) + " - " + ConstantNumber(endingBates, numDigits);
                    startingBates = endingBates + 1;
                }
            }
            if (lstBatesFiles.Items.Count > 0)
            {
                EmptyList(false);
                //lblEmptyList.Visible = false;
                //lblEmptyList.Enabled = false;
            }
            else
            {
                EmptyList(true);
                //lblEmptyList.Visible = true;
                //lblEmptyList.Enabled = true;
            }
            ResizeListViewColumns(lstBatesFiles);
        }
        private void ExportToExcel(ListView lv)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                    Workbook wb = app.Workbooks.Add(XlSheetType.xlWorksheet);
                    Worksheet ws = (Worksheet)app.ActiveSheet;
                    //Microsoft.Office.Interop.Excel.Range cells = ws.Columns("D");
                    Microsoft.Office.Interop.Excel.Range cells = (Microsoft.Office.Interop.Excel.Range)ws.get_Range("D2").EntireColumn;
                    // set each cell's format to Text
                    cells.NumberFormat = "@";
                    app.Visible = false;
                    for (int j = 1; j <= lv.Columns.Count; j++)
                    {
                        var newWidth = Math.Min(255, lv.Columns[j - 1].Width / 4);
                        ws.Columns[j].ColumnWidth = newWidth;
                        ws.Cells[1, j] = lv.Columns[j - 1].Text;
                    }
                    int i = 2;
                    foreach (ListViewItem item in lv.Items)
                    {
                        for (int k = 1; k <= item.SubItems.Count; k++)
                        {
                            ws.Cells[i, k] = item.SubItems[k - 1].Text;
                        }
                        i++;
                    }
                    wb.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, false, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Exported Successfully.", Globals.appName);
                }
            }
        }
        public void UpdateCurrentBatesNumber(int batesNumber, string batesPrefix)
        {
            txtStartNumber.Text = batesNumber.ToString();
            lblLastStamped.Text = "Last Stamped: " + batesPrefix + " " + (batesNumber - 1).ToString();
            lblLastStamped.Visible = true;
            lblLastStamped.Enabled = true;
        }

        public static void CopyRegionIntoImage(Bitmap srcBitmap, System.Drawing.Rectangle srcRegion, ref Bitmap destBitmap, System.Drawing.Rectangle destRegion)
        {
            using (Graphics grD = Graphics.FromImage(destBitmap))
            {
                grD.DrawImage(srcBitmap, destRegion, srcRegion, GraphicsUnit.Pixel);
            }
        }

        public Bitmap AddBorder(Bitmap image, int borderSize, Color borderColor)
        {
            Bitmap image2 = new Bitmap(image.Width + (borderSize * 2), image.Height + (borderSize * 2));
            System.Drawing.Rectangle originalImageRect = new System.Drawing.Rectangle(0, 0, image.Width, image.Height);
            System.Drawing.Rectangle newImageRect = new System.Drawing.Rectangle(0, 0, image2.Width, image2.Height);
            System.Drawing.Rectangle newImagePasteArea = new System.Drawing.Rectangle(borderSize, borderSize, image.Width, image.Height);
            Pen pen = new Pen(borderColor, borderSize);
            pen.Alignment = PenAlignment.Inset;
            using (Graphics graphics = Graphics.FromImage(image2))
            {
                graphics.DrawImage(image, newImagePasteArea, originalImageRect, GraphicsUnit.Pixel);
                graphics.DrawRectangle(pen, newImageRect);
            }
            return image2;
        }
        #endregion

        #region Buttons (Other than Menu)
        private void btnSelectOutput_Click(object sender, EventArgs e)
        {
            dialogFolderBates.Description = "Select Output Folder:";
            if (dialogFolderBates.ShowDialog() == DialogResult.OK)
            {
                var folder = dialogFolderBates.SelectedPath;
                Globals.outputFolder = folder;
                txtFolderBates.Text = folder;
                SaveFileStatic.outputFolder = folder;
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel(lstBatesFiles);
        }

        private void btnBatesStamp_Click(object sender, EventArgs e)
        {
            PDFUtility pu = new PDFUtility();
            int startNumber;
            string fullPath = "";
            string[] files;
            string batesPrefix = txtBatesPrefix.Text;
            List<string> fileCollection = new List<string>();
            //myCollection.Add(aString);
            for (int i = 0; i < lstBatesFiles.Items.Count; i++)
            {
                fullPath = lstBatesFiles.Items[i].SubItems[4].Text + @"\" + lstBatesFiles.Items[i].SubItems[1].Text;
                fileCollection.Add(fullPath);
            }
            files = fileCollection.ToArray();
            if (int.TryParse(txtStartNumber.Text, out startNumber))
            {
                // it's a valid integer
                BatesStamp(files, startNumber, batesPrefix);
                //txtStartNumber.Text = Globals.currentBates.ToString();
            }
            else
            {
                MessageBox.Show("Please enter a valid number in the Start At field.", Globals.appName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
        #endregion

        #region Main Menu Buttons
        private void btnOpenProject_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Bates Plus Files|*.bpp";
            ofd.DefaultExt = ".bpp";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ClearStatus(LoadProject(ofd.FileName));
            }
        }

        private void btnSaveProject_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Bates Plus Files|*.bpp";
            sfd.DefaultExt = ".bpp";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                ClearStatus(SaveProject(sfd.FileName));
            }
        }

        private void btnAddFilesBates_Click_1(object sender, EventArgs e)
        {
            if (dialogFileBates.ShowDialog() == DialogResult.OK)
            {
                string[] files = dialogFileBates.FileNames;
                AddFiles(files);
            }
        }

        private void btnAddFolderBates_Click(object sender, EventArgs e)
        {
            dialogFolderBates.Description = "Select Folder To Add:";
            if (dialogFolderBates.ShowDialog() == DialogResult.OK)
            {
                var folder = dialogFolderBates.SelectedPath;
                bool includeSubfolders = false;
                Globals.folderToStamp = folder;
                if (System.IO.Directory.GetDirectories(folder).Length > 0)
                {
                    string message = "Include subfolders?";
                    string caption = Globals.appName;
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;
                    result = MessageBox.Show(message, caption, buttons);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        includeSubfolders = true;
                    }
                }
                AddFolder(folder, includeSubfolders);
            }
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            PDFUtilityOptions.formOptions form = new PDFUtilityOptions.formOptions();
            form.Show();
        }
        #endregion

        #region ListView
        private void ResizeListViewColumns(ListView lv)
        {
            foreach (ColumnHeader column in lv.Columns)
            {
                column.Width = -2;
            }
        }
        
        private void lstBatesFiles_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.Link;
        }

        private void lstBatesFiles_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            //MessageBox.Show(lstBatesFiles.InsertionMark.Index.ToString());
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            
            int i, j;
            //var item;
            if (s == null)
            {
                //Reorder logic
                //MessageBox.Show(e.Data.ToString());
                var indexOfItemUnderMouseToDrop = -1;
                if(lstBatesFiles.HitTest(lstBatesFiles.PointToClient(new System.Drawing.Point(e.X, e.Y))).Item != null)
                    indexOfItemUnderMouseToDrop = lstBatesFiles.HitTest(lstBatesFiles.PointToClient(new System.Drawing.Point(e.X, e.Y))).Item.Index;

                // Updates the label text.
                if (indexOfItemUnderMouseToDrop != -1)
                {
                    int selected = lstBatesFiles.SelectedItems.Count;
                    MessageBox.Show(selected + "Drops before item #" + (indexOfItemUnderMouseToDrop));
                    for (j = 0; j < lstBatesFiles.SelectedItems.Count; j++)
                    {
                        var item = lstBatesFiles.SelectedItems[j];
                        lstBatesFiles.Items.Remove(lstBatesFiles.SelectedItems[j]);
                        lstBatesFiles.Items.Insert(indexOfItemUnderMouseToDrop + j, item);
                    }
                }
                else
                {
                    MessageBox.Show("Drops at the end.");
                    indexOfItemUnderMouseToDrop = lstBatesFiles.Items.Count - 1;
                    for (j = 0; j < lstBatesFiles.SelectedItems.Count; j++)
                    {
                        var item = lstBatesFiles.SelectedItems[j];
                        lstBatesFiles.Items.Remove(lstBatesFiles.SelectedItems[j]);
                        lstBatesFiles.Items.Insert(indexOfItemUnderMouseToDrop + j, item);
                    }
                }
            }
            else
            {
                IComparer comparer = new AlphanumComparator.AlphanumComparator();
                Array.Sort(s, comparer);
                for (i = 0; i < s.Length; i++)
                {
                    PDFUtility pu = new PDFUtility();
                    string fileName = Path.GetFileName(s[i]);
                    string path = Path.GetDirectoryName(s[i]);
                    string fileNumber = (1 + lstBatesFiles.Items.Count).ToString();
                    string pageCount = PageCount(s[i]).ToString();
                    ListViewItem item = new ListViewItem(new[] { fileNumber, fileName, pageCount, "N/A", path });
                    lstBatesFiles.Items.Add(item);
                    if (Globals.toStamp == null)
                    {
                        Globals.toStamp = new List<ActionToStamp>();
                    }
                    Globals.toStamp.Add(new ActionToStamp(fileNumber, fileName, pageCount, path));
                }
            }
            FixFileList();
        }

        public void InitializeListView()
        {
            lstBatesFiles.View = System.Windows.Forms.View.Details;
            lstBatesFiles.FullRowSelect = true;
        }  

        private void lstBatesFiles_MouseDown(object sender, MouseEventArgs e)
        {
            int startNum;
            startNum = this.lstBatesFiles.InsertionMark.Index;
            //MessageBox.Show(startNum.ToString());
        }

        private void lstBatesFiles_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Link);
        }

        public void ClearList()
        {
            lstBatesFiles.Items.Clear();
            FixFileList();
        }

        private void lstBatesFiles_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                foreach (ListViewItem eachItem in lstBatesFiles.SelectedItems)
                {
                    lstBatesFiles.Items.Remove(eachItem);
                }
                FixFileList();
            }
        }
        #endregion

        #region Tool Strip Menu
        private void nEwToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("SDF");
            if (Globals.isSaved && !Globals.isCurrent)
            {
                string message = "Do you wish to save changes to " + Globals.currentProject + "?";
                string caption = Globals.appName;
                MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
                DialogResult result;
                result = MessageBox.Show(message, caption, buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    ClearStatus(SaveProject(Globals.currentSavePath));
                    NewProject();
                }
                else if (result == System.Windows.Forms.DialogResult.No)
                {
                    NewProject();
                }
            }
            else if (Globals.isSaved && Globals.isCurrent)
            {
                NewProject();
            }
            else if (!Globals.isCurrent)
            {
                string message = "Do you wish to save changes to current project?";
                string caption = Globals.appName;
                MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
                DialogResult result;
                result = MessageBox.Show(message, caption, buttons);
                if (result == System.Windows.Forms.DialogResult.No)
                {
                    NewProject();
                }
                else if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "Bates Plus Files|*.bpp";
                    sfd.DefaultExt = ".bpp";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        ClearStatus(SaveProject(sfd.FileName));
                        NewProject();
                    }
                }
            }
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO: Check if project needs to be saved first
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Bates Plus Files|*.bpp";
            ofd.DefaultExt = ".bpp";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ClearStatus(LoadProject(ofd.FileName));
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearStatus(SaveProject(Globals.currentSavePath));
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Bates Plus Files|*.bpp";
            sfd.DefaultExt = ".bpp";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                ClearStatus(SaveProject(sfd.FileName));
            }
        }

        private void aboutBatesPlusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formAbout form = new formAbout();
            form.Show();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void viewHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO: View History
            PDFUtilityHistory.formHistory form = new PDFUtilityHistory.formHistory();
            form.Show();
        }
        #endregion

        #region AddFilesAndFolders        
        public void AddFolder(string folderPath, bool includeSubfolders)
        {
            
            //lblEmptyList.Visible = false;
            //lblEmptyList.Enabled = false;
            string[] files;
            string path;
            string fileName;
            string pageCount;
            string fileNumber;
            PDFUtility pu = new PDFUtility();
            if (includeSubfolders)
            {
                files = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories);
            }
            else
            {
                files = Directory.GetFiles(folderPath, "*.*");
            }
            IComparer comparer = new AlphanumComparator.AlphanumComparator();
            Array.Sort(files, comparer);
            path = folderPath;
            if (files.Length > 0)
            {
                EmptyList(false);
            }
            for (int i = 0; i < files.Length; i++)
            {
                fileName = Path.GetFileName(files[i]);
                fileNumber = (1 + lstBatesFiles.Items.Count).ToString();
                pageCount = PageCount(files[i]).ToString();
                ListViewItem item = new ListViewItem(new[] {fileNumber, fileName, pageCount, "N/A", path });
                lstBatesFiles.Items.Add(item);
                UpdateStatus(i+1, files.Length, Activity.ADDING_FILES);
                if (Globals.toStamp == null)
                {
                    Globals.toStamp = new List<ActionToStamp>();
                }
                Globals.toStamp.Add(new ActionToStamp(fileNumber, fileName, pageCount, path));
            }
            FixFileList();
            ClearStatus("Click Bates Stamp to stamp these files.");
        }

        public void AddFiles(string[] files)
        {
            string path;
            string fileName;
            string pageCount;
            string fileNumber;
            PDFUtility pu = new PDFUtility();
            IComparer comparer = new AlphanumComparator.AlphanumComparator();
            Array.Sort(files, comparer);
            if (files.Length > 0)
            {
                EmptyList(false);
            }
            //lblEmptyList.Visible = false;
            //lblEmptyList.Enabled = false;
            for (int i = 0; i < files.Length; i++)
            {
                fileName = Path.GetFileName(files[i]);
                fileNumber = (1 + lstBatesFiles.Items.Count).ToString();
                pageCount = PageCount(files[i]).ToString();
                path = Path.GetDirectoryName(files[i]);
                ListViewItem item = new ListViewItem(new[] { fileNumber, fileName, pageCount, "N/A", path });
                lstBatesFiles.Items.Add(item);
                UpdateStatus(i+1, files.Length, Activity.ADDING_FILES);
                if (Globals.toStamp == null)
                {
                    Globals.toStamp = new List<ActionToStamp>();
                }
                Globals.toStamp.Add(new ActionToStamp(fileNumber, fileName, pageCount, path));
            }
            FixFileList();
            ClearStatus("Click Bates Stamp to stamp these files.");
        }
        #endregion

        #region StatusBar
        public void UpdateStatus(int currentAction, int totalActions, Activity activity)
        {
            double newValue = 100 * (Convert.ToDouble(currentAction) / Convert.ToDouble(totalActions));
            progBarBates.Value = Convert.ToInt32(newValue);
            //Application.DoEvents();
            if (activity == Activity.ADDING_FILES)
            {
                statusTextBates.Text = "Adding Files.";
            }
            else if (activity == Activity.BATES_STAMPING)
            {
                statusTextBates.Text = "Stamping File " + currentAction + " of " + totalActions + ".";
            }
            System.Windows.Forms.Application.DoEvents();
        }
        public void ClearStatus(string newMessage)
        {
            progBarBates.Value = 0;
            statusTextBates.Text = newMessage;
            System.Windows.Forms.Application.DoEvents();
        }
        public void ClearStatus()
        {
            progBarBates.Value = 0;
            statusTextBates.Text = "";
            System.Windows.Forms.Application.DoEvents();
        }
        #endregion

        #region Change Events
        private void txtStartNumber_TextChanged(object sender, EventArgs e)
        {
            FixFileList();
        }
        #endregion

        #region Save and Load and New
        private void NewProject()
        {

        }
        private void InitializeSaveFile()
        {
            //Added after switch to fucking Globals.
            Globals.history = new List<Action>();
            //Initialize current static save file to all default values
            SaveFileStatic.batesNumber = 1;
            SaveFileStatic.batesPrefix = "";
            SaveFileStatic.font = new iTextSharp.text.Font(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 15);
            SaveFileStatic.history = new List<Action>();
            SaveFileStatic.location = Globals.stampLocation;
            SaveFileStatic.name = "bla bla bla";
            SaveFileStatic.outputFolder = Globals.outputFolder;
            SaveFileStatic.transparency = 1f;
            var baseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            if (Globals.font == null)
            {
                iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, 15);
                SaveFileStatic.font = font;
                //Fix Later
                Globals.font = font;
                Globals.bold = true;
                Globals.italic = false;
                Globals.batesFont = BatesFont.HELVETICA;
            }
        }

        public string SaveProject(string path)
        {
            string response = "Project Saved Successfully.";
            SaveFile s = new SaveFile();
            Globals.currentProject = Path.GetFileNameWithoutExtension(path);
            s.name = Globals.currentProject;
            s.batesNumber = Globals.currentBates;
            s.batesPrefix = Globals.batesPrefix;
            s.font = Globals.font;
            s.history = Globals.history;
            s.location = Globals.stampLocation;
            s.name = Globals.currentProject;
            s.outputFolder = Globals.outputFolder;
            s.transparency = Globals.stampTransparency;
            s.batesFont = Globals.batesFont;
            s.bold = Globals.bold;
            s.italic = Globals.italic;
            string data = JsonConvert.SerializeObject(s, Formatting.Indented);
            System.IO.File.WriteAllText(path, data);
            this.Text = Globals.appName + " - " + Globals.currentProject;
            Globals.currentSavePath = path;
            Globals.isCurrent = true;
            Globals.isSaved = true;
            return response;
        }

        public string LoadProject(string path)
        {
            string response = "Project Loaded Successfully.";
            string data = System.IO.File.ReadAllText(path);
            //[JsonProperty]
            //public Guid? ClientId { get; private set; }
            SaveFile s = JsonConvert.DeserializeObject<SaveFile>(data);
            Globals.currentProject = s.name;
            Globals.currentBates = s.batesNumber;
            Globals.batesPrefix = s.batesPrefix;
            Globals.stampTransparency = s.transparency;
            //Globals.font = s.font;
            Globals.stampLocation = s.location;
            Globals.history = s.history;
            SaveFileStatic.batesNumber = s.batesNumber;
            SaveFileStatic.batesPrefix = s.batesPrefix;
            SaveFileStatic.font = s.font;
            SaveFileStatic.history = s.history;
            SaveFileStatic.location = s.location;
            SaveFileStatic.name = s.name;
            SaveFileStatic.outputFolder = s.outputFolder;
            SaveFileStatic.transparency = s.transparency;
            Globals.outputFolder = s.outputFolder;
            Globals.currentSavePath = path;
            Globals.isCurrent = true;
            Globals.isSaved = true;
            Globals.toStamp = s.toStamp;
            Globals.batesFont = s.batesFont;
            
            int fontSize = Convert.ToInt32(Globals.font.Size);
            iTextSharp.text.pdf.BaseFont baseFont = BaseFont.CreateFont();
            bool bold = s.bold;
            bool italic = s.italic;
            switch (s.batesFont)
            {
                case BatesFont.COURIER:
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
                case BatesFont.HELVETICA:
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
                case BatesFont.TIMES_NEW_ROMAN:
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
                default:
                    baseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    break;
            }
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, fontSize);
            Globals.font = font;
            //Fill in list view now
            if (s.toStamp != null)
            {
                for (int i = 0; i < s.toStamp.Count; i++)
                {
                    ListViewItem item = new ListViewItem(new[] { s.toStamp[i].fileNumber, s.toStamp[i].fileName, s.toStamp[i].pageCount, "N/A", s.toStamp[i].path });
                    lstBatesFiles.Items.Add(item);
                }
            }
            //end list view
            txtFolderBates.Text = s.outputFolder;
            txtBatesPrefix.Text = s.batesPrefix;
            txtStartNumber.Text = s.batesNumber.ToString();
            this.Text = Globals.appName + " - " + Globals.currentProject;
            return response;
        }
        #endregion
                
        #region Bates Stamping
        public int PageCountWord(object Path)
        {
            // Get application object
            Microsoft.Office.Interop.Word.Application WordApplication = new Microsoft.Office.Interop.Word.Application();

            // Get document object
            object Miss = System.Reflection.Missing.Value;
            object ReadOnly = false;
            object Visible = false;
            Microsoft.Office.Interop.Word.Document Doc = WordApplication.Documents.Open(ref Path, ref Miss, ref ReadOnly, ref Miss, ref Miss, ref Miss, ref Miss, ref Miss, ref Miss, ref Miss, ref Miss, ref Visible, ref Miss, ref Miss, ref Miss, ref Miss);

            // Get pages count
            Microsoft.Office.Interop.Word.WdStatistic PagesCountStat = Microsoft.Office.Interop.Word.WdStatistic.wdStatisticPages;
            int PagesCount = Doc.ComputeStatistics(PagesCountStat, ref Miss);
            Doc.Close();
            return PagesCount;
        }
        public int PageCount(string fileName)
        {
            int pageCount = 0;
            if (Path.GetExtension(fileName) == ".pdf")
            {
                using (var reader = new PdfReader(fileName))
                {
                    pageCount = reader.NumberOfPages;
                }
            }
            else if (Path.GetExtension(fileName) == ".doc" || Path.GetExtension(fileName) == ".dot" || Path.GetExtension(fileName) == ".docx" || Path.GetExtension(fileName) == ".docm"
                        || Path.GetExtension(fileName) == ".dotx" || Path.GetExtension(fileName) == ".dotm" || Path.GetExtension(fileName) == ".docb")
            {
                pageCount = PageCountWord(fileName);
            }
            else if (Path.GetExtension(fileName) == ".gif" || Path.GetExtension(fileName) == ".bmp" || Path.GetExtension(fileName) == ".jpeg" || Path.GetExtension(fileName) == ".jpg"
                        || Path.GetExtension(fileName) == ".png" || Path.GetExtension(fileName) == ".tiff" || Path.GetExtension(fileName) == ".tif")
            {
                pageCount = 1;
            }
            return pageCount;
        }
        public void BatesStamp(string[] files, int startNumber, string batesPrefix)
        {
            string outputFolder = Globals.outputFolder;
            string fileNameOnly;
            string fileName;
            string outputPath = "";
            int currentBates = startNumber;
            bool smartStamp = Globals.smartStamp;
            string tempFolder = AppDomain.CurrentDomain.BaseDirectory + @"Temp\";
            formMain form1 = new formMain();
            form1 = (formMain)System.Windows.Forms.Application.OpenForms[0];
            bool outputGood = false;
            int numDigits = txtStartNumber.Text.Length;
            if (!Directory.Exists(outputFolder))
            {
                if (MessageBox.Show("The output folder does not exist.  Do you want to create it?", Globals.appName, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    System.IO.Directory.CreateDirectory(outputFolder);
                    outputGood = true;
                }
            }
            else
            {
                outputGood = true;
            }
            if (outputGood)
            {
                Globals.toStamp.Clear();
                for (int i = 0; i < files.Length; i++)
                {
                    fileName = files[i];

                    form1.UpdateStatus(i + 1, files.Length, Activity.BATES_STAMPING);
                    if (Path.GetExtension(fileName) == ".pdf")
                    {
                        //Smart Stamping
                        if (smartStamp)
                        {
                            PdfReader pReader = new PdfReader(fileName);
                            int numPages = pReader.NumberOfPages;
                            iTextSharp.text.Document doc = new iTextSharp.text.Document();
                            fileNameOnly = Path.GetFileName(fileName);
                            //outputPath = outputFolder + @"\" + fileNameOnly;                            
                            System.IO.Directory.CreateDirectory(tempFolder);
                            string tempPath = tempFolder + fileNameOnly;
                            PdfWriter pWriter = PdfWriter.GetInstance(doc, new FileStream(tempPath, FileMode.Create, FileAccess.Write));
                            doc.Open();
                            iTextSharp.text.Image img;
                            PdfImportedPage page;
                            PdfContentByte directContent = new PdfContentByte(pWriter);
                            for (int k = 1; k <= numPages; k++)
                            {
                                page = pWriter.GetImportedPage(pReader, k);
                                img = iTextSharp.text.Image.GetInstance(page);
                                img.ScaleAbsolute(PageSize.A4.Width - 72, PageSize.A4.Height - 72);
                                img.SetAbsolutePosition(36, 36);
                                directContent.AddImage(img);
                                doc.Add(img);
                                doc.NewPage();
                            }
                            doc.Close();
                            pReader.Close();
                            pWriter.Close();
                            fileName = tempPath;
                        }
                        //End Smart Stamping                   
                        using (var reader = new PdfReader(fileName))
                        {
                            fileNameOnly = Path.GetFileName(fileName);
                            outputPath = outputFolder + @"\" + fileNameOnly;
                            //var baseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                            //iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, 15);     
                            iTextSharp.text.Font font = Globals.font;
                            Globals.batesPrefix = batesPrefix;
                            using (PdfStamper stamper = new PdfStamper(reader, new FileStream(outputPath, FileMode.Create, FileAccess.Write)))
                            {
                                int pages = reader.NumberOfPages;
                                for (int j = 1; j <= pages; j++)
                                {
                                    /*
                                    if (j > 20)
                                    {
                                        int fakeVarForBreak = 420;
                                    }  */
                                    var batesStamp = batesPrefix + " " + ConstantNumber(currentBates, numDigits);
                                    Phrase p = new Phrase(batesStamp, font);
                                    currentBates++;
                                    float height = reader.GetCropBox(j).Height;
                                    float width = reader.GetCropBox(j).Width;
                                    float h = reader.GetPageSize(j).Height;
                                    float w = reader.GetPageSize(j).Width;
                                    iTextSharp.text.Rectangle rect = reader.GetPageSizeWithRotation(j);
                                    float printX, printY, switcherCoord;
                                    if (rect.Rotation == 90f || rect.Rotation == 270f)
                                    {
                                        switcherCoord = height;
                                        height = width;
                                        width = switcherCoord;
                                    }
                                    int alignment;
                                    
                                    switch (Globals.stampLocation)
                                    {
                                        case StampLocation.CENTER:
                                            printX = width / 2;
                                            printY = height / 2;
                                            alignment = Element.ALIGN_CENTER;
                                            break;
                                        case StampLocation.CENTER_BOTTOM:
                                            printX = width / 2;
                                            printY = 20;
                                            alignment = Element.ALIGN_CENTER;
                                            break;
                                        case StampLocation.CENTER_TOP:
                                            printX = width / 2;
                                            printY = height - 20;
                                            alignment = Element.ALIGN_CENTER;
                                            break;
                                        case StampLocation.LOWER_LEFT:
                                            printX = 20;
                                            printY = 20;
                                            alignment = Element.ALIGN_LEFT;
                                            break;
                                        case StampLocation.LOWER_RIGHT:
                                            printX = width - 20;
                                            printY = 20;
                                            alignment = Element.ALIGN_RIGHT;
                                            break;
                                        case StampLocation.UPPER_LEFT:
                                            printX = 20;
                                            printY = height - 20;
                                            alignment = Element.ALIGN_LEFT;
                                            break;
                                        case StampLocation.UPPER_RIGHT:
                                            printX = width - 20;
                                            printY = height - 20;
                                            alignment = Element.ALIGN_RIGHT;
                                            break;
                                        default:
                                            //Default: lower right
                                            printX = width - 20;
                                            printY = 20;
                                            alignment = Element.ALIGN_RIGHT;
                                            break;
                                    }//End Switch
                                    
                                    PdfContentByte contentByte = stamper.GetOverContent(j);
                                    PdfGState gState = new PdfGState();
                                    gState.FillOpacity = Globals.stampTransparency;
                                    contentByte.SetGState(gState);
                                    ColumnText.ShowTextAligned(contentByte, alignment, p, printX, printY, 0);
                                }//End For
                            }//End using
                            reader.Close();
                            string batesRange = (currentBates - reader.NumberOfPages).ToString() + "-" + (currentBates - 1).ToString();
                            Action action = new Action(fileNameOnly, fileName, fileNameOnly, outputPath, batesRange, batesPrefix, DateTime.Now);
                            Globals.history.Add(action);
                        }//End using
                    }
                    else if (Path.GetExtension(fileName) == ".gif" || Path.GetExtension(fileName) == ".bmp" || Path.GetExtension(fileName) == ".jpeg" || Path.GetExtension(fileName) == ".jpg"
                        || Path.GetExtension(fileName) == ".png" || Path.GetExtension(fileName) == ".tiff" || Path.GetExtension(fileName) == ".tif")
                    {
                        //Image stamping
                        string batesStamp = batesPrefix + " " + ConstantNumber(currentBates, numDigits);
                        currentBates++;                        
                        PointF loc = new PointF(10f, 10f); //TODO: Set Location
                        Bitmap img = (Bitmap)System.Drawing.Image.FromFile(fileName);
                        //variables needed for SmartStamping
                        const int borderSize = 72;
                        Bitmap img2 = new Bitmap(img.Width + (borderSize * 2), img.Height + (borderSize * 2));
                        System.Drawing.Rectangle originalImageRect = new System.Drawing.Rectangle(0, 0, img.Width, img.Height);
                        System.Drawing.Rectangle newImageRect = new System.Drawing.Rectangle(0, 0, img2.Width, img2.Height);
                        System.Drawing.Rectangle newImagePasteArea = new System.Drawing.Rectangle(borderSize, borderSize, img.Width, img.Height);
                        //End SmartStamp Variables
                        float newTransparency = Globals.stampTransparency;
                        int alpha = Convert.ToInt32((newTransparency) * 255);
                        alpha = Math.Min(alpha, 255);
                        alpha = Math.Max(alpha, 0);
                        SolidBrush brush = new SolidBrush(Color.FromArgb(alpha, 0, 0, 0));
                        RectangleF entireImage = new RectangleF(0, 0, img.Width, img.Height);                        

                        using (Graphics graphics = Graphics.FromImage(img))
                        {
                            //SmartStamp
                            /*
                            if (smartStamp)
                            {
                                const int borderSize = 10;
                                System.Drawing.Point pos = new System.Drawing.Point(20, 20);
                                using (Brush border = new SolidBrush(Color.White))
                                {
                                    //graphics.FillRectangle(border, pos.X - borderSize, pos.Y - borderSize, img.Width + borderSize, img.Height + borderSize);
                                }
                                graphics.DrawImage(img, pos);
                            }  */
                            //End SmartStamp
                            //For High Quality Text
                            graphics.SmoothingMode = SmoothingMode.AntiAlias;
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                            StringAlignment horizontal = new StringAlignment();
                            StringAlignment vertical = new StringAlignment();
                            switch (Globals.stampLocation)
                            {
                                case StampLocation.CENTER:
                                    horizontal = StringAlignment.Center;
                                    vertical = StringAlignment.Center;
                                    break;
                                case StampLocation.CENTER_BOTTOM:
                                    horizontal = StringAlignment.Center;
                                    vertical = StringAlignment.Far;
                                    break;
                                case StampLocation.CENTER_TOP:
                                    horizontal = StringAlignment.Center;
                                    vertical = StringAlignment.Near;
                                    break;
                                case StampLocation.LOWER_LEFT:
                                    horizontal = StringAlignment.Near;
                                    vertical = StringAlignment.Far;
                                    break;
                                case StampLocation.LOWER_RIGHT:
                                    horizontal = StringAlignment.Far;
                                    vertical = StringAlignment.Far;
                                    break;
                                case StampLocation.UPPER_LEFT:
                                    horizontal = StringAlignment.Near;
                                    vertical = StringAlignment.Near;
                                    break;
                                case StampLocation.UPPER_RIGHT:
                                    horizontal = StringAlignment.Far;
                                    vertical = StringAlignment.Near;
                                    break;
                                default:
                                    //Default: lower right
                                    horizontal = StringAlignment.Far;
                                    vertical = StringAlignment.Far;
                                    break;
                            }
                            StringFormat format = new StringFormat()
                            {
                                Alignment = horizontal,
                                LineAlignment = vertical
                            };
                            System.Drawing.FontFamily fam = new System.Drawing.FontFamily("Helvetica");
                            switch (Globals.batesFont)
                            {
                                case BatesFont.COURIER:
                                    fam = new System.Drawing.FontFamily("Helvetica");
                                    break;
                                case BatesFont.HELVETICA:
                                    fam = new System.Drawing.FontFamily("Helvetica");
                                    break;
                                case BatesFont.TIMES_NEW_ROMAN:
                                    fam = new System.Drawing.FontFamily("Helvetica");
                                    break;
                            }
                            System.Drawing.FontStyle style;
                            if (Globals.bold && Globals.italic)
                            {
                                style = FontStyle.Bold | FontStyle.Italic;
                            }
                            else if (Globals.bold)
                            {
                                style = FontStyle.Bold;
                            }
                            else if (Globals.italic)
                            {
                                style = FontStyle.Italic;
                            }
                            else
                            {
                                style = FontStyle.Regular;
                            }                       
                            using (System.Drawing.Font imageFont = new System.Drawing.Font(fam, Globals.font.Size, style)) //TODO: Select right font - Done, I think
                            {
                                if (smartStamp)
                                {
                                    CopyRegionIntoImage(img, originalImageRect, ref img2, newImagePasteArea);
                                    using (Graphics graphics2 = Graphics.FromImage(img2))
                                    {
                                        graphics2.DrawString(batesStamp, imageFont, brush, newImageRect, format);
                                    }
                                }
                                graphics.DrawString(batesStamp, imageFont, brush, entireImage, format); //TODO: Add Transparency - Done
                            }
                        }
                        fileNameOnly = Path.GetFileName(fileName);
                        outputPath = outputFolder + @"\" + fileNameOnly;
                        if (smartStamp)
                        {
                            img2.Save(outputPath);
                        }
                        else
                        {
                            img.Save(outputPath);
                        }
                    }
                    else if (Path.GetExtension(fileName) == ".doc" || Path.GetExtension(fileName) == ".dot" || Path.GetExtension(fileName) == ".docx" || Path.GetExtension(fileName) == ".docm"
                        || Path.GetExtension(fileName) == ".dotx" || Path.GetExtension(fileName) == ".dotm" || Path.GetExtension(fileName) == ".docb")
                    {
                        //Word Document - convert to PDF, then stamp using itext
                        Word2Pdf objWordToPDF = new Word2Pdf();

                        string fileExtention = Path.GetExtension(fileName);
                        
                        //fileNameOnly = Path.GetFileName(fileName);
                        //Might need to change this, replaces every instance of .pdf in path, might be multiple?
                        string changeExtension = fileName.Replace(fileExtention, ".pdf");
                        object FromLocation = fileName;
                        object ToLocation = changeExtension;
                        objWordToPDF.InputLocation = FromLocation;
                        objWordToPDF.OutputLocation = ToLocation;
                        objWordToPDF.Word2PdfCOnversion();

                        fileName = changeExtension;

                        //Copy of entire PDF Stamping code using the newly converted/created file:
                        //Smart Stamping
                        if (smartStamp)
                        {
                            PdfReader pReader = new PdfReader(fileName);
                            int numPages = pReader.NumberOfPages;
                            iTextSharp.text.Document doc = new iTextSharp.text.Document();
                            fileNameOnly = Path.GetFileName(fileName);
                            //outputPath = outputFolder + @"\" + fileNameOnly;                            
                            System.IO.Directory.CreateDirectory(tempFolder);
                            string tempPath = tempFolder + fileNameOnly;
                            PdfWriter pWriter = PdfWriter.GetInstance(doc, new FileStream(tempPath, FileMode.Create, FileAccess.Write));
                            doc.Open();
                            iTextSharp.text.Image img;
                            PdfImportedPage page;
                            PdfContentByte directContent = new PdfContentByte(pWriter);
                            for (int k = 1; k <= numPages; k++)
                            {
                                page = pWriter.GetImportedPage(pReader, k);
                                img = iTextSharp.text.Image.GetInstance(page);
                                img.ScaleAbsolute(PageSize.A4.Width - 72, PageSize.A4.Height - 72);
                                img.SetAbsolutePosition(36, 36);
                                directContent.AddImage(img);
                                doc.Add(img);
                                doc.NewPage();
                            }
                            doc.Close();
                            pReader.Close();
                            pWriter.Close();
                            fileName = tempPath;
                        }
                        //End Smart Stamping                   
                        using (var reader = new PdfReader(fileName))
                        {
                            fileNameOnly = Path.GetFileName(fileName);
                            outputPath = outputFolder + @"\" + fileNameOnly;
                            //var baseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                            //iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, 15);     
                            iTextSharp.text.Font font = Globals.font;
                            Globals.batesPrefix = batesPrefix;
                            using (PdfStamper stamper = new PdfStamper(reader, new FileStream(outputPath, FileMode.Create, FileAccess.Write)))
                            {
                                int pages = reader.NumberOfPages;
                                for (int j = 1; j <= pages; j++)
                                {
                                    /*
                                    if (j > 20)
                                    {
                                        int fakeVarForBreak = 420;
                                    }  */
                                    var batesStamp = batesPrefix + " " + ConstantNumber(currentBates, numDigits);
                                    Phrase p = new Phrase(batesStamp, font);
                                    currentBates++;
                                    float height = reader.GetCropBox(j).Height;
                                    float width = reader.GetCropBox(j).Width;
                                    float h = reader.GetPageSize(j).Height;
                                    float w = reader.GetPageSize(j).Width;
                                    iTextSharp.text.Rectangle rect = reader.GetPageSizeWithRotation(j);
                                    float printX, printY, switcherCoord;
                                    if (rect.Rotation == 90f || rect.Rotation == 270f)
                                    {
                                        switcherCoord = height;
                                        height = width;
                                        width = switcherCoord;
                                    }
                                    int alignment;

                                    switch (Globals.stampLocation)
                                    {
                                        case StampLocation.CENTER:
                                            printX = width / 2;
                                            printY = height / 2;
                                            alignment = Element.ALIGN_CENTER;
                                            break;
                                        case StampLocation.CENTER_BOTTOM:
                                            printX = width / 2;
                                            printY = 20;
                                            alignment = Element.ALIGN_CENTER;
                                            break;
                                        case StampLocation.CENTER_TOP:
                                            printX = width / 2;
                                            printY = height - 20;
                                            alignment = Element.ALIGN_CENTER;
                                            break;
                                        case StampLocation.LOWER_LEFT:
                                            printX = 20;
                                            printY = 20;
                                            alignment = Element.ALIGN_LEFT;
                                            break;
                                        case StampLocation.LOWER_RIGHT:
                                            printX = width - 20;
                                            printY = 20;
                                            alignment = Element.ALIGN_RIGHT;
                                            break;
                                        case StampLocation.UPPER_LEFT:
                                            printX = 20;
                                            printY = height - 20;
                                            alignment = Element.ALIGN_LEFT;
                                            break;
                                        case StampLocation.UPPER_RIGHT:
                                            printX = width - 20;
                                            printY = height - 20;
                                            alignment = Element.ALIGN_RIGHT;
                                            break;
                                        default:
                                            //Default: lower right
                                            printX = width - 20;
                                            printY = 20;
                                            alignment = Element.ALIGN_RIGHT;
                                            break;
                                    }//End Switch

                                    PdfContentByte contentByte = stamper.GetOverContent(j);
                                    PdfGState gState = new PdfGState();
                                    gState.FillOpacity = Globals.stampTransparency;
                                    contentByte.SetGState(gState);
                                    ColumnText.ShowTextAligned(contentByte, alignment, p, printX, printY, 0);
                                }//End For
                            }//End using
                            reader.Close();
                            string batesRange = (currentBates - reader.NumberOfPages).ToString() + "-" + (currentBates - 1).ToString();
                            Action action = new Action(fileNameOnly, fileName, fileNameOnly, outputPath, batesRange, batesPrefix, DateTime.Now);
                            Globals.history.Add(action);
                        }//End using
                        /*
                        //Word Document Stamping
                        fileNameOnly = Path.GetFileName(fileName);
                        outputPath = outputFolder + @"\" + fileNameOnly;
                        if (Globals.stampLocation == StampLocation.CENTER_BOTTOM || Globals.stampLocation == StampLocation.LOWER_LEFT || Globals.stampLocation == StampLocation.LOWER_RIGHT
                            || Globals.stampLocation == StampLocation.CENTER_TOP || Globals.stampLocation == StampLocation.UPPER_LEFT || Globals.stampLocation == StampLocation.UPPER_RIGHT)
                        {
                            File.Copy(fileName, outputPath);
                            bool header = false;
                            bool footer = true;
                            // Get application object
                            Microsoft.Office.Interop.Word.Application WordApplication = new Microsoft.Office.Interop.Word.Application();

                            // Get document object
                            object Miss = System.Reflection.Missing.Value;
                            object ReadOnly = false;
                            object Visible = false;
                            object OutputPath = outputPath;
                            Microsoft.Office.Interop.Word.Document Doc = WordApplication.Documents.Open(ref OutputPath, ref Miss, ref ReadOnly, ref Miss, ref Miss, ref Miss, ref Miss, ref Miss, ref Miss, ref Miss, ref Miss, ref Visible, ref Miss, ref Miss, ref Miss, ref Miss);

                            //Find out where to stamp
                            if (Globals.stampLocation == StampLocation.CENTER_BOTTOM || Globals.stampLocation == StampLocation.LOWER_LEFT || Globals.stampLocation == StampLocation.LOWER_RIGHT)
                            {
                                footer = true;
                            }
                            else if (Globals.stampLocation == StampLocation.CENTER_TOP || Globals.stampLocation == StampLocation.UPPER_LEFT || Globals.stampLocation == StampLocation.UPPER_RIGHT)
                            {
                                header = true;
                            }
                            else
                            {
                                //TODO: Do something for location in Center??
                            }
                            foreach (Microsoft.Office.Interop.Word.Page p in Doc.Sections)
                            {

                            }
                            foreach (Microsoft.Office.Interop.Word.Section wordSection in Doc.Sections)
                            {
                                Microsoft.Office.Interop.Word.Range range;
                                if (footer)
                                {
                                    range = wordSection.Footers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                                }
                                else if (header)
                                {
                                    range = wordSection.Headers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                                }
                                else
                                {
                                    range = wordSection.Headers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range; //TODO: Change to something for centered text
                                }
                                //Color color = Color.FromArgb(125, 255, 255, 255);
                                range.Font.Color = Microsoft.Office.Interop.Word.WdColor.wdColorBlack;
                                range.Font.Size = Globals.font.Size;
                                switch (Globals.batesFont)
                                {
                                    case BatesFont.COURIER:
                                        range.Font.Name = "Courier";
                                        break;
                                    case BatesFont.HELVETICA:
                                        range.Font.Name = "Helvetica";
                                        break;
                                    case BatesFont.TIMES_NEW_ROMAN:
                                        range.Font.Name = "Times New Roman";
                                        break;
                                }
                                // rng.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                                if (Globals.stampLocation == StampLocation.LOWER_LEFT || Globals.stampLocation == StampLocation.UPPER_LEFT)
                                {
                                    range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                                }
                                else if (Globals.stampLocation == StampLocation.LOWER_RIGHT || Globals.stampLocation == StampLocation.UPPER_RIGHT)
                                {
                                    range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
                                }
                                else if (Globals.stampLocation == StampLocation.CENTER_BOTTOM || Globals.stampLocation == StampLocation.CENTER_TOP)
                                {
                                    range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                                }
                                //range.Font.Fill.Transparency = 0.1f;
                                range.Font.Bold = Globals.bold ? 1 : 0;
                                range.Font.Italic = Globals.italic ? 1 : 0;
                                
                                var batesStamp = batesPrefix + " " + ConstantNumber(currentBates, numDigits);
                                currentBates++;
                                range.Text = batesStamp;
                            }
                            Doc.Save();
                            Doc.Close();
                        }
                        else  //It is centered, so create watermark
                        {
                            Spire.Doc.Document doc = new Spire.Doc.Document();
                            doc.LoadFromFile(fileName);
                            TextWatermark textWatermark = new TextWatermark();
                            var batesStamp = batesPrefix + " " + ConstantNumber(currentBates, numDigits);
                            currentBates++;
                            textWatermark.Text = batesStamp;
                            textWatermark.FontSize = Globals.font.Size;
                            int alpha = Convert.ToInt32((Globals.stampTransparency) * 255);
                            alpha = Math.Min(alpha, 255);
                            alpha = Math.Max(alpha, 0);
                            textWatermark.Color = Color.FromArgb(alpha, 0, 0, 0);
                            textWatermark.Layout = Spire.Doc.Documents.WatermarkLayout.Horizontal;
                            doc.Watermark = textWatermark;
                            doc.SaveToFile(outputPath);
                        }    */
                    }  
                    else
                    {
                        //TODO: Invalid file type
                    }//End if   
                }//End For
                form1.ClearStatus(files.Length + " Files Stamped to " + Path.GetDirectoryName(outputPath) + @"\");
                form1.ClearList();
                if (smartStamp)
                {
                    System.IO.DirectoryInfo dir = new DirectoryInfo(tempFolder);
                    foreach (FileInfo file in dir.GetFiles())
                    {
                        file.Delete();
                    }
                }
                Globals.currentBates = currentBates;
                form1.UpdateCurrentBatesNumber(currentBates, batesPrefix);
                MessageBox.Show("Bates Stamping Complete.", Globals.appName);
            }
        }
        #endregion
                
    }

    public partial class PDFUtility
    {
        //I think I can delete this but I'm too scared to do it now
        #region Helper Functions
        
        #endregion

        #region Main Logic
        
        #endregion

    } //End Class PdfUtility

    public class Action
    {
        public string oldFile, oldPath, newFile, newPath, batesRange, batesPrefix;
        public DateTime date;

        public Action(string oldFile, string oldPath, string newFile, string newPath, string batesRange, string batesPrefix, DateTime date)
        {
            this.oldFile = oldFile;
            this.oldPath = oldPath;
            this.newFile = newFile;
            this.newPath = newPath;
            this.batesRange = batesRange;
            this.batesPrefix = batesPrefix;
            this.date = date;
        }
    }

    public class ActionToStamp
    {
        public string fileNumber, fileName, pageCount, path;
        public ActionToStamp(string fileNumber, string fileName, string pageCount, string path)
        {
            this.fileNumber = fileNumber;
            this.fileName = fileName;
            this.pageCount = pageCount;
            this.path = path;
        }
    }

    static class SaveFileStatic
    {
        //I think I can delete this too since I switched everything over to Globals.
        private static string _name;
        public static string name
        {
            get { return _name; }
            set { _name = value; }
        }

        private static string _outputFolder;
        public static string outputFolder
        {
            get { return _outputFolder; }
            set { _outputFolder = value; }
        }

        private static string _batesPrefix;
        public static string batesPrefix
        {
            get { return _batesPrefix; }
            set { _batesPrefix = value; }
        }

        private static int _batesNumber;
        public static int batesNumber
        {
            get { return _batesNumber; }
            set { _batesNumber = value; }
        }

        private static StampLocation _location = StampLocation.LOWER_RIGHT;
        public static StampLocation location
        {
            get { return _location; }
            set { _location = value; }
        }

        private static float _transparency;
        public static float transparency
        {
            get { return _transparency; }
            set { _transparency = value; }
        }

        private static iTextSharp.text.Font _font;
        public static iTextSharp.text.Font font
        {
            get { return _font; }
            set { _font = value; }
        }

        private static bool _smartStamp;
        public static bool smartStamp
        {
            get { return _smartStamp; }
            set { _smartStamp = value; }
        }

        private static List<Action> _history;
        public static List<Action> history
        {
            get { return _history; }
            set { _history = value; }
        }
    }

    public class SaveFile
    {
        private string _name;
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _outputFolder;
        public string outputFolder
        {
            get { return _outputFolder; }
            set { _outputFolder = value; }
        }

        private string _batesPrefix;
        public string batesPrefix
        {
            get { return _batesPrefix; }
            set { _batesPrefix = value; }
        }

        private int _batesNumber;
        public int batesNumber
        {
            get { return _batesNumber; }
            set { _batesNumber = value; }
        }

        private StampLocation _location = StampLocation.LOWER_RIGHT;
        public StampLocation location
        {
            get { return _location; }
            set { _location = value; }
        }

        private float _transparency;
        public float transparency
        {
            get { return _transparency; }
            set { _transparency = value; }
        }

        private iTextSharp.text.Font _font;
        public iTextSharp.text.Font font
        {
            get { return _font; }
            set { _font = value; }
        }

        private static bool _smartStamp;
        public static bool smartStamp
        {
            get { return _smartStamp; }
            set { _smartStamp = value; }
        }

        private List<Action> _history;
        public List<Action> history
        {
            get { return _history; }
            set { _history = value; }
        }

        private List<ActionToStamp> _toStamp;
        public List<ActionToStamp> toStamp
        {
            get { return _toStamp; }
            set { _toStamp = value; }
        }
        private BatesFont _batesFont;
        public BatesFont batesFont
        {
            get { return _batesFont; }
            set { _batesFont = value; }
        }

        private bool _bold = true;
        public bool bold
        {
            get { return _bold; }
            set { _bold = value; }
        }

        private bool _italic = false;
        public bool italic
        {
            get { return _italic; }
            set { _italic = value; }
        }
    }
    
    static class Globals
    {
        private static string _folderToStamp = "";
        public static string folderToStamp
        {
            get { return _folderToStamp; }
            set { _folderToStamp = value; }
        }
        private static string _outputFolder = AppDomain.CurrentDomain.BaseDirectory + @"Output\";
        public static string outputFolder
        {
            get { return _outputFolder; }
            set { _outputFolder = value; }
        }
        private static string _currentProject = "Untitled Project";
        public static string currentProject
        {
            get { return _currentProject; }
            set { _currentProject = value; }
        }
        private static string _appName = "";
        public static string appName
        {
            get { return _appName; }
            set { _appName = value; }
        }
        private static string _batesPrefix;
        public static string batesPrefix
        {
            get { return _batesPrefix; }
            set { _batesPrefix = value; }
        }
        private static int _currentBates = 1;
        public static int currentBates
        {
            get { return _currentBates; }
            set { _currentBates = value; }
        }

        private static StampLocation _stampLocation = StampLocation.LOWER_RIGHT;
        public static StampLocation stampLocation
        {
            get { return _stampLocation; }
            set { _stampLocation = value; }
        }

        private static float _stampTransparency = 1;
        public static float stampTransparency
        {
            get { return _stampTransparency; }
            set { _stampTransparency = value; }
        }
        private static int _indexSelectedBatesList = 0;
        public static int indexSelectedBatesList
        {
            get { return _indexSelectedBatesList; }
            set { _indexSelectedBatesList = value; }
        }

        //var baseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        //iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, 15);
        private static iTextSharp.text.Font _font;
        public static iTextSharp.text.Font font
        {
            get { return _font; }
            set { _font = value; }
        }

        private static bool _bold = true;
        public static bool bold
        {
            get { return _bold; }
            set { _bold = value; }
        }

        private static bool _italic = false;
        public static bool italic
        {
            get { return _italic; }
            set { _italic = value; }
        }

        private static bool _smartStamp = false;
        public static bool smartStamp
        {
            get { return _smartStamp; }
            set { _smartStamp = value; }
        }

        private static List<Action> _history;
        public static List<Action> history
        {
            get { return _history; }
            set { _history = value; }
        }

        private static List<ActionToStamp> _toStamp;
        public static List<ActionToStamp> toStamp
        {
            get { return _toStamp; }
            set { _toStamp = value; }
        }

        private static bool _isSaved = false;
        public static bool isSaved
        {
            get { return _isSaved; }
            set { _isSaved = value; }
        }

        private static bool _isCurrent = true;
        public static bool isCurrent
        {
            get { return _isCurrent; }
            set { _isCurrent = value; }
        }

        private static string _currentSavePath = "";
        public static string currentSavePath
        {
            get { return _currentSavePath; }
            set { _currentSavePath = value; }
        }

        private static BatesFont _batesFont;
        public static BatesFont batesFont
        {
            get { return _batesFont; }
            set { _batesFont = value; }
        }
    }
}


