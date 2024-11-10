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
        public async Task<IActionResult> Index(int songId)
        {
            List<AppRole> userRoles = new();
            bool isFavorited = false;

            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                var roles = await _userManager.GetRolesAsync(user);
                isFavorited = _context.IsFavorited(user, songId);

                foreach (var role in roles)
                {
                    userRoles.Add((AppRole)Enum.Parse(typeof(AppRole), role));
                }
            }

            var selectedSong = _context.Songs.Find(songId);
            
            return View(new SongsViewModel
            {
                IsFavorited = isFavorited,
                DisplaySongsCount = 1,
                UserRoles = userRoles,
                Songs = _context.Songs.Include(s => s.Artist).ToList(),
                SelectedSong = selectedSong
            });
        }
    }
}
