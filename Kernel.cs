 using Cosmos.System.FileSystem.VFS;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.Core.Memory;
using WaveOS.WinManager;
using System;
using Cosmos.System;
using System.Threading;
using WaveOS.Apps;
using WaveOS.WaveAPI;
using Cosmos.HAL.Drivers.Video.SVGAII;

namespace WaveOS
{
    public class Kernel : Sys.Kernel
    {
        public static SVGAIICanvas display = new(new Mode(WaveConfigs.displayW, WaveConfigs.displayH, ColorDepth.ColorDepth32));
        //public VBECanvas display = new(new Mode(WaveConfigs.displayW, WaveConfigs.displayH, ColorDepth.ColorDepth32));
        public static Canvas displayM = FullScreenCanvas.GetFullScreenCanvas(new Mode(WaveConfigs.displayW, WaveConfigs.displayH, ColorDepth.ColorDepth32));
        public static string currentSignal = "NONE";
        public static int useCanvas = 1;
        protected override void BeforeRun()
        {
            if (VMTools.IsQEMU || VMTools.IsVMWare || VMTools.IsVirtualBox)
            {
                useCanvas = 2;
            }
            try
            {
                if (WaveConfigs.WaFs.Disks.Count > 0)
                {
                    VFSManager.RegisterVFS(WaveConfigs.WaFs);
                }
            }
            catch (Exception)
            {
                //ImprovedVBE._DrawACSIIString("Didn't make FS!");
            }
            WaveSystemInit waveInit = new WaveSystemInit();
            waveInit.Start();
            
            //WaveConfigs.display = FullScreenCanvas.GetFullScreenCanvas(new Mode(640, 480, ColorDepth.ColorDepth32));
            Cosmos.System.MouseManager.ScreenWidth = WaveConfigs.displayW;
            Cosmos.System.MouseManager.ScreenHeight = WaveConfigs.displayH;
            MouseManager.X = WaveConfigs.displayW / 2;
            MouseManager.Y = WaveConfigs.displayH / 2;
            WaveConfigs.WindowMgr = new();

            //HelpWindow newHelp = new();
            WaveTerm term = new();

            WaveConfigs.UpperMenu = new();
            WaveConfigs.UpperMenu.init();
        }
        private int frameCounter = 0;
        
        /// <summary>
        /// Modify this to redraw Background on next frame.
        /// </summary>
        public bool updateBg = true;
        
        //fps shit
        public static int FPS = 0;

        public static int LastS = -1;
        public static int Ticken = 0;

        public static void Update()
        {
            if (LastS == -1)
            {
                LastS = DateTime.UtcNow.Second;
            }
            if (DateTime.UtcNow.Second - LastS != 0)
            {
                if (DateTime.UtcNow.Second > LastS)
                {
                    FPS = Ticken / (DateTime.UtcNow.Second - LastS);
                    WaveConfigs.timer++;
                }
                LastS = DateTime.UtcNow.Second;
                Ticken = 0;
            }
            Ticken++;
        }

        protected override void Run()
        {
            Update();
            //winmgr
            WaveConfigs.WindowMgr.update();

            //menu show
            WaveConfigs.UpperMenu.show(); //top menu with fps counter

            //waveapis
            /*Message.WaveMessage msg = new();
            msg.message = "Hello World!";
            msg.id = 1;
            Message.NewShow(new());
            Message.DrawCurrentMessage();*/

            //cursor
            if (Sys.MouseManager.X < 0)
            {
                Sys.MouseManager.X = 0;
            } else if (Sys.MouseManager.X + WaveConfigs.waveCursor.Width > WaveConfigs.displayW)
            {
                Sys.MouseManager.X = WaveConfigs.displayW - WaveConfigs.waveCursor.Width;
            }
            if (Sys.MouseManager.Y < 0)
            {
                Sys.MouseManager.Y = 0;
            }
            else if (Sys.MouseManager.Y + WaveConfigs.waveCursor.Height > WaveConfigs.displayH)
            {
                Sys.MouseManager.Y = WaveConfigs.displayH - WaveConfigs.waveCursor.Height;
            }
            ImprovedVBE.DrawImageAlpha(WaveConfigs.waveCursor, (int)Sys.MouseManager.X, (int)Sys.MouseManager.Y);
            //prevX = (int)Sys.MouseManager.X;
            //prevY = (int)Sys.MouseManager.Y;

            //rendering
            if (useCanvas == 1)
            {
                ImprovedVBE.display(display);
                display.Display();
            } else
            {
                ImprovedVBE.display(displayM);
                displayM.Display();
            }
            Heap.Collect();
            if (currentSignal == "WAIT5-CLOSE")
            {
                ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(10, 10, 10), 0, 0, WaveConfigs.displayW - 1, WaveConfigs.displayH - 1);
                ImprovedVBE._DrawACSIIString("Shutting down!", 5, 5, ImprovedVBE.colourToNumber(201, 28, 28));
                ImprovedVBE.display(display);
                display.Display();
                Thread.Sleep(1000);
                ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(10, 10, 10), 0, 0, WaveConfigs.displayW - 1, WaveConfigs.displayH - 1);
                ImprovedVBE._DrawACSIIString("Shutting down!", 5, 5, ImprovedVBE.colourToNumber(201, 28, 28));

