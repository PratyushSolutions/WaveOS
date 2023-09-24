using Cosmos.System.Graphics;
using IL2CPU.API.Attribs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WaveOS.GraphicsWidgets;
using WaveOS.WinManager;

namespace WaveOS.Apps
{
    internal class WaveTerm
    {
        public window TermWindow;
        [ManifestResourceStream(ResourceName = "WaveOS.Resources.WaveTerm.bmp")] public static byte[] waveTermIcon;
        public static Bitmap waveTermLogo = new(waveTermIcon);

        public WaveTerm()
        {
            TermWindow = new(Controls, "WaveTerm", KeyHandler, waveTermLogo);
            TermWindow.x = WaveConfigs.defaultWindowPositionX - 490 / 2;
            TermWindow.y = WaveConfigs.defaultWindowPositionY - 342 / 2;
            TermWindow.width = 490;
            TermWindow.height = 342;
            TermWindow.wndType = WINDOWTYPE.Normal;
            TermWindow.showed = true;
            WaveConfigs.WindowMgr.winList.Add(TermWindow);
        }

        public void Controls()
        {
            Label sample = new(TermWindow, 10, 10, "Hey!", 255, 255, 255);

            sample.draw();
        }

        public void KeyHandler()
        {

        }
    }
}
