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

        [ManifestResourceStream(ResourceName = "WaveOS.Resources.WaveOS_HandCursor.bmp")] public static byte[] rawHandCursor;
        public static Bitmap waveHandCursor = new Bitmap(WaveConfigs.rawHandCursor);
        [ManifestResourceStream(ResourceName = "WaveOS.Resources.WaveOS_background.bmp")] public static byte[] rawWaveBg;
        public static Bitmap waveBg = new Bitmap(WaveConfigs.rawWaveBg);
        [ManifestResourceStream(ResourceName = "WaveOS.Resources.WaveOS_Cursor.bmp")] public static byte[] rawWaveCursor;
        public static Bitmap waveCursor = new Bitmap(rawWaveCursor);
        [ManifestResourceStream(ResourceName = "WaveOS.Resources.WaveOS_background_720.bmp")] public static byte[] rawWaveBg_720;
        public static Bitmap waveBg_720 = new Bitmap(WaveConfigs.rawWaveBg_720);
        [ManifestResourceStream(ResourceName = "WaveOS.Resources.WaveOS_background_768.bmp")] public static byte[] rawWaveBg_768;
        public static Bitmap waveBg_768 = new Bitmap(WaveConfigs.rawWaveBg_768);
        [ManifestResourceStream(ResourceName = "WaveOS.Resources.WaveOS_icon.bmp")] public static byte[] rawWaveIcon;
        public static Bitmap waveIcon = new Bitmap(WaveConfigs.rawWaveIcon);

        [ManifestResourceStream(ResourceName = "WaveOS.Resources.WaveOS_TopBar.bmp")] public static byte[] rawWaveTopBar;
        public static Bitmap waveTopBar = new Bitmap(WaveConfigs.rawWaveTopBar);

        public static Bitmap currentCursor = waveCursor;

        public const int displayW = 1024;
        public const int displayH = 768;
        public static ThemeConfiguration cTheme;
        public static ThemeConfiguration darkMode;
        public static ThemeConfiguration lightMode;

        public static LogonThemeConfig logonThemeConfig;
        public static LogonThemeConfig logTheme_Light;
        public static LogonThemeConfig logTheme_Dark;
        public static int timer = 0;

        public const int defaultWindowPositionX = displayW / 2;
        public const int defaultWindowPositionY = displayH / 2;
        public const string osName = "WaveOS";
        public const string osVersion = "v0.1";
        public const string osNameVersion = osName + " " + osVersion;

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

    public struct ThColor
    {
        public ThColor(int single)
        {
            r = single; g = single; b = single;
        }

        public ThColor(int r, int g, int b)
        {
            this.r = r; this.g = g; this.b = b;
        }

        public int r;
        public int g;
        public int b;
    }

    public struct ThemeConfiguration
    {
        public ThemeConfiguration(ThColor windowFore , ThColor windowBody, ThColor msgBoxColor   ,
                                  ThColor buttonBack , ThColor buttonFore,
                                  ThColor labelFore  ,
                                  ThColor gMenuBack  , ThColor gMenuFore , ThColor gMenu_ItemBack, ThColor gMenu_ItemFore)
        {
            winT          = windowFore     ;
            winBg         = windowBody     ;
            mBColor       = msgBoxColor    ;
            
            btBg          = buttonBack     ;
            btFo          = buttonFore     ;

            lbFo          = labelFore      ;

            gMBg          = gMenuBack      ;
            gMFo          = gMenuFore      ;
            gMI_Bg        = gMenu_ItemBack ;
            gMI_Fo        = gMenu_ItemFore ;
        }

        public ThColor winT , winBg, mBColor         ;
        public ThColor btBg , btFo                   ;
        public ThColor lbFo                          ;
        public ThColor gMBg , gMFo , gMI_Bg , gMI_Fo ;
    }

    public struct LogonThemeConfig
    {
        public LogonThemeConfig(ThColor logonButtonBackground,
                                ThColor logonButtonForeground,
                                
                                ThColor logonBackgroundColor)
        {
            lBtnBg = logonButtonBackground;
            lBtnFo = logonButtonForeground;
            lBgCo  = logonBackgroundColor ;
        }

        public ThColor lBtnBg;
        public ThColor lBtnFo;

        public ThColor lBgCo;
    }
}
