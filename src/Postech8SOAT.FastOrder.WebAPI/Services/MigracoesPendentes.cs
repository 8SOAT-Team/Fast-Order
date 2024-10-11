using Microsoft.EntityFrameworkCore;
using Postech8SOAT.FastOrder.Infra.Data.Context;
using Serilog;

namespace Postech8SOAT.FastOrder.WebAPI.Services;

public static class MigracoesPendentes
{
    public static void ExecuteMigration(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var serviceDb = serviceScope.ServiceProvider
                         .GetService<FastOrderContext>();
        try
        {
            serviceDb!.Database.Migrate();
        }
        catch (Exception ex)
        {
            Log.Information(ex.Message);
            var conn = serviceDb!.Database.GetConnectionString();
            throw new Exception($"connection: {conn}", ex);
        }
    }
}