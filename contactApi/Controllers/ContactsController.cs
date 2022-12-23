using contactApi.Data;
using contactApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace contactApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //here [controller] will return employee name

    public class ContactsController : Controller
    {
        private readonly ContactsAPIDbContext dbContext;
        public ContactsController(ContactsAPIDbContext dbContext)
        {
            this.dbContext= dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetConacts()
        {
            return Ok(await dbContext.Contacts.ToListAsync());//cant explictly convert list to action result there fore using  
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
            if (contact == null) return NotFound();

            return Ok(contact);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
            if (contact == null) return NotFound();

            dbContext.Remove(contact);
            await dbContext.SaveChangesAsync();
            return Ok(contact);
        }

        [HttpPost]
        public async Task <IActionResult> AddContacts(AddContactRequest addContactRequest) 
        {
            var contact = new Contacts()
            {//what are these curly braces ??
                Id = Guid.NewGuid(),
                Address = addContactRequest.Address,
                Email = addContactRequest.Email,
                FullName = addContactRequest.FullName,
                Phone = addContactRequest.Phone//if i pass 000 its showing some error somehting.
            };
            await dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync(); 
            return Ok(contact);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, UpdateContactRequest updateContactRequest)
        {
            var contact = dbContext.Contacts.Find(id);
            if (contact != null){
                contact.FullName = updateContactRequest.FullName;
                contact.Phone=updateContactRequest.Phone;   
                contact.Email = updateContactRequest.Email;
                contact.Address = updateContactRequest.Address;

                await dbContext.SaveChangesAsync();
                return Ok(contact);
            }

            return NotFound();
        }
    }
}
