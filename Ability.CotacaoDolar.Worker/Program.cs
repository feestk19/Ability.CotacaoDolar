using Ability.CotacaoDolar.Infrastructure;
using Ability.CotacaoDolar.Worker;
using Ability.CotacaoDolar.Worker.Configurations;
using Serilog;

var builder = Host.CreateApplicationBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Services.AddSerilog();

builder.Services.Configure<WorkerConfig>
    (builder.Configuration.GetSection("Worker"));

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddHostedService<Worker>();

try
{
    var host = builder.Build();
    host.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "A aplicação Worker foi encerrada inesperadamente.");
}
finally
{
    Log.CloseAndFlush();
}