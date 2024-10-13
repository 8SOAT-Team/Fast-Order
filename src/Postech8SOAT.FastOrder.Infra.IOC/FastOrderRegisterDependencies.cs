using MercadoPago.Client.OAuth;
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
using Postech8SOAT.FastOrder.Infra.Data.Context;
using Postech8SOAT.FastOrder.Upstream.Pagamentos.Gateways;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Gateways;

namespace Postech8SOAT.FastOrder.Infra.IOC;
public static class FastOrderRegisterDependencies
{

    public static void ConfigureDI(this IServiceCollection services, IConfiguration configuration)
    {
        var dbConnection = configuration["DefaultConnectionContainer"];
        //Registrar no container nativo de injeção de dependências.
        services.AddDbContext<FastOrderContext>(options =>
                options.UseLazyLoadingProxies().UseSqlServer(dbConnection));

        //client
        services.AddHttpClient();

        //Gateways
        services.AddScoped<IClienteGateway, ClienteGateway>();
        services.AddScoped<ICategoriaGateway, CategoriaGateway>();
        services.AddScoped<IPagamentoGateway, PagamentoGateway>();
        services.AddScoped<IProdutoGateway, ProdutoGateway>();
        services.AddScoped<IPedidoGateway, PedidoGateway>();

        //Controllers
        services.AddScoped<IClienteController, ClienteController>();
        services.AddScoped<ICategoriaController, CategoriaController>();
        services.AddScoped<IProdutoController, ProdutoController>();
        services.AddScoped<IPedidoController, PedidoController>();
        services.AddScoped<IPagamentoController, PagamentoController>();

        //Upstream
        services.UpstreamDI(configuration);
    }

    private static void UpstreamDI(this IServiceCollection services, IConfiguration configuration)
    {
        //Gateways
        services.AddSingleton<ISerializer, DefaultSerializer>();
        services.AddScoped<IFornecedorPagamentoGateway, FornecedorPagamentoGateway>();

        //client
        services.AddSingleton<PaymentClient>();
        services.AddSingleton<PreferenceClient>();

        //config
    }
}
