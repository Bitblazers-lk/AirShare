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

./copy-linux-configs.sh
