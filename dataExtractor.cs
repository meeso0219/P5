/*
 * Name: Changhyun Park
 * DATE: 04/29/2022
 * Class invariant for dataExtractor:
    -   dataobject encapsulates two integer arrays x and y that  individually  contain  only  unique  values.
        if array x contains duplicated value, it causes dataExtractor's state change
    -   array y is created internally: length of array y will be the half length of array x.
    -   If injected array x's length is less than minX, it changes state to false.
        If injected array x's length is greater than minX, it changed state to true
    -   dataExtractor object supports client requests to retrieve data  as follows:
            1) any():     returns number of multiple of 3 from array x and array y.
            2) target(z): if state is true, returns all values of odd numbers from array y.
                          if state is false, returns z values from array x.
            3) sum(z):    if state is true,  returns the sum of all even numbers from array y.
                          if state is false, returns the sum of z values from array x.
 * 
 * 
 * 
 *  Revision History:   Constructor check if client passes is null reference. (5/22/2022)
 *                      Added error response if z < 1 (5/22/2022)
 *                      Now any return multiple of three from array x and array y.
 */

using System;

namespace P5
{
    public partial class dataExtractor
    {
        private int minX;
        private int minY;
        private const int defaultMin = 5;
        private bool state;
        protected int[] arrX;
        protected int[] arrY;

        // PRECONDITION: Injected array x should be larger than minX
        // POSTCONDITION: array x's state become false if array size is smaller than minX
        //                array x's state become false if it continas same values.
        //                array x's state become true if it is valid
        public dataExtractor(int[] x)
        {
            if (x == null)
                throw new InvalidOperationException("x is null reference");
            
            arrX = x;
            minX = defaultMin + arrX[(arrX.Length - 5) % 10]; 
            minY = arrX.Length / 2; 
            arrY = new int[minY]; 
                                  
            for (int i = 0; i < minY; i++)
                arrY[i] = arrX[i] * (i + 1);

            Console.Write("array y: ");
            foreach (int i in arrY)
            {
                Console.Write("{0} ", i);
            }
            Console.WriteLine("\n");


            if (arrX.Length < minX)   
                state = false; 
            
            else if (!isUnique(arrX))
                state = false;

            else
                state = true;
        }

        // PRECONDITION: array x and array y must have at least one multiple of three otherwise, will get exception
        // POSTCONDITION: NONE
        public virtual int[] any()
        {
            int[] firstArr = multOfThree(arrX);
            int[] secondArr = multOfThree(arrY);
            int[] finalArr;
            int newSize = firstArr.Length + secondArr.Length;
            finalArr = new int[newSize];
            uint index = 0;

            for (int i = 0; i < firstArr.Length; i++)
            {
                finalArr[index++] = firstArr[i];
            }

            for (uint k = 0; k < secondArr.Length; k++)
            {
                finalArr[index++] = secondArr[k];
            }


            return finalArr;
        }

        // PRECONDITION: z must be equal or smaller than the length of array x otherwise, throw an exception
        // POSTCONDITION: NONE
        public virtual int[] target(int z)
        {
            int[] result;
            result = new int[z];

            if (!state) 
            {
                if (z > arrX.Length || z < 1)
                    throw new InvalidOperationException("z is bigger than arrX's length or smaller than 1");

                for (int i = 0; i < z; i++)
                    result[i] = arrX[i];
            }
            else 
            {
                for (int i = 0; i < numOdd(arrY); i++)
                {
                    if (arrY[i] % 2 != 0)
                        result[i] = arrY[i];
                }
            }

            return result;
        }

        // PRECONDITION: if state is false, z must not be greater than arrX length.
        // POSTCONDITION: NONE
        public virtual int sum(int z)
        {

            int result = 0;

            if (!state) 
            {
                if (z > arrX.Length || z < 1)
                    throw new InvalidOperationException("z is bigger than arrX's length or smaller than 1");

                else 
                {
                    for (int i = 0; i < z; i++)
                        result += arrX[i];
                }
            }
            else 
            {
                for (int i = 0; i < arrY.Length; i++)
                {
                    if (arrY[i] % 2 == 0)
                        result += arrY[i];
                }
            }
            return result;
        }

        protected int[] multOfThree(int[] tempArr)
        {
            int multipleOfThree = 0;
            int index = 0;

            for (int i = 0; i < tempArr.Length; i++)
            {
                if (tempArr[i] % 3 == 0 && tempArr[i] != 0)
                    multipleOfThree++;
            }

            if (multipleOfThree == 0)
                throw new ArgumentException("There are no number of multiple of three");

            int[] multipleThreeArr = new int[multipleOfThree];

            for (int i = 0; i < tempArr.Length; i++)
            {
                if (tempArr[i] % 3 == 0 && tempArr[i] != 0)
                    multipleThreeArr[index++] = tempArr[i];
            }

            return multipleThreeArr;
        }
        protected int numOdd(int[] array)
        {
            int counts = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if ((array[i] % 2) != 0 && array[i] != 0)
                    counts++;
            }

            return counts;
        }
        protected int numEven(int[] array)
        {
            int counts = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if ((array[i] % 2) == 0 && array[i] != 0)
                    counts++;
            }

            return counts;
        }

        protected bool isUnique(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                for (int k = 0; k < array.Length; k++)
                {
                    if (array[i] == array[k])
                        return false;
                }
            }
            return true;
        }
    }
}

/*
 *     Implementation invariant:
    -   minX and minY are set to be private so that derived classes cannot access it. 
        (Accessing these data members from dderived classes is not necessary and they shouldn't do that.)
    -   defaultMin is set to private and constant because it is used as default minimum length.
        It is used as a part of formula to calculate mimX.
    -   state is set to private since derived classes do not depend on state.
    -   bound is set to readonly because it should not be changed after number of bound is assigned.
        number of bound will be 1 plus one digit of array x's length. (if array x's length is 13, the bound will be 4).
        The adding 1 exists to prevent the bound becoming zero.
    -   numOdd and numEven are helper function so that when the program need to return odd numbers or even numbers,
        the helper functions make calculation easier.
        Also, zero is not counted as odd or even.
    -   For the target(z) and sum(z), I decided to throw an exception if the state is false and z is greater than 
        array x's size. Because it cannot return z values.
    -   I added multOfThree so that when the program need to return multiple of three, it can be used.
    -   Created helper function isUnique to check if the input array has no duplicated values.
*/