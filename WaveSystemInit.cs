using Cosmos.System.Graphics;
using IL2CPU.API.Attribs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WaveOS
{
    internal class WaveSystemInit
    {
        [ManifestResourceStream(ResourceName = "WaveOS.Resources.WaveOS.bmp")] public static byte[] waveLogo;
        public void Start()
        {
            WaveConfigs.Processes waveInit = new();
            waveInit.procID = 0;
            waveInit.running = true;
            WaveConfigs.proc.Add("WaveSystem", waveInit);


            //Canvas boot = FullScreenCanvas.GetFullScreenCanvas(new Mode(640, 480, ColorDepth.ColorDepth32));

            /*VBECanvas boot = new();
            boot.Clear();
            boot.Clear(Color.Black);
            boot.DrawImageAlpha(new Bitmap(waveLogo), 640 / 4, 480 / 2);
            boot.Display();
            Thread.Sleep(3000);
            boot.Disable();:*/
        }
    }
}
