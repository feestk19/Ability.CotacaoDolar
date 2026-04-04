using Ability.CotacaoDolar.Core.Services;
using Ability.CotacaoDolar.Worker.Configurations;
using Microsoft.Extensions.Options;
using Serilog.Context;

namespace Ability.CotacaoDolar.Worker
{
    public class Worker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<Worker> _logger;
        private readonly WorkerConfig _config;

        public Worker(IServiceProvider serviceProvider, ILogger<Worker> logger, IConfiguration configuration, IOptions<WorkerConfig> options)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _config = options.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (LogContext.PushProperty("Source", "Worker"))
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        _logger.LogInformation("Iniciando coleta da cotação do dólar...");

                        using var scope = _serviceProvider.CreateScope();

                        var servico = scope.ServiceProvider.GetRequiredService<ServicoColetaCotacaoDolar>();
                        await servico.ColetarESalvarAsync();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Erro ao executar a coleta da cotação do dólar.");
                    }

                    var intervalo = _config.IntervaloMinutos <= 0 ? 60 : _config.IntervaloMinutos;
                    var delay = TimeSpan.FromMinutes(intervalo);

                    _logger.LogInformation("Aguardando {Intervalo} minuto(s) para próxima execução", intervalo);

                    await Task.Delay(delay, stoppingToken);
                }
            }
        }
    }
}
