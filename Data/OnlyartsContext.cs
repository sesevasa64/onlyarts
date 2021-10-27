using Microsoft.EntityFrameworkCore;
using onlyarts.Models;

namespace onlyarts.Data
{
    public class OnlyartsContext : DbContext
    {
        public OnlyartsContext(DbContextOptions<OnlyartsContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
