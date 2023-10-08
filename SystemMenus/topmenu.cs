using Cosmos.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WaveOS.Apps;

namespace WaveOS.SystemMenus
{
    public struct TColor
    {
        public int r;
        public int g;
        public int b;
    }
    public struct TColor_Gray
    {
        public int c;
    }

    internal class topmenu
    {
        public static int menuHeight = 25;
        public TColor_Gray menuColor = new();

        public TColor_Gray waveOSMenu_Fore = new();
        public TColor_Gray waveOSMenu_Back = new();

        public TColor_Gray waveOSMenu__Shutdown_Fore = new();
        public TColor_Gray waveOSMenu__Shutdown_Back = new();

        public TColor_Gray waveOSMenu__About_Fore = new();
        public TColor_Gray waveOSMenu__About_Back = new();

        public TColor_Gray waveOSMenu__Reboot_Fore = new();
        public TColor_Gray waveOSMenu__Reboot_Back = new();

        public void init()
        {
            menuColor.c = WaveConfigs.cTheme.gMBg.r;


            waveOSMenu_Back.c = WaveConfigs.cTheme.gMI_Bg.r;
            waveOSMenu_Fore.c = WaveConfigs.cTheme.gMI_Fo.r;

            waveOSMenu__Shutdown_Fore.c = WaveConfigs.cTheme.gMI_Fo.r;
            waveOSMenu__Shutdown_Back.c = WaveConfigs.cTheme.gMI_Bg.r;

            waveOSMenu__About_Fore.c = WaveConfigs.cTheme.gMI_Fo.r;
            waveOSMenu__About_Back.c = WaveConfigs.cTheme.gMI_Bg.r;

            waveOSMenu__Reboot_Fore.c = WaveConfigs.cTheme.gMI_Fo.r;
            waveOSMenu__Reboot_Back.c = WaveConfigs.cTheme.gMI_Bg.r;
        }

        public bool toggledMenu_WaveOSMenu = false;

        public void show()
        {
            ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(menuColor.c, menuColor.c, menuColor.c), 0, 0, WaveConfigs.displayW, menuHeight);

            if (toggledMenu_WaveOSMenu)
            {
                //currentColor_WaveOSMenu_ForeR = menuR;
                //currentColor_WaveOSMenu_ForeG = menuG;
                //currentColor_WaveOSMenu_ForeB = menuB;

                waveOSMenu_Back.c = WaveConfigs.cTheme.gMI_Bg.r;
            } else
            {
                waveOSMenu_Fore.c = WaveConfigs.cTheme.gMI_Fo.r;
                waveOSMenu_Back.c = menuColor.c;
            }

            //global menu

            //--WaveOS permanent menu
            ImprovedVBE.DrawFilledCircle(ImprovedVBE.colourToNumber(waveOSMenu_Back.c), 15, menuHeight / 2, menuHeight / 2 + 1);
            ImprovedVBE.DrawFilledCircle(ImprovedVBE.colourToNumber(waveOSMenu_Back.c), 15 + 45, menuHeight / 2, menuHeight / 2);
            ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(waveOSMenu_Back.c), 20, 0, 45, menuHeight);
            //ImprovedVBE.DrawImageAlpha(WaveConfigs.waveTopBar, 0, 0);
            ImprovedVBE._DrawACSIIString("WaveOS", 15, 5, ImprovedVBE.colourToNumber(waveOSMenu_Fore.c));
            if (toggledMenu_WaveOSMenu)
            {
                //ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(0, 0, 0), 7, 5 + 9 + 5 + 2, 45, 2); //draw underline bar
                ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(waveOSMenu_Back.c), 0, menuHeight, 100, 100);

