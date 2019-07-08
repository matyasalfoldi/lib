using library.Models;
using library.ViewModels;
using System;
using System.Collections.Generic;

namespace library.Services
{
    public interface ILibraryService
    {
        IEnumerable<Book> GetBooks(
            string searchType,
            string sortType,
            string filter,
            Int32? page);

        DetailsViewModel GetDetails(Int32? bookId);

        Book GetBook(Int32? bookId);
        Volume GetVolume(Int32? volumeId);
        IEnumerable<Volume> GetVolumes(Int32? bookId);

        Byte[] GetBookImage(Int32? bookId);

        RentViewModel NewRent(Int32? volumeId);

        RentDateError ValidateRent(DateTime start, DateTime end, Int32? volumeId);

        Boolean SaveRent(Int32? volumeId, String userName, RentViewModel rent);
    }
}
