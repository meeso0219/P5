/*
 * Name: Changhyun Park
 * Date: 04/29/2022
    Interface Invariant:
    -   dataobject encapsulates two integer arrays x and y that  individually  contain  only  unique  values.
        if array x contains duplicated value, it causes dataExtractor's state change
    -   array y is created internally: length of array y will be the half length of array x.
    -   If injected array x's length is less than minX, it changes state to false.
        If injected array x's length is greater than minX, it changed state to true

    - dataHalf object is-a dataExtractor and thus operates like a dataExtractor object, except that
        1) every number in x is divided by 2
        2) when the number of failed client requests exceed a bound, the object is deactivated.
           if object is deactivated, the object cannot do any requests and will throw an exception.
        3) any() returns the same composite for the first and second request
                 returns the same composite for the third and fourth request
    
    -  dataPlus object is-a dataExtractor and thus operates like a dataExtractor object, except that
        1) array y is initially concatenated with a.
        2) upon every n ==j*kth client request, n is concatenated to the end of current array y.
        3) target(z) returns an array that contains z odd vales from array x and z even values from array y.



        dataExtractor object:
        // PRECONDITION: Injected array x should be larger than minX
        // POSTCONDITION: array x's state become false if array size is smaller than minX
        //                array x's state become false if it continas same values.
        //                array x's state become true if it is valid
        public dataExtractor(int[] x)

        // PRECONDITION: array x must have at least one multiple of three otherwise, will get exception
        // POSTCONDITION: NONE
        public virtual int[] any()

        // PRECONDITION: z must be equal or smaller than the length of array x otherwise, throw an exception
        // POSTCONDITION: NONE
        public virtual int[] target(int z)

        // PRECONDITION: if state is false, z must not be greater than arrX length.
        // POSTCONDITION: NONE
        public virtual int sum(int z)

        dataPlus object:
        // POSTCONDITION: array y will have extra component a at the end of the array.
        public dataPlus(int[] p) : base(p)

        // PRECONDITION: z must be equal or smaller than the length of array x otherwise, throw an exception
        // POSTCONDITION: array y might have extra value a if it is j*kth request.
        public override int[] target(int z)


        // PRECONDITION: z must be equal or smaller than the length of array x otherwise, throw an exception
        // POSTCONDITION: array y might have extra value a if it is j*kth request.
        public override int sum(int z)

        dataHalf object:
         // PRECONDITION:  array x must have not zero. 
        // POSTCONDITION: every number in array x is divided by 2
        //                After every number in x is divided by 2, there could be the same number in array x.
        public dataHalf(int[] e) : base(e)

        // PRECONDITION: z must be equal or smaller than the length of array x otherwise, throw an exception
        // POSTCONDITION: object will be deactivated if failed request exceeds a bound.
        public override int[] target(int z)

         // PRECONDITION: z must be equal or smaller than the length of array x otherwise, throw an exception
        // POSTCONDITION:   if exception is catched, increase failed count.
        //                  object will be deactivated if failed request exceeds a bound.
        public override int sum(int z)


        // PRECONDITION: NONE
        // POSTCONDITION: NONE
        public override int[] any()

 */

using System;
using System.Collections;

