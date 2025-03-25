cd DatabaseMigrator

dotnet run "<ConnectionString>" "../Structure"

dotnet run "<ConnectionString>"

cd ../..

dotnet ef database update --project Modules/Identity/Infrastructure/Infrastructure.csproj --startup-project WebApi/WebApi.csproj -- "<ConnectionString>" --environment Production
