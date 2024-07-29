using Microsoft.EntityFrameworkCore;
using Postech8SOAT.FastOrder.Infra.Data.Context;

namespace Postech8SOAT.FastOrder.WebAPI.Services;

public static class MigracoesPendentes
{
    public static void ExecuteMigration(this IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            var serviceDb = serviceScope.ServiceProvider
                             .GetService<FastOrderContext>();

            serviceDb!.Database.Migrate();
        }
    }
}