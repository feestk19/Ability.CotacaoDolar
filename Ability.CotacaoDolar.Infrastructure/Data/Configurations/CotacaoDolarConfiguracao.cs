using Ability.CotacaoDolar.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ability.CotacaoDolar.Infrastructure.Data.Configurations
{
    public class CotacaoDolarConfiguracao : IEntityTypeConfiguration<RegistroCotacaoDolar>
    {
        public void Configure(EntityTypeBuilder<RegistroCotacaoDolar> builder)
        {
            builder.ToTable("CotacoesDolar");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.TaxaCompra)
                .HasPrecision(18, 4)
                .IsRequired();

            builder.Property(c => c.TaxaVenda)
                .HasPrecision(18, 4)
                .IsRequired();

            builder.Property(c => c.DataHoraColeta)
                .IsRequired();

            builder.Property(c => c.DataHoraCriacao)
                .IsRequired();
        }
    }
}
