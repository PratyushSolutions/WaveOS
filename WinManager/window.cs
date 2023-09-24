using Cosmos.System;
using Cosmos.System.Graphics;
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
        public Action drawing;
        public string title;
        public Action? keyhandler = null;
        public KeyEvent currentKey;
        public Bitmap logo;

        public window(Action ControlsDraw, string title, Action? keyHandler = null, Bitmap? Logo = null)
        {
            showed = true;
            drawing = ControlsDraw;
            this.title = title;
            this.keyhandler = keyHandler;
            this.logo = Logo;
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
            ImprovedVBE.DrawFilledCircle(ImprovedVBE.colourToNumber(30, 31, 35), x + 7, y + 9, 9);
            ImprovedVBE.DrawFilledCircle(ImprovedVBE.colourToNumber(30, 31, 35), x + width - 8, y + 9, 9);

            ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(30, 31, 35), x - 2, y - 2 + 10, width + 2 + 2, height + 2 + 2 + 10);

            ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(30, 31, 35), x + 7, y + height + 20, width - 10, 12);
            ImprovedVBE.DrawFilledCircle(ImprovedVBE.colourToNumber(30, 31, 35), x + 7, y + height + 14 + 9, 9);
            ImprovedVBE.DrawFilledCircle(ImprovedVBE.colourToNumber(30, 31, 35), x + width - 8, y + height + 14 + 9, 9);

            if (focussed)
            {
                //ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(163, 194, 255), x + 7, y + height + 20 + 5, width - 10, 9);
                //ImprovedVBE.DrawFilledCircle(ImprovedVBE.colourToNumber(163, 194, 255), x + 7, y + height + 14 + 9, 9);
                //ImprovedVBE.DrawFilledCircle(ImprovedVBE.colourToNumber(163, 194, 255), x + width - 8, y + height + 14 + 9, 9);

                ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(30, 31, 35), x + 7, y + height + 20 + 5, width - 10, 9);
                ImprovedVBE.DrawFilledCircle(ImprovedVBE.colourToNumber(30, 31, 35), x + 7, y + height + 14 + 9, 9);
                ImprovedVBE.DrawFilledCircle(ImprovedVBE.colourToNumber(30, 31, 35), x + width - 8, y + height + 14 + 9, 9);

                ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(30, 31, 35), x - 2, y - 2 + 10, width + 2 + 2, height + 2 + 2 + 10);
                //ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(163, 194, 255), x, y + 14, width, height + 14);
            } else
            {
                //ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(77, 77, 77), x + 7, y + height + 20 + 5, width - 10, 9);
                //ImprovedVBE.DrawFilledCircle(ImprovedVBE.colourToNumber(77, 77, 77), x + 7, y + height + 14 + 9, 9);
                //ImprovedVBE.DrawFilledCircle(ImprovedVBE.colourToNumber(77, 77, 77), x + width - 8, y + height + 14 + 9, 9);

                ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(30, 31, 35), x + 7, y + height + 20 + 5, width - 10, 9);
                ImprovedVBE.DrawFilledCircle(ImprovedVBE.colourToNumber(30, 31, 35), x + 7, y + height + 14 + 9, 9);
                ImprovedVBE.DrawFilledCircle(ImprovedVBE.colourToNumber(30, 31, 35), x + width - 8, y + height + 14 + 9, 9);
                ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(30, 31, 35), x - 2, y - 2 + 10, width + 2 + 2, height + 2 + 2 + 10);
                //ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(77, 77, 77), x, y, width, height);
            }
            if (wndType == WINDOWTYPE.Normal)
            {
                ImprovedVBE.DrawFilledCircle(ImprovedVBE.colourToNumber(60, 59, 63), x + 7, y + 9, 9);
                ImprovedVBE.DrawFilledCircle(ImprovedVBE.colourToNumber(60, 59, 63), x + width - 8, y + 9, 9);
                ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(60, 59, 63), x + 5, y, width - 10, 20);

                ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(60, 59, 63), x - 2, y + 10, width + 2 + 2, 2 + 10);
                ImprovedVBE._DrawACSIIString(title, x + ((width / 2) - (title.Length * 4)), y + 5, ImprovedVBE.colourToNumber(255, 255, 255));
                ImprovedVBE.DrawFilledCircle(ImprovedVBE.colourToNumber(255, 91, 82), x + 9, y + 10, 5);

                if (logo != null)
                    ImprovedVBE.DrawImageAlpha(logo, x + ((width / 2) - (title.Length * 4)) - 23, y + 4);
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
            if ((MouseManager.X > x && MouseManager.X < x + width - 15 && MouseManager.Y > y && MouseManager.Y < y + 20 && MouseManager.MouseState == MouseState.Left)
                && wndType == WINDOWTYPE.Normal && focussed)
            {
                moving = true;                
            } else if ((MouseManager.X > x && MouseManager.X < x + width && MouseManager.Y > y && MouseManager.Y < y + height && MouseManager.MouseState == MouseState.Left)
                && wndType == WINDOWTYPE.FullyDraggable && focussed)
            {
                moving = true;
            } else if ((MouseManager.X > x + width - 15 && MouseManager.X < x + width && MouseManager.Y > y && MouseManager.Y < y + 20 && MouseManager.MouseState == MouseState.Left)
                && wndType == WINDOWTYPE.Normal && focussed)
            {
                showed = false;
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

            drawControls();
        }
        
        public void sendKey(KeyEvent key)
        {
            currentKey = key;
            keyhandler();
        }

        public void drawControls()
        {
            drawing();
        }
    }
}
