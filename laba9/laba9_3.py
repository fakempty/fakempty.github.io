import tkinter as tk
from tkinter import messagebox, filedialog
import os

class BracketCheckerApp:
    def __init__(self, root):
        self.root = root
        self.root.title("Лаб 9.3: Перевірка дужок (Стек)")
        self.root.geometry("600x450")

        # Змінна для збереження поточного тексту виразу
        self.expression = ""

        # --- Елементи GUI ---
        
        # Заголовок та інструкція
        tk.Label(root, text="Перевірка збалансованості круглих дужок '()'", font=("Arial", 12, "bold")).pack(pady=10)
        
        # Поле відображення виразу
        tk.Label(root, text="Вхідний вираз:").pack(anchor="w", padx=20)
        self.txt_expression = tk.Text(root, height=3, width=65, bg="#f0f0f0", font=("Consolas", 11))
        self.txt_expression.pack(padx=20, pady=5)

        # Кнопки
        frame_btns = tk.Frame(root)
        frame_btns.pack(pady=10)

        btn_load = tk.Button(frame_btns, text="1. Завантажити файл", command=self.load_file, bg="#dddddd", width=20)
        btn_load.pack(side=tk.LEFT, padx=10)

        btn_check = tk.Button(frame_btns, text="2. Перевірити баланс", command=self.check_balance, bg="lightblue", width=20)
        btn_check.pack(side=tk.LEFT, padx=10)

        # Поле результату
        tk.Label(root, text="Результат аналізу:").pack(anchor="w", padx=20)
        self.txt_result = tk.Text(root, height=10, width=65, bg="white", font=("Consolas", 10))
        self.txt_result.pack(padx=20, pady=5)

        # Створення тестового файлу при запуску
        self.create_test_file()

    def create_test_file(self):
        """Створює файл із прикладом з умови завдання, якщо файлу немає."""
        filename = "math_expr.txt"
        # Вираз з умови завдання: ((a+2)-4*(a-3)/(2-7+6) - тут не вистачає дужок
        example_expr = "((a+2)-4*(a-3)/(2-7+6)" 
        
        if not os.path.exists(filename):
            try:
                with open(filename, "w", encoding="utf-8") as f:
                    f.write(example_expr)
                self.log(f"Створено тестовий файл '{filename}' з виразом:\n{example_expr}\n(Спробуйте завантажити його)")
            except Exception as e:
                self.log(f"Помилка створення файлу: {e}")

    def load_file(self):
        file_path = filedialog.askopenfilename(filetypes=[("Text Files", "*.txt"), ("All Files", "*.*")])
        if file_path:
            try:
                with open(file_path, "r", encoding="utf-8") as f:
                    self.expression = f.read().strip()
                
                self.txt_expression.delete(1.0, tk.END)
                self.txt_expression.insert(tk.END, self.expression)
                self.log("Файл завантажено. Натисніть 'Перевірити баланс'.")
            except Exception as e:
                messagebox.showerror("Помилка", f"Не вдалося відкрити файл: {e}")

    def check_balance(self):
        if not self.expression:
            messagebox.showwarning("Увага", "Спочатку завантажте файл або введіть вираз!")
            return

        # --- РЕАЛІЗАЦІЯ СТЕКУ НА МАСИВІ ---
        stack = []   # Одновимірний масив (список) для зберігання індексів '('
        pairs = []   # Список для зберігання знайдених пар (відкрита, закрита)
        
        is_error = False
        error_msg = ""

        # Проходимо по виразу зліва направо
        for i, char in enumerate(self.expression):
            if char == '(':
                # Вставка елемента в стек (push) - зберігаємо індекс
                stack.append(i)
            
            elif char == ')':
                # Перевірка на порожнечу (чи є пара для цієї дужки?)
                if len(stack) == 0:
                    is_error = True
                    error_msg = f"Помилка: зайва закриваюча дужка ')' на позиції {i}."
                    break
                else:
                    # Витягування елемента зі стеку (pop)
                    open_index = stack.pop()
                    # Запам'ятовуємо пару. 
                    # Оскільки ми йдемо зліва направо, пари автоматично додаються 
                    # у порядку зростання номерів дужок, що закриваються.
                    pairs.append((open_index, i))

        # Перевірка після циклу: стек має бути порожнім
        if not is_error and len(stack) > 0:
            is_error = True
            # У стеці залишилися індекси відкритих дужок, яким не знайшлося пари
            unmatched_indices = ", ".join(map(str, stack))
            error_msg = f"Помилка: не вистачає закриваючих дужок.\nЗайві відкриваючі дужки '(' на позиціях: {unmatched_indices}"

        # --- Виведення результату ---
        self.txt_result.delete(1.0, tk.END)
        
        if is_error:
            self.txt_result.config(fg="red")
            self.txt_result.insert(tk.END, "ДУЖКИ НЕ ЗБАЛАНСОВАНІ!\n")
            self.txt_result.insert(tk.END, error_msg)
        else:
            self.txt_result.config(fg="blue")
            self.txt_result.insert(tk.END, "ДУЖКИ ЗБАЛАНСОВАНІ.\n\n")
            self.txt_result.insert(tk.END, "Пари дужок (індекс відкриття -> індекс закриття):\n")
            self.txt_result.insert(tk.END, "-" * 50 + "\n")
            
            # Виводимо пари. Вони вже відсортовані по закриваючій дужці (i) завдяки логіці циклу
            for open_idx, close_idx in pairs:
                self.txt_result.insert(tk.END, f"Позиція {open_idx} <---> Позиція {close_idx}\n")

    def log(self, message):
        self.txt_result.delete(1.0, tk.END)
        self.txt_result.insert(tk.END, message)

if __name__ == "__main__":
    root = tk.Tk()
    app = BracketCheckerApp(root)
    root.mainloop()