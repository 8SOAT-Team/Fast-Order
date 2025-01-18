using TechTalk.SpecFlow;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.ValueObjects;

namespace Postech8SOAT.FastOrder.Domain.BDD.Tests.Step
{
    [Binding]
    public class ClienteSteps
    {
        private Cliente _cliente;
        private Exception _exception;

        [Given(@"que o CPF do cliente é ""(.*)""")]
        public void DadoQueOCpfDoClienteE(string cpf)
        {
            try
            {
                if (_cliente == null)
                {
                    _cliente = new Cliente(Guid.NewGuid(), cpf, "João da Silva", "joao@example.com");
                }
            }
            catch (Exception ex)
            {
                _exception = ex;
            }
        }

        [Given(@"o nome do cliente é ""(.*)""")]
        public void DadoQueONomeDoClienteE(string nome)
        {
            try
            {
                if (_cliente == null)
                {
                    _cliente = new Cliente(Guid.NewGuid(), "111.222.333-44", nome, "joao@example.com");
                }
                else
                {
                    _cliente.ChangeNome(nome);
                }
            }
            catch (Exception ex)
            {
                _exception = ex;
            }
        }

        [Given(@"o email do cliente é ""(.*)""")]
        public void DadoQueOEmailDoClienteE(string email)
        {
            try
            {
                if (_cliente == null)
                {
                    _cliente = new Cliente(Guid.NewGuid(), "765.273.200-00", "João da Silva", email);
                }
                else
                {
                    _cliente.ChangeEmail(new EmailAddress(email));
                }
            }
            catch (Exception ex)
            {
                _exception = ex;
            }
        }

        [When(@"eu criar o cliente")]
        public void QuandoEuCriarOCliente()
        {
            try
            {
                // Caso os dados já estejam definidos, considera a criação
                _cliente ??= new Cliente(Guid.NewGuid(), "765.273.200-00", "João da Silva", "joao@example.com");
            }
            catch (Exception ex)
            {
                _exception = ex;
            }
        }

        [Then(@"o cliente deve ser válido")]
        public void EntaoOClienteDeveSerValido()
        {
            Assert.NotNull(_cliente);
            Assert.Null(_exception);
        }
    }
}
