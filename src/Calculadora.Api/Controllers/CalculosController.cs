using Calculadora.Application.Dtos;
using Calculadora.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Calculadora.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CalculosController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType<CalculoResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Calcular(
        [FromBody] CalcularRequest request,
        [FromServices] ICalcularUseCase useCase,
        CancellationToken cancellationToken)
    {
        var resposta = await useCase.ExecutarAsync(request, cancellationToken);
        return Ok(resposta);
    }

    [HttpGet]
    [ProducesResponseType<IReadOnlyList<CalculoResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> ListarHistorico(
        [FromServices] IListarHistoricoUseCase useCase,
        CancellationToken cancellationToken,
        [FromQuery] int quantidade = 20)
    {
        var historico = await useCase.ExecutarAsync(quantidade, cancellationToken);
        return Ok(historico);
    }
}
