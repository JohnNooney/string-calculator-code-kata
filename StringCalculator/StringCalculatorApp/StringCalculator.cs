using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculatorApp
{
    public class StringCalculator
    {
        private LinkedList<int> numberList;

        public StringCalculator() { 
            numberList = new LinkedList<int>();
        }

        public int Add(string numbersString)
        {
            foreach (char numberChar in numbersString)
            {
                try
                {
                    int number = int.Parse(numberChar.ToString());
                    numberList.AddLast(number);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error parsing char: " + numberChar + " for reason: " + ex.Message);
                }
            }

            return GetSumFromList();
        }

        private int GetSumFromList()
        {
            int sum = 0;

            while (numberList.Count > 0)
            {
                sum += numberList.Last();
                numberList.RemoveLast();
            }

            return sum;
        }

    }
}
