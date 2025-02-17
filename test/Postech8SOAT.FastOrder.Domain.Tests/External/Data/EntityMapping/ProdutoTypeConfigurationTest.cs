using Microsoft.EntityFrameworkCore;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Infra.Data.EntityMapping;
using System;
using System.Linq;
using Xunit;

namespace Postech8SOAT.FastOrder.Domain.Tests.External.Data.EntityMapping
{
    public class ProdutoTypeConfigurationTest
    {
        [Fact]
        public void ProdutoTypeConfiguration_ShouldConfigureEntity()
        {
            var modelBuilder = new ModelBuilder(new Microsoft.EntityFrameworkCore.Metadata.Conventions.ConventionSet());
            var configuration = new ProdutoTypeConfiguration();

            configuration.Configure(modelBuilder.Entity<Produto>());

            var model = modelBuilder.Model;
            var entity = model.FindEntityType(typeof(Produto));

            Assert.NotNull(entity);
            Assert.Equal("Produtos", entity.GetTableName());
            Assert.Equal(6, entity.GetProperties().Count());
            Assert.Equal("Id", entity.FindPrimaryKey().Properties.Single().Name);
            Assert.Equal(100, entity.FindProperty("Nome").GetMaxLength());
            Assert.Equal(100, entity.FindProperty("Descricao").GetMaxLength());
            Assert.Equal(300, entity.FindProperty("Imagem").GetMaxLength());
            Assert.Equal(18, entity.FindProperty("Preco").GetPrecision());
            Assert.Equal(2, entity.FindProperty("Preco").GetScale());
        }

        [Fact]
        public void ProdutoTypeConfiguration_ShouldSeedData()
        {
            var options = new DbContextOptionsBuilder<TestDbContextProduto>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new TestDbContextProduto(options))
            {
                context.Database.EnsureCreated();

                var produtos = context.Set<Produto>().ToList();
                Assert.Equal(0, produtos.Count); 
            }
        }
    }

    public class TestDbContextProduto : DbContext
    {
        public TestDbContextProduto(DbContextOptions<TestDbContextProduto> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new ProdutoTypeConfiguration().Configure(modelBuilder.Entity<Produto>());
        }
    }
}