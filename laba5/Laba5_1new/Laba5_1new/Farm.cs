using System;

namespace Laba5_1new
{
    public class Farm
    {

        private string _farmName;       // Назва ферми
        private string _ownerName;      // Власник
        private double _areaHa;         // Площа в гектарах
        private int _cowCount;          // Кількість корів
        private int _sheepCount;        // Кількість овець
        private int _workerCount;       // Кількість працівників
        private double _budget;         // Бюджет (грн)

        public Farm()
        {
            _farmName = "Невідома ферма";
            _ownerName = "Невідомий";
            _areaHa = 0;
            _cowCount = 0;
            _sheepCount = 0;
            _workerCount = 0;
            _budget = 0;
        }

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
                if (value < 0) _areaHa = 0;
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

        public int CalculateTotalAnimals()
        {
            return _cowCount + _sheepCount;
        }

        public double CalculateAnimalsPerWorker()
        {
            if (_workerCount == 0) return 0;
            return (double)CalculateTotalAnimals() / _workerCount;
        }

        public double CalculateLandTax()
        {
            double taxRatePerHa = 1500.0;
            return _areaHa * taxRatePerHa;
        }

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