using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FibonacciSequence
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("There is Fibonacci sequence:");
            Console.WriteLine("Enater sequence length:");
            string enteredValue = Console.ReadLine();
            int end = 20;
            int.TryParse(enteredValue, out end);

            var task1 = Task.Factory.StartNew(() =>
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();

                var list1 = GetFibonacciSequence(end);
                Console.WriteLine("Task without recuirsion");
                Console.WriteLine($"{string.Join(",", list1)}");

                sw.Stop();
                Console.WriteLine($"No recursion time = {sw.Elapsed}");
            });

            var task2 = Task.Factory.StartNew(() =>
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();

                var list2 = GetFibonacciSequenceWithRecursion(end);
                Console.WriteLine("Task with recuirsion");
                Console.WriteLine($"{string.Join(",", list2)}");

                sw.Stop();
                Console.WriteLine($"With recursion time = {sw.Elapsed}");
            });

            Console.ReadKey();
        }

        private static IList<int> GetFibonacciSequenceWithRecursion(int end)
        {
            var result = new List<int>();
            int counter = 0;
            while (counter <= end)
            {
                result.Add(GetNumberRecursion(counter));
                counter++;
            }

            return result;
        }

        private static int GetNumberRecursion(int n)
        {
            if (n == 0)
            {
                return 0;
            }
            if (n == 1)
            {
                return 1;
            }

            return GetNumberRecursion(n-1) + GetNumberRecursion(n-2);
        }

        private static IList<int> GetFibonacciSequence(int end)
        {
            var result = new List<int>();
            int counter = 0;
            while (counter <= end)
            {
                if (counter == 0 || counter == 1)
                {
                    result.Add(counter);
                }
                else
                {
                    result.Add(result[result.Count - 1] + result[result.Count - 2]);
                }
                counter++;
            }

            return result;
        }
    }
}
