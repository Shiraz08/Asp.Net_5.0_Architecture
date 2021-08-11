using CAS_UI_Project.Areas.Identity.Data;
using CAS_UI_Project.Data;
using CAS_UI_Project.Models;
using CAS_UI_Project.Models.Claims;
using CAS_UI_Project.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CAS_UI_Project.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _rolemanger;
        private IHttpContextAccessor _httpContextAccessor;
        private IdentitysContext _context;

        public AccountController(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> rolemanager,
            IHttpContextAccessor httpContextAccessor,
            IdentitysContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _rolemanger = rolemanager;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        #region Roles
        public IActionResult RoleList()
        {
            var roles = _rolemanger.Roles.ToList();
            List<UserRolesViewModel> vm = new List<UserRolesViewModel>();
            foreach (var item in roles)
            {
                vm.Add(new UserRolesViewModel() { Id = item.Id, RoleName = item.Name });
            }
            return View(vm);
        }
        [HttpGet]
        public IActionResult CreateRole(string id)
        {
            var rol = _rolemanger.Roles.Where(x => x.Id == id);
            UserRolesViewModel vm = new UserRolesViewModel();
            if (rol.Count() > 0)
            {
                vm.Id = rol.FirstOrDefault().Id;
                vm.RoleName = rol.FirstOrDefault().Name;
            }
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(UserRolesViewModel model)
        {
            if (model.Id != "" && model.Id != null)
            {
                IdentityRole role = await _rolemanger.FindByIdAsync(model.Id);
                role.Name = model.RoleName;
                await _rolemanger.UpdateAsync(role);
            }
            else
            {
                await _rolemanger.CreateAsync(new IdentityRole(model.RoleName));
            }
            return RedirectToAction("RoleList");
        }
        public async Task<IActionResult> DeleteRole(string id)
        {
            if (id != "" || id != null)
            {
                IdentityRole role = await _rolemanger.FindByIdAsync(id);
                await _rolemanger.DeleteAsync(role);
            }

            return RedirectToAction("RoleList");
        }
        #endregion

        #region Claims
        [HttpGet]
        public async Task<IActionResult> GetAllClaims(string roleid)
        {
            IdentityRole rol = await _rolemanger.FindByIdAsync(roleid);
            var existingclaims = await _rolemanger.GetClaimsAsync(rol);
            var model = new ClaimsViewModel()
            {
                RoleId = roleid
            };
            foreach (Claim claim in ClaimStore.AllClaims)
            {
                ClaimsListViewModel claimlist = new ClaimsListViewModel()
                {
                    ClaimsType = claim.Type
                };
                if (existingclaims.Any(c => c.Type == claim.Type))
                {
                    claimlist.IsSelected = true;
                }
                model.ClaimsList.Add(claimlist);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAllClaims(ClaimsViewModel model)
        {
            var Role = await _rolemanger.FindByIdAsync(model.RoleId);
            if (Role.Id != "" && Role.Id != null)
            {
                IList<Claim> cl = await _rolemanger.GetClaimsAsync(Role);
                for (var i = 0; i < cl.Count; i++)
                {
                    var result = await _rolemanger.RemoveClaimAsync(Role, cl[i]);
                }

                foreach (var item in model.ClaimsList.Where(c => c.IsSelected == true))
                {
                    Claim c = new Claim(item.ClaimsType, item.ClaimsType);
                    var r = await _rolemanger.AddClaimAsync(Role, c);
                }
            }
            return RedirectToAction("GetAllClaims", new { roleid = model.RoleId });
        }

        #endregion
        [AllowAnonymous]
        [Route("google-login")]
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }
        [AllowAnonymous]
        [Route("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var claims = result.Principal.Identities
                .FirstOrDefault().Claims.Select(claim => new
                {
                    claim.Issuer,
                    claim.OriginalIssuer,
                    claim.Type,
                    claim.Value
                });

            return Json(claims);
        }
    }
}
