using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Infra.Data.EntityMapping;
using System;
using System.Linq;
using Xunit;

namespace Postech8SOAT.FastOrder.Domain.Tests.External.Data.EntityMapping
{
    public class ItensDoPedidoTypeConfigurationTest
    {
        [Fact]
        public void ItensDoPedidoTypeConfiguration_ShouldConfigureEntity()
        {
            var modelBuilder = new ModelBuilder(new Microsoft.EntityFrameworkCore.Metadata.Conventions.ConventionSet());
            var configuration = new ItensDoPedidoTypeConfiguration();

            configuration.Configure(modelBuilder.Entity<ItemDoPedido>());

            var model = modelBuilder.Model;
            var entity = model.FindEntityType(typeof(ItemDoPedido));

            Assert.NotNull(entity);
            Assert.Equal("ItensDoPedido", entity.GetTableName());
            Assert.Equal(3, entity.GetProperties().Count());
            Assert.Equal("Id", entity.FindPrimaryKey().Properties.Single().Name);
        }
    }

    public class TestDbContextItens : DbContext
    {
        public TestDbContextItens(DbContextOptions<TestDbContextItens> options) : base(options) { }

        public DbSet<ItemDoPedido> ItensDoPedido { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new ItensDoPedidoTypeConfiguration().Configure(modelBuilder.Entity<ItemDoPedido>());
        }
    }
}