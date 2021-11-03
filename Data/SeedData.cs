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
            using (var context = new OnlyartsContext(
                    serviceProvider.GetRequiredService<
                    DbContextOptions<OnlyartsContext>>()))
            {
                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User { Id=0, Login="boba"},
                        new User { Id=1, Login="aboba"}
                    );
                    return;
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
                    return;
                }
                context.SaveChanges();
            }
        }
    }
}
