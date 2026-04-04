FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Ability.CotacaoDolar.Api/Ability.CotacaoDolar.Api.csproj", "Ability.CotacaoDolar.Api/"]
COPY ["Ability.CotacaoDolar.Infrastructure/Ability.CotacaoDolar.Infrastructure.csproj", "Ability.CotacaoDolar.Infrastructure/"]
COPY ["Ability.CotacaoDolar.Core/Ability.CotacaoDolar.Core.csproj", "Ability.CotacaoDolar.Core/"]

RUN dotnet restore "./Ability.CotacaoDolar.Api/Ability.CotacaoDolar.Api.csproj"

COPY . .
WORKDIR "/src/Ability.CotacaoDolar.Api"
RUN dotnet publish "./Ability.CotacaoDolar.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "Ability.CotacaoDolar.Api.dll"]