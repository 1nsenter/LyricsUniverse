using Microsoft.AspNetCore.Mvc;
using LyricsUniverse.ViewModels;
using LyricsUniverse.Models;
using LyricsUniverse.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace LyricsUniverse.Controllers
{
    [Authorize(Roles = "Moderator")]
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
                Songs = _context.GetSongs(false).OrderBy(s => s.CreatedAt).ToList()
            });
        }

        public IActionResult Review(int songId)
        {
            Song song = _context.Songs.Include(s => s.Artist).FirstOrDefault(s => s.Id == songId);

            return View(new ReviewViewModel
            {
                Id = song.Id,
                Title = song.Title,
                Text = song.Text,
                Translate = song.Translate,
                ArtistTitle = song.Artist.Title
            });
        }

        public IActionResult Approve(ReviewViewModel model, int songId)
        {
            _context.Songs.Where(s => s.Id == songId)
            .ExecuteUpdate(b =>
                b.SetProperty(s => s.Title, model.Title)
            );

            _context.Songs.Where(s => s.Id == songId)
            .ExecuteUpdate(b =>
                b.SetProperty(s => s.Text, model.Text)
            );

            _context.Songs.Where(s => s.Id == songId)
            .ExecuteUpdate(b =>
                b.SetProperty(s => s.Translate, model.Translate)
            );

            _context.Songs.Where(s => s.Id == songId)
            .ExecuteUpdate(b =>
                b.SetProperty(s => s.isModerated, true)
            );

            _context.Songs.Where(s => s.Id == songId)
            .ExecuteUpdate(b =>
                b.SetProperty(s => s.ApprovedAt, DateTime.Now)
            );

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Decline(int songId)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Edit(int songId)
        {
            Song song = _context.Songs.Include(s => s.Artist).FirstOrDefault(s => s.Id == songId);

            return View(new ReviewViewModel
            {
                Id = song.Id,
                Title = song.Title,
                Text = song.Text,
                Translate = song.Translate,
                ArtistTitle = song.Artist.Title
            });
        }

        [HttpPost]
        public IActionResult SaveChanges(ReviewViewModel model, int songId)
        {
            _context.Songs.Where(s => s.Id == songId)
            .ExecuteUpdate(b =>
                b.SetProperty(s => s.Title, model.Title)
            );

            _context.Songs.Where(s => s.Id == songId)
            .ExecuteUpdate(b =>
                b.SetProperty(s => s.Text, model.Text)
            );

            _context.Songs.Where(s => s.Id == songId)
            .ExecuteUpdate(b =>
                b.SetProperty(s => s.Translate, model.Translate)
            );

            _context.SaveChanges();

            return Redirect($"/Song?songId={songId}");
        }

        public IActionResult Delete(int songId)
        {
            var song = _context.Songs.FirstOrDefault(s => s.Id == songId);
            _context.Songs.Remove(song);
            _context.SaveChanges();
            return Redirect("/Home/Index");
        }
    }
}