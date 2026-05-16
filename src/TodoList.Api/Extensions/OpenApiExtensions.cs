using Scalar.AspNetCore;

namespace TodoList.Api.Extensions;

public static class OpenApiExtensions
{
    public static IServiceCollection AddOpenApiDocumentation(
        this IServiceCollection services)
    {
        services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((document, context, token) =>
            {
                document.Info = new()
                {
                    Title = "TodoList API",
                    Version = "v1",
                    Description = "CRUD con Clean Architecture y JWT Authentication"
                };
                return Task.CompletedTask;
            });
        });

        return services;
    }

    public static WebApplication UseOpenApiDocumentation(this WebApplication app)
    {
        app.MapOpenApi();
        app.MapScalarApiReference(options =>
        {
            options.Title = "TodoList API";
            options.Theme = ScalarTheme.Moon;
            options.DefaultHttpClient = new(ScalarTarget.CSharp, ScalarClient.HttpClient);
        });
        return app;
    }
}

