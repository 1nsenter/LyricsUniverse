﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using LyricsUniverse.ViewModels;
using LyricsUniverse.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using LyricsUniverse.Models;

namespace LyricsUniverse.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly LyricsDbContext _context;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,
            LyricsDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "AuthorizedUser")]
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var user = _context.Users.FirstOrDefault(x => x.Id == userId);

            return View(new AccountViewModel
            {
                CurrentUser = user
            });
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password,
                    model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToUrlOrDefault(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Попытка входа не удалась. Попробуйте позже.");
                    return View(model);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Name, Id = Guid.NewGuid().ToString() };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, AppRole.AuthorizedUser.ToString());
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToUrlOrDefault(returnUrl);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "AuthorizedUser")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToUrlOrDefault();
        }

        [HttpGet]
        [Authorize(Roles = "AuthorizedUser")]
        public IActionResult CreateSong()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "AuthorizedUser")]
        public IActionResult CreateSong(CreateSongViewModel model)
        {
            Song? newSong;

            if (ModelState.IsValid)
            {
                newSong = _context.CreateSong(model);

                if (newSong == null)
                {
                    ModelState.AddModelError(string.Empty, "Такая песня уже есть в базе данных.");
                    return View(model);
                }

                _context.AddSong(newSong);
            }
            return View(new CreateSongViewModel());
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult RedirectToUrlOrDefault(string url = null)
        {
            if (url is not null)
            {
                return Redirect(url);
            }
            else
                return RedirectToAction("Index", "Home");
        }
    }
}