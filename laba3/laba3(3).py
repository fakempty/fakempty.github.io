import tkinter as tk
from tkinter import messagebox
import re

def remove_odd_length_words():
    text = input_text.get("1.0", tk.END)
    words = re.findall(r'\w+', text)
    even_words = [word for word in words if len(word) % 2 == 0]
    result = ' '.join(even_words)
    output_text.delete("1.0", tk.END)
    output_text.insert(tk.END, result)
    removed_count = len(words) - len(even_words)
    messagebox.showinfo("Результат", f"Видалено {removed_count} слів непарної довжини.")

window = tk.Tk()
window.title("Видалення слів непарної довжини")
window.geometry("600x400")

tk.Label(window, text="Введіть текст:", font=("Arial", 12)).pack(pady=5)
input_text = tk.Text(window, height=8, width=70)
input_text.pack(padx=10, pady=5)

tk.Button(window, text="Вилучити слова непарної довжини", font=("Arial", 11), command=remove_odd_length_words).pack(pady=10)

tk.Label(window, text="Результат:", font=("Arial", 12)).pack(pady=5)
output_text = tk.Text(window, height=8, width=70)
output_text.pack(padx=10, pady=5)

window.mainloop()
