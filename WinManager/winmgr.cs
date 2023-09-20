using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveOS.WinManager
{
    public class winmgr
    {
        public List<window> winList = new();
        public winmgr() {
            //init code
        }

        public void add(int x, int y, int w, int h)
        {
            var tempWin = new window();
            tempWin.x = x; tempWin.y = y;
            tempWin.width = w; tempWin.height = h;
            tempWin.run = true;
            winList.Add(tempWin);
        }

        public void update()
        {
            if (winList.Count < 0)
            {
                ImprovedVBE._DrawACSIIString("Win: " +winList.Count, 10,10,ImprovedVBE.colourToNumber(255,255,255));
                return;
            }
            foreach (var win in winList)
            {
                win.render();
            }
        }
    }
}
