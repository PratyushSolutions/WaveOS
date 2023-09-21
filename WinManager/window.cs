using Cosmos.System;
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
        public WINDOWTYPE wndType = WINDOWTYPE.Normal;
        public bool run = false;
        public bool moving = false;

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
            ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(11,11,11), x - 2, y - 2, width + 2 + 2, height + 2 + 2);
            ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(163, 194, 255), x, y, width, height);
            if (wndType == WINDOWTYPE.Normal)
            {
                ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(11,11,11), x, y, width, 20);
            }
            if (moving && MouseManager.MouseState == MouseState.Left && MouseManager.X > 0 && MouseManager.Y > 25)
            {
                x = (int)MouseManager.X;
                y = (int)MouseManager.Y;
            }
            if ((MouseManager.X > x && MouseManager.X < x + width && MouseManager.Y > y && MouseManager.Y < y + 20 && MouseManager.MouseState == MouseState.Left)
                && wndType == WINDOWTYPE.Normal)
            {
                moving = true;
            } else if ((MouseManager.X > x && MouseManager.X < x + width && MouseManager.Y > y && MouseManager.Y < y + height && MouseManager.MouseState == MouseState.Left)
                && wndType == WINDOWTYPE.FullyDraggable)
            {
                moving = true;
            }
        }
    }
}
