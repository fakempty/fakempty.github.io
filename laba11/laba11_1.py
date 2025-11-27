def filter_even_numbers(nums: list[int]) -> list[int]:
    # Використовуємо списковий вираз:
    # [елемент for елемент in список if умова]
    return [num for num in nums if num % 2 == 0]

# --- Перевірка роботи функції ---
input_list = [1, 2, 3, 4, 5, 6, 7, 8]
result_list = filter_even_numbers(input_list)

print(f"Вхідний список: {input_list}")
print(f"Результат (парні числа): {result_list}")