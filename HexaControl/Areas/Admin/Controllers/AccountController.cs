using HexaControl.Areas.Admin.Model.Dto;
using HexaControl.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HexaControl.Areas.Admin.Controllers
{

    [Area("Admin")]

    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }


        public async Task<IActionResult> Login()
        {
            var email = _userManager.FindByEmailAsync("admin@admin.com");
            if (email.Result == null)
            {

                var user = new IdentityUser { Email = "admin@admin.com", UserName = "admin@admin.com" };
                await _userManager.CreateAsync(user, "P@ssw0rd");



            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto obj)
        {
            try
            {

                Microsoft.AspNetCore.Identity.SignInResult res = await _signInManager.
                                  PasswordSignInAsync(obj.Email, obj.Password, isPersistent: false, false);
                if (res.Succeeded)
                {
                    var claims = new Claim[]
                    {
                        new Claim(ClaimTypes.Name,obj.Email),
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, /*Explicit*/CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction(nameof(Index), "Home", "Admin");
                }

                return View();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View(obj);
            }
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }


    }
}
