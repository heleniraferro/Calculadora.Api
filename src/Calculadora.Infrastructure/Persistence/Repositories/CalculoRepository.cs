using Calculadora.Domain.Entities;
using Calculadora.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Calculadora.Infrastructure.Persistence.Repositories;

public class CalculoRepository : ICalculoRepository
{
    private readonly CalculadoraDbContext _contexto;

    public CalculoRepository(CalculadoraDbContext contexto)
    {
        _contexto = contexto;
    }

    public async Task AdicionarAsync(Calculo calculo, CancellationToken cancellationToken)
    {
        _contexto.Calculos.Add(calculo);
        await _contexto.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Calculo>> ListarRecentesAsync(int quantidade, CancellationToken cancellationToken)
    {
        return await _contexto.Calculos
            .AsNoTracking()
            .OrderByDescending(c => c.CriadoEm)
            .Take(quantidade)
            .ToListAsync(cancellationToken);
    }
}
