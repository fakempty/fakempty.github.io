# validate_password.py

from typing import Callable

# Набір спеціальних символів для перевірки
SPECIAL_CHARS = "!@#$%^&*()"

# 1. Функції-правила перевірки (повертають True, якщо правило виконано)

def has_uppercase(password: str) -> bool:
    """Перевіряє, чи містить пароль хоча б одну велику літеру."""
    # Використовуємо any() з генераторним виразом для ефективної перевірки
    return any(c.isupper() for c in password)

def has_digit(password: str) -> bool:
    """Перевіряє, чи містить пароль хоча б одну цифру."""
    return any(c.isdigit() for c in password)

def is_long_enough(password: str) -> bool:
    """Перевіряє, чи має пароль довжину не менше 8 символів."""
    return len(password) >= 8

def has_special_char(password: str) -> bool:
    """Перевіряє, чи містить пароль хоча б один спеціальний символ."""
    return any(c in SPECIAL_CHARS for c in password)

def no_spaces(password: str) -> bool:
    """Перевіряє, чи не містить пароль пробілів."""
    # Перевіряємо, що пробіл не міститься в паролі
    return ' ' not in password

# 2. Список правил
# list[Callable[[str], bool]] - список функцій, які приймають рядок і повертають bool.
RULES: list[Callable[[str], bool]] = [
    has_uppercase,
    has_digit,
    is_long_enough,
    has_special_char,
    no_spaces,
]

def validate_password(password: str) -> bool:
    """
    Перевіряє, чи відповідає пароль усім заданим вимогам безпеки.

    Args:
        password: Рядок-пароль.

    Returns:
        True, якщо всі правила виконані, інакше False.
    """
    # 3. Використовуємо all() з генераторним виразом:
    #    Для кожного правила (функції) у списку RULES викликаємо його з
    #    поточним паролем. all() поверне True, тільки якщо ВСІ виклики повернули True.
    return all(rule(password) for rule in RULES)

# Приклад 1: Успішний
password_1 = "StrongPass1!"
result_1 = validate_password(password_1)
print(f"Пароль: '{password_1}' -> Результат: {result_1}")
# Очікуваний результат: True

# Приклад 2: Немає великих літер, цифр, замала довжина (неуспішний)
password_2 = "weakpass"
result_2 = validate_password(password_2)
print(f"Пароль: '{password_2}' -> Результат: {result_2}")
# Очікуваний результат: False (немає великих літер, цифр, спец. символів, замала довжина)

# Приклад 3: Замала довжина (неуспішний)
password_3 = "Short7!"
result_3 = validate_password(password_3)
print(f"Пароль: '{password_3}' -> Результат: {result_3}")
# Очікуваний результат: False

# Приклад 4: Містить пробіл (неуспішний)
password_4 = "With Space1!"
result_4 = validate_password(password_4)
print(f"Пароль: '{password_4}' -> Результат: {result_4}")
# Очікуваний результат: False

# Приклад 5: Немає спеціального символу (неуспішний)
password_5 = "NoSpecialChar1"
result_5 = validate_password(password_5)
print(f"Пароль: '{password_5}' -> Результат: {result_5}")
# Очікуваний результат: False