using Calculadora.Domain.Enums;

namespace Calculadora.Application.Dtos;

public record CalcularRequest(decimal OperandoA, decimal OperandoB, Operacao Operacao);
