﻿using LyricsUniverse.Models;
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
            var songs = _context.GetSongs(true);

            var model = new SongsViewModel
            {
                Songs = songs,
                SelectedSong = null
            };

            return View(model);
        }
    }
}
