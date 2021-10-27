using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using onlyarts.Models;

namespace onlyarts.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new UserContext(
                    serviceProvider.GetRequiredService<
                    DbContextOptions<UserContext>>()))
            {
                if (context.Users.Any())
                {
                    return;
                }
                context.Users.AddRange(
                    new User(0, "boba"),
                    new User(1, "aboba")
                );
                context.SaveChanges();
            }
        }
    }
}
