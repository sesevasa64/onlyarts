﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using onlyarts.Data;

namespace onlyarts.Migrations
{
    [DbContext(typeof(OnlyartsContext))]
    partial class OnlyartsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("onlyarts.Models.Content", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ContentType")
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<int>("DislikesCount")
                        .HasColumnType("int");

                    b.Property<int>("LikesCount")
                        .HasColumnType("int");

                    b.Property<string>("LinkToBlur")
                        .HasColumnType("longtext");

                    b.Property<string>("LinkToPreview")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int?>("SubTypeId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("ViewCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SubTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Contents");
                });

            modelBuilder.Entity("onlyarts.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("ContentId")
                        .HasColumnType("int");

                    b.Property<string>("LinkToImage")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ContentId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("onlyarts.Models.LinkTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("ContentId")
                        .HasColumnType("int");

                    b.Property<int?>("TagId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ContentId");

                    b.HasIndex("TagId");

                    b.ToTable("LinkTags");
                });

            modelBuilder.Entity("onlyarts.Models.Reaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("ContentId")
                        .HasColumnType("int");

                    b.Property<bool>("Type")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ContentId");

                    b.HasIndex("UserId");

                    b.ToTable("Reactions");
                });

            modelBuilder.Entity("onlyarts.Models.SubType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<byte>("SubLevel")
                        .HasColumnType("tinyint unsigned");

                    b.Property<string>("Type")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("SubTypes");
                });

            modelBuilder.Entity("onlyarts.Models.Subscription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AuthorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndSubDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("SubTypeId")
                        .HasColumnType("int");

                    b.Property<int?>("SubUserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("SubTypeId");

                    b.HasIndex("SubUserId");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("onlyarts.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("TagName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("onlyarts.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("LinkToAvatar")
                        .HasColumnType("longtext");

                    b.Property<string>("Login")
                        .HasColumnType("longtext");

                    b.Property<decimal>("Money")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Nickname")
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("RegisDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("onlyarts.Models.Content", b =>
                {
                    b.HasOne("onlyarts.Models.SubType", "SubType")
                        .WithMany()
                        .HasForeignKey("SubTypeId");

                    b.HasOne("onlyarts.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("SubType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("onlyarts.Models.Image", b =>
                {
                    b.HasOne("onlyarts.Models.Content", "Content")
                        .WithMany()
                        .HasForeignKey("ContentId");

                    b.Navigation("Content");
                });

            modelBuilder.Entity("onlyarts.Models.LinkTag", b =>
                {
                    b.HasOne("onlyarts.Models.Content", "Content")
                        .WithMany()
                        .HasForeignKey("ContentId");

                    b.HasOne("onlyarts.Models.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId");

                    b.Navigation("Content");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("onlyarts.Models.Reaction", b =>
                {
                    b.HasOne("onlyarts.Models.Content", "Content")
                        .WithMany()
                        .HasForeignKey("ContentId");

                    b.HasOne("onlyarts.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Content");

                    b.Navigation("User");
                });

            modelBuilder.Entity("onlyarts.Models.Subscription", b =>
                {
                    b.HasOne("onlyarts.Models.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");

                    b.HasOne("onlyarts.Models.SubType", "SubType")
                        .WithMany()
                        .HasForeignKey("SubTypeId");

                    b.HasOne("onlyarts.Models.User", "SubUser")
                        .WithMany()
                        .HasForeignKey("SubUserId");

                    b.Navigation("Author");

                    b.Navigation("SubType");

                    b.Navigation("SubUser");
                });
#pragma warning restore 612, 618
        }
    }
}
