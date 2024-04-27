using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LyricsUniverse.Models.Entities
{
    public class Album
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int AlbumId { get; set; }
        [Required]
        public string Title { get; set; } = "Single";

        public List<Song> Songs { get; set; }
    }
}