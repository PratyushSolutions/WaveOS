﻿using Cosmos.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveOS.WinManager
{
    public enum WINDOWTYPE
    {
        Normal, //(This means that it draws a titlebar and can be only moved via a titlebar)
        FullyDraggable //(This means that it does not draw a titlebar and can be moved by dragging the entire window)
    }
    public class winmgr
    {
        public List<window> winList = new();
        public int currentFocussed = 0;
        public bool activeWindowDragging = false;

        public winmgr() {
            //init code
        }

        public void add(int x, int y, int w, int h, WINDOWTYPE type = WINDOWTYPE.Normal)
        {
            var tempWin = new window();
            tempWin.x = x; tempWin.y = y;
            tempWin.width = w; tempWin.height = h;
            tempWin.showed = true;
            tempWin.wndType = type;
            winList.Add(tempWin);
        }

        public void update()
        {
            currentFocussed = winList.Count - 1;
            winList[currentFocussed].focussed = true;
            if (winList.Count < 0)
            {
                ImprovedVBE._DrawACSIIString("Win: " + winList.Count, 10,10,ImprovedVBE.colourToNumber(255,255,255));
                return;
            }
            foreach (var win in winList)
            {
                win.render();
            }
            winList[currentFocussed].render();
        }
        
        public void moveWindowToFront(window win)
        {
            winList[winList.Count - 1].focussed = false;
            if (winList.Contains(win))
            {
                winList.Remove(win);
            }
            winList.Add(win);
        }
    }
}
