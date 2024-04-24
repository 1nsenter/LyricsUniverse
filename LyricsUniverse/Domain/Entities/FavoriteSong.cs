using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LyricsUniverse.Domain.Entities
{
    public class FavoriteSong
    {
        [Key]
        [Required]
        public int FavoriteSongId { get; set; }

        [Required]
        public string UserId { get; set; }
        public User User { get; set; }

        [Required]
        public int SongId { get; set; }
        public Song Song { get; set; }
    }
}
