using Calculadora.Domain.Enums;
using Calculadora.Domain.Services;

namespace Calculadora.Domain.Entities;

public class Calculo
{
    public Guid Id { get; private set; }
    public decimal OperandoA { get; private set; }
    public decimal OperandoB { get; private set; }
    public Operacao Operacao { get; private set; }
    public decimal Resultado { get; private set; }
    public DateTimeOffset CriadoEm { get; private set; }

    private Calculo()
    {
    }

    public static Calculo Realizar(
        decimal operandoA,
        decimal operandoB,
        Operacao operacao,
        ServicoDeCalculo servico,
        DateTimeOffset criadoEm)
    {
        return new Calculo
        {
            Id = Guid.NewGuid(),
            OperandoA = operandoA,
            OperandoB = operandoB,
            Operacao = operacao,
            Resultado = servico.Executar(operandoA, operandoB, operacao),
            CriadoEm = criadoEm
        };
    }
}
