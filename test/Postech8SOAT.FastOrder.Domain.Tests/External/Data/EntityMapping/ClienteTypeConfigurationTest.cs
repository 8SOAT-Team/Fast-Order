using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Infra.Data.EntityMapping;
using System;
using System.Linq;
using Xunit;

namespace Postech8SOAT.FastOrder.Domain.Tests.External.Data.EntityMapping
{
    public class ClienteTypeConfigurationTest
    {
        [Fact]
        public void ClienteTypeConfiguration_ShouldConfigureEntity()
        {
            var modelBuilder = new ModelBuilder(new Microsoft.EntityFrameworkCore.Metadata.Conventions.ConventionSet());
            var configuration = new ClienteTypeConfiguration();

            configuration.Configure(modelBuilder.Entity<Cliente>());

            var model = modelBuilder.Model;
            var entity = model.FindEntityType(typeof(Cliente));

            Assert.NotNull(entity);
            Assert.Equal("Clientes", entity.GetTableName());
            Assert.Equal(4, entity.GetProperties().Count());
            Assert.Equal("Id", entity.FindPrimaryKey().Properties.Single().Name);
            Assert.Equal(11, entity.FindProperty("Cpf").GetMaxLength());
            Assert.Equal(100, entity.FindProperty("Nome").GetMaxLength());
            Assert.Equal(100, entity.FindProperty("Email").GetMaxLength());
        }
        
    }

    public class TestDbContextCliente : DbContext
    {
        public TestDbContextCliente(DbContextOptions<TestDbContextCliente> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new ClienteTypeConfiguration().Configure(modelBuilder.Entity<Cliente>());
        }
    }
}