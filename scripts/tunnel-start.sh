#!/bin/bash


FILE=./sand/ngrok/ngrok
if [ -f "$FILE" ]; then
    echo "Starting ngrok"
else 
    echo "Downloading ngrok"
    bash $1
fi

mkdir -p sand/ngrok
cd sand/ngrok

pkill ngrok
./ngrok http 36120 --log=stdout -config ngrok.yml -config auth.yml

echo Started ngrok
