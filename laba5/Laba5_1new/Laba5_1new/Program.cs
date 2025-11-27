using Laba5_1new;
using System;
using System.Windows.Forms;

namespace Laba5_1new
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            Application.Run(new Form1());
        }
    }
}