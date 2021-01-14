#!/bin/bash

cd ~/.ssh
sudo service ssh start 
echo -e "\n\n\n" | ssh-keygen 
cat id_rsa.pub >> authorized_keys
chmod 640 authorized_keys
sudo service ssh restart

echo
echo "Install SSH finnish"
