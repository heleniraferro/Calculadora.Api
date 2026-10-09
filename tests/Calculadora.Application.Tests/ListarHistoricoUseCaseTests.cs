using Calculadora.Application.UseCases;
using Calculadora.Domain.Entities;
using Calculadora.Domain.Enums;
using Calculadora.Domain.Repositories;
using Calculadora.Domain.Services;

namespace Calculadora.Application.Tests;

public class ListarHistoricoUseCaseTests
{
    [Theory]
    [InlineData(0, 1)]
    [InlineData(20, 20)]
    [InlineData(5000, 100)]
    public async Task Executar_DeveLimitarQuantidadeSolicitada(int solicitada, int esperada)
    {
        var repositorio = new RepositorioEspiao();
        var useCase = new ListarHistoricoUseCase(repositorio);

        await useCase.ExecutarAsync(solicitada, CancellationToken.None);

        Assert.Equal(esperada, repositorio.QuantidadeRecebida);
    }

    [Fact]
    public async Task Executar_DeveMapearCalculosParaResposta()
    {
        var calculo = Calculo.Realizar(2, 3, Operacao.Multiplicacao, new ServicoDeCalculo(), DateTimeOffset.UtcNow);
        var useCase = new ListarHistoricoUseCase(new RepositorioEspiao(calculo));

        var historico = await useCase.ExecutarAsync(10, CancellationToken.None);

        var item = Assert.Single(historico);
        Assert.Equal(6, item.Resultado);
        Assert.Equal(Operacao.Multiplicacao, item.Operacao);
    }

    private sealed class RepositorioEspiao(params Calculo[] calculos) : ICalculoRepository
    {
        public int QuantidadeRecebida { get; private set; }

        public Task AdicionarAsync(Calculo calculo, CancellationToken cancellationToken) => Task.CompletedTask;

        public Task<IReadOnlyList<Calculo>> ListarRecentesAsync(int quantidade, CancellationToken cancellationToken)
        {
            QuantidadeRecebida = quantidade;
            return Task.FromResult<IReadOnlyList<Calculo>>(calculos);
        }
    }
}
