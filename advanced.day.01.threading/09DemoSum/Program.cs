namespace _09DemoSum
{
    using System;
    using System.Diagnostics;
    using System.Numerics;
    using System.Threading;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var arraySize = 50000000; // 50 000 000
            var array = BuildAnArray(arraySize);

            var stopwatch = Stopwatch.StartNew();

            var arrayProcessor = new ArrayProcessor(array, 0, arraySize);

            Thread tr = new Thread(arrayProcessor.CalculateSum);
            tr.Start();
            tr.Join();
           
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
