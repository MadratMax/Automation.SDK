using System.Collections.Generic;

namespace algorithms
{
    public class Sort
    {
        public static List<int> BubbleSort(List<int> array)
        {
            int temp = 0;
            int attempt = 0;
            var origin = array;
            var sorted = array;

            for (int walker = 0; walker <= array.Count; walker++)
            {
                for (int i = 0; i < array.Count - 1; i++)
                {
                    attempt++;
                    if (sorted[i] > sorted[i + 1])
                    {
                        temp = sorted[i];
                        sorted[i] = sorted[i + 1];
                        sorted[i + 1] = temp;
                    }
                }
            }

            var indexArray = new List<int>();

            Logger.ShowBubbleSortResult(origin.Count, origin, attempt, sorted.Count, sorted, indexArray);

            return sorted;
        }
    }
}
