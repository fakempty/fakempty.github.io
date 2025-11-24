using System;
using System.Drawing;
using System.Windows.Forms;

namespace Laba6_2new
{
    public partial class Form1 : Form
    {
        // Об'єкти (зберігаємо посилання через інтерфейс або базовий тип Object, 
        // але для простоти тут використаємо конкретні типи)
        private Helicopter myHelicopter;
        private Plane myPlane;

        // Прапорець, щоб знати, з ким працюємо зараз
        private bool isHelicopterMode = true;

        // Елементи GUI
        private RadioButton rbHelicopter, rbPlane;
        private TextBox txtModel, txtSpecParam, txtFrom, txtTo; // SpecParam = гвинти або двигуни
        private Label lblSpecParam;
        private Label lblResult;
        private Button btnCreate, btnFly, btnLand, btnRoute, btnUnique1, btnUnique2;

        public Form1()
        {
            this.Text = "Лаб 6: Інтерфейси (Транспорт)";
            this.Size = new Size(600, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            InitializeCustomGUI();
        }

        private void InitializeCustomGUI()
        {
            int x = 20; int y = 20;

            // 1. Вибір типу транспорту
            Label lblType = new Label { Text = "Оберіть транспорт:", Location = new Point(x, y), AutoSize = true };
            this.Controls.Add(lblType);

            rbHelicopter = new RadioButton { Text = "Вертоліт", Location = new Point(x + 120, y), Checked = true, AutoSize = true };
            rbPlane = new RadioButton { Text = "Літак", Location = new Point(x + 200, y), AutoSize = true };

            rbHelicopter.CheckedChanged += ModeChanged;
            rbPlane.CheckedChanged += ModeChanged;

            this.Controls.Add(rbHelicopter);
            this.Controls.Add(rbPlane);

            y += 40;

            // 2. Поля введення
            this.Controls.Add(new Label { Text = "Модель:", Location = new Point(x, y), AutoSize = true });
            txtModel = new TextBox { Location = new Point(x + 100, y), Width = 150, Text = "Mi-8" };
            this.Controls.Add(txtModel);

            y += 30;
            lblSpecParam = new Label { Text = "К-сть гвинтів:", Location = new Point(x, y), AutoSize = true };
            this.Controls.Add(lblSpecParam);
            txtSpecParam = new TextBox { Location = new Point(x + 100, y), Width = 150, Text = "1" };
            this.Controls.Add(txtSpecParam);

            y += 40;
            // Маршрут
            this.Controls.Add(new Label { Text = "Звідки:", Location = new Point(x, y) });
            txtFrom = new TextBox { Location = new Point(x + 60, y), Width = 100, Text = "Київ" };
            this.Controls.Add(txtFrom);

            this.Controls.Add(new Label { Text = "Куди:", Location = new Point(x + 180, y) });
            txtTo = new TextBox { Location = new Point(x + 230, y), Width = 100, Text = "Львів" };
            this.Controls.Add(txtTo);

            y += 40;
            // 3. Кнопка створення
            btnCreate = new Button { Text = "Створити об'єкт", Location = new Point(x, y), Size = new Size(330, 40), BackColor = Color.LightGreen };
            btnCreate.Click += BtnCreate_Click;
            this.Controls.Add(btnCreate);

            y += 60;
            // 4. Кнопки дій (Інтерфейси)
            this.Controls.Add(new Label { Text = "Методи інтерфейсів:", Location = new Point(x, y), Font = new Font(DefaultFont, FontStyle.Bold) });
            y += 25;

            btnFly = new Button { Text = "Злетіти (IAirTransport)", Location = new Point(x, y), Size = new Size(160, 30) };
            btnLand = new Button { Text = "Сісти (IAirTransport)", Location = new Point(x + 170, y), Size = new Size(160, 30) };
            btnFly.Click += BtnInterfaceAction_Click;
            btnLand.Click += BtnInterfaceAction_Click;
            this.Controls.Add(btnFly);
            this.Controls.Add(btnLand);

            y += 40;
            btnRoute = new Button { Text = "Розрахувати маршрут (IRoute)", Location = new Point(x, y), Size = new Size(330, 30) };
            btnRoute.Click += BtnInterfaceAction_Click;
            this.Controls.Add(btnRoute);

            y += 50;
            // 5. Унікальні кнопки
            this.Controls.Add(new Label { Text = "Унікальні методи класу:", Location = new Point(x, y), Font = new Font(DefaultFont, FontStyle.Bold) });
            y += 25;

            btnUnique1 = new Button { Text = "Зависнути", Location = new Point(x, y), Size = new Size(160, 30), BackColor = Color.LightYellow };
            btnUnique2 = new Button { Text = "Рятувальна місія", Location = new Point(x + 170, y), Size = new Size(160, 30), BackColor = Color.LightYellow };
            btnUnique1.Click += BtnUnique_Click;
            btnUnique2.Click += BtnUnique_Click;
            this.Controls.Add(btnUnique1);
            this.Controls.Add(btnUnique2);

            y += 50;
            // 6. Результат
            lblResult = new Label { Location = new Point(x, y), Size = new Size(500, 100), BorderStyle = BorderStyle.FixedSingle, Text = "Результат буде тут..." };
            this.Controls.Add(lblResult);
        }

        // Перемикання радіо-кнопок
        private void ModeChanged(object sender, EventArgs e)
        {
            isHelicopterMode = rbHelicopter.Checked;
            if (isHelicopterMode)
            {
                lblSpecParam.Text = "К-сть гвинтів:";
                txtModel.Text = "Mi-8";
                btnUnique1.Text = "Зависнути";
                btnUnique2.Text = "Рятувальна місія";
            }
            else
            {
                lblSpecParam.Text = "К-сть двигунів:";
                txtModel.Text = "Boeing 747";
                btnUnique1.Text = "Прибрати шасі";
                btnUnique2.Text = "Надзвуковий форсаж";
            }
        }

        // Створення об'єкта
        private void BtnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (isHelicopterMode)
                {
                    myHelicopter = new Helicopter();
                    myHelicopter.Model = txtModel.Text;
                    myHelicopter.RotorCount = int.Parse(txtSpecParam.Text);
                    // Задаємо маршрут відразу при створенні для тесту
                    myHelicopter.SetRoute(txtFrom.Text, txtTo.Text);

                    myPlane = null; // Очищуємо інший об'єкт
                    lblResult.Text = $"Створено ВЕРТОЛІТ: {myHelicopter.Model}";
                }
                else
                {
                    myPlane = new Plane();
                    myPlane.Model = txtModel.Text;
                    myPlane.JetEngines = int.Parse(txtSpecParam.Text);
                    myPlane.SetRoute(txtFrom.Text, txtTo.Text);

                    myHelicopter = null;
                    lblResult.Text = $"Створено ЛІТАК: {myPlane.Model}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка даних: " + ex.Message);
            }
        }

