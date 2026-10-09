namespace Calculadora.Domain.Exceptions;

public class DominioException : Exception
{
    public DominioException(string mensagem) : base(mensagem)
    {
    }
}
