using System;

// FactorArray = dynamic array that grows by MULTIPLYING capacity.
// Example factor x2:
// capacity: 1 -> 2 -> 4 -> 8 -> 16 -> ...
//
// Why it exists:
// - SingleArray reallocates every time (very slow).
// - VectorArray reallocates every +step (better).
// - FactorArray reallocates rarely because capacity grows fast.
//
// Main idea:
// - Keep extra free space inside `data`.
// - When we run out of space, allocate a bigger array and copy existing elements.
public class FactorArray<T>
{
    private T[] data = new T[0];

    // Real number of elements stored (used part of the array).
    public int Size { get; private set; }

    // Growth factor (default x2).
    // Using numerator/denominator so we can represent 1.5 as 3/2 etc.
    private readonly int factorNumerator;
    private readonly int factorDenominator;

    public FactorArray(int factorNumerator = 2, int factorDenominator = 1)
    {
        if (factorNumerator <= 1) throw new ArgumentOutOfRangeException(nameof(factorNumerator));
        if (factorDenominator <= 0) throw new ArgumentOutOfRangeException(nameof(factorDenominator));

        this.factorNumerator = factorNumerator;
        this.factorDenominator = factorDenominator;
    }

    public T Get(int index)
    {
        if (index < 0 || index >= Size) throw new ArgumentOutOfRangeException(nameof(index));
        return data[index];
    }

    public void Set(T item, int index)
    {
        if (index < 0 || index >= Size) throw new ArgumentOutOfRangeException(nameof(index));
        data[index] = item;
    }

    // Add to end (same as insert at index == Size)
    public void Add(T item) => Add(item, Size);

    // Insert item at index (0..Size). This may require shifting elements.
    public void Add(T item, int index)
    {
        if (index < 0 || index > Size) throw new ArgumentOutOfRangeException(nameof(index));

        // Ensure we have at least one free slot in data[]
        EnsureCapacity(Size + 1);

        // Shift right to free one position at `index`.
        // We go from end to start to avoid overwriting.
        for (int i = Size; i > index; i--)
            data[i] = data[i - 1];

        // Put new element into freed slot
        data[index] = item;

        // Increase number of stored elements
        Size++;
    }

    // Remove element at index and return it.
    public T Remove(int index)
    {
        if (index < 0 || index >= Size) throw new ArgumentOutOfRangeException(nameof(index));

        // Save removed value to return it
        T removed = data[index];

        // Shift left to close the gap
        for (int i = index + 1; i < Size; i++)
            data[i - 1] = data[i];

        // Decrease logical size
        Size--;

        // Optional: clear last used slot
        data[Size] = default(T);

        return removed;
    }

    // Make sure internal storage can hold minCapacity elements.
    // If not, allocate a bigger array and copy the current items.
    private void EnsureCapacity(int minCapacity)
    {
        // If we already have enough space, do nothing
        if (data.Length >= minCapacity) return;

        // Start capacity
        int newCapacity = data.Length == 0 ? 1 : data.Length;

        // Multiply capacity until it becomes >= minCapacity
        while (newCapacity < minCapacity)
        {
            // newCapacity = ceil(newCapacity * factorNumerator / factorDenominator)
            long mul = (long)newCapacity * factorNumerator;
            newCapacity = (int)((mul + factorDenominator - 1) / factorDenominator);
        }

        // Allocate new storage
        T[] newData = new T[newCapacity];

        // Copy only existing elements (0..Size-1)
        for (int i = 0; i < Size; i++)
            newData[i] = data[i];

        // Replace internal array with bigger one
        data = newData;
    }
}
