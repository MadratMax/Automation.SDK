using System;
using System.Collections.Generic;

namespace algorithms
{
    public class Logger
    {
        public static void Message(int searchAttempt, int arrMid)
        {
            Console.WriteLine("=================================");
            Console.WriteLine("attempts: " + searchAttempt);
            Console.WriteLine("=================================");
        }

        public static void ShowBubbleSortResult(
            int originCount, 
            List<int> origin, 
            int attempt, 
            int sortedCount, 
            List<int> sorted,
            List<int> indexArray)
        {
            Console.Clear();
            Console.WriteLine("unsorted: (" + originCount + " numbers)");
            Console.WriteLine(string.Join("|", origin));
            Console.WriteLine();
            Console.WriteLine("attempts: " + attempt);
            Console.WriteLine();
            Console.WriteLine("sorted: (" + sortedCount + " numbers)");
            Console.WriteLine(string.Join("|", sorted));

            foreach (var item in sorted)
            {
                indexArray.Add(sorted.IndexOf(item));
            }

            Console.WriteLine(string.Join("|", indexArray));
        }

        public static void ShowSearchProgress(int index, int mid, int middleValue, int key, int left, int right, int sortedLeft, int sortedRight)
        {
            Console.WriteLine("----------------------------- " + index + " --");
            Console.WriteLine("middle index: " + mid);
            Console.WriteLine("middle value:" + middleValue);
            Console.WriteLine("key: " + key);
            Console.WriteLine();
            Console.WriteLine("Searching area:");
            Console.WriteLine("| " + (left) + " <--> " + (right) + " |");
            Console.WriteLine("| from " + sortedLeft + " to " + sortedRight + " |");
        }
    }
}
