using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Xml.Schema;

namespace SpeedCompareListArr
{
    class Program
    {
        public static Random rng = new Random();
        public static List<int> filledList()
        {
            List<int> ret = new List<int>(new int[1000000]);
            for (int i = 0; i < 1000000; i++)
            {
                ret[i] = rng.Next(0,256);
            }
            return ret;
        }
        public static int[] filledArray()
        {
            int[] arr = new int[1000000];
            for (int i = 0; i < 1000000; i++)
            {
                arr[i] = rng.Next(0, 256);
            }
            return arr;
        }

        public static List<long> ArrayResults = new List<long>();
        public static List<long> ListResults = new List<long>();

        static void Main(string[] args)
        {
            for (int b = 0; b < 100; b++)
            {
                List<int> lst = filledList();
                int[] arr = filledArray();
                long CurrentArr = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                long sumA = 0;
                for (int i = 0; i < 1000000; i++)
                {
                    arr[i] = arr[i] + arr[i];
                    sumA += arr[i];
                }
                Console.Write(sumA);
                long LastArr = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                long CurrentLst = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                long sumL = 0;
                for (int i = 0; i < 1000000; i++)
                {
                    lst[i] = lst[i] + lst[i];
                    sumL += lst[i];
                }
                Console.Write(sumL);
                long LastLst = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                long DiffArr = LastArr - CurrentArr;
                long DiffLst = LastLst - CurrentLst;

                ArrayResults.Add(DiffArr);
                ListResults.Add(DiffLst);
                lst.Clear();
            }

            //Calculating average
            long totalArr = 0;
            long totalLst = 0;
            for(int i = 0; i<ArrayResults.Count; i++)
            {
                totalArr += ArrayResults[i];
                totalLst += ListResults[i];
            }

            long avgArr = totalArr / ArrayResults.Count;
            long avgLst = totalLst / ListResults.Count;

            Console.WriteLine("---- 100x times test (AVERAGE RESULTS) ----");
            Console.WriteLine("Array 1m read+write: "+avgArr+"ms");
            Console.WriteLine("List 1m read+write: "+avgLst+"ms");
            Console.WriteLine("---------------TOTAL TIME TAKEN------------");
            Console.WriteLine("Array -> " + totalArr + "ms -> " + totalArr / 100 + " seconds.");
            Console.WriteLine("List -> " + totalLst + "ms -> " + totalLst / 100 + " seconds.");
            Console.WriteLine("-------------------------------------------");
        }
    }
}
