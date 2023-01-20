using contactApi.Models;

namespace contactApi.Tests
{
    public class AddContactRequestShould
    {
        [Fact]
        public void CalculateAddressPhone()
        {
            AddContactRequest addPhT= new AddContactRequest();
            addPhT.Address = "hitech";
            addPhT.Phone = 97171717177;

            Assert.Equal("hitech 97171717177",addPhT.AddressAndPh);
        }
    }
}