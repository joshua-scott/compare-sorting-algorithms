using System;
using System.Threading.Tasks;

namespace CompareSorts
{
    public static class CountAndDisplay
    {
        /*  Variables to count the number of operations
         *  Unsigned longs used in all cases to be safe and avoid casting, though not required for all
         */
        public static ulong
        // Bubble:    
                            totalComparisonsBubble = 0,
                            totalSwapsBubble = 0,
        // Selection:
                            totalComparisonsSelection = 0,
                            totalSwapsSelection = 0,
        // Insertion:
                            totalComparisonsInsertion = 0,
                            totalShuffles = 0,
                            totalInsertions = 0,
        //Quick:
                            totalComparisonsQuick = 0,
                            totalSwapsQuick = 0,
        //Merge:
                            totalComparisonsMerge = 0,
                            totalSwapsMerge = 0;


        /* Calculate and display relevant totals and averages: */
        public static void DisplayResults(string sortingAlgorithm, ulong numberOfSorts, bool printUpdates)
        {
            if (numberOfSorts > 1 && printUpdates)
                Console.WriteLine("\n");

            switch (sortingAlgorithm)
            {
                case "All":
                    DisplayBubbleResults(numberOfSorts);
                    DisplaySelectionResults(numberOfSorts);
                    DisplayInsertionResults(numberOfSorts);
                    DisplayQuickResults(numberOfSorts);
                    DisplayMergeResults(numberOfSorts);
                    break;

                case "Bubble":
                    DisplayBubbleResults(numberOfSorts);
                    break;

                case "Selection":
                    DisplaySelectionResults(numberOfSorts);
                    break;

                case "Insertion":
                    DisplayInsertionResults(numberOfSorts);
                    break;

                case "Quick":
                    DisplayQuickResults(numberOfSorts);
                    break;

                case "Merge":
                    DisplayMergeResults(numberOfSorts);
                    break;
            }
        }

        private static void DisplayBubbleResults(ulong numberOfSorts)
        {
            ulong averageComparisonsBubble = totalComparisonsBubble / numberOfSorts;
            ulong averageSwapsBubble = totalSwapsBubble / numberOfSorts;
            Console.WriteLine(  "Bubble sort results:\n" +
                                "********************\n" +
                                "Total comparisons: {0}\nAverage comparisons: {1}\n" +
                                "Total swaps: {2}\nAverage swaps: {3}\n\n",
                                totalComparisonsBubble, averageComparisonsBubble, 
                                totalSwapsBubble, averageSwapsBubble);
        }

        private static void DisplaySelectionResults(ulong numberOfSorts)
        {
            ulong averageComparisonsSelection = totalComparisonsSelection / numberOfSorts;
            ulong averageSwapsSelection = totalSwapsSelection / numberOfSorts;
            Console.WriteLine(  "Selection sort results:\n" +
                                "***********************\n" +
                                "Total comparisons: {0}\nAverage comparisons: {1}\n" +
                                "Total swaps: {2}\nAverage swaps: {3}\n\n",
                                totalComparisonsSelection, averageComparisonsSelection, 
                                totalSwapsSelection, averageSwapsSelection);
        }

        private static void DisplayInsertionResults(ulong numberOfSorts)
        {
            ulong averageComparisonsInsertion = totalComparisonsInsertion / numberOfSorts;
            ulong averageShuffles = totalShuffles / numberOfSorts;
            ulong averageInsertions = totalInsertions / numberOfSorts;
            Console.WriteLine(  "Insertion sort results:\n" +
                                "***********************\n" +
                                "Total comparisons: {0}\nAverage comparisons: {1}\n" +
                                "Total shuffles: {2}\nAverage shuffles: {3}\n" +
                                "Total insertions: {4}\nAverage insertions: {5}\n\n",
                                totalComparisonsInsertion, averageComparisonsInsertion,
                                totalShuffles, averageShuffles,
                                totalInsertions, averageInsertions);
        }

        private static void DisplayQuickResults(ulong numberOfSorts)
        {
            ulong averageComparisonsQuick = totalComparisonsQuick / numberOfSorts;
            ulong averageSwapsQuick = totalSwapsQuick / numberOfSorts;
            Console.WriteLine(  "Quick sort results:\n" +
                                "*******************\n" +
                                "Total comparisons: {0}\nAverage comparisons: {1}\n" +                              
                                "Total swaps: {2}\nAverage swaps: {3}\n\n",
                                totalComparisonsQuick, averageComparisonsQuick,
                                totalSwapsQuick, averageSwapsQuick);
        }

        private static void DisplayMergeResults(ulong numberOfSorts)
        {
            ulong averageComparisonsMerge = totalComparisonsMerge / numberOfSorts;
            ulong averageSwapsMerge = totalSwapsMerge / numberOfSorts;
            Console.WriteLine(  "Merge sort results:\n" +
                                "*******************\n" +
                                "Total comparisons: {0}\nAverage comparisons: {1}\n" +
                                "Total swaps: {2}\nAverage swaps: {3}",
                                totalComparisonsMerge, averageComparisonsMerge, 
                                totalSwapsMerge, averageSwapsMerge);
        }
    }
}
