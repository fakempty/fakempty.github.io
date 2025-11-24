using Laba5_1new;
using System;
using System.Windows.Forms;

namespace Laba5_1new
{
    static class Program
    {
        /// <summary>
        /// Головна точка входу для програми.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Запускаємо форму Form1
            Application.Run(new Form1());
        }
    }
}