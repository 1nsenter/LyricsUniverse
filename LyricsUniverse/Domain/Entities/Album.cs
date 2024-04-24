using System.ComponentModel.DataAnnotations;

namespace LyricsUniverse.Domain.Entities
{
    public class Album
    {
        [Key]
        [Required]
        public int AlbumId { get; set; }
        [Required]
        public string Title { get; set; } = "Single";

        public List<Song> Songs { get; set; }
    }
}