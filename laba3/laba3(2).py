import re
import tkinter as tk
from tkinter import messagebox

def find_sites():
    text = text_input.get("1.0", tk.END)
    pattern = r'\b[\w.-]+\.cv\.ua\b' 
    global matches
    matches = re.findall(pattern, text)

    result_list.delete(0, tk.END)
    if matches:
        for site in matches:
            result_list.insert(tk.END, site)
        lbl_count.config(text=f"Кількість знайдених підрядків: {len(matches)}")
    else:
        lbl_count.config(text="Підрядки не знайдено")

def delete_selected():
    selection = result_list.curselection()
    if not selection:
        messagebox.showwarning("Увага", "Оберіть підрядок для видалення!")
        return

    selected_text = result_list.get(selection[0])
    text = text_input.get("1.0", tk.END)
    updated_text = text.replace(selected_text, "")
    text_input.delete("1.0", tk.END)
    text_input.insert(tk.END, updated_text)
    find_sites()

def replace_selected():
    selection = result_list.curselection()
    if not selection:
        messagebox.showwarning("Увага", "Оберіть підрядок для заміни!")
        return

    new_value = entry_replace.get()
    if not new_value:
        messagebox.showwarning("Увага", "Введіть нове значення для заміни!")
        return

    selected_text = result_list.get(selection[0])
    text = text_input.get("1.0", tk.END)
    updated_text = text.replace(selected_text, new_value)
    text_input.delete("1.0", tk.END)
    text_input.insert(tk.END, updated_text)
    find_sites()

# --- GUI ---
root = tk.Tk()
root.title("Пошук підрядків '.cv.ua'")
root.geometry("700x500")

lbl1 = tk.Label(root, text="Введіть текст:")
lbl1.pack()

text_input = tk.Text(root, height=10)
text_input.pack(fill=tk.BOTH, padx=10, pady=5, expand=True)

btn_find = tk.Button(root, text="Знайти підрядки '.cv.ua'", command=find_sites)
btn_find.pack(pady=5)

lbl_count = tk.Label(root, text="Кількість знайдених підрядків: 0")
lbl_count.pack()

result_list = tk.Listbox(root, height=8)
result_list.pack(fill=tk.BOTH, padx=10, pady=5, expand=True)

frame_bottom = tk.Frame(root)
frame_bottom.pack(pady=5)

entry_replace = tk.Entry(frame_bottom, width=30)
entry_replace.grid(row=0, column=0, padx=5)

btn_replace = tk.Button(frame_bottom, text="Замінити вибране", command=replace_selected)
btn_replace.grid(row=0, column=1, padx=5)

btn_delete = tk.Button(frame_bottom, text="Видалити вибране", command=delete_selected)
btn_delete.grid(row=0, column=2, padx=5)

root.mainloop()
