# HW-03 — Junior: Algebraic Algorithms

This folder contains small demo implementations for basic algorithmic patterns used in the junior-level tasks.

Contents
- `Program.cs` — example implementations and quick checks for:
  - `PowerIterative(a, n)` — iterative exponentiation (O(n))
  - `FibonacciRecursive(n)` — naive recursive Fibonacci (O(2^n))
  - `FibonacciIterative(n)` — iterative Fibonacci (O(n))
  - `CountPrimesNaive(n)` — naive prime counting (O(n^2))

How to run

1. Open a terminal in this directory:

```bash
cd hw-03-algebraic-algorithms/junior
```

2. Run the demo program:

```bash
dotnet run
```

The program prints simple test checks and shows whether each routine returns the expected value.

What the code does (brief)

- `PowerIterative(a, n)`
  - Multiplies `a` by itself `n` times in a loop.
  - Loop-based, straightforward, complexity O(n).

- `FibonacciRecursive(n)`
  - Implements the recurrence F(0)=0, F(1)=1, F(n)=F(n-1)+F(n-2).
  - Simple direct recursion; runtime grows exponentially with `n` (very slow for n>40).

- `FibonacciIterative(n)`
  - Keeps two variables for the previous two Fibonacci numbers and advances them in a loop.
  - Linear time O(n) and constant extra memory.

- `CountPrimesNaive(n)`
  - Checks each number from 2..n for primality by trial division up to x-1.
  - Simple but slow: O(n^2) time. Useful for demonstration; replace with sieve for production.


