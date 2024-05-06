using LyricsUniverse.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace LyricsUniverse.ViewModels
{
    public class AccountViewModel
    {
        public IdentityUser CurrentUser { get; set; }
    }
} 