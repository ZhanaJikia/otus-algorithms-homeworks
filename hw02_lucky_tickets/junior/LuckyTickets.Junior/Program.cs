using System;
using System.Diagnostics;

namespace LuckyTickets.Junior
{
    internal class Program
    {
        private const int DigitRange = 10;

        static void Main()
        {
            var stopwatch = Stopwatch.StartNew();
            long counter = CountLuckyTickets();
            stopwatch.Stop();

            Console.WriteLine(counter);
            Console.WriteLine($"Execution time: {stopwatch.ElapsedMilliseconds} ms");
        }

        static long CountLuckyTickets()
        {
            long counter = 0;

            for (int a = 0; a < DigitRange; a++)
            for (int a1 = 0; a1 < DigitRange; a1++)
            for (int a2 = 0; a2 < DigitRange; a2++)
            {
                int sum = a + a1 + a2;
                for (int b = 0; b < DigitRange; b++)
                for (int b1 = 0; b1 < DigitRange; b1++)
                {
                    int b3 = sum - b - b1;
                    if (b3 >= 0 && b3 < DigitRange)
                    {
                        counter++;
                    }
                }
            }

            return counter;
        }
    }
}
