using System;

namespace Kaolin.Models.Rail
{
    public class Passport
    {
        public PassportType Type { get; private set; }
        public string Series { get; private set; }
        public string Number { get; private set; }
        public string Citizenship { get; private set; }
        public DateTime? Expire { get; private set; }

        public Passport(PassportType type, string number) 
            : this(type, number, null, null)
        {
        }

        public Passport(PassportType type, string number, string citizenship, DateTime? expire)
            : this(type, null, number, citizenship, expire)
        {
        }

        public Passport(PassportType type, string series, string number)
            : this(type, series, number, null, null)
        {
        }

        public Passport(PassportType type, string series, string number, string citizenship, DateTime? expire)
        {
            Type = type;
            Series = series;
            Number = number;
            Citizenship = citizenship;
            Expire = expire;
        }
    }
        
    public enum PassportType
    {
        /// <summary>
        /// Общегражданский паспорт
        /// </summary>
        RussianPassport,
        /// <summary>
        /// Общегражданский заграничный паспорт
        /// </summary>
        RussianForeignPassport,
        /// <summary>
        /// Национальный паспорт
        /// </summary>
        ForeignNationalPassport,
        /// <summary>
        /// Свидетельство о рождении
        /// </summary>
        BirthCertificate,
        /// <summary>
        /// Военный билет
        /// </summary>
        MilitaryCard,
        /// <summary>
        /// Удостоверение личности для военнослужащих
        /// </summary>
        MilitaryOfficerCard,
        /// <summary>
        /// Свидетельство на возвращение в страны СНГ
        /// </summary>
        ReturnToCisCertificate,
        /// <summary>
        /// Дипломатический паспорт
        /// </summary>
        DiplomaticPassport,
        /// <summary>
        /// Служебный паспорт
        /// </summary>
        ServicePassport,
        /// <summary>
        /// Паспорт моряка
        /// </summary>
        SailorPassport,
        /// <summary>
        /// Удостоверение личности лица без гражданства
        /// </summary>
        StatelessPersonIdentityCard,
        /// <summary>
        /// Вид на жительство
        /// </summary>
        ResidencePermit,
        /// <summary>
        /// Временное удостоверение личности
        /// </summary>
        RussianTemporaryIdentityCard
    }
}
