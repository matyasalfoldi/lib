using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace library.Models
{
    public class Rent
    {
        [Key]
        public int Id { get; set; }
        
        public int VolumeId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Boolean Active { get; set; }

        public Volume Volume { get; set; }
        public User User { get; set; }
        
        public Boolean IsConflicting(DateTime startDate, DateTime endDate)
        {
            return StartDate >= startDate && StartDate < endDate ||
                   EndDate >= startDate && EndDate < endDate ||
                   StartDate < startDate && EndDate > endDate ||
                   StartDate > startDate && EndDate < endDate;
        }

        public Boolean IsActiveOrFutureRent()
        {
            return Active || 
                   StartDate >= DateTime.Today.Date;
        }
    }
}
