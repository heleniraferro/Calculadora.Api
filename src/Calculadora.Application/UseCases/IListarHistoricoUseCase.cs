using Calculadora.Application.Dtos;

namespace Calculadora.Application.UseCases;

public interface IListarHistoricoUseCase
{
    Task<IReadOnlyList<CalculoResponse>> ExecutarAsync(int quantidade, CancellationToken cancellationToken);
}
