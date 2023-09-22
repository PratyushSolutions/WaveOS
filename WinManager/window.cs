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
        public bool showed = false;
        public bool moving = false;
        public bool focussed = false;

        public window()
        {
            showed = true;
        }

        public void render()
        {
            if (showed)
                update();

            return;
        }

        public void close()
        {
            showed = false;
        }

        public void update()
        {
            ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(11,11,11), x - 2, y - 2, width + 2 + 2, height + 2 + 2);
            if (focussed)
            {
                ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(163, 194, 255), x, y, width, height);
            } else
            {
                ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(77, 77, 77), x, y, width, height);
            }
            if (wndType == WINDOWTYPE.Normal)
            {
                ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(11,11,11), x, y, width, 20);
            }
            if (moving && MouseManager.MouseState == MouseState.Left && MouseManager.X > 0 && MouseManager.Y > 25 && focussed)
            {
                WaveConfigs.WindowMgr.activeWindowDragging = true;
                x = (int)MouseManager.X;
                y = (int)MouseManager.Y;
            }
            if (MouseManager.MouseState == MouseState.None)
            {
                moving = false;
                WaveConfigs.WindowMgr.activeWindowDragging = false;
            }
            if ((MouseManager.X > x && MouseManager.X < x + width && MouseManager.Y > y && MouseManager.Y < y + 20 && MouseManager.MouseState == MouseState.Left)
                && wndType == WINDOWTYPE.Normal && focussed)
            {
                moving = true;                
            } else if ((MouseManager.X > x && MouseManager.X < x + width && MouseManager.Y > y && MouseManager.Y < y + height && MouseManager.MouseState == MouseState.Left)
                && wndType == WINDOWTYPE.FullyDraggable && focussed)
            {
                moving = true;
            }

            if (MouseManager.X > x && MouseManager.X < x + width && MouseManager.Y > y && MouseManager.Y < y + 20 && MouseManager.MouseState == MouseState.Left
                && wndType == WINDOWTYPE.Normal && !focussed && !WaveConfigs.WindowMgr.activeWindowDragging)
            {
                WaveConfigs.WindowMgr.moveWindowToFront(this);
            } else if (MouseManager.X > x && MouseManager.X < x + width && MouseManager.Y > y && MouseManager.Y < y + height && MouseManager.MouseState == MouseState.Left
                && wndType == WINDOWTYPE.FullyDraggable && !focussed && !WaveConfigs.WindowMgr.activeWindowDragging)
            {
                WaveConfigs.WindowMgr.moveWindowToFront(this);
            }
        }
    }
}
