using Library.Domain.Entities;
using Library.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Configuration
{
    public static class DbContextConfiguration
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData([
                new Author { Id=1, Name = "Stephen", Surname = "King" },
                new Author { Id=2, Name = "Chuck", Surname = "Palahniuk" },
                new Author { Id=3, Name = "Agatha", Surname = "Christie" }
                ]);

            modelBuilder.Entity<Genre>().HasData([
                new Genre { Id=1, Name = "Horror" },
                new Genre { Id=2, Name = "Detective" },
                new Genre { Id=3, Name = "Sci-fi" }
                ]);

            modelBuilder.Entity<Book>().HasData([
                new Book {Id = 1, AuthorId = 1, GenreId = 1, ISBN = "9785170654956", Description="Cool book", Name="IT", TakeDateTime = DateTime.Now, ReturnDateTime = DateTime.Now + TimeSpan.FromDays(30),Status = BookAvailability.Taken},
                new Book {Id = 2, AuthorId = 1, GenreId = 1, ISBN = "9785170624403", Description="Cool book", Name="Carrie", TakeDateTime = DateTime.Now - TimeSpan.FromHours(4),ReturnDateTime=DateTime.Now + TimeSpan.FromDays(30), Status = BookAvailability.Taken},
                new Book {Id = 3, AuthorId = 2, GenreId = 3, ISBN = "9785171475079", Description="Cool book", Name="Fight Club", TakeDateTime = new DateTime(2023,12,16,16,48,29),ReturnDateTime =DateTime.Now + TimeSpan.FromDays(30), Status = BookAvailability.InStock},
                new Book {Id = 4, AuthorId = 3, GenreId = 2, ISBN = "9785040992478", Description="Cool book", Name="Murder on the Orient Express", TakeDateTime = new DateTime(2024,01,05,12,45,37),ReturnDateTime= DateTime.Now + TimeSpan.FromDays(30), Status = BookAvailability.Taken},
                new Book {Id = 5, AuthorId = 3, GenreId = 2, ISBN = "9785043992478", Description="Cool book", Name="Murder on the Orient Express", TakeDateTime = new DateTime(2024,01,05,12,45,37),ReturnDateTime= DateTime.Now + TimeSpan.FromDays(30), Status = BookAvailability.Taken},
                new Book {Id = 6, AuthorId = 3, GenreId = 2, ISBN = "9785045992478", Description="Cool book", Name="Murder on the Orient Express", TakeDateTime = new DateTime(2024,01,05,12,45,37),ReturnDateTime= DateTime.Now + TimeSpan.FromDays(30), Status = BookAvailability.Taken},
                new Book {Id = 7, AuthorId = 3, GenreId = 2, ISBN = "9785046992478", Description="Cool book", Name="Murder on the Orient Express", TakeDateTime = new DateTime(2024,01,05,12,45,37),ReturnDateTime= DateTime.Now + TimeSpan.FromDays(30), Status = BookAvailability.Taken},
                new Book {Id = 8, AuthorId = 3, GenreId = 2, ISBN = "9785047992478", Description="Cool book", Name="Murder on the Orient Express", TakeDateTime = new DateTime(2024,01,05,12,45,37),ReturnDateTime= DateTime.Now + TimeSpan.FromDays(30), Status = BookAvailability.Taken},
                new Book {Id = 9, AuthorId = 3, GenreId = 2, ISBN = "9785048992478", Description="Cool book", Name="Murder on the Orient Express", TakeDateTime = new DateTime(2024,01,05,12,45,37),ReturnDateTime= DateTime.Now + TimeSpan.FromDays(30), Status = BookAvailability.Taken},
                new Book {Id = 10, AuthorId = 3, GenreId = 2, ISBN = "9785090992478", Description="Cool book", Name="Murder on the Orient Express", TakeDateTime = new DateTime(2024,01,05,12,45,37),ReturnDateTime= DateTime.Now + TimeSpan.FromDays(30), Status = BookAvailability.Taken},
                new Book {Id = 11, AuthorId = 3, GenreId = 2, ISBN = "9785010992478", Description="Cool book", Name="Murder on the Orient Express", TakeDateTime = new DateTime(2024,01,05,12,45,37),ReturnDateTime= DateTime.Now + TimeSpan.FromDays(30), Status = BookAvailability.Taken},
                new Book {Id = 12, AuthorId = 3, GenreId = 2, ISBN = "9785240992478", Description="Cool book", Name="Murder on the Orient Express", TakeDateTime = new DateTime(2024,01,05,12,45,37),ReturnDateTime= DateTime.Now + TimeSpan.FromDays(30), Status = BookAvailability.Taken},
                new Book {Id = 13, AuthorId = 3, GenreId = 2, ISBN = "9785340992478", Description="Cool book", Name="Murder on the Orient Express", TakeDateTime = new DateTime(2024,01,05,12,45,37),ReturnDateTime= DateTime.Now + TimeSpan.FromDays(30), Status = BookAvailability.Taken},
                new Book {Id = 14, AuthorId = 3, GenreId = 2, ISBN = "9787040992478", Description="Cool book", Name="Murder on the Orient Express", TakeDateTime = new DateTime(2024,01,05,12,45,37),ReturnDateTime= DateTime.Now + TimeSpan.FromDays(30), Status = BookAvailability.Taken},

             ]);

        }

        public static void ConfigureEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
              .HasKey(b => b.Id);

            modelBuilder.Entity<Book>()
                .HasAlternateKey(b => b.ISBN);

            modelBuilder.Entity<Book>()
                .Property(b => b.ISBN)
                .HasMaxLength(17);

            modelBuilder.Entity<Book>()
                .HasOne<Author>(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .IsRequired();

            modelBuilder.Entity<Book>()
                .HasOne<Genre>(b => b.Genre)
                .WithMany(g => g.Books)
                .HasForeignKey(b => b.GenreId)
                .IsRequired();
        }
    }
}
