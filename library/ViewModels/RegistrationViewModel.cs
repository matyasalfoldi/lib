using System;
using System.ComponentModel.DataAnnotations;

namespace library.ViewModels
{
    public class RegistrationViewModel : UserViewModel
    {
        [Required(ErrorMessage = "You must give a name.")]
        [RegularExpression("^[A-Za-z0-9_-]{5,40}$", ErrorMessage = "The usernames format, or length is not between the limits.")]
        public String UserName { get; set; }
        
        [Required(ErrorMessage = "You must give a password.")]
        [DataType(DataType.Password)]
        public String UserPassword { get; set; }
        
        [Required(ErrorMessage = "You must repeat your password.")]
        [Compare(nameof(UserPassword), ErrorMessage = "The passwords doesn't match.")]
        [DataType(DataType.Password)]
        public String UserConfirmPassword { get; set; }
    }
}