namespace P5
{
    class P5
    {
        static void Main(string[] args)
        {
            try
            {

                
                testExtractor();
                Console.WriteLine(" ");
                testHalf();

                /*
                Console.WriteLine(" ");
                Console.WriteLine("--------------------------------------");
                Console.WriteLine(" ");

                Console.WriteLine("Plus Start");
                testPlus();
                Console.WriteLine("Plus End");

                Console.WriteLine(" ");
                Console.WriteLine("--------------------------------------");
                Console.WriteLine(" ");


                Console.WriteLine("Half Start");
                
                Console.WriteLine("Half End");

                Console.WriteLine("Collection Start");
                testCollection();
                Console.WriteLine("Collection End");
                */

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public static void testExtractor()
        {
            Console.WriteLine("****************************** Extractor test ******************************");

            Random randomObj = new();
            int randomValue;
            int randomSize = randomObj.Next(10, 30);
            int[] example = new int[randomSize];

            for (int i = 0; i < randomSize; i++)
            {
                randomValue = randomObj.Next(1, 100);
                example[i] = (i + 1) + randomValue;
            }
            // 랜덤객체생성완료

            // dependency injection. Injecting example array
            dataExtractor Test1 = new(example);

            // print array x
            Console.Write("Printing array x: ");
            foreach (int i in example)
            {
                Console.Write("{0} ", i);
            }
            Console.WriteLine("\n");

            // print any
            Console.Write("any : ");
            foreach (int i in Test1.any())
            {
                Console.Write("{0} ", i);
            }
            Console.WriteLine("\n");


            int randomRepeatNum = randomObj.Next(5, 15);
            for (int w = 1; w < 5; w++)
            {
                // printing z value
                Console.Write("z: ");
                Console.Write(w);

                // target part
                Console.Write("| target: ");
                foreach (int i in Test1.target(w))
                {
                    Console.Write("{0} ", i);
                }

                // sum part
                Console.Write("| sum: ");
                Console.Write(Test1.sum(w));
                Console.WriteLine("\n");
            }
            Console.WriteLine("****************************** Extractor test end ******************************");
        }

        public static void testHalf()
        {

            Console.WriteLine("****************************** dataHalf test ******************************");

            Random randomObj = new();
            int randomValue;
            int randomSize = randomObj.Next(10, 30);
            int[] example = new int[randomSize];

            for (int i = 0; i < randomSize; i++)
            {
                randomValue = randomObj.Next(1, 100);
                example[i] = (i + 1) + randomValue;
            }
            // 랜덤객체생성완료

            // print array x
            Console.Write("original array x: ");
            foreach (int i in example)
            {
                Console.Write("{0} ", i);
            }
            Console.WriteLine("\n");

            // dependency injection. Injecting example array
            dataHalf Test1 = new(example);

            

            // print any
            Console.Write("any : ");
            foreach (int i in Test1.any())
            {
                Console.Write("{0} ", i);
            }
            Console.WriteLine("\n");


            int randomRepeatNum = randomObj.Next(5, 15);
            for (int w = 1; w < 5; w++)
            {
                // printing z value
                Console.Write("z: ");
                Console.Write(w);

                // target part
                Console.Write("| target: ");
                foreach (int i in Test1.target(w))
                {
                    Console.Write("{0} ", i);
                }

                // sum part
                Console.Write("| sum: ");
                Console.Write(Test1.sum(w));
                Console.WriteLine("\n");
            }
            Console.WriteLine("****************************** dataHalf test end ******************************");
            /*
            Random randomObj = new();
            int randomValue;
            int randomSize = randomObj.Next(10, 30);
            int[] example = new int[randomSize];

            for (int i = 0; i < randomSize; i++)
            {
                randomValue = randomObj.Next(1, 100);
                example[i] = (i + 1) + randomValue;
            }
            // 랜덤객체생성완료

            foreach (var i in example)
                Console.WriteLine(i.ToString());

            dataHalf Test1 = new(example);
            Console.WriteLine(Test1.any());

            int randomRepeatNum = randomObj.Next(5, 15);
            for (int w = 1; w < randomRepeatNum; w++)
            {
                Test1.any();
                Test1.target(w);
                Test1.sum(w);
            }
            */
        }

        public static void testPlus()
        {
            Random randomObj = new();
            int randomValue;
            int randomSize = randomObj.Next(10, 30);
            int[] example = new int[randomSize];

            for (int i = 0; i < randomSize; i++)
            {
                randomValue = randomObj.Next(1, 100);
                example[i] = (i + 1) + randomValue;
            }
            // 랜덤객체생성완료

            foreach (var i in example)
                Console.WriteLine(i.ToString());

            dataPlus Test1 = new(example);
            Console.WriteLine(Test1.any());

            int randomRepeatNum = randomObj.Next(5, 15);
            for (int w = 1; w < randomRepeatNum; w++)
            {
                Test1.any();
                Test1.target(w);
                Test1.sum(w);
            }
        }

        public static void testCollection()
        {
            Random randomObj = new();
            int randomValue;
            int randomSize = randomObj.Next(10, 30);
            int[] collection = new int[randomSize];
            for (int i = 0; i < randomSize; i++)
            {
                randomValue = randomObj.Next(1, 100);
                collection[i] = (i + 1) + randomValue;
            }
            ArrayList myAL = new ArrayList();
            myAL.Add(new dataExtractor(collection));
            myAL.Add(new dataHalf(collection));
            myAL.Add(new dataPlus(collection));
        }

    }

}