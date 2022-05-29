/*
 *  Name: Changhyun Park
 *  Date: 04/29/2022
    Class invariant:
        dataPlus object is-a dataExtractor and thus operates like a dataExtractor object, except that
        1) array y is initially concatenated with a.
        2) upon every n ==j*kth client request, n is concatenated to the end of current array y.
        3) target(z) returns an array that contains z odd vales from array x and z even values from array y.
        
        - After dataPlus's constructor, array y will have one more value a. a is not the same value across all objects,
                                        k will be determined and j is set to 1 initially.
        - before target(z) there is no condition change for array x and y.
           after target(z), array y might have extra value a if it is j*kth request.
            For the target(z), zero is not counted as odd or even
        - After sum(z), array y might have extra value a if it is j*kth request.
        - After any(), array y might have extra value a if it is j*kth request.

*/

using System;

namespace P5
{
    class dataPlus : dataExtractor
    {
        private int n;
        private int j; 
        private int k; 
        private int a; 
        private int plusCounter;


        // POSTCONDITION: array y will have extra component a at the end of the array.
        public dataPlus(int[] p) : base(p)
        {
            a = findUnique();
            k = (a % 10) + 1;
            j = 1;

            int[] temp = new int[arrY.Length + 1];
            for (int index = 0; index < arrY.Length; index++)
            {
                temp[index] = arrY[index];
            }

            temp[temp.Length - 1] = a;
            arrY = temp;
        }

        // PRECONDITION: z must be equal or smaller than the length of array x otherwise, throw an exception
        // POSTCONDITION: array y might have extra value a if it is j*kth request.
        public override int[] target(int z)
        {          
            int index = 0;
            int finalSize;
            int numberOfoddX = numOdd(arrX);
            int numberOfevenY = numEven(arrY);
            plusCounter++;

            if (k == plusCounter)
            {
                j++;
                concatenate();
            }

            finalSize = numberOfoddX + numberOfevenY;
            int[] combinedArr = new int[finalSize];

            for (int i = 0; i < arrX.Length; i++)
            {
                if ((arrX[i] % 2) != 0)
                    combinedArr[index++] = arrX[i];
            }
            for (int i = 0; i < arrY.Length; i++)
            {

                if ((arrY[i] % 2) == 0)
                    combinedArr[index++] = arrY[i];
            }
            return combinedArr;
        }

        // PRECONDITION: z must be equal or smaller than the length of array x otherwise, throw an exception
        // POSTCONDITION: array y might have extra value a if it is j*kth request.
        public override int sum(int z)
        {
            plusCounter++;
            if (k == plusCounter)
            {
                j++;
                concatenate();
            }

            try
            {
                return base.sum(z);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        // POSTCONDITION: array y might have extra value a if it is j*kth request.
        public override int[] any()
        {
            plusCounter++;

            if (k == plusCounter)
            {
                j++;
                concatenate();
            }
              

            try
            {
                return base.any();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private int findUnique()
        {
            int superUnique;

            superUnique = arrX[2];
            while (isExist(superUnique, arrX) || isExist(superUnique, arrY))
            {
                superUnique = (superUnique + 1) * 7;
            }

            return superUnique;
        }

        private bool isExist(int comparingNum, int[] someArr)
        {
            for (int i = 0; i < someArr.Length; i++)
            {
                if (comparingNum == someArr[i])
                    return true;
            }
            return false;
        }

        private void concatenate()
        {
            n = j * k;

            int[] temp = new int[arrY.Length + 1];

            for (int index = 0; index < arrY.Length; index++)
                temp[index] = arrY[index];
            temp[temp.Length - 1] = n;
            arrY = temp;
        }
    }
}

/* Implementation invariant:
 *      - to determine a which is unique across array x and y, I used helper function findUnique.
 *        inside the function, the value will keep increasing until the value does not exist in array x and y.
 *        In order to check the existence, I created another helper function isExist. It returns true if the number
 *        is exists in the array.
 *      - plusCounter is only for dataPlus object to count number of requests
 *      - For the target(z), zero is not counted as odd or even
 *      
 *      
 */