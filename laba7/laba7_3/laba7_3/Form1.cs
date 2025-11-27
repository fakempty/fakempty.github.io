using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Globalization; // Необхідно для точного парсингу дат
using System.Windows.Forms;

namespace laba7_3
{
    // 1. Опис структури
    public struct RepairRequest
    {
        public string FullName;       // ПІБ
        public string PhoneNumber;    // Телефон
        public DateTime FailureDate;  // Дата/Час поломки
        public DateTime RepairDate;   // Дата/Час усунення

        // Обчислювана властивість: тривалість ремонту
        public TimeSpan Duration => RepairDate - FailureDate;

        public RepairRequest(string name, string phone, DateTime failDate, DateTime repDate)
        {
            FullName = name;
            PhoneNumber = phone;
            FailureDate = failDate;
            RepairDate = repDate;
        }

        // Форматований вивід
        public override string ToString()
        {
            return $"{FullName} ({PhoneNumber}) | Поломка: {FailureDate:dd.MM.yyyy HH:mm} | Ремонт: {RepairDate:dd.MM.yyyy HH:mm}";
        }
    }

    // 2. Головна форма
    public partial class Form1 : Form
    {
        // Список заявок
        private List<RepairRequest> database = new List<RepairRequest>();

        // Формати введення згідно завдання
        private const string DateFormat = "ddMMyyyy";
        private const string TimeFormat = "HH:mm:ss";

        // Елементи інтерфейсу
        TextBox txtName, txtPhone;
        TextBox txtDateFail, txtTimeFail;
        TextBox txtDateRep, txtTimeRep;
        RichTextBox rtbOutput; // Для виведення звітів

        public Form1()
        {
            InitializeCustomComponents();

            // --- ТЕСТОВІ ДАНІ (Можна видалити) ---
            // Додамо кілька записів автоматично для перевірки, щоб не вводити вручну
            try
            {
                AddRecord("Іванов І.І.", "050-111-22-33", "01012024", "10:00:00", "05012024", "12:00:00"); // Минулий рік
                AddRecord("Петров П.П.", "067-999-88-77", "15102025", "09:00:00", "15102025", "18:30:00"); // Цей рік (швидко)
                AddRecord("Сидоров С.С.", "063-555-44-33", "20102025", "08:00:00", "25102025", "08:00:00"); // Минулий місяць (якщо зараз листопад)
            }
            catch { }
        }

        private void InitializeCustomComponents()
        {
            this.Text = "Лабораторна 7.3: Телефонна мережа";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Секція введення
            int y = 20;
            int lblW = 120;
            int txtW = 150;

            // ПІБ і Телефон
            CreateInputRow("ПІБ абонента:", ref txtName, 20, y);
            CreateInputRow("Номер телефону:", ref txtPhone, 350, y);
            y += 40;

            // Поломка
            CreateInputRow($"Дата поломки ({DateFormat}):", ref txtDateFail, 20, y);
            CreateInputRow($"Час поломки ({TimeFormat}):", ref txtTimeFail, 350, y);
            y += 40;

            // Усунення
            CreateInputRow($"Дата усунення ({DateFormat}):", ref txtDateRep, 20, y);
            CreateInputRow($"Час усунення ({TimeFormat}):", ref txtTimeRep, 350, y);
            y += 50;

            // Кнопка додати
            Button btnAdd = new Button() { Text = "ДОДАТИ ЗАЯВКУ", Location = new Point(20, y), Size = new Size(630, 30), BackColor = Color.LightSkyBlue };
            btnAdd.Click += BtnAdd_Click;
            this.Controls.Add(btnAdd);
            y += 40;

            // Кнопки завдань (Панель керування)
            CreateTaskButton("1. Всі заявки (термін у днях)", BtnShowAll_Click, 20, y);
            CreateTaskButton("2. Поломки за минулий місяць", BtnLastMonth_Click, 240, y);
            CreateTaskButton("3. Кількість заявок минулого року", BtnLastYearCount_Click, 460, y);
            y += 40;
            CreateTaskButton("4. Найдовше усунення (цього року)", BtnLongestRepair_Click, 20, y);
            CreateTaskButton("5. Всі поломки за останні 12 міс.", BtnLast12Months_Click, 240, y);

            y += 50;

            // Поле виводу
            Label lblOut = new Label() { Text = "Результати виконання:", Location = new Point(20, y), AutoSize = true, Font = new Font(this.Font, FontStyle.Bold) };
            this.Controls.Add(lblOut);

            rtbOutput = new RichTextBox() { Location = new Point(20, y + 25), Size = new Size(740, 200), Font = new Font("Consolas", 10) };
            this.Controls.Add(rtbOutput);
        }

        // Допоміжний метод для створення полів
        private void CreateInputRow(string labelText, ref TextBox textBox, int x, int y)
        {
            Label lbl = new Label() { Text = labelText, Location = new Point(x, y), AutoSize = true };
            textBox = new TextBox() { Location = new Point(x + 130, y - 3), Width = 150 };
            this.Controls.Add(lbl);
            this.Controls.Add(textBox);
        }

        private void CreateTaskButton(string text, EventHandler handler, int x, int y)
        {
            Button btn = new Button() { Text = text, Location = new Point(x, y), Size = new Size(210, 35), BackColor = Color.WhiteSmoke };
            btn.Click += handler;
            this.Controls.Add(btn);
        }

        // --- ЛОГІКА ПРОГРАМИ ---

        // Додавання запису
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            AddRecord(txtName.Text, txtPhone.Text, txtDateFail.Text, txtTimeFail.Text, txtDateRep.Text, txtTimeRep.Text);
        }

