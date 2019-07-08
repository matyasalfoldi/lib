using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace library.Models
{
    public class Volume
    {
        [Key]
        public int Id { get; set; }
        public int BookId { get; set; }

        public Book Book { get; set; }
        public IEnumerable<Rent> Rents { get; set; }
    }
}
