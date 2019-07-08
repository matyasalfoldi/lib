using library.Models;
using library.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace library.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly LibraryContext _context;
        private readonly RentDateValidator _rentDateValidator;
        public IEnumerable<Book> GetBooks(
            string searchType,
            string sortType,
            string filter,
            Int32? page)
        {
            if (page == null)
                page = 1;

            if (sortType == "Lexicographical")
            {
                var orderedBooks = _context.Books
                                    .Include(book => book.Volumes)
                                    .OrderBy(b => b.Title);
                return searchType == "Author"
                                    ? orderedBooks
                                        .Where(book => filter == null
                                                    || book.Author.Contains(filter))
                                    : orderedBooks
                                        .Where(book => filter == null
                                                    || book.Title.Contains(filter));
            }
            else
            {
                var popularity = _context.Volumes
                    .Include(v => v.Rents)
                    .GroupBy(g => g.BookId)
                    .Select(go => new { Id = go.Key, Count = go.Sum(o => o.Rents.Count()) });

                var orderedBooks = (from b in _context.Books
                                    join v in popularity
                                    on b.Id equals v.Id
                                    orderby v.Count descending
                                    select b).Include(bo => bo.Volumes);
                return searchType == "Author"
                                    ? orderedBooks
                                        .Where(book => filter == null
                                                    || book.Author.Contains(filter))
                                    : orderedBooks
                                        .Where(book => filter == null
                                                    || book.Title.Contains(filter));
            }
        }
        public LibraryService(LibraryContext context)
        {
            _context = context;
            _rentDateValidator = new RentDateValidator(_context);
        }

        public Book GetBook(Int32? bookId)
        {
            if (bookId == null)
                return null;
            return _context.Books
                .Include(b => b.Volumes)
                .FirstOrDefault(b => b.Id == bookId);
        }

        public Volume GetVolume(Int32? volumeId)
        {
            if (volumeId == null)
                return null;
            return _context.Volumes
                .Include(v => v.Book)
                .FirstOrDefault(vo => vo.Id == volumeId);
        }

        public IEnumerable<Volume> GetVolumes(Int32? bookId)
        {
            if (bookId == null)
                return null;
            return _context.Volumes
                .Where(v => v.BookId == bookId);
        }

        public Byte[] GetBookImage(Int32? bookId)
        {
            if (bookId == null)
                return null;

            return _context.Books
                .Where(b => b.Id == bookId)
                .Select(book => book.CoverImage)
                .FirstOrDefault();
        }

        public RentViewModel NewRent(Int32? volumeId)
        {
            if (volumeId == null)
                return null;


            RentViewModel rent = new RentViewModel
            {
                Volume = _context.Volumes
                            .FirstOrDefault(vol => vol.Id == volumeId)
            };
            if (rent.Volume == null)
                return null;

            rent.Book = _context.Books.FirstOrDefault(b => b.Id == rent.Volume.BookId);

            rent.RentStartDate = DateTime.Today.Date;
            rent.RentEndDate = rent.RentStartDate + TimeSpan.FromDays(1);

            return rent;
        }

        public DetailsViewModel GetDetails(Int32? bookId)
        {
            DetailsViewModel details = new DetailsViewModel
            {
                Book = _context.Books
                            .Include(b => b.Volumes)
                            .FirstOrDefault(bo => bo.Id == bookId),

                Volumes = _context.Volumes
                            .Include(v => v.Rents)
                            .Where(vo => vo.BookId == bookId).ToList()
            };

            return details;
        }

        public RentDateError ValidateRent(DateTime start, DateTime end, Int32? volumeId)
        {
            if (volumeId == null)
                return RentDateError.None;

            return _rentDateValidator.Validate(start, end, volumeId.Value);
        }

        public Boolean SaveRent(Int32? volumeId, String userName, RentViewModel rent)
        {
            if (volumeId == null || rent == null)
                return false;

            if (!Validator.TryValidateObject(rent, new ValidationContext(rent, null, null), null))
                return false;

            if (_rentDateValidator.Validate(rent.RentStartDate, rent.RentEndDate, volumeId.Value) != RentDateError.None)
                return false;

            User user = _context.Users
                .FirstOrDefault(u => u.UserName == userName);

            if (user == null)
                return false;

            _context.Rents.Add(new Rent
            {
                VolumeId = rent.Volume.Id,
                UserId = user.Id,
                StartDate = rent.RentStartDate,
                EndDate = rent.RentEndDate,
                Active = false
            });

            try
            {
                //Update the default values, if the user gives a different phone number, or address
                user.PhoneNumber = user.PhoneNumber == rent.UserPhoneNumber ?
                                    user.PhoneNumber :
                                    rent.UserPhoneNumber;
                user.Address = user.Address == rent.UserAddress ?
                                user.Address :
                                rent.UserAddress;
                user.Email = user.Email == rent.UserEmail ?
                                user.Email :
                                rent.UserEmail;
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
