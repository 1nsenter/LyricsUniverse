using LyricsUniverse.Models;
using LyricsUniverse.Models.Entities;
using LyricsUniverse.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LyricsUniverse.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly LyricsDbContext _context;
        private readonly UserManager<User> _userManager;
        public AdminController(LyricsDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var adminId = _userManager.GetUserId(User);
            var moderators = await _userManager.GetUsersInRoleAsync(AppRole.Moderator.ToString());

            foreach (var moderator in moderators.ToList())
            {
                if (moderator.Id == adminId)
                    moderators.Remove(moderator);
            }
                
            var model = new AdminViewModel
            {
                Moderators = moderators.ToList(),
                SelectedModerator = null
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddModerator()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddModerator(string email)
        {
            var user = await _userManager.FindByEmailAsync(email.ToUpper());

            if (user == null)
            {
                ModelState.AddModelError("UserIsNull", "Пользователь с такой почтой не найден в системе");
                return View(email);
            }

            await _userManager.AddToRoleAsync(user, AppRole.Moderator.ToString());
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveModerator(string moderatorId)
        {
            var user = await _userManager.FindByIdAsync(moderatorId);

            if (user == null)
                ModelState.AddModelError("UserIsNull", "Модератор не найден в системе");
            else
                await _userManager.RemoveFromRoleAsync(user, AppRole.Moderator.ToString());

            return RedirectToAction("Index");
        }
    }
}