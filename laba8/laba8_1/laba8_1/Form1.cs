using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq; // Необхідно для роботи з колекціями (Sum, Where)
using System.Windows.Forms;

namespace laba8_1
{
    // Клас форми
    public partial class Form1 : Form
    {
        // Оголошуємо список кортежів.
        // Кортеж містить 4 елементи: (Прізвище, Борг, Послуга, Адреса)
        private List<(string Surname, decimal Debt, string Service, string Address)> subscribers
            = new List<(string, decimal, string, string)>();

        // Елементи інтерфейсу
        private TextBox txtSurname, txtDebt, txtAddress;
        private ComboBox cmbService;
        private ListBox lstAllData;
        private RichTextBox rtbResult;

        public Form1()
        {
            InitializeCustomComponents();
        }

        // Побудова GUI програмно
        private void InitializeCustomComponents()
        {
            this.Text = "Лабораторна №8: Кортежі (Комунальні послуги)";
            this.Size = new Size(600, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            int xLabel = 20, xInput = 150, y = 20, gap = 35;

            // 1. Введення Прізвища (Обов'язкове)
            this.Controls.Add(new Label { Text = "Прізвище:", Location = new Point(xLabel, y), AutoSize = true });
            txtSurname = new TextBox { Location = new Point(xInput, y - 3), Width = 200 };
            this.Controls.Add(txtSurname);
            y += gap;

            // 2. Введення Боргу (Обов'язкове)
            this.Controls.Add(new Label { Text = "Сума боргу (грн):", Location = new Point(xLabel, y), AutoSize = true });
            txtDebt = new TextBox { Location = new Point(xInput, y - 3), Width = 100 };
            this.Controls.Add(txtDebt);
            y += gap;

            // 3. Тип послуги (Власне поле 1)
            this.Controls.Add(new Label { Text = "Послуга:", Location = new Point(xLabel, y), AutoSize = true });
            cmbService = new ComboBox { Location = new Point(xInput, y - 3), Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbService.Items.AddRange(new object[] { "Електроенергія", "Водопостачання", "Газ", "Опалення", "Квартплата" });
            cmbService.SelectedIndex = 0;
            this.Controls.Add(cmbService);
            y += gap;

            // 4. Адреса (Власне поле 2)
            this.Controls.Add(new Label { Text = "Адреса:", Location = new Point(xLabel, y), AutoSize = true });
            txtAddress = new TextBox { Location = new Point(xInput, y - 3), Width = 200 };
            this.Controls.Add(txtAddress);
            y += gap + 10;

            // Кнопка додавання
            Button btnAdd = new Button { Text = "Додати абонента", Location = new Point(20, y), Size = new Size(330, 30), BackColor = Color.LightBlue };
            btnAdd.Click += BtnAdd_Click;
            this.Controls.Add(btnAdd);
            y += 40;

            // Кнопка розрахунку (Логіка завдання)
            Button btnCalc = new Button { Text = "Знайти боржників на відключення", Location = new Point(20, y), Size = new Size(330, 40), BackColor = Color.Salmon };
            btnCalc.Click += BtnCalc_Click;
            this.Controls.Add(btnCalc);
            y += 50;

            // Виведення списку
            this.Controls.Add(new Label { Text = "Всі записи (Кортежі):", Location = new Point(20, y), AutoSize = true });
            lstAllData = new ListBox { Location = new Point(20, y + 20), Size = new Size(540, 100) };
            this.Controls.Add(lstAllData);
            y += 130;

            // Виведення результату
            this.Controls.Add(new Label { Text = "Результат аналізу:", Location = new Point(20, y), AutoSize = true, Font = new Font(this.Font, FontStyle.Bold) });
            rtbResult = new RichTextBox { Location = new Point(20, y + 20), Size = new Size(540, 80), ReadOnly = true, BackColor = Color.WhiteSmoke };
            this.Controls.Add(rtbResult);
        }

        // --- ОБРОБНИКИ ПОДІЙ ---

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Зчитування даних
                string surname = txtSurname.Text.Trim();
                string debtStr = txtDebt.Text.Trim().Replace('.', ','); // Захист від формату дробів
                string service = cmbService.SelectedItem.ToString();
                string address = txtAddress.Text.Trim();

                if (string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(debtStr))
                {
                    MessageBox.Show("Будь ласка, заповніть прізвище та суму боргу.");
                    return;
                }

                if (!decimal.TryParse(debtStr, out decimal debt) || debt < 0)
                {
                    MessageBox.Show("Борг має бути додатним числом!");
                    return;
                }

                // --- СТВОРЕННЯ КОРТЕЖУ ---
                // Ми створюємо іменований кортеж (ValueTuple)
                var newSubscriber = (Surname: surname, Debt: debt, Service: service, Address: address);

                // Додавання до колекції
                subscribers.Add(newSubscriber);

                // Виклик методу для виведення конкретного значення кортежу (відображення в списку)
                DisplayTuple(newSubscriber);

                // Очищення полів
                txtSurname.Clear();
                txtDebt.Clear();
                txtAddress.Clear();
                txtSurname.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка: " + ex.Message);
            }
        }

        // Метод для виводу значень кортежу (згідно завдання)
        private void DisplayTuple((string Surname, decimal Debt, string Service, string Address) tuple)
        {
            // Формуємо рядок, звертаючись до полів кортежу
            string displayString = $"{tuple.Surname} | Борг: {tuple.Debt} грн | {tuple.Service} | Адр: {tuple.Address}";
            lstAllData.Items.Add(displayString);
        }

        private void BtnCalc_Click(object sender, EventArgs e)
        {
            if (subscribers.Count == 0)
            {
                rtbResult.Text = "Список порожній.";
                return;
            }

            // 1. Рахуємо загальну суму всіх несплат
            decimal totalUnpaid = subscribers.Sum(s => s.Debt);

            // 2. Визначаємо поріг (половина від всіх несплат)
            decimal threshold = totalUnpaid / 2.0m;

            rtbResult.Text = $"Загальна сума боргів: {totalUnpaid} грн.\n";
            rtbResult.AppendText($"Критерій відключення (>= 50% від суми): {threshold} грн.\n\n");

            // 3. Шукаємо абонентів, чий борг >= половині загальної суми
            // Використовуємо LINQ Where
            var candidates = subscribers.Where(s => s.Debt >= threshold && totalUnpaid > 0).ToList();

            if (candidates.Count > 0)
            {
                rtbResult.AppendText($"Кількість абонентів на відключення: {candidates.Count}\n");
                foreach (var s in candidates)
                {
                    // Доступ до конкретного поля кортежу (s.Surname)
                    rtbResult.AppendText($">> {s.Surname} (Борг: {s.Debt} грн) - ВІДКЛЮЧИТИ\n");
                }
                rtbResult.ForeColor = Color.Red;
            }
            else
            {
                rtbResult.AppendText("Абонентів, чий борг становить половину всієї суми, не знайдено.");
                rtbResult.ForeColor = Color.Green;
            }
        }
    }
}