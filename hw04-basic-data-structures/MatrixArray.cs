using System;

// MatrixArray = dynamic array implemented as "array of blocks" (T[][]).
//
// Idea:
// - Instead of one big T[] array, we store elements in fixed-size blocks.
// - Each block is a T[blockSize] array.
// - When we need more space, we add ONE more block (no need to copy all elements).
//
// Example blockSize = 3, elements:
// index: 0 1 2 | 3 4 5 | 6 7 8
// block: 0 0 0 | 1 1 1 | 2 2 2
//
// Mapping index -> (block, offset):
// block = index / blockSize
// offset = index % blockSize
public class MatrixArray<T>
{
    // Array of blocks. Each block is a small array of fixed size.
    private T[][] blocks = new T[0][];

    // Size of each block (chunk)
    private readonly int blockSize;

    // Number of elements stored
    public int Size { get; private set; }

    public MatrixArray(int blockSize = 100)
    {
        if (blockSize <= 0) throw new ArgumentOutOfRangeException(nameof(blockSize));
        this.blockSize = blockSize;
    }

    public T Get(int index)
    {
        if (index < 0 || index >= Size) throw new ArgumentOutOfRangeException(nameof(index));

        // Find which block and offset contain this index
        int b = index / blockSize;
        int o = index % blockSize;

        return blocks[b][o];
    }

    public void Set(T item, int index)
    {
        if (index < 0 || index >= Size) throw new ArgumentOutOfRangeException(nameof(index));

        int b = index / blockSize;
        int o = index % blockSize;

        blocks[b][o] = item;
    }

    // Add to end (same as insert at index == Size)
    public void Add(T item) => Add(item, Size);

    // Insert item at index (0..Size)
    public void Add(T item, int index)
    {
        if (index < 0 || index > Size) throw new ArgumentOutOfRangeException(nameof(index));

        // Ensure there is a free slot at index == Size (new last position).
        // If the last block is full, allocate a new block.
        EnsureSlotForOneMore();

        // Shift right from the end to the insertion index.
        // We treat MatrixArray like a "big array", using Read/Write helpers.
        for (int i = Size; i > index; i--)
            Write(i, Read(i - 1));

        // Put new item into the freed slot
        Write(index, item);

        // Increase element count
        Size++;
    }

    // Remove element at index and return it
    public T Remove(int index)
    {
        if (index < 0 || index >= Size) throw new ArgumentOutOfRangeException(nameof(index));

        // Save removed value
        T removed = Read(index);

        // Shift left to close the gap
        for (int i = index + 1; i < Size; i++)
            Write(i - 1, Read(i));

        // Decrease size
        Size--;

        // Optional: clear last slot
        Write(Size, default(T));

        return removed;
    }

    // Read without bounds check (internal helper).
    // It also works for index == Size when we shift after EnsureSlotForOneMore.
    private T Read(int index)
    {
        int b = index / blockSize;
        int o = index % blockSize;
        return blocks[b][o];
    }

    // Write without bounds check (internal helper).
    private void Write(int index, T value)
    {
        int b = index / blockSize;
        int o = index % blockSize;
        blocks[b][o] = value;
    }

    // Make sure we can store one more element.
    // If Size+1 needs a new block, add it.
    private void EnsureSlotForOneMore()
    {
        int neededSize = Size + 1;

        // Number of blocks needed to store neededSize elements
        int neededBlocks = (neededSize + blockSize - 1) / blockSize;

        // If we already have enough blocks, do nothing
        if (blocks.Length >= neededBlocks)
            return;

        // Grow blocks array by +1 block (simplest way).
        // We allocate a new T[][], copy old block references, and add a new block.
        T[][] newBlocks = new T[blocks.Length + 1][];

        for (int i = 0; i < blocks.Length; i++)
            newBlocks[i] = blocks[i];

        // Create the new block itself
        newBlocks[newBlocks.Length - 1] = new T[blockSize];

        blocks = newBlocks;
    }
}
