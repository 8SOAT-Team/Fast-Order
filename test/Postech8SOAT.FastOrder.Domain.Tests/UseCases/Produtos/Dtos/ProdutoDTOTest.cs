using Postech8SOAT.FastOrder.UseCases.Produtos.Dtos;

namespace Postech8SOAT.FastOrder.Domain.Tests.UseCases.Produtos.Dtos;
public class ProdutoDTOTest
{
    [Fact]
    public void DeveCriarProdutoDTO_ComValoresCorretos()
    {
        // Arrange
        var id = Guid.NewGuid();
        var nome = "Produto A";
        var descricao = "Descrição do Produto A";
        var preco = 19.99m;
        var imagem = "imagem_produto_a.jpg";
        var categoria = new ProdutoCategoriaDTO
        {
            Id = Guid.NewGuid(),
            Nome = "Categoria A"
        };

        // Act
        var produto = new ProdutoDTO
        {
            Id = id,
            Nome = nome,
            Descricao = descricao,
            Preco = preco,
            Imagem = imagem,
            Categoria = categoria
        };

        // Assert
        Assert.Equal(id, produto.Id);
        Assert.Equal(nome, produto.Nome);
        Assert.Equal(descricao, produto.Descricao);
        Assert.Equal(preco, produto.Preco);
        Assert.Equal(imagem, produto.Imagem);
        Assert.Equal(categoria, produto.Categoria);
    }

    [Fact]
    public void DeveCriarProdutoDTO_ComPrecoZero()
    {
        // Arrange
        var id = Guid.NewGuid();
        var nome = "Produto B";
        var descricao = "Descrição do Produto B";
        var preco = 0m; // Preço zero
        var imagem = "imagem_produto_b.jpg";
        var categoria = new ProdutoCategoriaDTO
        {
            Id = Guid.NewGuid(),
            Nome = "Categoria B"
        };

        // Act
        var produto = new ProdutoDTO
        {
            Id = id,
            Nome = nome,
            Descricao = descricao,
            Preco = preco,
            Imagem = imagem,
            Categoria = categoria
        };

        // Assert
        Assert.Equal(id, produto.Id);
        Assert.Equal(nome, produto.Nome);
        Assert.Equal(descricao, produto.Descricao);
        Assert.Equal(preco, produto.Preco);
        Assert.Equal(imagem, produto.Imagem);
        Assert.Equal(categoria, produto.Categoria);
    }

    [Fact]
    public void DeveCriarProdutoDTO_ComCategoriaNula()
    {
        // Arrange
        var id = Guid.NewGuid();
        var nome = "Produto C";
        var descricao = "Descrição do Produto C";
        var preco = 29.99m;
        var imagem = "imagem_produto_c.jpg";
        ProdutoCategoriaDTO? categoria = null; 

        // Act
        var produto = new ProdutoDTO
        {
            Id = id,
            Nome = nome,
            Descricao = descricao,
            Preco = preco,
            Imagem = imagem,
            Categoria = categoria
        };

        // Assert
        Assert.Equal(id, produto.Id);
        Assert.Equal(nome, produto.Nome);
        Assert.Equal(descricao, produto.Descricao);
        Assert.Equal(preco, produto.Preco);
        Assert.Equal(imagem, produto.Imagem);
        Assert.Null(produto.Categoria); 
    }

    [Fact]
    public void DeveCriarProdutoDTO_ComImagemVazia()
    {
        // Arrange
        var id = Guid.NewGuid();
        var nome = "Produto D";
        var descricao = "Descrição do Produto D";
        var preco = 49.99m;
        var imagem = ""; // Imagem vazia
        var categoria = new ProdutoCategoriaDTO
        {
            Id = Guid.NewGuid(),
            Nome = "Categoria D"
        };

        // Act
        var produto = new ProdutoDTO
        {
            Id = id,
            Nome = nome,
            Descricao = descricao,
            Preco = preco,
            Imagem = imagem,
            Categoria = categoria
        };

        // Assert
        Assert.Equal(id, produto.Id);
        Assert.Equal(nome, produto.Nome);
        Assert.Equal(descricao, produto.Descricao);
        Assert.Equal(preco, produto.Preco);
        Assert.Equal(imagem, produto.Imagem);
        Assert.Equal(categoria, produto.Categoria);
    }

    [Fact]
    public void DeveSerIgualQuandoProdutoDTOForDoMesmoValor()
    {
        // Arrange
        var id = Guid.NewGuid();
        var nome = "Produto E";
        var descricao = "Descrição do Produto E";
        var preco = 59.99m;
        var imagem = "imagem_produto_e.jpg";
        var categoria = new ProdutoCategoriaDTO
        {
            Id = Guid.NewGuid(),
            Nome = "Categoria E"
        };

        var produto1 = new ProdutoDTO
        {
            Id = id,
            Nome = nome,
            Descricao = descricao,
            Preco = preco,
            Imagem = imagem,
            Categoria = categoria
        };

        var produto2 = new ProdutoDTO
        {
            Id = id,
            Nome = nome,
            Descricao = descricao,
            Preco = preco,
            Imagem = imagem,
            Categoria = categoria
        };

        // Act and Assert
        Assert.Equal(produto1, produto2); 
    }

    [Fact]
    public void DeveSerDiferenteQuandoProdutoDTOForDeValoresDiferentes()
    {
        // Arrange
        var id1 = Guid.NewGuid();
        var nome1 = "Produto F";
        var descricao1 = "Descrição do Produto F";
        var preco1 = 69.99m;
        var imagem1 = "imagem_produto_f.jpg";
        var categoria1 = new ProdutoCategoriaDTO
        {
            Id = Guid.NewGuid(),
            Nome = "Categoria F"
        };

        var id2 = Guid.NewGuid();
        var nome2 = "Produto G";
        var descricao2 = "Descrição do Produto G";
        var preco2 = 89.99m;
        var imagem2 = "imagem_produto_g.jpg";
        var categoria2 = new ProdutoCategoriaDTO
        {
            Id = Guid.NewGuid(),
            Nome = "Categoria G"
        };

        var produto1 = new ProdutoDTO
        {
            Id = id1,
            Nome = nome1,
            Descricao = descricao1,
            Preco = preco1,
            Imagem = imagem1,
            Categoria = categoria1
        };

        var produto2 = new ProdutoDTO
        {
            Id = id2,
            Nome = nome2,
            Descricao = descricao2,
            Preco = preco2,
            Imagem = imagem2,
            Categoria = categoria2
        };

        // Act and Assert
        Assert.NotEqual(produto1, produto2);
    }
}
