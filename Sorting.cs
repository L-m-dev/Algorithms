using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public static class Sorting
    {
        public static int[] SelectionSortWithExtraStorage(int[] unsortedArray)
        {
            int[] sortedArray = new int[unsortedArray.Length];
            bool[] skips = new bool[unsortedArray.Length];
            int skipsCounter = 0;
            int len = unsortedArray.Length;
            int sortedArrayCounter = 0;
            int currentSmallestNumber = Int32.MaxValue;
            int currentSmallestIndex = -1;

            for (int i = 0; i < len; i++)
            {
                currentSmallestNumber = Int32.MaxValue;
                for (int j = 0; j < len; j++)
                {
                    if (skips[j])
                    {
                        continue;
                    }
                    if (unsortedArray[j] <= currentSmallestNumber)
                    {
                        currentSmallestNumber = unsortedArray[j];
                        currentSmallestIndex = j;
                    }
                }
                sortedArray[sortedArrayCounter++] = currentSmallestNumber;
                skips[currentSmallestIndex] = true;
            }
            return sortedArray;
        }

        public static int[] SelectionSortInPlace(int[] numberArray)
        {
            int sortedPosition = 0;
            for (int i = sortedPosition; i < numberArray.Length; i++)
            {
                int smallestNumberIndex = 0;
                int smallestNumber = numberArray[sortedPosition];
                for (int j = sortedPosition+1; j < numberArray.Length; j++)
                {
                    if (numberArray[j] <= smallestNumber)
                    {
                        smallestNumber = numberArray[j];
                        smallestNumberIndex = j;
                    }
                }
                if (numberArray[sortedPosition] != smallestNumber)
                {
                    Swap(numberArray, sortedPosition, smallestNumberIndex);
                }
             sortedPosition++;       

            }
            return numberArray;
        }
        public static void Swap(int[] array, int index, int target)
        {
            int aux = array[target];
            array[target] = array[index];
            array[index] = aux;
        }
    }
}
