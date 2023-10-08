using Cosmos.System;
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

        public Button(window parentWindow, int x, int y, int width, int height, string text, int colorR = 2555, int colorG = 2555, int colorB = 2555)
        {
            this.parent = parentWindow;
            this.x = x;
            this.y = y;
            this.width = width - height / 2;
            this.height = height;
            this.label = text;
            if (colorR > 255 || colorG > 255 || colorB > 255)
            {
                colorR = WaveConfigs.cTheme.btBg.r;
                colorG = WaveConfigs.cTheme.btBg.g;
                colorB = WaveConfigs.cTheme.btBg.b;
            }
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
            ImprovedVBE.DrawFilledCircle(ImprovedVBE.colourToNumber(tempR, tempG, tempB), (parent.x + x) + height / 2, (parent.y + y) + height / 2, height / 2);
            ImprovedVBE.DrawFilledCircle(ImprovedVBE.colourToNumber(tempR, tempG, tempB), (parent.x + x + width) + height / 2, (parent.y + y) + height / 2, height / 2);
            ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(tempR, tempG, tempB), (parent.x + x) + height / 2, parent.y + y, width, height);
            ImprovedVBE._DrawACSIIString(label, parent.x + x + 7, parent.y + y + 7, ImprovedVBE.colourToNumber(255,255,255));
            if (MouseManager.X > parent.x + x && MouseManager.X < parent.x + x + width + width / 2
                && MouseManager.Y > parent.y + y && MouseManager.Y < parent.y + y + height + height / 2
                && parent.focussed)
            {
                if (MouseManager.MouseState == MouseState.Left)
                {
                    MouseManager.MouseState = MouseState.None;
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
