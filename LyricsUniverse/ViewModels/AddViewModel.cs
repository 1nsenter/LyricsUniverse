using System.ComponentModel.DataAnnotations;

namespace LyricsUniverse.ViewModels
{
    public class AddViewModel
    {
        [Required]
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Исполнитель")]
        public string Artist { get; set; }

        [Required]
        [Display(Name = "Текст")]
        public string Text { get; set; }
    }
}
