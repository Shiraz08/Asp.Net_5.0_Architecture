using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAS_UI_Project.Models.ViewModels
{
    public class RoleClaimViewModel
    {
        public string Id { get; set; }
        public string RoleId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}
