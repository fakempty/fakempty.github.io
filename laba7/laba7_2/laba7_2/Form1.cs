using System;
using System.Collections.Generic; // Для List
using System.Drawing;             // Для Point, Size, Color
using System.Linq;                // Для сортування
using System.Windows.Forms;       // Головний простір імен для форм

namespace laba7_2  // Ваша назва проекту з помилки
{
    // Опис структури
    public struct STUDENT
    {
        public string NAME;
        public string GROUP;
        public int[] SUBJECT;

        public STUDENT(string name, string group, int[] grades)
        {
            NAME = name;
            GROUP = group;
            SUBJECT = grades;
        }

        public override string ToString()
        {
            return $"{NAME} (Гр: {GROUP}) | Оцінки: {string.Join(", ", SUBJECT)}";
        }
    }

    // ВАЖЛИВО: додано ": Form" для виправлення помилок
    public partial class Form1 : Form
    {
        // Елементи інтерфейсу
        private TextBox txtName;
        private TextBox txtGroup;
        private TextBox txtGrades;
        private ListBox lstStudents;
        private Label lblResult;

        // Список студентів
        private List<STUDENT> tempStudentList = new List<STUDENT>();

        public Form1()
        {
            // Якщо у вас є файл Form1.Designer.cs, цей метод там визначений.
            // Якщо його немає або ви хочете створити інтерфейс кодом, 
            // ми ініціалізуємо налаштування тут:
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            // Налаштування форми
            this.Text = "Лабораторна №7 (Завдання 1.2)";
            this.Size = new Size(500, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Створення елементів (ті самі, що були в попередньому прикладі)
            Label lblName = new Label() { Text = "Прізвище та ініціали:", Location = new Point(20, 20), AutoSize = true };
            txtName = new TextBox() { Location = new Point(20, 45), Width = 200 };

            Label lblGroup = new Label() { Text = "Номер групи:", Location = new Point(240, 20), AutoSize = true };
            txtGroup = new TextBox() { Location = new Point(240, 45), Width = 100 };

            Label lblGrades = new Label() { Text = "5 оцінок (через пробіл або кому):", Location = new Point(20, 80), AutoSize = true };
            txtGrades = new TextBox() { Location = new Point(20, 105), Width = 320 };

            Button btnAdd = new Button() { Text = "Додати студента", Location = new Point(360, 45), Size = new Size(100, 80), BackColor = Color.LightBlue };
            btnAdd.Click += BtnAdd_Click;

            Button btnProcess = new Button() { Text = "Сортувати та знайти двієчників", Location = new Point(20, 140), Width = 440, Height = 40, BackColor = Color.LightGreen };
            btnProcess.Click += BtnProcess_Click;

            Label lblListTitle = new Label() { Text = "Список студентів:", Location = new Point(20, 190), AutoSize = true };
            lstStudents = new ListBox() { Location = new Point(20, 215), Width = 440, Height = 150 };

            Label lblResTitle = new Label() { Text = "Студенти з оцінкою '2':", Location = new Point(20, 380), AutoSize = true, Font = new Font(this.Font, FontStyle.Bold) };
            lblResult = new Label() { Location = new Point(20, 405), Size = new Size(440, 100), BorderStyle = BorderStyle.FixedSingle, BackColor = Color.WhiteSmoke };

            // Додавання на форму
            this.Controls.Add(lblName);
            this.Controls.Add(txtName);
            this.Controls.Add(lblGroup);
            this.Controls.Add(txtGroup);
            this.Controls.Add(lblGrades);
            this.Controls.Add(txtGrades);
            this.Controls.Add(btnAdd);
            this.Controls.Add(btnProcess);
            this.Controls.Add(lblListTitle);
            this.Controls.Add(lstStudents);
            this.Controls.Add(lblResTitle);
            this.Controls.Add(lblResult);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtName.Text.Trim();
                string group = txtGroup.Text.Trim();
                string gradesStr = txtGrades.Text;

                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(group))
                {
                    MessageBox.Show("Заповніть ім'я та групу!");
                    return;
                }

                int[] grades = gradesStr.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                                        .Select(int.Parse)
                                        .ToArray();

                if (grades.Length != 5)
                {
                    MessageBox.Show("Має бути введено рівно 5 оцінок!");
                    return;
                }

                STUDENT newStudent = new STUDENT(name, group, grades);
                tempStudentList.Add(newStudent);
                UpdateListBox(tempStudentList.ToArray());

                txtName.Clear(); txtGroup.Clear(); txtGrades.Clear(); txtName.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка введення: " + ex.Message);
            }
        }

        private void BtnProcess_Click(object sender, EventArgs e)
        {
            if (tempStudentList.Count == 0) return;

            // 1. Формуємо масив LEARNER
            STUDENT[] LEARNER = tempStudentList.ToArray();

            // 2. Сортуємо
            Array.Sort(LEARNER, (s1, s2) => string.Compare(s1.NAME, s2.NAME));
            UpdateListBox(LEARNER);

            // 3. Шукаємо двієчників
            string resultText = "";
            bool foundBadStudent = false;

            foreach (var student in LEARNER)
            {
                bool hasTwo = false;
                foreach (int grade in student.SUBJECT)
                {
                    if (grade == 2) { hasTwo = true; break; }
                }

                if (hasTwo)
                {
                    resultText += $"{student.NAME} (Група {student.GROUP})\n";
                    foundBadStudent = true;
                }
            }

            if (!foundBadStudent)
            {
                lblResult.Text = "Студентів з оцінкою '2' немає.";
                lblResult.ForeColor = Color.Green;
            }
            else
            {
                lblResult.Text = resultText;
                lblResult.ForeColor = Color.Red;
            }
        }

        private void UpdateListBox(STUDENT[] students)
        {
            lstStudents.Items.Clear();
            foreach (var s in students) lstStudents.Items.Add(s);
        }
    }
}