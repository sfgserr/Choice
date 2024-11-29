FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/WebApi/WebApi.csproj", "src/WebApi/"]
COPY ["src/BuildingBlocks/Infrastructure/Infrastructure.csproj", "src/BuildingBlocks/Infrastructure/"]
COPY ["src/BuildingBlocks/Application/Application.csproj", "src/BuildingBlocks/Application/"]
COPY ["src/BuildingBlocks/Domain/Domain.csproj", "src/BuildingBlocks/Domain/"]
COPY ["src/Modules/Administration/Infrastructure/Infrastructure.csproj", "src/Modules/Administration/Infrastructure/"]
COPY ["src/Modules/Administration/Application/Application.csproj", "src/Modules/Administration/Application/"]
COPY ["src/Modules/Administration/Domain/Domain.csproj", "src/Modules/Administration/Domain/"]
COPY ["src/Modules/Administration/IntegrationEvents/IntegrationEvents.csproj", "src/Modules/Administration/IntegrationEvents/"]
COPY ["src/Modules/Chat/Infrastructure/Infrastructure.csproj", "src/Modules/Chat/Infrastructure/"]
COPY ["src/Modules/Users/Infrastructure/Infrastructure.csproj", "src/Modules/Users/Infrastructure/"]
COPY ["src/Modules/Payments/IntegrationEvents/IntegrationEvents.csproj", "src/Modules/Payments/IntegrationEvents/"]
COPY ["src/Modules/Users/Application/Application.csproj", "src/Modules/Users/Application/"]
COPY ["src/Modules/Users/Domain/Domain.csproj", "src/Modules/Users/Domain/"]
COPY ["src/Modules/Users/IntegrationEvents/IntegrationEvents.csproj", "src/Modules/Users/IntegrationEvents/"]
COPY ["src/Modules/Chat/Application/Application.csproj", "src/Modules/Chat/Application/"]
COPY ["src/Modules/Chat/Domain/Domain.csproj", "src/Modules/Chat/Domain/"]
COPY ["src/Modules/Identity/Application/Application.csproj", "src/Modules/Identity/Application/"]
COPY ["src/Modules/Identity/Domain/Domain.csproj", "src/Modules/Identity/Domain/"]
COPY ["src/Modules/Identity/Infrastructure/Infrastructure.csproj", "src/Modules/Identity/Infrastructure/"]
COPY ["src/Modules/Payments/Infrastructure/Infrastructure.csproj", "src/Modules/Payments/Infrastructure/"]
COPY ["src/Modules/Payments/Domain/Domain.csproj", "src/Modules/Payments/Domain/"]
COPY ["src/Modules/Payments/Application/Application.csproj", "src/Modules/Payments/Application/"]
RUN dotnet restore "src/WebApi/WebApi.csproj"
COPY . .
WORKDIR "/src/src/WebApi"
RUN dotnet build "WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]
