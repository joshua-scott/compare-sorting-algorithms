using System;
using System.Threading.Tasks;

namespace CompareSorts
{
    public static class CallSpecifiedSort
    {
        public const string a = "ascending",
                            d = "descending",
                            r = "random";

        public const string B = "Bubble",
                            S = "Selection",
                            I = "Insertion",
                            Q = "Quick",
                            M = "Merge",
                            A = "All";

        public static void Init(string sortingAlgorithm, string orderOfInitialArray, ulong numberOfSorts, int lengthOfArray, bool printUpdates)
        {
            /* Initialise specified number of arrays of ints
             * Pass each one to Bubble||Selection||Insertion||Quick sort
             * Count total number of comparisons/swaps/shuffles/insertions as required
             */
            for (int i = 0; i < (int)numberOfSorts; i++)
            {
                if (numberOfSorts > 1 && printUpdates == true)
                    Console.WriteLine("\n\nRound {0}:\n", (i + 1));

                // Create array
                int[] array = ArrayOperations.Make(lengthOfArray, orderOfInitialArray);

                if (printUpdates && sortingAlgorithm != A)
                    ArrayOperations.Print(array, "initial array");

                switch (sortingAlgorithm)
                {
                    case B:
                        Bubble.Sort(array, printUpdates);
                        break;
                    case S:
                        Selection.Sort(array, printUpdates);
                        break;
                    case I:
                        Insertion.Sort(array, printUpdates);
                        break;
                    case Q:
                        Quick.Sort(array, 0, array.Length - 1, printUpdates);
                        break;
                    case M:
                        Merge.Sort(array, printUpdates);
                        break;
                    default:
                        CallAllSorts(array, printUpdates);
                        break;
                }
            }

        }

        /* Calls all sorting algorithms and prints additional information to specify which sort is which */
        private static void CallAllSorts(int[] array, bool printUpdates)
        {
            // Copy each array (since they're arrays of ints, shallow copy is enough)
            int[] array1 = (int[])array.Clone();
            int[] array2 = (int[])array.Clone();
            int[] array3 = (int[])array.Clone();
            int[] array4 = (int[])array.Clone();

            if (printUpdates)
            {
                Console.WriteLine("\nBubble sort:" +
                                  "\n************");
                ArrayOperations.Print(array, "initial array");
            }
            Bubble.Sort(array, printUpdates);

            if (printUpdates)
            {
                Console.WriteLine("\nSelection sort:" +
                                  "\n***************");
                ArrayOperations.Print(array1, "initial array");
            }
            Selection.Sort(array1, printUpdates);

            if (printUpdates)
            {
                Console.WriteLine("\nInsertion sort:" +
                                  "\n***************");
                ArrayOperations.Print(array2, "initial array");
            }
            Insertion.Sort(array2, printUpdates);

            if (printUpdates)
            {
                Console.WriteLine("\nQuick sort:" +
                                  "\n***********");
                ArrayOperations.Print(array3, "initial array");
            }
            Quick.Sort(array3, 0, array3.Length - 1, printUpdates);

            if (printUpdates)
            {
                Console.WriteLine("\nMerge sort:" +
                                  "\n***********");
                ArrayOperations.Print(array4, "initial array");
            }
            Merge.Sort(array4, printUpdates);

            if (printUpdates)
                Console.WriteLine();
        }
    }
}
