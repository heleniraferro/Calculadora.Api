using Calculadora.Domain.Entities;

namespace Calculadora.Domain.Repositories;

public interface ICalculoRepository
{
    Task AdicionarAsync(Calculo calculo, CancellationToken cancellationToken);

    Task<IReadOnlyList<Calculo>> ListarRecentesAsync(int quantidade, CancellationToken cancellationToken);
}
