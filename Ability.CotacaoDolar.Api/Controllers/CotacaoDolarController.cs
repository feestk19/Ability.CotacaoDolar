using Ability.CotacaoDolar.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog.Context;

namespace Ability.CotacaoDolar.Api.Controllers;

[Route("api/cotacao-dolar")]
[ApiController]
public class CotacaoDolarController : ControllerBase
{
    private readonly ServicoConsultaCotacaoDolar _servicoConsultaCotacaoDolar;
    private readonly ILogger<CotacaoDolarController> _logger;

    public CotacaoDolarController(ServicoConsultaCotacaoDolar servicoConsultaCotacaoDolar, ILogger<CotacaoDolarController> logger)
    {
        _servicoConsultaCotacaoDolar = servicoConsultaCotacaoDolar;
        _logger = logger;
    }

    /// <summary>
    /// Obtém a última cotação do dólar registrada no banco de dados.
    /// </summary>
    /// <returns> A última cotação do dólar. </returns>
    [HttpGet("ultima")]
    public async Task<IActionResult> ObterUltimaCotacao()
    {
        using (LogContext.PushProperty("Source", "Ability.CotacaoDolar.Api"))
        {
            _logger.LogInformation("Requisição recebida para obter a última cotação do dólar.");

            var cotacao = await _servicoConsultaCotacaoDolar.ObterUltimaCotacaoAsync();

            if (cotacao == null)
            {
                _logger.LogWarning("Nenhuma cotação encontrada para a consulta da última cotação.");
                return NotFound("Nenhuma cotação encontrada.");
            }

            _logger.LogInformation(
                "Última cotação retornada com sucesso. DataHoraColeta: {DataHoraColeta}",
                cotacao.DataHoraColeta);

            return Ok(cotacao);
        }
    }

    /// <summary>
    /// Obtém o histórico de cotações do dólar dentro de um intervalo de datas especificado. Se nenhuma data for fornecida, retorna todo o histórico disponível.
    /// </summary>
    /// <param name="dataInicial">Data inicial do intervalo de consulta.</param>
    /// <param name="dataFinal">Data final do intervalo de consulta.</param>
    /// <returns>O histórico de cotações do dólar dentro do intervalo especificado.</returns>
    [HttpGet("historico")]
    public async Task<IActionResult> ObterHistorico([FromQuery] DateTime? dataInicial, [FromQuery] DateTime? dataFinal)
    {
        using (LogContext.PushProperty("Source", "Ability.CotacaoDolar.Api"))
        {
            _logger.LogInformation("Requisição recebida para obter histórico de cotações. DataInicial: {DataInicial}, DataFinal: {DataFinal}",dataInicial, dataFinal);
            var historico = await _servicoConsultaCotacaoDolar.ObterHistoricoAsync(dataInicial, dataFinal);

            var TotalRegistros = historico.Count();
            _logger.LogInformation("Histórico de cotações retornado com sucesso. Total de registros: {TotalRegistros}", TotalRegistros);

            return Ok(historico);
        }
    } 
}
