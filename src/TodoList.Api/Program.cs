using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using TodoList.Application.Interfaces;
using TodoList.Application.Services;
using TodoList.Domain.Interfaces;
using TodoList.Infrastructure.Data;
using TodoList.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "No se encontró la cadena de conexión 'DefaultConnection' en appsettings.json");


builder.Services.AddDbContext<AppDbContext>( options => 
   options.UseMySql(
        connectionString,
        // ServerVersion.AutoDetect conecta a MySQL y detecta la versión
        // automáticamente. Pomelo necesita esto para generar SQL compatible.
        ServerVersion.AutoDetect(connectionString),
        mySqlOptions => mySqlOptions
            // Nombre del ensamblado donde están las migraciones
            .MigrationsAssembly("TodoList.Infrastructure") )

);

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
            Description = "CRUD profesional con Clean Architecture — Portafolio"
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
        // Tema visual — opciones: Default, Moon, Purple, Solarized, BluePlanet, etc.
        options.Theme = ScalarTheme.Moon;
        options.DefaultHttpClient = new(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
