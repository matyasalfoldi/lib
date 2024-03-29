﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace library.Models
{
    public class Librarian : IdentityUser<int>
    {
    }

    public class User : IdentityUser<int>
    {
        public User()
        {
            Rents = new HashSet<Rent>();
        }

        /* IdentityUser<T> contains:
		 * T Id
		 * string UserName
		 * string PasswordHash
		 * string Email
		 * string PhoneNumber
		 * string SecurityStamp
		 */

        public string Name { get; set; }

        public string Address { get; set; }

        public ICollection<Rent> Rents { get; set; }
    }
}
