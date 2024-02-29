#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["services/ordering/src/Ordering.Api/Ordering.Api.csproj", "services/ordering/src/Ordering.Api/"]
COPY ["services/event-bus-messages/EventBus.Messages.csproj", "services/event-bus-messages/"]
COPY ["services/ordering/src/Ordering.Application/Ordering.Application.csproj", "services/ordering/src/Ordering.Application/"]
COPY ["services/ordering/src/Ordering.Domain/Ordering.Domain.csproj", "services/ordering/src/Ordering.Domain/"]
COPY ["services/ordering/src/Ordering.Infrastructure/Ordering.Infrastructure.csproj", "services/ordering/src/Ordering.Infrastructure/"]
RUN dotnet restore "services/ordering/src/Ordering.Api/Ordering.Api.csproj"
COPY . .
WORKDIR "/src/services/ordering/src/Ordering.Api"
RUN dotnet build "Ordering.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ordering.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ordering.Api.dll"]