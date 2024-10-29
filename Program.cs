﻿

using Algorithms;
using System.Diagnostics;
using System.Reflection;
using static Algorithms.Search;
using static Algorithms.Sorting;
using static Algorithms.Recursion;



Random random = new Random();
Stopwatch stopwatch = new Stopwatch();

int[] numberArray = new int[16];
for (int i = 0; i < numberArray.Length; i++)
{
    //multiply a prime
    numberArray[i] = 5 * random.Next(16);
}

int[] badCaseBinarySearch = new int[5] { 7, 7, 7, 7, 7 };

Array.Sort(numberArray);

 var numberIsInArray = BinarySearch(numberArray, 17);
  
numberIsInArray = BinarySearch(badCaseBinarySearch, 2);

int[] unsortedNumberArray = new int[16];
for (int i = 0; i < unsortedNumberArray.Length; i++)
{
    unsortedNumberArray[i] = 3 * random.Next(256);
}

Array.ForEach(unsortedNumberArray, element => Console.Write($"{element} "));

var sortedNumberArray = SelectionSortWithExtraStorage(unsortedNumberArray);

Console.WriteLine("\nSort:");
Array.ForEach(sortedNumberArray, element => Console.Write($"{element} "));

Console.WriteLine("\n");

Span<int> staticSpan = new int[5] { 1, 9, 3, 2, 5 };

int[] staticArray = new int[5] { 1, 9, 3, 2, 5 };

staticArray = SelectionSortInPlace(staticArray);
Array.ForEach(staticArray, element => Console.Write($"{element} "));

Console.WriteLine(Factorial(5));

Console.WriteLine(Sum(staticSpan, staticSpan.Length-1));

Console.WriteLine(CountItems(staticSpan, staticSpan.Length-1));

Console.ReadLine();




















