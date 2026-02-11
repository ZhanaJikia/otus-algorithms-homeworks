using System;

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("Junior Manual Checks\n");

        // 1) Power O(N)
        Console.WriteLine("1) Power O(N) iterative:\n");
        PrintCheck("PowerIterative(2, 0)", 1, PowerIterative(2, 0));
        PrintCheck("PowerIterative(2, 3)", 8, PowerIterative(2, 3));
        PrintCheck("PowerIterative(5, 2)", 25, PowerIterative(5, 2));
        PrintCheck("PowerIterative(3, 4)", 81, PowerIterative(3, 4));

        // 2) Fibonacci: recursive O(2^N) and iterative O(N)
        Console.WriteLine("\n2) Fibonacci:\n");
        PrintCheck("FibonacciRecursive(0)", 0, FibonacciRecursive(0));
        PrintCheck("FibonacciRecursive(5)", 5, FibonacciRecursive(5));
        PrintCheck("FibonacciRecursive(1)", 1, FibonacciRecursive(1));
        PrintCheck("FibonacciRecursive(10)", 55, FibonacciRecursive(10));

        // 2) FibonacciIterative should give the same results as FibonacciRecursive, 
        // but much faster for larger n
        PrintCheck("FibonacciIterative(0)", 0, FibonacciIterative(0));
        PrintCheck("FibonacciIterative(5)", 5, FibonacciIterative(5));
        PrintCheck("FibonacciIterative(1)", 1, FibonacciIterative(1));
        PrintCheck("FibonacciIterative(10)", 55, FibonacciIterative(10));

        // 3) CountPrimesNaive O(N^2)
        Console.WriteLine("\n3) CountPrimesNaive:\n");
        PrintCheck("CountPrimesNaive(1)", 0, CountPrimesNaive(1));
        PrintCheck("CountPrimesNaive(2)", 1, CountPrimesNaive(2));
        PrintCheck("CountPrimesNaive(10)", 4, CountPrimesNaive(10)); // primes are 2,3,5,7
        PrintCheck("CountPrimesNaive(20)", 8, CountPrimesNaive(20)); // primes are 2,3,5,7,11,13,17,19
    }


    public static long PowerIterative(int a, int n)
    // Iterative means: you solve a problem by repeating steps in a loop (for, while) until you finish.
    // You do not call the same function again inside itself.
    // O(N) means: the number of steps you need to solve a problem grows linearly with the size of the input (N).

    {
        long result = 1;
        for (int i = 0; i < n; i++)
        {
            result *= a;
        }
        return result;
    }

    public static long FibonacciRecursive(int n)
    // Recursive means: you solve a problem by calling the same function again inside itself with smaller
    // inputs until you reach a base case (like n=0 or n=1).
    // O(2^N) means: the number of steps you need to solve a problem grows exponentially with 
    // the size of the input (N).
    // Fibonacci numbers are defined as: F(0)=0, F(1)=1, F(n)=F(n-1)+F(n-2) for n>1.
    {
        if (n <= 1) return n;
        return FibonacciRecursive(n - 1) + FibonacciRecursive(n - 2);
    }

    public static long FibonacciIterative(int n)
    // Iterative version of Fibonacci, O(N)
    // You can solve Fibonacci iteratively by keeping track of 
    // the last two Fibonacci numbers and updating them in a loop until you reach n.
    {
        if (n <= 1) return n;
        long a = 0, b = 1;

        for (int i = 2; i <= n; i++)
        {
            long c = a + b;
            a = b;
            b = c;
        }
        return b;
    }

    public static int CountPrimesNaive(int n) 
    // Count how many prime numbers are in 2..N.
    // A number x is prime if it has no divisors except 1 and itself.
    // Naive check: try dividing by every d from 2 to x-1.
    {
        int counter = 0;
        for (int x = 2; x <= n; x++)
        {
            bool isPrime = true;
            for (int d = 2; d < x; d++)
            {
                if (x % d == 0)
                {
                    isPrime = false;
                    break;
                }
            }
            if (isPrime) counter++;
        }
        return counter;
    }


    private static void PrintCheck(string name, long expected, long actual)
    {
        Console.WriteLine($"{name}: expected {expected}, got {actual} -> {(expected == actual ? "OK" : "FAIL")}");
    }
}
