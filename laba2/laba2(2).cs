using System;


class Program
{
    static int[,] rand_arr(int n)
    {
        int[,] array = new int[n, n];
        Random rnd = new Random();

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                array[i,j] = rnd.Next(10);
            }
        }
        return array;
    }

    static void print_arr(int[,] array)
    {
        int n = array.GetLength(0);
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write(array[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    static int[,] combine(int[,] array_a, int[,] array_b, int n)
    {
        int[] new_array = new int[n*n];

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (array_a[i,j] == array_b[i,j])
                {
                    new_array[i * n + j] = 1;
                }
                else
                {
                    new_array[i * n + j] = 0;
                }
            }
        }
        return new_array;
    }

    static void Main()
    {
        int n = 6;
        int[,] array_a = rand_arr(n);
        int[,] array_b = rand_arr(n);

        Console.WriteLine("array A:");
        print_arr(array_a);
        Console.WriteLine("array B:");
        print_arr(array_b);

        int[n*n] array_x = combine(array_a, array_b, n);

        for (int i = 0; i < (n*n); i++)
        {
            Console.Write(array_x[i] + " ");
        }
    
    }   
}