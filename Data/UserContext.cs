using Microsoft.EntityFrameworkCore;
using onlyarts.Models;

namespace onlyarts.Data
{
    public class UserContext : DbContext
    {
        public UserContext (DbContextOptions<UserContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
