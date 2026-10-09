using Calculadora.Application.Dtos;
using Calculadora.Application.UseCases;
using Calculadora.Domain.Entities;
using Calculadora.Domain.Enums;
using Calculadora.Domain.Exceptions;
using Calculadora.Domain.Repositories;
using Calculadora.Domain.Services;

namespace Calculadora.Application.Tests;

public class CalcularUseCaseTests
{
    private readonly RepositorioEmMemoria _repositorio = new();
    private readonly CalcularUseCase _useCase;

    public CalcularUseCaseTests()
    {
        _useCase = new CalcularUseCase(_repositorio, new ServicoDeCalculo(), TimeProvider.System);
    }

    [Fact]
    public async Task Executar_DeveRetornarResultadoEPersistirCalculo()
    {
        var resposta = await _useCase.ExecutarAsync(new CalcularRequest(7, 5, Operacao.Soma), CancellationToken.None);

        Assert.Equal(12, resposta.Resultado);
        Assert.Single(_repositorio.Calculos);
    }

    [Fact]
    public async Task Executar_DivisaoPorZero_NaoDevePersistir()
    {
        await Assert.ThrowsAsync<DominioException>(() =>
            _useCase.ExecutarAsync(new CalcularRequest(7, 0, Operacao.Divisao), CancellationToken.None));

        Assert.Empty(_repositorio.Calculos);
    }

    private sealed class RepositorioEmMemoria : ICalculoRepository
    {
        public List<Calculo> Calculos { get; } = [];

        public Task AdicionarAsync(Calculo calculo, CancellationToken cancellationToken)
        {
            Calculos.Add(calculo);
            return Task.CompletedTask;
        }

        public Task<IReadOnlyList<Calculo>> ListarRecentesAsync(int quantidade, CancellationToken cancellationToken)
            => Task.FromResult<IReadOnlyList<Calculo>>(Calculos.Take(quantidade).ToList());
    }
}
