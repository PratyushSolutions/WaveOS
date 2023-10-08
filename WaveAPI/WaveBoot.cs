using a = Cosmos.System;
using Cosmos.System.Graphics;
using IL2CPU.API.Attribs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using System.IO;

namespace WaveOS.WaveAPI
{
    internal class WaveBoot
    {
        [ManifestResourceStream(ResourceName = "WaveOS.Resources.waveBoot1.bmp")] public static byte[] s1;
        public Bitmap s1_b = new(s1);
        [ManifestResourceStream(ResourceName = "WaveOS.Resources.waveBoot2.bmp")] public static byte[] s2;
        public Bitmap s2_b = new(s2);
        public int currentOpt = 2;
        public int bootScreen()
        {
            if (File.Exists("0:\\BOOTTOGUI.signal"))
            {
                File.Delete("0:\\BOOTTOGUI.signal");
                return 2;
            }
            redraw:
            if (currentOpt == 1) { ImprovedVBE.DrawImageAlpha(s1_b, 1, 1); }
            else if (currentOpt == 2) { ImprovedVBE.DrawImageAlpha(s2_b, 1, 1); }
            ImprovedVBE.display(Kernel.display);
            Kernel.display.Display();
            var key = Console.ReadKey();
            if (key.Key == ConsoleKey.Escape) { a.Power.Shutdown(); }
            else if (key.Key == ConsoleKey.DownArrow && currentOpt == 1) { currentOpt = 2; goto redraw; }
            else if (key.Key == ConsoleKey.UpArrow && currentOpt == 2) { currentOpt = 1; goto redraw; }
            else if (!(key.Key == ConsoleKey.Enter)) { goto redraw; }
            ImprovedVBE.cover = ImprovedVBE.data;
            return currentOpt;
        }
    }
}
