using Calculadora.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Calculadora.Infrastructure.Persistence.Configurations;

public class CalculoConfiguration : IEntityTypeConfiguration<Calculo>
{
    public void Configure(EntityTypeBuilder<Calculo> builder)
    {
        builder.ToTable("calculos");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("id");
        builder.Property(c => c.OperandoA).HasColumnName("operando_a").HasColumnType("numeric");
        builder.Property(c => c.OperandoB).HasColumnName("operando_b").HasColumnType("numeric");
        builder.Property(c => c.Operacao).HasColumnName("operacao").HasConversion<string>().HasMaxLength(20);
        builder.Property(c => c.Resultado).HasColumnName("resultado").HasColumnType("numeric");
        builder.Property(c => c.CriadoEm).HasColumnName("criado_em");

        builder.HasIndex(c => c.CriadoEm);
    }
}
