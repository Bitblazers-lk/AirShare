#!/bin/bash

cd AirShareRelease
git switch windows64sc
cd ..

mv Publish/win64-sc/ win64-sc/

mkdir -p win64-sc/

mv AirShareRelease/.git win64-sc/
mv AirShareRelease/Readme.md win64-sc/

rm -r AirShareRelease

mv win64-sc/ AirShareRelease

cd AirShareRelease
git add .
