using Microsoft.OpenApi.Writers;

namespace contactApi.Models
{
    public class AddContactRequest
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public long Phone { get; set; }
        public string Address { get; set; }
        public string AddressAndPh => $"{Address} {Phone}";
    }
}
