using LyricsUniverse.Models.Entities;

namespace LyricsUniverse.ViewModels
{
    public class SongsViewModel
    {
        public int DisplaySongsCount { get; set; }
        public List<Song>? Songs { get; set; }
        public Song? SelectedSong { get; set; }
    }
}
