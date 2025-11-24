using System;
using System.Collections;
using System.Collections.Generic;

namespace Laba6_3new
{
    // --- 1. Основний клас (IComparable) ---
    public class Worker : IComparable<Worker>
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; set; }

        public Worker(string name, int age, decimal salary)
        {
            Name = name;
            Age = age;
            Salary = salary;
        }

        // Реалізація IComparable:
        // Цей метод викликається автоматично при List.Sort() без аргументів.
        // Ми порівнюємо поточний об'єкт (this) з іншим (other) за ВІКОМ.
        public int CompareTo(Worker other)
        {
            if (other == null) return 1;

            // Порівняння чисел (Age)
            return this.Age.CompareTo(other.Age);
        }

        public override string ToString()
        {
            return $"{Name} | Вік: {Age} | ЗП: {Salary} грн";
        }
    }

    // --- 2. Допоміжний клас для сортування (IComparer) ---
    // Дозволяє сортувати за іншим критерієм (Зарплата)
    public class SalaryComparer : IComparer<Worker>
    {
        public int Compare(Worker x, Worker y)
        {
            if (x == null || y == null) return 0;

            // Спочатку порівнюємо за Зарплатою
            int salaryComparison = x.Salary.CompareTo(y.Salary);

            // Якщо зарплати однакові, порівнюємо за віком (як додаткова умова)
            if (salaryComparison == 0)
            {
                return x.Age.CompareTo(y.Age);
            }

            return salaryComparison;
        }
    }

    // --- 3. Колекція (IEnumerable) ---
    // Дозволяє використовувати foreach для нашого власного класу команди
    public class WorkerTeam : IEnumerable
    {
        // Внутрішній список для зберігання даних
        private List<Worker> _workers = new List<Worker>();

        // Метод для додавання (просто обгортка над List.Add)
        public void Add(Worker w)
        {
            _workers.Add(w);
        }

        // Метод для отримання списку (щоб ми могли його сортувати в GUI)
        public List<Worker> GetList()
        {
            return _workers;
        }

        // Реалізація IEnumerable
        // Це дозволяє писати: foreach (var w in myTeam)
        public IEnumerator GetEnumerator()
        {
            // Повертаємо стандартний нумератор списку
            return _workers.GetEnumerator();

            // АБО (якщо робити вручну через yield return):
            /*
            foreach (var w in _workers)
            {
                yield return w;
            }
            */
        }
    }
}