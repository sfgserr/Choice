sudo docker compose -f docker-compose.yml -f docker-compose.override.yml up -d

../src/Database/migrate.sh

sudo docker compose start webapi
