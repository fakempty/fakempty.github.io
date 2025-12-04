# sort_by_age.py

def sort_by_age(people: list[dict]) -> list[dict]:
    """
    Повертає новий список, відсортований за значенням "age" за зростанням.

    Args:
        people: Список словників, кожен з яких має ключі "name" та "age".

    Returns:
        Новий відсортований список.
    """
    # 1. Використовуємо sorted() для створення нового списку (не змінюємо на місці).
    # 2. Використовуємо lambda-функцію як ключ, щоб витягнути значення "age"
    #    із кожного словника.
    return sorted(people, key=lambda person: person["age"])

# Приклад 1
people_list_1 = [
    {"name": "Alice", "age": 30},
    {"name": "Bob", "age": 25},
    {"name": "Eve", "age": 35},
]
result_1 = sort_by_age(people_list_1)
print(f"Вхідні дані 1: {people_list_1}")
print(f"Результат 1: {result_1}")
# Очікуваний результат: [{'name': 'Bob', 'age': 25}, {'name': 'Alice', 'age': 30}, {'name': 'Eve', 'age': 35}]

# Приклад 2
people_list_2 = [
    {"name": "Zara", "age": 18},
    {"name": "Liam", "age": 22},
    {"name": "Noah", "age": 20}
]
result_2 = sort_by_age(people_list_2)
print(f"\nВхідні дані 2: {people_list_2}")
print(f"Результат 2: {result_2}")
# Очікуваний результат: [{'name': 'Zara', 'age': 18}, {'name': 'Noah', 'age': 20}, {'name': 'Liam', 'age': 22}]