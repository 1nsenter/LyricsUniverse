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

        //[Required]
        //public int ArtistId { get; set; }
        //public Artist Artist { get; set; }

        public List<Song> Songs { get; set; }
    }
}