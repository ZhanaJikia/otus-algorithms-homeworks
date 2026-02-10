# Lucky Tickets - Junior Level

## Overview
This is a solution for the "Lucky Tickets" algorithm problem at the junior level.
<p align="center">
  <img width="244" height="127" alt="Screenshot 2026-02-10 at 17 21 42" src="https://github.com/user-attachments/assets/260c858e-00f7-4f88-9bf8-7480177ab4b5" />
</p>



A lucky ticket is a 6-digit number where the sum of the first three digits equals the sum of the last three digits.

## Problem
Count the total number of lucky tickets from 000000 to 999999.

## Solution
The program uses nested loops to:
1. Iterate through all combinations of the first three digits (a, a1, a2)
2. Calculate their sum
3. For each sum, find all valid combinations of the last three digits (b, b1, b3) where b3 is derived from the sum

## Running the Program

```bash
dotnet run
```

## Output
The program displays:
- Total count of lucky tickets
- Execution time in milliseconds

## Time Complexity
O(n⁵) where n = 10 (digits 0-9)

