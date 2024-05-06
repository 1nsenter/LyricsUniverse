using System.ComponentModel.DataAnnotations;

namespace LyricsUniverse.ViewModels
{
    public class EditViewModel
    {
        [Required]
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Текст")]
        public string Text { get; set; }

        public int Id { get; set; }
    }
}
