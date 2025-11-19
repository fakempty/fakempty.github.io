using System;
using System.IO;
using System.Windows.Forms;

public class Farm
{
    // --- Приватні поля ---
    private string name;            // Назва ферми
    private int animalsCount;       // Кількість тварин
    private double landArea;        // Площа землі (га)
    private string owner;           // Власник
    private double milkPerDay;      // Літрів молока за добу
    private double eggsPerDay;      // Яєць за добу
    private int workers;            // Кількість працівників

    // --- Властивості ---
    public string Name { get => name; set => name = value; }
    public int AnimalsCount { get => animalsCount; set => animalsCount = value; }
    public double LandArea { get => landArea; set => landArea = value; }
    public string Owner { get => owner; set => owner = value; }
    public double MilkPerDay { get => milkPerDay; set => milkPerDay = value; }
    public double EggsPerDay { get => eggsPerDay; set => eggsPerDay = value; }
    public int Workers { get => workers; set => workers = value; }

    // --- Конструктор без параметрів ---
    public Farm()
    {
        name = "NoName Farm";
        animalsCount = 0;
        landArea = 0;
        owner = "Unknown";
        milkPerDay = 0;
        eggsPerDay = 0;
        workers = 0;
    }

    // --- Методи класу ---
    // 1. Продуктивність ферми за день
    public double TotalProduction()
    {
        return milkPerDay * 1.0 + eggsPerDay * 1.0;
    }

    // 2. Продуктивність на одного працівника
    public double ProductivityPerWorker()
    {
        if (workers == 0) return 0;
        return TotalProduction() / workers;
    }

    // 3. Щільність тварин на гектар
    public double AnimalsDensity()
    {
        if (landArea == 0) return 0;
        return animalsCount / landArea;
    }
}


namespace FarmApp

{
    public partial class Form1 : Form
    {
        private Farm farm;

        public Form1()
        {
            InitializeComponent();
            farm = new Farm();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Заповнюємо об’єкт
            farm.Name = txtName.Text;
            farm.Owner = txtOwner.Text;
            farm.AnimalsCount = int.Parse(txtAnimals.Text);
            farm.Workers = int.Parse(txtWorkers.Text);
            farm.LandArea = double.Parse(txtLand.Text);
            farm.MilkPerDay = double.Parse(txtMilk.Text);
            farm.EggsPerDay = double.Parse(txtEggs.Text);

            // Запис у файл
            File.WriteAllText("farm.txt",
                $"Ферма: {farm.Name}\n" +
                $"Власник: {farm.Owner}\n" +
                $"Тварин: {farm.AnimalsCount}\n" +
                $"Працівників: {farm.Workers}\n" +
                $"Площа: {farm.LandArea}\n" +
                $"Молоко/день: {farm.MilkPerDay}\n" +
                $"Яйця/день: {farm.EggsPerDay}"
            );

            MessageBox.Show("Дані збережено.");
        }

        private void btnShowMethods_Click(object sender, EventArgs e)
        {
            string msg =
                $"Загальна продуктивність: {farm.TotalProduction()}\n" +
                $"Продуктивність на працівника: {farm.ProductivityPerWorker()}\n" +
                $"Щільність тварин: {farm.AnimalsDensity()} тварин/га";

            MessageBox.Show(msg);
        }
    }
}
