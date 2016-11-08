using System;
using System.Threading.Tasks;

namespace PsetS3
{
    class Program
    {
        const string a = "ascending";
        const string d = "descending";
        const string r = "random";
        
        const string B = "Bubble";
        const string S = "Selection";
        const string I = "Insertion";
        const string A = "All";

        // Variables required to count the number of operations
        // Unsigned longs used in all cases to be safe and avoid casting, though not required for all
        // Bubble:
        static ulong countComparisonsBubble = 0;
        static ulong totalComparisonsBubble = 0;
        static ulong averageComparisonsBubble = 0;
        static ulong countSwapsBubble = 0;
        static ulong totalSwapsBubble = 0;
        static ulong averageSwapsBubble = 0;
        // Selection:
        static ulong countComparisonsSelection = 0; 
        static ulong totalComparisonsSelection = 0;
        static ulong averageComparisonsSelection = 0;
        static ulong countSwapsSelection = 0;
        static ulong totalSwapsSelection = 0;
        static ulong averageSwapsSelection = 0;
        // Insertion:
        static ulong countComparisonsInsertion = 0;
        static ulong totalComparisonsInsertion = 0;
        static ulong averageComparisonsInsertion = 0;
        static ulong countShuffles;
        static ulong totalShuffles = 0;
        static ulong averageShuffles = 0;
        static ulong countInsertions = 0;
        static ulong totalInsertions = 0;
        static ulong averageInsertions = 0;

