using Postech8SOAT.FastOrder.WebAPI.Services;

namespace Postech8SOAT.FastOrder.WebAPI.Endpoints;

public static class MigrateExtension
{
    public static void AddEndpointMigrate(this WebApplication app)
    {
        app.MapPost("/migrate", async () =>
        {
            await MigracoesPendentes.ExecuteMigrationAsync(app);
            return Results.Ok();
        }).WithTags("Migrate")
        .WithSummary("Apply pending migrations.")
        .WithOpenApi();
    }
}
