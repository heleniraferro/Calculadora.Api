using Calculadora.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Calculadora.Infrastructure.Persistence;

public class CalculadoraDbContext : DbContext
{
    public CalculadoraDbContext(DbContextOptions<CalculadoraDbContext> options) : base(options)
    {
    }

    public DbSet<Calculo> Calculos => Set<Calculo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CalculadoraDbContext).Assembly);
    }
}