        static void Main(string[] args)
        {
            /*
             * Edit these variables to set up!
             */
            string sortingAlgorithm = A;    // B/S/I/A
            string orderOfArray = r;        // a/d/r
            ulong numberOfSorts = 10;
            int lengthOfArray = 10;
            bool printUpdates = true;
            
            Console.WriteLine("Sorting algorithm: {0}\nResults of {1} passes of {2}-int {3} arrays:", 
                sortingAlgorithm, numberOfSorts, lengthOfArray, orderOfArray);
            
            /* Initialise specified number of arrays of ints
             * Pass each one to Bubble||Selection||Insertion sort
             * Count total number of comparisons/swaps/shuffles/insertions as required
             */
            for (int i = 0; i < (int)numberOfSorts; i++)
            {
                if (numberOfSorts > 1 && printUpdates == true)
                    Console.WriteLine("\n\n\nRound {0}:\n", (i + 1));

                // Create array
                int[] array = MakeArray(lengthOfArray, orderOfArray);

                // Call Bubble||Selection||Insertion sort, and add correct variables
                if (sortingAlgorithm == B)
                {
                    BubbleSort(array, printUpdates, out countComparisonsBubble, out countSwapsBubble);
                    totalComparisonsBubble += countComparisonsBubble;
                    totalSwapsBubble += countSwapsBubble;
                }
                else if (sortingAlgorithm == S)
                {
                    SelectionSort(array, printUpdates, out countComparisonsSelection, out countSwapsSelection);
                    totalComparisonsSelection += countComparisonsSelection;
                    totalSwapsSelection += countSwapsSelection;
                }
                else if (sortingAlgorithm == I)
                {
                    InsertionSort(array, printUpdates, out countComparisonsInsertion, out countShuffles, out countInsertions);
                    totalComparisonsInsertion += countComparisonsInsertion;
                    totalShuffles += countShuffles;
                    totalInsertions += countInsertions;
                }
                else if (sortingAlgorithm == A)
                {
                    // Copy each array (since they're arrays of ints, shallow copy is enough)
                    int[] array1 = (int[])array.Clone();
                    int[] array2 = (int[])array.Clone();

                    if (printUpdates)
                        Console.WriteLine("Bubble sort:");
                    BubbleSort(array, printUpdates, out countComparisonsBubble, out countSwapsBubble);
                    totalComparisonsBubble += countComparisonsBubble;
                    totalSwapsBubble += countSwapsBubble;

                    if (printUpdates)
                        Console.WriteLine("\nSelection sort:");
                    SelectionSort(array1, printUpdates, out countComparisonsSelection, out countSwapsSelection);
                    totalComparisonsSelection += countComparisonsSelection;
                    totalSwapsSelection += countSwapsSelection;

                    if (printUpdates)
                        Console.WriteLine("\nInsertion sort:");
                    InsertionSort(array2, printUpdates, out countComparisonsInsertion, out countShuffles, out countInsertions);
                    totalComparisonsInsertion += countComparisonsInsertion;
                    totalShuffles += countShuffles;
                    totalInsertions += countInsertions;

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
        private static int[] BubbleSort(int[] array, bool printUpdates, out ulong countComparisonsBubble, out ulong countSwapsBubble)
        {
            if (printUpdates)
                PrintArray(array, "initial array");

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
                    if ((array[j] > array[j + 1]))
                    {
                        // Swap them, set 'swapped' to true, count 1 more swap
                        Swap(array, j, j + 1);
                        swapped = true;
                        swaps++;

                        // Since a swap was made, print array in current state and say what was swapped
                        if (printUpdates)
                        {
                            string message = "swapped " + array[j + 1].ToString().PadLeft(2) +
                                " with " + array[j].ToString().PadLeft(2);
                            PrintArray(array, message);
                        }
                    }
                }
                // After each loop, we know the biggest element must be sorted
                // So, it can be ignored on the next pass
                n--;
            } while (swapped);

            // Print number of comparisons and swaps that were performed
            if (printUpdates)
                Console.WriteLine("\nNumber of comparisons: " + comparisons +
                                "\nNumber of swaps: " + swaps);

            countComparisonsBubble = comparisons;
            countSwapsBubble = swaps;
            return array;
        }

        /* Sorts an array using Selection sort algorithm. 
         * Prints updates if printUpdates is true.
         * Doesn't redundantly swap the last remaining element with itself.
         * Time complexity: Worst = n^2, Best = n^2, Average = n^2.
         */
        private static int[] SelectionSort(int[] array, bool printUpdates, out ulong countComparisons, out ulong countSwaps)
        {
            if (printUpdates)
                PrintArray(array, "initial array");

            ulong comparisons = 0;
            ulong swaps = 0;
            int n = array.Length;

            // The leftmost element will be skipped at each successive loop, since we know it's now sorted.
            // Also, the condition is ( < n-1 ) instead of ( < n ) to avoid swapping the last element with itself.
            for (int firstUnsortedIndex = 0; firstUnsortedIndex < n - 1; firstUnsortedIndex++)
            {
                // Assume the first unsorted element is the smallest, store its index
                int min = array[firstUnsortedIndex];
                int indexOfMin = firstUnsortedIndex;
                // Iterate through the unsorted array and find the smallest element
                for (int currentIndex = firstUnsortedIndex + 1; currentIndex < n; currentIndex++)
                {
                    // Since the next line will compare elements, add 1 to comparisons
                    comparisons++;
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
                    string message = "swapped " + array[indexOfMin].ToString().PadLeft(2) +
                    " with " + array[firstUnsortedIndex].ToString().PadLeft(2);
                    PrintArray(array, message);
                }
            }

            // Print number of comparisons and swaps that were performed
            if (printUpdates)
                Console.WriteLine("\nNumber of comparisons: " + comparisons +
                                "\nNumber of swaps: " + swaps);

            countComparisons = comparisons;
            countSwaps = swaps;
            return array;
        }

        /* Sorts an array using Insertion sort algorithm. 
         * Prints updates if printUpdates is true.
         * At the end, prints a summary of the number of comparisons, shuffles, and insertions.
         * Time complexity: Worst = n^2, Best = n, Average = n^2.
         */
        private static int[] InsertionSort(int[] array, bool printUpdates, out ulong countComparisonsInsertion, out ulong countShuffles, out ulong countInsertions)
        {
            if (printUpdates)
                PrintArray(array, "initial array");

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
                // Shuffle the required number of previous elements, to 'make room' for the currentElement
                while (j > 0 && array[j - 1] > currentElement)
                {
                    array[j] = array[j - 1];
                    j--;
                    // An element was just moved by one index, so add 1 to shuffles
                    shuffles++;
                    // The while condition will now compare two elements again (unless j is now 0), so add 1 to comparisons
                    if (j != 0)
                        comparisons++;
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
                Console.WriteLine("\nNumber of comparisons: " + comparisons +
                        "\nNumber of shuffles: " + shuffles +
                        "\nNumber of insertions: " + insertions);

            countComparisonsInsertion = comparisons;
            countShuffles = shuffles;
            countInsertions = insertions;
            return array;
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
                averageComparisonsBubble = totalComparisonsBubble / numberOfSorts;
                averageSwapsBubble = totalSwapsBubble / numberOfSorts;
                if (sortingAlgorithm == "All")
                    Console.WriteLine("Bubble sort results:\n");
                Console.WriteLine("Total comparisons: {0}\nAverage comparisons: {1}\n", totalComparisonsBubble, averageComparisonsBubble);
                Console.WriteLine("Total swaps: {0}\nAverage swaps: {1}\n", totalSwapsBubble, averageSwapsBubble);
            }
            if (sortingAlgorithm == "Selection" || sortingAlgorithm == "All")
            {
                averageComparisonsSelection = totalComparisonsSelection / numberOfSorts;
                averageSwapsSelection = totalSwapsSelection / numberOfSorts;
                if (sortingAlgorithm == "All")
                    Console.WriteLine("Selection sort results:\n");
                Console.WriteLine("Total comparisons: {0}\nAverage comparisons: {1}\n", totalComparisonsSelection, averageComparisonsSelection);
                Console.WriteLine("Total swaps: {0}\nAverage swaps: {1}\n", totalSwapsSelection, averageSwapsSelection);
            }
            if (sortingAlgorithm == "Insertion" || sortingAlgorithm == "All")
            {
                averageComparisonsInsertion = totalComparisonsInsertion / numberOfSorts;
                averageShuffles = totalShuffles / numberOfSorts;
                averageInsertions = totalInsertions / numberOfSorts;
                if (sortingAlgorithm == "All")
                    Console.WriteLine("Insertion sort results:\n");
                Console.WriteLine("Total comparisons: {0}\nAverage comparisons: {1}\n", totalComparisonsInsertion, averageComparisonsInsertion);
                Console.WriteLine("Total shuffles: {0}\nAverage shuffles: {1}\n", totalShuffles, averageShuffles);
                Console.WriteLine("Total insertions: {0}\nAverage insertions: {1}\n", totalInsertions, averageInsertions);
            }
        }
    }
}
