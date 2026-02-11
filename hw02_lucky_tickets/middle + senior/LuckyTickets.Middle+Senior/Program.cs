using System;
using System.IO;
using System.Diagnostics;

internal class Program
{
    static void Main()
    {
        var stopwatch = Stopwatch.StartNew();
        
        string testDir = "tests";
        
        if (!Directory.Exists(testDir))
        {
            Console.WriteLine($"Test directory '{testDir}' not found.");
            return;
        }

        var inputFiles = Directory.GetFiles(testDir, "*.in");
        int passed = 0;
        int failed = 0;

        foreach (var inputFile in inputFiles)
        {
            string outputFile = inputFile.Replace(".in", ".out");
            
            if (!File.Exists(outputFile))
            {
                Console.WriteLine($"Missing output file for {inputFile}");
                continue;
            }

            string input = File.ReadAllText(inputFile).Trim();
            string expectedOutput = File.ReadAllText(outputFile).Trim();
            
            int n = string.IsNullOrWhiteSpace(input) ? 3 : int.Parse(input);
            long result = CountLuckyTickets(n);
            string actualOutput = result.ToString();

            if (actualOutput == expectedOutput)
            {
                Console.WriteLine($"✓ {Path.GetFileName(inputFile)}");
                passed++;
            }
            else
            {
                Console.WriteLine($"✗ {Path.GetFileName(inputFile)}: expected {expectedOutput}, got {actualOutput}");
                failed++;
            }
        }

        stopwatch.Stop();
        Console.WriteLine($"\nPassed: {passed}, Failed: {failed}");
        Console.WriteLine($"Execution time: {stopwatch.ElapsedMilliseconds} ms");
    }

    static long CountLuckyTickets(int N) 
    {
        // totals[s] = сколько N-значных половинок дают сумму s
        long[] totals = new long[N * 9 + 1];

        // digits временно хранит предыдущий totals, чтобы пересчитать новый
        long[] digits = new long[N * 9 + 1];

        // База для N = 1:
        // для одной цифры суммы 0..9 существуют ровно по 1 разу: 0,1,2,...,9
        for (int d = 0; d <= 9; d++)
            totals[d] = 1;

        // Если N > 1, достраиваем до нужной длины
        for (int j = 2; j <= N; j++)
        {
            int maxSumNow = j * 9;       // максимальная сумма для j цифр
            int maxSumPrev = maxSumNow - 9; // максимальная сумма для (j-1) цифр

            // копируем предыдущие значения totals в digits (только до maxSumPrev)
            for (int s = 0; s <= maxSumPrev; s++)
                digits[s] = totals[s];

            // обнуляем totals для пересчета нового уровня (до maxSumNow)
            for (int s = 0; s <= maxSumNow; s++)
                totals[s] = 0;

            // добавляем новую цифру d (0..9) ко всем старым суммам s
            for (int d = 0; d <= 9; d++)
                for (int s = 0; s <= maxSumPrev; s++)
                    totals[s + d] += digits[s];
        }

        // ответ = сумма квадратов totals[s]
        long luckyTickets = 0;
        for (int s = 0; s <= N * 9; s++)
            luckyTickets += totals[s] * totals[s];
        return luckyTickets;
    }
}