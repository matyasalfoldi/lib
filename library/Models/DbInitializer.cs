using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace library.Models
{
    public static class DbInitializer
    {
        private static LibraryContext _context;
        
        public static void Initialize(IApplicationBuilder app, string imageDirectory)
        {
            _context = app.ApplicationServices.GetRequiredService<LibraryContext>();
            
            _context.Database.EnsureCreated();

            if (_context.Users.Any())
            {
                return;
            }

            SeedBooks(imageDirectory);
            SeedUsers();
            SeedLibrarians();
            SeedVolumes();
            SeedRents();
        }

        private static void SeedVolumes()
        {
            var volumes = new Volume[]
            {
                new Volume
                {
                    BookId = 1,
                },
                new Volume
                {
                    BookId = 1,
                },
                new Volume
                {
                    BookId = 2,
                },
                new Volume
                {
                    BookId = 3,
                },
                new Volume
                {
                    BookId = 3,
                },
                new Volume
                {
                    BookId = 4,
                },
                new Volume
                {
                    BookId = 5,
                },
                new Volume
                {
                    BookId = 6,
                },
                new Volume
                {
                    BookId = 7,
                },
                new Volume
                {
                    BookId = 8,
                },
                new Volume
                {
                    BookId = 9,
                },
                new Volume
                {
                    BookId = 10,
                },
                new Volume
                {
                    BookId = 11,
                },
                new Volume
                {
                    BookId = 12,
                },
                new Volume
                {
                    BookId = 13,
                },
                new Volume
                {
                    BookId = 14,
                },
                new Volume
                {
                    BookId = 15,
                },
                new Volume
                {
                    BookId = 16,
                },
                new Volume
                {
                    BookId = 17,
                },
                new Volume
                {
                    BookId = 18,
                },
                new Volume
                {
                    BookId = 19,
                },
                new Volume
                {
                    BookId = 20,
                },
                new Volume
                {
                    BookId = 21,
                },
                new Volume
                {
                    BookId = 22,
                },
                new Volume
                {
                    BookId = 23,
                },
                new Volume
                {
                    BookId = 24,
                },
            };

            foreach (Volume volume in volumes)
            {
                _context.Volumes.Add(volume);
            }

            _context.SaveChanges();
        }

        private static void SeedRents()
        {
            var rents = new Rent[]
            {
                new Rent
                {
                    StartDate = new DateTime(2010,12,3),
                    EndDate = new DateTime(2011,12,5),
                    UserId = 1,
                    VolumeId = 1,
                    Active = false
                },
                new Rent
                {
                    StartDate = new DateTime(2019,3,7),
                    EndDate = new DateTime(2019,4,3),
                    UserId = 1,
                    VolumeId = 1,
                    Active = false
                },
                new Rent
                {
                    StartDate = new DateTime(2019,4,7),
                    EndDate = new DateTime(2019,5,3),
                    UserId = 1,
                    VolumeId = 1,
                    Active = true
                },
                new Rent
                {
                    StartDate = new DateTime(2019,6,7),
                    EndDate = new DateTime(2020,1,3),
                    UserId = 1,
                    VolumeId = 1,
                    Active = false
                },
                new Rent
                {
                    StartDate = new DateTime(2019,6,7),
                    EndDate = new DateTime(2020,1,3),
                    UserId = 1,
                    VolumeId = 2,
                    Active = false
                },
                new Rent
                {
                    StartDate = new DateTime(2019,6,7),
                    EndDate = new DateTime(2020,1,3),
                    UserId = 1,
                    VolumeId = 3,
                    Active = false
                },
                new Rent
                {
                    StartDate = new DateTime(2019,6,7),
                    EndDate = new DateTime(2020,1,3),
                    UserId = 2,
                    VolumeId = 4,
                    Active = false
                }
            };

            foreach (Rent rent in rents)
            {
                _context.Rents.Add(rent);
            }

            _context.SaveChanges();
        }

        private static void SeedUsers()
        {
            var passwordHasher = new PasswordHasher<User>();
            List<User> users = new List<User>();
            User jani = new User
            {
                UserName = "janijozsi",
                Name = "János József",
                Address = "xyz rtz str",
                Email = "abc@asd.cre",
                PhoneNumber = "1234567",
                LockoutEnabled = true,
                NormalizedUserName = "JANIJOZSI",
                NormalizedEmail = "ABC@ASD.CRE",
                SecurityStamp  = Guid.NewGuid().ToString(),
            };
            jani.PasswordHash = passwordHasher.HashPassword(jani, "Abcdabcd1");
            users.Add(jani);
            User bela = new User
            {
                UserName = "belaafelfedezo",
                Name = "Béla a Felfedező",
                Address = "xyz rtz st",
                Email = "qwe@ertt.er",
                PhoneNumber = "12345678",
                LockoutEnabled = true,
                NormalizedUserName = "BELAAFELFEDEZO",
                NormalizedEmail = "QWE@ERTT.ER",
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            bela.PasswordHash = passwordHasher.HashPassword(bela, "Bbcdabcd1");
            users.Add(bela);

            foreach (User user in users)
            {
                _context.Users.Add(user);
            }

            _context.SaveChanges();
        }
        private static void SeedLibrarians()
        {
            var passwordHasher = new PasswordHasher<Librarian>();
            Librarian lib = new Librarian
            {
                UserName = "librarian",
                PhoneNumber = "1234567",
                LockoutEnabled = true,
                NormalizedUserName = "LIBRARIAN",
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            lib.PasswordHash = passwordHasher.HashPassword(lib, "Abcdabcd1");
            
            _context.Librarians.Add(lib);

            _context.SaveChanges();
        }
        private static void SeedBooks(string imageDirectory)
        {
            var books = new Book[]
            {
                new Book
                {
                    Title = "Book1",
                    Author = "Kis Béla",
                    ReleaseDate = new DateTime(2010,10,1),
                    ISBN =  "9789633120347",
                    CoverImage  =  File.ReadAllBytes(Path.Combine(imageDirectory,"book.png"))
                },
                new Book
                {
                    Title = "Book2",
                    Author = "Kis Béla",
                    ReleaseDate = new DateTime(2000,3,1),
                    ISBN =  "9789633120348",
                    CoverImage  =  File.ReadAllBytes(Path.Combine(imageDirectory,"book.png"))
                },
                new Book
                {
                    Title = "Book3",
                    Author = "Kis Béla",
                    ReleaseDate = new DateTime(2000,7,1),
                    ISBN =  "9789633120349",
                    CoverImage  =  File.ReadAllBytes(Path.Combine(imageDirectory,"book.png"))
                },
                new Book
                {
                    Title = "Book4",
                    Author = "Kis Béla",
                    ReleaseDate = new DateTime(2010,10,1),
                    ISBN =  "9789633120327",
                    CoverImage  =  File.ReadAllBytes(Path.Combine(imageDirectory,"book.png"))
                },
                new Book
                {
                    Title = "Book5",
                    Author = "Kis Béla",
                    ReleaseDate = new DateTime(2000,3,1),
                    ISBN =  "9789633120338",
                    CoverImage  =  File.ReadAllBytes(Path.Combine(imageDirectory,"book.png"))
                },
                new Book
                {
                    Title = "Book6",
                    Author = "Kis Béla",
                    ReleaseDate = new DateTime(2000,7,1),
                    ISBN =  "9789633120359",
                    CoverImage  =  File.ReadAllBytes(Path.Combine(imageDirectory,"book.png"))
                },
                new Book
                {
                    Title = "Book7",
                    Author = "Kis Béla",
                    ReleaseDate = new DateTime(2010,10,1),
                    ISBN =  "1789633120347",
                    CoverImage  =  File.ReadAllBytes(Path.Combine(imageDirectory,"book.png"))
                },
                new Book
                {
                    Title = "Book8",
                    Author = "Kis Béla",
                    ReleaseDate = new DateTime(2000,3,1),
                    ISBN =  "2789633120348",
                    CoverImage  =  File.ReadAllBytes(Path.Combine(imageDirectory,"book.png"))
                },
                new Book
                {
                    Title = "Book9",
                    Author = "Kis Béla",
                    ReleaseDate = new DateTime(2000,7,1),
                    ISBN =  "3789633120349",
                    CoverImage  =  File.ReadAllBytes(Path.Combine(imageDirectory,"book.png"))
                },
                new Book
                {
                    Title = "Book10",
                    Author = "Kis Béla",
                    ReleaseDate = new DateTime(2010,10,1),
                    ISBN =  "4789633120327",
                    CoverImage  =  File.ReadAllBytes(Path.Combine(imageDirectory,"book.png"))
                },
                new Book
                {
                    Title = "Book11",
                    Author = "Kis Béla",
                    ReleaseDate = new DateTime(2000,3,1),
                    ISBN =  "5789633120338",
                    CoverImage  =  File.ReadAllBytes(Path.Combine(imageDirectory,"book.png"))
                },
                new Book
                {
                    Title = "Book12",
                    Author = "Kis Béla",
                    ReleaseDate = new DateTime(2000,7,1),
                    ISBN =  "6789633120359",
                    CoverImage  =  File.ReadAllBytes(Path.Combine(imageDirectory,"book.png"))
                },
                new Book
                {
                    Title = "Book13",
                    Author = "Kis Béla",
                    ReleaseDate = new DateTime(2010,10,1),
                    ISBN =  "7789633120327",
                    CoverImage  =  File.ReadAllBytes(Path.Combine(imageDirectory,"book.png"))
                },
                new Book
                {
                    Title = "Book14",
                    Author = "Kis Béla",
                    ReleaseDate = new DateTime(2000,3,1),
                    ISBN =  "8789633120338",
                    CoverImage  =  File.ReadAllBytes(Path.Combine(imageDirectory,"book.png"))
                },
                new Book
                {
                    Title = "Book15",
                    Author = "Kis Béla",
                    ReleaseDate = new DateTime(2000,7,1),
                    ISBN =  "9189633120359",
                    CoverImage  =  File.ReadAllBytes(Path.Combine(imageDirectory,"book.png"))
                },
                new Book
                {
                    Title = "Book16",
                    Author = "Kis Béla",
                    ReleaseDate = new DateTime(2010,10,1),
                    ISBN =  "1289633120347",
                    CoverImage  =  File.ReadAllBytes(Path.Combine(imageDirectory,"book.png"))
                },
                new Book
                {
                    Title = "Book17",
                    Author = "Kis Béla",
                    ReleaseDate = new DateTime(2000,3,1),
                    ISBN =  "1389633120348",
                    CoverImage  =  File.ReadAllBytes(Path.Combine(imageDirectory,"book.png"))
                },
                new Book
                {
                    Title = "Book18",
                    Author = "Kis Béla",
                    ReleaseDate = new DateTime(2000,7,1),
                    ISBN =  "1489633120349",
                    CoverImage  =  File.ReadAllBytes(Path.Combine(imageDirectory,"book.png"))
                },
                new Book
                {
                    Title = "Book19",
                    Author = "Kis Béla",
                    ReleaseDate = new DateTime(2010,10,1),
                    ISBN =  "1589633120327",
                    CoverImage  =  File.ReadAllBytes(Path.Combine(imageDirectory,"book.png"))
                },
                new Book
                {
                    Title = "Book20",
                    Author = "Kis Béla",
                    ReleaseDate = new DateTime(2000,3,1),
                    ISBN =  "1689633120338",
                    CoverImage  =  File.ReadAllBytes(Path.Combine(imageDirectory,"book.png"))
                },
                new Book
                {
                    Title = "Book21",
                    Author = "Kis Béla",
                    ReleaseDate = new DateTime(2000,7,1),
                    ISBN =  "1719663120359",
                    CoverImage  =  File.ReadAllBytes(Path.Combine(imageDirectory,"book.png"))
                },
                new Book
                {
                    Title = "Book22",
                    Author = "Kis Béla",
                    ReleaseDate = new DateTime(2010,10,1),
                    ISBN =  "1819633120327",
                    CoverImage  =  File.ReadAllBytes(Path.Combine(imageDirectory,"book.png"))
                },
                new Book
                {
                    Title = "Book23",
                    Author = "Zöld István",
                    ReleaseDate = new DateTime(2000,3,1),
                    ISBN =  "1919633120338",
                    CoverImage  =  File.ReadAllBytes(Path.Combine(imageDirectory,"book.png"))
                },
                new Book
                {
                    Title = "Book24",
                    Author = "Pál József",
                    ReleaseDate = new DateTime(2000,7,1),
                    ISBN =  "1719663120359",
                    CoverImage  =  File.ReadAllBytes(Path.Combine(imageDirectory,"book.png"))
                }
            };

            foreach (Book book in books)
            {
                _context.Books.Add(book);
            }

            _context.SaveChanges();
        }
    }
}
