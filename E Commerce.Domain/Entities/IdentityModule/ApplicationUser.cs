using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entities.IdentityModule
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; } = default!;
      
        // relation [ 1 -> Address (Mandatory) , 1 -> ApplicationUser (Optinal) ]
        public Address? Address { get; set; }
    }
}
