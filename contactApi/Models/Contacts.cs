namespace contactApi.Models
{
    public class Contacts
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }    
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }

        public long Phone { get; set; }
        public string Address { get; set; } 
    }
}
