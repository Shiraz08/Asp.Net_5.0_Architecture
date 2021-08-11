using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CAS_UI_Project.Models.Claims
{
    public static class ClaimStore
    {
        public static List<Claim> AllClaims = new List<Claim>()
        {
            new Claim("canadduser","canadduser"),
            new Claim("canupdateuser","canupdateuser"),
            new Claim("canviewuser","canviewuser"),
            new Claim("superadminpermission","superadminpermission")
        };
    }
}
