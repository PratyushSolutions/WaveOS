using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveOS.Wave_zSH
{
    internal class WSH
    {
        public static string handleWSHCommand(string command, string currentBuf)
        {
            string buf = currentBuf + "\n";
            if (command == "echo")
            {
                buf += "Helo!";
            }
            buf += "\n# ";
            return buf;
        }
    }
}
