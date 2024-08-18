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
        private List<string> delimiters;
        private string[] delimitedStrings;

        public StringCalculator() {
            numberList = new LinkedList<int>();
            delimitedStrings = new string[1];
            delimiters = new List<string>();

            delimiters.Add(",");
            delimiters.Add("\n");
        }

        public int Add(string numbersString)
        {
            DelimitString(numbersString);


            foreach (string stringSegment in delimitedStrings)
            {
                LinkedList<char> charsInSegment = new LinkedList<char>(stringSegment.ToList());

                LinkedList<int> numbersInSegment = FindNumbersInCharsSegment(charsInSegment);

                addNumbersToList(numbersInSegment);
            }

            return GetSumFromList();
        }
        private void DelimitString(string numbersString)
        {
            if (CheckForDelimiterPrefix(numbersString))
            {
                delimiters.Add(GetDelimiter(numbersString));
                
            }

            delimitedStrings = numbersString.Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries);
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

        private LinkedList<int> FindNumbersInCharsSegment(LinkedList<char> charsInSegment )
        {
            LinkedList<char>.Enumerator enumerator = charsInSegment.GetEnumerator();
            LinkedList<int> numbersInSegment = new LinkedList<int>();

            while (enumerator.MoveNext())
            {
                char currentChar = enumerator.Current;

                if (currentChar == '-' && enumerator.MoveNext())
                {
                    char nextChar = enumerator.Current;

                    if (int.TryParse(nextChar.ToString(), out int number))
                    {
                        numbersInSegment.AddLast(-1 * number);
                    }
                }
                else if (int.TryParse(currentChar.ToString(), out int number))
                {
                    numbersInSegment.AddLast(number);
                }
            }

            return numbersInSegment;
        }

        public void addNumbersToList(LinkedList<int> numbersInSegment)
        {
            if (delimitedStrings.Length > 1)
            {
                AddCompoundNumber(numbersInSegment);
            }
            else
            {
                numberList = numbersInSegment;
            }
        }

        private void AddCompoundNumber(LinkedList<int> numbersInSegment)
        {
            bool negativeFlag = false;

            // check if first number is negative to apply negativity after sanitation
            if (numbersInSegment.Count > 0 && numbersInSegment.First() < 0)
            {
                negativeFlag = true;
            }

            string numberToParse = CombineAndSanatizeNumbersInSegment(numbersInSegment);

            if (int.TryParse(numberToParse, out int parsedNumber))
            {
                // apply negativity 
                parsedNumber = negativeFlag == true ? parsedNumber *= -1 : parsedNumber;

                numberList.AddLast(parsedNumber);
            }
        }

        public string CombineAndSanatizeNumbersInSegment(LinkedList<int> numbersInSegment)
        {
            string joinedNumbers = string.Join("", numbersInSegment);

            return joinedNumbers.Replace("-", ""); ;
        }

        private int GetSumFromList()
        {
            int sum = 0;
            List<int> negativeNumbersFound = new List<int>();

            while (numberList.Count > 0)
            {
                int currentNumber = numberList.First();

                if (currentNumber < 0)
                {
                    negativeNumbersFound.Add(currentNumber);
                    numberList.RemoveFirst();
                    continue;
                }

                sum += currentNumber;
                numberList.RemoveFirst();
            }

            if(negativeNumbersFound.Count > 0) { 
                throw new ArgumentException("Negatives not allowed: " + string.Join(',',negativeNumbersFound)); 
            }

            return sum;
        }

    }
}
