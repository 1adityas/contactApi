using contactApi.Models;
using Microsoft.EntityFrameworkCore;

namespace contactApi.Data
{
    public class ContactsAPIDbContext : DbContext
    {
         
        public ContactsAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Contacts>Contacts{ get; set; }   

    }
}
