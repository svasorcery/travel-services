using System;
using System.Linq;
using System.Collections.Generic;

namespace Kaolin.Services.PassRzdRu.RailClient.Internal.Converters
{
    using Kaolin.Models.Rail;
    using Kaolin.Services.PassRzdRu.Parser.Structs;
    
    internal class PassengerToLayer5705
    {
        private readonly IReadOnlyDictionary<PassportType, Layer5705.DocumentTypes> _docTypes;

        public PassengerToLayer5705()
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

        public Layer5705.RequestPassenger ToLayer5705(PassengerRequest passenger, DateTime departDate, GetCars.Result.AgeRestrictions ageLimits)
        {
            if (passenger == null)
            {
                throw new ArgumentNullException(nameof(passenger));
            }

            return new Layer5705.RequestPassenger
            {
                Id = passenger.Ref,
                LastName = passenger.LastName,
                FirstName = passenger.FirstName,
                MidName = passenger.MiddleName,
                Gender = passenger.Gender.Value == Gender.FEMALE ? Layer5705.Gender.FEMALE : Layer5705.Gender.MALE,
                Birthdate = passenger.BirthDate?.ToString("dd.MM.yyyy"),
                DocType = ConvertDocType(passenger.Passport.Type),
                DocNumber = passenger.Passport.Series + passenger.Passport.Number,
                Country = passenger.Passport.Citizenship.RzdId ?? 114,
                Tariff = GetTariffByBirthDate(passenger.BirthDate.Value, departDate, ageLimits),
                Insurance = passenger.InsuranceProviderId,
                PolicyDate = passenger.PolicyEndDate,
                LoyalNum = passenger.LoalityCardNumber,
                UniversalNum = passenger.PartnerCardNumber,
                Volunteer = passenger.PartnerCardNumber != null
            };
        }

        public Passenger ToPassenger(int @ref, Layer5705.ResultPassenger passenger)
        {
            if (passenger == null)
            {
                throw new ArgumentNullException(nameof(passenger));
            }

            var person = new Person(
                passenger.GenderId == 1 ? Gender.FEMALE : Gender.MALE,
                passenger.FirstName,
                passenger.MidName,
                passenger.LastName,
                DateTime.Parse(passenger.BirthDate), 
                new Passport(ConvertPassportType(passenger.DocType), passenger.DocNumber)
            );

            return new Passenger(
                @ref,
                person, 
                insurance: ConvertInsurance(passenger.Insurance), 
                policy: ConvertMedicalPolicy(passenger.Policy)
                );
        }


        private Layer5705.DocumentTypes ConvertDocType(PassportType passportType)
            => _docTypes.ContainsKey(passportType) ? 
                _docTypes[passportType] : 
                throw new ArgumentOutOfRangeException(nameof(passportType), $"PassportType [{Enum.GetName(typeof(PassportType), passportType)}] is not supported by provider");

        private PassportType ConvertPassportType(int docTypeId)
        {
            var filter = _docTypes.Where(x => (int)x.Value == docTypeId);
            return filter.Count() > 0 ?
                filter.First().Key :
                throw new ArgumentOutOfRangeException(nameof(docTypeId), $"Provider's DocType [{docTypeId}] is not supported");
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

        private InsuranceProviderBase ConvertInsurance(Layer5705.Insurance insurance)
        {
            if (insurance == null)
                return null;

            return new InsuranceProviderBase
            {
                Id = insurance.Id,
                InsuranceCost = insurance.Cost,
                FullName = insurance.Name,
                ShortName = insurance.ShortName,
                OfferUrl = insurance.Href
            };
        }

        private MedicalPolicy ConvertMedicalPolicy(Layer5705.MedicalPolicy policy)
        {
            if (policy == null)
                return null;

            return new MedicalPolicy
            {
                StatusId = policy.StatusId,
                Number = policy.Number,
                Cost = policy.Cost,
                DateStart = policy.StartDate,
                DateEnd = policy.FinishDate,
                AreaId = policy.AreaId
            };
        }
    }
}
