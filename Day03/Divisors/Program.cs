using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Divisors
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting the process...");
            var stopwatch = Stopwatch.StartNew();

            Task<int> task = RunDivisors();
            task.Wait();

            stopwatch.Stop();
            Console.WriteLine($"Elapsed time: {stopwatch.Elapsed.TotalMilliseconds} ms");
        }

        private static async Task<int> RunDivisors()
        {
            return await Task.Run(() => NumberOfDivisors());
        }

        private static int NumberOfDivisors()
        {
            int numberOfDivisors = 1;  
            int largerNumberOfDivisors = 1;

            for (int number = 2; number <= 100000; number++)
            {
                int divisorCount = 0;  

                for (int divisor = 1; divisor <= number; divisor++)
                {  
                    if (number % divisor == 0)
                        divisorCount++;
                }

                if (divisorCount > numberOfDivisors)
                {
                    numberOfDivisors = divisorCount;
                    largerNumberOfDivisors = number;
                }

            }

            Console.WriteLine($"The integer that has the largest number of divisors is: " + largerNumberOfDivisors);
            Console.WriteLine($"The number of divisors that { largerNumberOfDivisors} has is {numberOfDivisors} ");

            return largerNumberOfDivisors;

        }
    }
}
