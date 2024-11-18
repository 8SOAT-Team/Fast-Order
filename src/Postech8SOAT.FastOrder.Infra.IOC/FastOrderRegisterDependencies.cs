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
               .Decorate<IClienteGateway, ClienteGatewayCache>();

        services.AddScoped<ICategoriaGateway, CategoriaGateway>()
                .Decorate<ICategoriaGateway, CategoriaGatewayCache>();

        services.AddScoped<IPagamentoGateway, PagamentoGateway>()
                .Decorate<IPagamentoGateway, PagamentoGatewayCache>();

        services.AddScoped<IProdutoGateway, ProdutoGateway>()
                .Decorate<IProdutoGateway, ProdutoGatewayCache>();

        services.AddScoped<IPedidoGateway, PedidoGateway>()
                .Decorate<IPedidoGateway, PedidoGatewayCache>();

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
        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(EnvConfig.DistributedCacheUrl));
        services.AddSingleton<ICacheContext, CacheContext>();

        return services;
    }
}
