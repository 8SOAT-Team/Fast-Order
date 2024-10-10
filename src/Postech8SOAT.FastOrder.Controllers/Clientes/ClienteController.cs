using CleanArch.UseCase.Logging;
using Postech8SOAT.FastOrder.Controllers.Clientes.Dtos;
using Postech8SOAT.FastOrder.Controllers.Interfaces;
using Postech8SOAT.FastOrder.Domain.ValueObjects;
using Postech8SOAT.FastOrder.Gateways.Interfaces;
using Postech8SOAT.FastOrder.Types.Results;
using Postech8SOAT.FastOrder.UseCases.Clientes;
using Postech8SOAT.FastOrder.UseCases.Clientes.Dtos;
using Postech8SOAT.FastOrder.Controllers.Problems;
using Postech8SOAT.FastOrder.Presenters.Clientes;
using Postech8SOAT.FastOrder.Domain.Entities;

namespace Postech8SOAT.FastOrder.Controllers.Clientes;
public class ClienteController : IClienteController
{
    private readonly ILogger _logger;
    private readonly IClienteGateway _clienteGateway;

    public ClienteController(ILogger logger, IClienteGateway clienteGateway)
    {
        _logger = logger;
        _clienteGateway = clienteGateway;
    }

    public async Task<Result<ClienteIdentificadoDto>> IdentificarClienteAsync(string document)
    {
        var isCpfValid = Cpf.TryCreate(document, out var cpf);

        if (isCpfValid is false)
        {
            return Result<ClienteIdentificadoDto>.Failure(new AppBadRequestProblemDetails("Cpf inválido", document));
        }

        var useCase = new IdentificarClienteUseCase(_logger, _clienteGateway);

        var useCaseResult = await useCase.ResolveAsync(cpf);

        if (useCase.IsFailure)
        {
            return Result<ClienteIdentificadoDto>.Failure(useCase.GetErrors().AdaptUseCaseErrors().ToList());
        }

        return useCaseResult.HasValue ? Result<ClienteIdentificadoDto>.Succeed(ClientePresenter.AdaptClienteIdentificado(useCaseResult.Value!))
            : Result<ClienteIdentificadoDto>.Empty();
    }

    public async Task<Result<ClienteIdentificadoDto>> CriarNovoClienteAsync(NovoClienteDto newCliente)
    {
        var isCpfValid = Cpf.TryCreate(newCliente.Cpf, out var cpf);
        var isEmailvalid = EmailAddress.TryCreate(newCliente.Email, out var email);

        var isValid = isCpfValid && isEmailvalid;
        if (!isValid)
        {
            var errors = new List<AppProblemDetails>();

            if (!isCpfValid)
            {
                errors.Add(new AppBadRequestProblemDetails("Cpf inválido", newCliente.Cpf));
            }

            if (!isEmailvalid)
            {
                errors.Add(new AppBadRequestProblemDetails("Email inválido", newCliente.Email));
            }

            return Result<ClienteIdentificadoDto>.Failure(errors);
        }

        var useCase = new CriarNovoClienteUseCase(_logger, _clienteGateway);
        var useCaseResult = await useCase.ResolveAsync(new CriarNovoClienteDto(cpf, newCliente.Nome, email));

        return ControllerResultBuilder<ClienteIdentificadoDto, Cliente>
           .ForUseCase(useCase)
           .WithResult(useCaseResult)
           .AdaptUsing(ClientePresenter.AdaptClienteIdentificado)
           .Build();
    }
}
