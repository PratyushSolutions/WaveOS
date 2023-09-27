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

        public static WaveMessage currentMessage;

        public static void NewShow(WaveMessage newMsg) => currentMessage = newMsg;

        public static void DrawCurrentMessage()
        {
            if (currentMessage.id == 0)
                return;
            ImprovedVBE.DrawFilledCircle(ImprovedVBE.colourToNumber(32, 32, 32), WaveConfigs.displayW - 200 + 3, topmenu.menuHeight + 5 + 10 - 1, 10);
            ImprovedVBE.DrawFilledCircle(ImprovedVBE.colourToNumber(32, 32, 32), WaveConfigs.displayW - 200 + 3, topmenu.menuHeight + 50 + 5 - 10 - 1 - 1, 10);
            ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(32, 32, 32), WaveConfigs.displayW - 200 - 7, topmenu.menuHeight + 5 + 10, 10, 35);

            ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(32, 32, 32), WaveConfigs.displayW - 200, topmenu.menuHeight + 5, 200 - 20, 50);

            ImprovedVBE.DrawFilledCircle(ImprovedVBE.colourToNumber(32, 32, 32), WaveConfigs.displayW - 20 + 3, topmenu.menuHeight + 5 + 10 - 1, 10);
            ImprovedVBE.DrawFilledCircle(ImprovedVBE.colourToNumber(32, 32, 32), WaveConfigs.displayW - 20 + 3, topmenu.menuHeight + 50 + 5 - 10 - 1 - 1, 10);
            ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(32, 32, 32), WaveConfigs.displayW - 20, topmenu.menuHeight + 5 + 10, 14, 35);

            ImprovedVBE._DrawACSIIString(currentMessage.message, WaveConfigs.displayW - 200 + 5, topmenu.menuHeight + 10, ImprovedVBE.colourToNumber(32, 32, 32));
        }
    }
}
