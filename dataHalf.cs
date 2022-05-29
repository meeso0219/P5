/*
 *  Name: Changhyun Park
 *  Date: 04/29/2022
    Class invariant:
        dataHalf object is-a dataExtractor and thus operates like a dataExtractor object, except that
        1) every number in x is divided by 2
        2) when the number of failed client requests exceed a bound, the object is deactivated.
           if object is deactivated, the object cannot do any requests and will throw an exception.
        3) any() returns the same composite for the first and second request
                 returns the same composite for the third and fourth request

        kinds of failed request:
            - target(z): 1) if the state is false and if z is greater than length of arrX
                         2) if the state is true and if z is greater than the number of odd in arrY
            - sum(z):    1) if the state is false and if z is greater than length of arrX
                         2) if the state is true and if z is greater than the number of even in arrY
        
        - After dataHalf's constructor, array x might have dupliacted numbers.
        - target(z): the functionality is almost same but it changes number of failed counts if it catches exception.
                     if the object is deactivated, throw exception and it does not do the request
        - sum(z): the functionality is almost same but it changes number of failed counts if it catches exception.
                  if the object is deactivated, throw exception and it does not do the request
        - any(): any's first and second request returns multiple of three.
                 any's thrid and fourth request returns multiple of four.
                From fifth request, it returns multiple of three.
    
*/


using System;

namespace P5
{
    class dataHalf : dataExtractor 
    {
        private int failedCount;
        private readonly int bound;
        private int anyCount;

        // PRECONDITION:  array x must have not zero. 
        // POSTCONDITION: every number in array x is divided by 2
        //                After every number in x is divided by 2, there could be the same number in array x.
        public dataHalf(int[] e) : base(e)
        {
            for (int i = 0; i < arrX.Length; i++)
            {
                arrX[i] = arrX[i] / 2;
            }
            bound = (arrX.Length % 10) + 1;

            Console.Write("array x in dataHalf: ");
            foreach (int i in arrX)
            {
                Console.Write("{0} ", i);
            }
            Console.WriteLine("\n");
        }

        // PRECONDITION: z must be equal or smaller than the length of array x otherwise, throw an exception
        // POSTCONDITION: object will be deactivated if failed request exceeds a bound.
        public override int[] target(int z)
        {
            if (isDisable())
                throw new Exception("The object is deactivated since it failed too many requests...");

            try
            {
                return base.target(z);
            }
            catch (Exception e)
            {
                failedCount++;
                throw;
            }
        }

        // PRECONDITION: z must be equal or smaller than the length of array x otherwise, throw an exception
        // POSTCONDITION:   if exception is catched, increase failed count.
        //                  object will be deactivated if failed request exceeds a bound.
        public override int sum(int z)
        {
            if (isDisable())
                throw new Exception("The object is deactivated since it failed too many requests...");

            try
            {
                return base.sum(z);
            }
            catch (Exception e)
            {
                failedCount++;
                throw;
            }
        }

        // PRECONDITION: NONE
        // POSTCONDITION: NONE
        public override int[] any()
        {
            anyCount++;
            if (anyCount == 1 || anyCount == 2)
                return multOfThree(arrX);

            else if (anyCount == 3 || anyCount == 4)
                return multOfFour(arrX); 

            else  
                return multOfThree(arrY);
        }

        private int[] multOfFour(int[] tempArr)
        {
            int multipleOfThree = 0;
            int index = 0;

            for (int i = 0; i < tempArr.Length; i++)
            {
                if (tempArr[i] % 4 == 0 && tempArr[i] != 0)
                    multipleOfThree++;
            }

            if (multipleOfThree == 0)
                throw new ArgumentException("There are no number of multiple of four");

            int[] multipleThreeArr = new int[multipleOfThree];

            for (int i = 0; i < tempArr.Length; i++)
            {
                if (tempArr[i] % 4 == 0 && tempArr[i] != 0)
                    multipleThreeArr[index++] = tempArr[i];
            }

            return multipleThreeArr;
        }

        private bool isDisable()
        {
            if (failedCount > bound)
                return true;
            else 
                return false;
        }

       

    }
}
/*
    Implementation invariant:
        -   dataHalf object divide every number in x by 2 such that, array x might have same numbers.
        -   I set failedCount and anyCount to be private because it is only need for dataHalf object.
        -   I used private readonly for bound such that when the bound is assigned in the constructor it cannot be changed.
        -   I decided to override target and sum to count failed request and react according to the state of deactivation
            the failed count increments when failed request is happened.
        -   I added multOfFour and isDisabled as helper function. 
            multOfFour returns an array that contains only multiple of four
            multOfFour does not count zero. isDisabled become true if the number of failed request exceeds a bound.
*/