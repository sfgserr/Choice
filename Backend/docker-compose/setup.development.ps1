docker compose -f docker-compose.yml -f docker-compose.override.yml up -d

../src/Database/migrate.ps1

cd $env:CHOICE_HOME\Backend\docker-compose && docker compose start webapi
