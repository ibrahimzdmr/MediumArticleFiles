using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRPArticle
{
    static class OldSchoolAddification
    {
        //Numbers lengths must be the same
        public static int AddNumbers(int firstNumber, int secondNumber)
        {
            var firstNumberList = NumberToList(firstNumber).ToList();
            var secondNumberList = NumberToList(secondNumber).ToList();
            var resultList = new List<int>();

            var carry = 0;
            for (int i = 0; i < Math.Min(firstNumberList.Count, secondNumberList.Count); i++)
            {
                var theInt = firstNumberList[i] + secondNumberList[i] + carry;
                if (theInt > 9)
                {
                    carry = 1;
                    resultList.Add(theInt-10);
                }
                else
                {
                    carry = 0;
                    resultList.Add(theInt);
                }
            }
            if (carry == 1)
                resultList.Add(carry);

            
            return ListToNumber(resultList);
        }

        private static int FindIntegerLength(int number)
        {
            return Convert.ToInt32(Math.Floor(Math.Log10(number)+1));
        }
        private static int FindDigit(int number, int digit)
        {
            int length = FindIntegerLength(number);
            return FindDigit(number, digit, length);
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

        private static IEnumerable<int> NumberToList(int number)
        {
            int length = FindIntegerLength(number);
            return NumberToList(number,length);
        }

        private static IEnumerable<int> NumberToList(int number, int length)
        {
            for (int i = 1; i <= length; i++)
            {
                yield return FindDigit(number, i, length);
            }
        }

        private static string NumberListToString(List<int> numbers)
        {
            var s = "";
            foreach (var item in numbers)
            {
                s = item.ToString() + s;
            }
            return s;
        }

        private static int ListToNumber(List<int> numbers)
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
