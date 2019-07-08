using System;
using library.Models;
using library.Services;
using library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace library.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ILibraryService _libraryService;

        public BaseController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);

            ViewBag.CurrentUserName = String.IsNullOrEmpty(User.Identity.Name) ? null : User.Identity.Name;
        }
    }
}