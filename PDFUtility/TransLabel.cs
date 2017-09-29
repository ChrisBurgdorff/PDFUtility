using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDFUtility
{
    public partial class TransLabel : System.Windows.Forms.Label
    {
        private int _transparency = 255;

        public int Transparency
        {
            get { return _transparency; }
            set { _transparency = value; Invalidate(); }
        }

        public TransLabel()
        { }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Brush b = new SolidBrush(Color.FromArgb(_transparency, 0, 0, 0));
            pe.Graphics.DrawString(this.Text, this.Font, b, new PointF(0, 0));
            b.Dispose();
        }
    }
}
