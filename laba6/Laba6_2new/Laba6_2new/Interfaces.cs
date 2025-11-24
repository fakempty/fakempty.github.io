using System;

namespace Laba6_2new
{
    // Інтерфейс 1: Повітряний транспорт
    // Вимагає реалізації методів польоту
    public interface IAirTransport
    {
        string Fly();   // Абстрактний метод 1
        string Land();  // Абстрактний метод 2
    }

    // Інтерфейс 2: Маршрут
    // Вимагає реалізації методів навігації
    public interface IRoute
    {
        void SetRoute(string from, string to); // Абстрактний метод 1
        string GetEstimatedTime();             // Абстрактний метод 2
    }
}