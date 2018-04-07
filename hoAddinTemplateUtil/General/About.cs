using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace IfManUtil.General
{
    public  class Generals {
        /// <summary>
        /// OutputAboutMessage.
        /// </summary>
        /// <param name="description"></param>
        /// <param name="caption"></param>
        /// <param name="lDllNames"></param>
        /// <param name="userData"></param>
        public static void AboutMessage(string description, string caption, string[] lDllNames, string userData)
        {
            string pathRoot = Assembly.GetExecutingAssembly().Location;
            pathRoot = Path.GetDirectoryName(pathRoot);

            description = $@"{description}

Helmut.Ortmann@t-online.de
(+49) 172 / 51 79 16 7

";
            foreach (string dllName in lDllNames)
            {
                string pathDll = Path.Combine(new[] {pathRoot, dllName});
                string version = FileVersionInfo.GetVersionInfo(pathDll).FileVersion;
                string tab = dllName.Length > 10 ? "\t" : "\t\t";
                description =
                    $"{description}- {dllName,-26}{tab}: V{version}{Environment.NewLine}";
            }
            description = $"{description}\r\n\r\nInstalled at:\t {pathRoot}\r\nSettings:\t\t{userData}";
            MessageBox.Show(description, caption);
        }
    }
}

