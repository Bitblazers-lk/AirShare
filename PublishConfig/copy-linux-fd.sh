#!/bin/bash

cd AirShareRelease
git switch linux64fd
cd ..

mv Publish/linux-fd linux-fd

mkdir -p linux-fd/

mv AirShareRelease/.git linux-fd/
mv AirShareRelease/Readme.md linux-fd/

rm -r AirShareRelease

mv linux-fd AirShareRelease

cd AirShareRelease/

git update-index --add --chmod=+x AirShare
git update-index --add --chmod=+x scripts/*.sh
git add .

