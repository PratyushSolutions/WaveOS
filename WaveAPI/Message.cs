using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveOS.SystemMenus;

namespace WaveOS.WaveAPI
{
    internal static class Message
    {
        public struct WaveMessage
        {
            public string message;
            public int id;
            public string app;
            public Bitmap? logo;
        }

        public static WaveMessage? currentMessage;

        public static void NewShow(WaveMessage newMsg) => currentMessage = newMsg;

        public static void DrawCurrentMessage()
        {
            ImprovedVBE.DrawFilledCircle(ImprovedVBE.colourToNumber(32, 32, 32), WaveConfigs.displayW - 200 + 3, topmenu.menuHeight + 5 + 10 - 1, 10);
            ImprovedVBE.DrawFilledCircle(ImprovedVBE.colourToNumber(32, 32, 32), WaveConfigs.displayW - 200 + 3, topmenu.menuHeight + 50 + 5 - 10 - 1, 10);
            ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(32, 32, 32), WaveConfigs.displayW - 200, topmenu.menuHeight + 5, 200 - 10, 50);
        }
    }
}
