using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;            // Для роботи з файлами
using System.Linq;          // Для методу Distinct()
using System.Text;          // Для StringBuilder
using System.Windows.Forms;

namespace laba9_1
{
    public partial class Form1 : Form
    {
        // Список L (згідно завдання)
        private List<char> L = new List<char>();

        // Елементи інтерфейсу
        private RichTextBox rtbOriginal;
        private RichTextBox rtbProcessed;
        private Button btnLoad;
        private Button btnProcessAndSave;
        private Label lblStatus;

        public Form1()
        {
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            this.Text = "Лабораторна №9: Списки та файли";
            this.Size = new Size(600, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            // 1. Блок оригінальних даних
            Label lblOrig = new Label() { Text = "Вхідні дані (з файлу):", Location = new Point(20, 20), AutoSize = true };
            rtbOriginal = new RichTextBox() { Location = new Point(20, 45), Size = new Size(540, 100), ReadOnly = true, BackColor = Color.WhiteSmoke };

            btnLoad = new Button() { Text = "1. Завантажити файл", Location = new Point(20, 155), Size = new Size(150, 30), BackColor = Color.LightBlue };
            btnLoad.Click += BtnLoad_Click;

            // 2. Блок оброблених даних
            Label lblProc = new Label() { Text = "Результат (без повторів):", Location = new Point(20, 210), AutoSize = true };
            rtbProcessed = new RichTextBox() { Location = new Point(20, 235), Size = new Size(540, 100), ReadOnly = true };

            btnProcessAndSave = new Button() { Text = "2. Обробити та Зберегти", Location = new Point(20, 345), Size = new Size(150, 30), BackColor = Color.LightGreen, Enabled = false };
            btnProcessAndSave.Click += BtnProcessAndSave_Click;

            // Рядок статусу
            lblStatus = new Label() { Text = "Очікування дій...", Location = new Point(20, 400), AutoSize = true, ForeColor = Color.Gray };

            // Додавання на форму
            this.Controls.AddRange(new Control[] { lblOrig, rtbOriginal, btnLoad, lblProc, rtbProcessed, btnProcessAndSave, lblStatus });
        }

        // --- ЛОГІКА РОБОТИ ---

        // 1. Завантаження з файлу
        private void BtnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string fileContent = File.ReadAllText(ofd.FileName);

                    // Перетворення стрічки у список символів List<char>
                    L = fileContent.ToList();

                    // Відображення на GUI
                    rtbOriginal.Text = string.Join(" ", L); // Показуємо через пробіл для наочності
                    lblStatus.Text = $"Файл завантажено. Кількість символів: {L.Count}";

                    // Активуємо кнопку обробки
                    btnProcessAndSave.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка читання файлу: " + ex.Message);
                }
            }
        }

        // 2. Обробка та збереження
        private void BtnProcessAndSave_Click(object sender, EventArgs e)
        {
            if (L == null || L.Count == 0) return;

            // --- ЕТАП 1: Вилучення повторів ---
            // Distinct() залишає тільки унікальні елементи в порядку їх першої появи
            List<char> uniqueList = L.Distinct().ToList();

            // Відображення результату на екрані
            rtbProcessed.Text = string.Join(" ", uniqueList);

            // --- ЕТАП 2: Форматування (по 8 елементів у рядок) ---
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < uniqueList.Count; i++)
            {
                sb.Append(uniqueList[i]);

                // Перевіряємо, чи є цей елемент восьмим у поточному рядку (індекси 7, 15, 23...)
                // А також перевіряємо, що це не останній елемент списку (щоб не було порожнього рядка в кінці)
                if ((i + 1) % 8 == 0 && i != uniqueList.Count - 1)
                {
                    sb.AppendLine(); // Додаємо перенос рядка
                }
            }

            // --- ЕТАП 3: Збереження у файл ---
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text Files (*.txt)|*.txt";
            sfd.FileName = "Result_Lab9.txt";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.WriteAllText(sfd.FileName, sb.ToString());
                    lblStatus.Text = $"Успішно збережено! Унікальних символів: {uniqueList.Count}";
                    MessageBox.Show("Файл збережено!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка запису файлу: " + ex.Message);
                }
            }
        }
    }
}