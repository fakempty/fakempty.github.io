from collections import namedtuple

# 1. Оголошення іменованого кортежу для зберігання вкладень за 6 місяців
# Імена полів можуть бути будь-якими, наприклад m1, m2... або month1...
Cash = namedtuple('Cash', ['m1', 'm2', 'm3', 'm4', 'm5', 'm6'])

def get_contributions(depositor: str, cash: tuple, ratio_productivity: int) -> str:
    """
    Розраховує розмір кредиту на основі вкладень та продуктивності.
    
    :param depositor: Прізвище вкладника
    :param cash: іменований кортеж з 6 вкладами
    :param ratio_productivity: коефіцієнт (1-12)
    :return: Форматований рядок з результатом
    """
    
    # Н – сума грошових надходжень за останні 6 місяців
    H = sum(cash)
    
    # ГН – середнє арифметичне значення грошових надходжень
    # Оскільки в кортежі завжди 6 елементів (за умовою)
    GN = H / 6
    
    # Розрахунок кредиту за формулою: credit = 2/3Н + ГН*ratio_productivity
    credit = (2/3 * H) + (GN * ratio_productivity)
    
    # Формування результату з заокругленням до 2 знаків
    return f"Особі {depositor} надається кредит в розмірі {credit:.2f}"

# --- Блок тестування ---
if __name__ == "__main__":
    # Створення тестових даних
    # Приклад: вклади по 1000 грн щомісяця
    my_cash = Cash(1000, 1000, 1000, 1000, 1000, 1000)
    depositor_name = "Петренко"
    productivity = 5  # Наприклад, 5 балів
    
    # Виклик функції
    result = get_contributions(depositor_name, my_cash, productivity)
    
    # Вивід результату
    print(f"Вхідні дані: {my_cash}")
    print(f"Коефіцієнт: {productivity}")
    print("-" * 30)
    print(result)
    
    print("\n" + "-" * 30 + "\n")

    # Приклад 2: Різні суми
    other_cash = Cash(500, 600, 450, 700, 800, 1200)
    print(get_contributions("Іванов", other_cash, 8))