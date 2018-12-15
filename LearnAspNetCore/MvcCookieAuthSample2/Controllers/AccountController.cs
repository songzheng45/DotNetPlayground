using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcCookieAuthSample2.Models;
using Microsoft.AspNetCore.Identity;
using MvcCookieAuthSample2.Data;

namespace MvcCookieAuthSample2.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signManager)
        {
            _userManager = userManager;
            _signManager = signManager;
        }

        [NonAction]
        public IActionResult ReturnToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: /<controller>/
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["returnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = new ApplicationUser()
            {
                Email = model.Email,
                UserName = model.Email,
                NormalizedEmail = model.Email,
                NormalizedUserName = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signManager.SignInAsync(user, true);

                return ReturnToLocal(returnUrl);
            }

            ViewData["Message"] = string.Join(",", result.Errors.Select(e => e.Description).ToList());

            return View();
        }

        public IActionResult Login(string returnUrl = null)
        {
            ViewData["returnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _userManager.FindByEmailAsync(model.Email); ;
            if (user == null)
            {
                ViewData["Message"] = "用户不存在";
                return View();
            }

            await _signManager.SignInAsync(user, true);

            return ReturnToLocal(returnUrl);
        }


        public IActionResult Logout()
        {
            _signManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
