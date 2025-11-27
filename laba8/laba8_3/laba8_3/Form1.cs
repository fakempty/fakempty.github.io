using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace laba8_3
{
    // 1. Оголошення Enum (Перелічення)
    public enum Season
    {
        Winter,
        Spring,
        Summer,
        Autumn,
        Exit,   // Для виходу (0)
        Unknown // Для помилкового вводу
    }

    public partial class Form1 : Form
    {
        // 2. Оголошення Dictionary (Словник)
        // Ключ - Enum (Season), Значення - Інформація про місяці
        private Dictionary<Season, string> seasonData;

        // Елементи GUI
        private TextBox txtInput;
        private Label lblResult;
        private Button btnCheck;

        public Form1()
        {
            InitializeCustomComponents();
            InitializeData();
        }

        // Ініціалізація словника даними
        private void InitializeData()
        {
            seasonData = new Dictionary<Season, string>
            {
                { Season.Spring, "Березень (31 день), Квітень (30 днів), Травень (31 день)" },
                { Season.Summer, "Червень (30 днів), Липень (31 день), Серпень (31 день)" },
                { Season.Autumn, "Вересень (30 днів), Жовтень (31 день), Листопад (30 днів)" },
                { Season.Winter, "Грудень (31 день), Січень (31 день), Лютий (28/29 днів)" }
            };
        }

        private void InitializeCustomComponents()
        {
            this.Text = "Лабораторна №8 (Завдання 1.3)";
            this.Size = new Size(500, 350);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Інструкція
            Label lblInstruct = new Label()
            {
                Text = "Введіть назву пори року (весна, літо, осінь, зима)\nабо '0' для виходу:",
                Location = new Point(20, 20),
                AutoSize = true,
                Font = new Font("Arial", 10)
            };

            // Поле вводу
            txtInput = new TextBox() { Location = new Point(20, 70), Width = 200, Font = new Font("Arial", 12) };

            // Кнопка перевірки
            btnCheck = new Button()
            {
                Text = "Отримати інформацію",
                Location = new Point(230, 68),
                Size = new Size(150, 30),
                BackColor = Color.LightGreen
            };
            btnCheck.Click += BtnCheck_Click;

            // Поле результату
            lblResult = new Label()
            {
                Text = "Результат буде тут...",
                Location = new Point(20, 120),
                Size = new Size(440, 100),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.WhiteSmoke,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 11, FontStyle.Bold)
            };

            this.Controls.Add(lblInstruct);
            this.Controls.Add(txtInput);
            this.Controls.Add(btnCheck);
            this.Controls.Add(lblResult);
        }

        // Метод обробки (імітація тіла циклу while)
        private void BtnCheck_Click(object sender, EventArgs e)
        {
            string userInput = txtInput.Text.Trim().ToLower();

            // 1. Визначаємо Enum на основі тексту
            Season currentSeason = ParseInput(userInput);

            // 2. Логіка SWITCH, як вимагається в завданні
            switch (currentSeason)
            {
                case Season.Spring:
                case Season.Summer:
                case Season.Autumn:
                case Season.Winter:
                    // Виклик методу для виведення інформації
                    ShowSeasonInfo(currentSeason);
                    break;

                case Season.Exit:
                    // Якщо введено '0'
                    MessageBox.Show("Робота завершена. До побачення!");
                    Application.Exit();
                    break;

                default: // Case Season.Unknown
                    lblResult.Text = "Невідома команда.\nСпробуйте: весна, літо, осінь, зима або 0.";
                    lblResult.ForeColor = Color.Red;
                    break;
            }

            // Очищення та фокус для наступного "оберту" циклу
            txtInput.SelectAll();
            txtInput.Focus();
        }

        // Допоміжний метод для перетворення рядка в Enum
        private Season ParseInput(string input)
        {
            if (input == "0") return Season.Exit;
            if (input == "весна") return Season.Spring;
            if (input == "літо") return Season.Summer;
            if (input == "осінь") return Season.Autumn;
            if (input == "зима") return Season.Winter;

            return Season.Unknown;
        }

        // Метод, який виводить перелік місяців (робота з Dictionary)
        private void ShowSeasonInfo(Season season)
        {
            if (seasonData.ContainsKey(season))
            {
                string info = seasonData[season];
                lblResult.Text = $"{season.ToString().ToUpper()}:\n{info}";
                lblResult.ForeColor = Color.Blue;
            }
        }
    }
}