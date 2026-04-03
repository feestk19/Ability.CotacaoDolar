using Ability.CotacaoDolar.Core.Entities;
using Ability.CotacaoDolar.Core.Interfaces;
using Ability.CotacaoDolar.Core.Services;

namespace Ability.CotacaoDolar.Worker
{
    public class Worker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;

        public Worker(IServiceProvider serviceProvider, ILogger<Worker> logger, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();

                    var servico = scope.ServiceProvider.GetRequiredService<ServicoColetaCotacaoDolar>();
                    await servico.ColetarESalvarAsync();

                    _logger.LogInformation("Cotação do dólar coletada e salva com sucesso.");

                }
                catch (Exception ex)
                {

                    _logger.LogError($"Erro ao coletar cotação: {ex}");
                }

                var minutos = _configuration.GetValue<int>("Worker:IntervaloMinutos");
                await Task.Delay(TimeSpan.FromMinutes(minutos), stoppingToken);
            }
            
        }
    }
}
