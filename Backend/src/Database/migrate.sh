cd DatabaseMigrator

dotnet run "User Id=root;Password=pgadminchoice;Database=Choice;Host=localhost;Port=5432;" "../Structure"

dotnet run "User Id=root;Password=pgadminchoice;Database=Choice;Host=localhost;Port=5432;" "../Scripts/SeedData"

cd ../..

dotnet ef database update --project Modules/Identity/Infrastructure/Infrastructure.csproj --startup-project WebApi/WebApi.csproj
