using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveOS.WinManager
{
    public class window
    {
        public int x;
        public int y;
        public int width;
        public int height;
        public bool run = false;

        public window()
        {
            run = true;
        }

        public void render()
        {
            if (run)
                update();

            return;
        }

        public void close()
        {
            run = false;
        }

        public void update()
        {
            ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(75, 123, 219), x, y, width, height);
        }
    }
}
