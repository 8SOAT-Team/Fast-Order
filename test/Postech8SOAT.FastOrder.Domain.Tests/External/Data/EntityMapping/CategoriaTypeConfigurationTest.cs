using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Infra.Data.EntityMapping;
using System;
using System.Linq;
using Xunit;

namespace Postech8SOAT.FastOrder.Domain.Tests.External.Data.EntityMapping
{
    public class CategoriaTypeConfigurationTest
    {
        [Fact]
        public void CategoriaTypeConfiguration_ShouldConfigureEntity()
        {
            var modelBuilder = new ModelBuilder(new Microsoft.EntityFrameworkCore.Metadata.Conventions.ConventionSet());
            var configuration = new CategoriaTypeConfiguration();

            configuration.Configure(modelBuilder.Entity<Categoria>());

            var model = modelBuilder.Model;
            var entity = model.FindEntityType(typeof(Categoria));

            Assert.NotNull(entity);
            Assert.Equal("Categorias", entity.GetTableName());
            Assert.Equal(3, entity.GetProperties().Count());
            Assert.Equal("Id", entity.FindPrimaryKey().Properties.Single().Name);
            Assert.Equal(100, entity.FindProperty("Nome").GetMaxLength());
            Assert.Equal(100, entity.FindProperty("Descricao").GetMaxLength());
        }

        [Fact]
        public void CategoriaTypeConfiguration_ShouldSeedData()
        {
            var options = new DbContextOptionsBuilder<TestDbContextCategoria>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new TestDbContextCategoria(options))
            {
                context.Database.EnsureCreated();

                var categorias = context.Set<Categoria>().ToList();
                Assert.Equal(4, categorias.Count);
                Assert.Contains(categorias, c => c.Nome == "Lanche" && c.Descricao == "Lanches");
                Assert.Contains(categorias, c => c.Nome == "Acompanhamento" && c.Descricao == "Acompanhamentos");
                Assert.Contains(categorias, c => c.Nome == "Bebida" && c.Descricao == "Bebidas");
                Assert.Contains(categorias, c => c.Nome == "Sobremesa" && c.Descricao == "Sobremesas");
            }
        }
    }

    public class TestDbContextCategoria : DbContext
    {
        public TestDbContextCategoria(DbContextOptions<TestDbContextCategoria> options) : base(options) { }

        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new CategoriaTypeConfiguration().Configure(modelBuilder.Entity<Categoria>());
        }
    }
}