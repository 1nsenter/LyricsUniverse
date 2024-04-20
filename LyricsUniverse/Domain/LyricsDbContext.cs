using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LyricsUniverse.Domain.Entities;

namespace LyricsUniverse.Domain
{
    public class LyricsDbContext : IdentityDbContext<IdentityUser>
    {
        DbSet<Song> Songs { get; set; }
        DbSet<Artist> Artist { get; set; }
        DbSet<Album> Albums { get; set; }
        DbSet<FavoriteSong> FavoriteSongs { get; set; }

        public LyricsDbContext(DbContextOptions<LyricsDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FavoriteSong>()
                .HasOne(fs => fs.User)
                .WithMany()
                .HasForeignKey(fs => fs.UserId);

            //modelBuilder.Entity<FavoriteSong>()
            //    .HasOne(fs => fs.Song)
            //    .WithMany()
            //    .HasForeignKey(fs => fs.SongId);
        }
    }
}