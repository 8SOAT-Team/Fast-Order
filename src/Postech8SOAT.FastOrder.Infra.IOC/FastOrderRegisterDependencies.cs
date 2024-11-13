using MercadoPago.Client.Payment;
using MercadoPago.Client.Preference;
using MercadoPago.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Postech8SOAT.FastOrder.Controllers;
using Postech8SOAT.FastOrder.Controllers.Clientes;
using Postech8SOAT.FastOrder.Controllers.Interfaces;
using Postech8SOAT.FastOrder.Controllers.Pagamentos;
using Postech8SOAT.FastOrder.Controllers.Pedidos;
using Postech8SOAT.FastOrder.Gateways;
using Postech8SOAT.FastOrder.Gateways.Cache;
using Postech8SOAT.FastOrder.Infra.Data.Context;
using Postech8SOAT.FastOrder.Upstream.Pagamentos.Gateways;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Gateways;
using StackExchange.Redis;

namespace Postech8SOAT.FastOrder.Infra.IOC;
public static class FastOrderRegisterDependencies
{

    public static void ConfigureDI(this IServiceCollection services, IConfiguration configuration)
    {
        var dbConnection = configuration.GetConnectionString("DefaultConnectionContainer");
        //Registrar no container nativo de injeção de dependências.
        services.AddDbContext<FastOrderContext>(options =>
                options.UseLazyLoadingProxies().UseSqlServer(dbConnection));

        //client
        services.AddHttpClient();

        //Gateways
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

        //Controllers
        services.AddScoped<IClienteController, ClienteController>();
        services.AddScoped<ICategoriaController, CategoriaController>();
        services.AddScoped<IProdutoController, ProdutoController>();
        services.AddScoped<IPedidoController, PedidoController>();
        services.AddScoped<IPagamentoController, PagamentoController>();

        //Upstream
        services.UpstreamDI(configuration);
        services.CacheDI(configuration);
    }

    private static void UpstreamDI(this IServiceCollection services, IConfiguration configuration)
    {
        //Gateways
        services.AddSingleton<ISerializer, DefaultSerializer>();
        services.AddScoped<IFornecedorPagamentoGateway, FornecedorPagamentoGateway>();

        //client
        services.AddSingleton<PaymentClient>();
        services.AddSingleton<PreferenceClient>();
    }

    private static void CacheDI(this IServiceCollection services, IConfiguration configuration)
    {
        var cacheConnection = configuration.GetValue<string>("DISTRIBUTED_CACHE_URl");

        if(cacheConnection is null || cacheConnection == string.Empty)
        {
            throw new Exception("DISTRIBUTED_CACHE_URL não configurado");
        }

        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(cacheConnection));
        services.AddSingleton<ICacheContext, CacheContext>();
    }
}
