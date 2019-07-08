using System;
using System.ComponentModel.DataAnnotations;

namespace library.ViewModels
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "You must give a name!")]
        [StringLength(60, ErrorMessage = "The name can be maximum 60 characters!")]
        public String Name { get; set; }
        
        [Required(ErrorMessage = "You must give an address!")]
        public String UserAddress { get; set; }

        [Required(ErrorMessage = "You must give an email address!")]
        [EmailAddress(ErrorMessage = "The format of the email is invalid!")]
        [DataType(DataType.EmailAddress)]
        public String UserEmail { get; set; }

        [Required(ErrorMessage = "You must give a phone number!")]
        [Phone(ErrorMessage = "The format of the phone number is invalid!")]
        [DataType(DataType.PhoneNumber)]
        public String UserPhoneNumber { get; set; }
    }
}