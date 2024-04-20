using LyricsUniverse.Domain;
using Microsoft.EntityFrameworkCore;
using LyricsUniverse.Service;

namespace LyricsUniverse
{
    class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Configuration.Bind("ProjectConfig", new ProjectConfig());
            builder.Services.AddDbContext<LyricsDbContext>(options => options.UseSqlServer(ProjectConfig.ConnectionString));

            var app = builder.Build();

            app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}