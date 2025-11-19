using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Lab6GUI
{
    // ============================
    // Клас Робочий
    // ============================
    public class Worker : IComparable<Worker>, IComparer<Worker>, IEnumerable
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public double Salary { get; set; }

        public Worker(string name, int age, double salary)
        {
            Name = name;
            Age = age;
            Salary = salary;
        }

        // --------------------------
        // IComparable — сортування за віком
        // --------------------------
        public int CompareTo(Worker other)
        {
            if (other == null) return 1;
            return this.Age.CompareTo(other.Age);
        }

        // --------------------------
        // IComparer — сортування за віком і зарплатою
        // --------------------------
        public int Compare(Worker x, Worker y)
        {
            int ageCompare = x.Age.CompareTo(y.Age);
            if (ageCompare == 0)
                return x.Salary.CompareTo(y.Salary);
            return ageCompare;
        }

        // --------------------------
        // IEnumerable — для перебору об'єктів
        // --------------------------
        private List<Worker> workersList = new List<Worker>();
        public void AddWorker(Worker w) => workersList.Add(w);
        public IEnumerator GetEnumerator() => workersList.GetEnumerator();

        public override string ToString() => $"{Name}, Вік: {Age}, Зарплата: {Salary}";
    }

    // ============================
    // GUI
    // ============================
    public class MainForm : Form
    {
        private TextBox txtName, txtAge, txtSalary;
        private ListBox lstWorkers;
        private Button btnAdd, btnSort;
        private Worker workers = new Worker("",0,0);

        public MainForm()
        {
            this.Text = "ЛР6 — Список робочих";
            this.Size = new System.Drawing.Size(500, 400);

            Label lblName = new Label() { Text = "Ім'я", Left = 20, Top = 20 };
            txtName = new TextBox() { Left = 100, Top = 20, Width = 150 };

            Label lblAge = new Label() { Text = "Вік", Left = 20, Top = 50 };
            txtAge = new TextBox() { Left = 100, Top = 50, Width = 50 };

            Label lblSalary = new Label() { Text = "Зарплата", Left = 20, Top = 80 };
            txtSalary = new TextBox() { Left = 100, Top = 80, Width = 100 };

            btnAdd = new Button() { Text = "Додати", Left = 20, Top = 120 };
            btnAdd.Click += BtnAdd_Click;

            btnSort = new Button() { Text = "Впорядкувати за зарплатою", Left = 120, Top = 120 };
            btnSort.Click += BtnSort_Click;

            lstWorkers = new ListBox() { Left = 20, Top = 160, Width = 400, Height = 180 };

            this.Controls.Add(lblName);
            this.Controls.Add(txtName);
            this.Controls.Add(lblAge);
            this.Controls.Add(txtAge);
            this.Controls.Add(lblSalary);
            this.Controls.Add(txtSalary);
            this.Controls.Add(btnAdd);
            this.Controls.Add(btnSort);
            this.Controls.Add(lstWorkers);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            if(!int.TryParse(txtAge.Text, out int age) || !double.TryParse(txtSalary.Text, out double salary))
            {
                MessageBox.Show("Вік та зарплата повинні бути числами!");
                return;
            }
            Worker w = new Worker(name, age, salary);
            workers.AddWorker(w);
            UpdateList();
            txtName.Clear(); txtAge.Clear(); txtSalary.Clear();
        }

        private void BtnSort_Click(object sender, EventArgs e)
        {
            var sorted = workers.Cast<Worker>().OrderBy(w => w.Salary).ToList();
            lstWorkers.Items.Clear();
            foreach(var w in sorted)
                lstWorkers.Items.Add(w);
        }

        private void UpdateList()
        {
            lstWorkers.Items.Clear();
            foreach(var w in workers)
                lstWorkers.Items.Add(w);
        }
    }

    // ============================
    // Запуск програми
    // ============================
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
