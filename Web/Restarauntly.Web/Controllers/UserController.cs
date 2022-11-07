﻿namespace Restarauntly.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Restarauntly.Data.Models;
    using Restarauntly.Web.ViewModels.User;

    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        private readonly SignInManager<ApplicationUser> signInManager;

        public UserController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (this.User?.Identity?.IsAuthenticated ?? false)
            {
                return this.RedirectToAction("Index", "Home");
            }

            var model = new RegisterViewModel();

            return this.View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = new ApplicationUser()
            {
                Email = model.Email,
                UserName = model.UserName,
                PhoneNumber = model.PhoneNumber,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };

            var result = await this.userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return this.RedirectToAction("Index", "Home");
            }

            foreach (var item in result.Errors)
            {
                this.ModelState.AddModelError("", item.Description);
            }

            return this.View(model);
        }

        //[HttpGet]
        //[AllowAnonymous]
        //public IActionResult Login()
        //{
        //    if (User?.Identity?.IsAuthenticated ?? false)
        //    {
        //        return RedirectToAction("All", "Books");
        //    }

        //    var model = new LoginViewModel();

        //    return View(model);
        //}

        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> Login(LoginViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    var user = await userManager.FindByNameAsync(model.UserName);

        //    if (user != null)
        //    {
        //        var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);

        //        if (result.Succeeded)
        //        {
        //            return RedirectToAction("All", "Books");
        //        }
        //    }

        //    ModelState.AddModelError("", "Invalid login");

        //    return View(model);
        //}

        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();

            return this.RedirectToAction("Index", "Home");
        }
    }
}