using Cosmos.System.FileSystem;
using Cosmos.System.Graphics;
using IL2CPU.API.Attribs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveOS
{
    internal static class WaveConfigs
    {

        public static CosmosVFS WaFs = new();
        public static Dictionary<string, Processes> proc = new();

        public static Canvas display;
        [ManifestResourceStream(ResourceName = "WaveOS.Resources.WaveOS_background.bmp")] public static byte[] rawWaveBg;
        public static Image waveBg = new Bitmap(WaveConfigs.rawWaveBg);
        [ManifestResourceStream(ResourceName = "WaveOS.Resources.WaveOS_Cursor.bmp")] public static byte[] rawWaveCursor;
        public static Image waveCursor = new Bitmap(rawWaveCursor);

        public class Processes
        {
            public int procID;
            public bool running;
            public void kill()
            {
                running = false;
                if (procID == 0) { Cosmos.System.Power.Shutdown(); }
            }
        }
    }
}
