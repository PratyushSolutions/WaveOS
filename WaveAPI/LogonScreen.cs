using Cosmos.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveOS.WaveAPI
{
    public static class LogonScreen
    {
        private static bool _logged = false;
        private static string unlock = "Unlock >>";
        public static bool showScreen()
        {
            // background
            ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(16, 50, 173), 0, 0, WaveConfigs.displayW - 1, WaveConfigs.displayH - 1);

            // time
            ImprovedVBE._DrawACSIIString(DateTime.Now.ToString().Split(' ')[1], (WaveConfigs.displayW / 2) - (DateTime.Now.ToString().Split(' ')[1].Length * 6), 50, ImprovedVBE.colourToNumber(255, 255, 255));

            // 'unlock' button
            ImprovedVBE.DrawFilledCircle(ImprovedVBE.colourToNumber(33, 33, 33), WaveConfigs.displayW / 2 - (150 / 2), WaveConfigs.displayH / 2 - (50 / 2) + 24, 24);
            ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(33, 33, 33), WaveConfigs.displayW / 2 - (150 / 2), WaveConfigs.displayH / 2 - (50 / 2), 150, 50);
            ImprovedVBE.DrawFilledCircle(ImprovedVBE.colourToNumber(33, 33, 33), WaveConfigs.displayW / 2 + 74, WaveConfigs.displayH / 2 - (50 / 2) + 24, 24);

            ImprovedVBE._DrawACSIIString(unlock, WaveConfigs.displayW / 2 - (150 / 2) + 30, WaveConfigs.displayH / 2 - (50 / 2 / 2) + 5, ImprovedVBE.colourToNumber(255,255,255));


            // logics
            if (MouseManager.X > WaveConfigs.displayW / 2 - (150 / 2) - 24 && MouseManager.X < WaveConfigs.displayW / 2 - (150 / 2) + 150 + 24
                && MouseManager.Y > WaveConfigs.displayH / 2 - (50 / 2) && MouseManager.Y < WaveConfigs.displayH / 2 - (50 / 2) + 50)
            {
                if (MouseManager.MouseState == MouseState.Left)
                {
                    MouseManager.MouseState = MouseState.None;
                    _logged = true;
                }
                unlock = " Unlock  >>";
            } else
            {
                unlock = "Unlock >>";
            }
            return _logged;
        }
    }
}