        // Виконання методів інтерфейсів (Спільні для обох)
        private void BtnInterfaceAction_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string result = "";

            if (isHelicopterMode && myHelicopter != null)
            {
                if (btn == btnFly) result = myHelicopter.Fly();
                if (btn == btnLand) result = myHelicopter.Land();
                if (btn == btnRoute) result = myHelicopter.GetEstimatedTime();
            }
            else if (!isHelicopterMode && myPlane != null)
            {
                if (btn == btnFly) result = myPlane.Fly();
                if (btn == btnLand) result = myPlane.Land();
                if (btn == btnRoute) result = myPlane.GetEstimatedTime();
            }
            else
            {
                result = "Спочатку створіть об'єкт!";
            }

            lblResult.Text = result;
        }

        // Виконання унікальних методів
        private void BtnUnique_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string result = "";

            if (isHelicopterMode && myHelicopter != null)
            {
                if (btn == btnUnique1) result = myHelicopter.Hover();
                if (btn == btnUnique2) result = myHelicopter.RescueMission();
            }
            else if (!isHelicopterMode && myPlane != null)
            {
                if (btn == btnUnique1) result = myPlane.RetractLandingGear();
                if (btn == btnUnique2) result = myPlane.SupersonicBoost();
            }
            else
            {
                result = "Спочатку створіть об'єкт!";
            }

            lblResult.Text = result;
        }
    }
}