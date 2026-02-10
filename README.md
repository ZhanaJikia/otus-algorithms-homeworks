# OTUS Algorithms Homeworks

Repository containing algorithm problem solutions for the OTUS course at different difficulty levels: **Junior**, **Middle**, and **Senior**.

## Project Structure

```
otus-algorithms-homeworks/
├── hw02_lucky_tickets/
│   ├── junior/       # Junior level solution
│   ├── middle/       # Middle level solution
│   └── senior/       # Senior level solution
└── README.md
```

## Homeworks

### HW02: Lucky Tickets

A lucky ticket is a 6-digit number where the sum of the first three digits equals the sum of the last three digits.

**Task:** Count all lucky tickets from 000000 to 999999.

#### Junior Level
- **Location:** `hw02_lucky_tickets/junior/`
- **Language:** C# (.NET)
- **Algorithm:** Brute force approach with nested loops
- **Time Complexity:** O(n⁵)

To run:
```bash
cd hw02_lucky_tickets/junior/LuckyTickets.Junior
dotnet run
```

#### Middle Level
- **Location:** `hw02_lucky_tickets/middle/`
- Coming soon...

#### Senior Level
- **Location:** `hw02_lucky_tickets/senior/`
- Coming soon...

## Requirements

- .NET SDK 6.0 or higher
- C# compiler

## Usage

Each level has its own project folder with a complete solution. Navigate to the specific level directory and run:

```bash
dotnet run
```

The program will display:
- The count of lucky tickets
- Execution time in milliseconds

## Learning Goals

- Implement algorithm solutions efficiently
- Compare different approaches (brute force, optimized, etc.)
- Understand time and space complexity
- Practice problem-solving at different difficulty levels

---

**Course:** OTUS  
**Language:** C#  
**Year:** 2026