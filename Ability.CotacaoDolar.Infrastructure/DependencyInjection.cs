using Ability.CotacaoDolar.Core.Interfaces;
using Ability.CotacaoDolar.Core.Services;
using Ability.CotacaoDolar.Infrastructure.Data;
using Ability.CotacaoDolar.Infrastructure.Data.Repositories;
using Ability.CotacaoDolar.Infrastructure.ExternalServices.BancoCentral;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ability.CotacaoDolar.Infrastructure;

/// <summary>
/// Classe de extensão para configurar a injeção de dependências dos serviços relacionados à infraestrutura da aplicação, como o contexto do banco de dados e os repositórios. Esta classe é responsável por registrar os serviços necessários para a camada de infraestrutura, permitindo que eles sejam injetados em outras partes da aplicação, como controladores ou serviços de domínio.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Método que adiciona os serviços de infraestrutura à coleção de serviços da aplicação, configurando o contexto do banco de dados e os repositórios necessários para a persistência das cotações do dólar.
    /// </summary>
    /// <param name="services">A coleção de serviços da aplicação.</param>
    /// <param name="configuration">A configuração da aplicação.</param>
    /// <returns>A coleção de serviços atualizada.</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<ICotacaoDolarRepositorio, CotacaoDolarRepositorio>();
        services.AddScoped<ServicoColetaCotacaoDolar>();
        services.AddScoped<ServicoConsultaCotacaoDolar>();

        services.AddHttpClient<ICotacaoDolarColetor, CotacaoDolarBancoCentralColetor>();

        return services;
    }
}
