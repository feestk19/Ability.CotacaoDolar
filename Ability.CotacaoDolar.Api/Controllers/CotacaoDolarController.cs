using Ability.CotacaoDolar.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ability.CotacaoDolar.Api.Controllers;

[Route("api/cotacao-dolar")]
[ApiController]
public class CotacaoDolarController : ControllerBase
{
    private readonly ServicoConsultaCotacaoDolar _servicoConsultaCotacaoDolar;

    public CotacaoDolarController(ServicoConsultaCotacaoDolar servicoConsultaCotacaoDolar)
    {
        _servicoConsultaCotacaoDolar = servicoConsultaCotacaoDolar;
    }

    /// <summary>
    /// Obtém a última cotação do dólar registrada no banco de dados.
    /// </summary>
    /// <returns> A última cotação do dólar. </returns>
    [HttpGet("ultima")]
    public async Task<IActionResult> ObterUltimaCotacao()
    {
        var cotacao = await _servicoConsultaCotacaoDolar.ObterUltimaCotacaoAsync();

        if (cotacao == null)
            return NotFound("Nenhuma cotação encontrada.");

        return Ok(cotacao);
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
        var historico = await _servicoConsultaCotacaoDolar.ObterHistoricoAsync(dataInicial, dataFinal);

        return Ok(historico);   
    } 
}
