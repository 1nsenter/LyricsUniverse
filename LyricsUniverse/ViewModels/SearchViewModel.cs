using LyricsUniverse.Models.Entities;

namespace LyricsUniverse.ViewModels
{
    public class SearchViewModel
    {
        public List<Song> Songs { get; set; }
        public string Query { get; set; }
    }
}
