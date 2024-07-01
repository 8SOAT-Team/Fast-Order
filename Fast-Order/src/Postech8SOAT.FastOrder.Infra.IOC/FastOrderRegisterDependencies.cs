using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Postech8SOAT.FastOrder.Infra.IOC;
public static class FastOrderRegisterDependencies
{
    private static IConfiguration _configuration;

    public static void ConfigureDI(this IServiceCollection services, IConfiguration configuration)
    {
        //Registrar no container nativo de injeção de dependências.
    }
}
