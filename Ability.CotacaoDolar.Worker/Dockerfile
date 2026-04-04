FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Ability.CotacaoDolar.Worker/Ability.CotacaoDolar.Worker.csproj", "Ability.CotacaoDolar.Worker/"]
COPY ["Ability.CotacaoDolar.Core/Ability.CotacaoDolar.Core.csproj", "Ability.CotacaoDolar.Core/"]
COPY ["Ability.CotacaoDolar.Infrastructure/Ability.CotacaoDolar.Infrastructure.csproj", "Ability.CotacaoDolar.Infrastructure/"]

RUN dotnet restore "Ability.CotacaoDolar.Worker/Ability.CotacaoDolar.Worker.csproj"

COPY . .
WORKDIR "/src/Ability.CotacaoDolar.Worker"
RUN dotnet publish "Ability.CotacaoDolar.Worker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Ability.CotacaoDolar.Worker.dll"]