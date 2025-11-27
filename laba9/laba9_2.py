import tkinter as tk
from tkinter import messagebox, filedialog
import random
import os

class StudentTransferApp:
    def __init__(self, root):
        self.root = root
        self.root.title("Лаб 9: Кільцеві списки (Завдання 1.2)")
        self.root.geometry("600x550")
        
        # Дані списків
        self.group1 = []
        self.group2 = []

        # --- Елементи GUI ---
        
        # Заголовки
        tk.Label(root, text="Група 1", font=("Arial", 12, "bold")).place(x=50, y=10)
        tk.Label(root, text="Група 2", font=("Arial", 12, "bold")).place(x=350, y=10)

        # Списки (Listbox)
        self.lb_group1 = tk.Listbox(root, width=30, height=15)
        self.lb_group1.place(x=20, y=40)
        
        self.lb_group2 = tk.Listbox(root, width=30, height=15)
        self.lb_group2.place(x=320, y=40)

        # Кнопка завантаження
        self.btn_load = tk.Button(root, text="1. Завантажити з файлів", command=self.load_files, bg="#dddddd")
        self.btn_load.place(x=20, y=300, width=550, height=30)

        # Поле введення L
        tk.Label(root, text="Кількість студентів для переводу (L):").place(x=20, y=350)
        self.entry_L = tk.Entry(root, width=10)
        self.entry_L.place(x=250, y=350)

        # Кнопка переводу
        self.btn_transfer = tk.Button(root, text="2. Перевести L студентів (випадковий старт)", 
                                      command=self.transfer_students, bg="lightblue")
        self.btn_transfer.place(x=20, y=390, width=550, height=40)

        # Кнопка збереження
        self.btn_save = tk.Button(root, text="3. Зберегти у файли", command=self.save_files, bg="lightgreen")
        self.btn_save.place(x=20, y=440, width=550, height=30)

        # Лог дій
        self.lbl_status = tk.Label(root, text="Очікування...", fg="gray", wraplength=550, justify="left")
        self.lbl_status.place(x=20, y=480)

        # Автоматичне створення тестових файлів, якщо їх немає
        self.create_dummy_files()

    def create_dummy_files(self):
        """Створює файли для тесту, якщо вони відсутні"""
        if not os.path.exists("group1.txt"):
            with open("group1.txt", "w", encoding="utf-8") as f:
                f.write("\n".join(["Іванов", "Петров", "Сидоров", "Коваленко", "Бондаренко", "Ткаченко", "Шевченко"]))
        if not os.path.exists("group2.txt"):
            with open("group2.txt", "w", encoding="utf-8") as f:
                f.write("\n".join(["Сміт", "Джонсон", "Вільямс"]))

    def load_files(self):
        try:
            # Зчитуємо Групу 1
            if os.path.exists("group1.txt"):
                with open("group1.txt", "r", encoding="utf-8") as f:
                    self.group1 = [line.strip() for line in f if line.strip()]
            
            # Зчитуємо Групу 2
            if os.path.exists("group2.txt"):
                with open("group2.txt", "r", encoding="utf-8") as f:
                    self.group2 = [line.strip() for line in f if line.strip()]
            
            self.update_listboxes()
            self.lbl_status.config(text="Файли завантажено успішно.", fg="green")
            
        except Exception as e:
            messagebox.showerror("Помилка", f"Не вдалося зчитати файли: {e}")

    def update_listboxes(self):
        self.lb_group1.delete(0, tk.END)
        for student in self.group1:
            self.lb_group1.insert(tk.END, student)
            
        self.lb_group2.delete(0, tk.END)
        for student in self.group2:
            self.lb_group2.insert(tk.END, student)

    def transfer_students(self):
        if not self.group1:
            messagebox.showwarning("Увага", "Група 1 порожня!")
            return

        try:
            L = int(self.entry_L.get())
            if L <= 0:
                raise ValueError("Число має бути більше 0")
            if L > len(self.group1):
                raise ValueError(f"У групі 1 всього {len(self.group1)} студентів. Неможливо взяти {L}.")
        except ValueError as ve:
            messagebox.showerror("Помилка введення", f"Некоректне значення L: {ve}")
            return

        # --- Логіка Кільцевого Списку ---
        
        # 1. Випадковий індекс k (від 0 до довжини списку - 1)
        k = random.randint(0, len(self.group1) - 1)
        start_student = self.group1[k]

        # 2. Щоб імітувати кільце і взяти L студентів починаючи з k,
        # найпростіше "покрутити" список так, щоб k-й елемент став першим.
        # Це робиться за допомогою зрізів:
        # Новий порядок: [k, k+1, ... кінець, початок ... k-1]
        group1_rotated = self.group1[k:] + self.group1[:k]

        # 3. Відрізаємо перші L студентів (ті, що переїжджають)
        moving_students = group1_rotated[:L]
        remaining_students = group1_rotated[L:]

        # 4. Оновлюємо списки
        self.group1 = remaining_students
        self.group2.extend(moving_students)

        # Оновлення GUI
        self.update_listboxes()
        
        # Вивід інформації про подію
        log_msg = (f"Випадковий індекс k = {k} (Студент: {start_student}).\n"
                   f"Переведено {L} студентів: {', '.join(moving_students)}")
        self.lbl_status.config(text=log_msg, fg="blue")
        messagebox.showinfo("Успіх", "Переведення завершено!")

    def save_files(self):
        try:
            with open("group1_updated.txt", "w", encoding="utf-8") as f:
                f.write("\n".join(self.group1))
            
            with open("group2_updated.txt", "w", encoding="utf-8") as f:
                f.write("\n".join(self.group2))
                
            self.lbl_status.config(text="Списки збережено у файли 'group1_updated.txt' та 'group2_updated.txt'", fg="green")
            messagebox.showinfo("Збережено", "Файли успішно записані!")
        except Exception as e:
            messagebox.showerror("Помилка", f"Помилка запису: {e}")

if __name__ == "__main__":
    root = tk.Tk()
    app = StudentTransferApp(root)
    root.mainloop()