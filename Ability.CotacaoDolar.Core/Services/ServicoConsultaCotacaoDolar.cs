using Ability.CotacaoDolar.Core.Entities;
using Ability.CotacaoDolar.Core.Interfaces;

namespace Ability.CotacaoDolar.Core.Services;

/// <summary>
/// Classe que representa um serviço de consulta de cotação do dólar, responsável por fornecer métodos para obter a última cotação registrada e o histórico de cotações dentro de um intervalo de datas. Este serviço utiliza um repositório para acessar os dados de cotação do dólar armazenados em um banco de dados ou outra forma de persistência, permitindo que outras partes da aplicação, como controladores ou outros serviços, possam obter informações sobre as cotações do dólar de forma fácil e eficiente.
/// </summary>
public class ServicoConsultaCotacaoDolar
{
    private readonly ICotacaoDolarRepositorio _cotacaoDolarRepositorio;

    public ServicoConsultaCotacaoDolar(ICotacaoDolarRepositorio cotacaoDolarRepositorio)
    {
        _cotacaoDolarRepositorio = cotacaoDolarRepositorio;
    }

    /// <summary>
    /// Obtém a última cotação do dólar registrada no repositório. Se não houver cotações registradas, o método deve retornar null.
    /// </summary>
    /// <returns>A última cotação do dólar ou null se não houver cotações registradas.</returns>
    public async Task<RegistroCotacaoDolar?> ObterUltimaCotacaoAsync()
    {
        return await _cotacaoDolarRepositorio.ObterUltimaCotacaoAsync();
    }

    /// <summary>
    /// Obtém o histórico de cotações do dólar dentro de um intervalo de datas especificado. Se nenhuma data for fornecida, retorna todo o histórico disponível.
    /// </summary>
    /// <param name="dataInicial">Data inicial do intervalo de consulta.</param>
    /// <param name="dataFinal">Data final do intervalo de consulta.</param>
    /// <returns>Uma coleção de cotações do dólar dentro do intervalo especificado.</returns>
    public async Task<IEnumerable<RegistroCotacaoDolar>> ObterHistoricoAsync(DateTime? dataInicial = null, DateTime? dataFinal = null)
    {
        return await _cotacaoDolarRepositorio.ObterHistoricoAsync(dataInicial, dataFinal);
    }
}
