import tkinter as tk
from tkinter import ttk, messagebox
from collections import deque

# --- ЕТАП 1: Структура Вузла та Дерева ---

class Node:
    def __init__(self, day, time, subject):
        self.day = day          # Наприклад: "Понеділок"
        self.time = time        # Наприклад: 10.30 (float)
        self.subject = subject  # Назва предмету
        self.left = None
        self.right = None
        
        # Генеруємо унікальний ключ для сортування в дереві
        # Понеділок = 1, Вівторок = 2...
        # Ключ = (номер_дня * 100) + час. Наприклад: Понеділок 10:00 -> 110.0
        self.sort_key = self._calculate_weight(day, time)

    def _calculate_weight(self, day, time):
        days_map = {
            "Понеділок": 1, "Вівторок": 2, "Середа": 3,
            "Четвер": 4, "П'ятниця": 5, "Субота": 6
        }
        day_num = days_map.get(day, 0)
        try:
            time_val = float(time)
        except ValueError:
            time_val = 0.0
        return (day_num * 100) + time_val

    def __str__(self):
        return f"[{self.day} {self.time}] {self.subject}"

class ScheduleTree:
    def __init__(self):
        self.root = None

    # 1. Включення елемента (стандартна вставка в BST)
    def insert(self, day, time, subject):
        new_node = Node(day, time, subject)
        if self.root is None:
            self.root = new_node
        else:
            self._insert_recursive(self.root, new_node)

    def _insert_recursive(self, current, new_node):
        if new_node.sort_key < current.sort_key:
            if current.left is None:
                current.left = new_node
            else:
                self._insert_recursive(current.left, new_node)
        elif new_node.sort_key > current.sort_key:
            if current.right is None:
                current.right = new_node
            else:
                self._insert_recursive(current.right, new_node)
        else:
            # Якщо час співпадає, оновлюємо предмет
            current.subject = new_node.subject

    # 2. Пошук в бінарному дереві
    def search(self, day, time):
        # Створюємо тимчасовий вузол, щоб отримати ключ для пошуку
        temp = Node(day, time, "")
        target_key = temp.sort_key
        return self._search_recursive(self.root, target_key)

    def _search_recursive(self, current, key):
        if current is None:
            return None
        if key == current.sort_key:
            return current
        elif key < current.sort_key:
            return self._search_recursive(current.left, key)
        else:
            return self._search_recursive(current.right, key)

    # 3. Видалення елементу
    def delete(self, day, time):
        temp = Node(day, time, "")
        key = temp.sort_key
        self.root = self._delete_recursive(self.root, key)

    def _delete_recursive(self, root, key):
        if root is None:
            return root

        if key < root.sort_key:
            root.left = self._delete_recursive(root.left, key)
        elif key > root.sort_key:
            root.right = self._delete_recursive(root.right, key)
        else:
            # Вузол знайдено.
            # Варіант 1: Немає дітей або один
            if root.left is None:
                return root.right
            elif root.right is None:
                return root.left

            # Варіант 2: Два дочірніх вузли
            # Знаходимо найменший елемент у правому піддереві
            min_node = self._min_value_node(root.right)
            # Копіюємо дані
            root.day = min_node.day
            root.time = min_node.time
            root.subject = min_node.subject
            root.sort_key = min_node.sort_key
            # Видаляємо дублікат
            root.right = self._delete_recursive(root.right, min_node.sort_key)

        return root

    def _min_value_node(self, node):
        current = node
        while current.left is not None:
            current = current.left
        return current

    # 4. Обхід дерева (Через ЧЕРГУ - Level Order / BFS)
    def traversal_queue(self):
        """
        Реалізація обходу в ширину з використанням черги.
        Саме це часто мається на увазі під 'дерево через чергу' при обході.
        """
        results = []
        if self.root is None:
            return results

        queue = deque([self.root])  # Використання черги

        while queue:
            current = queue.popleft() # Витягуємо перший елемент (FIFO)
            results.append(current)

            if current.left:
                queue.append(current.left)
            if current.right:
                queue.append(current.right)
        
        return results

    # Додатково: Обхід для красивого виводу (In-Order - відсортований)
    def traversal_sorted(self):
        results = []
        self._in_order(self.root, results)
        return results

    def _in_order(self, node, res):
        if node:
            self._in_order(node.left, res)
            res.append(node)
            self._in_order(node.right, res)

# --- ЕТАП 2: Графічний інтерфейс ---

