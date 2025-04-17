sudo openssl req -x509 -newkey rsa:2048 -keyout /etc/ssl/choice/private_key.pem -out /etc/ssl/choice/certificate.pem -days 365 -subj "/CN=www.choice.ru choice.ru minio.choice.ru/O=choice/C=US" -passout pass:Pass123
sudo openssl rsa -in /etc/ssl/choice/private_key.pem -out /etc/ssl/choice/private_key.pem -passin pass:Pass123

sudo cp /etc/ssl/choice/certificate.pem ../nginx/certificate.pem
sudo cp /etc/ssl/choice/private_key.pem ../nginx/private_key.pem

sudo docker compose -f docker-compose.yml -f docker-compose.override.yml up -d

sudo ../src/Database/migrate.development.sh

sudo docker compose start webapi
