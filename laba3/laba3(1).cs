using System;
using System.Linq;
using System.Windows.Forms;

namespace abcfinder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_count(object sender, EventArgs e)
        {
            string input = txtInput.Text;
            int count = 0;
            
            for (int i = 0; i < input.Length - 2; i++)
            {
                if (input[i] == 'a' && input[i + 1] == 'b' && input[i + 2] == 'c')
                {
                    count++;
                }
            }

            lblResult.Text = $"Кількість входжень 'abc': {count}";
        }
    }
}
