sudo openssl req -x509 -newkey rsa:2048 -keyout ../.docker/https/localhost.key -out ../.docker/https/localhost.crt -days 365 -subj "/CN=choice.local/O=choice.local/C=US" -config ./ssl-selfsigned.cnf -passout pass:MyCertificatePassword
sudo openssl pkcs12 -export -out ../.docker/https/localhost.pfx -inkey ../.docker/https/localhost.key -in ../.docker/https/localhost.crt -name "Localhost selfsigned certificate" -password pass:MyCertificatePassword -passin pass:MyCertificatePassword
sudo openssl rsa -in ../.docker/https/localhost.key -out ../.docker/https/localhost.key -passin pass:MyCertificatePassword

sudo cp ../.docker/https/localhost.crt /usr/local/share/ca-certificates/

sudo chown -R 1000:1000 ../.docker/https
sudo chmod -R 755 ../.docker/https


sudo cat << EOF >> sudo /etc/hosts
EOF

sudo docker compose -f docker-compose.yml -f docker-compose.override.yml up -d

../src/Database/migrate.sh

sudo docker compose start webapi
