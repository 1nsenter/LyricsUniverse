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
            var songs = _context.Songs.Include(s => s.Artist).ToList();

            var model = new SongsViewModel
            {
                Songs = songs,
                SelectedSong = songs.FirstOrDefault()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult SelectItem(int selectedItemId)
        {
            var selectedSong = _context.Songs.Find(selectedItemId);
            return View("Index", new SongsViewModel
            {
                Songs = _context.Songs.Include(s => s.Artist).ToList(),
                SelectedSong = selectedSong
            });
        }
    }
}
