using Calculadora.Application.Dtos;
using Calculadora.Domain.Repositories;

namespace Calculadora.Application.UseCases;

public class ListarHistoricoUseCase : IListarHistoricoUseCase
{
    private const int QuantidadeMaxima = 100;

    private readonly ICalculoRepository _repositorio;

    public ListarHistoricoUseCase(ICalculoRepository repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task<IReadOnlyList<CalculoResponse>> ExecutarAsync(int quantidade, CancellationToken cancellationToken)
    {
        var limite = Math.Clamp(quantidade, 1, QuantidadeMaxima);
        var calculos = await _repositorio.ListarRecentesAsync(limite, cancellationToken);

        return calculos.Select(CalculoResponse.De).ToList();
    }
}
