using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Laba5_1new
{
    public partial class Form1 : Form
    {
        private TextBox txtName, txtOwner, txtArea, txtCows, txtSheep, txtWorkers, txtBudget;
        private Label lblResult;

        private Farm myFarm;

        public Form1()
        {
            this.Text = "Лаб 5: Ферма (Інкапсуляція)";
            this.Size = new Size(500, 650);
            this.StartPosition = FormStartPosition.CenterScreen;

            InitializeCustomGUI();
        }

        private void InitializeCustomGUI()
        {
            int y = 20; 
            int labelX = 20;
            int txtX = 200;
            int gap = 40; 

            void CreateRow(string title, ref TextBox textBox, string defaultValue)
            {
                Label lbl = new Label { Text = title, Location = new Point(labelX, y), AutoSize = true };
                this.Controls.Add(lbl);

                textBox = new TextBox { Location = new Point(txtX, y), Width = 200, Text = defaultValue };
                this.Controls.Add(textBox);

                y += gap;
            }

            CreateRow("Назва ферми:", ref txtName, "Зоряна");
            CreateRow("Власник:", ref txtOwner, "Петренко П.П.");
            CreateRow("Площа землі (га):", ref txtArea, "100,5");
            CreateRow("Кількість корів:", ref txtCows, "50");
            CreateRow("Кількість овець:", ref txtSheep, "200");
            CreateRow("Кількість працівників:", ref txtWorkers, "12");
            CreateRow("Бюджет (грн):", ref txtBudget, "150000");

            y += 10;

            Button btnCreate = new Button
            {
                Text = "1. Створити об'єкт та зберегти у файл",
                Location = new Point(20, y),
                Size = new Size(380, 40),
                BackColor = Color.LightGreen
            };
            btnCreate.Click += BtnCreate_Click;
            this.Controls.Add(btnCreate);

            y += 50;

            Button btnCalc = new Button
            {
                Text = "2. Виконати методи класу (Розрахунки)",
                Location = new Point(20, y),
                Size = new Size(380, 40),
                BackColor = Color.LightSkyBlue
            };
            btnCalc.Click += BtnCalc_Click;
            this.Controls.Add(btnCalc);

            y += 50;

            Label lblTitleRes = new Label { Text = "Результати:", Location = new Point(20, y), AutoSize = true, Font = new Font(this.Font, FontStyle.Bold) };
            this.Controls.Add(lblTitleRes);

            y += 25;

            lblResult = new Label
            {
                Location = new Point(20, y),
                Size = new Size(440, 150),
                BorderStyle = BorderStyle.FixedSingle,
                Text = "Тут будуть результати...",
                Padding = new Padding(5)
            };
            this.Controls.Add(lblResult);
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                myFarm = new Farm();


                myFarm.FarmName = txtName.Text;
                myFarm.OwnerName = txtOwner.Text;

                myFarm.AreaHa = double.Parse(txtArea.Text);
                myFarm.CowCount = int.Parse(txtCows.Text);
                myFarm.SheepCount = int.Parse(txtSheep.Text);
                myFarm.WorkerCount = int.Parse(txtWorkers.Text);
                myFarm.Budget = double.Parse(txtBudget.Text);

                string info = myFarm.GetInfoString();
                File.WriteAllText("FarmData.txt", info, Encoding.UTF8);

                MessageBox.Show("Об'єкт успішно створено!\nДані збережено у файл FarmData.txt", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);

                lblResult.Text = "Об'єкт створено. Дані:\n" + info;
            }
            catch (FormatException)
            {
                MessageBox.Show("Помилка формату! Перевірте, чи ввели ви числа там, де потрібно (площа, кількість).", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка: " + ex.Message);
            }
        }

        private void BtnCalc_Click(object sender, EventArgs e)
        {
            if (myFarm == null)
            {
                MessageBox.Show("Спочатку створіть об'єкт (кнопка 1)!", "Увага");
                return;
            }

            int total = myFarm.CalculateTotalAnimals();
            double load = myFarm.CalculateAnimalsPerWorker();
            double tax = myFarm.CalculateLandTax();

            string report = $"--- РОЗРАХУНКИ МЕТОДІВ КЛАСУ ---\n\n" +
                            $"Загальна кількість тварин: {total} голів\n" +
                            $"Навантаження на 1 працівника: {load:F1} тварин\n" +
                            $"Податок на землю (прогноз): {tax:F2} грн";

            lblResult.Text = report;
        }
    }
}