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
using PDFUtility;

namespace PDFUtilityHistory
{
    public partial class formHistory : Form
    {
        public formHistory()
        {
            InitializeComponent();
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
    }
}
