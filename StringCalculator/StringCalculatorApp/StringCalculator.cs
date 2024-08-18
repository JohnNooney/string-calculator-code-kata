using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StringCalculatorApp
{
    public class StringCalculator
    {
        private LinkedList<int> numberList;
        private string delimiter;
        private string[] delimitedStrings;

        public StringCalculator() {
            numberList = new LinkedList<int>();
        }

        public int Add(string numbersString)
        {
            DelimitString(numbersString);


            foreach (string stringSegment in delimitedStrings)
            {
                LinkedList<char> charsInSegment = new LinkedList<char>(stringSegment.ToList());
                LinkedList<int> numbersInSegment = new LinkedList<int>();

                foreach (char numberChar in charsInSegment)
                {
                    if (numberChar == '-')
                    {
                        numbersInSegment.AddLast(numberChar);
                    }
                    if (int.TryParse(numberChar.ToString(), out int number))
                    {
                        numbersInSegment.AddLast(number);
                    }
                }

                if (delimitedStrings.Length > 1 && int.TryParse(string.Join("", numbersInSegment), out int parsedNumberSegment))
                {
                    numberList.AddLast(parsedNumberSegment);
                }
                else
                {
                    numberList = numbersInSegment;
                }

            }

            return GetSumFromList();
        }
        private void DelimitString(string numbersString)
        {
            if (CheckForDelimiterPrefix(numbersString))
            {
                delimiter = GetDelimiter(numbersString);
                delimitedStrings = numbersString.Split(delimiter);
            }
            else
            {
                delimitedStrings = new string[1] { numbersString };
            }
        }

        private bool CheckForDelimiterPrefix(string numbersString)
        {
            // Check prefix based on this pattern - "//[delimiter]\n[numbers]"
            // Minimum number of 6 characters
            return numbersString.Length > 6 && numbersString.Substring(0, 2) == "//";
        }

        private string GetDelimiter(string numbersString)
        {
            // Get the prefix based on this pattern - "//[delimiter]\n[numbers]"
            string delimiterPrefix = numbersString.Split("\n")[0];
            
            // Return delimiter by removing the starting //
            return delimiterPrefix.Substring(2);
        }

        private int GetSumFromList()
        {
            int sum = 0;
            List<int> negativeNumbersFound = new List<int>();

            while (numberList.Count > 0)
            {
                if(numberList.First() < 0)
                {
                    negativeNumbersFound.Add(numberList.First());
                }

                sum += numberList.First();
                numberList.RemoveFirst();
            }

            if(negativeNumbersFound.Count > 0) { 
                throw new ArgumentException("Negatives not allowed:" + string.Join(',',negativeNumbersFound)); 
            }

            return sum;
        }

    }
}
