using Postech8SOAT.FastOrder.UseCases.Produtos.Dtos;

namespace Postech8SOAT.FastOrder.Domain.Tests.UseCases.Produtos.Dtos;
public class NovoProdutoDTOTest
{

    [Fact]
    public void DeveCriarNovoProdutoDTO_ComValoresCorretos()
    {
        // Arrange
        var nome = "Novo Produto";
        var descricao = "Descrição do Novo Produto";
        var preco = 39.99m;
        var imagem = "imagem_novo_produto.jpg";
        var categoriaId = Guid.NewGuid();

        // Act
        var novoProduto = new NovoProdutoDTO
        {
            Nome = nome,
            Descricao = descricao,
            Preco = preco,
            Imagem = imagem,
            CategoriaId = categoriaId
        };

        // Assert
        Assert.Equal(nome, novoProduto.Nome);
        Assert.Equal(descricao, novoProduto.Descricao);
        Assert.Equal(preco, novoProduto.Preco);
        Assert.Equal(imagem, novoProduto.Imagem);
        Assert.Equal(categoriaId, novoProduto.CategoriaId);
    }

    [Fact]
    public void DeveCriarNovoProdutoDTO_ComPrecoZero()
    {
        // Arrange
        var nome = "Produto com Preço Zero";
        var descricao = "Descrição do Produto com Preço Zero";
        var preco = 0m; 
        var imagem = "imagem_produto_preco_zero.jpg";
        var categoriaId = Guid.NewGuid();

        // Act
        var novoProduto = new NovoProdutoDTO
        {
            Nome = nome,
            Descricao = descricao,
            Preco = preco,
            Imagem = imagem,
            CategoriaId = categoriaId
        };

        // Assert
        Assert.Equal(nome, novoProduto.Nome);
        Assert.Equal(descricao, novoProduto.Descricao);
        Assert.Equal(preco, novoProduto.Preco);
        Assert.Equal(imagem, novoProduto.Imagem);
        Assert.Equal(categoriaId, novoProduto.CategoriaId);
    }

    [Fact]
    public void DeveCriarNovoProdutoDTO_ComImagemVazia()
    {
        // Arrange
        var nome = "Produto com Imagem Vazia";
        var descricao = "Descrição do Produto com Imagem Vazia";
        var preco = 59.99m;
        var imagem = ""; 
        var categoriaId = Guid.NewGuid();

        // Act
        var novoProduto = new NovoProdutoDTO
        {
            Nome = nome,
            Descricao = descricao,
            Preco = preco,
            Imagem = imagem,
            CategoriaId = categoriaId
        };

        // Assert
        Assert.Equal(nome, novoProduto.Nome);
        Assert.Equal(descricao, novoProduto.Descricao);
        Assert.Equal(preco, novoProduto.Preco);
        Assert.Equal(imagem, novoProduto.Imagem);
        Assert.Equal(categoriaId, novoProduto.CategoriaId);
    }

    [Fact]
    public void DeveCriarNovoProdutoDTO_ComNomeEDescricaoLongos()
    {
        // Arrange
        var nome = new string('A', 100); 
        var descricao = new string('B', 200); 
        var preco = 99.99m;
        var imagem = "imagem_nome_descricao_longos.jpg";
        var categoriaId = Guid.NewGuid();

        // Act
        var novoProduto = new NovoProdutoDTO
        {
            Nome = nome,
            Descricao = descricao,
            Preco = preco,
            Imagem = imagem,
            CategoriaId = categoriaId
        };

        // Assert
        Assert.Equal(nome, novoProduto.Nome);
        Assert.Equal(descricao, novoProduto.Descricao);
        Assert.Equal(preco, novoProduto.Preco);
        Assert.Equal(imagem, novoProduto.Imagem);
        Assert.Equal(categoriaId, novoProduto.CategoriaId);
    }

    [Fact]
    public void DeveSerIgualQuandoNovoProdutoDTOForDoMesmoValor()
    {
        // Arrange
        var nome = "Produto A";
        var descricao = "Descrição do Produto A";
        var preco = 19.99m;
        var imagem = "imagem_produto_a.jpg";
        var categoriaId = Guid.NewGuid();

        var produto1 = new NovoProdutoDTO
        {
            Nome = nome,
            Descricao = descricao,
            Preco = preco,
            Imagem = imagem,
            CategoriaId = categoriaId
        };

        var produto2 = new NovoProdutoDTO
        {
            Nome = nome,
            Descricao = descricao,
            Preco = preco,
            Imagem = imagem,
            CategoriaId = categoriaId
        };

        // Act and Assert
        Assert.Equal(produto1, produto2); 
    }

    [Fact]
    public void DeveSerDiferenteQuandoNovoProdutoDTOForDeValoresDiferentes()
    {
        // Arrange
        var nome1 = "Produto B";
        var descricao1 = "Descrição do Produto B";
        var preco1 = 29.99m;
        var imagem1 = "imagem_produto_b.jpg";
        var categoriaId1 = Guid.NewGuid();

        var nome2 = "Produto C";
        var descricao2 = "Descrição do Produto C";
        var preco2 = 49.99m;
        var imagem2 = "imagem_produto_c.jpg";
        var categoriaId2 = Guid.NewGuid();

        var produto1 = new NovoProdutoDTO
        {
            Nome = nome1,
            Descricao = descricao1,
            Preco = preco1,
            Imagem = imagem1,
            CategoriaId = categoriaId1
        };

        var produto2 = new NovoProdutoDTO
        {
            Nome = nome2,
            Descricao = descricao2,
            Preco = preco2,
            Imagem = imagem2,
            CategoriaId = categoriaId2
        };

        // Act and Assert
        Assert.NotEqual(produto1, produto2); 
    }

}
