using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveOS.GraphicsWidgets;
using WaveOS.WinManager;

namespace WaveOS.Apps
{
    internal class HelpWindow
    {
        public window newWin;
        public HelpWindow()
        {
            newWin = new window(Controls, "About");
            newWin.x = WaveConfigs.defaultWindowPositionX;
            newWin.y = WaveConfigs.defaultWindowPositionY;
            newWin.width = 100;
            newWin.height = 80;
            newWin.wndType = WINDOWTYPE.Normal;
            newWin.showed = true;
            winmgr.winList.Add(newWin);
        }

        public void Controls()
        {
            Button btnOk = new(newWin, 10, 50, newWin.width - 20 - 10, 25, "Ok");
            Label lblInfo = new(newWin, 10, 15, WaveConfigs.osNameVersion, 200, 200, 200);

            lblInfo.draw();
            btnOk.draw(onClick_buttonOk);
        }

        public void onClick_buttonOk()
        {
            winmgr.winList.Remove(newWin);
            newWin.close();
            winmgr.winList.Add(newWin);
        }
    }
}
