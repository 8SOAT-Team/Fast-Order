using Microsoft.EntityFrameworkCore;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Infra.Data.EntityMapping;
using System;
using System.Linq;
using Postech8SOAT.FastOrder.Domain.Tests.Stubs.Pedidos;
using Xunit;

namespace Postech8SOAT.FastOrder.Domain.Tests.External.Data.EntityMapping
{
    public class PagamentoTypeConfigurationTest
    {
        [Fact]
        public void PagamentoTypeConfiguration_ShouldConfigureEntity()
        {
            var modelBuilder = new ModelBuilder(new Microsoft.EntityFrameworkCore.Metadata.Conventions.ConventionSet());
            var configuration = new PagamentoTypeConfiguration();

            configuration.Configure(modelBuilder.Entity<Pagamento>());

            var model = modelBuilder.Model;
            var entity = model.FindEntityType(typeof(Pagamento));

            Assert.NotNull(entity);
            Assert.Equal("Pagamentos", entity.GetTableName());
            Assert.Equal(6, entity.GetProperties().Count());
            Assert.Equal("Id", entity.FindPrimaryKey().Properties.Single().Name);
            Assert.Equal(18, entity.FindProperty("ValorTotal").GetPrecision());
            Assert.Equal(2, entity.FindProperty("ValorTotal").GetScale());
        }
        
    }

    public class TestDbContextPagamento : DbContext
    {
        public TestDbContextPagamento(DbContextOptions<TestDbContextPagamento> options) : base(options) { }

        public DbSet<Pagamento> Pagamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new PagamentoTypeConfiguration().Configure(modelBuilder.Entity<Pagamento>());
        }
    }
}