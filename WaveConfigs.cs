using Cosmos.System.FileSystem;
using Cosmos.System.Graphics;
using IL2CPU.API.Attribs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveOS.SystemMenus;
using WaveOS.WinManager;

namespace WaveOS
{
    internal class WaveConfigs
    {

        public static CosmosVFS WaFs = new();
        public static Dictionary<string, Processes> proc = new();

        //public static Canvas display;

        [ManifestResourceStream(ResourceName = "WaveOS.Resources.WaveOS_background.bmp")] public static byte[] rawWaveBg;
        public static Bitmap waveBg = new Bitmap(WaveConfigs.rawWaveBg);
        [ManifestResourceStream(ResourceName = "WaveOS.Resources.WaveOS_Cursor.bmp")] public static byte[] rawWaveCursor;
        public static Bitmap waveCursor = new Bitmap(rawWaveCursor);
        [ManifestResourceStream(ResourceName = "WaveOS.Resources.WaveOS_background_720.bmp")] public static byte[] rawWaveBg_720;
        public static Bitmap waveBg_720 = new Bitmap(WaveConfigs.rawWaveBg_720);
        [ManifestResourceStream(ResourceName = "WaveOS.Resources.WaveOS_icon.bmp")] public static byte[] rawWaveIcon;
        public static Bitmap waveIcon = new Bitmap(WaveConfigs.rawWaveIcon);

        public const int displayW = 1280;
        public const int displayH = 720;
        public const int defaultWindowPositionX = displayW / 2;
        public const int defaultWindowPositionY = displayH / 2;

        public static winmgr WindowMgr;
        public static topmenu UpperMenu;

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
