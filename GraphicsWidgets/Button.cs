﻿using Cosmos.System;
using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveOS.WinManager;

namespace WaveOS.GraphicsWidgets
{
    internal class Button
    {
        public window parent;
        public int x;
        public int y;
        public int width;
        public int height;
        public string label;
        public int r;
        public int g;
        public int b;

        public Button(window parentWindow, int x, int y, int width, int height, string text, int colorR, int colorG, int colorB)
        {
            this.parent = parentWindow;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.label = text;
            this.r = colorR;
            this.g = colorG;
            this.b = colorB;
            tempR = colorR;
            tempG = colorG;
            tempB = colorB;
        }

        public int tempR;
        public int tempG;
        public int tempB;

        public void draw(Action onClicked)
        {
            if (parent.wndType == WINDOWTYPE.Normal)
            {
                y += 21;
            }
            ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(tempR, tempG, tempB), parent.x + x, parent.y + y, width, height);
            ImprovedVBE._DrawACSIIString(label, parent.x + x + 7, parent.y + y + 7, ImprovedVBE.colourToNumber(255,255,255));
            if (MouseManager.X > parent.x + x && MouseManager.X < parent.x + x + width
                && MouseManager.Y > parent.y + y && MouseManager.Y < parent.y + y + height
                && parent.focussed)
            {
                if (MouseManager.MouseState == MouseState.Left)
                {
                    onClicked();
                } else
                {
                    tempR = 10;
                    tempG = 10;
                    tempB = 10;
                }
            } else
            {
                tempR = r;
                tempG = g;
                tempB = b;
            }
        }
    }
}