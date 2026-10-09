using Calculadora.Domain.Enums;
using Calculadora.Domain.Exceptions;
using Calculadora.Domain.Services;

namespace Calculadora.Domain.Tests;

public class ServicoDeCalculoTests
{
    private readonly ServicoDeCalculo _servico = new();

    [Theory]
    [InlineData(2, 3, Operacao.Soma, 5)]
    [InlineData(2, 3, Operacao.Subtracao, -1)]
    [InlineData(2, 3, Operacao.Multiplicacao, 6)]
    [InlineData(6, 3, Operacao.Divisao, 2)]
    [InlineData(1, 4, Operacao.Divisao, 0.25)]
    public void Executar_DeveRetornarResultadoCorreto(decimal a, decimal b, Operacao operacao, decimal esperado)
    {
        Assert.Equal(esperado, _servico.Executar(a, b, operacao));
    }

    [Fact]
    public void Executar_DivisaoPorZero_DeveLancarDominioException()
    {
        var excecao = Assert.Throws<DominioException>(() => _servico.Executar(1, 0, Operacao.Divisao));

        Assert.Equal("Não é possível dividir por zero.", excecao.Message);
    }

    [Fact]
    public void Executar_OperacaoInvalida_DeveLancarDominioException()
    {
        Assert.Throws<DominioException>(() => _servico.Executar(1, 1, (Operacao)99));
    }

    [Fact]
    public void Executar_Overflow_DeveLancarDominioException()
    {
        Assert.Throws<DominioException>(() => _servico.Executar(decimal.MaxValue, 2, Operacao.Multiplicacao));
    }
}
