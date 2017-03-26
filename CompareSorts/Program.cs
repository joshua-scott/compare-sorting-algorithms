using System;
using System.Threading.Tasks;

namespace CompareSorts
{
    public class Program
    {
        /*
         * Edit these variables to set up!
         */
        const string sortingAlgorithm = CallSpecifiedSort.A,    // B/S/I/Q/M/A (denoting Bubble/Selection/Insertion/Quick/Merge/All)
                     orderOfInitalArray = CallSpecifiedSort.r;  // a/d/r (denoting ascending/descending/random)
        const ulong numberOfSorts = 5;                          // How many times to sort the array.
        const int lengthOfArray = 10;                           // How many elements to sort. Use no more than 99 if printUpdates is true.
        const bool printUpdates = true;                         // If true, prints the array and information at each operation.

        static void Main(string[] args)
        {
            Console.WriteLine("Sorting algorithm: {0}\nResults of {1} passes of {2}-int {3} arrays:\n",
                sortingAlgorithm, numberOfSorts, lengthOfArray, orderOfInitalArray);

            CallSpecifiedSort.Init(sortingAlgorithm, orderOfInitalArray, numberOfSorts, lengthOfArray, printUpdates);

            CountAndDisplay.DisplayResults(sortingAlgorithm, numberOfSorts, printUpdates);

            Console.ReadKey();
        }
    }
}
