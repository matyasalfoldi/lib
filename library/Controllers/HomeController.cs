using library.Services;
using library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using X.PagedList;

namespace library.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(ILibraryService libraryService)
            : base(libraryService)
        {
        }

        public IActionResult Index(Int32? page,
            string filter,
            string searchType = "Title",
            string sortType = "Popularity")
        {
            if (page == null)
                page = 1;
            var books = _libraryService
                .GetBooks(searchType, sortType, filter, page)
                .ToList()
                .ToPagedList(page.Value, 20);

            if (books.Count == 0)
                return RedirectToAction(nameof(Index));

            return View("Index", books);
        }

        public IActionResult Details(Int32? bookId)
        {
            if (bookId == null)
                return RedirectToAction(nameof(Index));

            ViewBag.ImageId = bookId;

            DetailsViewModel details = _libraryService.GetDetails(bookId);
            if (details.Book == null ||
                details.Volumes == null)
                return RedirectToAction(nameof(Index));

            return View("Details", details);
        }

        public FileResult ImageForBook(Int32? bookId)
        {
            Byte[] imageContent = _libraryService.GetBookImage(bookId);

            if (imageContent == null)
                return File("~/images/NoImage.png", "image/png");

            return File(imageContent, "image/png");
        }

    }
}