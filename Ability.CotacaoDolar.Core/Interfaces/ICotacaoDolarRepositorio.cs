using Ability.CotacaoDolar.Core.Entities;

namespace Ability.CotacaoDolar.Core.Interfaces;

/// <summary>
/// Interface que define o contrato para um repositório de cotação do dólar, responsável por armazenar e recuperar as cotações do dólar em um banco de dados ou outra forma de persistência. O repositório deve fornecer métodos para salvar novas cotações, bem como para consultar cotações anteriores com base em critérios como data e hora.
/// </summary>
public interface ICotacaoDolarRepositorio
{
    /// <summary>
    /// Salva uma nova cotação do dólar no repositório.
    /// </summary>
    /// <param name="cotacao"></param>
    /// <returns></returns>
    Task SalvarAsync(RegistroCotacaoDolar cotacao);

    /// <summary>
    /// Obtém a última cotação do dólar registrada no repositório. Se não houver cotações registradas, o método deve retornar null.
    /// </summary>
    /// <returns>A última cotação do dólar ou null se não houver cotações registradas.</returns>
    Task<RegistroCotacaoDolar?> ObterUltimaCotacaoAsync();

    /// <summary>
    /// Obtém o histórico de cotações do dólar dentro de um intervalo de datas especificado. Se nenhuma data for fornecida, retorna todo o histórico disponível.
    /// </summary>
    /// <param name="dataInicial">Data inicial do intervalo de consulta.</param>
    /// <param name="dataFinal">Data final do intervalo de consulta.</param>
    /// <returns>Uma coleção de cotações do dólar dentro do intervalo especificado.</returns>
    Task<IEnumerable<RegistroCotacaoDolar>> ObterHistoricoAsync(DateTime? dataInicial = null, DateTime? dataFinal = null);
}
