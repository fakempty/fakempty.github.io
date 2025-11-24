using System;
using System.Windows.Forms;

namespace Laba6_2new
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Якщо виникне помилка тут, перевірте, чи назва форми Form1 правильна
            Application.Run(new Form1());
        }
    }
}