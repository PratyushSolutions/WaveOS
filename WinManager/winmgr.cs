using Cosmos.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
        public static List<window> winList = new();
        public int currentFocussed = winList.Count - 1;
        public bool activeWindowDragging = false;
        public bool activeWindowOnTop = false;

        public winmgr() {
            //init code
        }

        public void add(int x, int y, int w, int h, string title, Action drawControls, WINDOWTYPE type = WINDOWTYPE.Normal)
        {
            var tempWin = new window(drawControls, title);
            tempWin.x = x; tempWin.y = y;
            tempWin.width = w; tempWin.height = h;
            tempWin.showed = true;
            tempWin.wndType = type;
            winList.Add(tempWin);
        }

        public void update()
        {
            if (winList.Count > 0)
            {
                if (!winList[winList.Count - 1].showed)
                {
                    winList.Remove(winList[winList.Count - 1]);
                    moveWindowToFront(winList[winList.Count - 1]);
                }
                currentFocussed = winList.Count - 1;
                winList[currentFocussed].focussed = true;
            }
            if (KeyboardManager.TryReadKey(out var key) && winList.Count > 0)
            {
                PassKeyToFocussedWindow(key);
            }

            /*
            if (winList.Count < 0)
            {
                ImprovedVBE._DrawACSIIString("Win: " + winList.Count, 10,10,ImprovedVBE.colourToNumber(255,255,255));
                return;
            }
            */

            if (winList.Count > 0) {
                foreach (var win in winList)
                {
                    win.render();
                }
                winList[currentFocussed].render();
            }
        }
        
        public void moveWindowToFront(window win)
        {
            if (!(winList.Count > 0)) { return; }
            winList[winList.Count - 1].focussed = false;
            if (winList.Contains(win))
            {
                winList.Remove(win);
            }
            winList.Add(win);
        }

        public bool checkBoundsWithFocussedWindow(window attemptWnd)
        {
            return activeWindowOnTop;
        }

        public void PassKeyToFocussedWindow(KeyEvent key)
        {
            if (!(winList.Count > 0)) { return; }
            winList[winList.Count - 1].sendKey(key);
        }
    }
}
