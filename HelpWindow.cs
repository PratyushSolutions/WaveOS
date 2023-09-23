using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveOS.GraphicsWidgets;
using WaveOS.WinManager;

namespace WaveOS
{
    internal class HelpWindow
    {
        public window newWin;
        public HelpWindow()
        {
            newWin = new window(Controls, "Help");
            newWin.x = WaveConfigs.defaultWindowPositionX - 150;
            newWin.y = WaveConfigs.defaultWindowPositionY - 100;
            newWin.width = 150;
            newWin.height = 100;
            newWin.wndType = WINDOWTYPE.Normal;
            newWin.showed = true;
            WaveConfigs.WindowMgr.winList.Add(newWin);
        }

        public void Controls()
        {
            Button btnOk = new(newWin, 10, 45, 90, 30, "Ok", 11,11,11);
            Label lblInfo = new(newWin, 10, 15, "WaveOS v0.1", 50, 50, 50);

            lblInfo.draw();
            btnOk.draw(onClick_buttonOk);
        }

        public void onClick_buttonOk()
        {
            WaveConfigs.WindowMgr.winList.Remove(newWin);
            newWin.showed = false;
            WaveConfigs.WindowMgr.winList.Add(newWin);
        }
    }
}
