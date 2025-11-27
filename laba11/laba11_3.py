from typing import Iterator

def float_range(start: float, stop: float, step: float) -> Iterator[float]:
    """
    Генераторна функція, яка повертає послідовність дійсних чисел
    від start до stop (не включаючи) з кроком step.
    """
    # 1. Перевірка на нульовий крок
    if step == 0:
        raise ValueError("Step cannot be zero")

    current = start
    
    # Визначаємо точність для заокруглення, щоб уникнути артефактів типу 0.300000000004
    # Зазвичай 10 знаків після коми достатньо для стандартних задач
    precision = 10

    # 2. Основний цикл генерації
    while True:
        # Умова зупинки для додатного кроку
        if step > 0 and current >= stop:
            break
        # Умова зупинки для від'ємного кроку
        elif step < 0 and current <= stop:
            break
            
        yield current
        
        # Оновлення поточного значення з заокругленням
        current += step
        current = round(current, precision)

if __name__ == "__main__":
    print("Приклад 1 (1.0 -> 2.0, крок 0.3):")
    print(list(float_range(1.0, 2.0, 0.3)))  
    # Очікується: [1.0, 1.3, 1.6, 1.9]

    print("\nПриклад 2 (5.0 -> 3.0, крок -0.5):")
    print(list(float_range(5.0, 3.0, -0.5))) 
    # Очікується: [5.0, 4.5, 4.0, 3.5]

    print("\nПриклад 3 (нескінченний ряд 0.0 -> 1.0, беремо перші 3):")
    # Тут stop=1.0, крок 0.1, воно скінченне, але демонстрація зрізу
    print(list(float_range(0.0, 1.0, 0.1))[:3])
    # Очікується: [0.0, 0.1, 0.2]

    print("\nПриклад 4 (start == stop):")
    print(list(float_range(0.0, 0.0, 1.0))) 
    # Очікується: []

    print("\nПриклад 5 (недосяжна умова, 1.0 -> 2.0 з мінусовим кроком):")
    print(list(float_range(1.0, 2.0, -1.0))) 
    # Очікується: []