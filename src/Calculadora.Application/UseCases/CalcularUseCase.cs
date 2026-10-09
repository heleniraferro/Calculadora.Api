using Calculadora.Application.Dtos;
using Calculadora.Domain.Entities;
using Calculadora.Domain.Repositories;
using Calculadora.Domain.Services;

namespace Calculadora.Application.UseCases;

public class CalcularUseCase : ICalcularUseCase
{
    private readonly ICalculoRepository _repositorio;
    private readonly ServicoDeCalculo _servico;
    private readonly TimeProvider _relogio;

    public CalcularUseCase(ICalculoRepository repositorio, ServicoDeCalculo servico, TimeProvider relogio)
    {
        _repositorio = repositorio;
        _servico = servico;
        _relogio = relogio;
    }

    public async Task<CalculoResponse> ExecutarAsync(CalcularRequest request, CancellationToken cancellationToken)
    {
        var calculo = Calculo.Realizar(
            request.OperandoA,
            request.OperandoB,
            request.Operacao,
            _servico,
            _relogio.GetUtcNow());

        await _repositorio.AdicionarAsync(calculo, cancellationToken);

        return CalculoResponse.De(calculo);
    }
}
