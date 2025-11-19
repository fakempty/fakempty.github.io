import tkinter as tk
from tkinter import messagebox


# ===========================
# КЛАСИ
# ===========================
class PhotoCamera:
    def __init__(self, model="", zoom=1, material="plastic"):
        self.model = model
        self.zoom = float(zoom)
        self.material = material.lower()

    def cost(self):
        if self.material == "plastic":
            return (self.zoom + 2) * 10
        else:
            return (self.zoom + 2) * 15

    def info(self):
        return f"[Фотоапарат]\nМодель: {self.model}\nZoom: {self.zoom}\nВартість: {self.cost()}$"


class DigitalCamera(PhotoCamera):
    def __init__(self, model="", zoom=1, material="plastic", megapixels=1):
        super().__init__(model, zoom, material)
        self.megapixels = float(megapixels)

    def cost(self):
        return super().cost() * self.megapixels

    def update_model(self):
        self.megapixels += 2

    def info(self):
        return f"[Цифровий фотоапарат]\nМодель: {self.model}\nZoom: {self.zoom}\nMP: {self.megapixels}\nВартість: {self.cost()}$"


class Camera(PhotoCamera):
    def __init__(self, model="", zoom=1, material="plastic", megapixels=1, cam_type=""):
        super().__init__(model, zoom, material)
        self.megapixels = float(megapixels)
        self.cam_type = cam_type

    def cost(self):
        return super().cost() * self.megapixels * 10

    def update_model(self):
        self.megapixels += 20

    def info(self):
        return f"[Камера]\nМодель: {self.model}\nТип: {self.cam_type}\nZoom: {self.zoom}\nMP: {self.megapixels}\nВартість: {self.cost()}$"


# ===========================
# GUI
# ===========================
root = tk.Tk()
root.title("ЛР 5 — Фотоапарати (OOP)")
root.geometry("600x500")


# Поля введення
entries = {}

labels = [
    "Модель",
    "Zoom (1-35)",
    "Матеріал (metal/plastic)",
    "Мегапікселі",
    "Тип камери (для 3-го)"
]

for i, text in enumerate(labels):
    lbl = tk.Label(root, text=text, font=("Arial", 11))
    lbl.grid(row=i, column=0, pady=5, sticky="w")

    ent = tk.Entry(root, width=25, font=("Arial", 11))
    ent.grid(row=i, column=1, pady=5)
    entries[text] = ent

# Об’єкти
obj1 = obj2 = obj3 = None


def create_objects():
    global obj1, obj2, obj3

    model = entries["Модель"].get()
    zoom = entries["Zoom (1-35)"].get()
    material = entries["Матеріал (metal/plastic)"].get()
    mp = entries["Мегапікселі"].get()
    cam_type = entries["Тип камери (для 3-го)"].get()

    if not model or not zoom or not material or not mp:
        messagebox.showerror("Помилка", "Заповніть всі поля!")
        return

    try:
        obj1 = PhotoCamera(model, float(zoom), material)
        obj2 = DigitalCamera(model, float(zoom), material, float(mp))
        obj3 = Camera(model, float(zoom), material, float(mp), cam_type)

        messagebox.showinfo("OK", "Об'єкти створені!")

    except:
        messagebox.showerror("Помилка", "Перевірте введені дані!")


def show_info():
    if not obj1:
        messagebox.showerror("Помилка", "Спочатку створіть об'єкти!")
        return

    text = (
        obj1.info() + "\n\n" +
        obj2.info() + "\n\n" +
        obj3.info()
    )
    output.delete("1.0", tk.END)
    output.insert(tk.END, text)


def update_models():
    if not obj1:
        messagebox.showerror("Помилка", "Створіть об'єкти!")
        return

    obj2.update_model()
    obj3.update_model()

    messagebox.showinfo("OK", "Моделі оновлено! (MP +2 і +20)")


# Кнопки
btn1 = tk.Button(root, text="Створити об'єкти", width=20, font=("Arial", 12), command=create_objects)
btn1.grid(row=6, column=0, pady=10)

btn2 = tk.Button(root, text="Показати інформацію", width=20, font=("Arial", 12), command=show_info)
btn2.grid(row=6, column=1, pady=10)

btn3 = tk.Button(root, text="Оновити моделі", width=20, font=("Arial", 12), command=update_models)
btn3.grid(row=7, column=0, pady=10)


# Вікно виводу
output = tk.Text(root, width=60, height=10, font=("Arial", 11))
output.grid(row=8, column=0, columnspan=3, pady=20)


root.mainloop()
