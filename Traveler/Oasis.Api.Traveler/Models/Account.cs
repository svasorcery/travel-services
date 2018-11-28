using System;

namespace Oasis.Api.Traveler.Models
{
    public class Account
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public string AvatarUri { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdateAt { get; set; }
        public bool IsDeleted { get; set; }


        public Account()
        {

        }
    }
}