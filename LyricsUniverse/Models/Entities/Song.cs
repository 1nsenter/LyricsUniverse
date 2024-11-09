using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LyricsUniverse.Models.Entities
{
    public class Song
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }

        public string? Translate { get; set; }

        [Required]
        public bool isModerated { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? ApprovedAt { get; set; }

        [Required]
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
    }
}
