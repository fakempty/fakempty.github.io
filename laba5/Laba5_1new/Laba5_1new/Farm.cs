using System;

namespace Laba5_1new
{
    // Клас, що описує Ферму
    public class Farm
    {
        // --- 1. ПРИВАТНІ ПОЛЯ (не менше 7) ---
        // Вони недоступні ззовні класу (Інкапсуляція)
        private string _farmName;       // Назва ферми
        private string _ownerName;      // Власник
        private double _areaHa;         // Площа в гектарах
        private int _cowCount;          // Кількість корів
        private int _sheepCount;        // Кількість овець
        private int _workerCount;       // Кількість працівників
        private double _budget;         // Бюджет (грн)

        // --- 2. КОНСТРУКТОР БЕЗ ПАРАМЕТРІВ ---
        public Farm()
        {
            // Ініціалізація значень за замовчуванням
            _farmName = "Невідома ферма";
            _ownerName = "Невідомий";
            _areaHa = 0;
            _cowCount = 0;
            _sheepCount = 0;
            _workerCount = 0;
            _budget = 0;
        }

        // --- 3. ВЛАСТИВОСТІ (Properties) ---
        // Забезпечують доступ до приватних полів (get/set)
        public string FarmName
        {
            get { return _farmName; }
            set { _farmName = value; }
        }

        public string OwnerName
        {
            get { return _ownerName; }
            set { _ownerName = value; }
        }

        public double AreaHa
        {
            get { return _areaHa; }
            set
            {
                if (value < 0) _areaHa = 0; // Перевірка на коректність
                else _areaHa = value;
            }
        }

        public int CowCount
        {
            get { return _cowCount; }
            set { _cowCount = (value < 0) ? 0 : value; }
        }

        public int SheepCount
        {
            get { return _sheepCount; }
            set { _sheepCount = (value < 0) ? 0 : value; }
        }

        public int WorkerCount
        {
            get { return _workerCount; }
            set { _workerCount = (value < 0) ? 0 : value; }
        }

        public double Budget
        {
            get { return _budget; }
            set { _budget = value; }
        }

        // --- 4. МЕТОДИ КЛАСУ (не менше 3-х) ---

        // Метод 1: Підрахунок загальної кількості тварин
        public int CalculateTotalAnimals()
        {
            return _cowCount + _sheepCount;
        }

        // Метод 2: Розрахунок навантаження на одного працівника (тварин на людину)
        public double CalculateAnimalsPerWorker()
        {
            if (_workerCount == 0) return 0;
            return (double)CalculateTotalAnimals() / _workerCount;
        }

        // Метод 3: Прогноз податку на землю (наприклад, 1500 грн за гектар)
        public double CalculateLandTax()
        {
            double taxRatePerHa = 1500.0;
            return _areaHa * taxRatePerHa;
        }

        // Додатковий метод для формування рядка інформації (для запису у файл)
        public string GetInfoString()
        {
            return $"Ферма: {_farmName}\n" +
                   $"Власник: {_ownerName}\n" +
                   $"Площа: {_areaHa} га\n" +
                   $"Корів: {_cowCount}, Овець: {_sheepCount}\n" +
                   $"Працівників: {_workerCount}\n" +
                   $"Бюджет: {_budget} грн";
        }
    }
}