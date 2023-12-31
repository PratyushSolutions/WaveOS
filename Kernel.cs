﻿ using Cosmos.System.FileSystem.VFS;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.Core.Memory;
using WaveOS.WinManager;
using s = System;
using Cosmos.System;
using System.Threading;
using WaveOS.Apps;
using WaveOS.WaveAPI;
using Cosmos.HAL.Drivers.Video.SVGAII;
using System.Collections;
using WaveOS.SystemMenus;
using System.Diagnostics;
using System;
using System.IO;

namespace WaveOS
{
    public class Kernel : Sys.Kernel
    {
        public static SVGAIICanvas display = new(new Mode(WaveConfigs.displayW, WaveConfigs.displayH, ColorDepth.ColorDepth32));
        //public static VGACanvas displayM = new(new Mode(WaveConfigs.displayW, WaveConfigs.displayH, ColorDepth.ColorDepth32));
        public static Canvas displayM = FullScreenCanvas.GetFullScreenCanvas(new Mode(WaveConfigs.displayW, WaveConfigs.displayH, ColorDepth.ColorDepth32));
        public static string currentSignal = "NONE";
        public static int useCanvas = 1;
        public bool loggedIn = false;
        public const bool debugMode = true;
        protected override void BeforeRun()
        {
            if (VMTools.IsVMWare || VMTools.IsVirtualBox)
            {
                useCanvas = 2;
            }

            try
            {
                //if (WaveConfigs.WaFs.Disks.Count > 0)
                //{
                    VFSManager.RegisterVFS(WaveConfigs.WaFs);
                //}
            }
            catch (s.Exception)
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

            WaveConfigs.logTheme_Dark = new(new(33, 33, 33), new(255, 255, 255), new(16, 50, 173));
            WaveConfigs.logTheme_Light = new();
            WaveConfigs.darkMode = new(new(255, 255, 255), new(30, 31, 35), new(150, 150, 150),
                                                        new(21, 101, 238), new(255, 255, 255),
                                                        new(255),
                                                        new(52, 51, 56), new(255), new(80), new(255));

            WaveConfigs.cTheme = WaveConfigs.darkMode;
            WaveConfigs.logonThemeConfig = WaveConfigs.logTheme_Dark;


            if (!debugMode)
            {
                WaveBoot newBooter = new();
                useMode = newBooter.bootScreen();
            } else { useMode = 2; }

        login:
            if (!loggedIn && useMode == 2 && !debugMode)
            {
                loggedIn = LogonScreen.showScreen();
                if (Sys.MouseManager.X < 0)
                {
                    Sys.MouseManager.X = 0;
                }
                else if (Sys.MouseManager.X + WaveConfigs.currentCursor.Width > WaveConfigs.displayW)
                {
                    Sys.MouseManager.X = WaveConfigs.displayW - WaveConfigs.currentCursor.Width;
                }
                if (Sys.MouseManager.Y < 0)
                {
                    Sys.MouseManager.Y = 0;
                }
                else if (Sys.MouseManager.Y + WaveConfigs.currentCursor.Height > WaveConfigs.displayH)
                {
                    Sys.MouseManager.Y = WaveConfigs.displayH - WaveConfigs.currentCursor.Height;
                }
                ImprovedVBE.DrawImageAlpha(WaveConfigs.currentCursor, (int)Sys.MouseManager.X, (int)Sys.MouseManager.Y);
                if (useCanvas == 1)
                {
                    ImprovedVBE.display(displayM);
                    displayM.Display();
                }
                else
                {
                    ImprovedVBE.display(display);
                    display.Display();
                }
                Heap.Collect();
                goto login;
            }
            if (useMode == 2)
            {
                window backHold = new(new(nothing), "SYS");
                backHold.x = 0; backHold.y = 0;
                backHold.width = topmenu.menuHeight / 2; backHold.height = topmenu.menuHeight / 2;
                backHold.showed = true;
                backHold.hidden = true;
                backHold.wndType = WINDOWTYPE.FullyDraggable;
                winmgr.winList.Add(backHold);
            }
            else
            {
                display.Disable();
                displayM.Disable();
                s.Console.BackgroundColor = s.ConsoleColor.DarkBlue;
                s.Console.Clear();
                s.Console.WriteLine("Booting WaveOS Console!\nPress anything to continue.");
                s.Console.ReadKey();
                s.Console.BackgroundColor = ConsoleColor.Black;
                s.Console.Clear();
                s.Console.Write(WaveConfigs.osNameVersion);
                s.Console.WriteLine(" Console Session started.");
                s.Console.WriteLine("On " + DateTime.Now);
            }
        }
        private int frameCounter = 0;

        public void nothing() { if (0 == 0) { return; } }
        /// <summary>
        /// Modify this to redraw Background on next frame.
        /// </summary>
        public bool updateBg = true;
        
        //fps shit
        public static int FPS = 0;

        public static int LastS = -1;
        public static int Ticken = 0;

        public static int useMode = 0;

        public static void Update()
        {
            if (LastS == -1)
            {
                LastS = s.DateTime.UtcNow.Second;
            }
            if (s.DateTime.UtcNow.Second - LastS != 0)
            {
                if (s.DateTime.UtcNow.Second > LastS)
                {
                    FPS = Ticken / (s.DateTime.UtcNow.Second - LastS);
                    WaveConfigs.timer++;
                }
                LastS = s.DateTime.UtcNow.Second;
                Ticken = 0;
            }
            Ticken++;
        }

        protected override void Run()
        {
            if (useMode == 1)
            {
                console();
            } else
            {
                gui();
            }
        }

        public static void console()
        {
            redo:
            s.Console.Write("wave::> ");
            var ln = s.Console.ReadLine();
            if (ln == "startw")
            {
                //start wavegui
                WaveConfigs.WaFs.CreateFile("0:\\BOOTTOGUI.signal");
                Power.Reboot();
            } else if (ln == "shutdown")
            {
                Power.Shutdown();
            }
            goto redo;
        }

        public static void gui()
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
            }
            else if (Sys.MouseManager.X + WaveConfigs.currentCursor.Width > WaveConfigs.displayW)
            {
                Sys.MouseManager.X = WaveConfigs.displayW - WaveConfigs.currentCursor.Width;
            }
            if (Sys.MouseManager.Y < 0)
            {
                Sys.MouseManager.Y = 0;
            }
            else if (Sys.MouseManager.Y + WaveConfigs.currentCursor.Height > WaveConfigs.displayH)
            {
                Sys.MouseManager.Y = WaveConfigs.displayH - WaveConfigs.currentCursor.Height;
            }
            ImprovedVBE.DrawImageAlpha(WaveConfigs.currentCursor, (int)Sys.MouseManager.X, (int)Sys.MouseManager.Y);
            //prevX = (int)Sys.MouseManager.X;
            //prevY = (int)Sys.MouseManager.Y;

            //rendering
            if (useCanvas == 1)
            {
                ImprovedVBE.display(displayM);
                displayM.Display();
            }
            else
            {
                ImprovedVBE.display(display);
                display.Display();
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
            }
            else if (currentSignal == "WAIT5-REBOOT")
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
