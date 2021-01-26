using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestAppTownHall
{
    class OverlayControl : System.Windows.Forms.Control
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams CP = base.CreateParams;
                CP.ExStyle |= 0x20;
                return CP;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //base.OnPaintBackground(pevent);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Red, 4f);
            e.Graphics.DrawEllipse(pen, this.ClientRectangle);
        }
    }
}
