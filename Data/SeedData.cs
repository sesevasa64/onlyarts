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
                    Email = "asd@mail.ru",
                    LinkToAvatar = "https://img5.goodfon.ru/wallpaper/nbig/a/fd/cyberpunk-2077-samurai-logo.jpg",
                    Info = "Ну да, я Лёха, просто Лёха",
                    RegisDate = DateTime.Now,
                    Money = 0
                };
                User u2 = new User
                { 
                    Id=2, 
                    Login="a.yunusov",
                    Password = "b@sed",
                    Nickname = "Yun",
                    Email = "zxcvn@mail.ru",
                    LinkToAvatar = "https://realava.ru/wp-content/gallery/kartinki-na-avu-dlya-patsanov/Q71nV-KbbXc.jpg",
                    Info = "Учу C#",
                    RegisDate = DateTime.Now,
                    Money = 0
                };
                User u3 = new User
                { 
                    Id=3, 
                    Login="oleg.rad",
                    Password = "b@sed",
                    Nickname = "Rad",
                    Email = "qwerty@mail.ru",
                    LinkToAvatar = "https://i.pinimg.com/736x/4b/7b/b4/4b7bb44e4cfcc154af52ef6b6f3f8f1f.jpg",
                    Info = "Квантовый человек",
                    RegisDate = DateTime.Now,
                    Money = 0
                };
                User u4 =  new User 
                { 
                    Id=4, 
                    Login="sesevasa",
                    Password = "zxc1000-7",
                    Nickname = "Aboba",
                    Email = "fghjd@mail.ru",
                    LinkToAvatar = "https://i.pinimg.com/originals/db/66/49/db664957ab0ba56c9b74b691c545bfde.jpg",
                    Info = "Торчист - питонист",
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

                Tag T1 = new Tag
                {
                    Id = 1,
                    TagName = "фото"
                };
                Tag T2 = new Tag
                {
                    Id = 2,
                    TagName = "арт"
                };
                Tag T3 = new Tag
                {
                    Id = 3,
                    TagName = "мем"
                };

                LinkTag LT1 = new LinkTag
                {
                    Id = 1,
                    Tag = T1,
                    Content = Con1
                };
                LinkTag LT2 = new LinkTag
                {
                    Id = 2,
                    Tag = T1,
                    Content = Con2
                };
                LinkTag LT3 = new LinkTag
                {
                    Id = 3,
                    Tag = T2,
                    Content = Con3
                };
                LinkTag LT4 = new LinkTag
                {
                    Id = 4,
                    Tag = T3,
                    Content = Con3
                };
                var users = context.Users.Where(U => U.Id > 0);
                context.Users.RemoveRange(users);
                context.Users.AddRange(u1,u2,u3,u4);
                context.SaveChanges();             

                var contents = context.Contents.Where(C => C.Id > 0);
                context.Contents.RemoveRange(contents);
                context.Contents.AddRange(Con1,Con2,Con3);
                context.SaveChanges();

                var tags = context.Tags.Where(T => T.Id > 0);
                context.Tags.RemoveRange(tags);
                context.Tags.AddRange(T1,T2,T3);
                context.SaveChanges();

                var linktags = context.LinkTags.Where(LT => LT.Id > 0);
                context.LinkTags.RemoveRange(linktags);
                context.LinkTags.AddRange(LT1,LT2,LT3,LT4);
                context.SaveChanges();

            }
        }
    }
}
