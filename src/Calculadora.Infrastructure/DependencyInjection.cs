using Calculadora.Domain.Repositories;
using Calculadora.Infrastructure.Persistence;
using Calculadora.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Calculadora.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Calculadora")
            ?? throw new InvalidOperationException("Connection string 'Calculadora' não configurada.");

        services.AddDbContext<CalculadoraDbContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped<ICalculoRepository, CalculoRepository>();

        return services;
    }
}
