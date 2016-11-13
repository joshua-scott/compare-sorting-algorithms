using System;
using System.Threading.Tasks;

namespace CompareSorts
{
    class Program
    {
        const string a = "ascending";
        const string d = "descending";
        const string r = "random";

        const string B = "Bubble";
        const string S = "Selection";
        const string I = "Insertion";
        const string Q = "Quick";
        const string M = "Merge";
        const string A = "All";

        // Variables required to count the number of operations
        // Unsigned longs used in all cases to be safe and avoid casting, though not required for all
        // Bubble:
        static ulong totalComparisonsBubble = 0;
        static ulong totalSwapsBubble = 0;
        // Selection:
        static ulong totalComparisonsSelection = 0;
        static ulong totalSwapsSelection = 0;
        // Insertion:
        static ulong totalComparisonsInsertion = 0;
        static ulong totalShuffles = 0;
        static ulong totalInsertions = 0;
        //Quick:
        static ulong totalComparisonsQuick = 0;
        static ulong totalSwapsQuick = 0;
        //Merge:
        static ulong totalComparisonsMerge = 0;
        static ulong totalSwapsMerge = 0;

        static void Main(string[] args)
        {
            /*
             * Edit these variables to set up!
             */
            string sortingAlgorithm = A;    // B/S/I/Q/M/A
            string orderOfArray = r;        // a/d/r
            ulong numberOfSorts = 1;
            int lengthOfArray = 10;
            bool printUpdates = true;

            Console.WriteLine("Sorting algorithm: {0}\nResults of {1} passes of {2}-int {3} arrays:\n",
                sortingAlgorithm, numberOfSorts, lengthOfArray, orderOfArray);

            /* Initialise specified number of arrays of ints
             * Pass each one to Bubble||Selection||Insertion||Quick sort
             * Count total number of comparisons/swaps/shuffles/insertions as required
             */
            for (int i = 0; i < (int)numberOfSorts; i++)
            {
                if (numberOfSorts > 1 && printUpdates == true)
                    Console.WriteLine("\n\nRound {0}:\n", (i + 1));

                // Create array
                int[] array = MakeArray(lengthOfArray, orderOfArray);

                if (printUpdates && sortingAlgorithm != A)
                    PrintArray(array, "initial array");

                // Call Bubble||Selection||Insertion||Quick||Merge sort, and add correct variables
                if (sortingAlgorithm == B)
                    BubbleSort(array, printUpdates);
                else if (sortingAlgorithm == S)
                    SelectionSort(array, printUpdates);
                else if (sortingAlgorithm == I)
                    InsertionSort(array, printUpdates);
                else if (sortingAlgorithm == Q)
                    QuickSort(array, 0, array.Length - 1, printUpdates);
                else if (sortingAlgorithm == M)
                    MergeSort(array, printUpdates);
                else if (sortingAlgorithm == A)
                {
                    // Copy each array (since they're arrays of ints, shallow copy is enough)
                    int[] array1 = (int[])array.Clone();
                    int[] array2 = (int[])array.Clone();
                    int[] array3 = (int[])array.Clone();
                    int[] array4 = (int[])array.Clone();

                    if (printUpdates)
                    {
                        Console.WriteLine("\nBubble sort:");
                        PrintArray(array, "initial array");
                    }
                    BubbleSort(array, printUpdates);

                    if (printUpdates)
                    {
                        Console.WriteLine("\nSelection sort:");
                        PrintArray(array, "initial array");
                    }
                    SelectionSort(array1, printUpdates);

                    if (printUpdates)
                    {
                        Console.WriteLine("\nInsertion sort:");
                        PrintArray(array, "initial array");
                    }
                    InsertionSort(array2, printUpdates);

                    if (printUpdates)
                    {
                        Console.WriteLine("\nQuick sort:");
                        PrintArray(array, "initial array");
                    }
                    QuickSort(array3, 0, array3.Length - 1, printUpdates);

                    if (printUpdates)
                    {
                        Console.WriteLine("\nMerge sort:");
                        PrintArray(array, "initial array");
                    }
                    MergeSort(array4, printUpdates);

                    if (printUpdates)
                        Console.WriteLine();
                }
            }

            DisplayResults(sortingAlgorithm, numberOfSorts, printUpdates);

            Console.ReadKey();
        }

