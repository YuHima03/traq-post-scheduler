using PostScheduler.WebApp.Components;

namespace PostScheduler.WebApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configure services.
        {
            var services = builder.Services;

            services.AddRazorComponents()
            .AddInteractiveWebAssemblyComponents();

            // Logging
            services.AddLogging(lb =>
            {
                lb.AddSimpleConsole(options =>
                {
                    options.IncludeScopes = true;
                    options.ColorBehavior = Microsoft.Extensions.Logging.Console.LoggerColorBehavior.Enabled;
                });

                if (builder.Environment.IsProduction())
                {
                    lb.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
                }
                else if (builder.Environment.IsDevelopment())
                {
                    lb.AddFilter("Dakoq", LogLevel.Debug);
                }
            });
        }

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

        app.Run();
    }
}
