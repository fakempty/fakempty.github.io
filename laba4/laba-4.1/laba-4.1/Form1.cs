using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace laba_4._1
{
    public partial class Form1 : Form
    {
        List<Zoo> zoos = new List<Zoo>();

        public Form1()
        {
            InitializeComponent();

            // Підключаємо обробник кнопки
            button1.Click += button1_Click;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadData(@"C:\Users\kolia\OneDrive\Документы\123\fakempty.github.io\laba4\laba-4.1\laba-4.1\InputData.txt");
            ShowUssuriTigers();
        }

        private void LoadData(string filePath)
        {
            if (!File.Exists(filePath))
            {
                MessageBox.Show("Файл не знайдено!");
                return;
            }

            zoos.Clear(); // Очищуємо список перед новим завантаженням

            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split(';');
                if (parts.Length < 12) continue;

                zoos.Add(new Zoo
                {
                    AnimalName = parts[0],
                    AnimalCount = int.Parse(parts[1]),
                    PostalCode = parts[2],
                    Country = parts[3],
                    Region = parts[4],
                    District = parts[5],
                    City = parts[6],
                    Street = parts[7],
                    House = parts[8],
                    Apartment = parts[9],
                    TotalAnimals = int.Parse(parts[10]),
                    EmployeesCount = int.Parse(parts[11])
                });
            }
        }

        private void ShowUssuriTigers()
        {
            string result = "";
            bool found = false;

            foreach (var zoo in zoos)
            {
                if (zoo.AnimalName.ToLower().Contains("ussuri") || zoo.AnimalName.ToLower().Contains("уссурійський"))
                {
                    if (!found)
                    {
                        result = "Зоопарки з уссурійськими тиграми:\r\n\r\n"; // Додаємо заголовок тільки один раз
                        found = true;
                    }

                    result += $"Тварина: {zoo.AnimalName}, Видів: {zoo.AnimalCount}, Адреса: {zoo.PostalCode}, {zoo.Country}, {zoo.Region}, {zoo.District}, {zoo.City}, {zoo.Street}, {zoo.House}, {zoo.Apartment}, Тварин: {zoo.TotalAnimals}, Працівників: {zoo.EmployeesCount}\r\n";
                }
            }

            if (!found)
            {
                result = "Не знайдено зоопарки з уссурійськими тиграми";
            }

            textBox1.Text = result;
            File.WriteAllText("OutputData.txt", result);
        }
    }

    // Повний клас Zoo за завданням
    public class Zoo
    {
        public string AnimalName { get; set; }    // Назва тварини
        public int AnimalCount { get; set; }      // Кількість виду
        public string PostalCode { get; set; }    // Поштовий індекс
        public string Country { get; set; }       // Країна
        public string Region { get; set; }        // Область
        public string District { get; set; }      // Район
        public string City { get; set; }          // Місто
        public string Street { get; set; }        // Вулиця
        public string House { get; set; }         // Будинок
        public string Apartment { get; set; }     // Квартира
        public int TotalAnimals { get; set; }     // Загальна кількість тварин
        public int EmployeesCount { get; set; }   // Кількість працівників
    }
}
