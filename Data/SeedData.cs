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
           /* using (var context = new OnlyartsContext(
                    serviceProvider.GetRequiredService<
                    DbContextOptions<OnlyartsContext>>()))
            {
                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User { Id=1, Login="boba"},
                        new User { Id=2, Login="aboba"}
                    );
                }
                if (!context.Contents.Any())
                {
                    context.Contents.AddRange(
                        new Content {
                            Id = 1,
                            Name = "boba",
                            Description = "ne aboba",
                            LikesCount = 0,
                            DislikesCount = 0,
                        },
                        new Content {
                            Id = 2,
                            Name = "aboba",
                            Description = "ne boba",
                            LikesCount = 0,
                            DislikesCount = 0,
                        }
                    );
                }
                context.SaveChanges();
            }*/
        }
    }
}
