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
        public bool msgBox = false;

        public int bR = 30;
        public int bG = 31;
        public int bB = 35;

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
            if (focussed &&
                MouseManager.X > x && MouseManager.X < x + width &&
                MouseManager.Y > y && MouseManager.Y < y + height)
            {
                WaveConfigs.WindowMgr.activeWindowOnTop = true;
            }
            else { WaveConfigs.WindowMgr.activeWindowOnTop = false; }
            if (msgBox)
            {
                bR = 150; bG = 150; bB = 150;
            }
            else
            {
                bR = 30; bG = 31; bB = 35;
            }

            if (showed)
                update();

            return;
        }

        public void close()
        {
            showed = false;
        }

        public int dragX;
        public int dragY;

        public void update()
        {
            ImprovedVBE.DrawFilledCircle(ImprovedVBE.colourToNumber(bR, bG, bB), x + 7, y + 9, 9);
            ImprovedVBE.DrawFilledCircle(ImprovedVBE.colourToNumber(bR, bG, bB), x + width - 8, y + 9, 9);

            ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(bR, bG, bB), x - 2, y - 2 + 10, width + 2 + 2, height + 2 + 2 + 10);

            ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(bR, bG, bB), x + 7, y + height + 20, width - 10, 12);
            ImprovedVBE.DrawFilledCircle(ImprovedVBE.colourToNumber(bR, bG, bB), x + 7, y + height + 14 + 9, 9);
            ImprovedVBE.DrawFilledCircle(ImprovedVBE.colourToNumber(bR, bG, bB), x + width - 8, y + height + 14 + 9, 9);

            if ((MouseManager.X > x && MouseManager.X < x + width && MouseManager.Y > y && MouseManager.Y < y + height && MouseManager.MouseState == MouseState.Left)
                && !focussed && !WaveConfigs.WindowMgr.activeWindowDragging && !moving && !WaveConfigs.WindowMgr.checkBoundsWithFocussedWindow(this))
            {
                WaveConfigs.WindowMgr.moveWindowToFront(this);
            }

            if ((MouseManager.X > x && MouseManager.X < x + 20 && MouseManager.Y > y && MouseManager.Y < y + 10 && MouseManager.MouseState == MouseState.Left)
                && wndType == WINDOWTYPE.Normal)
            {
                showed = false;
            }

            if (focussed)
            {
                //ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(163, 194, 255), x + 7, y + height + 20 + 5, width - 10, 9);
                //ImprovedVBE.DrawFilledCircle(ImprovedVBE.colourToNumber(163, 194, 255), x + 7, y + height + 14 + 9, 9);
                //ImprovedVBE.DrawFilledCircle(ImprovedVBE.colourToNumber(163, 194, 255), x + width - 8, y + height + 14 + 9, 9);

                ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(bR, bG, bB), x + 7, y + height + 20 + 5, width - 10, 9);
                ImprovedVBE.DrawFilledCircle(ImprovedVBE.colourToNumber(bR, bG, bB), x + 7, y + height + 14 + 9, 9);
                ImprovedVBE.DrawFilledCircle(ImprovedVBE.colourToNumber(bR, bG, bB), x + width - 8, y + height + 14 + 9, 9);

                ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(bR, bG, bB), x - 2, y - 2 + 10, width + 2 + 2, height + 2 + 2 + 10);
                //ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(163, 194, 255), x, y + 14, width, height + 14);
            } else
            {
                //ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(77, 77, 77), x + 7, y + height + 20 + 5, width - 10, 9);
                //ImprovedVBE.DrawFilledCircle(ImprovedVBE.colourToNumber(77, 77, 77), x + 7, y + height + 14 + 9, 9);
                //ImprovedVBE.DrawFilledCircle(ImprovedVBE.colourToNumber(77, 77, 77), x + width - 8, y + height + 14 + 9, 9);

                ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(bR, bG, bB), x + 7, y + height + 20 + 5, width - 10, 9);
                ImprovedVBE.DrawFilledCircle(ImprovedVBE.colourToNumber(bR, bG, bB), x + 7, y + height + 14 + 9, 9);
                ImprovedVBE.DrawFilledCircle(ImprovedVBE.colourToNumber(bR, bG, bB), x + width - 8, y + height + 14 + 9, 9);
                ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(bR, bG, bB), x - 2, y - 2 + 10, width + 2 + 2, height + 2 + 2 + 10);
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
                WaveConfigs.WindowMgr.moveWindowToFront(this);
                if ((int)MouseManager.X - dragX + width < WaveConfigs.displayW
                    && (int)MouseManager.Y - dragY + height < WaveConfigs.displayH &&
                    (int)MouseManager.X - dragX > 0
                    && (int)MouseManager.Y - dragY > 0)
                {
                    x = (int)MouseManager.X - dragX;
                    y = (int)MouseManager.Y - dragY;
                }
            }
            if (MouseManager.MouseState == MouseState.None && moving)
            {
                moving = false;
                WaveConfigs.WindowMgr.activeWindowDragging = false;
                dragX = 0; dragY = 0;
            }
            if ((MouseManager.X > x + 10 && MouseManager.X < x + width && MouseManager.Y > y && MouseManager.Y < y + 20 && MouseManager.MouseState == MouseState.Left)
                && wndType == WINDOWTYPE.Normal && focussed && !WaveConfigs.WindowMgr.activeWindowDragging)
            {
                if (dragX == 0 && dragY == 0)
                    dragX = (int)MouseManager.X - x; dragY = (int)MouseManager.Y - y;
                moving = true;
            } else if ((MouseManager.X > x && MouseManager.X < x + width && MouseManager.Y > y && MouseManager.Y < y + height && MouseManager.MouseState == MouseState.Left)
                && wndType == WINDOWTYPE.FullyDraggable && focussed && !WaveConfigs.WindowMgr.activeWindowDragging)
            {
                if (dragX == 0 && dragY == 0)
                    dragX = (int)MouseManager.X - x; dragY = (int)MouseManager.Y - y;
                moving = true;
            }

            if (MouseManager.X > x && MouseManager.X < x + width && MouseManager.Y > y && MouseManager.Y < y + 20 && MouseManager.MouseState == MouseState.Left
                && wndType == WINDOWTYPE.Normal && !focussed && !WaveConfigs.WindowMgr.activeWindowDragging &&
                !WaveConfigs.WindowMgr.checkBoundsWithFocussedWindow(this))
            {
                WaveConfigs.WindowMgr.moveWindowToFront(this);
            } else if (MouseManager.X > x && MouseManager.X < x + width && MouseManager.Y > y && MouseManager.Y < y + height && MouseManager.MouseState == MouseState.Left
                && wndType == WINDOWTYPE.FullyDraggable && !focussed && !WaveConfigs.WindowMgr.activeWindowDragging &&
                !WaveConfigs.WindowMgr.checkBoundsWithFocussedWindow(this))
            {
                WaveConfigs.WindowMgr.moveWindowToFront(this);
            }

            drawControls();
        }
        
        public void sendKey(KeyEvent key)
        {
            currentKey = key;
            if (keyhandler != null)
                keyhandler();
        }

        public void drawControls()
        {
            drawing();
        }
    }
}
