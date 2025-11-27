import tkinter as tk
from tkinter import messagebox
from datetime import datetime

class BirthdayCalculator:
    """
    Клас, що виконує роль структури для обробки даних про дату народження.
    Відповідає темі "Масиви структур та тип даних".
    """
    def __init__(self, birth_date_str):
        self.format_str = "%d%m%Y"
        self.birth_date_input = birth_date_str
        self.current_date = datetime.now()

    def get_days_info(self):
        try:
            # Парсинг введеної дати
            dob = datetime.strptime(self.birth_date_input, self.format_str)
        except ValueError:
            return "Помилка", "Невірний формат дати! Використовуйте ДДММРРРР."

        # Визначаємо день народження в поточному році
        try:
            birthday_this_year = datetime(self.current_date.year, dob.month, dob.day)
        except ValueError:
            # Обробка 29 лютого для невисокосних років (переносимо на 1 березня)
            birthday_this_year = datetime(self.current_date.year, 3, 1)

        # Скидаємо час до півночі для коректного розрахунку різниці в днях
        today_midnight = datetime(self.current_date.year, self.current_date.month, self.current_date.day)
        
        delta = birthday_this_year - today_midnight

        if delta.days > 0:
            return "Результат", f"До дня народження залишилося: {delta.days} днів."
        elif delta.days < 0:
            # Якщо дата вже минула, abs(delta.days) покаже скільки днів пройшло
            return "Результат", f"День народження вже минув.\nПройшло днів: {abs(delta.days)}."
        else:
            return "Вітання!", "З Днем Народження! Це сьогодні!"

def on_calculate():
    """Обробник події натискання кнопки"""
    input_date = entry_date.get()
    
    if not input_date:
        messagebox.showwarning("Увага", "Будь ласка, введіть дату.")
        return

    # Створення об'єкту (структури) та виконання розрахунків
    calc = BirthdayCalculator(input_date)
    title, message = calc.get_days_info()
    
    # Виведення результату
    lbl_result.config(text=f"{title}: {message}")
    if title == "Помилка":
        messagebox.showerror(title, message)

# --- Налаштування GUI (tkinter) ---
root = tk.Tk()
root.title("Лабораторна №7: Електронний календар")
root.geometry("400x250")
root.resizable(False, False)

# Заголовок
lbl_instruction = tk.Label(root, text="Введіть дату народження\n(формат ДДММРРРР, напр. 25122000):", font=("Arial", 12))
lbl_instruction.pack(pady=20)

# Поле введення
entry_date = tk.Entry(root, font=("Arial", 14), justify='center')
entry_date.pack(pady=5)

# Кнопка розрахунку
btn_calc = tk.Button(root, text="Розрахувати", command=on_calculate, font=("Arial", 11), bg="#dddddd")
btn_calc.pack(pady=15)

# Поле для виведення результату
lbl_result = tk.Label(root, text="", font=("Arial", 11, "bold"), fg="blue")
lbl_result.pack(pady=10)

# Запуск головного циклу
root.mainloop()