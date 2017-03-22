# compare-sorting-algorithms
A console program to compare the efficiency of common sorting algorithms (bubble/selection/insertion/merge/quick sort).

## What does it do?
This program can create and sort arrays of integers between 0 and 99, using the five common algorithms mentioned above. It will output the number of comparisons and swaps that were performed, to give you a sense of the relative efficiencies of the algorithms.

By editing the variables in the class Program.cs, you can adjust:
- The size of the array you want to be sorted
- The sorting algorithm to use (one specific one, or all of them)
- The order of the pre-sorted array (randomised, descending, or already sorted into ascending order)
- Whether to output updates to the console or not (e.g. "Compared x with y, x is bigger so swapped them", as well as the state of the array at each change. This can take some time with larger arrays)
- How many times to sort (if you want to compare the efficiency of a sorting algorithm, it's useful to call the same algorithm a few times; the program will display an average)

## Why? Who is it for?
Well, it's for me! Whilst learning Data Structures and Algorithms at university, I wanted to develop my understanding of common sorting algorithms. Writing this program was a fun way to achieve that, and improve my C# coding skills in the process. 

## Future updates
I may or may not add the following features at some point in the future:
- Output a comparison of the times each algorithm took to sort the data
- More sorting algorithms (most likely Heap Sort and Radix Sort)
- Interactive prompt to specify the options upon running the program (as opposed to doing it in the code)

## This looks fun! Can I get involved?
Sure! Just create a branch, get coding, and submit a pull request whenever you like! Or fork it and play around with it if you like. 
