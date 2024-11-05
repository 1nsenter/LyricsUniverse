using LyricsUniverse.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace LyricsUniverse.ViewModels
{
    public class ReviewViewModel
    {
        [Required]
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Текст")]
        public string Text { get; set; }

        [Required]
        [Display(Name = "Перевод")]
        public string? Translate { get; set; }

        [Required]
        [Display(Name = "Исполнитель")]
        public string ArtistTitle { get; set; }

        public int Id { get; set; }
    }
}
