using Microsoft.AspNetCore.Mvc;
using LyricsUniverse.ViewModels;
using LyricsUniverse.Models;

namespace LyricsUniverse.Controllers
{
    public class ModeratorController : Controller
    {
        private readonly LyricsDbContext _context;

        public ModeratorController(LyricsDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(new SongsViewModel
            {
                Songs = _context.GetUnmoderatedSongs()
            });
        }
    }
}
