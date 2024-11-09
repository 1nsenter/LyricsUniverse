using LyricsUniverse.Models;
using LyricsUniverse.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LyricsUniverse.Controllers
{
    public class HomeController : Controller
    {
        private readonly LyricsDbContext _context;

        public HomeController(LyricsDbContext context)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
            int displaySongsCount;
            var songs = _context.GetLastApprovedSongs();

            if (songs.Count >= 10)
                displaySongsCount = 10;
            else
                displaySongsCount = songs.Count;

            var model = new SongsViewModel
            {
                DisplaySongsCount = displaySongsCount,
                Songs = songs,
                SelectedSong = null
            };

            return View(model);
        }
    }
}
