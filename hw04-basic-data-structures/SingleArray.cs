
using System;
// Dynamic array implementation

// SingleArray dynamic array.
// Key idea:
// - Internally it stores elements in a plain array: T[] data
// - There is NO extra capacity.
// - data.Length is always exactly equal to Size.
// - So every insert/remove must allocate a new array and copy elements.
//
// Pros: simplest to understand.
// Cons: very slow for many operations (lots of allocations + copies).

public class SingleArray<T>
{
    private T[] data = new T[0];
    public int Size { get; private set; }

    // Get element by index (O(1)).
    public T Get(int index)
    {
        if (index < 0 || index >= Size)
            throw new ArgumentOutOfRangeException(nameof(index));

        return data[index];
    }

    // Set element by index (O(1)).
    public void Set(T item, int index)
    {
        if (index < 0 || index >= Size)
            throw new ArgumentOutOfRangeException(nameof(index));
        data[index] = item;
    }

    
    // Insert item at specific index (O(N)).
    
    public void Add(T item, int index)
    {
        // Valid insertion positions are [0..Size].
        // index == Size means "insert at the end".
        if (index < 0 || index > Size)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }
       
       // Allocate a NEW array with exactly one more slot.
       T[] newData = new T[Size + 1];

        // Copy left part: [0..index-1] stays in the same position.
       for (int i =0;  i < index; i++)
       {
           newData[i] = data[i];
       }

       // Insert the new item.
       newData[index] = item;

       // Copy right part: [index..Size-1] moves one position to the right.
        for (int i = index; i < Size; i++)
        {
            newData[i + 1] = data[i];
        }

        // Replace old array with the new one.
        data = newData;
        Size++;
    }


    public void Remove(int index)
    {
        // Valid removal positions are [0..Size-1].

        if (index < 0 || index >= Size)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        T removed = data[index];

        // Allocate a NEW array with exactly one less slot.
        T[] newData = new T[Size - 1];
        // Copy left part: [0..index-1] stays in the same position.
        for (int i = 0; i < index; i++)        
        {
            newData[i] = data[i];           
        }

        // Copy right part: [index+1..Size-1] moves one position to the left.
        for (int i = index + 1; i < Size; i++)
        {
            newData[i - 1] = data[i];
        }

        // Replace old array with the new one.
        data = newData;
        Size--;
    }
}