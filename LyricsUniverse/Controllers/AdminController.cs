using LyricsUniverse.Models;
using LyricsUniverse.Models.Entities;
using LyricsUniverse.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    }
}