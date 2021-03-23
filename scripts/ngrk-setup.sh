#!/bin/bash


mkdir -p sand/ngrok
cd sand/ngrok

lockfile -r 0 the.lock || exit 1
# ------------------------------


wget -c -O ngrok  https://github.com/Bitblazers-lk/Overseer-Extras/raw/ngrok/ngrok

rm ngrok.yml
wget -O ngrok.yml  https://github.com/Bitblazers-lk/Overseer-Extras/raw/ngrok/ngrok.yml

touch auth.yml

chmod +x ngrok

echo Setup ngrok completed

# ------------------------------
rm -f the.lock