#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["services/company-service/CompanyService.Api.csproj", "services/company-service/"]
COPY ["services/common/Application/Application.csproj", "services/common/Application/"]
COPY ["services/common/Common/Common.csproj", "services/common/Common/"]
COPY ["services/common/Infrastructure/Infrastructure.csproj", "services/common/Infrastructure/"]
COPY ["services/event-bus-messages/EventBus.Messages.csproj", "services/event-bus-messages/"]
RUN dotnet restore "services/company-service/CompanyService.Api.csproj"
COPY . .
WORKDIR "/src/services/company-service"
RUN dotnet build "CompanyService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CompanyService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CompanyService.Api.dll"]