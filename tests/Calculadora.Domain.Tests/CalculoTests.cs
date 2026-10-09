using Calculadora.Domain.Entities;
using Calculadora.Domain.Enums;
using Calculadora.Domain.Services;

namespace Calculadora.Domain.Tests;

public class CalculoTests
{
    [Fact]
    public void Realizar_DevePreencherDadosECalcularResultado()
    {
        var criadoEm = new DateTimeOffset(2026, 10, 9, 12, 0, 0, TimeSpan.Zero);

        var calculo = Calculo.Realizar(10, 4, Operacao.Subtracao, new ServicoDeCalculo(), criadoEm);

        Assert.NotEqual(Guid.Empty, calculo.Id);
        Assert.Equal(10, calculo.OperandoA);
        Assert.Equal(4, calculo.OperandoB);
        Assert.Equal(Operacao.Subtracao, calculo.Operacao);
        Assert.Equal(6, calculo.Resultado);
        Assert.Equal(criadoEm, calculo.CriadoEm);
    }
}
