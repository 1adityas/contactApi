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
        //private string aditya="aditya";

        public ContactsController(ContactsAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] Contacts obj)
        {
            if (obj == null) return BadRequest();
            
            var user=await dbContext.Contacts.FirstOrDefaultAsync(x=>x.Id==obj.Id && x.Password==obj.Password);
            if (user == null) return NotFound();

            return Ok(new
            {
                Message="login Successful"
            }
            ) ;

        }

        //already implementing it using post request.
        //[HttpPost("register")]
        //public async Task<IActionResult> RegisterUser([FromBody] Contacts obj)
        //{
        //    if (obj == null) return BadRequest();

        //    var user = await dbContext.Contacts.FirstOrDefaultAsync(x => x.Id == obj.Id && x.Password == obj.Password);
        //    if (user == null) return NotFound();

        //    return Ok(new
        //    {
        //        Message = "login Successful"
        //    }
        //    );

        //}

        [HttpGet]
        public async Task<IActionResult> GetConacts()
        {
            string aditya = "";
            return Ok(await dbContext.Contacts.Where(acc => acc.FullName == "aditya").ToListAsync());//cant explictly convert list to action result there fore using  
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
                Phone = addContactRequest.Phone,//if i pass 000 its showing some error somehting.
                Password= addContactRequest.Password
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
