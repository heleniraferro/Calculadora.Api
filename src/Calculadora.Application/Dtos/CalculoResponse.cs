using Calculadora.Domain.Entities;
using Calculadora.Domain.Enums;

namespace Calculadora.Application.Dtos;

public record CalculoResponse(
    Guid Id,
    decimal OperandoA,
    decimal OperandoB,
    Operacao Operacao,
    decimal Resultado,
    DateTimeOffset CriadoEm)
{
    public static CalculoResponse De(Calculo calculo) => new(
        calculo.Id,
        calculo.OperandoA,
        calculo.OperandoB,
        calculo.Operacao,
        calculo.Resultado,
        calculo.CriadoEm);
}
