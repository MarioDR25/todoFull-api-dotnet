using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using TodoList.Application.Interfaces;
using TodoList.Application.Services;
using TodoList.Domain.Interfaces;
using TodoList.Infrastructure.Data;
using TodoList.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);


// ── CONTROLADORES ────────────────────────────────────────────────
builder.Services.AddControllers();

// ── BASE DE DATOS  ───────────────────────────────────────
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "No se encontró la cadena de conexión 'DefaultConnection' en appsettings.json");


builder.Services.AddDbContext<AppDbContext>( options => 
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString),
        mySqlOptions => mySqlOptions
            .MigrationsAssembly("TodoList.Infrastructure") ));


   /* Conexion con Sqlite
        options.UseSqlite(connectionString))
    */



// Repositorios
builder.Services.AddScoped<ITodoItemRepository, TodoItemRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Servicios de aplicación
builder.Services.AddScoped<ITodoItemService, TodoItemService>();
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, token) =>
    {
        document.Info = new()
        {
            Title = "TodoList API",
            Version = "v1",
            Description = "CRUD de tareas, con arquitectura de capas"
        };
        return Task.CompletedTask;
    });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Title = "TodoList API";
        options.Theme = ScalarTheme.Moon;
        options.DefaultHttpClient = new(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.Migrate(); // Esto aplica las migraciones automáticamente
}

app.MapGet("/", () => Results.Redirect("/scalar/v1"));

app.Run();
