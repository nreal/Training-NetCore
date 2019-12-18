using Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class MachineContext : DbContext
    {
        public MachineContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
