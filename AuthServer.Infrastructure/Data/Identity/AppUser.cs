using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace AuthServer.Infrastructure.Data.Identity
{
    public class AppUser : IdentityUser
    {
        // Add additional profile data for application users by adding properties to this class
        public string Name { get; set; }
        public string Surname { get; set; } 
        public string Domain {get; set; }
        public string DomainUsername { get; set; }
    }
}
