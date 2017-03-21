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
                     orderOfInitalArray = CallSpecifiedSort.r;        // a/d/r (denoting ascending/descending/random)
        const ulong numberOfSorts = 5;
        const int lengthOfArray = 5;
        const bool printUpdates = true;

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
