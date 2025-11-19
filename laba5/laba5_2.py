# lab5_oop.py
# Лабораторна робота №5 — ООП (Python + Tkinter)
# Варіант: Тест, Іспит, Випускний іспит, Випробування
# Автор у деструкторі: Кадюк Микола (студент 2 курсу)

import tkinter as tk
from tkinter import ttk, messagebox, scrolledtext
import gc
from datetime import datetime

# -------------------------
# Класи предметної області
# -------------------------
class Assessment:
    """Базовий клас для оцінювання/перевірок"""
    def __init__(self, title="Unnamed", max_score=100, date=None):
        self._title = title
        self._max_score = max_score
        self._date = date if date is not None else datetime.today().strftime("%Y-%m-%d")
    
    # Альтернативний конструктор (імітація перевантаження)
    @classmethod
    def from_params(cls, title, max_score, date):
        return cls(title, max_score, date)
    
    def evaluate(self, score):
        """Базова оцінка — повертає відсоток від max_score"""
        if self._max_score <= 0:
            return 0.0
        return (score / self._max_score) * 100.0
    
    def show(self):
        return f"Assessment: {self._title}, Max: {self._max_score}, Date: {self._date}"
    
    def __del__(self):
        # Відповідно до вимоги виводимо повідомлення в консоль
        print("Лабораторна робота виконанна студентом 2 курсу Кадюк Микола")

# Похідний клас Test
class Test(Assessment):
    def __init__(self, title="Test", max_score=50, date=None, duration_min=30, num_questions=20, passing_score=30):
        super().__init__(title, max_score, date)
        self.duration_min = duration_min
        self.num_questions = num_questions
        self.passing_score = passing_score
    
    @classmethod
    def from_params(cls, title, max_score, date, duration_min, num_questions, passing_score):
        return cls(title, max_score, date, duration_min, num_questions, passing_score)
    
    def evaluate(self, score):
        pct = super().evaluate(score)
        passed = score >= self.passing_score
        return {"percent": pct, "passed": passed}
    
    def show(self):
        base = super().show()
        return f"{base}\nType: Test | Duration: {self.duration_min} min | Q: {self.num_questions} | Passing: {self.passing_score}"

# Похідний клас Exam
class Exam(Assessment):
    def __init__(self, title="Exam", max_score=100, date=None, examiner="Unknown", room="101", oral=False):
        super().__init__(title, max_score, date)
        self.examiner = examiner
        self.room = room
        self.oral = oral
    
    @classmethod
    def from_params(cls, title, max_score, date, examiner, room, oral):
        return cls(title, max_score, date, examiner, room, oral)
    
    def evaluate(self, score, bonus=0):
        # Exam може мати бонусні бали
        total = min(score + bonus, self._max_score)
        pct = super().evaluate(total)
        passed = pct >= 50.0
        return {"percent": pct, "passed": passed, "with_bonus": bonus}
    
    def show(self):
        base = super().show()
        return f"{base}\nType: Exam | Examiner: {self.examiner} | Room: {self.room} | Oral: {self.oral}"

# Похідний клас FinalExam (Випускний іспит)
class FinalExam(Exam):
    def __init__(self, title="Final Exam", max_score=200, date=None, examiner="Committee", room="Main Hall", oral=True, thesis_required=True, diploma_recommendation=False):
        super().__init__(title, max_score, date, examiner, room, oral)
        self.thesis_required = thesis_required
        self.diploma_recommendation = diploma_recommendation
    
    @classmethod
    def from_params(cls, title, max_score, date, examiner, room, oral, thesis_required, diploma_recommendation):
        return cls(title, max_score, date, examiner, room, oral, thesis_required, diploma_recommendation)
    
    def evaluate(self, score, thesis_score=0):
        # Final exam: враховується захист диплома (thesis_score)
        total = min(score + thesis_score, self._max_score)
        pct = super(Exam, self).evaluate(total)  # використовуємо Assessment.evaluate напряму
        passed = (total >= (0.6 * self._max_score)) and (thesis_score >= 20 or not self.thesis_required)
        return {"percent": pct, "passed": passed, "thesis": thesis_score}
    
    def show(self):
        base = super().show()
        return f"{base}\nType: FinalExam | Thesis required: {self.thesis_required} | Diploma rec.: {self.diploma_recommendation}"

# Похідний клас Trial (Випробування)
class Trial(Assessment):
    def __init__(self, title="Trial", max_score=30, date=None, difficulty="Medium", attempts_allowed=1, supervisor="TBD"):
        super().__init__(title, max_score, date)
        self.difficulty = difficulty
        self.attempts_allowed = attempts_allowed
        self.supervisor = supervisor
    
    @classmethod
    def from_params(cls, title, max_score, date, difficulty, attempts_allowed, supervisor):
        return cls(title, max_score, date, difficulty, attempts_allowed, supervisor)
    
    def evaluate(self, score):
        pct = super().evaluate(score)
        success = pct >= 70.0
        return {"percent": pct, "success": success}
    
    def show(self):
        base = super().show()
        return f"{base}\nType: Trial | Difficulty: {self.difficulty} | Attempts: {self.attempts_allowed} | Supervisor: {self.supervisor}"

