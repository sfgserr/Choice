export $CHOICE_HOME=<CHOICE_HOME>

sudo openssl req -x509 -newkey rsa:2048 -keyout /etc/ssl/choice/signing.key -out /etc/ssl/choice/signing.crt -days 365 -subj "/CN=Choice Signing Certificate" -passout pass:<CertPassword>

sudo openssl rsa -in /etc/ssl/choice/signing.key -out /etc/ssl/choice/signing.key -passin pass:<CertPassword>

openssl pkcs12 -export -out /etc/ssl/choice/signing.pfx \
    -inkey /etc/ssl/choice/signing.key \
    -in /etc/ssl/choice/signing.crt \
    -password pass:<CertPassword> \
    -passin pass:<CertPassword>

chmod -R 777 /etc/ssl/choice

$CHOICE_HOME/Backend/src/Database/migrate.production.sh

sudo docker compose -f docker-compose.yml -f docker-compose.production.yml up -d