                //--WaveOS permanent menu --> shutdown button
                ImprovedVBE.DrawFilledCircle(ImprovedVBE.colourToNumber(waveOSMenu__Shutdown_Back.c), 10, menuHeight + 2 + 10, menuHeight / 2);
                ImprovedVBE.DrawFilledCircle(ImprovedVBE.colourToNumber(waveOSMenu__Shutdown_Back.c), 15 + (100 - 10), menuHeight + 2 + 10, menuHeight / 2);

                ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(waveOSMenu__Shutdown_Back.c), 15, menuHeight + 2, 100 - 10, 20);
                ImprovedVBE._DrawACSIIString("Shutdown", 7, menuHeight + 5, ImprovedVBE.colourToNumber(waveOSMenu__Shutdown_Fore.c));

                if (MouseManager.X > 5 && MouseManager.X < 100 && MouseManager.Y > menuHeight && MouseManager.Y < menuHeight + 20)
                {
                    if (MouseManager.MouseState == MouseState.Left)
                    {
                        toggledMenu_WaveOSMenu = false;
                        Kernel.sendSignalToKernel("WAIT5-CLOSE");
                        return;
                    } else
                    {
                        waveOSMenu__Shutdown_Fore.c = 0;
                        waveOSMenu__Shutdown_Back.c = 130;
                    }
                } else
                {
                    waveOSMenu__Shutdown_Fore.c = WaveConfigs.cTheme.gMI_Fo.r;
                    waveOSMenu__Shutdown_Back.c = WaveConfigs.cTheme.gMI_Bg.r;
                }

                //--WaveOS permanent menu --> about button
                ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(waveOSMenu__About_Back.c), 5, menuHeight + 2 + 20 + 2, 100 - 10, 20);
                ImprovedVBE._DrawACSIIString("About", 7, menuHeight + 5 + 20 + 2, ImprovedVBE.colourToNumber(waveOSMenu__About_Fore.c));

                if (MouseManager.X > 0 && MouseManager.X < 100 && MouseManager.Y > menuHeight + 20 && MouseManager.Y < menuHeight + 20 + 2 + 20)
                {
                    if (MouseManager.MouseState == MouseState.Left)
                    {
                        HelpWindow newAbout = new();
                        toggledMenu_WaveOSMenu = false;
                    }
                    else
                    {
                        waveOSMenu__About_Fore.c = 0;
                        waveOSMenu__About_Back.c = 130;
                    }
                }
                else
                {
                    waveOSMenu__About_Fore.c = WaveConfigs.cTheme.gMI_Fo.r;

                    waveOSMenu__About_Back.c = WaveConfigs.cTheme.gMI_Bg.r;
                }

                //--WaveOS permanent menu --> reboot button
                ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(waveOSMenu__Reboot_Back.c), 5, menuHeight + 2 + 20 + 20 + 2, 100 - 10, 20);
                ImprovedVBE._DrawACSIIString("Reboot", 7, menuHeight + 5 + 20 + 20 + 2, ImprovedVBE.colourToNumber(waveOSMenu__Reboot_Fore.c));

                if (MouseManager.X > 0 && MouseManager.X < 100 && MouseManager.Y > menuHeight + 20 + 20 && MouseManager.Y < menuHeight + 20 + 2 + 20 + 20)
                {
                    if (MouseManager.MouseState == MouseState.Left)
                    {
                        Kernel.sendSignalToKernel("WAIT5-REBOOT");
                        return;
                    }
                    else
                    {
                        waveOSMenu__Reboot_Fore.c = 0;
                        waveOSMenu__Reboot_Back.c = 130;
                    }
                }
                else
                {
                    waveOSMenu__Reboot_Fore.c = WaveConfigs.cTheme.gMI_Fo.r;
                    waveOSMenu__Reboot_Back.c = WaveConfigs.cTheme.gMI_Bg.r;
                }
            }

            ImprovedVBE._DrawACSIIString("FPS: " + Kernel.FPS.ToString(), WaveConfigs.displayW - (("FPS: " + Kernel.FPS.ToString()).Length * 10) - 5, 5, ImprovedVBE.colourToNumber(190,190,190));
            if (MouseManager.X >= 0 && MouseManager.X < 60 && MouseManager.Y >= 0 && MouseManager.Y < menuHeight
                && MouseManager.MouseState == MouseState.Left)
            {
                toggledMenu_WaveOSMenu = !toggledMenu_WaveOSMenu;
                MouseManager.MouseState = MouseState.None;
            } else if (MouseManager.X >= 0 && MouseManager.X < 60 && MouseManager.Y >= 0 && MouseManager.Y < menuHeight)
            {
                MouseManager.MouseState = MouseState.None;
                waveOSMenu_Back.c = 140;
            }
        }
    }
}
