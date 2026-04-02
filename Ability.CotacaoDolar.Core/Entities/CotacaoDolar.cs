namespace Ability.CotacaoDolar.Core.Entities;

/// <summary>
/// Representa a cotação do dólar, incluindo as taxas de compra e venda, bem como as datas de coleta e criação da cotação.
/// </summary>
public class RegistroCotacaoDolar
{
    /// <summary>
    /// Identificador único da cotação no sistema.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Taxa de compra do dólar informada pelo Banco Central na data de coleta.
    /// </summary>
    public decimal TaxaCompra { get; set; }
    /// <summary>
    /// Taxa de venda do dólar informada pelo Banco Central na data de coleta.
    /// </summary>
    public decimal TaxaVenda { get; set; }
    /// <summary>
    /// Data e hora referente à cotação informada pelo Banco Central.
    /// </summary>
    public DateTime DataHoraColeta { get; set; }
    /// <summary>
    /// Data e hora em que a cotação foi criada no sistema.
    /// </summary>
    public DateTime DataHoraCriacao { get; set; }
}
