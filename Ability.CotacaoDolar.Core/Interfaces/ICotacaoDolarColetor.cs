using Ability.CotacaoDolar.Core.Entities;

namespace Ability.CotacaoDolar.Core.Interfaces;

/// <summary>
/// Interface que define o contrato para um coletor de cotação do dólar, responsável por obter as taxas de compra e venda do dólar a partir de uma fonte de dados, como o Banco Central ou outras APIs financeiras.
/// </summary>
public interface ICotacaoDolarColetor
{
    /// <summary>
    /// Obtém a cotação do dólar, incluindo as taxas de compra e venda, a partir da fonte de dados configurada.
    /// </summary>
    /// <returns>A cotação do dólar.</returns>
    Task<RegistroCotacaoDolar> ColetarCotacaoDolarAsync();
}
