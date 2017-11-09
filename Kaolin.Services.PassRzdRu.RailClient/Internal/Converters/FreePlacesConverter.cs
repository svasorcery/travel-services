using System;
using System.Linq;
using System.Collections.Generic;

namespace Kaolin.Services.PassRzdRu.RailClient.Internal.Converters
{
    public static class FreePlacesConverter
    {
        public static int[] Convert(string places)
        {
            var freePlaces = new List<int>();

            foreach (var item in places.Split(','))
            {
                if (String.IsNullOrEmpty(item))
                    continue;

                if (item.Contains("-"))
                {
                    freePlaces.AddRange(GetRangePlaces(item));
                }
                else
                {
                    freePlaces.Add(GetSinglePlace(item));
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
