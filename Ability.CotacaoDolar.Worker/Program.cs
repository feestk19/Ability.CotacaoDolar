using Ability.CotacaoDolar.Infrastructure;
using Ability.CotacaoDolar.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddInfrastructure(builder.Configuration);

var host = builder.Build();
host.Run();
