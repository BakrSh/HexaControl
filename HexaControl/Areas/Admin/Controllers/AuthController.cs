using HexaControl.Areas.Admin.Model.Dto;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HexaControl.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class AuthController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(LoginDto obj)
        {


            var user = new IdentityUser { Email = obj.Email, UserName = obj.Email };
            var res = await _userManager.CreateAsync(user, obj.Password);
            await _signInManager.SignInAsync(user, isPersistent: false);

            if (!res.Succeeded)
            {

                return View();
            }
            return RedirectToAction(nameof(Index), "Home", "Admin");
        }



    }
}
