using Ability.CotacaoDolar.Core.Entities;
using Ability.CotacaoDolar.Core.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System.Globalization;
using System.Text.Json;

namespace Ability.CotacaoDolar.Infrastructure.ExternalServices.BancoCentral;

/// <summary>
/// Classe que implementa o coletor de cotação do dólar para o Banco Central, responsável por obter as taxas de compra e venda do dólar a partir da API do Banco Central.
/// </summary>
public class CotacaoDolarBancoCentralColetor : ICotacaoDolarColetor
{
    private readonly HttpClient _httpClient;

    public CotacaoDolarBancoCentralColetor(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Método que coleta a cotação do dólar do Banco Central, fazendo uma requisição HTTP para a API do Banco Central e retornando um objeto RegistroCotacaoDolar com as taxas de compra e venda do dólar. Caso haja algum erro na requisição ou na deserialização dos dados, uma exceção é lançada.
    /// </summary>
    /// <returns>Objeto RegistroCotacaoDolar contendo as taxas de compra e venda do dólar.</returns>
    /// <exception cref="Exception">Exceção lançada quando não é possível obter a cotação do dólar do Banco Central.</exception>
    public async Task<RegistroCotacaoDolar> ColetarCotacaoDolarAsync()
    {
        var data = DateTime.Now;

        //Caso o Worker rode em finais de semana ou feriados, o Banco Central não terá a cotação do dia, então vamos tentar obter a cotação dos últimos 5 dias, caso não encontre, lançamos uma exceção.
        for (int i = 0; i < 5; i++)
        {
            var dataFormatada = data.ToString("MM-dd-yyyy");

            var url =
            $"https://olinda.bcb.gov.br/olinda/servico/PTAX/versao/v1/odata/" +
            $"CotacaoDolarDia(dataCotacao=@dataCotacao)?@dataCotacao='{dataFormatada}'&$top=1&$format=json";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var jsonRetorno = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var dados = JsonSerializer.Deserialize<CotacaoDolarBancoCentralResponse>(jsonRetorno, options);

            if (dados?.Value?.Any() == true)
            {
                var cotacao = dados.Value.First();    

                return new RegistroCotacaoDolar
                {
                    TaxaCompra = cotacao.CotacaoCompra,
                    TaxaVenda = cotacao.CotacaoVenda,
                    DataHoraColeta = DateTime.Parse(cotacao.DataHoraCotacao),
                    DataHoraCriacao = DateTime.UtcNow
                };
            }

            //volta um dia
            data = data.AddDays(-1);
        }
        
        throw new Exception("Não foi possível obter cotação nos últimos dias.");
    }
}
