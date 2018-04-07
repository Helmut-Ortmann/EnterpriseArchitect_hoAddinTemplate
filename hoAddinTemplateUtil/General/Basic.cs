using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace IfManUtil.General
{
    public class Basic
    {
        public static void StartFile(string file)
        {
            try
            {
                Process.Start(file);
            }

            catch (Exception e)
            {
                MessageBox.Show($"File: '{file}'\r\n\r\n{e}", "Error start file!");
            }
        }
    }
}
