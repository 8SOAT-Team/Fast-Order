using TechTalk.SpecFlow;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.ValueObjects; 

namespace Postech8SOAT.FastOrder.Tests.BDD.Step
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

        [Given(@"que o cliente foi criado com CPF ""(.*)"", nome ""(.*)"" e email ""(.*)""")]
        public void DadoQueOClienteFoiCriadoComCPFNomeEEmail(string cpf, string nome, string email)
        {
            try
            {
                _cliente = new Cliente(Guid.NewGuid(), cpf, nome, email);
            }
            catch (Exception ex)
            {
                _exception = ex;
            }

        }


        [Given(@"o novo nome do cliente é ""(.*)""")]
        public void DadoONovoNomeDoClienteE(string novoNome)
        {
            try
            {
                _cliente.ChangeNome(novoNome);
            }
            catch (Exception ex)
            {
                _exception = ex;
            }

        }

        [When(@"eu alterar o nome do cliente")]
        public void QuandoEuAlterarONomeDoCliente()
        {
            try
            {
                _cliente.ChangeNome("João Silva");
            }
            catch (Exception ex)
            {
                _exception = ex;
            }
        }

        [When(@"eu tentar criar o cliente")]
        public void QuandoEuTentarCriarOCliente()
        {
            try
            {
                _cliente ??= new Cliente(Guid.NewGuid(), "765.273.200-00", "João da Silva", "joao@example.com");
            }
            catch (Exception ex)
            {
                _exception = ex;
            }
        }

        [Then(@"uma exceção de validação deve ser lançada")]
        public void EntaoUmaExcecaoDeValidacaoDeveSerLancada()
        {
            Assert.NotNull(_exception);
        }

        [Then(@"o nome do cliente deve ser ""(.*)""")]
        public void EntaoONomeDoClienteDeveSer(string nome)
        {
            Assert.Equal(nome, _cliente.Nome);
        }

        [Then(@"o cliente deve ser válido")]
        public void EntaoOClienteDeveSerValido()
        {
            Assert.NotNull(_cliente);
            Assert.Null(_exception);
        }

        [Given(@"o novo email do cliente é ""(.*)""")]
        public void DadoONovoEmailDoClienteE(string email)
        {
            try
            {
                _cliente.ChangeEmail(new EmailAddress(email));
            }
            catch (Exception ex)
            {
                _exception = ex;
            }
        }

        [When(@"eu alterar o email do cliente para ""(.*)""")]
        public void QuandoEuAlterarOEmailDoCliente(string email)
        {
            _cliente.ChangeEmail(new EmailAddress(email));
        }

        [Then(@"o email do cliente deve ser ""(.*)""")]
        public void EntaoOEmailDoClienteDeveSer(string emailAlterado)
        {
            Assert.Equal(emailAlterado, _cliente.Email);
        }
    }
}
