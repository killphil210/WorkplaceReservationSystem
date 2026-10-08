using WorkplaceReservationSystem.Application.Services;
using WorkplaceReservationSystem.Application.State;
using WorkplaceReservationSystem.ViewModels;
using WorkplaceReservationSystem.Views.Components;

namespace WorkplaceReservationSystem.Views
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
                
            builder.Services.AddScoped<MainViewModel>();
            builder.Services.AddSingleton<WorkspaceService>();
            builder.Services.AddSingleton<WorkspaceState>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
