using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public static class Recursion
    {
        public static int Factorial(int n)
        {
            if (n == 1)
            {
                return 1;
            }
            return n * Factorial(n - 1);
        }

        public static int Sum(Span<int> numberArray, int i)
        {
            if (numberArray == null || numberArray.Length == 0)
            {
                throw new ArgumentNullException();
            }
            if (i >= numberArray.Length || i < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (i == 0)
            {
                return numberArray[0];
            }
            //pre-decrement.
            return numberArray[i] + Sum(numberArray, --i);
        }

        public static int CountItems(Span<int> numberArray, int i)
        {
         
            if (i == 0)
            {
                return 1;
            }
            //pre-decrement.
            return 1 + CountItems(numberArray, --i);
        }
    }
}

