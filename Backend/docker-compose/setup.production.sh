sudo openssl req -x509 -newkey rsa:2048 -keyout /etc/ssl/choice/signing.key -out /etc/ssl/choice/signing.crt -days 365 -subj "/CN=Choice Signing Certificate"

sudo openssl rsa -in /etc/ssl/choice/signing.key -out /etc/ssl/choice/signing.key

../src/Database/migrate.production.sh

sudo docker compose -f docker-compose.yml -f docker-compose.production.yml up -d


