import random
from collections import Counter

def random_array(n):
    arr=[[random.randint(1,10) for j in range(n)] for i in range(n)]
    return arr

n=4

array=random_array(n)

print("array:")
for row in array:
    print(row)

all_num=[elem for row in array for elem in row]

counter = Counter(all_num)

unique_num = [num for num, count in counter.items() if count == 1]

sum_of_uni = sum(unique_num)

print("all uniaue numbers :"+ str(unique_num))

print("sum of unique :"+str(sum_of_uni))
