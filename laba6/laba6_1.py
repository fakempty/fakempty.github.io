import tkinter as tk
from tkinter import ttk, messagebox
from abc import ABC, abstractmethod

# ============================
# Абстрактний клас Plant
# ============================
class Plant(ABC):
    def __init__(self, name, family, is_red_book):
        self.name = name
        self.family = family
        self.is_red_book = is_red_book

    def info(self):
        return f"Назва: {self.name}\nРодина: {self.family}\nУ Червоній книзі: {self.is_red_book}"

    @abstractmethod
    def type_name(self):
        pass

    @abstractmethod
    def characteristic(self):
        pass


# ============================
# Клас Tree
# ============================
class Tree(Plant):
    def __init__(self, name, family, is_red_book, height):
        super().__init__(name, family, is_red_book)
        self.height = height

    def type_name(self):
        return "Дерево"

    def characteristic(self):
        return f"Висота дерева: {self.height} м"

    def grow(self):
        return f"Дерево {self.name} росте..."


# ============================
# Клас Flower
# ============================
class Flower(Plant):
    def __init__(self, name, family, is_red_book, color):
        super().__init__(name, family, is_red_book)
        self.color = color

    def type_name(self):
        return "Квіти"

    def characteristic(self):
        return f"Колір пелюсток: {self.color}"

    def smell(self):
        return f"Квіти {self.name} мають приємний аромат."


# ============================
# GUI
# ============================
class App:
    def __init__(self, root):
        self.root = root
        self.root.title("ЛР6 — Рослини (Абстрактні класи та інтерфейси)")
        self.root.geometry("700x500")

        self.plants = []

        # ---------- Вхідні поля ----------
        frm = tk.Frame(root)
        frm.pack(pady=10)

        tk.Label(frm, text="Назва").grid(row=0, column=0)
        tk.Label(frm, text="Родина").grid(row=1, column=0)
        tk.Label(frm, text="Червона книга (так/ні)").grid(row=2, column=0)
        tk.Label(frm, text="Тип").grid(row=3, column=0)
        tk.Label(frm, text="Висота / Колір").grid(row=4, column=0)

        self.name_var = tk.Entry(frm)
        self.family_var = tk.Entry(frm)
        self.red_var = tk.Entry(frm)
        self.extra_var = tk.Entry(frm)

        self.name_var.grid(row=0, column=1)
        self.family_var.grid(row=1, column=1)
        self.red_var.grid(row=2, column=1)
        self.extra_var.grid(row=4, column=1)

        self.type_var = ttk.Combobox(frm, values=["Дерево", "Квіти"])
        self.type_var.grid(row=3, column=1)
        self.type_var.current(0)

        # ---------- Кнопки ----------
        tk.Button(frm, text="Додати рослину", command=self.add_plant).grid(row=5, column=0, columnspan=2, pady=10)
        tk.Button(frm, text="Показати всі", command=self.show_all).grid(row=6, column=0, columnspan=2)
        tk.Button(frm, text="Пошук Червоної книги", command=self.show_red_book).grid(row=7, column=0, columnspan=2, pady=5)

        # ---------- Вивід ----------
        self.text = tk.Text(root, width=80, height=15)
        self.text.pack(pady=10)

    # --------------------------
    # Додати рослину
    # --------------------------
    def add_plant(self):
        name = self.name_var.get().strip()
        family = self.family_var.get().strip()
        red = self.red_var.get().strip().lower()
        extra = self.extra_var.get().strip()
        ptype = self.type_var.get()

        if not name or not family or not extra:
            messagebox.showerror("Помилка", "Заповніть усі поля!")
            return

        is_red = True if red in ["так", "yes", "1", "true"] else False

        if ptype == "Дерево":
            try:
                height = float(extra)
            except:
                messagebox.showerror("Помилка", "Висота повинна бути числом!")
                return
            plant = Tree(name, family, is_red, height)

        else:
            plant = Flower(name, family, is_red, extra)

        self.plants.append(plant)
        messagebox.showinfo("Успіх", "Рослину додано!")
        self.clear_inputs()

    # --------------------------
    def clear_inputs(self):
        self.name_var.delete(0, tk.END)
        self.family_var.delete(0, tk.END)
        self.red_var.delete(0, tk.END)
        self.extra_var.delete(0, tk.END)

    # --------------------------
    # Показати всі рослини
    # --------------------------
    def show_all(self):
        self.text.delete(1.0, tk.END)
        if not self.plants:
            self.text.insert(tk.END, "База порожня!")
            return

        for p in self.plants:
            self.text.insert(tk.END, "=============================\n")
            self.text.insert(tk.END, f"Тип: {p.type_name()}\n")
            self.text.insert(tk.END, p.info() + "\n")
            self.text.insert(tk.END, p.characteristic() + "\n\n")

    # --------------------------
    # Показати рослини Червоної книги
    # --------------------------
    def show_red_book(self):
        self.text.delete(1.0, tk.END)
        red = [p for p in self.plants if p.is_red_book]

        if not red:
            self.text.insert(tk.END, "Немає рослин з Червоної книги.")
            return

        for p in red:
            self.text.insert(tk.END, f"- {p.name} ({p.type_name()})\n")


# ============================
# Запуск програми
# ============================
if __name__ == "__main__":
    root = tk.Tk()
    app = App(root)
    root.mainloop()