        /* Returns an array of specified length.
         * Array's elements are ints dependent on 'orderOfArray':
         * "ascending" from 0 to length - 1
         * "descending" from length - 1 to 0
         * "random" from 0 - 99 inclusive (no check for uniqueness)
         */
        private static int[] MakeArray(int length, string orderOfArray)
        {
            Random rnd = new Random();

            int[] array = new int[length];
            for (int i = 0; i < length; i++)
            {
                switch (orderOfArray)
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

        /* Sorts an array using Bubble sort algorithm. 
         * Prints updates if printUpdates is true.
         * Optimised: will only loop once through an array once after it's sorted,
         * and distinguishes between 'sorted' and 'unsorted' parts.
         * Time complexity: Worst = n^2, Best = n, Average = n^2.
         */
        private static int[] BubbleSort(int[] array, bool printUpdates)
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
                        Swap(array, j, j + 1);
                        swapped = true;
                        swaps++;

                        // Since a swap was made, print array in current state and say what was swapped
                        if (printUpdates)
                        {
                            Console.WriteLine(", it's bigger so swap:");
                            string message = "swapped " + array[j + 1].ToString().PadLeft(2) +
                                " with " + array[j].ToString().PadLeft(2);
                            PrintArray(array, message);
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

            totalComparisonsBubble += comparisons;
            totalSwapsBubble += swaps;
            return array;
        }

        /* Sorts an array using Selection sort algorithm. 
         * Prints updates if printUpdates is true.
         * Doesn't redundantly swap the last remaining element with itself.
         * Time complexity: Worst = n^2, Best = n^2, Average = n^2.
         */
        private static int[] SelectionSort(int[] array, bool printUpdates)
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
                Swap(array, indexOfMin, firstUnsortedIndex);
                swaps++;

                // Since a swap was made, print array in current state and say what was swapped
                if (printUpdates)
                {
                    Console.WriteLine("Minimum = {0}, swap with first unsorted element:", min);
                    string message = "swapped " + array[indexOfMin].ToString().PadLeft(2) +
                        " with " + array[firstUnsortedIndex].ToString().PadLeft(2);
                    PrintArray(array, message);
                }
            }

            // Print number of comparisons and swaps that were performed
            if (printUpdates)
            {
                Console.WriteLine("Since there's only one element left, it must be in the right place and we're done!");
                Console.WriteLine("\nNumber of comparisons: " + comparisons +
                                "\nNumber of swaps: " + swaps);
            }

            totalComparisonsSelection += comparisons;
            totalSwapsSelection += swaps;
            return array;
        }

        /* Sorts an array using Insertion sort algorithm. 
         * Prints updates if printUpdates is true.
         * At the end, prints a summary of the number of comparisons, shuffles, and insertions.
         * Time complexity: Worst = n^2, Best = n, Average = n^2.
         */
        private static int[] InsertionSort(int[] array, bool printUpdates)
        {
            // Since elements aren't directly swapped, it doesn't make sense to count swaps like the other algorithms.
            // Instead, we count shuffles (when an element moves up one index) 
            // and insertions (where the currentElement is inserted into its correct index for that pass)
            ulong comparisons = 0;
            ulong shuffles = 0;
            ulong insertions = 0;
            int n = array.Length;

            // We begin at index 1 since we will compare with the element before it
            for (int i = 1; i < n; i++)
            {
                // Store the current element's value and index
                int currentElement = array[i];
                int j = i;

                // The next line will compare two elements, so add 1 to comparisons
                comparisons++;
                if (printUpdates && j > 0)
                    Console.Write("^ Compare " + currentElement.ToString().PadLeft(2) +
                        " with " + array[j - 1].ToString().PadLeft(2));
                bool endReached = false;
                // Shuffle the required number of previous elements, to 'make room' for the currentElement
                while (j > 0 && array[j - 1] > currentElement)
                {
                    array[j] = array[j - 1];
                    j--;
                    // An element was just moved by one index, so add 1 to shuffles
                    shuffles++;

                    if (printUpdates)
                        Console.WriteLine(", {0} is bigger so we shuffle it one place up to index {1}", array[j], j);

                    // The while condition will now compare two elements again (unless j is now 0), so add 1 to comparisons
                    if (j != 0)
                    {
                        comparisons++;
                        if (printUpdates)
                            Console.Write("^ Compare " + currentElement.ToString().PadLeft(2) +
                                " with " + array[j - 1].ToString().PadLeft(2));
                    }
                    else if (printUpdates)
                    {
                        endReached = true;
                        Console.WriteLine("We've reached the first index so insert {0} here:", currentElement.ToString().PadLeft(2));
                    }
                }

                if (printUpdates && !endReached)
                {
                    Console.WriteLine(", it's not smaller so we insert {0} here:", currentElement.ToString().PadLeft(2));
                }

                // Insert the currentElement in its correct index for this pass, and add 1 to insertions
                array[j] = currentElement;
                insertions++;

                // Since an insertion was made, print array in current state and say what was inserted
                if (printUpdates)
                {
                    string message = "inserted " + currentElement.ToString().PadLeft(2) +
                        " to index " + j.ToString().PadLeft(2);
                    PrintArray(array, message);
                }
            }

            // Print number of comparisons, shuffles and insertions that were performed
            if (printUpdates)
            {
                Console.WriteLine("Since that was the last element, we're done!");
                Console.WriteLine("\nNumber of comparisons: " + comparisons +
                        "\nNumber of shuffles: " + shuffles +
                        "\nNumber of insertions: " + insertions);
            }

            totalComparisonsInsertion += comparisons;
            totalShuffles += shuffles;
            totalInsertions += insertions;
            return array;
        }

