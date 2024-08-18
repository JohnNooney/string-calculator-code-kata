using StringCalculatorApp;
using System;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            StringCalculator calculator = new StringCalculator();
            calculator.Add("");
        }
    }
}