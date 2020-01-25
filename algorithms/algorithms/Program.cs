using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using algorithms.Stack;
using algorithms.Queue;

namespace algorithms
{
    public static class Program
    {
        public static void Main()
        {
            // LinkedList

            //var list = new LinkedList(5);
            //list.Append(2);
            //list.Append(3);
            //list.Append(6);
            //System.Console.WriteLine(string.Join(",", list));
            //Console.ReadLine();

            // Stack

            //var num = 1;
            //var stack = new MyStack<int>(num);
            //stack.Push(2);
            //stack.Push(6);
            //stack.Push(8);
            //var removedTop = stack.Pop();
            //var top = stack.GetTop();
            //System.Console.WriteLine("Top: " + top);

            // Queue

            // var queue = new Queue.Queue<int>();

            // queue.Add(1);
            // queue.Add(2);
            // queue.Add(3);
            // queue.Add(4);
            // var removed = queue.Remove();

            // Console.WriteLine("removed: " + removed);
            // Console.WriteLine(queue.first.data);
            // Console.WriteLine(queue.last.data);

            //Fibonacci

            var i = Fibonachi.FiboSequence(0, 1, 1, 10).ToString();

            System.Console.WriteLine(i);
            Console.ReadLine();
        }

        private static void Run(int[] intArray)
        {
            Console.Clear();

            if (intArray == null)
            {
                intArray = new int[]
                {
                    1,5,66,7,
                    3,2,9,87,12,4,21,90,76,8,10,99,1033,41,13,11,6,56,63,42,
                    79,88,98,97,19,400,138,199,175,873,901,111,122,133,777,20,50,44,
                    102,34,31,29,123,980,888,100
                };
            }

            List<int> array = intArray.ToList();

            Console.WriteLine("Enter number to search:\n");

            var num = Console.ReadLine();

            int n;

            bool isNumeric = int.TryParse(num, out n);

            if(!isNumeric)
            {
                return;
            }

            Find(array, Convert.ToInt32(num));
        }

        private static void Find(List<int> array, int num)
        {
            var searchRes = Search.BinarySearch(array, num);

            if (searchRes == -1)
            {
                Console.WriteLine("Number |" + num + "| not found");
            }
            else
            {
                Console.WriteLine("Number |" + num + "| was found with index: " + searchRes);
            }
        }
    }   
}