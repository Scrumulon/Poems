using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PrimeNumberProj
{
    public partial class PrimeFinder
    {
        public const char TERMINATOR = '\n';
        static string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Primes.txt";
        List<BigInteger> Primes = new List<BigInteger>();
        public PrimeFinder()
        {
            InitialisePrimesFile();
            InitialiseList();
        }
        void InitialisePrimesFile()
        {
            if (File.Exists(filePath))
            {
                string? first;
                string? second;
                using (StreamReader sr = new StreamReader(filePath))
                {
                    first = sr.ReadLine();
                    second = sr.ReadLine();
                }
                if (first == "2" & second == "3") return;
            }
            using (StreamWriter sw = new StreamWriter(filePath, false))
            {
                sw.Write($"2{TERMINATOR}3{TERMINATOR}");
            }
        }
        void InitialiseList()
        {
            string[] strings;
            using (StreamReader sr = new StreamReader(filePath))
            {
                string primeString = sr.ReadToEnd();
                strings = primeString.Split(TERMINATOR);
            }
            for (int i = 0; i < strings.Length -1; i++)
            {
                string? prime = strings[i];
                Primes.Add(BigInteger.Parse(prime));
            }
        }
        public  List<BigInteger>? GetPrimeFactors(BigInteger num)
        {
            PrimeFinderRun primeFinder = new(ref Primes);
            return primeFinder.GetPrimeFactors(num);
        }
        public  void FindPrimes()
        {
            BigInteger nextInt = Primes[Primes.Count - 1] + 2;
            using (PrimeFinderRun primeFinderRun = new(ref Primes))
            {
                while (true)
                {
                    using (StreamWriter sw = new(filePath, true))
                    {
                        if (primeFinderRun.IsPrime(nextInt))
                        {
                            sw.WriteLine($"{nextInt}");
                        }
                    }
                }

                nextInt += 2;
            }
        }
        public  void FindPrimesAndOutput()
        {
            BigInteger lastPrime;
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Primes.txt";
            using (StreamReader sr = new StreamReader(filePath))
            {
                string primeString = sr.ReadToEnd();
                string[] strings = primeString.Split(TERMINATOR);
                lastPrime = BigInteger.Parse(strings[strings.Length - 2]);

            }
            BigInteger nextInt = lastPrime + 2;
            while (true)
            {
                using (PrimeFinderRun primeFinderRun = new(ref Primes ))
                {
                    if (primeFinderRun.IsPrime(nextInt))
                    {
                        using (StreamWriter sw = new(filePath, true))
                        {
                            sw.Write($"{nextInt}{TERMINATOR}");
                        }
                        string consoleString = String.Format("{0,6:N0}", nextInt);
                        Console.WriteLine(consoleString);

                    }
                }
                nextInt += 2;
            }
        }
    }
}
