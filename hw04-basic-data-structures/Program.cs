using System;
using System.Diagnostics;

public class Program
{
    public static void Main()
    {
        int n = 10000; // if too slow, try 3000

        Console.WriteLine("N = " + n);
        Console.WriteLine();

        // -------- SingleArray --------
        long singleAdd = MeasureMs(() =>
        {
            var a = new SingleArray<int>();
            for (int i = 0; i < n; i++)
                a.Add(i, a.Size); // add to end
        });

        long singleRemove = MeasureMs(() =>
        {
            var a = new SingleArray<int>();
            for (int i = 0; i < n; i++)
                a.Add(i, a.Size); // build array first

            for (int i = 0; i < n; i++)
                a.Remove(0); // remove from beginning
        });

        Console.WriteLine($"SingleArray  AddEnd={singleAdd} ms   RemoveBegin={singleRemove} ms");


        // -------- VectorArray --------
        long vectorAdd = MeasureMs(() =>
        {
            var a = new VectorArray<int>(step: 100);
            for (int i = 0; i < n; i++)
                a.Add(i, a.Size);
        });

        long vectorRemove = MeasureMs(() =>
        {
            var a = new VectorArray<int>(step: 100);
            for (int i = 0; i < n; i++)
                a.Add(i, a.Size);

            for (int i = 0; i < n; i++)
                a.Remove(0);
        });

        Console.WriteLine($"VectorArray  AddEnd={vectorAdd} ms   RemoveBegin={vectorRemove} ms");


        // -------- FactorArray --------
        long factorAdd = MeasureMs(() =>
        {
            var a = new FactorArray<int>(); // x2 growth
            for (int i = 0; i < n; i++)
                a.Add(i, a.Size);
        });

        long factorRemove = MeasureMs(() =>
        {
            var a = new FactorArray<int>();
            for (int i = 0; i < n; i++)
                a.Add(i, a.Size);

            for (int i = 0; i < n; i++)
                a.Remove(0);
        });

        Console.WriteLine($"FactorArray  AddEnd={factorAdd} ms   RemoveBegin={factorRemove} ms");


        // -------- MatrixArray --------
        long matrixAdd = MeasureMs(() =>
        {
            var a = new MatrixArray<int>(blockSize: 100);
            for (int i = 0; i < n; i++)
                a.Add(i, a.Size);
        });

        long matrixRemove = MeasureMs(() =>
        {
            var a = new MatrixArray<int>(blockSize: 100);
            for (int i = 0; i < n; i++)
                a.Add(i, a.Size);

            for (int i = 0; i < n; i++)
                a.Remove(0);
        });

        Console.WriteLine($"MatrixArray  AddEnd={matrixAdd} ms   RemoveBegin={matrixRemove} ms");
    }

    static long MeasureMs(Action action)
    {
        action(); // warmup
        var sw = Stopwatch.StartNew();
        action();
        sw.Stop();
        return sw.ElapsedMilliseconds;
    }
}
