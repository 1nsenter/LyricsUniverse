using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LyricsUniverse.Models.Entities
{
    public class Artist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ArtistId { get; set; }
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DateFormed { get; set; }

        public List<Song> Songs { get; set; }
    }
}
