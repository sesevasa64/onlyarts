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
        public DbSet<Tag> Tags { get; set; }
        public DbSet<SubType> SubTypes { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<LinkTag> LinkTags { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}
