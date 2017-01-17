using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Goldbach
{
    static class Program
    {
        private static int _upTo;
        private static int _n = 2;

        private static readonly Dictionary<int, Tuple<int, int>> Goldbachs = new Dictionary<int, Tuple<int, int>>();
        private static readonly object Lock = new object();
        
        static void Main(string[] args)
        {
            _upTo = ReadToInt("Up to what number would you like to confirm the Goldbach conjecture?");
            var numberOfThreads = ReadToInt("How many threads would you like to use?");
            Print("You may quit at any time by pressing enter");

            var taskManager = new CancellationTokenSource();
            var cancelSignal = taskManager.Token;

            var tasks = new List<Task>();
            for (var i = 0; i < numberOfThreads; i++)
            {
                var t = Task.Factory.StartNew(() => CalculateGoldbachs(cancelSignal));
                tasks.Add(t);
            }


            while (!Task.WhenAll(tasks).IsCompleted)
                if (EnterKeyDetected)
                    taskManager.Cancel();
            
            Print("Done. Press Enter to see results.");
            if (EnterKeyPressed)
                Goldbachs
                    .OrderBy(t => t.Key)
                    .ToList()
                    .ForEach(t => Print($"{t.Key} = {t.Value.Item1} + {t.Value.Item2}"));
            Console.ReadKey();
        }

       

        private static void CalculateGoldbachs(CancellationToken signal)
        {
            int x = NextEvenNumber;
            while (!signal.IsCancellationRequested && x <= _upTo)
            {
                var composition = Goldbach.Composition(x);
                lock (Lock) Goldbachs.Add(x, composition);
                x = NextEvenNumber;
            }     
        }
        
        private static bool EnterKeyDetected => Console.KeyAvailable && EnterKeyPressed;

        private static bool EnterKeyPressed => Console.ReadKey().Key == ConsoleKey.Enter;

        private static int NextEvenNumber => Interlocked.Add(ref _n, 2);

        private static void Print(string s)
        {
            Console.WriteLine(s);
        }

        private static string Read(string s)
        {
            Print(s);
            return Console.ReadLine();
        }

        private static int ReadToInt(string s)
        {
            return Read(s).ToInt();
        }

        private static int ToInt(this string s)
        {
            return Convert.ToInt32(s);
        }
    }
}