using System.Collections.Generic;

namespace algorithms
{
    public static class Search
    {
        public static int BinarySearch(List<int> array, int key)
        {
            var sortedArray = Sort.BubbleSort(array);
            int left = 0;
            int right = sortedArray.Count - 1;
            int index = 0;
            int mid = 0;
            int middleValue = 0;

            if (key < sortedArray[left] || key > sortedArray[right])
                return -1;

            if (left == right)
            {
                return left;
            }

            while (true)
            {
                index++;

                if (right - left == 1)
                {
                    if (sortedArray[left].CompareTo(key) == 0)
                    {
                        Logger.ShowSearchProgress(index, mid, middleValue, key, left, right, sortedArray[left], sortedArray[right]);
                        Logger.Message(index, mid);
                        return left;
                    }
                    if (sortedArray[right].CompareTo(key) == 0)
                    {
                        Logger.ShowSearchProgress(index, mid, middleValue, key, left, right, sortedArray[left], sortedArray[right]);
                        Logger.Message(index, mid);
                        return right;
                    }

                    Logger.ShowSearchProgress(index, mid, middleValue, key, left, right, sortedArray[left], sortedArray[right]);
                    Logger.Message(index, mid);
                    return -1;
                }
                else
                {
                    mid = left + (right - left) / 2;

                    middleValue = sortedArray[mid];

                    Logger.ShowSearchProgress(index, mid, middleValue, key, left, right, sortedArray[left], sortedArray[right]);

                    if (middleValue == key)
                    {
                        Logger.Message(index, mid);
                        return mid;
                    }

                    if (middleValue > key)
                        right = mid;
                    else
                        left = mid;
                }
            }
        }
    }
}
