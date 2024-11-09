using LyricsUniverse.Models;
using LyricsUniverse.Models.Entities;

namespace LyricsUniverse.ViewModels
{
    public class SongsViewModel
    {
        public List<AppRole> CurrentUserRoles { get; set; }
        public int DisplaySongsCount { get; set; }
        public List<Song>? Songs { get; set; }
        public Song? SelectedSong { get; set; }
    }
}
