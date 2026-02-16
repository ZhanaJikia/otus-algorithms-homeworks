# OTUS Algorithms — Homework 4 (C#)

**Language:** C#

## Goal
Implement different dynamic array algorithms and compare their performance.  
(Optional) implement PriorityQueue or SpaceArray.

---

## Task 1: Dynamic arrays

Implemented methods for each array:

- `void Add(T item, int index)`
- `T Remove(int index)`

Arrays:

1) **SingleArray**  
- No extra capacity  
- Every insert/remove allocates a new array and copies all elements

2) **VectorArray**  
- Has extra capacity  
- Grows by a fixed step `step` (example: +100)  
- When full, allocates a bigger array and copies elements

3) **FactorArray**  
- Has extra capacity  
- Grows by multiplication (example: x2)  
- Capacity grows like 1 → 2 → 4 → 8 → ...

4) **MatrixArray**  
- Stores elements in blocks: `T[][]`  
- Each block has fixed size `blockSize`  
- When full, adds a new block (no full copy of all elements)

---

## Task 2: Performance comparison

Benchmark uses `Stopwatch` and compares operations:

- `AddEnd`: `Add(item, Size)`
- `AddBegin`: `Add(item, 0)`
- `RemoveEnd`: `Remove(Size - 1)`
- `RemoveBegin`: `Remove(0)`

Input sizes tested (example):
- 1_000
- 5_000
- 10_000
- 20_000
- 40_000

---

## Conclusions

- **SingleArray** is the slowest because it reallocates and copies data on every insert/remove.
- **VectorArray** is faster than SingleArray because it reallocates less often (fixed step growth).
- **FactorArray** is usually the fastest for many adds because exponential growth makes reallocations rare.
- **MatrixArray** grows by blocks, so growth is cheap, but insert/remove still requires shifting elements (O(N)).

---

## How to run

1) Open the solution in Visual Studio  
2) Run the console project  
3) The console output prints correctness tests and benchmark timings
