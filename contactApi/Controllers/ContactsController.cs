using contactApi.Data;
using Microsoft.AspNetCore.Mvc;

namespace contactApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //here [controller] will return employee name

    public class ContactsController : Controller
    {
        private readonly ContactsAPIDbContext dbContext;
        public ContactsController(ContactsAPIDbContext dbContext)
        {
                
        }
        [HttpGet]
        public IActionResult GetConacts()
        {
            return Ok(dbContext.Contacts.ToList());//cant explictly convert list to action result there fore using  
        }
    }
}
