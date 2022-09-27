namespace PrimeNumberProj
{
    public partial class PrimeFinder
    {
        private class PrimeFinderRun : IDisposable
        {
            List<BigInteger> primes;
            public PrimeFinderRun(ref List<BigInteger> primes)
            {
                this.primes = primes;
            }
            BigInteger number;
            int divisorDex = 0;
            BigInteger divisor;
            BigInteger ceiling;
            private bool disposedValue;
            public List<BigInteger>? GetPrimeFactors(BigInteger numToCheck)
            {
                number = numToCheck;
                ceiling = number / 2;
                divisorDex = 0;
                divisor = 2;
                while (divisor <= ceiling)
                {
                    if (number % divisor == 0)
                    {
                        //Console.Beep();
                        List<BigInteger> list = new List<BigInteger>();
                        BigInteger divisor1 = number / divisor;
                        using (PrimeFinderRun primeFinder = new(ref primes))
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
                divisorDex = 0;
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
                if (divisorDex == primes.Count() - 1)
                {
                    // could make the last value of the array divisor after
                    divisor += 2;
                }
                else
                {
                    divisorDex++;
                    divisor = primes[divisorDex];
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
    }
}
