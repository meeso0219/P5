using System;
namespace P5
{
    public partial class guard
    {
        private int[] arrS;
        protected bool mode; // mode used in guard and quirkyGuard
        public guard(int[] x)
        {
            mode = true;
            arrS = x;
        }

        // PRECONDITION: x should be valid integer for successful return value
        //               if x is not valid integer, return value will be -1
        public virtual int value(int x)
        {
            if (mode) // true for up
            {

            }
            return -1; // error response
        }

        private int findSmallestPrime(int[] array)
        {
            return 0;
        }

        // check if the number is prime or not
        protected bool isPrime(int num) // used in quirckyGuard so protected
        {
            if (num <= 1) return false; // a number as prime if it is divided by exactly two numbers
            if (num == 2) return true; // 2 -> 1 and 2 so prime
            if (num % 2 == 0) return false; //even num cannot be prime except for 2

            var boundary = (int)Math.Floor(Math.Sqrt(num));

            for (int i = 3; i <= boundary; i += 2)
                if (num % i == 0)
                     return false;
    
            return true;        
        }
    }
}