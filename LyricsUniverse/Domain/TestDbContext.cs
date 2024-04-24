using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using LyricsUniverse.Domain.Entities;

namespace LyricsUniverse.Domain
{
    public class TestDbContext : IdentityDbContext<User>
    {
        public TestDbContext(DbContextOptions<TestDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
