﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DemoSum02
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var arraySize = 50000000; // 50 000 000
            var array = BuildAnArray(arraySize);

            var stopwatch = Stopwatch.StartNew();

            var arrayProcessor = new ArrayProcessor(array, 0, arraySize);

            Task t1 = new Task(arrayProcessor.CalculateSum);
            t1.Start();
            t1.Wait();

            arrayProcessor.CalculateSum();
            var totalSum = arrayProcessor.Sum;

            stopwatch.Stop();

            Console.WriteLine($"Elapsed time: {stopwatch.Elapsed.TotalMilliseconds} ms");
            Console.WriteLine($"Sum: {totalSum}");
        }

        public static int[] BuildAnArray(int size)
        {
            var array = new int[size];

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = i;
            }

            return array;
        }
    }
}
