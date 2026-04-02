using Ability.CotacaoDolar.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ability.CotacaoDolar.Infrastructure.Data;

/// <summary>
/// Classe que representa o contexto do banco de dados para a aplicação, utilizando Entity Framework Core. Esta classe é responsável por configurar as entidades e as relações do banco de dados, bem como fornecer acesso aos conjuntos de dados (DbSet) para as cotações do dólar.
/// </summary>
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<RegistroCotacaoDolar> CotacoesDolar { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
