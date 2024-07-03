using Microsoft.EntityFrameworkCore;
using MoviesWebApp.Data;
using MoviesWebApp.Services;

namespace MoviesWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<MoviesDbContext>(options =>
            options.UseSqlite("Data Source=app.db"));
            builder.Services.AddScoped<IMovieService, MovieService>();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error/Index");
                app.UseHsts();
            }

            using (var scope = app.Services.CreateScope())
            {
                var ctx = scope.ServiceProvider.GetRequiredService<MoviesDbContext>();
                ctx.Database.EnsureDeleted();
                ctx.Database.EnsureCreated();
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
