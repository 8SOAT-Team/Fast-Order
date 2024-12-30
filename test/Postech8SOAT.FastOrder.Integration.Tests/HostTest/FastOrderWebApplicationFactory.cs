using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Postech8SOAT.FastOrder.Infra.Data.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.MsSql;
using Polly;

namespace Postech8SOAT.FastOrder.Integration.Tests.HostTest;
public class FastOrderWebApplicationFactory: WebApplicationFactory<Program>, IAsyncLifetime
{
    private IServiceScope scope;

    public FastOrderContext Context { get; private set; }

    private readonly MsSqlContainer _mssqlContainer = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2022-latest")        
        .Build();

    public async Task InitializeAsync()
    {

        await _mssqlContainer.StartAsync();
        this.scope = Services.CreateScope();
        Context = scope.ServiceProvider.GetRequiredService<FastOrderContext>();
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
        await _mssqlContainer.DisposeAsync();
    }

    //public async Task<HttpClient> GetClientWithAccessTokenAsync()
    //{
    //    var client = this.CreateClient();

    //    var user = new UserDTO { Email = "tester@email.com", Password = "Senha123@" };

    //    var response = await client.PostAsJsonAsync("/auth-login", user);

    //    response.EnsureSuccessStatusCode();

    //    var result = await response.Content.ReadAsStringAsync();

    //    var options = new JsonSerializerOptions
    //    {
    //        PropertyNameCaseInsensitive = true,
    //    };

    //    var token = JsonSerializer.Deserialize<UserTokenDTO>(result, options);

    //    var clientAutenticado = this.CreateClient();

    //    clientAutenticado.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token!.Token);
    //    return clientAutenticado;
    //}
}
