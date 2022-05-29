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
                int smallestPrimeNumber = findSmallestPrime(arrS);
                if (x < smallestPrimeNumber) return smallestPrimeNumber;
                // x is not valid integer since it is larger than smallestPrimeNumber
                else return -1; 
            }
            else // false for down
            {
                int largestPrimeNumber = findLargestPrime(arrS);
                if (x >= largestPrimeNumber) return largestPrimeNumber;
                else return -1; // x is not valid integer
            }
        }

        private int findSmallestPrime(int[] array)
        {
            int smallestPrimeNumber = int.MaxValue;
            //int smallestNumber = array[0];
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] < smallestPrimeNumber && isPrime(array[i]))
                    smallestPrimeNumber = array[i]; 
            }
            // if there is no prime number, return -1 for error response
            if (smallestPrimeNumber == int.MaxValue) return -1;

            return smallestPrimeNumber;
        }

        private int findLargestPrime(int[] array)
        {
            int largestPrimeNumber = int.MinValue;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] > largestPrimeNumber && isPrime(array[i]))
                    largestPrimeNumber = array[i]; 
            }
            // if there is no prime number, return -1 for error response
            if (largestPrimeNumber == int.MinValue) return -1;


            return largestPrimeNumber;
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