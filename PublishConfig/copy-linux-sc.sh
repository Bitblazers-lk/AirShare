#!/bin/bash

cd AirShareRelease
git switch linux64sc
cd ..

mv Publish/linux-sc/ linux-sc/

mkdir -p linux-sc/

mv AirShareRelease/.git linux-sc/
mv AirShareRelease/Readme.md linux-sc/

rm -r AirShareRelease

mv linux-sc AirShareRelease

cd AirShareRelease/

git update-index --add --chmod=+x AirShare
git update-index --add --chmod=+x scripts/*.sh
git add .
