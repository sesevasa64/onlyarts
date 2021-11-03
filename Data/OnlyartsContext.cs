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
        public DbSet<Reaction> Reactions { get; set; }
        public DbSet<Content> Contents { get; set; }
    }
}
