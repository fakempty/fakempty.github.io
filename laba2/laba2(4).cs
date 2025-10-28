using System;

public class Program
{
    public static int[][] random_arr(int n)
    {
        Random rnd = new Random();
        int[][] array = new int[n][];
        for (int i = 0; i < n; i++)
        {
            int rowl = rnd.Next(1, 6);
            array[i] = new int[rowl];
            for (int j = 0; j < rowl; j++)
            {
                array[i][j] = rnd.Next(-10, 11);
            }
        }
        return array;
    }

    public static void print_arr(int[][] arr, int n)
    {
        Console.WriteLine("Array:");
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < arr[i].Length; j++)
            {
                Console.Write(arr[i][j] + " ");
            }
            Console.WriteLine();
        }
    }

    public static int[] first_plus(int[][] arr, int n)
    {
        int[] new_arr = new int[n];
        for (int i = 0; i < n; i++)
        {
            bool found = false;
            for (int j = 0; j < arr[i].Length; j++)
            {
                if (arr[i][j] > 0)
                {
                    new_arr[i] = arr[i][j];
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                new_arr[i] = 0;
            }
        }
        return new_arr;
    }

    public static void Main()
    {
        int n = 5;

        int[][] array = random_arr(n);

        print_arr(array, n);

        int[] plus_arr = first_plus(array, n);

        Console.WriteLine("\nfirst positive numbers from each row:");
        for (int i = 0; i < plus_arr.Length; i++)
        {
            Console.Write(plus_arr[i] + " ");
        }

        Console.WriteLine();
    }
}
