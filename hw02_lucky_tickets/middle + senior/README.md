# Lucky Tickets - Middle & Senior Levels

## Problem Statement

Given a number **N**, a ticket has **2N digits**. A ticket is **lucky** if the sum of the first **N** digits equals the sum of the last **N** digits.

**Task:** Count all lucky tickets for a given N.

### Example: N = 1 (2-digit tickets)

```
Ticket format: [a][b]
Lucky condition: a = b

Possible lucky tickets:
00, 11, 22, 33, 44, 55, 66, 77, 88, 99
Total: 10 tickets
```

### Example: N = 2 (4-digit tickets)

```
Ticket format: [a][b] | [c][d]
Lucky condition: a+b = c+d

Examples:
Sum 0: 00|00 → 0000 (1 combination)
Sum 1: (01,10) × (01,10) → 0101, 0110, 1001, 1010 (4 combinations)
Sum 2: (02,11,20) × (02,11,20) → 9 combinations
...
```

---

## Algorithm: Dynamic Programming Approach

### Key Insight

Instead of checking all 2N digits independently (which would be O(10^(2N))), we:

1. **Count half-tickets** for each digit sum
2. **Multiply pairs** with matching sums

```
For each sum s:
  left_halves[s] = number of N-digit sequences with digit sum = s
  lucky_tickets += left_halves[s] × left_halves[s]
```

### Algorithm Steps

#### Step 1: Initialize for N=1
```
totals[0] = 1 (one half: "0")
totals[1] = 1 (one half: "1")
...
totals[9] = 1 (one half: "9")
```

#### Step 2: Build for N=2, N=3, etc.
For each new digit position, we add it to all previous sums:

```
totals[s] = sum of (ways to get sum (s-d) with previous digits + digit d)

For d = 0 to 9:
  For s = 0 to max_sum_previous:
    new_totals[s+d] += old_totals[s]
```

#### Step 3: Calculate Lucky Tickets
```
answer = Σ(totals[s])²  for all s
```

---

## Code Walkthrough

### Full Code Structure

```csharp
using System;
using System.IO;
using System.Diagnostics;

internal class Program
{
    static void Main()
    {
        // START TIMER
        // READ TEST FILES
        // RUN TESTS
        // MEASURE TIME
    }

    static long CountLuckyTickets(int N)
    {
        // IMPLEMENT DP ALGORITHM
    }
}
```

### Line-by-Line Explanation

#### Main Method

```csharp
static void Main()
{
    var stopwatch = Stopwatch.StartNew();
```
**What it does:** Starts a timer to measure total execution time.

```csharp
    string testDir = "tests";
    
    if (!Directory.Exists(testDir))
    {
        Console.WriteLine($"Test directory '{testDir}' not found.");
        return;
    }
```
**What it does:** Checks if the "tests" folder exists. If not, exits early.

```csharp
    var inputFiles = Directory.GetFiles(testDir, "*.in");
    int passed = 0;
    int failed = 0;
```
**What it does:** 
- Finds all files ending with `.in` in the tests folder
- Initializes counters for passed/failed tests

```csharp
    foreach (var inputFile in inputFiles)
    {
        string outputFile = inputFile.Replace(".in", ".out");
```
**What it does:** For each input file, creates the corresponding output filename by replacing `.in` with `.out`.

```csharp
        if (!File.Exists(outputFile))
        {
            Console.WriteLine($"Missing output file for {inputFile}");
            continue;
        }
```
**What it does:** Checks that the expected output file exists. If not, skip this test.

```csharp
        string input = File.ReadAllText(inputFile).Trim();
        string expectedOutput = File.ReadAllText(outputFile).Trim();
```
**What it does:** Reads both files into strings, removing extra whitespace.

```csharp
        int n = string.IsNullOrWhiteSpace(input) ? 3 : int.Parse(input);
        long result = CountLuckyTickets(n);
        string actualOutput = result.ToString();
```
**What it does:**
- Parses N from input (defaults to 3 if empty)
- Calls algorithm to count lucky tickets
- Converts result to string for comparison

```csharp
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
```
**What it does:** Compares actual vs expected output and prints result (✓ or ✗).

```csharp
    }

    stopwatch.Stop();
    Console.WriteLine($"\nPassed: {passed}, Failed: {failed}");
    Console.WriteLine($"Execution time: {stopwatch.ElapsedMilliseconds} ms");
```
**What it does:** Ends the timer and prints summary statistics.

---

#### CountLuckyTickets Method

```csharp
static long CountLuckyTickets(int N) 
{
    long[] totals = new long[N * 9 + 1];
    long[] digits = new long[N * 9 + 1];
```
**What it does:**
- `totals[s]` = number of N-digit sequences with digit sum = s
- `digits[s]` = temporary array to hold previous iteration's `totals`
- Maximum possible sum is N × 9 (all digits are 9)

