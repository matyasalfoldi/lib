using library.Models;
using System.Collections.Generic;

namespace library.ViewModels
{
    public class DetailsViewModel
    {
        public Book Book { get; set; }

        public IEnumerable<Volume> Volumes { get; set; }

    }
}
