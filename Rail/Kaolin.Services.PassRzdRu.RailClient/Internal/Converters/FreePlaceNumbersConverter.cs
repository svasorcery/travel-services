using System;
using System.Linq;
using System.Collections.Generic;

namespace Kaolin.Services.PassRzdRu.RailClient.Internal.Converters
{
    public static class FreePlaceNumbersConverter
    {
        public static IEnumerable<int> Convert(string placeNumbers)
        {
            var freePlaces = new List<int>();

            foreach (var placeNumber in placeNumbers.Split(','))
            {
                if (String.IsNullOrEmpty(placeNumber))
                    continue;

                if (placeNumber.Contains("-"))
                {
                    freePlaces.AddRange(GetRangePlaces(placeNumber));
                }
                else
                {
                    freePlaces.Add(GetSinglePlace(placeNumber));
                }
            }

            return freePlaces.ToArray();
        }

        private static int GetSinglePlace(string num)
        {
            if (Char.IsLetter(num.Last()))
            {
                num = num.Remove(num.Length - 1);
            }

            return int.Parse(num);
        }

        private static IEnumerable<int> GetRangePlaces(string numRange)
        {
            var result = new List<int>();

            if (Char.IsLetter(numRange.Last()))
            {
                numRange = numRange.Remove(numRange.Length - 1);
            }

            var range = numRange.Split('-').Select(x => int.Parse(x));

            for (int number = range.ElementAt(0); number <= range.ElementAt(1); number++)
            {
                result.Add(number);
            }

            return result;
        }
    }
}
