using Microsoft.AspNetCore.Identity;

namespace LyricsUniverse.Models.Entities
{
    public class User : IdentityUser
    {
        public List<FavoriteSong> FavoriteSongs { get; set; }
    }
}
