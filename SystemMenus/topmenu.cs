using Cosmos.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WaveOS.SystemMenus
{
    internal class topmenu
    {
        public int menuHeight = 25;

        public int menuR = 50;
        public int menuG = 50;
        public int menuB = 50;

        public void init()
        {
            //add something, idk
        }

        public int currentColor_WaveOSMenu_ForeR = 255;
        public int currentColor_WaveOSMenu_ForeG = 255;
        public int currentColor_WaveOSMenu_ForeB = 255;

        public int currentColor_WaveOSMenu_BackR = 130;
        public int currentColor_WaveOSMenu_BackG = 130;
        public int currentColor_WaveOSMenu_BackB = 130;

        public int currentColor_WaveOSMenu_Shutdown_ForeR = 255;
        public int currentColor_WaveOSMenu_Shutdown_ForeG = 255;
        public int currentColor_WaveOSMenu_Shutdown_ForeB = 255;
        public int currentColor_WaveOSMenu_Shutdown_BackR = 130;
        public int currentColor_WaveOSMenu_Shutdown_BackG = 130;
        public int currentColor_WaveOSMenu_Shutdown_BackB = 130;

        public int currentColor_WaveOSMenu_About_ForeR = 255;
        public int currentColor_WaveOSMenu_About_ForeG = 255;
        public int currentColor_WaveOSMenu_About_ForeB = 255;
        public int currentColor_WaveOSMenu_About_BackR = 130;
        public int currentColor_WaveOSMenu_About_BackG = 130;
        public int currentColor_WaveOSMenu_About_BackB = 130;

        public int currentColor_WaveOSMenu_Reboot_ForeR = 255;
        public int currentColor_WaveOSMenu_Reboot_ForeG = 255;
        public int currentColor_WaveOSMenu_Reboot_ForeB = 255;
        public int currentColor_WaveOSMenu_Reboot_BackR = 130;
        public int currentColor_WaveOSMenu_Reboot_BackG = 130;
        public int currentColor_WaveOSMenu_Reboot_BackB = 130;

        public bool toggledMenu_WaveOSMenu = false;

        public void show()
        {
            ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(menuR, menuG, menuB), 0, 0, WaveConfigs.displayW - 1, menuHeight);

            if (toggledMenu_WaveOSMenu)
            {
                currentColor_WaveOSMenu_ForeR = menuR;
                currentColor_WaveOSMenu_ForeG = menuG;
                currentColor_WaveOSMenu_ForeB = menuB;

                currentColor_WaveOSMenu_BackR = 130;
                currentColor_WaveOSMenu_BackG = 130;
                currentColor_WaveOSMenu_BackB = 130;
            } else
            {
                currentColor_WaveOSMenu_ForeR = 255;
                currentColor_WaveOSMenu_ForeG = 255;
                currentColor_WaveOSMenu_ForeB = 255;

                currentColor_WaveOSMenu_BackR = menuR;
                currentColor_WaveOSMenu_BackG = menuG;
                currentColor_WaveOSMenu_BackB = menuB;
            }

            //global menu

            //--WaveOS permanent menu
            ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(currentColor_WaveOSMenu_BackR, currentColor_WaveOSMenu_BackG, currentColor_WaveOSMenu_BackB), 0, 0, 60, menuHeight);
            ImprovedVBE._DrawACSIIString("WaveOS", 7, 5, ImprovedVBE.colourToNumber(currentColor_WaveOSMenu_ForeR, currentColor_WaveOSMenu_ForeG, currentColor_WaveOSMenu_ForeB));
            if (toggledMenu_WaveOSMenu)
            {
                ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(0, 0, 0), 7, 5 + 9 + 5 + 2, 45, 2);
                ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(currentColor_WaveOSMenu_BackR, currentColor_WaveOSMenu_BackG, currentColor_WaveOSMenu_BackB), 0, menuHeight, 100, 100);

                //--WaveOS permanent menu --> shutdown button
                ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(currentColor_WaveOSMenu_Shutdown_BackR, currentColor_WaveOSMenu_Shutdown_BackG, currentColor_WaveOSMenu_Shutdown_BackB), 5, menuHeight + 2, 100 - 10, 20);
                ImprovedVBE._DrawACSIIString("Shutdown", 7, menuHeight + 5, ImprovedVBE.colourToNumber(currentColor_WaveOSMenu_Shutdown_ForeR, currentColor_WaveOSMenu_Shutdown_ForeG, currentColor_WaveOSMenu_Shutdown_ForeB));

                if (MouseManager.X > 0 && MouseManager.X < 100 && MouseManager.Y > menuHeight && MouseManager.Y < menuHeight + 20)
                {
                    if (MouseManager.MouseState == MouseState.Left)
                    {
                        toggledMenu_WaveOSMenu = false;
                        ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(10, 10, 10), 0, 0, WaveConfigs.displayW - 2, WaveConfigs.displayH - 2);
                        ImprovedVBE._DrawACSIIString("Shutting down!", 5, 5, ImprovedVBE.colourToNumber(201, 28, 28));
                        Kernel.sendSignalToKernel("WAIT5-CLOSE");
                        return;
                    } else
                    {
                        currentColor_WaveOSMenu_Shutdown_ForeR = 0;
                        currentColor_WaveOSMenu_Shutdown_ForeG = 0;
                        currentColor_WaveOSMenu_Shutdown_ForeB = 0;

                        currentColor_WaveOSMenu_Shutdown_BackR = 190;
                        currentColor_WaveOSMenu_Shutdown_BackG = 190;
                        currentColor_WaveOSMenu_Shutdown_BackB = 190;
                    }
                } else
                {
                    currentColor_WaveOSMenu_Shutdown_ForeR = 255;
                    currentColor_WaveOSMenu_Shutdown_ForeG = 255;
                    currentColor_WaveOSMenu_Shutdown_ForeB = 255;

                    currentColor_WaveOSMenu_Shutdown_BackR = 130;
                    currentColor_WaveOSMenu_Shutdown_BackG = 130;
                    currentColor_WaveOSMenu_Shutdown_BackB = 130;
                }

                //--WaveOS permanent menu --> about button
                ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(currentColor_WaveOSMenu_About_BackR, currentColor_WaveOSMenu_About_BackG, currentColor_WaveOSMenu_About_BackB), 5, menuHeight + 2 + 20 + 2, 100 - 10, 20);
                ImprovedVBE._DrawACSIIString("About", 7, menuHeight + 5 + 20 + 2, ImprovedVBE.colourToNumber(currentColor_WaveOSMenu_About_ForeR, currentColor_WaveOSMenu_About_ForeG, currentColor_WaveOSMenu_About_ForeB));

                if (MouseManager.X > 0 && MouseManager.X < 100 && MouseManager.Y > menuHeight + 20 && MouseManager.Y < menuHeight + 20 + 2 + 20)
                {
                    if (MouseManager.MouseState == MouseState.Left)
                    {
                        HelpWindow newAbout = new();
                        toggledMenu_WaveOSMenu = false;
                    }
                    else
                    {
                        currentColor_WaveOSMenu_About_ForeR = 0;
                        currentColor_WaveOSMenu_About_ForeG = 0;
                        currentColor_WaveOSMenu_About_ForeB = 0;

                        currentColor_WaveOSMenu_About_BackR = 190;
                        currentColor_WaveOSMenu_About_BackG = 190;
                        currentColor_WaveOSMenu_About_BackB = 190;
                    }
                }
                else
                {
                    currentColor_WaveOSMenu_About_ForeR = 255;
                    currentColor_WaveOSMenu_About_ForeG = 255;
                    currentColor_WaveOSMenu_About_ForeB = 255;

                    currentColor_WaveOSMenu_About_BackR = 130;
                    currentColor_WaveOSMenu_About_BackG = 130;
                    currentColor_WaveOSMenu_About_BackB = 130;
                }

                //--WaveOS permanent menu --> reboot button
                ImprovedVBE.DrawFilledRectangle(ImprovedVBE.colourToNumber(currentColor_WaveOSMenu_Reboot_BackR, currentColor_WaveOSMenu_Reboot_BackG, currentColor_WaveOSMenu_Reboot_BackB), 5, menuHeight + 2 + 20 + 20 + 2, 100 - 10, 20);
                ImprovedVBE._DrawACSIIString("Reboot", 7, menuHeight + 5 + 20 + 20 + 2, ImprovedVBE.colourToNumber(currentColor_WaveOSMenu_Reboot_ForeR, currentColor_WaveOSMenu_Reboot_ForeG, currentColor_WaveOSMenu_Reboot_ForeB));

                if (MouseManager.X > 0 && MouseManager.X < 100 && MouseManager.Y > menuHeight + 20 + 20 && MouseManager.Y < menuHeight + 20 + 2 + 20 + 20)
                {
                    if (MouseManager.MouseState == MouseState.Left)
                    {
                        Kernel.sendSignalToKernel("WAIT5-REBOOT");
                        return;
                    }
                    else
                    {
                        currentColor_WaveOSMenu_Reboot_ForeR = 0;
                        currentColor_WaveOSMenu_Reboot_ForeG = 0;
                        currentColor_WaveOSMenu_Reboot_ForeB = 0;

                        currentColor_WaveOSMenu_Reboot_BackR = 190;
                        currentColor_WaveOSMenu_Reboot_BackG = 190;
                        currentColor_WaveOSMenu_Reboot_BackB = 190;
                    }
                }
                else
                {
                    currentColor_WaveOSMenu_Reboot_ForeR = 255;
                    currentColor_WaveOSMenu_Reboot_ForeG = 255;
                    currentColor_WaveOSMenu_Reboot_ForeB = 255;

                    currentColor_WaveOSMenu_Reboot_BackR = 130;
                    currentColor_WaveOSMenu_Reboot_BackG = 130;
                    currentColor_WaveOSMenu_Reboot_BackB = 130;
                }
            }

            ImprovedVBE._DrawACSIIString("FPS: " + Kernel.FPS.ToString(), WaveConfigs.displayW - (("FPS: " + Kernel.FPS.ToString()).Length * 10) - 5, 5, ImprovedVBE.colourToNumber(130, 130, 130));
            if (MouseManager.X >= 0 && MouseManager.X < 60 && MouseManager.Y >= 0 && MouseManager.Y < menuHeight
                && MouseManager.MouseState == MouseState.Left)
            {
                toggledMenu_WaveOSMenu = !toggledMenu_WaveOSMenu;
                MouseManager.MouseState = MouseState.None;
            }
        }
    }
}
