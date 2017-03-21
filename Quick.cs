using System;
using System.Threading.Tasks;

namespace CompareSorts
{
    public static class Quick
    {
        /* Sorts an array using Quick sort algorithm, along with Partition().
         * Calls Partition(), then makes recursive calls on the left and right of partitionIndex
         * Time complexity: Worst = n^2, Best = n logn, Average = n logn.
         */
        public static void Sort(int[] array, int start, int end, bool printUpdates)
        {
            // Base case
            if (start >= end)
            {
                if (printUpdates)
                    Console.WriteLine(" Base case reached");
                return;
            }

            // Call for partition
            int partitionIndex = Partition(array, start, end, printUpdates);
            // Left side recursive call
            if (printUpdates)
                Console.WriteLine("Recursive call on left side, from index {0} to {1}:", start, partitionIndex - 1);
            Sort(array, start, partitionIndex - 1, printUpdates);
            // Right side recursive call
            if (printUpdates)
                Console.WriteLine("Recursive call on right side, from index {0} to {1}:", partitionIndex + 1, end);
            Sort(array, partitionIndex + 1, end, printUpdates);
        }

        /* Sets pivot to some element in the given array, and partitionIndex to the first.
         * Iterates through each element and compares against the pivot.
         * If it's less than or equal to the pivot, swap it with partitionIndex++.
         * If it's greater than pivot, leave it alone.
         * Finally, swap the pivot with the partitionIndex.
         * Now, all elements <= pivot are on its left, and elements > pivot are on the right.
         */
        public static int Partition(int[] array, int start, int end, bool printUpdates)
        {
            ulong comparisons = 0;
            ulong swaps = 0;

            // Set pivot (one of three methods, see GetPivot method)
            int pivotIndex = GetPivot(array, start, end);
            int pivot = array[pivotIndex];
            // Set partitionIndex to start
            int partitionIndex = start;

            if (printUpdates)
                Console.WriteLine("Pivot is {0}, move < elements to left and >= to right", pivot);

            // Iterate from start to end - 1 (because end is always the pivot)
            for (int i = start; i < end; i++)
            {
                // Compare each element to the pivot
                comparisons++;
                if (array[i] < pivot)
                {
                    // If it's < pivot, swap to left of partition
                    ArrayOperations.Swap(array, i, partitionIndex);
                    partitionIndex++;
                    swaps++;
                }
            }

            // Move the pivot to the partition. Now, elements left are <= pivot, right are > pivot
            ArrayOperations.Swap(array, end, partitionIndex);
            swaps++;

            CountAndDisplay.totalComparisonsQuick += comparisons;
            CountAndDisplay.totalSwapsQuick += swaps;

            if (printUpdates)
            {
                string message = "Pivot was " + pivot.ToString().PadLeft(2) + ", moved to index " + partitionIndex.ToString().PadLeft(2);
                ArrayOperations.Print(array, message);
            }
            return partitionIndex;
        }

        /* Returns the pivot for Quicksort's Partition method.
         * Choice of three methods of finding the pivot: end, random, and median-of-three
         * Choose by setting the value of 'pivotMethod' a few lines down.
         * 
         * A note on median-of-three from Wikipedia:
         * "Although this approach optimizes quite well, it is typically outperformed in practice
         * by instead choosing random pivots, which has average linear time for selection and 
         * average log-linear time for sorting, and avoids the overhead of computing the pivot."
         */
        public static int GetPivot(int[] array, int start, int end)
        {
            int pivotMethod = 2;    //Set here: 0 = end, 1 = random, 2 = median-of-three

            // Method 1: Pivot = end
            // This gives Quicksort a running time O(n^2) in the worst case (already sorted)
            if (pivotMethod == 0)
                return end;

            // Method 2: Pivot = random
            else if (pivotMethod == 1)
            {
                // Pivot is the element at a random index in this part of the array
                Random rnd = new Random();
                int i = rnd.Next(start, end + 1);

                // Swap the pivot with the end (end is now pivot)
                ArrayOperations.Swap(array, i, end);
                CountAndDisplay.totalSwapsQuick++;

                return end;
            }

            // Method 3: Pivot = 'median of three', change the array as required
            else
            {
                int mid = (start + end) / 2;

                // Ensure start < mid < end
                if (array[start] > array[end])
                {
                    ArrayOperations.Swap(array, start, end);
                    CountAndDisplay.totalSwapsQuick++;
                }
                if (array[start] > array[mid])
                {
                    ArrayOperations.Swap(array, start, mid);
                    CountAndDisplay.totalSwapsQuick++;
                }
                if (array[mid] > array[end])
                {
                    ArrayOperations.Swap(array, mid, end);
                    CountAndDisplay.totalSwapsQuick++;
                }

                // Swap mid and end (end is now pivot)
                ArrayOperations.Swap(array, end, mid);
                CountAndDisplay.totalSwapsQuick++;

                return end;
            }
        }
    }
}
