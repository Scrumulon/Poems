using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PrimeNumberProj
{
    public static class PrimeFinder
    {
        static int[] knownPrimes = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97 };
        public static List<BigInteger>? GetPrimeFactors(BigInteger num)
        {
            PrimeFinderRun primeFinder = new();
            return primeFinder.GetPrimeFactors(num);
        }
        private  class PrimeFinderRun : IDisposable
        {
            BigInteger number;
            int primeDex;
            BigInteger divisor;
            BigInteger ceiling;
            private bool disposedValue;

            public List<BigInteger>? GetPrimeFactors(BigInteger numToCheck)
            {
                number = numToCheck;
                ceiling = number / 2;
                primeDex = 0;
                divisor = 2;
                while (divisor <= ceiling)
                {
                    if (number % divisor == 0)
                    {
                        //Console.Beep();
                        List<BigInteger> list = new List<BigInteger>();
                        BigInteger divisor1 = number / divisor;
                        using (PrimeFinderRun primeFinder = new())
                        {
                            var divisorFactors = primeFinder.GetPrimeFactors(divisor);
                            if (divisorFactors is null) list.Add(divisor);
                            else list.AddRange(divisorFactors);
                            var divisor1Factors = primeFinder.GetPrimeFactors(divisor1);
                            if (divisor1Factors is null) list.Add(divisor1);
                            else list.AddRange(divisor1Factors);
                        }

                        return list;
                    }
                    Increment();
                }
                return null;
            }
            public bool IsPrime(BigInteger numToCheck)
            {
                number = numToCheck;
                ceiling = number / 2;
                primeDex = 0;
                divisor = 2;
                while (divisor <= ceiling)
                {
                    if (number % divisor == 0)
                    {
                        return false;
                    }
                    Increment();
                }
                return true;
            }
            void Increment()
            {
                if (primeDex == knownPrimes.Length - 1)
                {
                    // could make the last value of the array divisor after
                    divisor += 2;
                }
                else
                {
                    primeDex++;
                    divisor = knownPrimes[primeDex];
                }
                ceiling = number / divisor + number % divisor;
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    if (disposing)
                    {
                        // TODO: dispose managed state (managed objects)
                    }

                    // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                    // TODO: set large fields to null
                    disposedValue = true;
                }
            }

            // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
            // ~PrimeFinderRun()
            // {
            //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            //     Dispose(disposing: false);
            // }

            public void Dispose()
            {
                // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }
        }
        public static void FindPrimes()
        {
            BigInteger lastPrime;
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\primes.txt";
            using (StreamReader sr = new StreamReader(filePath))
            {
                string primeString = sr.ReadToEnd();
                string[] strings = primeString.Split(";");
                lastPrime = BigInteger.Parse(strings[strings.Length - 2]);

            }
            BigInteger nextInt = lastPrime + 2;
            while (true)
            {
                using (PrimeFinderRun primeFinderRun = new())
                {
                    if (primeFinderRun.IsPrime(nextInt))
                    {
                        using (StreamWriter sw = new(filePath, true))
                        {
                            sw.Write($"{nextInt};");
                        }
                    }
                }

                nextInt += 2;
            }
        }
        public static void FindPrimesAndOutput()
        {
            BigInteger lastPrime;
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\primes.txt";
            using (StreamReader sr = new StreamReader(filePath))
            {
                string primeString = sr.ReadToEnd();
                string[] strings = primeString.Split(";");
                lastPrime = BigInteger.Parse(strings[strings.Length - 2]);

            }
            BigInteger nextInt = lastPrime + 2;
            while (true)
            {
                using (PrimeFinderRun primeFinderRun = new())
                {
                    if (primeFinderRun.IsPrime(nextInt))
                    {
                        using (StreamWriter sw = new(filePath, true))
                        {
                            sw.Write($"{nextInt};");
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
