import tkinter as tk
from tkinter import messagebox
from tkinter import filedialog
import os

INPUT_FILE = "InputData.txt"
OUTPUT_FILE = "OutputData.txt"
SESSION_LOG = "SessionLog.txt"

with open(SESSION_LOG, "w", encoding="utf-8") as f:
    f.write("Дія 1: додаток запущено\n")

# --- Глобальні змінні ---
num1 = None
num2 = None
operation = None
action_counter = 1

def log_action(text):
    global action_counter
    with open(SESSION_LOG, "a", encoding="utf-8") as f:
        f.write(f"Дія {action_counter}: {text}\n")
    action_counter += 1

def import_data():
    global num1, num2
    try:
        if not os.path.exists(INPUT_FILE):
            messagebox.showerror("Помилка", "Файл InputData.txt не знайдено!")
            return
        with open(INPUT_FILE, "r", encoding="utf-8") as f:
            line = f.readline()
        if not line.strip():
            messagebox.showerror("Помилка", "Файл порожній, введіть дані")
            return
        parts = line.strip().split()
        if len(parts) != 2:
            messagebox.showerror("Помилка", "Недопустимі значення введених параметрів")
            return
        num1 = float(parts[0])
        num2 = float(parts[1])
        entryNum1.delete(0, tk.END)
        entryNum1.insert(0, str(num1))
        entryNum2.delete(0, tk.END)
        entryNum2.insert(0, str(num2))
        log_action("обрано «Імпортувати вхідні дані»")
    except Exception:
        messagebox.showerror("Помилка", "Недопустимі значення введених параметрів")

def calculate():
    global num1, num2, operation
    try:
        if num1 is None or num2 is None:
            num1 = float(entryNum1.get())
            num2 = float(entryNum2.get())
        operation = operation_var.get()
        result = None
        if operation == "+":
            result = num1 + num2
        elif operation == "-":
            result = num1 - num2
        elif operation == "*":
            result = num1 * num2
        elif operation == "/":
            if num2 == 0:
                messagebox.showerror("Помилка", "Ділення на 0 заборонено")
                return
            result = num1 / num2
        elif operation == "**":
            result = num1 ** num2
        else:
            messagebox.showerror("Помилка", "Оберіть арифметичну операцію")
            return
        labelResult.config(text=f"Результат: {result}")
        log_action(f"обрано арифметичну операцію «{operation}»")
        log_action("обрано «Обчислити вираз»")
    except Exception:
        messagebox.showerror("Помилка", "Недопустимі значення введених параметрів")

def export_result():
    try:
        if labelResult.cget("text") == "":
            messagebox.showerror("Помилка", "Немає результату для експорту")
            return
        with open(OUTPUT_FILE, "a", encoding="utf-8") as f:
            f.write(f"{num1} {operation} {num2}, Результат: {labelResult.cget('text').split(': ')[1]}\n")
        log_action("обрано «Експортувати результат у файл»")
        messagebox.showinfo("Готово", f"Результат збережено у {OUTPUT_FILE}")
    except Exception:
        messagebox.showerror("Помилка", "Не вдалося записати файл")

def on_close():
    log_action("додаток закрито")
    root.destroy()

# --- GUI ---
root = tk.Tk()
root.title("Калькулятор")

tk.Label(root, text="Число 1:").grid(row=0, column=0, padx=5, pady=5)
entryNum1 = tk.Entry(root)
entryNum1.grid(row=0, column=1, padx=5, pady=5)

tk.Label(root, text="Число 2:").grid(row=1, column=0, padx=5, pady=5)
entryNum2 = tk.Entry(root)
entryNum2.grid(row=1, column=1, padx=5, pady=5)

operation_var = tk.StringVar()
operation_var.set(None)

tk.Label(root, text="Операція:").grid(row=2, column=0, padx=5, pady=5)
frame_ops = tk.Frame(root)
frame_ops.grid(row=2, column=1, padx=5, pady=5)

tk.Radiobutton(frame_ops, text="+", variable=operation_var, value="+").pack(side=tk.LEFT)
tk.Radiobutton(frame_ops, text="-", variable=operation_var, value="-").pack(side=tk.LEFT)
tk.Radiobutton(frame_ops, text="*", variable=operation_var, value="*").pack(side=tk.LEFT)
tk.Radiobutton(frame_ops, text="/", variable=operation_var, value="/").pack(side=tk.LEFT)
tk.Radiobutton(frame_ops, text="**", variable=operation_var, value="**").pack(side=tk.LEFT)

buttonImport = tk.Button(root, text="Імпортувати вхідні дані", command=import_data)
buttonImport.grid(row=3, column=0, padx=5, pady=5)

buttonCalculate = tk.Button(root, text="Обчислити вираз", command=calculate)
buttonCalculate.grid(row=3, column=1, padx=5, pady=5)

buttonExport = tk.Button(root, text="Експортувати результат у файл", command=export_result)
buttonExport.grid(row=4, column=0, columnspan=2, padx=5, pady=5)

labelResult = tk.Label(root, text="", fg="blue")
labelResult.grid(row=5, column=0, columnspan=2, padx=5, pady=5)

root.protocol("WM_DELETE_WINDOW", on_close)
root.mainloop()
