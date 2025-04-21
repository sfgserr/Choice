openssl req -x509 -newkey rsa:2048 -keyout ../nginx/private_key.pem -out ../nginx/certificate.pem -days 365 -config ./ssl-selfsigned.cnf -passout pass:Pass123
openssl rsa -in ../nginx/private_key.pem -out ../nginx/private_key.pem -passin pass:Pass123

docker compose -f docker-compose.yml -f docker-compose.override.yml up -d

../src/Database/migrate.development.ps1

cd $env:CHOICE_HOME\Backend\docker-compose && docker compose start webapi
