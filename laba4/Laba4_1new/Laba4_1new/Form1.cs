using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ZooLab
{
    public partial class Form1 : Form
    {
        // Елементи управління 
        private DataGridView dataGridView1;
        private Button btnLoad;
        private Button btnProcess;
        private Label lblStatus;


        private string inputFilePath = "Input_Data.txt";
        private string outputFilePath = "Output_Data.txt";


        private string[,] zooData;

        public Form1()
        {
            this.Text = "Лабораторна №4: Зоопарк";
            this.Size = new Size(900, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            // 1. Кнопка завантаження
            btnLoad = new Button();
            btnLoad.Text = "1. Зчитати файл";
            btnLoad.Location = new Point(12, 12);
            btnLoad.Size = new Size(150, 40);
            btnLoad.Click += BtnLoad_Click;
            this.Controls.Add(btnLoad);

            // 2. Кнопка обробки та запису
            btnProcess = new Button();
            btnProcess.Text = "2. Знайти тигрів і записати";
            btnProcess.Location = new Point(170, 12);
            btnProcess.Size = new Size(200, 40);
            btnProcess.Click += BtnProcess_Click;
            this.Controls.Add(btnProcess);

            // 3. Статус бар (лейбл)
            lblStatus = new Label();
            lblStatus.Text = "Очікування дій...";
            lblStatus.Location = new Point(380, 25);
            lblStatus.AutoSize = true;
            lblStatus.ForeColor = Color.Blue;
            this.Controls.Add(lblStatus);

            // 4. Таблиця для виводу даних
            dataGridView1 = new DataGridView();
            dataGridView1.Location = new Point(12, 70);
            dataGridView1.Size = new Size(860, 380);
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.Controls.Add(dataGridView1);

            string[] headers = { "Тварина", "Кількість", "Індекс", "Країна", "Область", "Район", "Місто", "Вулиця", "Буд.", "Кв.", "Всього тварин", "Працівників" };
            foreach (var header in headers)
            {
                dataGridView1.Columns.Add(header, header);
            }
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                string fullPath = Path.GetFullPath(inputFilePath);

                if (!File.Exists(inputFilePath))
                {
                    MessageBox.Show($"Файл не знайдено!\n\nПрограма шукає його тут:\n{fullPath}\n\nБудь ласка, створіть файл за цим шляхом.",
                                    "Помилка шляху", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Clipboard.SetText(Path.GetDirectoryName(fullPath));
                    return;
                }

                // Зчитуємо всі рядки
                string[] lines = File.ReadAllLines(inputFilePath, Encoding.UTF8);

                if (lines.Length == 0) return;

                int rows = lines.Length;
                int cols = lines[0].Split(';').Length;

                zooData = new string[rows, cols];
                dataGridView1.Rows.Clear();

                for (int i = 0; i < rows; i++)
                {
                    string[] parts = lines[i].Split(';');
                    if (parts.Length < cols) continue;

                    for (int j = 0; j < cols; j++)
                    {
                        zooData[i, j] = parts[j].Trim();
                    }
                    dataGridView1.Rows.Add(parts);
                }

                lblStatus.Text = $"Завантажено записів: {rows}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}");
            }
        }


        private void BtnProcess_Click(object sender, EventArgs e)
        {
            if (zooData == null || zooData.GetLength(0) == 0)
            {
                MessageBox.Show("Спочатку зчитайте дані!");
                return;
            }

            try
            {
                StringBuilder resultBuilder = new StringBuilder();
                int foundCount = 0;

                int rows = zooData.GetLength(0);

                // Проходимо по масиву і шукаємо тигрів
                for (int i = 0; i < rows; i++)
                {
                    string animalName = zooData[i, 0].ToLower();

                    if (animalName.Contains("уссурійський тигр") || animalName.Contains("тигр уссурійський"))
                    {
                        foundCount++;
                        string line = "";
                        for (int j = 0; j < zooData.GetLength(1); j++)
                        {
                            line += zooData[i, j] + ";";
                        }
                        resultBuilder.AppendLine(line.TrimEnd(';'));
                    }
                }

                File.WriteAllText(outputFilePath, resultBuilder.ToString(), Encoding.UTF8);

                lblStatus.Text = $"Знайдено та записано: {foundCount}";
                MessageBox.Show($"Дані успішно оброблено!\nЗнайдено записів: {foundCount}\nРезультат збережено у {outputFilePath}", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка запису: {ex.Message}");
            }
        }
    }
}