# -----------------------------------------
# GUI: Tkinter — мінімальний функціонал
# -----------------------------------------
class App:
    def __init__(self, root):
        self.root = root
        root.title("Лабораторна 5 — ООП (Python)")
        self.obj = None  # поточний об'єкт
        
        main = ttk.Frame(root, padding=10)
        main.grid(row=0, column=0, sticky="nsew")
        
        # Тип об'єкта
        ttk.Label(main, text="Тип:").grid(row=0, column=0, sticky="w")
        self.type_var = tk.StringVar(value="Test")
        self.type_combo = ttk.Combobox(main, textvariable=self.type_var, values=["Test","Exam","FinalExam","Trial"], state="readonly", width=15)
        self.type_combo.grid(row=0, column=1, sticky="w")
        self.type_combo.bind("<<ComboboxSelected>>", lambda e: self.update_fields())
        
        # Загальні поля
        ttk.Label(main, text="Title:").grid(row=1, column=0, sticky="w")
        self.title_entry = ttk.Entry(main, width=30); self.title_entry.grid(row=1, column=1, sticky="w")
        self.title_entry.insert(0, "Sample")
        
        ttk.Label(main, text="Max score:").grid(row=2, column=0, sticky="w")
        self.max_entry = ttk.Entry(main, width=10); self.max_entry.grid(row=2, column=1, sticky="w")
        self.max_entry.insert(0, "100")
        
        ttk.Label(main, text="Date (YYYY-MM-DD):").grid(row=3, column=0, sticky="w")
        self.date_entry = ttk.Entry(main, width=15); self.date_entry.grid(row=3, column=1, sticky="w")
        self.date_entry.insert(0, datetime.today().strftime("%Y-%m-%d"))
        
        # Специфічні поля (змінюються залежно від типу)
        self.spec_frame = ttk.LabelFrame(main, text="Специфічні поля")
        self.spec_frame.grid(row=4, column=0, columnspan=2, pady=8, sticky="ew")
        # ми додамо динамічно
        self.spec_widgets = {}
        self.update_fields()
        
        # Кнопки
        btn_frame = ttk.Frame(main)
        btn_frame.grid(row=5, column=0, columnspan=2, pady=6)
        ttk.Button(btn_frame, text="Створити об'єкт", command=self.create_object).grid(row=0, column=0, padx=4)
        ttk.Button(btn_frame, text="Показати (Show)", command=self.show_object).grid(row=0, column=1, padx=4)
        ttk.Button(btn_frame, text="Оцінити (Evaluate)", command=self.evaluate_object).grid(row=0, column=2, padx=4)
        ttk.Button(btn_frame, text="Видалити об'єкт", command=self.delete_object).grid(row=0, column=3, padx=4)
        
        # Вивід
        self.output = scrolledtext.ScrolledText(main, width=60, height=12, wrap=tk.WORD)
        self.output.grid(row=6, column=0, columnspan=2, pady=6)
    
    def clear_spec(self):
        for w in self.spec_widgets.values():
            w.destroy()
        self.spec_widgets = {}
    
    def update_fields(self):
        typ = self.type_var.get()
        self.clear_spec()
        r = 0
        def add_label_entry(key, label, default=""):
            nonlocal r
            ttk.Label(self.spec_frame, text=label).grid(row=r, column=0, sticky="w", padx=4, pady=2)
            ent = ttk.Entry(self.spec_frame, width=25)
            ent.grid(row=r, column=1, sticky="w", padx=4, pady=2)
            ent.insert(0, default)
            self.spec_widgets[key] = ent
            r += 1
        
        if typ == "Test":
            add_label_entry("duration_min", "Duration (min):", "30")
            add_label_entry("num_questions", "Number of questions:", "20")
            add_label_entry("passing_score", "Passing score:", "15")
        elif typ == "Exam":
            add_label_entry("examiner", "Examiner:", "Dr. Ivanov")
            add_label_entry("room", "Room:", "201")
            add_label_entry("oral", "Oral (True/False):", "False")
            add_label_entry("bonus", "Bonus (for evaluate):", "0")
        elif typ == "FinalExam":
            add_label_entry("examiner", "Examiner:", "Committee")
            add_label_entry("room", "Room:", "Main Hall")
            add_label_entry("thesis_required", "Thesis required (True/False):", "True")
            add_label_entry("diploma", "Diploma recommendation (True/False):", "False")
            add_label_entry("thesis_score", "Thesis score (for evaluate):", "0")
        elif typ == "Trial":
            add_label_entry("difficulty", "Difficulty (Low/Medium/High):", "Medium")
            add_label_entry("attempts_allowed", "Attempts allowed:", "1")
            add_label_entry("supervisor", "Supervisor:", "TBD")
    
    def create_object(self):
        typ = self.type_var.get()
        title = self.title_entry.get()
        try:
            max_score = float(self.max_entry.get())
        except ValueError:
            messagebox.showerror("Error", "Max score must be a number")
            return
        date = self.date_entry.get()
        # Просте створення об'єкта на основі типу
        if typ == "Test":
            try:
                dur = int(self.spec_widgets["duration_min"].get())
                nq = int(self.spec_widgets["num_questions"].get())
                ps = float(self.spec_widgets["passing_score"].get())
            except Exception:
                messagebox.showerror("Error", "Invalid Test fields")
                return
            self.obj = Test.from_params(title, max_score, date, dur, nq, ps)
        elif typ == "Exam":
            examiner = self.spec_widgets["examiner"].get()
            room = self.spec_widgets["room"].get()
            oral = self.spec_widgets["oral"].get().lower() in ("true","1","yes","y")
            self.obj = Exam.from_params(title, max_score, date, examiner, room, oral)
        elif typ == "FinalExam":
            examiner = self.spec_widgets["examiner"].get()
            room = self.spec_widgets["room"].get()
            thesis_required = self.spec_widgets["thesis_required"].get().lower() in ("true","1","yes","y")
            diploma = self.spec_widgets["diploma"].get().lower() in ("true","1","yes","y")
            self.obj = FinalExam.from_params(title, max_score, date, examiner, room, True, thesis_required, diploma)
        elif typ == "Trial":
            difficulty = self.spec_widgets["difficulty"].get()
            try:
                attempts = int(self.spec_widgets["attempts_allowed"].get())
            except Exception:
                attempts = 1
            supervisor = self.spec_widgets["supervisor"].get()
            self.obj = Trial.from_params(title, max_score, date, difficulty, attempts, supervisor)
        self.output.insert(tk.END, f"Створено об'єкт типу {typ}\n")
        self.output.see(tk.END)
    
    def show_object(self):
        if not self.obj:
            messagebox.showinfo("Info", "Спочатку створіть об'єкт")
            return
        self.output.insert(tk.END, "=== Show() ===\n")
        self.output.insert(tk.END, self.obj.show() + "\n\n")
        self.output.see(tk.END)
    
    def evaluate_object(self):
        if not self.obj:
            messagebox.showinfo("Info", "Спочатку створіть об'єкт")
            return
        # Попросимо користувача ввести оцінку (через просте вікно)
        score_str = tk.simpledialog.askstring("Input", "Введіть отриманий бал (number):", parent=self.root)
        if score_str is None:
            return
        try:
            score = float(score_str)
        except ValueError:
            messagebox.showerror("Error", "Score must be a number")
            return
        # Різні сигнатури evaluate -> намагаємось підігнати
        typ = type(self.obj).__name__
        res = None
        if isinstance(self.obj, Exam) and not isinstance(self.obj, FinalExam):
            bonus = 0.0
            # якщо в полях є bonus у GUI, пробуємо його знайти
            if "bonus" in self.spec_widgets:
                try:
                    bonus = float(self.spec_widgets["bonus"].get())
                except Exception:
                    bonus = 0.0
            res = self.obj.evaluate(score, bonus=bonus)
        elif isinstance(self.obj, FinalExam):
            thesis_score = 0.0
            if "thesis_score" in self.spec_widgets:
                try:
                    thesis_score = float(self.spec_widgets["thesis_score"].get())
                except Exception:
                    thesis_score = 0.0
            res = self.obj.evaluate(score, thesis_score=thesis_score)
        else:
            res = self.obj.evaluate(score)
        self.output.insert(tk.END, "=== Evaluate() ===\n")
        self.output.insert(tk.END, f"{res}\n\n")
        self.output.see(tk.END)
    
    def delete_object(self):
        if not self.obj:
            messagebox.showinfo("Info", "Немає об'єкта для видалення")
            return
        # Явно видаляємо об'єкт і викликаємо збір сміття, щоб спровокувати __del__
        self.output.insert(tk.END, f"Видаляємо об'єкт {type(self.obj).__name__}...\n")
        del self.obj
        self.obj = None
        gc.collect()
        self.output.insert(tk.END, "Об'єкт видалено. Перевірте консоль для повідомлення деструктора.\n\n")
        self.output.see(tk.END)

if __name__ == "__main__":
    root = tk.Tk()
    app = App(root)
    root.mainloop()
