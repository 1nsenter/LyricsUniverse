﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LyricsUniverse.Models;
using LyricsUniverse.Models.Entities;
using Microsoft.AspNetCore.Identity;
using LyricsUniverse.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace LyricsUniverse.Controllers
{
    [Authorize(Roles = "AuthorizedUser")]
    public class FavoritesController : Controller
    {
        private readonly LyricsDbContext _context;
        private readonly UserManager<User> _userManager;

        public FavoritesController (LyricsDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int songId = -1)
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

            var model = new FavoriteSongsViewModel();
            var userId = _userManager.GetUserId(User);

            model.UserRoles = userRoles;
            model.SelectedSong = _context.Songs.Find(songId);
            model.FavoriteSongs = _context.FavoriteSongs
                .Include(fs => fs.Song)
                .ThenInclude(s => s.Artist)
                .Where(fs => fs.UserId == userId)
                .OrderBy(fs => fs.Song.Title)
                .ToList();

            return View(model);
        }

        public IActionResult Add(int songId)
        {
            var userId = _userManager.GetUserId(User);

            if (userId != null)
            {
                var favoriteSong = _context.FavoriteSongs.FirstOrDefault(fs => fs.UserId == userId &&
                    fs.SongId == songId);

                if (favoriteSong is null)
                {
                    _context.FavoriteSongs.Add(new FavoriteSong
                    {
                        UserId = userId,
                        SongId = songId
                    });
                    _context.SaveChanges();
                   
                }
            }
            return Redirect($"/Song?songId={songId}");
        }

        public IActionResult Delete(int songId)
        {
            var userId = _userManager.GetUserId(User);

            if (userId != null)
            {
                var favoriteSong = _context.FavoriteSongs.FirstOrDefault(fs => fs.UserId == userId &&
                    fs.SongId == songId);

                if (favoriteSong is not null)
                {
                    _context.FavoriteSongs.Remove(favoriteSong);
                    _context.SaveChanges();
                }
            }
            return Redirect($"/Favorites/");
        }
    }
}
