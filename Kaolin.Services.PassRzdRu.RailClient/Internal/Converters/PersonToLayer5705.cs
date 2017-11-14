using System;
using System.Collections.Generic;

namespace Kaolin.Services.PassRzdRu.RailClient.Internal.Converters
{
    using Kaolin.Models.Rail;

    internal class PersonToLayer5705
    {
        private readonly IReadOnlyDictionary<PassportType, int> _docTypes;

        public PersonToLayer5705()
        {
            _docTypes = new Dictionary<PassportType, int>
            {
                [PassportType.RussianPassport] = 1,
                [PassportType.RussianForeignPassport] = 3,
                [PassportType.ForeignNationalPassport] = 4,
                [PassportType.BirthCertificate] = 6,
                [PassportType.MilitaryCard] = 7,
                [PassportType.SailorPassport] = 5
            };
        }

        public IEnumerable<PassportType> SupportedPassportTypes => _docTypes.Keys;

        public Parser.Structs.Layer5705.RequestPassenger Convert(Person person)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }

            return new Parser.Structs.Layer5705.RequestPassenger
            {
                Id = person.Ref,
                LastName = person.LastName,
                FirstName = person.FirstName,
                MidName = person.MiddleName,
                Gender = person.Gender.Value == Gender.FEMALE ? 1 : 2,
                Birthdate = person.BirthDate?.ToString("dd.MM.yyyy"),
                DocType = ConvertDocType(person.Passport.Type),
                DocNumber = person.Passport.Series + person.Passport.Number,
                Country = CountryCountryCode(person.Passport.Citizenship ?? "RU"),
                Tariff = "Adult" // TODO: add tariff type calculation, ref #52
            };
        }

        private int ConvertDocType(PassportType passportType)
            => _docTypes.ContainsKey(passportType) ? _docTypes[passportType] : throw new ArgumentOutOfRangeException(nameof(passportType), $"PassportType [{Enum.GetName(typeof(PassportType), passportType)}] is not supported by provider");

        private int CountryCountryCode(string isoCountryCode)
        {
            switch (isoCountryCode)
            {
                case "RU": return 114;
                // TODO: add more country codes. ref #58
                default: throw new ArgumentOutOfRangeException(nameof(isoCountryCode), $"CountryCode [{isoCountryCode}] is not supported by provider");
            }
        }
    }
}
