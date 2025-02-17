using System;
using Xunit;
using Postech8SOAT.FastOrder.Infra.Env;

namespace Postech8SOAT.FastOrder.Domain.Tests.External.Env
{
    public class EnvConfigTest
    {
        [Fact]
        public void EnvironmentName_ShouldReturnCorrectValue()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
            Assert.Equal("Development", EnvConfig.EnvironmentName);
        }

        [Fact]
        public void IsTestEnv_ShouldReturnTrueForTestEnvironment()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "test");
            Assert.True(EnvConfig.IsTestEnv);
        }

        [Fact]
        public void DatabaseConnection_ShouldReturnCorrectValue()
        {
            Environment.SetEnvironmentVariable("CONNECTIONSTRINGS_DEFAULTCONNECTIONCONTAINER", "Server=myServer;Database=myDB;");
            Assert.Equal("Server=myServer;Database=myDB;", EnvConfig.DatabaseConnection);
        }

        [Fact]
        public void PagamentoWebhookUrl_ShouldReturnCorrectUri()
        {
            Environment.SetEnvironmentVariable("PAGAMENTO_WEBHOOK_URL", "https://example.com/webhook");
            Assert.Equal(new Uri("https://example.com/webhook"), EnvConfig.PagamentoWebhookUrl);
        }

        [Fact]
        public void PagamentoFornecedorAccessToken_ShouldReturnCorrectValue()
        {
            Environment.SetEnvironmentVariable("PAGAMENTO_FORNECEDOR_ACESS_TOKEN", "token123");
            Assert.Equal("token123", EnvConfig.PagamentoFornecedorAccessToken);
        }

        [Fact]
        public void DistributedCacheUrl_ShouldReturnCorrectValue()
        {
            Environment.SetEnvironmentVariable("DISTRIBUTED_CACHE_URL", "https://cache.example.com");
            Assert.Equal("https://cache.example.com", EnvConfig.DistributedCacheUrl);
        }

        [Fact]
        public void RunMigrationsOnStart_ShouldReturnTrue()
        {
            Environment.SetEnvironmentVariable("RUN_MIGRATIONS_ON_START", "true");
            Assert.True(EnvConfig.RunMigrationsOnStart);
        }

        [Fact]
        public void RunMigrationsOnStart_ShouldReturnFalse()
        {
            Environment.SetEnvironmentVariable("RUN_MIGRATIONS_ON_START", "false");
            Assert.False(EnvConfig.RunMigrationsOnStart);
        }
    }
}