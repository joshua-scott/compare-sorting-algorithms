using System;
using System.Threading.Tasks;

namespace CompareSorts
{
    public static class ArrayOperations
    {
        /* Returns an array of specified length.
         * Array's elements are ints dependent on 'orderOfArray':
         * "ascending" from 0 to length - 1
         * "descending" from length - 1 to 0
         * "random" from 0 - 99 inclusive (no check for uniqueness)
         */
        public static int[] Make(int length, string orderOfInitialArray)
        {
            Random rnd = new Random();

            int[] array = new int[length];
            for (int i = 0; i < length; i++)
            {
                switch (orderOfInitialArray)
                {
                    // Ascending:
                    case "ascending":
                        array[i] = i;
                        break;
                    // Descending:
                    case "descending":
                        array[i] = length - i - 1;
                        break;
                    // Random:
                    case "random":
                        array[i] = rnd.Next(100);
                        break;
                    // Default to ascending if incorrect parameter
                    default:
                        array[i] = i;
                        break;
                }
            }
            return array;
        }
        // Prints the current array with a message to describe the situation
        public static void Print(int[] array, string text)
        {
            int n = array.Length;
            for (int i = 0; i < n; i++)
                Console.Write(array[i].ToString().PadLeft(3));
            Console.WriteLine("  " + text);
        }

        //Swaps two ints in an array
        public static void Swap(int[] array, int p1, int p2)
        {
            int temp = array[p1];
            array[p1] = array[p2];
            array[p2] = temp;
        }
    }
}
