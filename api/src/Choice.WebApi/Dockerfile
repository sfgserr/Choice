#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Choice.WebApi/Choice.WebApi.csproj", "src/Choice.WebApi/"]
COPY ["src/Choice.Domain/Choice.Domain.csproj", "src/Choice.Domain/"]
COPY ["src/Choice.Application/Choice.Application.csproj", "src/Choice.Application/"]
COPY ["src/Choice.Infrastructure/Choice.Infrastructure.csproj", "src/Choice.Infrastructure/"]
RUN dotnet restore "src/Choice.WebApi/Choice.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Choice.WebApi"
RUN dotnet build "Choice.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Choice.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Choice.WebApi.dll"]