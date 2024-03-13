#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["services/ordering/src/Ordering.Grpc/Ordering.Grpc.csproj", "services/ordering/src/Ordering.Grpc/"]
COPY ["services/ordering/src/Ordering.Application/Ordering.Application.csproj", "services/ordering/src/Ordering.Application/"]
COPY ["services/common/Application/Application.csproj", "services/common/Application/"]
COPY ["services/ordering/src/Ordering.Domain/Ordering.Domain.csproj", "services/ordering/src/Ordering.Domain/"]
COPY ["services/common/Common/Common.csproj", "services/common/Common/"]
COPY ["services/ordering/src/Ordering.Infrastructure/Ordering.Infrastructure.csproj", "services/ordering/src/Ordering.Infrastructure/"]
COPY ["services/common/Infrastructure/Infrastructure.csproj", "services/common/Infrastructure/"]
RUN dotnet restore "services/ordering/src/Ordering.Grpc/Ordering.Grpc.csproj"
COPY . .
WORKDIR "/src/services/ordering/src/Ordering.Grpc"
RUN dotnet build "Ordering.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ordering.Grpc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ordering.Grpc.dll"]