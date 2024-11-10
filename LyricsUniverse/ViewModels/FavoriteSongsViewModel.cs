using LyricsUniverse.Models;
using LyricsUniverse.Models.Entities;

namespace LyricsUniverse.ViewModels
{
    public class FavoriteSongsViewModel
    {
        public List<AppRole> UserRoles { get; set; }
        public List<FavoriteSong> FavoriteSongs { get; set; }
        public Song? SelectedSong { get; set; }
    }
}
