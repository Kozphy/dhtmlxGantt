using DHX.Gantt.Middlewares;
using Microsoft.EntityFrameworkCore;
using DHX.Gantt.Models;

namespace DHX.Gantt
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<GanttContext>(options =>
                options.UseSqlServer(connectionString));

            // Add services to the container.
            builder.Services.AddRazorPages();

            // Add controller to Services.
            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            
            app.UseHttpsRedirection();

            app.UseDefaultFiles();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.MapControllers();

            app.InitializeDatabase();

            app.UseGanttErrorMiddleware();

            app.Run();
        }
    }
}