#!/bin/bash

mkdir -p sand/ngrok
cd sand/ngrok

wget -c -O ngrok  https://github.com/Bitblazers-lk/Overseer-Extras/raw/ngrok/ngrok
wget -c -O ngrok.yml  https://github.com/Bitblazers-lk/Overseer-Extras/raw/ngrok/ngrok.yml

touch auth.yml

chmod +x ngrok

echo Setup ngrok completed
