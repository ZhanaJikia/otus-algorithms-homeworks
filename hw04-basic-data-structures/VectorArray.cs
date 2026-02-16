using System;
using System.Diagnostics;

// VectorArray = dynamic array that grows by a FIXED STEP (vector size).
// Example: step = 100
// When capacity is full, allocate a new array with (oldCapacity + step) and copy.
// Insert/remove still require shifting, but reallocations happen less often than SingleArray.

public class VectorArray<T>
{
    private T[] data;
    private readonly int step;

    public int Size { get; private set; } = 0;


    public VectorArray(int step = 3)
    {
        if (step <= 0) throw new ArgumentOutOfRangeException(nameof(step));
        this.step = step;
        data = new T[0];
    }

    public T Get(int index)
    {
        if (index < 0 || index >= Size)
            throw new ArgumentOutOfRangeException(nameof(index));
        return data[index];
    }

    public void Set(T item, int index)
    {
        if (index < 0 || index >= Size)
            throw new ArgumentOutOfRangeException(nameof(index));
        data[index] = item;
    }


    public void Add(T item, int index)
    {
        if (index < 0 || index > Size)
            throw new ArgumentOutOfRangeException(nameof(index));
        EnsureCapacity(Size + 1);
        
        // shift right to free slot at index
        for (int i = Size; i > index; i--)
            data[i] = data[i - 1];

        data[index] = item;
        Size++;
    }

    public T Remove(int index)
    {
        if (index < 0 || index >= Size)
            throw new ArgumentOutOfRangeException(nameof(index));

        long start = Stopwatch.GetTimestamp();

        T removed = data[index];

        // shift left to fill the gap
        for (int i = index + 1; i < Size; i++)
            data[i - 1] = data[i];

        Size--;

        // optional: clear last slot (not required, but nice)
        data[Size] = default(T);

        return removed;
    }

    private void EnsureCapacity(int minCapacity)
    {
        if (data.Length >= minCapacity) return;

        int newCapacity = data.Length;
        if (newCapacity == 0) newCapacity = step;

        while (newCapacity < minCapacity)
            newCapacity += step;

        T[] newData = new T[newCapacity];

        // copy only used part
        for (int i = 0; i < Size; i++)
            newData[i] = data[i];

        data = newData;
    }
}
