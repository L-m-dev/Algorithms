
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public static class Search
    {
        public static bool BinarySearch(Int32[] numberArray, int target)
        {
            int low = 0;
            int high = numberArray.Length - 1;
            int counter = 0;
            while (low <= high){
                int middle = (low + high) / 2;
                counter++;

                Array.ForEach(numberArray, element =>
                {
                    Console.Write($"{element} ");
                });

                Console.WriteLine($"\n\nTarget: {target}\nLow: {numberArray[low]}\nMiddle: {numberArray[middle]}\nHigh: {numberArray[high]}");
                Console.WriteLine($"Current Loop: {counter}");

                //Adding this for clarity when not finding a value.
                if (numberArray[low] == numberArray[middle] &&
                   numberArray[middle] == numberArray[high] &&
                    target != numberArray[low])
                {
                    Console.WriteLine("Item not found.");
                    return false;
                }
                if (target == numberArray[middle]){
                    Console.WriteLine("Item found!");
                    return true;
                }
                if (target < numberArray[middle]){
                    high = middle - 1;
                }
                if (target > numberArray[middle]){
                    low = middle + 1;
                }
             }
            return false;
        }
    }
}
