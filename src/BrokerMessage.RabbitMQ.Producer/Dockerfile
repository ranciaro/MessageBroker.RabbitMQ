#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/BrokerMessage.RabbitMQ.Producer/BrokerMessage.RabbitMQ.Producer.csproj", "src/BrokerMessage.RabbitMQ.Producer/"]
COPY ["src/BrokerMessage.RabbitMQ.Core/BrokerMessage.RabbitMQ.Core.csproj", "src/BrokerMessage.RabbitMQ.Core/"]
RUN dotnet restore "src/BrokerMessage.RabbitMQ.Producer/BrokerMessage.RabbitMQ.Producer.csproj"
COPY . .
WORKDIR "/src/src/BrokerMessage.RabbitMQ.Producer"
RUN dotnet build "BrokerMessage.RabbitMQ.Producer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BrokerMessage.RabbitMQ.Producer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BrokerMessage.RabbitMQ.Producer.dll"]