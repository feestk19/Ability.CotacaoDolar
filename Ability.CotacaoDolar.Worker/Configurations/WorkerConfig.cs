namespace Ability.CotacaoDolar.Worker.Configurations;

/// <summary>
/// Classe que representa as configurações do Worker, contendo propriedades que podem ser configuradas no arquivo de configuração (appsettings.json) para controlar o comportamento do serviço em segundo plano, como o intervalo de tempo entre as coletas da cotação do dólar.
/// </summary>
public class WorkerConfig
{
    /// <summary>
    /// Propriedade que define o intervalo de tempo, em minutos, entre as coletas da cotação do dólar. O valor pode ser configurado no arquivo de configuração (appsettings.json) e é utilizado para determinar a frequência com que o serviço em segundo plano irá coletar e salvar as cotações do dólar. Se o valor for menor ou igual a zero, o intervalo padrão de 10 minutos será utilizado.
    /// </summary>
    public int IntervaloMinutos { get; set; }
}
