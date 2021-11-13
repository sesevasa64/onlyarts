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
                User u1 = new User
                {
                    Id=1, 
                    Login="alexeisuhanov",
                    Password = "cringe1",
                    Nickname = "Alex",
                    LinkToAvatar = "ava1",
                    RegisDate = DateTime.Now,
                    Money = 0
                };
                User u2 = new User
                { 
                    Id=2, 
                    Login="a.yunusov",
                    Password = "b@sed",
                    Nickname = "Yun",
                    LinkToAvatar = "ava2",
                    RegisDate = DateTime.Now,
                    Money = 0
                };
                User u3 = new User
                { 
                    Id=3, 
                    Login="oleg.rad",
                    Password = "b@sed",
                    Nickname = "Rad",
                    LinkToAvatar = "ava3",
                    RegisDate = DateTime.Now,
                    Money = 0
                };
                User u4 =  new User 
                { 
                    Id=4, 
                    Login="sesevasa",
                    Password = "zxc1000-7",
                    Nickname = "Aboba",
                    LinkToAvatar = "ava4",
                    RegisDate = DateTime.Now,
                    Money = 0
                };
                Content Con1 = new Content 
                {
                    Id = 1,
                    User = u1,
                    Name = "Ночной Челябинск",
                    Description = "Челябинск ночью",
                    ContentType = "pic",
                    LinkToPreview = "https://pbs.twimg.com/media/EWyzpzRXsAA5UV6.jpg",
                    LinkToBlur = "https://pbs.twimg.com/media/EWyzpzRXsAA5UV6.jpg",
                    LikesCount = 0,
                    DislikesCount = 0,
                    ViewCount = 0
                };
                Content Con2 = new Content 
                {
                    Id = 2,
                    User = u1,
                    Name = "Ночной Екатиренбург",
                    Description = "Екатиренбург ночью",
                    ContentType = "pic",
                    LinkToPreview = "https://i.mycdn.me/i?r=AzEPZsRbOZEKgBhR0XGMT1RkMYCDWEYeb5SlcObYi8Mo1KaKTM5SRkZCeTgDn6uOyic",
                    LinkToBlur = "https://i.mycdn.me/i?r=AzEPZsRbOZEKgBhR0XGMT1RkMYCDWEYeb5SlcObYi8Mo1KaKTM5SRkZCeTgDn6uOyic",
                    LikesCount = 0,
                    DislikesCount = 0,
                    ViewCount = 0
                };
                Content Con3 = new Content 
                {
                    Id = 3,
                    User = u2,
                    Name = "Pepe",
                    Description = "С сыном",
                    ContentType = "pic",
                    LinkToPreview = "https://avatars.mds.yandex.net/i?id=b65bd6f623c3ee550acc09c2fd9a7ef6-4504894-images-thumbs&n=13",
                    LinkToBlur = "https://avatars.mds.yandex.net/i?id=b65bd6f623c3ee550acc09c2fd9a7ef6-4504894-images-thumbs&n=13",
                    LikesCount = 0,
                    DislikesCount = 0,
                    ViewCount = 0
                };

               
                var users = context.Users.Where(U => U.Id > 0);
                context.Users.RemoveRange(users);

                context.Users.AddRange(u1,u2,u3,u4);
                context.SaveChanges();             

                var contents = context.Contents.Where(C => C.Id > 0);
                context.Contents.RemoveRange(contents);
        
                context.Contents.AddRange(Con1,Con2,Con3);
                context.SaveChanges();
            }
        }
    }
}
