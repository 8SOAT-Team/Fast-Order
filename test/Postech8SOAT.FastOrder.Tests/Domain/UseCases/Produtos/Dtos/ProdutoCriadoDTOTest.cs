using Postech8SOAT.FastOrder.UseCases.Produtos.Dtos;

namespace Postech8SOAT.FastOrder.Tests.Domain.UseCases.Produtos.Dtos;
public class ProdutoCriadoDTOTest
{

    [Fact]
    public void DeveCriarProdutoCriadoDTO_ComValoresCorretos()
    {
        // Arrange
        var id = Guid.NewGuid();
        var nome = "Produto Criado";
        var descricao = "Descrição do Produto Criado";
        var preco = 59.99m;
        var imagem = "imagem_produto_criado.jpg";
        var categoria = new ProdutoCategoriaDTO
        {
            Id = Guid.NewGuid(),
            Nome = "Categoria Criada"
        };

        // Act
        var produtoCriado = new ProdutoCriadoDTO
        {
            Id = id,
            Nome = nome,
            Descricao = descricao,
            Preco = preco,
            Imagem = imagem,
            Categoria = categoria
        };

        // Assert
        Assert.Equal(id, produtoCriado.Id);
        Assert.Equal(nome, produtoCriado.Nome);
        Assert.Equal(descricao, produtoCriado.Descricao);
        Assert.Equal(preco, produtoCriado.Preco);
        Assert.Equal(imagem, produtoCriado.Imagem);
        Assert.Equal(categoria, produtoCriado.Categoria);
    }

    [Fact]
    public void DeveCriarProdutoCriadoDTO_ComPrecoZero()
    {
        // Arrange
        var id = Guid.NewGuid();
        var nome = "Produto Criado com Preço Zero";
        var descricao = "Descrição do Produto Criado com Preço Zero";
        var preco = 0m;
        var imagem = "imagem_produto_preco_zero.jpg";
        var categoria = new ProdutoCategoriaDTO
        {
            Id = Guid.NewGuid(),
            Nome = "Categoria Preço Zero"
        };

        // Act
        var produtoCriado = new ProdutoCriadoDTO
        {
            Id = id,
            Nome = nome,
            Descricao = descricao,
            Preco = preco,
            Imagem = imagem,
            Categoria = categoria
        };

        // Assert
        Assert.Equal(id, produtoCriado.Id);
        Assert.Equal(nome, produtoCriado.Nome);
        Assert.Equal(descricao, produtoCriado.Descricao);
        Assert.Equal(preco, produtoCriado.Preco);
        Assert.Equal(imagem, produtoCriado.Imagem);
        Assert.Equal(categoria, produtoCriado.Categoria);
    }

    [Fact]
    public void DeveCriarProdutoCriadoDTO_ComImagemVazia()
    {
        // Arrange
        var id = Guid.NewGuid();
        var nome = "Produto Criado com Imagem Vazia";
        var descricao = "Descrição do Produto Criado com Imagem Vazia";
        var preco = 79.99m;
        var imagem = "";
        var categoria = new ProdutoCategoriaDTO
        {
            Id = Guid.NewGuid(),
            Nome = "Categoria Imagem Vazia"
        };

        // Act
        var produtoCriado = new ProdutoCriadoDTO
        {
            Id = id,
            Nome = nome,
            Descricao = descricao,
            Preco = preco,
            Imagem = imagem,
            Categoria = categoria
        };

        // Assert
        Assert.Equal(id, produtoCriado.Id);
        Assert.Equal(nome, produtoCriado.Nome);
        Assert.Equal(descricao, produtoCriado.Descricao);
        Assert.Equal(preco, produtoCriado.Preco);
        Assert.Equal(imagem, produtoCriado.Imagem);
        Assert.Equal(categoria, produtoCriado.Categoria);
    }

    [Fact]
    public void DeveCriarProdutoCriadoDTO_ComNomeEDescricaoLongos()
    {
        // Arrange
        var nome = new string('A', 100);
        var descricao = new string('B', 200);
        var preco = 99.99m;
        var imagem = "imagem_nome_descricao_longos.jpg";
        var categoria = new ProdutoCategoriaDTO
        {
            Id = Guid.NewGuid(),
            Nome = "Categoria Longa"
        };

        // Act
        var produtoCriado = new ProdutoCriadoDTO
        {
            Id = Guid.NewGuid(),
            Nome = nome,
            Descricao = descricao,
            Preco = preco,
            Imagem = imagem,
            Categoria = categoria
        };

        // Assert
        Assert.Equal(nome, produtoCriado.Nome);
        Assert.Equal(descricao, produtoCriado.Descricao);
        Assert.Equal(preco, produtoCriado.Preco);
        Assert.Equal(imagem, produtoCriado.Imagem);
        Assert.Equal(categoria, produtoCriado.Categoria);
    }

    [Fact]
    public void DeveSerIgualQuandoProdutoCriadoDTOForDoMesmoValor()
    {
        // Arrange
        var id = Guid.NewGuid();
        var nome = "Produto Criado A";
        var descricao = "Descrição do Produto Criado A";
        var preco = 49.99m;
        var imagem = "imagem_produto_a.jpg";
        var categoria = new ProdutoCategoriaDTO
        {
            Id = Guid.NewGuid(),
            Nome = "Categoria A"
        };

        var produto1 = new ProdutoCriadoDTO
        {
            Id = id,
            Nome = nome,
            Descricao = descricao,
            Preco = preco,
            Imagem = imagem,
            Categoria = categoria
        };

        var produto2 = new ProdutoCriadoDTO
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
    public void DeveSerDiferenteQuandoProdutoCriadoDTOForDeValoresDiferentes()
    {
        // Arrange
        var id1 = Guid.NewGuid();
        var nome1 = "Produto Criado B";
        var descricao1 = "Descrição do Produto Criado B";
        var preco1 = 69.99m;
        var imagem1 = "imagem_produto_b.jpg";
        var categoria1 = new ProdutoCategoriaDTO
        {
            Id = Guid.NewGuid(),
            Nome = "Categoria B"
        };

        var id2 = Guid.NewGuid();
        var nome2 = "Produto Criado C";
        var descricao2 = "Descrição do Produto Criado C";
        var preco2 = 89.99m;
        var imagem2 = "imagem_produto_c.jpg";
        var categoria2 = new ProdutoCategoriaDTO
        {
            Id = Guid.NewGuid(),
            Nome = "Categoria C"
        };

        var produto1 = new ProdutoCriadoDTO
        {
            Id = id1,
            Nome = nome1,
            Descricao = descricao1,
            Preco = preco1,
            Imagem = imagem1,
            Categoria = categoria1
        };

        var produto2 = new ProdutoCriadoDTO
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
