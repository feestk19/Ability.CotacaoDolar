using System.Text.Json.Serialization;

namespace Ability.CotacaoDolar.Infrastructure.ExternalServices.BancoCentral;

/// <summary>
/// Classe que representa a resposta da API do Banco Central para a cotação do dólar. A resposta é composta por uma lista de objetos, cada um contendo as taxas de compra e venda do dólar. 
/// </summary>
public class CotacaoDolarBancoCentralResponse
{
    [JsonPropertyName("value")]
    public List<CotacaoDolarBancoCentralItemResponse> Value { get; set; } = [];
}

/// <summary>
/// Classe que representa um item da resposta da API do Banco Central para a cotação do dólar, contendo as taxas de compra e venda do dólar. Cada item corresponde a uma cotação específica, com as taxas de compra e venda associadas.
/// </summary>
public class CotacaoDolarBancoCentralItemResponse
{
    [JsonPropertyName("cotacaoCompra")]
    public decimal CotacaoCompra { get; set; }

    [JsonPropertyName("cotacaoVenda")]
    public decimal CotacaoVenda { get; set; }

    [JsonPropertyName("dataHoraCotacao")]
    public string DataHoraCotacao { get; set; } = string.Empty;
}
