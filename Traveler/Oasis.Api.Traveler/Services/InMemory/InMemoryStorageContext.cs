using System.Collections.Generic;
using Oasis.Api.Traveler.Models;

namespace Oasis.Api.Traveler.Services.InMemory
{
    public class InMemoryStorageContext
    {
        public readonly ICollection<Account> Accounts;
        public readonly ICollection<Person> Persons;
    }
}
