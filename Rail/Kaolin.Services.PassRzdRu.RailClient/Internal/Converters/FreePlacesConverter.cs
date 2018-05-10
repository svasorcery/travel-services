using System;
using System.Linq;
using System.Collections.Generic;
using Kaolin.Models.Rail;

namespace Kaolin.Services.PassRzdRu.RailClient.Internal.Converters
{
    public static class FreePlacesConverter
    {
        public static IEnumerable<QueryCars.Result.CarPlace> Convert(string placeNumbers, Price price = null)
        {
            var freePlaces = new List<QueryCars.Result.CarPlace>();

            foreach (var placeNumber in placeNumbers.Split(','))
            {
                if (String.IsNullOrEmpty(placeNumber))
                    continue;

                if (placeNumber.Contains('-'))
                {
                    freePlaces.AddRange(GetRangePlaces(placeNumber, price));
                }
                else
                {
                    freePlaces.Add(GetSinglePlace(placeNumber, price));
                }
            }

            return freePlaces;
        }

        private static QueryCars.Result.CarPlace GetSinglePlace(string num, Price price = null)
        {
            char genderLetter = ' ';

            if (Char.IsLetter(num.Last()))
            {
                genderLetter = num.Last();
                num = num.Remove(num.Length - 1);
            }

            return new QueryCars.Result.CarPlace(int.Parse(num), GetGenderType(genderLetter), price);
        }

        private static IEnumerable<QueryCars.Result.CarPlace> GetRangePlaces(string numRange, Price price = null)
        {
            var result = new List<QueryCars.Result.CarPlace>();
            char genderLetter = ' ';

            if (Char.IsLetter(numRange.Last()))
            {
                genderLetter = numRange.Last();
                numRange = numRange.Remove(numRange.Length - 1);
            }

            var gender = GetGenderType(genderLetter);
            var range = numRange.Split('-').Select(x => int.Parse(x));

            for (int number = range.ElementAt(0); number <= range.ElementAt(1); number++)
            {
                result.Add(new QueryCars.Result.CarPlace(number, gender, price));
            }

            return result;
        }

        private static string GetGenderType(char letter)
        {
            switch (letter)
            {
                case 'М': return "male";
                case 'Ж': return "female";
                case 'С': return "mix";
                case 'Ц': return "any";
                default:  return "none";
            }
        }
    }
}
