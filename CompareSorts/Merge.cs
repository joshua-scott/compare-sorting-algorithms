using System;
using System.Threading.Tasks;

namespace CompareSorts
{
    public static class Merge
    {
        /* Sorts an array using Mergesort algorithm, along with Merge().
         * Splits the array into two halves, then recursively calls Mergesort on each half.
         * Base case is when there's the array only contains one element (i.e. it must be sorted).
         * Sorted arrays are then Merged() to create larger sorted arrays.
         * Time complexity: Worst = n logn, Best = n logn, Average = n logn.
         * Space complexity: O(n) (note it's usually O(logn) for a linked list version)
         */
        public static void Sort(int[] array, bool printUpdates)
        {
            int n = array.Length;
            int mid = n / 2;

            // Base case
            if (n < 2)
            {
                if (printUpdates)
                    Console.WriteLine(" Base case reached");
                return;
            }

            // Split the array into two halves
            if (printUpdates)
                Console.WriteLine("Recursive call on left side, from index 0 to {0}:", mid - 1);
            int[] left = new int[mid];
            Array.Copy(array, 0, left, 0, mid);
            if (printUpdates)
                Console.WriteLine("Recursive call on right side, from index {0} to {1}:", mid, n - 1);
            int[] right = new int[n - mid];
            Array.Copy(array, mid, right, 0, n - mid);

            //Recursive call to sort the two halves
            Sort(left, printUpdates);
            Sort(right, printUpdates);

            // Call to merge the sorted halves into a sorted whole
            MergeArrays(array, left, right, printUpdates);
        }

        // Merges two sorted arrays into one sorted array
        public static void MergeArrays(int[] array, int[] leftArray, int[] rightArray, bool printUpdates)
        {
            ulong comparisons = 0;
            ulong swaps = 0;

            int leftCount = leftArray.Length;
            int rightCount = rightArray.Length;

            int l = 0; // index of left sub-array
            int r = 0; // index of right sub-array
            int m = 0; // index of merged array


            // Copy the next value in left and right sub-arrays to the main array
            while (l < leftCount && r < rightCount)
            {
                comparisons++;
                swaps++;
                if (leftArray[l] < rightArray[r])
                    array[m++] = leftArray[l++];
                else
                    array[m++] = rightArray[r++];
            }
            // If end of right subarray reached first, copy remaining elements from left subarray
            while (l < leftCount)
            {
                swaps++;
                array[m++] = leftArray[l++];
            }
            // If end of left subarray reached first, copy remaining elements from right subarray
            while (r < rightCount)
            {
                swaps++;
                array[m++] = rightArray[r++];
            }

            if (printUpdates)
                ArrayOperations.Print(array, "<- After a completed Merge of two sub-arrays");

            CountAndDisplay.totalComparisonsMerge += comparisons;
            CountAndDisplay.totalSwapsMerge += swaps;
        }

    }
}