        /* Sorts an array using Quick sort algorithm, along with Partition().
         * Calls Partition(), then makes recursive calls on the left and right of partitionIndex
         * Time complexity: Worst = n^2, Best = n logn, Average = n logn.
         */
        private static void QuickSort(int[] array, int start, int end, bool printUpdates)
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
            QuickSort(array, start, partitionIndex - 1, printUpdates);
            // Right side recursive call
            if (printUpdates)
                Console.WriteLine("Recursive call on right side, from index {0} to {1}:", partitionIndex + 1, end);
            QuickSort(array, partitionIndex + 1, end, printUpdates);
        }

        /* Sets pivot to the last element in given array, and partitionIndex to the first.
         * Iterates through each element and compares against the pivot.
         * If it's less than or equal to the pivot, swap it with partitionIndex++.
         * If it's greater than pivot, leave it alone.
         * Finally, swap the pivot with the partitionIndex.
         * Now, all elements <= pivot are on its left, and elements > pivot are on the right.
         */
        private static int Partition(int[] array, int start, int end, bool printUpdates)
        {
            ulong comparisons = 0;
            ulong swaps = 0;

            // Set pivot to end and partitionIndex to start
            int pivot = array[end];
            int partitionIndex = start;
            if (printUpdates)
                Console.WriteLine("Pivot is {0}, move <= elements to left and > to right", pivot);

            // Iterate from start to end
            for (int i = start; i < end; i++)
            {
                // Compare each element to the pivot
                comparisons++;
                if (array[i] <= pivot)
                {
                    // If it's <= pivot, swap to left of partition
                    Swap(array, i, partitionIndex);
                    partitionIndex++;
                    swaps++;
                }
            }

            // Move the pivot to the partition. Now, elements left are <= pivot, right are > pivot
            Swap(array, end, partitionIndex);
            swaps++;

            totalComparisonsQuick += comparisons;
            totalSwapsQuick += swaps;

            PrintArray(array, "Pivot moved to index " + partitionIndex.ToString().PadLeft(2));
            return partitionIndex;
        }

        /* Sorts an array using Mergesort algorithm, along with Merge().
         * Splits the array into two halves, then recursively calls Mergesort on each half.
         * Base case is when there's the array only contains one element (i.e. it must be sorted).
         * Sorted arrays are then Merged() to create larger sorted arrays.
         * Time complexity: Worst = n logn, Best = n logn, Average = n logn.
         * Space complexity: O(n) (note it's usually O(logn) for a linked list version)
         */
        private static void MergeSort(int[] array, bool printUpdates)
        {
            int n = array.Length;
            int mid = n/2;

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
            MergeSort(left, printUpdates);
            MergeSort(right, printUpdates);

            // Call to merge the sorted halves into a sorted whole
            Merge(array, left, right, printUpdates);
        }

        // Merges two sorted arrays into one sorted array
        private static void Merge(int[] array, int[] leftArray, int[] rightArray, bool printUpdates)
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
                PrintArray(array, "<- After a completed Merge of two sub-arrays");

