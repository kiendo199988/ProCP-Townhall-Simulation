using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace TestAppTownHall
{
    public class CircularPanel : System.Windows.Forms.Panel
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            GraphicsPath g = new GraphicsPath();
            g.AddEllipse(0, 0,20,20);
            this.Region = new System.Drawing.Region(g);
            base.OnPaint(e);
        }
    }
}
