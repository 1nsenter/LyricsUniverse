using Microsoft.AspNetCore.Identity;

namespace LyricsUniverse.Domain.Entities
{
    public class User : IdentityUser
    {
        public int Year { get; set; }
        //public List<FavoriteSong> FavoriteSongs { get; set; }
    }
}
