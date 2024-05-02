using Microsoft.EntityFrameworkCore;
using Practico.Models;

namespace Practico
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSession(so =>
             {
                 so.IdleTimeout = TimeSpan.FromSeconds(3600);
             });
            var conn = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<MyContext>(options => options.UseSqlServer(conn));

            //builder.Services.AddDbContext<MyContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();

            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}