source ./.env

sudo openssl req -x509 -newkey rsa:2048 -keyout /etc/ssl/choice/signing.key -out /etc/ssl/choice/signing.crt -days 365 -subj "/CN=Choice Signing Certificate" -passout pass:$CERTIFICATE_PASSWORD

sudo openssl rsa -in /etc/ssl/choice/signing.key -out /etc/ssl/choice/signing.key -passin pass:$CERTIFICATE_PASSWORD

openssl pkcs12 -export -out /etc/ssl/choice/signing.pfx \
    -inkey /etc/ssl/choice/signing.key \
    -in /etc/ssl/choice/signing.crt \
    -password pass:$CERTIFICATE_PASSWORD \
    -passin pass:$CERTIFICATE_PASSWORD

chmod -R 777 /etc/ssl/choice

$CHOICE_HOME/Backend/src/Database/migrate.production.sh

sudo docker compose -f docker-compose.yml -f docker-compose.production.yml up -d

rm -r /etc/letsencrypt/live/certfolder*

rm -rf ../nginx/certificate.pem
rm -rf ../nginx/private_key.pem

sudo certbot certonly --webroot -w /var/www/certbot -d $HOST_NAME -d www.$HOST_NAME -d $MINIO_HOST_NAME --cert-name=certfolder

cp /etc/letsencrypt/live/certfolder*/fullchain.pem ../nginx/certificate.pem
cp /etc/letsencrypt/live/certfolder*/privkey.pem ../nginx/private_key.pem
