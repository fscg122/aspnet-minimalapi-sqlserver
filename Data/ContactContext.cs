using BeepoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BeepoApi.Data
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> options)
            : base(options)
        {
        }

        public DbSet<Contact> Contact { get; set; } = default!;
    }
}
