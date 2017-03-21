using System;
using System.Threading.Tasks;

namespace CompareSorts
{
    public static class Bubble
    {
        /* Sorts an array using Bubble sort algorithm. 
         * Prints updates if printUpdates is true.
         * Optimised: will only loop once through an array once after it's sorted,
         * and distinguishes between 'sorted' and 'unsorted' parts.
         * Time complexity: Worst = n^2, Best = n, Average = n^2.
         */
        public static int[] Sort(int[] array, bool printUpdates)
        {
            ulong comparisons = 0;
            ulong swaps = 0;
            // Get (length - 1) because we compare with (j + 1)
            int n = array.Length - 1;
            bool swapped = false;

            /* Loop through the array, checking that each element is smaller than the next.
             * If not, swap those two elements and mark 'swapped' to true.
             * Stop looping when the whole array has been checked without making a swap.
             */
            do
            {
                swapped = false;
                for (int j = 0; j < n; j++)
                {
                    // Since the next line will compare elements, add 1 to comparisons
                    comparisons++;
                    if (printUpdates)
                        Console.Write("^ Compare " + array[j].ToString().PadLeft(2) +
                            " with " + array[j + 1].ToString().PadLeft(2));
                    if ((array[j] > array[j + 1]))
                    {
                        // Swap them, set 'swapped' to true, count 1 more swap
                        ArrayOperations.Swap(array, j, j + 1);
                        swapped = true;
                        swaps++;

                        // Since a swap was made, print array in current state and say what was swapped
                        if (printUpdates)
                        {
                            Console.WriteLine(", it's bigger so swap:");
                            string message = "swapped " + array[j + 1].ToString().PadLeft(2) +
                                " with " + array[j].ToString().PadLeft(2);
                            ArrayOperations.Print(array, message);
                        }
                    }
                    else if (printUpdates)
                    {
                        Console.WriteLine(", it's not bigger so don't swap");
                    }
                }
                // After each loop, we know the biggest element must be sorted
                // So, it can be ignored on the next pass
                n--;
            } while (swapped);

            // Print number of comparisons and swaps that were performed
            if (printUpdates)
            {
                Console.WriteLine("Since no swaps were made this pass, we're done!");
                Console.WriteLine("\nNumber of comparisons: " + comparisons +
                                "\nNumber of swaps: " + swaps);
            }

            CountAndDisplay.totalComparisonsBubble += comparisons;
            CountAndDisplay.totalSwapsBubble += swaps;
            return array;
        }
    }
}
