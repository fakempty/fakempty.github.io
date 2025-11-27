class Countdown:
    """
    Клас-ітератор, який генерує числа у зворотному порядку 
    від вказаного числа start до 0 включно.
    """
    def __init__(self, start: int):
        # Ініціалізуємо поточне значення
        self.current = start

    def __iter__(self):
        # Протокол ітератора вимагає, щоб цей метод повертав сам об'єкт ітератора
        return self

    def __next__(self):
        # Перевірка умови зупинки: якщо число менше 0, ітерація завершується
        if self.current < 0:
            raise StopIteration
        
        # Зберігаємо поточне значення, щоб повернути його
        value_to_return = self.current
        
        # Зменшуємо лічильник для наступного кроку
        self.current -= 1
        
        return value_to_return

# --- Перевірка роботи класу (згідно з прикладом у завданні) ---
if __name__ == "__main__":
    print("Loop output:")
    c = Countdown(5)
    for number in c:
        print(number, end=" ") # 5 4 3 2 1 0
    print("\n")

    print(f"list(Countdown(3)): {list(Countdown(3))}")   # [3, 2, 1, 0]
    print(f"list(Countdown(0)): {list(Countdown(0))}")   # [0]
    print(f"list(Countdown(-3)): {list(Countdown(-3))}") # []