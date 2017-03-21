using System;
using System.Threading.Tasks;

namespace CompareSorts
{
    public static class Insertion
    {
        /* Sorts an array using Insertion sort algorithm. 
         * Prints updates if printUpdates is true.
         * At the end, prints a summary of the number of comparisons, shuffles, and insertions.
         * Time complexity: Worst = n^2, Best = n, Average = n^2.
         */
        public static int[] Sort(int[] array, bool printUpdates)
        {
            // Since elements aren't directly swapped, it doesn't make sense to count swaps like the other algorithms.
            // Instead, we count shuffles (when an element moves up one index) 
            // and insertions (where the currentElement is inserted into its correct index for that pass)
            ulong comparisons = 0,
                  shuffles = 0,
                  insertions = 0;
            int n = array.Length;

            // We begin at index 1 since we will compare with the element before it
            for (int i = 1; i < n; i++)
            {
                // Store the current element's value and index
                int currentElement = array[i],
                    j = i;

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
                    Console.WriteLine(", it's not smaller so we insert {0} here:", currentElement.ToString().PadLeft(2));

                // Insert the currentElement in its correct index for this pass, and add 1 to insertions
                array[j] = currentElement;
                insertions++;

                // Since an insertion was made, print array in current state and say what was inserted
                if (printUpdates)
                {
                    string message = "inserted " + currentElement.ToString().PadLeft(2) +
                        " to index " + j.ToString().PadLeft(2);
                    ArrayOperations.Print(array, message);
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

            CountAndDisplay.totalComparisonsInsertion += comparisons;
            CountAndDisplay.totalShuffles += shuffles;
            CountAndDisplay.totalInsertions += insertions;
            return array;
        }
    }
}
