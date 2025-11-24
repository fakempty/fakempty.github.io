using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Laba6_3new
{
    public partial class Form1 : Form
    {
        // Наша колекція, що реалізує IEnumerable
        private WorkerTeam myTeam = new WorkerTeam();

        // Елементи GUI
        private TextBox txtName, txtAge, txtSalary;
        private ListBox lstWorkers;
        private Button btnAdd, btnSortAge, btnSortSalary;

        public Form1()
        {
            this.Text = "Лаб 6: Інтерфейси (IComparable, IComparer)";
            this.Size = new Size(500, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            InitializeCustomGUI();

            // Додамо декілька тестових даних одразу
            myTeam.Add(new Worker("Іван", 45, 15000));
            myTeam.Add(new Worker("Олена", 25, 20000));
            myTeam.Add(new Worker("Петро", 30, 12000));
            UpdateListDisplay();
        }

        private void InitializeCustomGUI()
        {
            int y = 20;

            // Поля введення
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

            // Кнопка додавання
            btnAdd = new Button { Text = "Додати", Location = new Point(270, 20), Size = new Size(100, 80), BackColor = Color.LightGreen };
            btnAdd.Click += BtnAdd_Click;
            this.Controls.Add(btnAdd);

            y += 50;
            // Кнопки сортування
            btnSortAge = new Button { Text = "Сортувати за ВІКОМ (IComparable)", Location = new Point(20, y), Size = new Size(220, 30) };
            btnSortAge.Click += BtnSortAge_Click;
            this.Controls.Add(btnSortAge);

            btnSortSalary = new Button { Text = "Сортувати за ЗАРПЛАТОЮ (IComparer)", Location = new Point(250, y), Size = new Size(220, 30) };
            btnSortSalary.Click += BtnSortSalary_Click;
            this.Controls.Add(btnSortSalary);

            y += 40;
            // Список виводу
            lstWorkers = new ListBox { Location = new Point(20, y), Size = new Size(450, 300) };
            this.Controls.Add(lstWorkers);
        }

        // Додавання нового працівника
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

        // 1. Сортування за віком (IComparable)
        private void BtnSortAge_Click(object sender, EventArgs e)
        {
            List<Worker> list = myTeam.GetList();

            // Метод Sort() без параметрів використовує інтерфейс IComparable,
            // який ми реалізували всередині класу Worker (порівняння за віком).
            list.Sort();

            UpdateListDisplay();
            MessageBox.Show("Відсортовано за віком (IComparable).");
        }

        // 2. Сортування за зарплатою (IComparer)
        private void BtnSortSalary_Click(object sender, EventArgs e)
        {
            List<Worker> list = myTeam.GetList();

            // Метод Sort() з параметром приймає об'єкт IComparer.
            // Ми передаємо наш клас SalaryComparer.
            list.Sort(new SalaryComparer());

            UpdateListDisplay();
            MessageBox.Show("Відсортовано за зарплатою (IComparer).");
        }

        // Метод оновлення списку (Демонстрація IEnumerable)
        private void UpdateListDisplay()
        {
            lstWorkers.Items.Clear();

            // ТУТ ПРАЦЮЄ IEnumerable!
            // Завдяки тому, що WorkerTeam реалізує IEnumerable,
            // ми можемо використовувати його прямо в foreach.
            foreach (Worker w in myTeam)
            {
                lstWorkers.Items.Add(w); // Тут автоматично викликається w.ToString()
            }
        }
    }
}