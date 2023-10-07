using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveOS.WinManager;

namespace WaveOS.GraphicsWidgets
{
    internal class Label
    {
        public window parent;
        public int r;
        public int g;
        public int b;
        public string label;
        public int x;
        public int y;

        public Label(window parentWindow, int x, int y, string label, int r = 255, int g = 255, int b = 255) {
            this.parent = parentWindow;
            this.x = x;
            this.y = y;
            this.label = label;
            this.r = r;
            this.g = g;
            this.b = b;
        }

        public void draw()
        {
            if (parent.wndType == WINDOWTYPE.Normal)
            {
                y += 21;
            }
            ImprovedVBE._DrawACSIIString(label, parent.x + x, parent.y + y, ImprovedVBE.colourToNumber(r, g, b));
        }
    }
}