                ImprovedVBE._DrawACSIIString("3..", 5, 20, ImprovedVBE.colourToNumber(255, 255, 255));
                ImprovedVBE.display(display);
                display.Display();
                Thread.Sleep(1000);
                ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(10, 10, 10), 0, 0, WaveConfigs.displayW - 1, WaveConfigs.displayH - 1);
                ImprovedVBE._DrawACSIIString("Shutting down!", 5, 5, ImprovedVBE.colourToNumber(201, 28, 28));

                ImprovedVBE._DrawACSIIString("3..2..", 5, 20, ImprovedVBE.colourToNumber(255, 255, 255));
                ImprovedVBE.display(display);
                display.Display();
                Thread.Sleep(1000);
                ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(10, 10, 10), 0, 0, WaveConfigs.displayW - 1, WaveConfigs.displayH - 1);
                ImprovedVBE._DrawACSIIString("Shutting down!", 5, 5, ImprovedVBE.colourToNumber(201, 28, 28));

                ImprovedVBE._DrawACSIIString("3..2..1..", 5, 20, ImprovedVBE.colourToNumber(255, 255, 255));
                ImprovedVBE.display(display);
                display.Display();
                Thread.Sleep(1000);
                ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(10, 10, 10), 0, 0, WaveConfigs.displayW - 1, WaveConfigs.displayH - 1);
                ImprovedVBE._DrawACSIIString("Shutting down!", 5, 5, ImprovedVBE.colourToNumber(201, 28, 28));

                ImprovedVBE._DrawACSIIString("3..2..1..0", 5, 20, ImprovedVBE.colourToNumber(255, 255, 255));
                ImprovedVBE.display(display);
                display.Display();
                Thread.Sleep(1000);
                Power.Shutdown();
            } else if (currentSignal == "WAIT5-REBOOT")
            {
                ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(10, 10, 10), 0, 0, WaveConfigs.displayW - 1, WaveConfigs.displayH - 1);
                ImprovedVBE._DrawACSIIString("Rebooting the wave!", 5, 5, ImprovedVBE.colourToNumber(201, 28, 28));
                ImprovedVBE.display(display);
                display.Display();
                Thread.Sleep(1000);
                ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(10, 10, 10), 0, 0, WaveConfigs.displayW - 1, WaveConfigs.displayH - 1);
                ImprovedVBE._DrawACSIIString("Rebooting the wave!", 5, 5, ImprovedVBE.colourToNumber(201, 28, 28));

                ImprovedVBE._DrawACSIIString("3..", 5, 20, ImprovedVBE.colourToNumber(255, 255, 255));
                ImprovedVBE.display(display);
                display.Display();
                Thread.Sleep(1000);
                ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(10, 10, 10), 0, 0, WaveConfigs.displayW - 1, WaveConfigs.displayH - 1);
                ImprovedVBE._DrawACSIIString("Rebooting the wave!", 5, 5, ImprovedVBE.colourToNumber(201, 28, 28));

                ImprovedVBE._DrawACSIIString("3..2..", 5, 20, ImprovedVBE.colourToNumber(255, 255, 255));
                ImprovedVBE.display(display);
                display.Display();
                Thread.Sleep(1000);
                ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(10, 10, 10), 0, 0, WaveConfigs.displayW - 1, WaveConfigs.displayH - 1);
                ImprovedVBE._DrawACSIIString("Rebooting the wave!", 5, 5, ImprovedVBE.colourToNumber(201, 28, 28));

                ImprovedVBE._DrawACSIIString("3..2..1..", 5, 20, ImprovedVBE.colourToNumber(255, 255, 255));
                ImprovedVBE.display(display);
                display.Display();
                Thread.Sleep(1000);
                ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(10, 10, 10), 0, 0, WaveConfigs.displayW - 1, WaveConfigs.displayH - 1);
                ImprovedVBE._DrawACSIIString("Rebooting the wave!", 5, 5, ImprovedVBE.colourToNumber(201, 28, 28));

                ImprovedVBE._DrawACSIIString("3..2..1..0", 5, 20, ImprovedVBE.colourToNumber(255, 255, 255));
                ImprovedVBE.display(display);
                display.Display();
                Thread.Sleep(1000);
                Power.Reboot();
            }
        }

        public static void sendSignalToKernel(string signal) => currentSignal = signal;
    }
}
