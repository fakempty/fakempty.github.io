from typing import Iterator

def walk_tree(data: dict) -> Iterator[str]:
    """
    Рекурсивно проходить дерево (вкладені словники) та повертає ключі
    у порядку глибини (depth-first traversal).
    """
    for key, value in data.items():
        # Спочатку повертаємо поточний ключ
        yield key
        
        # Якщо значення є словником, рекурсивно обходимо його
        if isinstance(value, dict):
            # yield from делегує виконання генератору walk_tree(value)
            # і повертає всі значення, які той згенерує
            yield from walk_tree(value)

if __name__ == "__main__":
    print("Приклад 1:")
    data1 = {
        "a": {
            "b": {
                "c": 1
            }
        },
        "d": 2
    }
    # Очікується: ['a', 'b', 'c', 'd']
    print(list(walk_tree(data1))) 

    print("\nПриклад 2:")
    data2 = {
        "x": {"y": {"z": {}}},
        "m": {"n": 42}
    }
    # Очікується: ['x', 'y', 'z', 'm', 'n']
    print(list(walk_tree(data2)))