#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["services/client-service/src/ClientService.Api/ClientService.Api.csproj", "services/client-service/src/ClientService.Api/"]
COPY ["services/event-bus-messages/EventBus.Messages.csproj", "services/event-bus-messages/"]
COPY ["services/client-service/src/ClientService.Application/ClientService.Application.csproj", "services/client-service/src/ClientService.Application/"]
COPY ["services/client-service/src/ClientService.Domain/ClientService.Domain.csproj", "services/client-service/src/ClientService.Domain/"]
COPY ["services/client-service/src/ClientService.Infrastructure/ClientService.Infrastructure.csproj", "services/client-service/src/ClientService.Infrastructure/"]
RUN dotnet restore "services/client-service/src/ClientService.Api/ClientService.Api.csproj"
COPY . .
WORKDIR "/src/services/client-service/src/ClientService.Api"
RUN dotnet build "ClientService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ClientService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClientService.Api.dll"]