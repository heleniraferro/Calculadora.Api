using Calculadora.Application.UseCases;
using Calculadora.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Calculadora.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton(TimeProvider.System);
        services.AddSingleton<ServicoDeCalculo>();
        services.AddScoped<ICalcularUseCase, CalcularUseCase>();
        services.AddScoped<IListarHistoricoUseCase, ListarHistoricoUseCase>();

        return services;
    }
}
