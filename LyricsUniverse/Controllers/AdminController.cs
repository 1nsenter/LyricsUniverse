using LyricsUniverse.Models;
using LyricsUniverse.Models.Entities;
using LyricsUniverse.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public IActionResult Index()
        {
            var songs = _context.Songs
                .Include(s => s.Artist)
                .OrderBy(s => s.Title)
                .ToList();

            var model = new SongsViewModel
            {
                Songs = songs,
                SelectedSong = null
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int songId)
        {
            var song = _context.Songs.FirstOrDefault(s => s.SongId == songId);

            var model = new EditViewModel
            {
                Title = song.Title,
                Text = song.Text,
                Id = songId
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditViewModel model, int songId)
        {
            _context.Songs.Where(s => s.SongId == songId)
            .ExecuteUpdate(b =>
                b.SetProperty(s => s.Title, model.Title)
            );

            _context.Songs.Where(s => s.SongId == songId)
            .ExecuteUpdate(b =>
                b.SetProperty(s => s.Text, model.Text)
            );

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int songId)
        {
            var song = _context.Songs.FirstOrDefault(s => s.SongId == songId);
            _context.Songs.Remove(song);
            _context.SaveChanges();
            return RedirectToAction("Index");
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
            return RedirectToAction("Add");
        }
    }
}