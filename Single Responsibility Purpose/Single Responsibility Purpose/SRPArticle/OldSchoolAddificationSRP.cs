using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRPArticle
{
    static class OldSchoolAddificationSRP
    {
        //Numbers lengths must be the same
        public static int AddNumbers(int firstNumber, int secondNumber)
        {
            var firstNumberList = Parser.Integer(firstNumber).ToList();
            var secondNumberList = Parser.Integer(secondNumber).ToList();
            var resultList = new List<int>();

            var carry = 0;
            for (int i = 0; i < Math.Min(firstNumberList.Count, secondNumberList.Count); i++)
            {
                var theInt = firstNumberList[i] + secondNumberList[i] + carry;
                if (theInt > 9)
                {
                    carry = 1;
                    resultList.Add(theInt - 10);
                }
                else
                {
                    carry = 0;
                    resultList.Add(theInt);
                }
            }
            if (carry == 1)
                resultList.Add(carry);

            return Combiner.Integer(resultList);

        }
    }

    static class Parser
    {
        public static IEnumerable<int> Integer(int number)
        {
            var length = Convert.ToInt32(Math.Floor(Math.Log10(number) + 1));
            for (int i = 1; i <= length; i++)
            {
                yield return FindDigit(number, i, length);
            }
        }

        private static int FindDigit(int number, int digit, int length)
        {
            if (digit > length)
                return -1;
            else
            {
                return Convert.ToInt32((number / Convert.ToInt32(Math.Pow(10, digit - 1))) % 10);
            }

        }

        public static IEnumerable<char> String(string str)
        {
            foreach (var item in str.ToCharArray())
            {
                yield return item;
            }
        }
    }

    static class Combiner
    {
        public static string String(List<char> strList)
        {
            var s = "";
            foreach (var item in strList)
            {
                s += item;
            }
            return s;
        }
        public static int Integer(List<int> numbers)
        {
            int number = 0;
            for (int i = 0; i < numbers.Count; i++)
            {
                number += numbers[i] * Convert.ToInt32(Math.Pow(10, i));
            }
            return number;
        }
    }
        
}
