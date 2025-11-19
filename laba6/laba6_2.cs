using System;

// ============================
// Інтерфейс: Повітряний транспорт
// ============================
public interface IAirTransport
{
    void Fly();               // Політ
    double MaxSpeed();        // Максимальна швидкість
}

// ============================
// Інтерфейс: Маршрут
// ============================
public interface IRoute
{
    void SetRoute(string start, string end);
    string GetRoute();
}

// ============================
// Клас: Вертоліт
// ============================
public class Helicopter : IAirTransport, IRoute
{
    public string Model { get; set; }
    public int Blades { get; set; }
    private string StartPoint;
    private string EndPoint;

    public Helicopter(string model, int blades)
    {
        Model = model;
        Blades = blades;
    }

    // Реалізація інтерфейсу IAirTransport
    public void Fly()
    {
        Console.WriteLine($"Вертоліт {Model} летить вертикально вверх.");
    }

    public double MaxSpeed()
    {
        return 250; // км/год
    }

    // Реалізація інтерфейсу IRoute
    public void SetRoute(string start, string end)
    {
        StartPoint = start;
        EndPoint = end;
    }

    public string GetRoute()
    {
        return $"Маршрут: {StartPoint} → {EndPoint}";
    }

    // Власні методи
    public void Hover()
    {
        Console.WriteLine("Вертоліт зависає на місці.");
    }

    public void StartEngine()
    {
        Console.WriteLine("Двигун вертольота запущено.");
    }
}

// ============================
// Клас: Літак
// ============================
public class Airplane : IAirTransport, IRoute
{
    public string Model { get; set; }
    public int Engines { get; set; }
    private string StartPoint;
    private string EndPoint;

    public Airplane(string model, int engines)
    {
        Model = model;
        Engines = engines;
    }

    // Реалізація інтерфейсу IAirTransport
    public void Fly()
    {
        Console.WriteLine($"Літак {Model} виконує зліт і набирає висоту.");
    }

    public double MaxSpeed()
    {
        return 900; // км/год
    }

    // Реалізація інтерфейсу IRoute
    public void SetRoute(string start, string end)
    {
        StartPoint = start;
        EndPoint = end;
    }

    public string GetRoute()
    {
        return $"Маршрут: {StartPoint} → {EndPoint}";
    }

    // Власні методи
    public void Landing()
    {
        Console.WriteLine("Літак виконує посадку.");
    }

    public void CheckSystems()
    {
        Console.WriteLine("Виконується перевірка всіх систем літака.");
    }
}

// ============================
// Тестовий Main
// ============================
public class Program
{
    public static void Main()
    {
        Helicopter h = new Helicopter("Mi-8", 5);
        h.SetRoute("Київ", "Львів");
        h.Fly();
        Console.WriteLine(h.GetRoute());
        h.Hover();

        Console.WriteLine();

        Airplane a = new Airplane("Boeing 737", 2);
        a.SetRoute("Одеса", "Харків");
        a.Fly();
        Console.WriteLine(a.GetRoute());
        a.CheckSystems();
    }
}
