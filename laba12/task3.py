# capitalize_words.py

from typing import Iterable

def capitalize_words(words: Iterable[str]) -> Iterable[str]:
    """
    Повертає нову ітерабельну послідовність, де кожен рядок починається з великої літери.

    Args:
        words: Ітерабельна послідовність рядків.

    Returns:
        Ітерабельна послідовність з капіталізованими рядками (результат map()).
    """
    # Використовуємо map() з функцією str.capitalize (або її посиланням)
    # для застосування до кожного елементу.
    # Повертаємо результат map() без перетворення на list/tuple.
    return map(str.capitalize, words)

# Приклад 1
words_1 = ["python", "java", "c++"]
result_1 = capitalize_words(words_1)
print(f"Вхідні дані 1: {words_1}")
print(f"Результат 1 (як список): {list(result_1)}")
# Очікуваний результат: ['Python', 'Java', 'C++']

# Приклад 2
words_2 = ("hello", "world")
result_2 = capitalize_words(words_2)
print(f"\nВхідні дані 2: {words_2}")
print(f"Результат 2 (як кортеж): {tuple(result_2)}")
# Очікуваний результат: ('Hello', 'World')

# Приклад 3
words_3 = [""]
result_3 = capitalize_words(words_3)
print(f"\nВхідні дані 3: {words_3}")
print(f"Результат 3 (як список): {list(result_3)}")
# Очікуваний результат: ['']