using System.ComponentModel.DataAnnotations;

namespace LyricsUniverse.Domain.Entities
{
    public class Song
    {
        [Key]
        [Required]
        public int SongId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }

        [Required]
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }

        [Required]
        public int AlbumId { get; set; }
        public Album Album { get; set; }
    }
}
