using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Postech8SOAT.FastOrder.Controllers;
using Postech8SOAT.FastOrder.Controllers.Clientes;
using Postech8SOAT.FastOrder.Controllers.Interfaces;
using Postech8SOAT.FastOrder.Controllers.Pedidos;
using Postech8SOAT.FastOrder.Gateways;
using Postech8SOAT.FastOrder.Gateways.Interfaces;
using Postech8SOAT.FastOrder.Infra.Data.Context;
using Postech8SOAT.FastOrder.Infra.Data.Repositories;
using Postech8SOAT.FastOrder.Infra.Data.Repositories.Repository;
using Postech8SOAT.FastOrder.UseCases.Commands.Pedidos;
using Postech8SOAT.FastOrder.UseCases.Service;
using Postech8SOAT.FastOrder.UseCases.Service.Interfaces;

namespace Postech8SOAT.FastOrder.Infra.IOC;
public static class FastOrderRegisterDependencies
{

    public static void ConfigureDI(this IServiceCollection services, IConfiguration configuration)
    {
        //Registrar no container nativo de injeção de dependências.
        services.AddDbContext<FastOrderContext>(options =>
                options.UseLazyLoadingProxies().UseSqlServer(configuration.GetConnectionString("DefaultConnectionContainer")));

        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<IPedidoRepository, PedidoRepository>();
        services.AddScoped<IPagamentoRepository, PagamentoRepository>();

        services.AddScoped<IClienteUseCase, ClienteUseCase>();
        services.AddScoped<IPedidoUseCase, PedidoUseCase>();
        services.AddSingleton<IPedidoServiceUseCaseInvoker, PedidoServiceUseCaseInvoker>();
        services.AddScoped<IPagamentoUseCase, PagamentoUseCase>();

        //Gateways
        services.AddScoped<IClienteGateway, ClienteGateway>();
        services.AddScoped<ICategoriaGateway, CategoriaGateway>();
        services.AddScoped<IPagamentoGateway,PagamentoGateway>();
        services.AddScoped<IProdutoGateway,ProdutoGateway>();
        services.AddScoped<IPedidoGateway, PedidoGateway>();

        //Controllers
        services.AddScoped<IClienteController, ClienteController>();         
        services.AddScoped<ICategoriaController,CategoriaController>();
        services.AddScoped<IPagamentoController, PagamentoController>();
        services.AddScoped<IProdutoController, ProdutoController>();
        services.AddScoped<IPedidoController, PedidoController>();

        services.AddScoped<IClienteController, ClienteController>();
        services.AddScoped<IClienteGateway, ClienteGateway>(); 
    }
}
