using System;

namespace algorithms
{
    public class Fibonachi
    {
        private static int exit;

        public static int FiboSequence(int a, int b, int counter, int number)
        {
            var i = exit;

            if (counter < number) 
                exit = FiboSequence(b, a+b, counter+1, number);

            return i;
        }
    }
}