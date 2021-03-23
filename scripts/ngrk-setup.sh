#!/bin/bash


mkdir -p sand/ngrok
cd sand/ngrok


FILE=ngrok.lock
if [ -f "$FILE" ]; then
    echo "Already running"
    exit 1
else 
    touch $FILE
fi
# ------------------------------


wget -c -O ngrok  https://github.com/Bitblazers-lk/Overseer-Extras/raw/ngrok/ngrok

rm ngrok.yml
wget -O ngrok.yml  https://github.com/Bitblazers-lk/Overseer-Extras/raw/ngrok/ngrok.yml

touch auth.yml

chmod +x ngrok

echo Setup ngrok completed

# ------------------------------
rm -f $FILE