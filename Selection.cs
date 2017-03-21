using System;
using System.Threading.Tasks;

namespace CompareSorts
{
    public static class Selection
    {
        /* Sorts an array using Selection sort algorithm. 
         * Prints updates if printUpdates is true.
         * Doesn't redundantly swap the last remaining element with itself.
         * Time complexity: Worst = n^2, Best = n^2, Average = n^2.
         */
        public static int[] Sort(int[] array, bool printUpdates)
        {
            ulong comparisons = 0;
            ulong swaps = 0;
            int n = array.Length;

            // The leftmost element will be skipped at each successive loop, since we know it's now sorted.
            // Also, the condition is ( < n-1 ) instead of ( < n ) to avoid swapping the last element with itself.
            for (int firstUnsortedIndex = 0; firstUnsortedIndex < n - 1; firstUnsortedIndex++)
            {
                if (printUpdates)
                    Console.WriteLine("Go through unsorted section to find minimum:");
                // Assume the first unsorted element is the smallest, store its index
                int min = array[firstUnsortedIndex];
                int indexOfMin = firstUnsortedIndex;
                // Iterate through the unsorted array and find the smallest element
                for (int currentIndex = firstUnsortedIndex + 1; currentIndex < n; currentIndex++)
                {
                    // Since the next line will compare elements, add 1 to comparisons
                    comparisons++;
                    if (printUpdates)
                        Console.WriteLine("^ Compare " + min.ToString().PadLeft(2) +
                            " with " + array[currentIndex].ToString().PadLeft(2));
                    if (array[currentIndex] < min)
                    {
                        min = array[currentIndex];
                        indexOfMin = currentIndex;
                    }
                }
                // Swap the smallest element with the first unsorted element. Count 1 more swap.
                ArrayOperations.Swap(array, indexOfMin, firstUnsortedIndex);
                swaps++;

                // Since a swap was made, print array in current state and say what was swapped
                if (printUpdates)
                {
                    Console.WriteLine("Minimum = {0}, swap with first unsorted element:", min);
                    string message = "swapped " + array[indexOfMin].ToString().PadLeft(2) +
                        " with " + array[firstUnsortedIndex].ToString().PadLeft(2);
                    ArrayOperations.Print(array, message);
                }
            }

            // Print number of comparisons and swaps that were performed
            if (printUpdates)
            {
                Console.WriteLine("Since there's only one element left, it must be in the right place and we're done!");
                Console.WriteLine("\nNumber of comparisons: " + comparisons +
                                "\nNumber of swaps: " + swaps);
            }

            CountAndDisplay.totalComparisonsSelection += comparisons;
            CountAndDisplay.totalSwapsSelection += swaps;
            return array;
        }
    }
}
