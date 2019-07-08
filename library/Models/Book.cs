using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace library.Models
{
    public class Book
    {
        public Book()
        {
            Volumes = new HashSet<Volume>();
        }

        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime ReleaseDate { get; set; }

        [StringLength(13)]
        public string ISBN { get; set; }
        public byte[] CoverImage { get; set; }

        public ICollection<Volume> Volumes { get; set; }
    }
}
