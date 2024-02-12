#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["api/src/Choice.WebApi/Choice.WebApi.csproj", "api/src/Choice.WebApi/"]
COPY ["eventbus-messages/EventBus.Messages/EventBus.Messages.csproj", "eventbus-messages/EventBus.Messages/"]
COPY ["api/src/Choice.Application/Choice.Application.csproj", "api/src/Choice.Application/"]
COPY ["api/src/Choice.Domain/Choice.Domain.csproj", "api/src/Choice.Domain/"]
COPY ["api/src/Choice.Infrastructure/Choice.Infrastructure.csproj", "api/src/Choice.Infrastructure/"]
RUN dotnet restore "api/src/Choice.WebApi/Choice.WebApi.csproj"
COPY . .
WORKDIR "/src/api/src/Choice.WebApi"
RUN dotnet build "Choice.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Choice.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Choice.WebApi.dll"]