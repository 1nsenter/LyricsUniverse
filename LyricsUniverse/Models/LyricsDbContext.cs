using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LyricsUniverse.Models.Entities;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            string adminEmail = "shared_admin@mail.ru";
            Guid adminId = Guid.NewGuid();
            Guid adminRoleId = Guid.NewGuid();
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
                UserId = adminId.ToString()
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = authorizedUserRoleId.ToString(),
                UserId = adminId.ToString()
            });
        }
    }
}