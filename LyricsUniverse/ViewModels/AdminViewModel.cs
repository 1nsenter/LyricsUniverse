using LyricsUniverse.Models.Entities;

namespace LyricsUniverse.ViewModels
{
    public class AdminViewModel
    {
        public List<User>? Moderators { get; set; }
        public User? SelectedModerator { get; set; }
    }
}
