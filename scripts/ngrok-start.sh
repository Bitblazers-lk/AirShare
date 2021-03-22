#!/bin/bash

mkdir -p sand/ngrok
cd sand/ngrok

pkill ngrok
./ngrok http 36120 --log=stdout -config ngrok.yml -config auth.yml

echo Started ngrok
