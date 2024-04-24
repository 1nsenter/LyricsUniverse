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

            string adminLogin = "shared_admin";
            Guid adminRoleId = Guid.NewGuid();
            Guid authorizedUserRoleId = Guid.NewGuid();

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = adminLogin,
                UserName = adminLogin,
                NormalizedUserName = adminLogin.ToUpper(),
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<User>().HashPassword(null, "shared_admin_0000"),
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
                Id = authorizedUserRoleId.ToString(),
                Name = "authorizedUser",
                NormalizedName = "AUTHORIZEDUSER"
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = adminRoleId.ToString(),
                UserId = adminLogin
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = authorizedUserRoleId.ToString(),
                UserId = adminLogin
            });
        }
    }
}