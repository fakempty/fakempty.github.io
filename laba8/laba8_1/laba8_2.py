import tkinter as tk
from tkinter import messagebox
from collections import namedtuple

# --- ЕТАП 1: Оголошення іменованого кортежу ---
# Створюємо тип даних "CashInfo" з 6 полями для кожного місяця
CashInfo = namedtuple('CashInfo', ['m1', 'm2', 'm3', 'm4', 'm5', 'm6'])

# --- ЕТАП 2: Функція розрахунку (згідно завдання) ---
def get_contributions(depositor, cash, ratio_productivity):
    """
    depositor: стрічка (Прізвище)
    cash: namedtuple (6 значень вкладів)
    ratio_productivity: число (1-12)
    """
    
    # Н – сума грошових надходжень за останні 6 місяців
    # Оскільки cash - це кортеж, ми можемо використати sum()
    H = sum(cash)
    
    # ГН – середнє арифметичне значення
    GN = H / 6
    
    # Формула: credit = 2/3 * Н + ГН * ratio_productivity
    credit = (2/3 * H) + (GN * ratio_productivity)
    
    return f"Особі {depositor} надається кредит в розмірі {credit:.2f}"

# --- ЕТАП 3: Графічний інтерфейс ---
def on_calculate():
    try:
        # 1. Отримання Прізвища
        name = entry_name.get()
        if not name:
            messagebox.showwarning("Помилка", "Введіть прізвище!")
            return

        # 2. Отримання продуктивності
        try:
            ratio = float(entry_ratio.get())
            if not (1 <= ratio <= 12):
                raise ValueError("Діапазон 1-12")
        except ValueError:
            messagebox.showerror("Помилка", "Продуктивність має бути числом від 1 до 12.")
            return

        # 3. Збирання даних про 6 вкладів
        deposits_list = []
        for entry in entries_months:
            val = float(entry.get())
            if val < 0:
                raise ValueError("Від'ємне значення")
            deposits_list.append(val)
        
        # --- СТВОРЕННЯ namedtuple ---
        # Розпаковуємо список deposits_list у конструктор namedtuple
        cash_tuple = CashInfo(*deposits_list)

        # 4. Виклик функції розрахунку
        result_message = get_contributions(name, cash_tuple, ratio)
        
        # 5. Вивід результату
        lbl_result.config(text=result_message, fg="green")

    except ValueError:
        messagebox.showerror("Помилка", "Перевірте правильність введення сум (мають бути числа).")

# Налаштування головного вікна
root = tk.Tk()
root.title("Лабораторна №8: Кредитний калькулятор")
root.geometry("450x450")

# Блок: Прізвище
tk.Label(root, text="Прізвище вкладника:").pack(pady=(10, 0))
entry_name = tk.Entry(root)
entry_name.pack(pady=5)

# Блок: Продуктивність
tk.Label(root, text="Коефіцієнт продуктивності (1-12):").pack(pady=(10, 0))
entry_ratio = tk.Entry(root)
entry_ratio.pack(pady=5)

# Блок: Вклади за 6 місяців
frame_months = tk.Frame(root)
frame_months.pack(pady=15)
tk.Label(root, text="Вклади за останні 6 місяців:").pack()

entries_months = []
# Створюємо 6 полів введення в циклі
for i in range(6):
    frm = tk.Frame(frame_months)
    frm.pack(pady=2)
    tk.Label(frm, text=f"Місяць {i+1}: ", width=10, anchor='e').pack(side=tk.LEFT)
    en = tk.Entry(frm, width=15)
    en.pack(side=tk.LEFT)
    entries_months.append(en)

# Кнопка
btn_calc = tk.Button(root, text="Розрахувати кредит", command=on_calculate, bg="#ddd", font=("Arial", 10, "bold"))
btn_calc.pack(pady=20)

# Результат
lbl_result = tk.Label(root, text="Очікування даних...", font=("Arial", 10), wraplength=400)
lbl_result.pack(pady=10)

root.mainloop()