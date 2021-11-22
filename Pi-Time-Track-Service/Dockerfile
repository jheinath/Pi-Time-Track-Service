FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Pi-Time-Track-Service/Pi-Time-Track-Service.csproj", "Pi-Time-Track-Service/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Adapters.Hangfire/Adapters.Hangfire.csproj", "Adapters.Hangfire/"]
COPY ["Adapters.Database/Adapters.Database.csproj", "Adapters.Database/"]
RUN dotnet restore "Pi-Time-Track-Service/Pi-Time-Track-Service.csproj"
COPY . .
WORKDIR "/src/Pi-Time-Track-Service"
RUN dotnet build "Pi-Time-Track-Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pi-Time-Track-Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pi-Time-Track-Service.dll"]