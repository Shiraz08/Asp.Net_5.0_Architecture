using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAS_UI_Project.Models.UserRole
{
    public class RoleAsignToUser
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
