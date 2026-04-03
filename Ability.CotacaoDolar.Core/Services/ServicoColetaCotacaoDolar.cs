using Ability.CotacaoDolar.Core.Interfaces;

namespace Ability.CotacaoDolar.Core.Services;

/// <summary>
/// Classe que representa o serviço de coleta da cotação do dólar, responsável por obter as taxas de compra e venda do dólar a partir de uma fonte externa, como uma API de um banco central ou outro serviço de dados financeiros.
/// </summary>
public class ServicoColetaCotacaoDolar
{
    private readonly ICotacaoDolarColetor _cotacaoDolarColetor;
    private readonly ICotacaoDolarRepositorio _cotacaoDolarRepositorio;

    public ServicoColetaCotacaoDolar(ICotacaoDolarColetor cotacaoDolarColetor, ICotacaoDolarRepositorio cotacaoDolarRepositorio)
    {
        _cotacaoDolarColetor = cotacaoDolarColetor;
        _cotacaoDolarRepositorio = cotacaoDolarRepositorio;
    }

    public async Task ColetarESalvarAsync()
    {
        var cotacao = await _cotacaoDolarColetor.ColetarCotacaoDolarAsync();

        await _cotacaoDolarRepositorio.SalvarAsync(cotacao);
    }


}
