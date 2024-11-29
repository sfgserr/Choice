sudo openssl req -x509 -newkey rsa:2048 -keyout /etc/ssl/choice/localhost.key -out /etc/ssl/choice/localhost.crt -days 365 -subj "/CN=choice.local/O=choice.local/C=US" -config ./ssl-selfsigned.cnf -passout pass:MyCertificatePassword
sudo openssl pkcs12 -export -out /etc/ssl/choice/localhost.pfx -inkey /etc/ssl/choice/localhost.key -in /etc/ssl/choice/localhost.crt -name "Localhost selfsigned certificate" -password pass:MyCertificatePassword -passin pass:MyCertificatePassword
sudo openssl rsa -in /etc/ssl/choice/localhost.key -out /etc/ssl/choice/localhost.key -passin pass:MyCertificatePassword

sudo cp /etc/ssl/choice/localhost.crt /usr/local/share/ca-certificates/


cat << EOF >> /etc/hosts
EOF
exit
