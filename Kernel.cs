using Cosmos.System.FileSystem.VFS;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.Core.Memory;

namespace WaveOS
{
    public class Kernel : Sys.Kernel
    {
        public VBECanvas display = new(new Mode(640, 480, ColorDepth.ColorDepth32));
        protected override void BeforeRun()
        {
            VFSManager.RegisterVFS(WaveConfigs.WaFs);
            WaveSystemInit waveInit = new WaveSystemInit();
            waveInit.Start();
            
            //WaveConfigs.display = FullScreenCanvas.GetFullScreenCanvas(new Mode(640, 480, ColorDepth.ColorDepth32));
            Cosmos.System.MouseManager.ScreenWidth = 640;
            Cosmos.System.MouseManager.ScreenHeight = 480;
        }
        private int frameCounter = 0;
        
        /// <summary>
        /// Modify this to redraw Background on next frame.
        /// </summary>
        public bool updateBg = true;

        private int prevX = 0;
        private int prevY = 0;

        protected override void Run()
        {
            //if (updateBg)
            //{
                ImprovedVBE.DrawImageAlpha(WaveConfigs.waveBg, 1, 1);
            //    updateBg = false;
            //}

            /*if (prevX != Sys.MouseManager.X || prevY != Sys.MouseManager.Y || (prevX != Sys.MouseManager.X && prevY != Sys.MouseManager.Y))
            {
                updateBg = true;
            }*/
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

            frameCounter++;
            if (frameCounter == 40)
            {
                frameCounter = 0;
            }

            ImprovedVBE.display(display);
            display.Display();
            Heap.Collect();
        }
    }
}
