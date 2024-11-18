sudo docker run -d -e POSTGRES_USER=root -e POSTGRES_PASSWORD=pgadminchoice -e POSTGRES_DB=Choice -p 5432:5432 --name db postgres

cd ~/Projects/Choice/Backend/src/Database/DatabaseMigrator

dotnet run "User Id=root;Password=pgadminchoice;Database=Choice;Host=localhost;Port=5432;" "../Structure"

dotnet run "User Id=root;Password=pgadminchoice;Database=Choice;Host=localhost;Port=5432;" "../Scripts/SeedData"

cd ../..

dotnet ef database update --project Modules/Identity/Infrastructure/Infrastructure.csproj --startup-project WebApi/WebApi.csproj