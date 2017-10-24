using PDFUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using PDFUtility;

namespace PDFUtilityHistory
{
    public partial class formHistory : Form
    {
        public formHistory()
        {
            InitializeComponent();
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
                    Microsoft.Office.Interop.Excel.Range cells = (Microsoft.Office.Interop.Excel.Range)ws.get_Range("F2").EntireColumn;
                    // set each cell's format to Text
                    cells.NumberFormat = "@";
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

        private void formHistory_Load(object sender, EventArgs e)
        {
            //Get History and populate ListView
            List<PDFUtility.Action> history = Globals.history;
            for (int i = 0; i < history.Count; i++)
            {
                ListViewItem item = new ListViewItem(new[] { history[i].oldFile, history[i].newFile, history[i].oldPath, history[i].newPath, history[i].batesPrefix, history[i].batesRange, history[i].date.ToShortDateString() });
                lstHistory.Items.Add(item);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExportHistory_Click(object sender, EventArgs e)
        {
            ExportToExcel(lstHistory);
        }
    }
}
