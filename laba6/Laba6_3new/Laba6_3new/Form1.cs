using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Laba6_3new
{
    public partial class Form1 : Form
    {
        private WorkerTeam myTeam = new WorkerTeam();

        private TextBox txtName, txtAge, txtSalary;
        private ListBox lstWorkers;
        private Button btnAdd, btnSortAge, btnSortSalary;

        public Form1()
        {
            this.Text = "Лаб 6: Інтерфейси (IComparable, IComparer)";
            this.Size = new Size(500, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            InitializeCustomGUI();

            myTeam.Add(new Worker("Іван", 45, 15000));
            myTeam.Add(new Worker("Олена", 25, 20000));
            myTeam.Add(new Worker("Петро", 30, 12000));
            UpdateListDisplay();
        }

        private void InitializeCustomGUI()
        {
            int y = 20;

            this.Controls.Add(new Label { Text = "Ім'я:", Location = new Point(20, y) });
            txtName = new TextBox { Location = new Point(100, y), Width = 150 };
            this.Controls.Add(txtName);

            y += 30;
            this.Controls.Add(new Label { Text = "Вік:", Location = new Point(20, y) });
            txtAge = new TextBox { Location = new Point(100, y), Width = 150 };
            this.Controls.Add(txtAge);

            y += 30;
            this.Controls.Add(new Label { Text = "Зарплата:", Location = new Point(20, y) });
            txtSalary = new TextBox { Location = new Point(100, y), Width = 150 };
            this.Controls.Add(txtSalary);

            btnAdd = new Button { Text = "Додати", Location = new Point(270, 20), Size = new Size(100, 80), BackColor = Color.LightGreen };
            btnAdd.Click += BtnAdd_Click;
            this.Controls.Add(btnAdd);

            y += 50;
            btnSortAge = new Button { Text = "Сортувати за ВІКОМ (IComparable)", Location = new Point(20, y), Size = new Size(220, 30) };
            btnSortAge.Click += BtnSortAge_Click;
            this.Controls.Add(btnSortAge);

            btnSortSalary = new Button { Text = "Сортувати за ЗАРПЛАТОЮ (IComparer)", Location = new Point(250, y), Size = new Size(220, 30) };
            btnSortSalary.Click += BtnSortSalary_Click;
            this.Controls.Add(btnSortSalary);

            y += 40;
            lstWorkers = new ListBox { Location = new Point(20, y), Size = new Size(450, 300) };
            this.Controls.Add(lstWorkers);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtName.Text;
                int age = int.Parse(txtAge.Text);
                decimal salary = decimal.Parse(txtSalary.Text);

                myTeam.Add(new Worker(name, age, salary));
                UpdateListDisplay();

                txtName.Clear(); txtAge.Clear(); txtSalary.Clear();
            }
            catch
            {
                MessageBox.Show("Перевірте коректність вводу чисел!");
            }
        }

        private void BtnSortAge_Click(object sender, EventArgs e)
        {
            List<Worker> list = myTeam.GetList();

            list.Sort();

            UpdateListDisplay();
            MessageBox.Show("Відсортовано за віком (IComparable).");
        }

        private void BtnSortSalary_Click(object sender, EventArgs e)
        {
            List<Worker> list = myTeam.GetList();

            list.Sort(new SalaryComparer());

            UpdateListDisplay();
            MessageBox.Show("Відсортовано за зарплатою (IComparer).");
        }

        private void UpdateListDisplay()
        {
            lstWorkers.Items.Clear();

            foreach (Worker w in myTeam)
            {
                lstWorkers.Items.Add(w);
            }
        }
    }
}