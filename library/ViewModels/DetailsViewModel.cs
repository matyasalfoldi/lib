using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using library.Models;

namespace library.ViewModels
{
    public class DetailsViewModel
    {
        public Book Book { get; set; }

        public IEnumerable<Volume> Volumes { get; set; }
        
    }
}
