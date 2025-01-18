using Postech8SOAT.FastOrder.Domain.Entities;
using TechTalk.SpecFlow;

namespace Postech8SOAT.FastOrder.Domain.BDD.Tests.Step;

[Binding]
public class CategoriaSteps
{
    private string _nome;
    private string _descricao;
    private Categoria _categoria;
    private Exception _exception;

    [Given(@"que o nome da categoria é ""(.*)""")]
    public void DadoQueONomeDaCategoriaE(string nome)
    {
        _nome = nome;
    }

    [Given(@"a descrição da categoria é ""(.*)""")]
    public void DadoADescricaoDaCategoriaE(string descricao)
    {
        _descricao = descricao;
    }

    [When(@"eu criar uma categoria")]
    public void QuandoEuCriarUmaCategoria()
    {
        try
        {
            _categoria = new Categoria(_nome, _descricao);
        }
        catch (Exception ex)
        {
            _exception = ex;
        }
    }

    [Then(@"a categoria deve ser criada com sucesso")]
    public void EntaoACategoriaDeveSerCriadaComSucesso()
    {
        // Verifique se a categoria foi criada com sucesso
        Assert.NotNull(_categoria);
    }

    [Then(@"uma exceção do tipo ""(.*)"" deve ser lançada")]
    public void EntaoUmaExcecaoDoTipoDeveSerLancada(string exceptionType)
    {
        Assert.NotNull(_exception);
        Assert.Equal(exceptionType, _exception.GetType().Name);
    }
}
