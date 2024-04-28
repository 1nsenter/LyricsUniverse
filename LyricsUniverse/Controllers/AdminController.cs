using LyricsUniverse.Models;
using LyricsUniverse.Models.Entities;
using LyricsUniverse.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LyricsUniverse.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly LyricsDbContext _context;

        public AdminController(LyricsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddViewModel model)
        {
            if (ModelState.IsValid)
            {
                Song newSong = new Song();

                var artist = _context.Artists.FirstOrDefault(e => e.Title.ToUpper() == model.Artist.ToUpper());
                if (artist != null)
                {
                    var song = _context.Songs.FirstOrDefault(e => e.Title.ToUpper() == model.Title.ToUpper());

                    if (song != null)
                    {
                        ModelState.AddModelError(string.Empty, "Такая песня уже есть в базе данных. " +
                            "Вы можете отредактировать ее в любой момент.");
                        return View(model);
                    }

                    newSong.Artist = artist;
                }
                else
                {
                    Artist newArtist = new Artist
                    {
                        Title = model.Artist,
                    };
                    _context.Artists.Add(newArtist);
                    _context.SaveChanges();

                    newSong.Artist = newArtist;
                }

                newSong.Title = model.Title;
                newSong.Text = model.Text;

                _context.Songs.Add(newSong);
                _context.SaveChanges();
            }
            return View();
        }
    }
}