namespace Oasis.Api.Traveller.Models
{
    public class Traveller
    {
        public string Id { get; set; }
        public int PersonId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public string AvatarUri { get; set; }

        
        public Traveller()
        {

        }
    }
}