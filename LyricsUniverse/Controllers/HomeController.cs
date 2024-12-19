using LyricsUniverse.Models;
using LyricsUniverse.Models.Entities;
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

        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return View(new SearchViewModel
                {
                    Songs = new List<Song>(),
                    Query = query
                });
            }

            var keywords = query.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            keywords = keywords.Select(k => k.ToLower()).ToArray();

            var songs = await _context.Songs
                .Include(s => s.Artist)
                .Where(s => s.isModerated == true &&
                    keywords.All(k => s.Title.ToLower().Contains(k) || s.Artist.Title.ToLower().Contains(k)))
                .OrderByDescending(s => keywords.Count(k => s.Artist.Title.ToLower().Contains(k)))
                .ThenByDescending(s => keywords.Count(k => s.Title.ToLower().Contains(k)))
                .ToListAsync();

            return View(new SearchViewModel
            {
                Songs = songs,
                Query = query
            });
        }
    }
}