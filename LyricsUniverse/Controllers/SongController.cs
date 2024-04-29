using LyricsUniverse.Models;
using LyricsUniverse.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LyricsUniverse.Controllers
{
    public class SongController : Controller
    {
        private readonly LyricsDbContext _context;

        public SongController(LyricsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Id(int id)
        {
            var selectedSong = _context.Songs.Find(id);
            return View(new SongsViewModel
            {
                Songs = _context.Songs.Include(s => s.Artist).ToList(),
                SelectedSong = selectedSong
            });
        }
    }
}