class ScheduleApp:
    def __init__(self, root):
        self.tree = ScheduleTree()
        self.root = root
        self.root.title("Лаб 10: Розклад (Дерево + Черга)")
        self.root.geometry("600x500")

        # --- Поля вводу ---
        frame_input = tk.Frame(root, pady=10)
        frame_input.pack()

        tk.Label(frame_input, text="День:").grid(row=0, column=0)
        self.cmb_day = ttk.Combobox(frame_input, values=["Понеділок", "Вівторок", "Середа", "Четвер", "П'ятниця", "Субота"])
        self.cmb_day.current(0)
        self.cmb_day.grid(row=0, column=1, padx=5)

        tk.Label(frame_input, text="Час (напр. 10.30):").grid(row=0, column=2)
        self.entry_time = tk.Entry(frame_input, width=10)
        self.entry_time.grid(row=0, column=3, padx=5)

        tk.Label(frame_input, text="Предмет:").grid(row=1, column=0, pady=5)
        self.entry_subj = tk.Entry(frame_input, width=35)
        self.entry_subj.grid(row=1, column=1, columnspan=3, pady=5)

        # --- Кнопки ---
        frame_btns = tk.Frame(root)
        frame_btns.pack(pady=5)

        tk.Button(frame_btns, text="Додати", command=self.add_item, bg="#ccffcc", width=10).pack(side=tk.LEFT, padx=5)
        tk.Button(frame_btns, text="Видалити", command=self.delete_item, bg="#ffcccc", width=10).pack(side=tk.LEFT, padx=5)
        tk.Button(frame_btns, text="Знайти", command=self.search_item, bg="#ffffcc", width=10).pack(side=tk.LEFT, padx=5)

        # --- Вивід ---
        tk.Label(root, text="Результати обходу:", font=("bold")).pack(pady=10)
        
        frame_out = tk.Frame(root)
        frame_out.pack(fill=tk.BOTH, expand=True, padx=10, pady=5)

        # Treeview для відображення списку
        columns = ("day", "time", "subj")
        self.tv = ttk.Treeview(frame_out, columns=columns, show="headings", height=10)
        self.tv.heading("day", text="День")
        self.tv.heading("time", text="Час")
        self.tv.heading("subj", text="Предмет")
        
        self.tv.column("day", width=100)
        self.tv.column("time", width=80)
        self.tv.column("subj", width=300)
        
        self.tv.pack(side=tk.LEFT, fill=tk.BOTH, expand=True)

        # Скролбар
        scrollbar = ttk.Scrollbar(frame_out, orient=tk.VERTICAL, command=self.tv.yview)
        self.tv.configure(yscroll=scrollbar.set)
        scrollbar.pack(side=tk.RIGHT, fill=tk.Y)

        # Кнопки оновлення вигляду
        frame_view = tk.Frame(root, pady=10)
        frame_view.pack()
        tk.Button(frame_view, text="Показати: Відсортовано (In-Order)", command=self.show_sorted).pack(side=tk.LEFT, padx=10)
        tk.Button(frame_view, text="Показати: Як у дереві (Queue BFS)", command=self.show_bfs).pack(side=tk.LEFT, padx=10)

    def add_item(self):
        d, t = self.cmb_day.get(), self.entry_time.get()
        s = self.entry_subj.get()
        if not t or not s:
            messagebox.showwarning("Помилка", "Заповніть час і предмет")
            return
        try:
            self.tree.insert(d, t, s)
            self.show_sorted()
            self.clear_entries()
        except Exception as e:
            messagebox.showerror("Помилка", str(e))

    def delete_item(self):
        d, t = self.cmb_day.get(), self.entry_time.get()
        if not t:
            messagebox.showwarning("Увага", "Введіть час для видалення")
            return
        self.tree.delete(d, t)
        self.show_sorted()

    def search_item(self):
        d, t = self.cmb_day.get(), self.entry_time.get()
        node = self.tree.search(d, t)
        if node:
            messagebox.showinfo("Знайдено", f"Заняття:\n{node.day} о {node.time}\nПредмет: {node.subject}")
        else:
            messagebox.showinfo("Результат", "На цей час нічого не заплановано.")

    def show_sorted(self):
        """Виводить дані в порядку зростання часу (зручно для людини)"""
        data = self.tree.traversal_sorted()
        self.update_table(data)

    def show_bfs(self):
        """Виводить структуру дерева (по рівнях)"""
        data = self.tree.traversal_queue()
        self.update_table(data)

    def update_table(self, nodes):
        for row in self.tv.get_children():
            self.tv.delete(row)
        for node in nodes:
            self.tv.insert("", tk.END, values=(node.day, node.time, node.subject))

    def clear_entries(self):
        self.entry_subj.delete(0, tk.END)

if __name__ == "__main__":
    root = tk.Tk()
    app = ScheduleApp(root)
    root.mainloop()