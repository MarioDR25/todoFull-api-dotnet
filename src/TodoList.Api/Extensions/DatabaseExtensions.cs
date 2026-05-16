using Microsoft.EntityFrameworkCore;
using TodoList.Infrastructure.Data;

namespace TodoList.Api.Extensions;

/// <summary>
/// Extensión que registra y configura la base de datos.
/// Separado de los demás servicios porque la configuración
/// de BD puede cambiar independientemente (SQLite → MySQL → PostgreSQL).
/// </summary>
public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("No se encontró 'DefaultConnection'.");

        services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString),
                o => o.MigrationsAssembly("TodoList.Infrastructure")
            ));

        return services;
    }

    /// <summary>
    /// Ejecuta las migraciones pendientes al iniciar la aplicación.
    /// Se llama sobre WebApplication, no sobre IServiceCollection.
    /// </summary>
    public static WebApplication ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.Migrate();
        return app;
    }
}