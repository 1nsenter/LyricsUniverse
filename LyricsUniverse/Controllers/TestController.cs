using Microsoft.AspNetCore.Mvc;

namespace LyricsUniverse.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
