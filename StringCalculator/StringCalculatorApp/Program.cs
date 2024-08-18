using StringCalculatorApp;
using System;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StringCalculator calculator = new StringCalculator();

            Console.WriteLine("Welcome to the string calculator app! ");
            Console.WriteLine("You can add together any sequence of numbers. Just follow these rules:");
            Console.WriteLine("- default delimiters: \\n and ,");
            Console.WriteLine("- create your own delimiters by following this format: //[delimiter]\\n<your number sequence>");
            Console.WriteLine("   - multiple delimiters by following this format: //[delimiter][delimiter]\\n<your number sequence>");
            Console.WriteLine("   - valid examples: //|\\n1|2|3  , //[|][%]\\n1|2%3");

            while (true)
            {
                
                Console.Write("\nEnter your sequence: ");
                string input;
                try
                {
                    input = Console.ReadLine();
                }
                catch (IOException e)
                {
                    Console.WriteLine("Error reading from console: " + e.Message);
                    // Handle the error, e.g., retry, log, exit
                    return;
                }

                int sum = calculator.Add(input);

                Console.WriteLine("SUM = " + sum);
            }
        }
    }
}