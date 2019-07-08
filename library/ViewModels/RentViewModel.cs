using System;
using System.ComponentModel.DataAnnotations;
using library.Models;

namespace library.ViewModels
{
    public class RentViewModel : UserViewModel
    {
        public Volume Volume { get; set; }

        public Book Book { get; set; }
        
        [Required(ErrorMessage = "You must give a start date.")]
        [DataType(DataType.Date)]
        public DateTime RentStartDate { get; set; }
        
        [Required(ErrorMessage = "You must give an end date.")]
        [DataType(DataType.Date)]
        public DateTime RentEndDate { get; set; }
    }
}