using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveOS.SystemMenus
{
    internal class topmenu
    {
        public int menuHeight = 25;

        public void init()
        {
            //add something, idk
        }
        
        public void show()
        {
            ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(50, 50, 50), 0, 0, WaveConfigs.displayW - 1, menuHeight);
            ImprovedVBE.DrawImageAlpha(WaveConfigs.waveIcon, 0, 0);
            ImprovedVBE._DrawACSIIString("FPS: " + Kernel.FPS.ToString(), WaveConfigs.displayW - (("FPS: " + Kernel.FPS.ToString()).Length * 10) - 5, 5, ImprovedVBE.colourToNumber(255, 255, 255));
        }
    }
}
