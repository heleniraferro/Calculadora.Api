using Calculadora.Domain.Enums;
using Calculadora.Domain.Exceptions;

namespace Calculadora.Domain.Services;

public class ServicoDeCalculo
{
    public decimal Executar(decimal operandoA, decimal operandoB, Operacao operacao)
    {
        try
        {
            return operacao switch
            {
                Operacao.Soma => operandoA + operandoB,
                Operacao.Subtracao => operandoA - operandoB,
                Operacao.Multiplicacao => operandoA * operandoB,
                Operacao.Divisao => Dividir(operandoA, operandoB),
                _ => throw new DominioException("Operação inválida.")
            };
        }
        catch (OverflowException)
        {
            throw new DominioException("O resultado excede o limite suportado.");
        }
    }

    private static decimal Dividir(decimal dividendo, decimal divisor)
    {
        if (divisor == 0)
            throw new DominioException("Não é possível dividir por zero.");

        return dividendo / divisor;
    }
}
