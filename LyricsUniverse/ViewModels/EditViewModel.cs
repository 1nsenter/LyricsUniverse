using System.ComponentModel.DataAnnotations;

namespace LyricsUniverse.ViewModels
{
    public class EditViewModel
    {
        [Required]
        [Display(Name = "Исполнитель")]
        public string ArtistTitle { get; set; }

        [Required]
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Текст")]
        public string Text { get; set; }

        [Required]
        [Display(Name = "Текст")]
        public string Translate { get; set; }

        public int Id { get; set; }
    }
}