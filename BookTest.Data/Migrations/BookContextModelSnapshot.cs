﻿// <auto-generated />
using System;
using BookTest.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookTest.Data.Migrations
{
    [DbContext(typeof(BookContext))]
    partial class BookContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BookTest.Data.Entities.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.HasKey("Id");

                    b.ToTable("Authors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "J.K. Rowling"
                        },
                        new
                        {
                            Id = 2,
                            Name = "J.R.R. Tolkien"
                        },
                        new
                        {
                            Id = 3,
                            Name = "George R.R. Martin"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Stephen King"
                        });
                });

            modelBuilder.Entity("BookTest.Data.Entities.AuthorBook", b =>
                {
                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.HasKey("AuthorId", "BookId");

                    b.HasIndex("BookId");

                    b.ToTable("AuthorBooks");
                });

            modelBuilder.Entity("BookTest.Data.Entities.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("PublicationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.HasKey("Id");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            PublicationDate = new DateTime(1997, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Harry Potter and the Philosopher's Stone"
                        },
                        new
                        {
                            Id = 2,
                            PublicationDate = new DateTime(1998, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Harry Potter and the Chamber of Secrets"
                        },
                        new
                        {
                            Id = 3,
                            PublicationDate = new DateTime(1999, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Harry Potter and the Prisoner of Azkaban"
                        },
                        new
                        {
                            Id = 4,
                            PublicationDate = new DateTime(2000, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Harry Potter and the Goblet of Fire"
                        },
                        new
                        {
                            Id = 5,
                            PublicationDate = new DateTime(2003, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Harry Potter and the Order of the Phoenix"
                        },
                        new
                        {
                            Id = 6,
                            PublicationDate = new DateTime(2005, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Harry Potter and the Half-Blood Prince"
                        },
                        new
                        {
                            Id = 7,
                            PublicationDate = new DateTime(2007, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Harry Potter and the Deathly Hallows"
                        },
                        new
                        {
                            Id = 8,
                            PublicationDate = new DateTime(1937, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "The Hobbit"
                        },
                        new
                        {
                            Id = 9,
                            PublicationDate = new DateTime(1954, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "The Fellowship of the Ring"
                        },
                        new
                        {
                            Id = 10,
                            PublicationDate = new DateTime(1954, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "The Two Towers"
                        },
                        new
                        {
                            Id = 11,
                            PublicationDate = new DateTime(1955, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "The Return of the King"
                        },
                        new
                        {
                            Id = 12,
                            PublicationDate = new DateTime(1996, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "A Game of Thrones"
                        },
                        new
                        {
                            Id = 13,
                            PublicationDate = new DateTime(1998, 11, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "A Clash of Kings"
                        },
                        new
                        {
                            Id = 14,
                            PublicationDate = new DateTime(2000, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "A Storm of Swords"
                        },
                        new
                        {
                            Id = 15,
                            PublicationDate = new DateTime(2005, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "A Feast for Crows"
                        },
                        new
                        {
                            Id = 16,
                            PublicationDate = new DateTime(2011, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "A Dance with Dragons"
                        },
                        new
                        {
                            Id = 17,
                            PublicationDate = new DateTime(1977, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "The Shining"
                        },
                        new
                        {
                            Id = 18,
                            PublicationDate = new DateTime(1986, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "It"
                        });
                });

            modelBuilder.Entity("BookTest.Data.Entities.AuthorBook", b =>
                {
                    b.HasOne("BookTest.Data.Entities.Author", "Author")
                        .WithMany("AuthorBooks")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookTest.Data.Entities.Book", "Book")
                        .WithMany("AuthorBooks")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("BookTest.Data.Entities.Author", b =>
                {
                    b.Navigation("AuthorBooks");
                });

            modelBuilder.Entity("BookTest.Data.Entities.Book", b =>
                {
                    b.Navigation("AuthorBooks");
                });
#pragma warning restore 612, 618
        }
    }
}
