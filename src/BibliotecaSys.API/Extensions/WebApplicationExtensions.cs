
using BibliotecaSys.API.Controllers;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaSys.API.Extensions;

public static class WebApplicationExtensions
{
    /// <summary>
    ///     Extension method to map application-specific endpoints for the WebApplication.
    /// </summary>
    /// <param name="app">The WebApplication instance for which to map the application endpoints.</param>
    public static void MapApplicationEndpoints(this WebApplication app)
    {
        // TODO: Add more endpoint mappings here as needed
        app.MapLibrosEndpoints();
    }

    /// <summary>
    ///     Applies any pending migrations for the context to the database. Will create the database if it does not already exist.
    /// </summary>
    /// <typeparam name="TContext">The type of the DbContext to apply the migrations for.</typeparam>
    /// <param name="app">The WebApplication instance to extend.</param>
    public static void ApplyMigrations<TContext>(this WebApplication app) where TContext : DbContext
    {
        using var scope = app.Services.CreateScope();
        {
            var context = scope.ServiceProvider.GetRequiredService<TContext>();
            context.Database.EnsureCreated();
        }
    }
}