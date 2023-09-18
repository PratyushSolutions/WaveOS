using Cosmos.System.FileSystem.VFS;
using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace WaveOS
{
    public class Kernel : Sys.Kernel
    {

        protected override void BeforeRun()
        {
            VFSManager.RegisterVFS(WaveConfigs.WaFs);
            WaveSystemInit waveInit = new WaveSystemInit();
            waveInit.Start();

            WaveConfigs.display = FullScreenCanvas.GetFullScreenCanvas(new Mode(640, 480, ColorDepth.ColorDepth32));
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
            if (updateBg)
            {
                WaveConfigs.display.DrawImageAlpha(WaveConfigs.waveBg, 0, 0);
                updateBg = false;
            }

            if (prevX != Sys.MouseManager.X || prevY != Sys.MouseManager.Y || (prevX != Sys.MouseManager.X && prevY != Sys.MouseManager.Y))
            {
                updateBg = true;
            }

            WaveConfigs.display.DrawImageAlpha(WaveConfigs.waveCursor, (int)Sys.MouseManager.X, (int)Sys.MouseManager.Y);
            prevX = (int)Sys.MouseManager.X;
            prevY = (int)Sys.MouseManager.Y;

            WaveConfigs.display.Display();
            frameCounter++;
            if (frameCounter == 30)
            {
                Cosmos.Core.Memory.Heap.Collect();
                frameCounter = 0;
            }
        }
    }
}
