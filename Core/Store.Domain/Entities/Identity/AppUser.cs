
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Store.Domain.Entities.Identity
{
    // Inherit from IdentityUser with string as the key type
    // Key => string (GUID)
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; } // Additional property for user's display name
        public Address Address { get; set; } // Navigation property for user's address
    }
}
