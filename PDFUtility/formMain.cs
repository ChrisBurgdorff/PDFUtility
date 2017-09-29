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

namespace PDFUtility
{
    #region enums
    public enum Location
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
    #endregion

    public partial class formMain : Form
    {
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
            InitializeSaveFile();
        }
        #endregion

        #region Helper Functions
        private void FixFileList()
        {
            int fileNum = 1;
            int startingBates = 1;
            int endingBates;
            int numPages;
            if (int.TryParse(txtStartNumber.Text, out startingBates)) { } //Puts int value of text from start number field in startingBates
            else
            { //if empty string or non-int make everything 1, which is default
                startingBates = 1;
                txtStartNumber.Text = "1";
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
                    lstBatesFiles.Items[i].SubItems[3].Text = startingBates.ToString() + " - " + endingBates.ToString();
                    startingBates = endingBates + 1;
                }
            }
            if (lstBatesFiles.Items.Count > 0)
            {
                lblEmptyList.Visible = false;
                lblEmptyList.Enabled = false;
            }
            else
            {
                lblEmptyList.Visible = true;
                lblEmptyList.Enabled = true;
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
                    app.Visible = false;
                    for (int j = 1; j <= lv.Columns.Count; j++)
                    {
                        var newWidth = Math.Min(255, lv.Columns[j - 1].Width / 2);
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
                    MessageBox.Show("Exported Successfully.");
                }
            }
        }
        #endregion

        #region Buttons (Other than Menu)
        private void btnSelectOutput_Click(object sender, EventArgs e)
        {
            dialogFolderBates.ShowDialog();
            var folder = dialogFolderBates.SelectedPath;
            Globals.outputFolder = folder;
            txtFolderBates.Text = folder;
            SaveFileStatic.outputFolder = folder;
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
                pu.BatesStamp(files, startNumber, batesPrefix);
                txtStartNumber.Text = Globals.currentBates.ToString();
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
            ofd.Filter = "Bates Plus Plus Files|*.bpp";
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
                    string pageCount = pu.PageCount(s[i]).ToString();
                    ListViewItem item = new ListViewItem(new[] { fileNumber, fileName, pageCount, "N/A", path });
                    lstBatesFiles.Items.Add(item);
                }
            }
            FixFileList();
        }

        public void InitializeListView()
        {
            lstBatesFiles.View = View.Details;
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
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
        #endregion

        #region AddFilesAndFolders        
        public void AddFolder(string folderPath, bool includeSubfolders)
        {
            lblEmptyList.Visible = false;
            lblEmptyList.Enabled = false;
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
            for (int i = 0; i < files.Length; i++)
            {
                fileName = Path.GetFileName(files[i]);
                fileNumber = (1 + lstBatesFiles.Items.Count).ToString();
                pageCount = pu.PageCount(files[i]).ToString();
                ListViewItem item = new ListViewItem(new[] {fileNumber, fileName, pageCount, "N/A", path });
                lstBatesFiles.Items.Add(item);
                UpdateStatus(i+1, files.Length, Activity.ADDING_FILES);
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
            lblEmptyList.Visible = false;
            lblEmptyList.Enabled = false;
            for (int i = 0; i < files.Length; i++)
            {
                fileName = Path.GetFileName(files[i]);
                fileNumber = (1 + lstBatesFiles.Items.Count).ToString();
                pageCount = pu.PageCount(files[i]).ToString();
                path = Path.GetDirectoryName(files[i]);
                ListViewItem item = new ListViewItem(new[] { fileNumber, fileName, pageCount, "N/A", path });
                lstBatesFiles.Items.Add(item);
                UpdateStatus(i+1, files.Length, Activity.ADDING_FILES);
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

        #region Save and Load
        private void InitializeSaveFile()
        {
            //Initialize current static save file to all default values
            SaveFileStatic.batesNumber = 1;
            SaveFileStatic.batesPrefix = "";
            SaveFileStatic.font = new iTextSharp.text.Font(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 15);
            SaveFileStatic.history = new List<Action>();
            SaveFileStatic.location = Globals.stampLocation;
            SaveFileStatic.name = "";
            SaveFileStatic.outputFolder = Globals.outputFolder;
            SaveFileStatic.transparency = 1f;
        }

        public string SaveProject(string path)
        {
            string response = "Project Saved Successfully.";
            SaveFile s = new SaveFile();
            s.batesNumber = SaveFileStatic.batesNumber;
            s.batesPrefix = SaveFileStatic.batesPrefix;
            s.font = SaveFileStatic.font;
            s.history = SaveFileStatic.history;
            s.location = SaveFileStatic.location;
            s.name = SaveFileStatic.name;
            s.outputFolder = SaveFileStatic.outputFolder;
            s.transparency = SaveFileStatic.transparency;
            string data = JsonConvert.SerializeObject(s);
            System.IO.File.WriteAllText(path, data);
            return response;
        }

        public string LoadProject(string path)
        {
            string response = "Project Loaded Successfully.";
            string data = System.IO.File.ReadAllText(path);
            SaveFile s = JsonConvert.DeserializeObject<SaveFile>(data);
            SaveFileStatic.batesNumber = s.batesNumber;
            SaveFileStatic.batesPrefix = s.batesPrefix;
            SaveFileStatic.font = s.font;
            SaveFileStatic.history = s.history;
            SaveFileStatic.location = s.location;
            SaveFileStatic.name = s.name;
            SaveFileStatic.outputFolder = s.outputFolder;
            SaveFileStatic.transparency = s.transparency;
            Globals.outputFolder = s.outputFolder;
            txtFolderBates.Text = s.outputFolder;
            return response;
        }
        #endregion

        private void nEwToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("SDF");
        }

        private void aboutBatesPlusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formAbout form = new formAbout();
            form.Show();
        }
    }

    public partial class PDFUtility
    {
        #region Helper Functions
        public int PageCount(string pdfFile)
        {
            int pageCount = 0;
            if (Path.GetExtension(pdfFile) == ".pdf")
            {
                using (var reader = new PdfReader(pdfFile))
                {
                    pageCount = reader.NumberOfPages;
                }
            }
            return pageCount;
        }
        #endregion

        #region Main Logic
        public void BatesStamp(string[] files, int startNumber, string batesPrefix)
        {
            string outputFolder = Globals.outputFolder;
            string fileNameOnly;
            string fileName;
            string outputPath = "";
            int currentBates = startNumber;
            formMain form1 = new formMain();
            form1 = (formMain)System.Windows.Forms.Application.OpenForms[0];
            if (Directory.Exists(outputFolder))
            {
                for (int i = 0; i < files.Length; i++)
                {
                    fileName = files[i];
                    
                    form1.UpdateStatus(i+1, files.Length, Activity.BATES_STAMPING);
                    if (Path.GetExtension(fileName) == ".pdf")
                    {
                        //Smart Stamping
                        PdfReader pReader = new PdfReader(fileName);
                        int numPages = pReader.NumberOfPages;
                        Document doc = new Document();
                        fileNameOnly = Path.GetFileName(fileName);
                        outputPath = outputFolder + @"\" + fileNameOnly;
                        PdfWriter pWriter = PdfWriter.GetInstance(doc, new FileStream(outputPath, FileMode.Create, FileAccess.Write));
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
                        fileName = outputPath;
                        //End Smart Stamping
                        using (var reader = new PdfReader(fileName))
                        {
                            fileNameOnly = Path.GetFileName(fileName);
                            outputPath = outputFolder + @"\" + fileNameOnly;
                            var baseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, 15);                    
                            using (PdfStamper stamper = new PdfStamper(reader, new FileStream(outputPath, FileMode.Create, FileAccess.Write)))
                            {
                                int pages = reader.NumberOfPages;
                                for (int j = 1; j <= pages; j++)
                                {
                                    var batesStamp = batesPrefix + " " + currentBates.ToString();
                                    Phrase p = new Phrase(batesStamp, font);
                                    currentBates++;
                                    float height = reader.GetCropBox(j).Height;
                                    float width = reader.GetCropBox(j).Width;
                                    int alignment;
                                    float printX, printY;
                                    switch (Globals.stampLocation)
                                    {
                                        case Location.CENTER:
                                            printX = width / 2;
                                            printY = height / 2;
                                            alignment = Element.ALIGN_CENTER;
                                            break;
                                        case Location.CENTER_BOTTOM:
                                            printX = width / 2;
                                            printY = 20;
                                            alignment = Element.ALIGN_CENTER;
                                            break;
                                        case Location.CENTER_TOP:
                                            printX = width / 2;
                                            printY = height - 20;
                                            alignment = Element.ALIGN_CENTER;
                                            break;
                                        case Location.LOWER_LEFT:
                                            printX = 20;
                                            printY = 20;
                                            alignment = Element.ALIGN_LEFT;
                                            break;
                                        case Location.LOWER_RIGHT:
                                            printX = width - 20;
                                            printY = 20;
                                            alignment = Element.ALIGN_RIGHT;
                                            break;
                                        case Location.UPPER_LEFT:
                                            printX = 20;
                                            printY = height - 20;
                                            alignment = Element.ALIGN_LEFT;
                                            break;
                                        case Location.UPPER_RIGHT:
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
                        }//End using
                    } //End if   
                }//End For
                form1.ClearStatus(files.Length + " Files Stamped to " + Path.GetDirectoryName(outputPath) + @"\");
                form1.ClearList();
                MessageBox.Show("Bates Stamping Complete.", Globals.appName);
            }
            else
            {
                //Directory doesn't exist
                //Create or warn not sure yet
            }
        }
        #endregion

    } //End Class PdfUtility

    public class Action
    {
        private string oldFile, oldPath, newFile, newPath, batesRange, batesPrefix;
        private DateTime date;

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

    static class SaveFileStatic
    {
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

        private static Location _location = Location.LOWER_RIGHT;
        public static Location location
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

        private Location _location = Location.LOWER_RIGHT;
        public Location location
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

        private List<Action> _history;
        public List<Action> history
        {
            get { return _history; }
            set { _history = value; }
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

        private static int _currentBates = 1;
        public static int currentBates
        {
            get { return _currentBates; }
            set { _currentBates = value; }
        }

        private static Location _stampLocation = Location.LOWER_RIGHT;
        public static Location stampLocation
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

        private static iTextSharp.text.Font _font;
        public static iTextSharp.text.Font font
        {
            get { return _font; }
            set { _font = value; }
        }
    }
}
