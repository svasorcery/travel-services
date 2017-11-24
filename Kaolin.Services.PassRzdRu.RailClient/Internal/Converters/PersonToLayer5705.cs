using System;
using System.Collections.Generic;

namespace Kaolin.Services.PassRzdRu.RailClient.Internal.Converters
{
    using Kaolin.Models.Rail;
    using Kaolin.Services.PassRzdRu.Parser.Structs;

    internal class PersonToLayer5705
    {
        private readonly IReadOnlyDictionary<PassportType, Layer5705.DocumentTypes> _docTypes;

        public PersonToLayer5705()
        {
            _docTypes = new Dictionary<PassportType, Layer5705.DocumentTypes>
            {
                [PassportType.RussianPassport] = Layer5705.DocumentTypes.PN,
                [PassportType.RussianForeignPassport] = Layer5705.DocumentTypes.PSP,
                [PassportType.ForeignNationalPassport] = Layer5705.DocumentTypes.NP,
                [PassportType.BirthCertificate] = Layer5705.DocumentTypes.SR,
                [PassportType.MilitaryCard] = Layer5705.DocumentTypes.VB,
                [PassportType.SailorPassport] = Layer5705.DocumentTypes.PM
            };
        }

        public IEnumerable<PassportType> SupportedPassportTypes => _docTypes.Keys;

        public Layer5705.RequestPassenger Convert(Person person, DateTime departDate, GetCars.Result.AgeRestrictions ageLimits)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }

            return new Layer5705.RequestPassenger
            {
                Id = person.Ref,
                LastName = person.LastName,
                FirstName = person.FirstName,
                MidName = person.MiddleName,
                Gender = person.Gender.Value == Gender.FEMALE ? Layer5705.Gender.FEMALE : Layer5705.Gender.MALE,
                Birthdate = person.BirthDate?.ToString("dd.MM.yyyy"),
                DocType = ConvertDocType(person.Passport.Type),
                DocNumber = person.Passport.Series + person.Passport.Number,
                Country = CountryCountryCode(person.Passport.Citizenship ?? "RU"),
                Tariff = GetTariffByBirthDate(person.BirthDate.Value, departDate, ageLimits)
            };
        }

        private Layer5705.DocumentTypes ConvertDocType(PassportType passportType)
            => _docTypes.ContainsKey(passportType) ? 
                _docTypes[passportType] : 
                throw new ArgumentOutOfRangeException(nameof(passportType), $"PassportType [{Enum.GetName(typeof(PassportType), passportType)}] is not supported by provider");

        private int CountryCountryCode(string isoCountryCode)
        {
            switch (isoCountryCode)
            {
                case "RU": return 114;
                // TODO: add more country codes. ref #58
                default: throw new ArgumentOutOfRangeException(nameof(isoCountryCode), $"CountryCode [{isoCountryCode}] is not supported by provider");
            }
        }

        private string GetTariffByBirthDate(DateTime birthDate, DateTime departDate, GetCars.Result.AgeRestrictions limits)
        {
            var ageYears = GetYearsDiff(birthDate, departDate);

            if (ageYears >= limits.ChildWithPlace)
            {
                return Parser.Constants.RailPassengerCategories.Adult;
            }

            if (ageYears < limits.InfantWithoutPlace)
            {
                return Parser.Constants.RailPassengerCategories.BabyWithoutPlace;
            }

            return Parser.Constants.RailPassengerCategories.Child;
        }

        private int GetYearsDiff(DateTime start, DateTime end)
        {
            int years = end.Year - start.Year;

            if ((end.Month == start.Month && end.Day < start.Day) ||
                (end.Month < start.Month))
            {
                years--;
            }

            return years;
        }
    }
}
