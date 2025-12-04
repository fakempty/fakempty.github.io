# filter_words.py

def filter_long_words(words: list[str]) -> list[str]:
    """
    Повертає новий список, що містить лише ті слова, довжина яких > 3 символів.

    Args:
        words: Список рядків (слів).

    Returns:
        Новий відфільтрований список слів.
    """
    # 1. Використовуємо lambda-функцію всередині filter(), яка перевіряє,
    #    чи довжина слова (len(word)) більша за 3.
    # 2. Результат filter() (ітератор) перетворюємо на список за допомогою list().
    return list(filter(lambda word: len(word) > 3, words))

# Приклад 1
words_1 = ["a", "the", "code", "Python", "is", "fun"]
result_1 = filter_long_words(words_1)
print(f"Вхідні дані 1: {words_1}")
print(f"Результат 1: {result_1}")
# Очікуваний результат: ['code', 'Python']

# Приклад 2
words_2 = ["cat", "dog", "fish", "go", "egg"]
result_2 = filter_long_words(words_2)
print(f"\nВхідні дані 2: {words_2}")
print(f"Результат 2: {result_2}")
# Очікуваний результат: ['fish']

# Приклад 3
words_3 = ["", "aa", "bbb", "cccc", "ddddd"]
result_3 = filter_long_words(words_3)
print(f"\nВхідні дані 3: {words_3}")
print(f"Результат 3: {result_3}")
# Очікуваний результат: ['cccc', 'ddddd']