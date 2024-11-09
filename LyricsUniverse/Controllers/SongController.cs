using LyricsUniverse.Models;
using LyricsUniverse.Models.Entities;
using LyricsUniverse.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LyricsUniverse.Controllers
{
    public class SongController : Controller
    {
        private readonly LyricsDbContext _context;
        UserManager<User> _userManager;

        public SongController(LyricsDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Id(int id)
        {
            List<AppRole> userRoles = new();

            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                var roles = await _userManager.GetRolesAsync(user);

                foreach (var role in roles)
                {
                    userRoles.Add((AppRole)Enum.Parse(typeof(AppRole), role));
                }
            }

            var selectedSong = _context.Songs.Find(id);
            
            return View(new SongsViewModel
            {
                DisplaySongsCount = 1,
                CurrentUserRoles = userRoles,
                Songs = _context.Songs.Include(s => s.Artist).ToList(),
                SelectedSong = selectedSong
            });
        }
    }
}
