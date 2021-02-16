#!/bin/bash

cd AirShareRelease
git switch osx64-sc
cd ..

mv Publish/osx64-sc/ osx64-sc/

mkdir -p osx64-sc/

mv AirShareRelease/.git osx64-sc/
mv AirShareRelease/Readme.md osx64-sc/

rm -r AirShareRelease

mv osx64-sc/ AirShareRelease

cd AirShareRelease
git add .
