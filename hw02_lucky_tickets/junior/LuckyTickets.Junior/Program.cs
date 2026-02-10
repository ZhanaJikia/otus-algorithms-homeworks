using System;
using System.Diagnostics;

internal class Program

{
    static void Main()
    {
       var stopwatch = Stopwatch.StartNew();
       long counter = 0;

       for (int a = 0; a < 10; a++)
       for (int a1 = 0; a1 < 10; a1++)
       for (int a2 = 0; a2 < 10; a2++)
       {
        int sum = a + a1 + a2;
        for (int b = 0; b < 10; b++)
        for (int b1 = 0; b1 < 10; b1++)
        {
            int b3 = sum - b - b1;
            if (b3 >= 0 && b3 < 10)
            {
                counter++;
            }
        }
       }
        Console.WriteLine(counter);
        stopwatch.Stop();
        Console.WriteLine($"Execution time: {stopwatch.ElapsedMilliseconds} ms");
    }
}