        private void AddRecord(string name, string phone, string dFail, string tFail, string dRep, string tRep)
        {
            try
            {
                // Парсинг дати та часу. Використовуємо ParseExact для суворої відповідності формату ДДММРРРР
                DateTime dtFail = DateTime.ParseExact(dFail + tFail, DateFormat + TimeFormat, CultureInfo.InvariantCulture);
                DateTime dtRep = DateTime.ParseExact(dRep + tRep, DateFormat + TimeFormat, CultureInfo.InvariantCulture);

                if (dtRep < dtFail)
                {
                    MessageBox.Show("Дата усунення не може бути раніше дати поломки!");
                    return;
                }

                RepairRequest req = new RepairRequest(name, phone, dtFail, dtRep);
                database.Add(req);

                rtbOutput.Text = $"Запис успішно додано!\n{req}";

                // Очищення полів
                txtName.Clear(); txtPhone.Clear();
            }
            catch (FormatException)
            {
                MessageBox.Show($"Помилка формату!\nДата має бути: {DateFormat} (напр. 01052024)\nЧас має бути: {TimeFormat} (напр. 14:30:00)");
            }
        }

        // Завдання 1: Вивести всі заявки + термін у днях
        private void BtnShowAll_Click(object sender, EventArgs e)
        {
            rtbOutput.Clear();
            rtbOutput.AppendText("--- ВСІ ЗАЯВКИ ТА ТЕРМІНИ ---\n");

            if (database.Count == 0) { rtbOutput.AppendText("Список порожній."); return; }

            foreach (var req in database)
            {
                // TotalDays повертає дробове число (напр. 1.5 дні). Форматуємо до 2 знаків.
                rtbOutput.AppendText($"Абонент: {req.FullName}\n");
                rtbOutput.AppendText($"   Поломка: {req.FailureDate} -> Усунення: {req.RepairDate}\n");
                rtbOutput.AppendText($"   Термін усунення: {req.Duration.TotalDays:F2} днів\n");
                rtbOutput.AppendText(new string('-', 40) + "\n");
            }
        }

        // Завдання 2: Поломки за минулий місяць
        private void BtnLastMonth_Click(object sender, EventArgs e)
        {
            rtbOutput.Clear();
            DateTime now = DateTime.Now;
            // Визначаємо дату початку і кінця минулого місяця
            DateTime firstDayOfCurrentMonth = new DateTime(now.Year, now.Month, 1);
            DateTime firstDayOfLastMonth = firstDayOfCurrentMonth.AddMonths(-1);
            DateTime lastDayOfLastMonth = firstDayOfCurrentMonth.AddDays(-1);

            rtbOutput.AppendText($"--- ПОЛОМКИ ЗА МИНУЛИЙ МІСЯЦЬ ({firstDayOfLastMonth:MM.yyyy}) ---\n");

            var filtered = database.Where(r => r.FailureDate >= firstDayOfLastMonth && r.FailureDate <= lastDayOfLastMonth).ToList();

            if (filtered.Count == 0) rtbOutput.AppendText("Записів не знайдено.");

            foreach (var req in filtered) rtbOutput.AppendText(req.ToString() + "\n");
        }

        // Завдання 3: Кількість заявок минулого року
        private void BtnLastYearCount_Click(object sender, EventArgs e)
        {
            int lastYear = DateTime.Now.Year - 1;
            var lastYearRequests = database.Where(r => r.FailureDate.Year == lastYear).ToList();

            rtbOutput.Clear();
            rtbOutput.AppendText($"--- СТАТИСТИКА ЗА {lastYear} РІК ---\n");
            rtbOutput.AppendText($"Кількість заявок: {lastYearRequests.Count}\n\n");

            foreach (var req in lastYearRequests)
            {
                rtbOutput.AppendText(req.ToString() + "\n");
            }
        }

        // Завдання 4: Найдовше усунення цього року
        private void BtnLongestRepair_Click(object sender, EventArgs e)
        {
            int currentYear = DateTime.Now.Year;
            // Беремо тільки ті, що почалися або завершилися цього року (або обидва, за умовою "інформацію... цього року")
            // Будемо вважати - поломки, що сталися цього року.
            var thisYearRequests = database.Where(r => r.FailureDate.Year == currentYear).ToList();

            rtbOutput.Clear();
            rtbOutput.AppendText($"--- НАЙДОВШИЙ РЕМОНТ ({currentYear}) ---\n");

            if (thisYearRequests.Count == 0)
            {
                rtbOutput.AppendText("Цього року заявок ще не було.");
                return;
            }

            // Сортуємо за спаданням тривалості і беремо перший елемент
            var longest = thisYearRequests.OrderByDescending(r => r.Duration).First();

            rtbOutput.AppendText($"Абонент: {longest.FullName}\n");
            rtbOutput.AppendText($"Тривалість: {longest.Duration.TotalHours:F1} годин ({longest.Duration.TotalDays:F2} днів)\n");
            rtbOutput.AppendText($"Період: з {longest.FailureDate} по {longest.RepairDate}");
        }

        // Завдання 5: Всі поломки за останній рік (останні 12 місяців)
        private void BtnLast12Months_Click(object sender, EventArgs e)
        {
            rtbOutput.Clear();
            DateTime oneYearAgo = DateTime.Now.AddYears(-1);

            rtbOutput.AppendText($"--- ПОЛОМКИ ЗА ОСТАННІЙ РІК (з {oneYearAgo:dd.MM.yyyy}) ---\n");

            var filtered = database.Where(r => r.FailureDate >= oneYearAgo).OrderBy(r => r.FailureDate).ToList();

            if (filtered.Count == 0) rtbOutput.AppendText("Записів не знайдено.");

            foreach (var req in filtered)
            {
                rtbOutput.AppendText($"{req.FailureDate:dd.MM.yyyy} - {req.FullName} ({req.PhoneNumber})\n");
            }
        }
    }
}