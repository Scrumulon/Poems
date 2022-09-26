// See https://aka.ms/new-console-template for more information
global using System.Numerics;

using PrimeNumberProj;
static void AskForNumbersToCheck()
{
    Console.WriteLine("Hello, World!");
    while (true)
    {
        Console.WriteLine("Enter a number to see if its prime!");
        BigInteger input;
        if (!(BigInteger.TryParse(Console.ReadLine(), out input)))
        {
            Console.WriteLine("That is not a number.");
            continue;
        }
        List<BigInteger>? PrimeFactors = PrimeFinder.GetPrimeFactors(input);
        if (PrimeFactors is null) Console.WriteLine("That's a prime number!");
        else
        {
            Console.WriteLine("That is not a prime number.");
            Console.WriteLine($"{input}'s prime factors are:");
            foreach (var primeFactor in PrimeFactors)
            {
                Console.WriteLine($"\t{primeFactor}");
            }
        }
    }
}
static void FindPrimes(bool showInConsole)
{
    if (showInConsole) PrimeFinder.FindPrimesAndOutput();
    else PrimeFinder.FindPrimes();

}

FindPrimes(true);