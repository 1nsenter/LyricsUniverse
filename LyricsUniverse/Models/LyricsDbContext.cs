﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LyricsUniverse.Models.Entities;
using LyricsUniverse.ViewModels;

namespace LyricsUniverse.Models
{
    public class LyricsDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Song> Songs { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<FavoriteSong> FavoriteSongs { get; set; }

        public LyricsDbContext(DbContextOptions<LyricsDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public Artist GetArtistOrCreateNew(string title)
        {
            Artist artist = GetArtistByTitle(title);

            if (artist is not null)
                return artist;
            else
                return CreateNewArtist(title);
        }

        public Artist? GetArtistByTitle(string title)
        {
            return Artists.FirstOrDefault(e => e.Title.ToUpper() == title.ToUpper());
        }

        public Artist CreateNewArtist(string title)
        {
            Artist artist = GetArtistByTitle(title);

            if (artist is not null)
                throw new Exception($"An artist with this name ({artist.Title}) has already been created.");
            else
            {
                artist = new Artist
                {
                    Title = title
                };

                Artists.Add(artist);
                SaveChanges();

                return GetArtistByTitle(title);
            }
        }

        public Song GetSongByArtistAndTitle(Artist artist, string title)
        {
            Song? song = Songs.FirstOrDefault(s =>
                        s.Artist.Id == artist.Id &&
                        s.Title.ToUpper() == title.ToUpper());

            if (song != null)
                return song;
            else
                throw new Exception($"A song '{title}' by {artist.Title} was not found.");
        }

        public void AddSong(Song song)
        {
            Songs.Add(song);
            SaveChanges();
        }

        public List<Song>? GetSongs(bool isModerated)
        {
            return Songs.Where(s => s.isModerated == isModerated).Include(a => a.Artist).ToList();
        }

        public Song? CreateSong(CreateSongViewModel model)
        {
            Song createdSong = new Song();

            var artist = GetArtistByTitle(model.Artist);
            if (artist != null)
            {
                var song = GetSongByArtistAndTitle(artist, model.Title);

                if (song != null)
                    return null;

                createdSong.Artist = artist;
            }
            else
                createdSong.Artist = CreateNewArtist(model.Artist);

            createdSong.Title = model.Title;
            createdSong.Text = model.Text;
            createdSong.Translate = model.Translate;
            createdSong.isModerated = false;
            createdSong.CreatedAt = DateTime.Now;

            return createdSong;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            string adminEmail = "shared_admin@mail.ru";
            Guid adminId = Guid.NewGuid();
            Guid adminRoleId = Guid.NewGuid();
            string moderatorEmail = "shared_moderator@mail.ru";
            Guid moderatorId = Guid.NewGuid();
            Guid moderatorRoleId = Guid.NewGuid();
            Guid authorizedUserRoleId = Guid.NewGuid();

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = adminId.ToString(),
                UserName = adminEmail,
                NormalizedUserName = adminEmail.ToUpper(),
                Email = adminEmail,
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<User>().HashPassword(null, "shared_admin_0000"),
                SecurityStamp = string.Empty
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = moderatorId.ToString(),
                UserName = moderatorEmail,
                NormalizedUserName = moderatorEmail.ToUpper(),
                Email = moderatorEmail,
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<User>().HashPassword(null, "shared_moderator_0000"),
                SecurityStamp = string.Empty
            });

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = adminRoleId.ToString(),
                Name = "admin",
                NormalizedName = "ADMIN"
            });

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = moderatorRoleId.ToString(),
                Name = "moderator",
                NormalizedName = "MODERATOR"
            });

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = authorizedUserRoleId.ToString(),
                Name = "authorizedUser",
                NormalizedName = "AUTHORIZEDUSER"
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = adminRoleId.ToString(),
                UserId = adminId.ToString()
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = moderatorRoleId.ToString(),
                UserId = moderatorId.ToString()
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = authorizedUserRoleId.ToString(),
                UserId = adminId.ToString()
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = authorizedUserRoleId.ToString(),
                UserId = moderatorId.ToString()
            });
        }
    }
}