using System;
using System.Collections.Generic;
using Oasis.Api.Traveler.Models;

namespace Oasis.Api.Traveler.DbContexts
{
    public class InMemoryStorageContext
    {
        public readonly ICollection<Account> Accounts = new List<Account>()
        {
            new Account
            {
                Id = 1,
                PersonId = 1,
                UserName = "svasorcery",
                Password = "gfhjkm123",
                Email = "sva.sorcery@gmail.com",
                AvatarUri = "https://avatars2.githubusercontent.com/u/20383833",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            }
        };

        public readonly ICollection<Person> Persons = new List<Person>()
        {
            new Person
            {
                Id = 1,
                Gender = Gender.MALE,
                FirstName = "Vladimir",
                LastName = "Sinyavsky"
            }
        };
    }
}
