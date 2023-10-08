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
using WaveOS.Wave_zSH;

namespace WaveOS.Apps
{
    internal class WaveTerm
    {
        public window TermWindow;
        [ManifestResourceStream(ResourceName = "WaveOS.Resources.WaveTerm.bmp")] public static byte[] waveTermIcon;
        public static Bitmap waveTermLogo = new(waveTermIcon);
        public static string WaveBuffer = "# ";
        public static string currentCommand = "";

        public WaveTerm()
        {
            TermWindow = new(Controls, "WaveTerm", KeyHandler, waveTermLogo);
            TermWindow.x = WaveConfigs.defaultWindowPositionX - 490 / 2;
            TermWindow.y = WaveConfigs.defaultWindowPositionY - 342 / 2;
            TermWindow.width = 490;
            TermWindow.height = 342;
            TermWindow.wndType = WINDOWTYPE.Normal;
            TermWindow.showed = true;
            winmgr.winList.Add(TermWindow);
        }

        public void Controls()
        {
            Label sample = new(TermWindow, 1, 1, WaveBuffer, 255, 255, 255);

            sample.draw();
        }

        public void KeyHandler()
        {
            if (winmgr.winList[winmgr.winList.Count - 1].currentKey.Key != Cosmos.System.ConsoleKeyEx.Backspace ||
                winmgr.winList[winmgr.winList.Count - 1].currentKey.Key != Cosmos.System.ConsoleKeyEx.Enter)
            {
                currentCommand += winmgr.winList[winmgr.winList.Count - 1].currentKey.KeyChar;
                WaveBuffer += winmgr.winList[winmgr.winList.Count - 1].currentKey.KeyChar;
            } else
            {
                switch (winmgr.winList[winmgr.winList.Count - 1].currentKey.Key)
                {
                    case Cosmos.System.ConsoleKeyEx.Enter:
                        WaveBuffer = WSH.handleWSHCommand(currentCommand, WaveBuffer);
                        currentCommand = "";
                        ImprovedVBE._DrawACSIIString(WaveBuffer, 50, 50, ImprovedVBE.colourToNumber(255, 255, 255));
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
