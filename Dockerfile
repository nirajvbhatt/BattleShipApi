FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 2000
ENV ASPNETCORE_URLS=http://*:2000

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["BattleShipApi.csproj", "./"]
RUN dotnet restore "BattleShipApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "BattleShipApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BattleShipApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BattleShipApi.dll"]
