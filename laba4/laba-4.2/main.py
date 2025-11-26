import string
import tkinter as tk
from tkinter import scrolledtext

# --- Функція обробки ---
def process_text():
    vowels = "AEIOUYaeiouy"  
    input_text = input_box.get("1.0", tk.END)  
    words_starting_with_vowel = []

    # Обробка рядків
    for line in input_text.splitlines():
        clean_line = line.translate(str.maketrans('', '', string.punctuation))
        for word in clean_line.split():
            if word and word[0] in vowels:
                words_starting_with_vowel.append(word)

    # Вивід у другий Text
    output_box.delete("1.0", tk.END)
    for word in words_starting_with_vowel:
        output_box.insert(tk.END, word + "\n")

    # Запис у TF_2.txt
    with open("TF_2.txt", "w", encoding="utf-8") as f:
        for word in words_starting_with_vowel:
            f.write(word + "\n")

# --- GUI ---
root = tk.Tk()
root.title("Слова, що починаються з голосної")


tk.Label(root, text="Введіть текст:").pack()

input_box = scrolledtext.ScrolledText(root, width=60, height=10)
input_box.pack(padx=10, pady=5)

# Кнопка запуску обробки
btn = tk.Button(root, text="Знайти слова на голосну", command=process_text)
btn.pack(pady=5)

tk.Label(root, text="Слова, що починаються з голосної:").pack()
# Текстове поле для результату
output_box = scrolledtext.ScrolledText(root, width=60, height=10)
output_box.pack(padx=10, pady=5)

root.mainloop()
