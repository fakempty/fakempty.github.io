using System;
using System.Windows.Forms;

namespace ZooLab
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

            // Запускаємо форму, яка знаходиться в цьому ж просторі імен (ZooLab)
            Application.Run(new Form1());
        }
    }
}