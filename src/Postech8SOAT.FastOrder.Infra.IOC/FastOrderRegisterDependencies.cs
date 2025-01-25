using MercadoPago.Client.Payment;
using MercadoPago.Client.Preference;
using MercadoPago.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Postech8SOAT.FastOrder.Controllers;
using Postech8SOAT.FastOrder.Controllers.Clientes;
using Postech8SOAT.FastOrder.Controllers.Interfaces;
using Postech8SOAT.FastOrder.Controllers.Pagamentos;
using Postech8SOAT.FastOrder.Controllers.Pedidos;
using Postech8SOAT.FastOrder.Gateways;
using Postech8SOAT.FastOrder.Gateways.Cache;
using Postech8SOAT.FastOrder.Infra.Data.Context;
using Postech8SOAT.FastOrder.Infra.Env;
using Postech8SOAT.FastOrder.Upstream.Pagamentos.Gateways;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Gateways;
using StackExchange.Redis;

namespace Postech8SOAT.FastOrder.Infra.IOC;

public static class DecoratorExtension
{
    public static IServiceCollection DecorateIf<TService, TDecorator>(this IServiceCollection services,
        Func<bool> condition)
        where TService : class
        where TDecorator : TService
    {
        return !condition() ? services : services.Decorate<TService, TDecorator>();
    }
}

public static class FastOrderRegisterDependencies
{
    public static void ConfigureDI(this IServiceCollection services)
    {
        services.AddDbContext<FastOrderContext>(options =>
            options.UseLazyLoadingProxies().UseSqlServer(EnvConfig.DatabaseConnection));

        services.AddHttpClient();

        services.AddGateways()
            .AddControllers()
            .UpstreamDI()
            .CacheDI();
    }

    private static IServiceCollection AddGateways(this IServiceCollection services)
    {
        services.AddScoped<IClienteGateway, ClienteGateway>()
            .DecorateIf<IClienteGateway, ClienteGatewayCache>(() => !EnvConfig.IsTestEnv);

        services.AddScoped<ICategoriaGateway, CategoriaGateway>()
            .DecorateIf<ICategoriaGateway, CategoriaGatewayCache>(() => !EnvConfig.IsTestEnv);

        services.AddScoped<IPagamentoGateway, PagamentoGateway>()
            .DecorateIf<IPagamentoGateway, PagamentoGatewayCache>(() => !EnvConfig.IsTestEnv);

        services.AddScoped<IProdutoGateway, ProdutoGateway>()
            .DecorateIf<IProdutoGateway, ProdutoGatewayCache>(() => !EnvConfig.IsTestEnv);

        services.AddScoped<IPedidoGateway, PedidoGateway>()
            .DecorateIf<IPedidoGateway, PedidoGatewayCache>(() => !EnvConfig.IsTestEnv);

        return services;
    }

    private static IServiceCollection AddControllers(this IServiceCollection services)
    {
        services.AddScoped<IClienteController, ClienteController>();
        services.AddScoped<ICategoriaController, CategoriaController>();
        services.AddScoped<IProdutoController, ProdutoController>();
        services.AddScoped<IPedidoController, PedidoController>();
        services.AddScoped<IPagamentoController, PagamentoController>();

        return services;
    }

    private static IServiceCollection UpstreamDI(this IServiceCollection services)
    {
        //Gateways
        services.AddSingleton<ISerializer, DefaultSerializer>();
        services.AddScoped<IFornecedorPagamentoGateway, FornecedorPagamentoGateway>();

        //client
        services.AddSingleton<PaymentClient>();
        services.AddSingleton<PreferenceClient>();

        return services;
    }

    private static IServiceCollection CacheDI(this IServiceCollection services)
    {
        services.AddSingleton<IConnectionMultiplexer>(
            ConnectionMultiplexer.Connect(EnvConfig.DistributedCacheUrl, o =>
            {
                o.AbortOnConnectFail = false;
                o.ConnectRetry = 2;
                o.Ssl = false;
                o.ConnectTimeout = 5;
            }));
        services.AddSingleton<ICacheContext, CacheContext>();

        return services;
    }
}