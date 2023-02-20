using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Bookstore.Models
{
    public class User : IdentityUser
    {
        [NotMapped]
        public IList<string> RoleNames { get; set; }
    }
}