**Visual:**
```
N=1: totals array size = 1*9+1 = 10
     [0][1][2][3][4][5][6][7][8][9]
      
N=2: totals array size = 2*9+1 = 19
     [0][1][2]...[17][18]
```

```csharp
    for (int d = 0; d <= 9; d++)
        totals[d] = 1;
```
**What it does:** Initializes for N=1. Each digit 0-9 appears exactly once, so each sum 0-9 has exactly 1 way.

**Visual:**
```
After initialization (N=1):
totals = [1, 1, 1, 1, 1, 1, 1, 1, 1, 1]
         [0] [1] [2] [3] [4] [5] [6] [7] [8] [9]  ← sum values
          ↑   ↑   ↑   ↑   ↑   ↑   ↑   ↑   ↑   ↑
          ways to make each sum
```

```csharp
    for (int j = 2; j <= N; j++)
    {
```
**What it does:** Loop to build sequences of length 2, 3, ..., up to N.

```csharp
        int maxSumNow = j * 9;       
        int maxSumPrev = maxSumNow - 9;
```
**What it does:** Calculates the range of possible sums.

**Example for j=2:**
```
maxSumNow = 2*9 = 18 (sum of two digits: 0+0 to 9+9)
maxSumPrev = 18-9 = 9 (sum of one digit: 0 to 9)
```

```csharp
        for (int s = 0; s <= maxSumPrev; s++)
            digits[s] = totals[s];
```
**What it does:** Copies the current `totals` into `digits` for safe reference while we recalculate.

```csharp
        for (int s = 0; s <= maxSumNow; s++)
            totals[s] = 0;
```
**What it does:** Resets `totals` to 0 before adding new values.

```csharp
        for (int d = 0; d <= 9; d++)
            for (int s = 0; s <= maxSumPrev; s++)
                totals[s + d] += digits[s];
```
**What it does:** The core DP update! For each digit d and each previous sum s, we add the count to the new sum s+d.

**Visual Example (N=1 → N=2):**
```
Before (j=1, stored in digits):
digits = [1, 1, 1, 1, 1, 1, 1, 1, 1, 1]
         [0] [1] [2] [3] [4] [5] [6] [7] [8] [9]

Adding digit 0:
totals[0+0] += digits[0] = 1
totals[1+0] += digits[1] = 1
...
totals[9+0] += digits[9] = 1

Adding digit 1:
totals[0+1] += digits[0] = 1  → totals[1] += 1
totals[1+1] += digits[1] = 1  → totals[2] += 1
...
totals[9+1] += digits[9] = 1  → totals[10] += 1

After processing all digits (d=0..9):
totals = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1]
         [0] [1] [2] [3] [4] [5] [6] [7] [8] [9] [10][11][12][13][14][15][16][17][18]
         
Meaning:
- Sum 0: 1 way (00)
- Sum 1: 2 ways (01, 10)
- Sum 2: 3 ways (02, 11, 20)
- Sum 9: 10 ways (09, 18, 27, 36, 45, 54, 63, 72, 81, 90)
- Sum 10: 9 ways (19, 28, 37, 46, 55, 64, 73, 82, 91)
```

```csharp
    }

    long luckyTickets = 0;
    for (int s = 0; s <= N * 9; s++)
        luckyTickets += totals[s] * totals[s];
    return luckyTickets;
```
**What it does:** For each sum s, multiply the left half count by the right half count, then sum all products.

**Visual Example (N=2):**
```
totals = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1]

For each sum s:
  luckyTickets += totals[s]²

Sum 0: 1² = 1
Sum 1: 2² = 4
Sum 2: 3² = 9
Sum 3: 4² = 16
...
Sum 9: 10² = 100
Sum 10: 9² = 81
...

Total = 1 + 4 + 9 + 16 + 25 + 36 + 49 + 64 + 81 + 100 + 81 + 64 + 49 + 36 + 25 + 16 + 9 + 4 + 1
      = 670
```

---

## Complexity Analysis

| Metric | Value |
|--------|-------|
| **Time Complexity** | O(N × 10 × N×9) = **O(N² × 10)** |
| **Space Complexity** | **O(N × 9)** |
| **vs Brute Force** | ~100× faster for N=3 |

### Why It's Fast

- **Brute force:** 10^6 = 1,000,000 iterations for N=3
- **DP:** ~2,700 iterations for N=3

---

## Test Files Format

### Input File (example.in)
```
3
```

### Output File (example.out)
```
55252
```

---

## Running the Program

```bash

# Create tests folder with test files
mkdir -p tests
echo "3" > tests/test1.in
echo "55252" > tests/test1.out

# Run program
dotnet run
```

### Expected Output
```
✓ test1.in
Passed: 1, Failed: 0
Execution time: 2 ms
```

---

## Key Takeaways

1. **Divide & Conquer:** Split 2N digits into two N-digit halves
2. **Dynamic Programming:** Build up digit sums incrementally
3. **Squaring:** Lucky tickets = Σ(ways to make sum s)²
4. **Efficiency:** Reduce from exponential to polynomial time

