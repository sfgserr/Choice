cd $CHOICE_HOME/Backend/src/Database/DatabaseMigrator

dotnet run "<ConnectionString>" "../Structure"

dotnet run "<ConnectionString>" "../Scripts/SeedData"

dotnet ef database update --project $CHOICE_HOME/Backend/src/Modules/Identity/Infrastructure/Infrastructure.csproj --startup-project $CHOICE_HOME/Backend/src/WebApi/WebApi.csproj -- "<ConnectionString>" --environment Production
