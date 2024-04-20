using System.ComponentModel.DataAnnotations;

namespace LyricsUniverse.Domain.Entities
{
    public class Artist
    {
        [Key]
        [Required]
        public int ArtistId { get; set; }
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DateFormed { get; set; }

        public List<Song> Songs { get; set; }
    }
}
