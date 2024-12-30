using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Postech8SOAT.FastOrder.Infra.Data.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.MsSql;


namespace Postech8SOAT.FastOrder.Integration.Tests.HostTest;
public class FastOrderWebApplicationFactory: WebApplicationFactory<Program>, IAsyncLifetime
{
    private IServiceScope? scope;

    private readonly MsSqlContainer _mssqlContainer= new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")            
            .Build();

    public FastOrderContext? Context { get; private set; }

    public async Task InitializeAsync()
    {        
        await _mssqlContainer.StartAsync();       
        this.scope = Services.CreateScope();
        this.Context = scope.ServiceProvider.GetRequiredService<FastOrderContext>();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<FastOrderContext>));

            services.AddDbContext<FastOrderContext>(options =>
                options
                .UseLazyLoadingProxies()
                .UseSqlServer(_mssqlContainer.GetConnectionString()));
        });
    }

    public new async Task DisposeAsync()
    {
        if (scope != null)
        {
            scope.Dispose();
        }

        await _mssqlContainer.DisposeAsync();
    }

}
