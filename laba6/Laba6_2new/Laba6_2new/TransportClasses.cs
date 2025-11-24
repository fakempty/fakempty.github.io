using System;

namespace Laba6_2new
{
    // --- КЛАС 1: ВЕРТОЛІТ ---
    // Реалізує обидва інтерфейси
    public class Helicopter : IAirTransport, IRoute
    {
        // Поля
        public string Model { get; set; }
        public int RotorCount { get; set; }

        private string _routeInfo = "Маршрут не задано";

        // Реалізація методів інтерфейсу IAirTransport
        public string Fly()
        {
            return $"Вертоліт {Model} розкручує {RotorCount} гвинтів і вертикально злітає.";
        }

        public string Land()
        {
            return $"Вертоліт {Model} повільно сідає на майданчик.";
        }

        // Реалізація методів інтерфейсу IRoute
        public void SetRoute(string from, string to)
        {
            _routeInfo = $"Вертоліт летить: {from} -> {to} (низька висота)";
        }

        public string GetEstimatedTime()
        {
            return $"{_routeInfo}. Час у дорозі: 2 години.";
        }

        // --- Унікальні методи Вертольота ---
        public string Hover()
        {
            return "УВАГА: Вертоліт завис у повітрі на одному місці!";
        }

        public string RescueMission()
        {
            return "МІСІЯ: Вертоліт спускає рятувальний трос.";
        }
    }

    // --- КЛАС 2: ЛІТАК ---
    // Реалізує обидва інтерфейси
    public class Plane : IAirTransport, IRoute
    {
        // Поля
        public string Model { get; set; }
        public int JetEngines { get; set; }

        private string _routeInfo = "Маршрут не задано";

        // Реалізація IAirTransport
        public string Fly()
        {
            return $"Літак {Model} вмикає {JetEngines} турбін і розганяється по смузі.";
        }

        public string Land()
        {
            return $"Літак {Model} випускає шасі і торкається смуги.";
        }

        // Реалізація IRoute
        public void SetRoute(string from, string to)
        {
            _routeInfo = $"Рейс літака: {from} -> {to} (ешелон 10 000м)";
        }

        public string GetEstimatedTime()
        {
            return $"{_routeInfo}. Час у дорозі: 45 хвилин.";
        }

        // --- Унікальні методи Літака ---
        public string RetractLandingGear()
        {
            return "ДІЯ: Шасі прибрано в корпус літака.";
        }

        public string SupersonicBoost()
        {
            return "ФОРСАЖ: Літак переходить на надзвукову швидкість!";
        }
    }
}