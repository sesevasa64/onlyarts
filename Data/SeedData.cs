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
                //пользователи и боты
                User[] user = new User[5];
                String[][] us = new String[][]
                {
                    new String[]
                    {
                        "alexeisuhanov", 
                        "cringe1", 
                        "Alex", 
                        "asd@mail.ru", 
                        "https://img5.goodfon.ru/wallpaper/nbig/a/fd/cyberpunk-2077-samurai-logo.jpg",
                        "Ну да, я Лёха, просто Лёха"
                    },
                    new String[] 
                    {
                        "a.yunusov", 
                        "b@sed", 
                        "Yun", 
                        "zxcvn@mail.ru", 
                        "https://realava.ru/wp-content/gallery/kartinki-na-avu-dlya-patsanov/Q71nV-KbbXc.jpg",
                        "Учу C#"
                    },
                    new String[] 
                    {
                        "oleg.rad", 
                        "b@sed", 
                        "Radius", 
                        "qwerty@mail.ru", 
                        "https://i.pinimg.com/736x/4b/7b/b4/4b7bb44e4cfcc154af52ef6b6f3f8f1f.jpg",
                        "Квантовый человек"
                    },
                    new String[] 
                    {
                        "sesevasa", 
                        "zxc1000-7", 
                        "Aboba",
                        "fghjd@mail.ru",
                        "https://i.pinimg.com/originals/db/66/49/db664957ab0ba56c9b74b691c545bfde.jpg",
                        "Торчист - питонист"
                    },
                    new String[] 
                    {
                        "sokolik",
                        "sd1928!log",
                        "sokol",
                        "sawqdq@mail.ru",
                        "https://mobimg.b-cdn.net/v3/fetch/c4/c493aac67877288476b0fc52d55f55cf.jpeg",
                        "Вы думаете я вас не переиграю? Я вас уничтожу!"
                    }
                }; 
                for(int i = 0; i < 5; i++)
                {
                    user[i] =  new User 
                    { 
                        Id = i + 1, 
                        Login = us[i][0],
                        Password = us[i][1],
                        Nickname = us[i][2],
                        Email = us[i][3],
                        LinkToAvatar = us[i][4],
                        Info = us[i][5],
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


                //типы подписок
                SubType FreeSub = new SubType
                {
                    Id = 1,
                    Type = "Бесплатная",
                    Cost = 0,
                    Duration = 0,   
                    SubLevel = 0
                };
                SubType PaidSub_1 = new SubType
                {
                    Id = 2,
                    Type = "Платная подписка, на 1 месяц",
                    Cost = 100,
                    Duration = 30,   
                    SubLevel = 1
                };
                SubType PaidSub_2 = new SubType
                {
                    Id = 3,
                    Type = "Платная подписка, на 6 месяц",
                    Cost = 500,
                    Duration = 180,   
                    SubLevel = 1
                };
                SubType VeryPaidSub = new SubType
                {
                    Id = 4,
                    Type = "Очень платная подписка",
                    Cost = 500,
                    Duration = 30,   
                    SubLevel = 2
                };

                //подписки
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
                            SubType = FreeSub
                        };
                }

                //контент
                String[] link = new String[] 
                {
                    "https://img-fotki.yandex.ru/get/5800/18746936.23/0_6929f_3ff82e94_XXL.jpg",
                    "https://i.mycdn.me/i?r=AzEPZsRbOZEKgBhR0XGMT1RkMYCDWEYeb5SlcObYi8Mo1KaKTM5SRkZCeTgDn6uOyic",
                    "https://avatars.mds.yandex.net/i?id=b65bd6f623c3ee550acc09c2fd9a7ef6-4504894-images-thumbs&n=13",
                    "https://api.nsn.fm/storage/medialib/360264/large_image-a12ba51225f6f81a83f167e668d83417.jpg",
                    "https://kurganfm.ru/wp-content/uploads/2019/08/1431031451_ThinkstockPhotos-174308224.jpg",
                    "https://eda-land.ru/images/article/orig/2018/07/kak-proverit-kachestvo-tvoroga-v-domashnih-usloviyah.jpg",
                    "https://funik.ru/wp-content/uploads/2018/10/17478da42271207e1d86.jpg"
                };
                String[] name = new String[]
                {
                    "Ночной Челябинск",
                    "Ночной Екатиренбург",
                    "Pepe",
                    "Копейск",
                    "Спорт",
                    "Творог",
                    "Котики"
                }; 
                String[] description = new String[]
                {
                    "Челябинск ночью",
                    "Екатиренбург ночью",
                    "С сыном",
                    "Копейск. Просто Копейск. Просто копейка",
                    "Спорт пацаны, это наше все! ЗОЖ!!! ЗОЖ!!! ЗОЖ!!!",
                    "Люблю творог. Он такой красивый, он такой вкусный!",
                    "Котики милые? Конечно они всегда милые!"
                }; 
                Content[] con = new Content[7];
                for(int i = 0; i < 7; i++)
                {
                    con[i] = new Content
                    {
                        Id = i + 1,
                        User = user[i / 2],
                        Name = name[i],
                        Description = description[i],
                        ContentType = "pic",
                        LinkToPreview = link[i],
                        LinkToBlur = link[i],
                        LikesCount = 0,
                        DislikesCount = 0,
                        ViewCount = 0
                    };
                }

                //изображения в контенте
                String[] link_img = new String[]
                {
                    "https://img-fotki.yandex.ru/get/5800/18746936.23/0_6929f_3ff82e94_XXL.jpg",
                    "https://nashchelyabinsk.ru/media/images/bb4bb9fca82847be952dccfe1a1d17fe.normal.jpg",
                    "https://i.mycdn.me/i?r=AzEPZsRbOZEKgBhR0XGMT1RkMYCDWEYeb5SlcObYi8Mo1KaKTM5SRkZCeTgDn6uOyic",
                    "http://ekaterinburg-2018.ru/upload/5953696b47254562d7b89ff61c4f90b4.jpg",
                    "https://avatars.mds.yandex.net/i?id=b65bd6f623c3ee550acc09c2fd9a7ef6-4504894-images-thumbs&n=13",
                    "https://cs11.pikabu.ru/post_img/2019/10/07/8/og_og_157045542525526807.jpg",
                    "https://maps.spravka-region.ru/img/212/05053017434787899.jpg",
                    "http://photocdn.photogoroda.com/source2/cn3159/r5507/c5522/18912333.jpg?v=20171213112136",
                    "https://avatars.mds.yandex.net/get-zen_doc/3582174/pub_6085a9d211963e55626a2364_6085aa3b5bfb251ca7cb1b0f/scale_1200",
                    "https://edutorg.ru/image/cache/catalog/tovary/trenazheri-shtangi-diski-ganteli/gantel-v-vinilovoj-obolochke-9lb-4-05-kg-700x700.jpg",
                    "https://images.ru.prom.st/420780686_w640_h640_shtangi-razbornye-kupit.jpg",
                    "https://avatars.mds.yandex.net/get-zen_doc/1714257/pub_5ecaa0918689093b98383f17_5ecaa58116dc9e6bc0902fb1/scale_1200",
                    "https://mykaleidoscope.ru/uploads/posts/2021-09/1632866239_9-mykaleidoscope-ru-p-tvorog-s-klubnikoi-krasivo-foto-10.jpg",
                    "https://www.zastavki.com/pictures/originals/2019Food_Cottage_cheese_with_berries_in_a_wooden_bowl_on_the_table_with_apples_131863_.jpg",
                    "https://ufkis33.ru/800/600/https/cdn.bellinigroup.ru/upload/202004/5e9805d4252de_1080x1080_fit.jpeg",
                    "https://krasivosti.pro/uploads/posts/2021-07/1626113550_58-krasivosti-pro-p-dobrii-kotik-koti-krasivo-foto-64.jpg",
                    "https://phonoteka.org/uploads/posts/2021-07/1625190189_5-phonoteka-org-p-zastavki-na-telefon-kotiki-krasivie-zastav-5.jpg",
                    "https://i02.fotocdn.net/s116/f3e8783ad7851098/public_pin_l/2633618515.jpg",
                    "https://crosti.ru/patterns/00/21/da/b144f6ffd7/picture.jpg",
                    "https://funik.ru/wp-content/uploads/2018/10/cd494811d5a5edcb3159.jpg"
                };

                int[] n_img = new int[] {2, 2, 2, 2, 3, 4, 5};
                Image[] img = new Image[20];
                for(int i = 0, m = 0; i < 7; i++)
                {
                    for(int j = 0; j < n_img[i]; j++)
                    {
                        img[m] = new Image
                        {
                            Id = m + 1,
                            LinkToImage = link_img[m],
                            Content = con[i]
                        };
                        m++;
                    }
                }
                
                //теги
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
                Tag T4 = new Tag
                {
                    Id = 4,
                    TagName = "спорт"
                };
                Tag T5 = new Tag
                {
                    Id = 5,
                    TagName = "еда"
                };



                //линк тег
                LinkTag[] LT = new LinkTag[10];
                Tag[] LT_tag = new Tag[] 
                {T1, T2, T2, T3, T1, T1, T4, T1, T5, T1};
                Content[] LT_content = new Content[] 
                {con[0], con[1], con[2], con[2], con[3], con[4], con[4], con[5], con[5], con[6]};
                for(int i = 0; i < 10; i++)
                {
                    LT[i] = new LinkTag
                    {
                        Id = i + 1,
                        Tag = LT_tag[i],
                        Content = LT_content[i]
                    };
                }

                Reaction[] reac = new Reaction[700];
                for(int i = 0; i < 700; i++)
                {
                    reac[i] = new Reaction
                    {
                        Id = i + 1,
                        Type = Convert.ToBoolean(i % 2),
                        User = bot[i / 7],
                        Content = con[i / 100]
                    };
                }
                var users = context.Users.Where(U => U.Id > 0);
                context.Users.RemoveRange(users);
                context.Users.AddRange(user);
                context.Users.AddRange(bot);
                context.SaveChanges();

                var subtypes = context.SubTypes.Where(C => C.Id > 0);
                context.SubTypes.RemoveRange(subtypes);
                context.SubTypes.AddRange(FreeSub, PaidSub_1, PaidSub_2, VeryPaidSub);
                context.SaveChanges();

                var tags = context.Tags.Where(T => T.Id > 0);
                context.Tags.RemoveRange(tags);
                context.Tags.AddRange(T1,T2,T3, T4, T5);
                context.SaveChanges();

                var subscriptions = context.Subscriptions.Where(U => U.Id > 0);
                context.Subscriptions.RemoveRange(subscriptions);
                context.Subscriptions.AddRange(sub);
                context.SaveChanges();

                var contents = context.Contents.Where(C => C.Id > 0);
                context.Contents.RemoveRange(contents);
                context.Contents.AddRange(con);
                context.SaveChanges();

                var linktags = context.LinkTags.Where(LT => LT.Id > 0);
                context.LinkTags.RemoveRange(linktags);
                context.LinkTags.AddRange(LT);
                context.SaveChanges();

                var images = context.Images.Where(Im => Im.Id > 0);
                context.Images.RemoveRange(images);
                context.Images.AddRange(img);
                context.SaveChanges();

                var reaction = context.Reactions.Where(Im => Im.Id > 0);
                context.Reactions.RemoveRange(reaction);
                context.Reactions.AddRange(reac);
                context.SaveChanges();
            }
        }
    }
}
