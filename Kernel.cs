﻿using Cosmos.System.FileSystem.VFS;
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

namespace WaveOS
{
    public class Kernel : Sys.Kernel
    {
        public VBECanvas display = new(new Mode(WaveConfigs.displayW, WaveConfigs.displayH, ColorDepth.ColorDepth32));
        public static string currentSignal = "NONE";
        protected override void BeforeRun()
        {
            VFSManager.RegisterVFS(WaveConfigs.WaFs);
            WaveSystemInit waveInit = new WaveSystemInit();
            waveInit.Start();
            
            //WaveConfigs.display = FullScreenCanvas.GetFullScreenCanvas(new Mode(640, 480, ColorDepth.ColorDepth32));
            Cosmos.System.MouseManager.ScreenWidth = WaveConfigs.displayW;
            Cosmos.System.MouseManager.ScreenHeight = WaveConfigs.displayH;
            MouseManager.X = WaveConfigs.displayW / 2;
            MouseManager.Y = WaveConfigs.displayH / 2;
            WaveConfigs.WindowMgr = new();

            HelpWindow newHelp = new();

            WaveConfigs.UpperMenu = new();
            WaveConfigs.UpperMenu.init();
        }
        private int frameCounter = 0;
        
        /// <summary>
        /// Modify this to redraw Background on next frame.
        /// </summary>
        public bool updateBg = true;

        private int prevX = 0;
        private int prevY = 0;

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
                }
                LastS = DateTime.UtcNow.Second;
                Ticken = 0;
            }
            Ticken++;
        }

        protected override void Run()
        {
            Update();
            if (FPS < 15)
            {
                Heap.Collect();
            }
            //winmgr
            WaveConfigs.WindowMgr.update();

            //menu show
            WaveConfigs.UpperMenu.show(); //top menu with fps counter

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

            //random useless bullshit
            frameCounter++;
            if (frameCounter == 40)
            {
                frameCounter = 0;
            }

            //rendering
            ImprovedVBE.display(display);
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
            }
            if (currentSignal == "WAIT5-REBOOT")
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
            display.Display();
            Heap.Collect();
        }

        public static void sendSignalToKernel(string signal) => currentSignal = signal;
    }
}
