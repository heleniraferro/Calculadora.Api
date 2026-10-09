using Calculadora.Application.Dtos;

namespace Calculadora.Application.UseCases;

public interface ICalcularUseCase
{
    Task<CalculoResponse> ExecutarAsync(CalcularRequest request, CancellationToken cancellationToken);
}
