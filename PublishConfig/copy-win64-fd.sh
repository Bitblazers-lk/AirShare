#!/bin/bash

cd AirShareRelease
git switch windows64fd
cd ..

mv Publish/win64-fd/ win64-fd/

mkdir -p win64-fd/ 

mv AirShareRelease/.git win64-fd/
mv AirShareRelease/Readme.md win64-fd/

rm -r AirShareRelease

mv win64-fd/ AirShareRelease

cd AirShareRelease
git add .
