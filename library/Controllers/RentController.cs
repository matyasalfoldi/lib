using library.Models;
using library.Services;
using library.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace library.Controllers
{
	public class RentController : BaseController
    {
		private readonly UserManager<User> _userManager;
        
		public RentController(ILibraryService libraryService,
			UserManager<User> userManager)
			: base(libraryService)
		{
			_userManager = userManager;
		}
        
		[HttpGet]
        public async Task<IActionResult> Index(Int32? volumeId)
        {
            RentViewModel rent = _libraryService.NewRent(volumeId);

            if (rent == null)
                return RedirectToAction("Index", "Home");

	        if (User.Identity.IsAuthenticated)
	        {
		        User user = await _userManager.FindByNameAsync(User.Identity.Name);

		        if (user != null)
		        {
			        rent.UserAddress = user.Address;
                    rent.Name = user.Name;
                    rent.UserEmail = user.Email;
			        rent.UserPhoneNumber = user.PhoneNumber;
		        }
	        }

			return View("Index", rent);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Int32? volumeId, RentViewModel rent)
        {
            if (volumeId == null || rent == null)
                return RedirectToAction("Index", "Home");

            rent.Volume = _libraryService.GetVolume(volumeId);
            rent.Book = _libraryService.GetBook(rent.Volume.BookId);
            
            if (rent.Volume == null)
                return RedirectToAction("Index", "Home");

            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            switch (_libraryService.ValidateRent(rent.RentStartDate, rent.RentEndDate, volumeId.Value))
            {
                case RentDateError.StartInvalid:
                    ModelState.AddModelError("RentStartDate", "The start date is too soon!");
                    break;
                case RentDateError.EndInvalid:
                    ModelState.AddModelError("RentEndDate", "The end date is invalid!");
                    break;
                case RentDateError.LengthInvalid:
                    ModelState.AddModelError("RentEndDate", "The end date is the same day as the starting day!");
                    break;
                case RentDateError.Conflicting:
                    ModelState.AddModelError("RentStartDate", "There rent date conflict with an already existing one!");
                    break;
            }

			if (!ModelState.IsValid)
                return View("Index", rent);

	        User user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (!_libraryService.SaveRent(volumeId, user.UserName, rent))
            {
                ModelState.AddModelError("", "Renting the volume was unsuccessfull, please try again!");
                return View("Index", rent);
            }

			ViewBag.Message = "The renting of the volume was successfull!";
            return View("Result", rent);
        }
    }
}