            totalComparisonsMerge += comparisons;
            totalSwapsMerge += swaps;
        }

        // Prints the current array with a message to describe the situation
        private static void PrintArray(int[] array, string text)
        {
            int n = array.Length;
            for (int i = 0; i < n; i++)
                Console.Write(array[i].ToString().PadLeft(3));
            Console.WriteLine("  " + text);
        }

        //Swaps two ints in an array
        private static void Swap(int[] array, int p1, int p2)
        {
            int temp = array[p1];
            array[p1] = array[p2];
            array[p2] = temp;
        }

        // Calculate and display relevant totals and averages:
        private static void DisplayResults(string sortingAlgorithm, ulong numberOfSorts, bool printUpdates)
        {
            if (numberOfSorts > 1 && printUpdates)
                Console.WriteLine("\n");

            if (sortingAlgorithm == "Bubble" || sortingAlgorithm == "All")
            {
                ulong averageComparisonsBubble = totalComparisonsBubble / numberOfSorts;
                ulong averageSwapsBubble = totalSwapsBubble / numberOfSorts;
                if (sortingAlgorithm == "All")
                    Console.WriteLine("Bubble sort results:\n");
                Console.WriteLine("Total comparisons: {0}\nAverage comparisons: {1}\n", totalComparisonsBubble, averageComparisonsBubble);
                Console.WriteLine("Total swaps: {0}\nAverage swaps: {1}\n", totalSwapsBubble, averageSwapsBubble);
            }
            if (sortingAlgorithm == "Selection" || sortingAlgorithm == "All")
            {
                ulong averageComparisonsSelection = totalComparisonsSelection / numberOfSorts;
                ulong averageSwapsSelection = totalSwapsSelection / numberOfSorts;
                if (sortingAlgorithm == "All")
                    Console.WriteLine("Selection sort results:\n");
                Console.WriteLine("Total comparisons: {0}\nAverage comparisons: {1}\n", totalComparisonsSelection, averageComparisonsSelection);
                Console.WriteLine("Total swaps: {0}\nAverage swaps: {1}\n", totalSwapsSelection, averageSwapsSelection);
            }
            if (sortingAlgorithm == "Insertion" || sortingAlgorithm == "All")
            {
                ulong averageComparisonsInsertion = totalComparisonsInsertion / numberOfSorts;
                ulong averageShuffles = totalShuffles / numberOfSorts;
                ulong averageInsertions = totalInsertions / numberOfSorts;
                if (sortingAlgorithm == "All")
                    Console.WriteLine("Insertion sort results:\n");
                Console.WriteLine("Total comparisons: {0}\nAverage comparisons: {1}\n", totalComparisonsInsertion, averageComparisonsInsertion);
                Console.WriteLine("Total shuffles: {0}\nAverage shuffles: {1}\n", totalShuffles, averageShuffles);
                Console.WriteLine("Total insertions: {0}\nAverage insertions: {1}\n", totalInsertions, averageInsertions);
            }
            if (sortingAlgorithm == "Quick" || sortingAlgorithm == "All")
            {
                ulong averageComparisonsQuick = totalComparisonsQuick / numberOfSorts;
                ulong averageSwapsQuick = totalSwapsQuick / numberOfSorts;
                if (sortingAlgorithm == "All")
                    Console.WriteLine("Quick sort results:\n");
                Console.WriteLine("Total comparisons: {0}\nAverage comparisons: {1}\n", totalComparisonsQuick, averageComparisonsQuick);
                Console.WriteLine("Total swaps: {0}\nAverage swaps: {1}\n", totalSwapsQuick, averageSwapsQuick);
            }
            if (sortingAlgorithm == "Merge" || sortingAlgorithm == "All")
            {
                ulong averageComparisonsMerge = totalComparisonsMerge / numberOfSorts;
                ulong averageSwapsMerge = totalSwapsMerge / numberOfSorts;
                if (sortingAlgorithm == "All")
                    Console.WriteLine("Merge sort results:\n");
                Console.WriteLine("Total comparisons: {0}\nAverage comparisons: {1}\n", totalComparisonsMerge, averageComparisonsMerge);
                Console.WriteLine("Total swaps: {0}\nAverage swaps: {1}\n", totalSwapsMerge, averageSwapsMerge);
            }
        }
    }
}
