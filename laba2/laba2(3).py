import random

salary = [[random.randint(1000, 5000) for _ in range(12)] for _ in range(10)]

print("Salary for workers for every month:")
for i, emp in enumerate(salary, 1):
    print(f"Employer {i}: {emp}")

total_salary = [sum(emp) for emp in salary]
print("\nTotal salary for each employer:")
for i, total in enumerate(total_salary, 1):
    print(f"Employer {i}: {total}")

average_per_month = [sum(salary[emp][month] for emp in range(10)) / 10 for month in range(12)]

months = ["January", "February", "March", "April", "May", "June",
          "July", "August", "September", "October", "November", "December"]

print("\nAverage salary for every month:")
for month, avg in zip(months, average_per_month):
    print(f"{month}: {avg:.2f}")
