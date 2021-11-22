FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /src
COPY ["Pi-Time-Track-Service/Pi-Time-Track-Service.csproj", "Pi-Time-Track-Service/"]

RUN dotnet restore "Pi-Time-Track-Service/Pi-Time-Track-Service.csproj"

COPY . .

WORKDIR "/src/Pi-Time-Track-Service"

RUN dotnet build "Pi-Time-Track-Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pi-Time-Track-Service.csproj" -c Release -o /app/publish