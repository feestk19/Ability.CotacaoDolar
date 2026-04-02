using Ability.CotacaoDolar.Core.Entities;
using Ability.CotacaoDolar.Core.Interfaces;

namespace Ability.CotacaoDolar.Worker
{
    public class Worker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public Worker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var repositorio = scope.ServiceProvider.GetRequiredService<ICotacaoDolarRepositorio>();

            var cotacao = new RegistroCotacaoDolar
            {
                TaxaCompra = 5.10m,
                TaxaVenda = 5.20m,
                DataHoraColeta = DateTime.UtcNow,
                DataHoraCriacao = DateTime.UtcNow
            };

            await repositorio.SalvarAsync(cotacao);

            Console.WriteLine("Cotação salva com sucesso!");

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
    }
}
