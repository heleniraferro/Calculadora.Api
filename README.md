# Calculadora.Api

API de uma calculadora básica (soma, subtração, multiplicação e divisão) em .NET 10, organizada com Clean Architecture e DDD. Guarda o histórico de cálculos no PostgreSQL.

Front-end: [Calculadora.Web](https://github.com/heleniraferro/Calculadora.Web)

## Arquitetura

```
src/
├─ Calculadora.Domain           Entidade Calculo, serviço de cálculo, regras e interface do repositório (sem dependências)
├─ Calculadora.Application      Casos de uso (Calcular, ListarHistorico) e DTOs
├─ Calculadora.Infrastructure   EF Core + Npgsql, mapeamento, repositório e migrations
└─ Calculadora.Api              Controllers, tratamento de erros, CORS e OpenAPI
tests/
├─ Calculadora.Domain.Tests
└─ Calculadora.Application.Tests
```

Dependências apontam para dentro: `Api → Application → Domain` e `Infrastructure → Application/Domain`.

## Endpoints

| Método | Rota | Descrição |
|---|---|---|
| POST | `/api/calculos` | `{ "operandoA": 10, "operandoB": 2, "operacao": "Divisao" }` → calcula e grava no histórico |
| GET | `/api/calculos?quantidade=20` | Lista o histórico, do mais recente para o mais antigo (máx. 100) |

Operações aceitas: `Soma`, `Subtracao`, `Multiplicacao`, `Divisao`. Divisão por zero retorna `400` (ProblemDetails).

## Como executar

Requisitos: .NET 10 SDK e Docker.

```bash
# 1. Banco de dados
cp .env.example .env        # ajuste a senha em .env
docker compose up -d

# 2. Connection string (fica fora do repositório)
dotnet user-secrets set "ConnectionStrings:Calculadora" "Host=localhost;Port=5432;Database=calculadora;Username=calculadora;Password=<SENHA_DO_.env>" --project src/Calculadora.Api

# 3. API (em Development as migrations são aplicadas automaticamente)
dotnet run --project src/Calculadora.Api --launch-profile https
```

A API sobe em `https://localhost:7100`. O documento OpenAPI fica em `/openapi/v1.json`.

## Testes

```bash
dotnet test
```

## Migrations

```bash
dotnet tool restore
dotnet dotnet-ef migrations add <Nome> --project src/Calculadora.Infrastructure --startup-project src/Calculadora.Api --output-dir Persistence/Migrations
```

## CORS

As origens do front ficam em `Cors:Origens` (`appsettings.json`).
