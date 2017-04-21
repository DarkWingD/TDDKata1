using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorKata
{
    public class Calculator
    {
        public static int Add(string numbers)
        {
            if (numbers == "") return 0;
            List<string> delimList = new List<string>();
            List<string> negatives = new List<string>();
            var returnValue = 0;
            ProcessDelimeters(ref numbers, ref delimList);
            returnValue = ProcessNumbers(numbers, delimList, negatives, returnValue);
            ProcessNegatives(negatives);
            return returnValue;
        }

        private static void ProcessNegatives(List<string> negatives)
        {
            if (negatives.Count > 0)
                throw new ArgumentException($"Negatives not allowed = {string.Join(",", negatives)}");
        }

        private static int ProcessNumbers(string numbers, List<string> delimList, List<string> negatives, int returnValue)
        {
            foreach (var number in numbers.Split(delimList.ToArray(), StringSplitOptions.None))
            {
                int numberInt = int.Parse(number);
                if (numberInt < 0)
                    negatives.Add(number);
                else if (numberInt < 1000)
                    returnValue += numberInt;
            }
            return returnValue;
        }

        private static void ProcessDelimeters(ref string numbers, ref List<string> delimList)
        {
            if (numbers.StartsWith("//"))
            {
                if (numbers.Contains("[") && numbers.Contains("]"))
                {
                    var delimSection = numbers.Substring(2, (numbers.IndexOf("\n") - 2));
                    delimList = delimSection.Replace("][", ",").Replace("[", "").Replace("]", "").Split(',').ToList();
                    numbers = numbers.Substring(numbers.IndexOf("\n") + 1);
                }
                else
                {
                    delimList.Add(numbers.Substring(2, 1));
                    numbers = numbers.Substring(4);
                }
            }
            if (delimList.Count() == 0)
            {
                delimList.Add(",");
                delimList.Add("\n");
            }
        }
    }
}
