using TodoList.Api.Configuration;
using TodoList.Application.Interfaces;
using TodoList.Application.Services;
using TodoList.Domain.Interfaces;
using TodoList.Infrastructure.Repositories;
using TodoList.Infrastructure.Services;

namespace TodoList.Api.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITodoItemRepository, TodoItemRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }

    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtOptions = new JwtOptions
        {
            Key = configuration["Jwt:Key"]
                ?? throw new InvalidOperationException("Jwt:Key no configurada."),
            Issuer = configuration["Jwt:Issuer"]
                ?? throw new InvalidOperationException("Jwt:Issuer no configurado."),
            Audience = configuration["Jwt:Audience"]
                ?? throw new InvalidOperationException("Jwt:Audience no configurado."),
            ExpirationHours = int.Parse(configuration["Jwt:ExpirationHours"] ?? "8")
        };

        // AuthService recibe los valores primitivos — sin depender de JwtOptions
        services.AddScoped<IAuthService>(_ => new AuthService(
            key: jwtOptions.Key,
            issuer: jwtOptions.Issuer,
            audience: jwtOptions.Audience,
            expirationHours: jwtOptions.ExpirationHours
        ));

        // UserAuthService recibe solo lo que necesita de JWT
        services.AddScoped<IUserAuthService>(sp => new UserAuthService(
            userRepository: sp.GetRequiredService<IUserRepository>(),
            authService: sp.GetRequiredService<IAuthService>(),
            expirationHours: jwtOptions.ExpirationHours
        ));

        services.AddScoped<ITodoItemService, TodoItemService>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}