﻿using System;
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
            newWin.width = 150;
            newWin.height = 100;
            newWin.wndType = WINDOWTYPE.Normal;
            newWin.showed = true;
            WaveConfigs.WindowMgr.winList.Add(newWin);
        }

        public void Controls()
        {
            Button btnOk = new(newWin, 10, 50, 135, 25, "Ok", 11,11,11);
            Label lblInfo = new(newWin, 10, 15, WaveConfigs.osNameVersion, 50, 50, 50);

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