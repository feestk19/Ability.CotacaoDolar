using Ability.CotacaoDolar.Core.Interfaces;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace Ability.CotacaoDolar.Core.Services;

/// <summary>
/// Classe que representa o serviço de coleta da cotação do dólar, responsável por obter as taxas de compra e venda do dólar a partir de uma fonte externa, como uma API de um banco central ou outro serviço de dados financeiros.
/// </summary>
public class ServicoColetaCotacaoDolar
{
    private readonly ICotacaoDolarColetor _cotacaoDolarColetor;
    private readonly ICotacaoDolarRepositorio _cotacaoDolarRepositorio;
    private readonly ILogger<ServicoColetaCotacaoDolar> _logger;

    public ServicoColetaCotacaoDolar(ICotacaoDolarColetor cotacaoDolarColetor, ICotacaoDolarRepositorio cotacaoDolarRepositorio, ILogger<ServicoColetaCotacaoDolar> logger)
    {
        _cotacaoDolarColetor = cotacaoDolarColetor;
        _cotacaoDolarRepositorio = cotacaoDolarRepositorio;
        _logger = logger;
    }

    public async Task ColetarESalvarAsync()
    {
        using (LogContext.PushProperty("Source", "Worker"))
        {
            var cotacao = await _cotacaoDolarColetor.ColetarCotacaoDolarAsync();

            var ultimaCotacao = await _cotacaoDolarRepositorio.ObterUltimaCotacaoAsync();

            if (ultimaCotacao is not null && ultimaCotacao.DataHoraColeta == cotacao.DataHoraColeta)
            {
                _logger.LogWarning(
                    "Cotação ignorada por duplicidade. DataHoraColeta: {DataHoraColeta}",
                    cotacao.DataHoraColeta);

                return;
            }

            await _cotacaoDolarRepositorio.SalvarAsync(cotacao);

            _logger.LogInformation(
                "Cotação salva com sucesso | Compra: {Compra} | Venda: {Venda} | Coleta: {DataHora}",
                cotacao.TaxaCompra,
                cotacao.TaxaVenda,
                cotacao.DataHoraColeta);
        }
    }
}
