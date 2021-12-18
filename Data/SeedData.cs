using System;
using System.Linq;
using System.IO;
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
                //теги
                var tags = context.Tags.Where(T => T.Id > 0);
                context.Tags.RemoveRange(tags);
                
                String[] str_tag = File.ReadAllLines("Data\\tags.txt");
                Tag[] tag = new Tag[str_tag.Length];
                for(int i = 0; i < str_tag.Length; i++)
                {
                    tag[i] = new Tag
                    {
                        Id = i + 1,
                        TagName = str_tag[i]
                    };
                }

                context.Tags.AddRange(tag);
                context.SaveChanges();

                //пользователи и боты
                 var users = context.Users.Where(U => U.Id > 0);
                context.Users.RemoveRange(users);
            
                String[] str_user = File.ReadAllLines("Data\\users.txt");
                User[] user = new User[(str_user.Length + 1) / 7];
                for(int i = 0; i < str_user.Length; i += 7)
                {
                    user[i / 7] =  new User 
                    { 
                        Id = i / 7 + 1, 
                        Login = str_user[i],
                        Password = str_user[i + 1],
                        Nickname = str_user[i + 2],
                        Email = str_user[i + 3],
                        LinkToAvatar = str_user[i + 4],
                        Info = str_user[i + 5],
                        RegisDate = DateTime.Now,
                        Money = 0
                    };
                }
                User[] bot = new User[100];
                for(int i = 0; i < 100; i++)
                {
                    bot[i] = new User
                        {
                            Id = 6 + i,
                            Login = "bot" + i.ToString(),
                            Password = "passw" + i.ToString(),
                            Nickname = "Beautiful Bot" + i.ToString(),
                            Email = "asd" + i.ToString() + "@mail.ru",
                            LinkToAvatar = "https://tgram.ru/wiki/bots/image/zernovozbot.jpg",
                            Info = "Я самый лучший бот" + i.ToString() + " в мире. Завидуешь? ну и правильно!",
                            RegisDate = DateTime.Now,
                            Money = 10 * i
                        };
                }

                context.Users.AddRange(user);
                context.Users.AddRange(bot);
                context.SaveChanges();

                //типы подписок
                var subtypes = context.SubTypes.Where(C => C.Id > 0);
                context.SubTypes.RemoveRange(subtypes);
        
                String[] str_subtype = File.ReadAllLines("Data\\subtypes.txt");
                SubType[] subtype = new SubType[(str_subtype.Length + 1) / 5];
                for(int i = 0; i < str_subtype.Length; i += 5)
                {
                    subtype[i / 5] = new SubType
                    {
                        Id = i / 5 + 1,
                        Type = str_subtype[i],
                        Cost = Convert.ToDecimal(str_subtype[i + 1]),
                        Duration = Convert.ToInt32(str_subtype[i + 2]),   
                        SubLevel = Convert.ToByte(str_subtype[i + 3])
                    };
                }

                context.SubTypes.AddRange(subtype);
                context.SaveChanges();

                //подписки
                var subscriptions = context.Subscriptions.Where(U => U.Id > 0);
                context.Subscriptions.RemoveRange(subscriptions);
                
                DateTime date1 = new DateTime(9999, 1, 1); 
                Subscription[] sub = new Subscription[100];
                for(int i = 0; i < 100; i++)
                {
                    sub[i] = new Subscription
                        {
                            Id = i + 1,
                            EndSubDate = date1,
                            SubUser = bot[i],
                            Author = user[i / 20],
                            SubType = subtype[0]
                        };
                }

                context.Subscriptions.AddRange(sub);
                context.SaveChanges();

                //контент
                var contents = context.Contents.Where(C => C.Id > 0);
                context.Contents.RemoveRange(contents);
                
                String[] str_content = File.ReadAllLines("Data\\contents.txt");
                Content[] content = new Content[(str_content.Length + 1) / 5];
                for(int i = 0; i < str_content.Length; i += 5)
                {
                    content[i / 5] = new Content
                    {
                        Id = i / 5 + 1,
                        Name = str_content[i + 1],
                        Description = str_content[i + 2],
                        ContentType = "pic",
                        LinkToPreview = str_content[i],
                        LinkToBlur = str_content[i],
                        ViewCount = 100 * i,
                        User = user[Convert.ToInt32(str_content[i + 3])],
                        SubType = subtype[0]
                    };
                }

                context.Contents.AddRange(content);
                context.SaveChanges();

                //изображения в контенте
                var images = context.Images.Where(Im => Im.Id > 0);
                context.Images.RemoveRange(images);
        
                String[] str_image = File.ReadAllLines("Data\\images.txt");
                Image[] image = new Image[str_image.Length / 2];
                for(int i = 0; i < str_image.Length; i += 2)
                {
                    image[i / 2] = new Image
                    {
                        Id = i / 2 + 1,
                        LinkToImage = str_image[i],
                        Content = content[Convert.ToInt32(str_image[i + 1])]
                    };
                }
               
                context.Images.AddRange(image);
                context.SaveChanges();

                //линк тег
                var linktags = context.LinkTags.Where(LT => LT.Id > 0);
                context.LinkTags.RemoveRange(linktags);

                int[] LT_tag = new int[]
                {
                    0, 1, 
                    0, 1, 
                    2, 
                    0, 1,
                    0, 3,
                    0, 4,
                    0, 5,
                    0, 5,
                    6, 
                    6,
                    0, 7, 
                    7, 14,
                    0,
                    0, 8,
                    9, 14,
                    10, 14,
                    11, 14,
                    0, 12,
                    13,
                    9, 14
                };
                int[] LT_content = new int[]
                {
                    0, 0, 
                    1, 1, 
                    2, 
                    3, 3, 
                    4, 4,
                    5, 5,
                    6, 6, 
                    7, 7,
                    8, 
                    9,
                    10, 10, 
                    11, 11,
                    12,
                    13, 13,
                    14, 14,
                    15, 15, 
                    16, 16,
                    17, 17,
                    18, 
                    19, 19
                };
                LinkTag[] LT = new LinkTag[LT_tag.Length];
                for(int i = 0; i < LT_tag.Length; i++)
                {
                    LT[i] = new LinkTag
                    {
                        Id = i + 1,
                        Tag = tag[LT_tag[i]],
                        Content = content[LT_content[i]]
                    };
                }

                context.LinkTags.AddRange(LT);
                context.SaveChanges();

                //реакции
                var reaction = context.Reactions.Where(Im => Im.Id > 0);
                context.Reactions.RemoveRange(reaction);

                Reaction[] reac = new Reaction[700];
                for(int i = 0; i < 700; i++)
                {
                    reac[i] = new Reaction
                    {
                        Id = i + 1,
                        Type = Convert.ToBoolean(i % 2),
                        User = bot[i / content.Length],
                        Content = content[i % content.Length]
                    };
                }
                
                context.Reactions.AddRange(reac);
                context.SaveChanges();
            }
        }
    }
}
