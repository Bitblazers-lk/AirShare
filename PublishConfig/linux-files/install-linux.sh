#!/bin/bash

sudo mkdir -p /usr/share/AirShare/AirShareRelease/
sudo mkdir -p /usr/local/bin/

sudo cp -r ./* /usr/share/AirShare/AirShareRelease/
sudo chmod +x /usr/share/AirShare/AirShareRelease/AirShare

sudo cp -r ./shell-installed/* /usr/local/bin/

sudo chmod +x /usr/local/bin/airshare
sudo chmod +x /usr/local/bin/airshare-update
sudo chmod +x /usr/local/bin/airshare-autostart

echo "Completed"
