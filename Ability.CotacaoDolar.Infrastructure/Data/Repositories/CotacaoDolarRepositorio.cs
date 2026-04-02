using Ability.CotacaoDolar.Core.Entities;
using Ability.CotacaoDolar.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ability.CotacaoDolar.Infrastructure.Data.Repositories
{
    /// <summary>
    /// Classe que implementa a interface ICotacaoDolarRepositorio, responsável por fornecer métodos para salvar e recuperar cotações do dólar em um banco de dados ou outra forma de persistência.
    /// </summary>
    public class CotacaoDolarRepositorio : ICotacaoDolarRepositorio
    {
        private readonly ApplicationDbContext _context;

        public CotacaoDolarRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SalvarAsync(RegistroCotacaoDolar cotacao)
        {
            if (cotacao.DataHoraCriacao == default)
                cotacao.DataHoraCriacao = DateTime.UtcNow;

            await _context.CotacoesDolar.AddAsync(cotacao);
            await _context.SaveChangesAsync();
        }

        public async Task<RegistroCotacaoDolar?> ObterUltimaCotacaoAsync()
        {
            return await _context.CotacoesDolar
                .AsNoTracking()
                .OrderByDescending(c => c.DataHoraColeta)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<RegistroCotacaoDolar>> ObterHistoricoAsync(DateTime? dataInicial = null, DateTime? dataFinal = null)
        {
            var query = _context.CotacoesDolar
                .AsNoTracking()
                .AsQueryable();

            if (dataInicial.HasValue)
                query = query.Where(c => c.DataHoraColeta >= dataInicial.Value);

            if (dataFinal.HasValue)
                query = query.Where(c => c.DataHoraColeta <= dataFinal.Value);

            return await query
                .OrderByDescending(c => c.DataHoraColeta)
                .ToListAsync();
        }

    }
}
