using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PostScheduler.WebApp.Components;
using System.Collections.Concurrent;

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

            // Repository
            services.AddKeyedSingleton<ConcurrentDictionary<Guid, Domain.Model.TraqAccessToken>>(Infrastructure.Repository.Repository.KeyAccessTokens);
            services.AddDbContextFactory<Infrastructure.Repository.Repository>(options =>
            {
                options.UseMySQL("");
                options.EnableSensitiveDataLogging(builder.Environment.IsDevelopment());
            });
            services.AddSingleton<Domain.Repository.IRepositoryFactory, Infrastructure.Repository.RepositoryFactory>(sp => new(sp.GetRequiredService<IDbContextFactory<Infrastructure.Repository.Repository>>()));

            // Session
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.IdleTimeout = TimeSpan.FromMinutes(5);
            });

            // Cookie Authentication
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromDays(7);
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
