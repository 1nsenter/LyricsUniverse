using LyricsUniverse.Models.Entities;

namespace LyricsUniverse.ViewModels
{
    public class FavoriteSongsViewModel
    {
        public List<FavoriteSong> FavoriteSongs { get; set; }
        public Song? SelectedSong { get; set; }
    }
}
